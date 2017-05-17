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
using System.Text;
using System.Xml;

namespace Syrinx2
{
    public class BaseMenu : IMenu
    {
        public enum SubMenuShowEffect { display, fade, slide, toggle };
        public enum DisplayOrientation { Horizontal, Vertical };

        protected DisplayOrientation m_orient = DisplayOrientation.Horizontal;
        protected SubMenuShowEffect m_subMenuShowEffect = SubMenuShowEffect.display;
        protected int m_numLayers, m_itemNumLayers, m_subMenuNumLayers, m_subMenuItemNumLayers;
        protected string m_primaryClassName, m_className, m_itemClassName, m_subMenuClassName, m_subMenuItemClassName;
        protected List<IBasicMenuItem> m_items = new List<IBasicMenuItem>();
        protected string m_externalLinkDefaultTarget;
        protected string m_currentPageBaseUrl;
        protected string m_horzSubMenuIndicatorClass, m_vertSubMenuIndicatorClass;
        protected string m_defaultMenuItemTemplate = null;
        protected bool m_generateUL = false;
        protected string m_clientID = "";
        protected string m_id = "";
        protected ConvertUrl m_converter;

        public BaseMenu(string id, ConvertUrl converter)
        {
            m_id = id;
            m_converter = converter;
        }

        public List<IBasicMenuItem> Items { get { return m_items; } }
        
        public virtual void OnLoad()
        {
            onLoadMenuSetup(Items);
        }

        protected virtual void onLoadMenuSetup(List<IBasicMenuItem> items)
        {
            foreach (IBasicMenuItem bmi in items)
            {
                bmi.onLoadMenuSetup(this);
                IMenuItem mi = bmi as IMenuItem;
                if (mi != null && mi.Items.Count > 0)
                    onLoadMenuSetup(mi.Items);
            }
        }

        public string Id { get { return m_id; } set { m_id = value; } }
        public string ClientID { get { return m_clientID; } set { m_clientID = value; } }


        public bool GenerateUL { get { return m_generateUL; } set { m_generateUL = value; } }

        public string HorzSubMenuIndicatorClass { get { return m_horzSubMenuIndicatorClass; } set { m_horzSubMenuIndicatorClass = value; } }

        public string VertSubMenuIndicatorClass { get { return m_vertSubMenuIndicatorClass; } set { m_vertSubMenuIndicatorClass = value; } }

        public DisplayOrientation Orientation { get { return m_orient; } set { m_orient = value; } }

        public SubMenuShowEffect MenuShowEffect { get { return m_subMenuShowEffect; } set { m_subMenuShowEffect = value; } }

        public int NumLayers { get { return m_numLayers; } set { m_numLayers = value; } }

        public int ItemNumLayers { get { return m_itemNumLayers; } set { m_itemNumLayers = value; } }

        public int SubMenuNumLayers { get { return m_subMenuNumLayers; } set { m_subMenuNumLayers = value; } }

        public int SubMenuItemNumLayers { get { return m_subMenuItemNumLayers; } set { m_subMenuItemNumLayers = value; } }

        public string MenuItemTemplate { get { return m_defaultMenuItemTemplate; } set { m_defaultMenuItemTemplate = value; } }

        public string CssClass { get { return m_className; } set { m_className = value; } }

        public string PrimaryCssClass { get { return m_primaryClassName; } set { m_primaryClassName = value; } }

        public string ItemCssClass { get { return m_itemClassName; } set { m_itemClassName = value; } }

        public string SubMenuCssClass { get { return m_subMenuClassName; } set { m_subMenuClassName = value; } }

        public string SubMenuItemCssClass { get { return m_subMenuItemClassName; } set { m_subMenuItemClassName = value; } }

        public string ExternalLinkDefaultTarget { get { return m_externalLinkDefaultTarget; } set { m_externalLinkDefaultTarget = value; } }





        public virtual void Render(TextWriter writer)
        {
            if (GenerateUL)
                renderMenuAreaUL(writer, Items, NumLayers, PrimaryCssClass, CssClass, ItemNumLayers, ItemCssClass, ClientID);
            else
                renderMenuArea(writer, Items, Orientation, NumLayers, PrimaryCssClass, CssClass, ItemNumLayers, ItemCssClass, ClientID);
        }

        protected virtual void renderMenuArea(TextWriter writer, List<IBasicMenuItem> items, DisplayOrientation orient, int numLayers, string primaryCssClass, string cssClass, int itemNumLayers, string itemCssClass, string firstDivId)
        {
            renderStartLayers(writer, numLayers, primaryCssClass, cssClass, null, firstDivId);
            writer.Write("<table border='0' cellspacing='0' cellpadding='0'>");
            if (orient == DisplayOrientation.Horizontal)
                writer.Write("<tr>");
            bool includeImgSpan = shouldIncludeImageSpanInSubmenu(items);

            renderMenuItems(writer, items, orient, numLayers, cssClass, itemNumLayers, itemCssClass, includeImgSpan);

            if (orient == DisplayOrientation.Horizontal)
                writer.Write("</tr>");
            writer.Write("</table>");
            renderEndLayers(writer, numLayers);
        }

