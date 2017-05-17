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
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Security.Permissions;
using System.Web;
using System.Web.UI;

[assembly: WebResource("Syrinx2.jquery.syrinxmenu.js", "application/x-javascript")]
namespace Syrinx2
{
    [AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Minimal)]
    [Themeable(true)]
    [ParseChildren(true, "Items")]
    public class Menu : UserControl, IMenu
    {
        protected BaseMenu m_menu;

        public Menu()
        {
            m_menu = new BaseMenu(ID, (val) => Page.ResolveUrl(val));
        }

        [Category("Appearance")]
        public bool UseExternalDefinition { get; set; }

        [Category("Appearance")]
        public bool RegisterScripts { get; set; }


        [Category("Appearance")]
        public bool GenerateUL { get { return m_menu.GenerateUL; } set { m_menu.GenerateUL = value; } }

        [Category("Appearance")]
        public string HorzSubMenuIndicatorClass { get { return m_menu.HorzSubMenuIndicatorClass; } set { m_menu.HorzSubMenuIndicatorClass = value; } }

        [Category("Appearance")]
        public string VertSubMenuIndicatorClass { get { return m_menu.VertSubMenuIndicatorClass; } set { m_menu.VertSubMenuIndicatorClass = value; } }


        [Category("Appearance")]
        public BaseMenu.DisplayOrientation Orientation { get { return m_menu.Orientation; } set { m_menu.Orientation = value; } }

        [Category("Appearance")]
        public BaseMenu.SubMenuShowEffect MenuShowEffect { get { return m_menu.MenuShowEffect; } set { m_menu.MenuShowEffect = value; } }

        [Category("Appearance")]
        public int NumLayers { get { return m_menu.NumLayers; } set { m_menu.NumLayers = value; } }

        [Category("Appearance")]
        public int ItemNumLayers { get { return m_menu.ItemNumLayers; } set { m_menu.ItemNumLayers = value; } }

        [Category("Appearance")]
        public int SubMenuNumLayers { get { return m_menu.SubMenuNumLayers; } set { m_menu.SubMenuNumLayers = value; } }

        [Category("Appearance")]
        public int SubMenuItemNumLayers { get { return m_menu.SubMenuItemNumLayers; } set { m_menu.SubMenuItemNumLayers = value; } }

        [Category("Appearance")]
        public string MenuItemTemplate { get { return m_menu.MenuItemTemplate; } set { m_menu.MenuItemTemplate = value; } }

        [Category("Appearance")]
        public string CssClass { get { return m_menu.CssClass; } set { m_menu.CssClass = value; } }

        [Category("Appearance")]
        public string PrimaryCssClass { get { return m_menu.PrimaryCssClass; } set { m_menu.PrimaryCssClass = value; } }

        [Category("Appearance")]
        public string ItemCssClass { get { return m_menu.ItemCssClass; } set { m_menu.ItemCssClass = value; } }

        [Category("Appearance")]
        public string SubMenuCssClass { get { return m_menu.SubMenuCssClass; } set { m_menu.SubMenuCssClass = value; } }

        [Category("Appearance")]
        public string SubMenuItemCssClass { get { return m_menu.SubMenuItemCssClass; } set { m_menu.SubMenuItemCssClass = value; } }

        public string ExternalLinkDefaultTarget { get { return m_menu.ExternalLinkDefaultTarget; } set { m_menu.ExternalLinkDefaultTarget = value; } }

        public List<IBasicMenuItem> Items
        {
            get { return m_menu.Items; }
        }

        public string generateMenuItemAnchor(IMenuItem mi, string menuItemHtml)
        {
            return m_menu.generateMenuItemAnchor(mi, menuItemHtml);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            m_menu.Id = ID;

            if (UseExternalDefinition)
                SiteFileMenuLoader.loadMenuItems(m_menu.Id, Items, (val) => Page.ResolveUrl(val));

            m_menu.OnLoad();
        }

        protected override void Render(HtmlTextWriter writer)
        {
            m_menu.Render(writer);
        }


        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            if (RegisterScripts)
            {
                string scriptUrl = Page.ClientScript.GetWebResourceUrl(typeof(Menu), "Syrinx2.jquery.syrinxmenu.js");
                Page.ClientScript.RegisterClientScriptInclude("Swaf.MenuScript", scriptUrl);

                Page.ClientScript.RegisterStartupScript(typeof(Menu), "SyrinxMenuSetup", "$('.syrinx-menu').syrinxMenu({});", true);
            }
        }
    }
}
