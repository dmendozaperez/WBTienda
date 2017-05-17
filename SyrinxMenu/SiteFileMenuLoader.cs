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
using System.Xml;
using System.Web;

namespace Syrinx2
{
    public class SiteFileMenuLoader
    {
        protected static Dictionary<string, Type> s_menuItemTypes = new Dictionary<string, Type>();

        public static void registerMenuItemType(string name, Type t)
        {
            s_menuItemTypes[name] = t;
        }

        static SiteFileMenuLoader()
        {
            registerMenuItemType("MenuItem", typeof(MenuItem));
        }

        public static void loadMenuItems(string id, List<IBasicMenuItem> items, ConvertUrl converter)
        {
            if (!string.IsNullOrEmpty(id))
            {
                string path = System.Web.HttpContext.Current.Server.MapPath("~/Menus/" + id + ".xml");
                if (File.Exists(path))
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(path);
                    processChildren(doc.DocumentElement, items, converter);
                }
            }
        }

        protected static void processChildren(XmlElement el, List<IBasicMenuItem> children, ConvertUrl converter)
        {
            foreach (XmlNode node in el.ChildNodes)
            {
                if (node is XmlElement)
                {
                    XmlElement childEl = (XmlElement)node;
                    IMenuItem item = createItemFromName(childEl.Name);
                    if (item != null)
                    {
                        foreach (XmlAttribute att in childEl.Attributes)
                        {
                            string val = att.Value;
                            if (converter != null && string.Compare(att.Name, "navigateUrl", true) == 0)
                            {
                                val = converter(val);
                            }
                            Cmn.callPropertySet(item, att.Name, val, true, false);
                        }
                        processChildren(childEl, item.Items, converter);
                        children.Add(item);
                    }
                }
            }
        }

        private static Type[] s_emptyTypes = new Type[0];
        private static object[] s_emptyObjs = new object[0];
        protected static IMenuItem createItemFromName(string itemName)
        {
            IMenuItem item = null;

            if (s_menuItemTypes.ContainsKey(itemName))
                item = Cmn.createType(s_menuItemTypes[itemName], s_emptyTypes, s_emptyObjs, false) as IMenuItem;

            return item;
        }
    }
}