        protected virtual void renderMenuAreaUL(TextWriter writer, List<IBasicMenuItem> items, int numLayers, string primaryCssClass, string cssClass, int itemNumLayers, string itemCssClass, string firstDivId)
        {
            writer.Write(string.Format("<div class='{0}'>", primaryCssClass));
            renderMenuItemsUL(writer, items, cssClass, true, ClientID);
            writer.Write("</div>");
        }

        protected virtual void renderMenuItemsUL(TextWriter writer, List<IBasicMenuItem> items, string ulClass, bool includeUl, string ulID)
        {
            if (includeUl)
            {
                writer.Write("<ul");
                if (!string.IsNullOrEmpty(ulClass))
                    writer.Write(string.Format(" class='{0}'", ulClass));
                if (!string.IsNullOrEmpty(ulID))
                    writer.Write(string.Format(" id='{0}'", ulID));
                writer.Write(">");
            }

            for (int pos = 0; pos < items.Count; pos++)
            {
                IBasicMenuItem bmi = items[pos];
                bmi.setupMenuItem(this);
                if (bmi.Visible)
                {
                    IMenuItem mi = bmi as IMenuItem;
                    string extraAnchorAttribs = "";
                    string extraLiAttribs = " class='dropdown'";
                    if (mi != null && mi.Items.Count != 0)
                        extraAnchorAttribs = "class='dropdown-toggle' data-toggle='dropdown'";

                    writer.Write(string.Format("<li{3}><a href='{0}' {2}>{1}", getMenuItemNavUrl(mi), mi.Text, extraAnchorAttribs, extraLiAttribs));
                    if (!String.IsNullOrEmpty(mi.Description))
                        writer.Write(string.Format("<span>{0}</span>", mi.Description));
                    writer.Write("</a>");

                    if (mi != null && mi.Items.Count != 0)
                        renderMenuItemsUL(writer, mi.Items, this.SubMenuCssClass /*null*/, mi.ChildLayout == ChildLayoutType.UseSubMenus, null);
                    writer.Write("</li>");
                }
            }

            if (includeUl)
                writer.Write("</ul>");
        }

        protected virtual void renderMenuItems(TextWriter writer, List<IBasicMenuItem> items, DisplayOrientation orient, int numLayers, string cssClass, int itemNumLayers, string itemCssClass, bool includeImgSpan)
        {
            for (int pos = 0; pos < items.Count; pos++)
            {
                IBasicMenuItem bmi = items[pos];
                bmi.setupMenuItem(this);
                if (bmi.Visible)
                {
                    IMenuItem mi = bmi as IMenuItem;
                    if (orient == DisplayOrientation.Vertical)
                        writer.Write("<tr>");

                    string tdClass = "";
                    if (mi != null && mi.Items.Count != 0)
                        tdClass = string.Format(" class='{0}'", orient == DisplayOrientation.Horizontal ? HorzSubMenuIndicatorClass : VertSubMenuIndicatorClass);

                    writer.Write("<td{0}>", tdClass);
                    if (mi != null && mi.Items.Count != 0)
                        writer.Write("<div class='{0} SubMenuParent' style='width:100%;position:relative;'>", orient == DisplayOrientation.Horizontal ? "VertMenu" : "HorzMenu");
                    string extraClass = "";
                    if (pos == 0)
                        if (items.Count == 1)
                            extraClass = "FirstChild LastChild";
                        else
                            extraClass = "FirstChild";
                    else if (pos == items.Count - 1)
                        extraClass = "LastChild";
                    if (mi != null && isMenuItemSelectedPage(mi))
                        extraClass += " Selected";
                    if (bmi.CssClass != null)
                        extraClass += " " + bmi.CssClass;

                    if (bmi.RenderInItemLayers)
                        renderStartLayers(writer, itemNumLayers, "", itemCssClass, extraClass, mi.ID);
                    if (mi != null)
                        writer.Write(generateMenuItemAnchor(mi, getMenuItemHtml(includeImgSpan, mi)));
                    else
                        writer.Write(getMenuItemHtml(includeImgSpan, bmi));

                    if (bmi.RenderInItemLayers)
                        renderEndLayers(writer, itemNumLayers);
                    if (mi != null && mi.Items.Count != 0)
                    {
                        if (mi.ChildLayout == ChildLayoutType.UseSubMenus)
                        {
                            string extraSubMenuClass = "";
                            if (MenuShowEffect != SubMenuShowEffect.display)
                                extraSubMenuClass = MenuShowEffect.ToString() + "Menu";
                            writer.Write("</div><div class='SubMenu {0} {1}' style='z-index:9000;position:absolute;display:none;'>", extraSubMenuClass, bmi.CssClass != null ? bmi.CssClass : "");
                            if (mi.RenderOwnSubMenu)
                            {
                                renderStartLayers(writer, SubMenuNumLayers, "", SubMenuCssClass, null, null);
                                mi.renderSubMenu(this, writer);
                                renderEndLayers(writer, SubMenuNumLayers);

                            }
                            else
                                renderMenuArea(writer, mi.Items, DisplayOrientation.Vertical, SubMenuNumLayers, "", SubMenuCssClass, SubMenuItemNumLayers, SubMenuItemCssClass, null);
                            writer.Write("</div>");
                        }
                        else if (mi.ChildLayout == ChildLayoutType.AsSiblings)
                        {
                            renderMenuItems(writer, mi.Items, orient, numLayers, cssClass, itemNumLayers, itemCssClass, includeImgSpan);
                        }
                    }
                    writer.Write("</td>");
                    if (orient == DisplayOrientation.Vertical)
                        writer.Write("</tr>");
                }
            }
        }

