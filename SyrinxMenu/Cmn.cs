//Copyright 2009-1012 Syrinx Development, Inc.
//This file is part of the Syrinx Web Menu.
// == BEGIN LICENSE ==
//
// Licensed under the terms of any of the following licenses at your
// choice:
//
//  - GNU General Public License Version 3 or later (the "GPL")
//    http://www.gnu.org/licenses/gpl.html
//
//  - GNU Lesser General Public License Version 3 or later (the "LGPL")
//    http://www.gnu.org/licenses/lgpl.html
//
//  - Mozilla Public License Version 1.1 or later (the "MPL")
//    http://www.mozilla.org/MPL/MPL-1.1.html
//
// == END LICENSE ==
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Syrinx2
{
    public class Cmn
    {
        protected static Dictionary<string, Type> s_classTypes = new Dictionary<string, Type>();
        /// <summary>Calls the constructor of the class named <c>classname</c>.
        /// Passes the array of <c>paramValues</c> (which are of the classes
        /// found in the array <c>paramTypes</c>) to the method.
        /// </summary>
        /// <returns>the constructed object.
        /// 
        /// </returns>
        public static object callCtor(string classname, Type[] paramTypes, object[] paramValues)
        {
            object ans = null;

            Type classType = findType(classname);
            if (classType != null)
                ans = createType(classType, paramTypes, paramValues, true);
            else
                throw new ReflectionException(classname, "Able to load assembly, but cannot find class with given name");
            return ans;
        }

        public static object createType(Type theType, string theVal)
        {
            return createType(theType, new Type[] { typeof(string) }, new object[] { theVal }, true);
        }

        public static object createType(Type classType, Type[] paramTypes, object[] paramValues, bool throwException)
        {
            MethodBase ctor = null;
            object ans = null;
            if (classType.FullName == "System.String")
            {
                if (paramValues.Length > 0)
                {
                    if (paramValues[0] == null)
                        return null; //Out of place to return here, but we need to short-cut things when a string is being created with a null, which means a null should be returned as well.
                    else
                        return paramValues[0].ToString();
                }
            }
            if (!classType.IsValueType)
                ctor = classType.GetConstructor(paramTypes);
            else
            {
                if (classType.IsEnum)
                {
                    if (paramValues.Length != 0)
                        return System.Enum.Parse(classType, paramValues[0].ToString(), true);
                    else
                        return null; //TODO: Is it really appropriate to return null when trying to create a enum type but didn't specify the enum string to parse?
                }
                else
                {
                    //This chunk of code will try to call a static method called Parse
                    //if the classType is a value type.  Many .NET value types have
                    //a Parse method rather than a constructor.
                    ctor = classType.GetMethod("Parse", new Type[] { typeof(String) });
                    if (paramValues.Length == 0)
                    {
                        if (classType == typeof(Int32))
                            return 0;
                        else if (classType == typeof(Int16))
                            return (Int16)0;
                        else if (classType == typeof(Int64))
                            return (Int64)0;
                        else if (classType == typeof(Double))
                            return (Double)0.0;
                        else if (classType == typeof(bool))
                            return false;
                        else if (classType == typeof(byte))
                            return (byte)0;
                        else if (classType == typeof(DateTime))
                            return new DateTime();
                        else if (classType == typeof(char))
                            return (char)0;
                        paramValues = new object[] { "" };
                    }
                    else if (paramValues.Length != 1)
                    {
                        //This is done because calling toString on datetime will clip milliseconds and the
                        //newly created date wont match the given date exactly (it would down to the second only).
                        //This check avoids that scenario.
                        if (classType == typeof(DateTime) && paramValues[0] is DateTime)
                            return paramValues[0];

                        paramValues = new object[] { paramValues[0].ToString() };
                    }
                }
            }
            if (ctor == null)
            {
                //Because Class.getConstructor only matches exact types and not "kind ofs", we
                //will look for a matching constructor with looser requirements
                System.Reflection.ConstructorInfo[] ctors = classType.GetConstructors();
                for (int pos = 0; pos < ctors.Length; ++pos)
                {
                    try
                    {
                        if (ctors[pos].GetParameters().Length == paramValues.Length)
                            return ctors[pos].Invoke(paramValues);
                    }
                    catch (Exception)
                    {

                        //Intentionally left blank, try the next contrstructor...
                    }
                }
                //We didn't successfully call a constructor so rethrow the error.
                if (throwException)
                    throw new ReflectionException(classType.Name,
                        String.Format("Able to load assembly, and find class type, but cannot find constructor or 'Parse' method that takes the param types '{0}'", paramTypes));
            }
            else
            {
                try
                {
                    if (ctor.IsConstructor)
                        ans = ((ConstructorInfo)ctor).Invoke(paramValues);
                    else if (ctor.IsStatic) //try and call as a static method to create
                        ans = ctor.Invoke(null, paramValues);
                }
                catch (Exception e)
                {
                    //The above code attempted to create an instance of the object given a null param value, which failed.
                    //This code will return null based on that situation.
                    if (paramValues != null && paramValues.Length > 0 && paramValues[0] == null)
                        return null;
                    if (e is TargetInvocationException && e.InnerException != null)
                        e = e.InnerException;
                    if (throwException)
                        throw new ReflectionException(e, classType.Name, "Unable to create instance of given class type because an exception was thrown during construction");
                }
            }

            return ans;
        }


        public static object callPropertyGet(object onThis, string name, object[] indexerParam, bool throwException)
        {
            System.Type clas = null;
            System.Reflection.PropertyInfo member = null;
            System.Reflection.FieldInfo fieldMember = null;
            try
            {
                clas = onThis.GetType();
                if (clas.Name.Equals("__ComObject"))
                {
                    return clas.InvokeMember(name, BindingFlags.Instance | BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.NonPublic, null, onThis, indexerParam);
                }
                member = clas.GetProperty(name, BindingFlags.Instance | BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.NonPublic);
                if (member == null)
                    fieldMember = clas.GetField(name, BindingFlags.Instance | BindingFlags.IgnoreCase | BindingFlags.NonPublic | BindingFlags.Public);
                if (member != null)
                    return member.GetValue(onThis, indexerParam);
                else if (fieldMember != null)
                    return fieldMember.GetValue(onThis);
                else if (throwException)
                    throw new ReflectionException(clas.FullName, name);
            }
            catch (ReflectionException e) { throw; }
            catch (Exception e)
            {
                if (throwException)
                {
                    string className = (clas == null) ? "(unknown class)" : clas.FullName;
                    string signature = (member == null) ? className + "." + name : member.ToString();
                    throw new ReflectionException(e, className, signature);
                }
            }
            return null;
        }

        public static bool callPropertySet(object onThis, string name, object val, bool convertValue, bool throwOnError)
        {
            bool setValue = false;
            System.Type clas = null;
            System.Reflection.PropertyInfo member = null;
            System.Reflection.FieldInfo fieldMember = null;
            try
            {
                clas = onThis.GetType();
                member = clas.GetProperty(name, BindingFlags.Instance | BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.NonPublic);
                if (member == null)
                    fieldMember = clas.GetField(name, BindingFlags.Instance | BindingFlags.IgnoreCase | BindingFlags.NonPublic | BindingFlags.Public);
                if (convertValue && member != null && val != null && member.PropertyType != val.GetType())
                {
                    val = Cmn.callCtor(member.PropertyType.FullName, new Type[] { val.GetType() }, new object[] { val });
                }
                try
                {
                    if (member != null)
                    {
                        member.SetValue(onThis, val, null);
                        setValue = true;
                    }
                    else if (fieldMember != null)
                    {
                        fieldMember.SetValue(onThis, val);
                        setValue = true;
                    }
                }
                catch (Exception e)
                {
                    string v = val.ToString();
                    int pos = v.LastIndexOf(".");
                    if (pos > 0)
                    {
                        object realVal = Cmn.callStatic(member.PropertyType.AssemblyQualifiedName, v.Substring(pos + 1), new Type[0], new object[0]);
                        member.SetValue(onThis, realVal, null);
                    }
                    else
                        member.SetValue(onThis, v, null);
                }
            }
            //catch (InvalidPropertyException)
            //{
            //    if (throwOnError)
            //        throw;
            //}
            catch (Exception e)
            {
                if (throwOnError)
                {
                    string className = (clas == null) ? "(unknown class)" : clas.FullName;
                    string signature = (member == null) ? className + "." + name : member.ToString();
                    throw new ReflectionException(e, className, signature);
                }
            }
            return setValue;
        }

        public static object callStatic(string classname, string methodname, Type[] paramTypes, object[] paramValues)
        {
            Type ctype = findType(classname);
            //Type ctype = Type.GetType(classname);
            if (ctype != null)
            {
                MemberInfo[] m = ctype.GetMember(methodname, BindingFlags.Static | BindingFlags.Public);
                if (m.Length > 0)
                {
                    if (m[0].MemberType == MemberTypes.Method)
                        return ctype.InvokeMember(methodname, BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Static, null, null, paramValues);
                    //return ((MethodInfo)m[0]).Invoke(null, paramValues);
                    else if (m[0].MemberType == MemberTypes.Property)
                        return ((PropertyInfo)m[0]).GetValue(null, new object[0]);
                    else if (m[0].MemberType == MemberTypes.Field)
                        return ((FieldInfo)m[0]).GetValue(null);
                }
                else
                    throw new ReflectionException(classname, "Able to load assembly, but cannot find static method with given name");
            }
            throw new ReflectionException(classname, "Unable to load assembly for static method call");
        }

        public static Type findType(string classname)
        {
            string mapKey = classname.Replace('.', '-');
            Type classType;
            lock (s_classTypes)
                s_classTypes.TryGetValue(mapKey, out classType);
            if (classType == null)
            {
                classType = Type.GetType(classname);
                if (classType == null)
                {
                    int pos = classname.IndexOf(',');
                    if (pos != -1)
                    {

                        string cname = classname.Substring(0, pos);
                        string assemblyName = classname.Substring(pos + 1);
                        Assembly a = null;

                        if (a == null)
                        {

                            //TODO: This code assumes that assembly names always are in the format
                            //of just the filename minus extension such as .dll.'
                            //#if(!COMPACT_FRAMEWORK)
                            //						string asmName = classname.Substring(pos +1);
                            //						//AssemblyName an = AssemblyName.GetAssemblyName(asmName);
                            //						Assembly a = Assembly.LoadWithPartialName(asmName);
                            //string curDir = Directory.GetCurrentDirectory();
                            //#else
                            string baseDir = new System.IO.FileInfo(typeof(Cmn).Assembly.CodeBase.Substring(8)).DirectoryName;

                            string assemblyExt = assemblyName.Substring(assemblyName.Length - 4);
                            if (String.Compare(assemblyExt, ".dll", false) != 0 &&
                                String.Compare(assemblyExt, ".exe", false) != 0)
                                assemblyName += ".dll";
                            try
                            {
                                string tmpPartialName = classname.Substring(pos + 1);
                                a = Assembly.LoadWithPartialName(tmpPartialName);
                                if (a == null)
                                    a = Assembly.LoadFrom(System.IO.Path.Combine(baseDir, assemblyName));
                            }
                            catch (Exception e)
                            {
                                throw new ReflectionException(classname.Substring(0, pos), String.Format("Unable to load assembly '{0}'.", assemblyName), e);
                            }
                        }
                        //#endif
                        if (a != null)
                            classType = a.GetType(cname);
                        else
                            throw new ReflectionException(cname, String.Format("Unable to load assembly '{0}'.", assemblyName));
                    }
                }
                if (classType != null)
                    lock (s_classTypes)
                        s_classTypes[mapKey] = classType;

            }
            return classType;
        }
    }
    /// <summary>This is the exception that is thrown when something goes wrong within
    /// <c>Cmn.callMethod()</c> and/or <c>Cmn.callCtor()</c>.</summary>
    /// <remarks>
    /// The idea is to "contain" the real <c>Exception</c> that was thrown during
    /// those routines, then provide a way for the caller to extract it again.
    /// This simplifies the caller's code -- it needs only one <c>catch</c> handler.
    /// </remarks>
    [Serializable]
    public class ReflectionException : ApplicationException
    {
        protected internal Exception m_inner;
        protected internal string m_className;
        protected internal string m_signature;

        public ReflectionException(string className, string signature)
            : base(className + ": " + signature)
        {
            m_inner = null;
            m_className = className;
            m_signature = signature;
        }

        public ReflectionException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
        }

        public ReflectionException(string className, string signature, Exception innerException)
            : base(className + ": " + signature, innerException)
        {
            m_className = className;
            m_signature = signature;
        }

        public ReflectionException(Exception except, string className, string signature)
            : base(signature, except)
        {
            m_inner = except;
            m_className = className;
            m_signature = signature;
        }
        /*
                    public override string Message
                    {
                        get
                        {
                            Exception inner = m_inner;
                            if (inner is System.Reflection.TargetInvocationException)
                                inner = (Exception) ((System.Reflection.TargetInvocationException) inner).GetBaseException();
                            return inner.Message;
                        }
				
                    }

        */
        /// <summary>Returns the exception that was actually thrown by the <c>System.Reflection</c> primitives.
        /// </summary>
        public virtual Exception exception()
        {
            return this.InnerException;
        }

        /// <summary>Returns the name of the class that contains the method or constructor that was called.
        /// For example, if calling a method on a <c>Integer</c> object, this would return <c>System.Int32</c>.
        /// </summary>
        public virtual string className()
        {
            return m_className;
        }

        /// <summary>Returns the signature of the method or constructor that was called.
        /// For example, if calling the <c>Parse(String)</c> (static) method
        /// associated with class <c>Int32</c>, this would return something
        /// like <c>System.Int32.Parse(System.String)</c>.</summary>
        /// <remarks>
        /// Note that the specifics of the returned string are not guaranteed,
        /// since if an exception occurred early enough in the process, the
        /// signature may be built by hand.
        /// </remarks>
        public virtual string signature()
        {
            return m_signature;
        }


        /*
        public override string ToString()
        {
            Exception inner = m_inner;
            if (inner is System.Reflection.TargetInvocationException)
                inner = (Exception) ((System.Reflection.TargetInvocationException) inner).GetBaseException();
            return inner.ToString();
        }
        */
    }
}
