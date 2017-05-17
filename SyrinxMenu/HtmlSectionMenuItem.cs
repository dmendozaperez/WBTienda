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
using System.Text;
using System.Web;
using System.Web.UI;

namespace Syrinx2
{
    [Serializable]
    [ParseChildren(true, "Html")]
    public class HtmlSection : IBasicMenuItem
    {
        protected string m_id;
        protected string m_html, m_cssClass;
        protected string m_clientClick;
        protected bool m_visible = true;
        protected bool m_renderInItemLayers = true;

        public virtual string ID { get { return m_id; } set { m_id = value; } }

        [PersistenceModeAttribute(PersistenceMode.EncodedInnerDefaultProperty)]
        public string Html { get { return m_html; } set { m_html = value; } }

        public virtual bool RenderOwnSubMenu { get { return false; } }

        public virtual bool RenderInItemLayers { get { return m_renderInItemLayers; } set { m_renderInItemLayers = value; } }

        public virtual string ClientClick { get { return m_clientClick; } set { m_clientClick = value; } }

        public virtual bool Visible
        {
            get
            {
                return m_visible;
            }
            set
            {
                m_visible = false;
            }
        }

        public string CssClass { get { return m_cssClass; } set { m_cssClass = value; } }

        public virtual string getMenuItemHtml(IMenu parent, bool includeImgSpan, string suggestedTemplate)
        {
            return includeImgSpan ? "<span class='nimg'></span>" + Html : Html;
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