        public virtual string generateMenuItemAnchor(IMenuItem mi, string menuItemHtml)
        {
            string target = calcTargetName(mi);
            if (target.Length != 0)
                target = string.Format(" target='{0}'", target);
            string clientClick = mi.ClientClick;
            if (clientClick == null)
                clientClick = "";
            else
                clientClick = string.Format(" onclick='{0}'", clientClick.Replace("'", "\\'"));

            string extraAncAttribs = getMenuItemAnchorAttribs(mi);

            return string.Format("<a href='{0}'{2}{3}{4} style='display:block;white-space:nowrap;'><span style='white-space:nowrap;cursor:pointer;width:100%;display:block;'>{1}</span></a>",
                            mi.getCurrentNavigateUrl() == null ? "javascript:;" : getMenuItemNavUrl(mi), menuItemHtml, target, clientClick, extraAncAttribs);
        }

        protected virtual string getMenuItemAnchorAttribs(IMenuItem mi)
        {
            if(mi.ExtraAnchorAttributes == null || mi.ExtraAnchorAttributes.Count == 0)
                return "";

            StringBuilder buff = new StringBuilder();
            foreach (KeyValuePair<string, string> kvp in mi.ExtraAnchorAttributes)
            {
                buff.Append(" ").Append(kvp.Key).Append("='").Append(kvp.Value).Append("'");
            }
            return buff.ToString();
        }

        protected virtual bool isMenuItemSelectedPage(IMenuItem mi)
        {
            bool isIt = false;
            string nav = getMenuItemNavUrl(mi);
            if (nav != null)
                isIt = nav.EndsWith(CurrentPageBaseUrl, StringComparison.CurrentCultureIgnoreCase);

            return isIt;
        }

        protected virtual string CurrentPageBaseUrl
        {
            get
            {
                if (m_currentPageBaseUrl == null && System.Web.HttpContext.Current != null)
                {
                    m_currentPageBaseUrl = System.Web.HttpContext.Current.Request.RawUrl;
                    int i = m_currentPageBaseUrl.IndexOf('?');
                    if (i > 0)
                        m_currentPageBaseUrl = m_currentPageBaseUrl.Substring(0, i);
                }
                return m_currentPageBaseUrl == null?"":m_currentPageBaseUrl;
            }
        }

        protected virtual bool shouldIncludeImageSpanInSubmenu(List<IBasicMenuItem> menu)
        {
            bool should = false;
            foreach (IBasicMenuItem mi in menu)
                if (mi is IMenuItem && (should = ((IMenuItem)mi).ImageUrl != null))
                    break;
            return should;
        }

        protected virtual string getMenuItemHtml(bool includeImgSpan, IBasicMenuItem mi)
        {
            return mi.getMenuItemHtml(this, includeImgSpan, MenuItemTemplate);
        }

        protected virtual string getMenuItemNavUrl(IMenuItem mi)
        {
            string navUrl = mi.getCurrentNavigateUrl();
            return navUrl == null ? null : m_converter(navUrl).Replace("'", "\"");
        }

        protected virtual string calcTargetName(IMenuItem mi)
        {
            string target = mi.Target;
            if ((target == null || target.Length == 0) &&
                 mi.NavigateUrl != null && mi.NavigateUrl.IndexOf(':') != -1 && ExternalLinkDefaultTarget != null)
                target = ExternalLinkDefaultTarget;
            return target == null ? "" : target;
        }

        protected static void renderStartLayers(TextWriter writer, int numLayers, string primaryCssClass, string cssClass, string extraCssClass, string firstDivId)
        {
            string firstClass = cssClass;
            if (extraCssClass != null)
                firstClass += " " + extraCssClass;

            if (firstDivId != null)
                writer.Write("<div id='{1}' class='{2} {0}'>", firstClass, firstDivId, primaryCssClass);
            else
                writer.Write("<div class='{0}'>", firstClass);

            char p = 'a';
            for (int pos = 0; pos < numLayers - 1; pos++)
                writer.Write("<div class='{0}{1}'>", cssClass, (char)(p + pos));

            if (numLayers > 0)
            {
                writer.Write("<div class='{0}-c' >", cssClass);
                writer.Write("<div class='{0}-w'></div>", cssClass);
            }
        }

        protected static void renderEndLayers(TextWriter writer, int numLayers)
        {
            writer.Write("</div>");
            for (int pos = 0; pos < numLayers; pos++)
                writer.Write("</div>");
        }

    }
    public delegate string ConvertUrl(string url);
}
