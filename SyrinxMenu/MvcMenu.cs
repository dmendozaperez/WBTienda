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
using System.Web;
using System.Xml.Serialization;

namespace Syrinx2
{
    public class MvcMenu
    {
        protected BaseMenu m_menu;

        public MvcMenu(string id, ConvertUrl converter)
        {
            m_menu = new BaseMenu(id, converter);
            SiteFileMenuLoader.loadMenuItems(m_menu.Id, Items, converter);
        }

        public string Id { get { return m_menu.Id; } set { m_menu.Id = value; } }

        public List<IBasicMenuItem> Items { get { return m_menu.Items; } }

        public bool GenerateUL { get { return m_menu.GenerateUL; } set { m_menu.GenerateUL = value; } }

        public string HorzSubMenuIndicatorClass { get { return m_menu.HorzSubMenuIndicatorClass; } set { m_menu.HorzSubMenuIndicatorClass = value; } }

        public string VertSubMenuIndicatorClass { get { return m_menu.VertSubMenuIndicatorClass; } set { m_menu.VertSubMenuIndicatorClass = value; } }

        public BaseMenu.DisplayOrientation Orientation { get { return m_menu.Orientation; } set { m_menu.Orientation = value; } }

        public BaseMenu.SubMenuShowEffect MenuShowEffect { get { return m_menu.MenuShowEffect; } set { m_menu.MenuShowEffect = value; } }

        public int NumLayers { get { return m_menu.NumLayers; } set { m_menu.NumLayers = value; } }

        public int ItemNumLayers { get { return m_menu.ItemNumLayers; } set { m_menu.ItemNumLayers = value; } }

        public int SubMenuNumLayers { get { return m_menu.SubMenuNumLayers; } set { m_menu.SubMenuNumLayers = value; } }

        public int SubMenuItemNumLayers { get { return m_menu.SubMenuItemNumLayers; } set { m_menu.SubMenuItemNumLayers = value; } }

        public string MenuItemTemplate { get { return m_menu.MenuItemTemplate; } set { m_menu.MenuItemTemplate = value; } }

        public string CssClass { get { return m_menu.CssClass; } set { m_menu.CssClass = value; } }

        public string PrimaryCssClass { get { return m_menu.PrimaryCssClass; } set { m_menu.PrimaryCssClass = value; } }

        public string ItemCssClass { get { return m_menu.ItemCssClass; } set { m_menu.ItemCssClass = value; } }

        public string SubMenuCssClass { get { return m_menu.SubMenuCssClass; } set { m_menu.SubMenuCssClass = value; } }

        public string SubMenuItemCssClass { get { return m_menu.SubMenuItemCssClass; } set { m_menu.SubMenuItemCssClass = value; } }

        public string ExternalLinkDefaultTarget { get { return m_menu.ExternalLinkDefaultTarget; } set { m_menu.ExternalLinkDefaultTarget = value; } }


        public string Html
        {
            get
            {
                StringWriter writer = new StringWriter();
                m_menu.Render(writer);
                return writer.ToString();
            }
        }
    }
}
