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
using System.Security.Permissions;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;

namespace Syrinx2
{
    [Serializable]
    [ParseChildren(true, "Items")]
    [AspNetHostingPermissionAttribute(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
    [AspNetHostingPermissionAttribute(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
    public class MenuItem : IMenuItem
    {
        protected string m_id;
        protected string m_text, m_description, m_navigateUrl, m_target, m_imageUrl, m_cssClass;
        protected string m_clientClick;
        protected bool m_mustHaveChildrenToShow = false;
        protected bool m_renderInItemLayers = true;
        protected ChildLayoutType m_childLayout = ChildLayoutType.UseSubMenus;
        protected string m_skinId = null;
        protected List<IBasicMenuItem> m_items = new List<IBasicMenuItem>();
        protected Dictionary<string, string> m_anchorAttributes = new Dictionary<string,string>();

        public MenuItem()
        {
        }
        public MenuItem(string text, string navigateUrl)
        {
            m_text = text;
            m_navigateUrl = navigateUrl;
        }
        public MenuItem(string text, string navigateUrl, string target)
        {
            m_text = text;
            m_navigateUrl = navigateUrl;
            m_target = target;
        }

        public virtual string ID { get { return m_id; } set { m_id = value; } }

        public ChildLayoutType ChildLayout { get { return m_childLayout; } set { m_childLayout = value; } }

        public virtual string Text { get { return m_text; } set { m_text = value; } }

        public virtual string Description { get { return m_description; } set { m_description = value; } }

        public string Target { get { return m_target; } set { m_target = value; } }

        public string NavigateUrl { get { return m_navigateUrl; } set { m_navigateUrl = value; } }

        public string ImageUrl { get { return m_imageUrl; } set { m_imageUrl = value; } }

        public string CssClass { get { return m_cssClass; } set { m_cssClass = value; } }

        public virtual string ClientClick { get { return m_clientClick; } set { m_clientClick = value; } }

        public bool MustHaveVisibleChildrenToBeVisible { get { return m_mustHaveChildrenToShow; } set { m_mustHaveChildrenToShow = value; } }

        public Dictionary<string, string> ExtraAnchorAttributes { get { return m_anchorAttributes; } }

        public virtual bool RenderInItemLayers { get { return m_renderInItemLayers; } set { m_renderInItemLayers = value; } }

        public List<IBasicMenuItem> Items { get { return m_items; } }

        public virtual bool RenderOwnSubMenu { get { return false; } }
        public virtual void renderSubMenu(IMenu parent, TextWriter writer) { }


        public virtual bool Visible
        {
            get
            {
                bool visible = true;
                if (MustHaveVisibleChildrenToBeVisible)
                {
                    visible = false;
                    foreach (MenuItem mi in Items)
                        if (mi.Visible)
                        {
                            visible = true;
                            break;
                        }
                }
                return visible;
            }
        }

        public virtual string getCurrentNavigateUrl() { return NavigateUrl; }

        public virtual string getMenuItemHtml(IMenu parent, bool includeImgSpan, string suggestedTemplate)
        {
            string t = Text.Replace("&", "&amp;");
            if (suggestedTemplate != null)
                t = suggestedTemplate.Replace("$text$", Text).Replace("$description$", Description);
            else if (Description != null && Description != "")
                t = string.Format("<span title='{0}'>{1}</span>", Description.Replace("'", "\\'"), t);

            if (ImageUrl == null)
                return includeImgSpan ? "<span class='nimg'></span>" + t : t;

            return string.Format("<img style='border:0;vertical-align:middle;' src='{0}' alt=''/>{1}", ImageUrl, t);
        }

        public virtual void setupMenuItem(IMenu parent)
        {
            //Intentionally left blank - Base menu item has not setup.
        }

        public virtual void onLoadMenuSetup(IMenu parent)
        {
            //Intentionally left blank - Base menu item has not setup.
        }
    }
}
