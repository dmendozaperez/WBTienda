
//Copyright 1997-2010 Syrinx Development, Inc.
//This file is part of the Syrinx Web Application Framework (SWAF).
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
using System.Text;

namespace Syrinx
{
	public interface IMenu
	{
		List<IBasicMenuItem> Items { get; }

		string generateMenuItemAnchor(IMenuItem mi, string menuItemHtml);
	}

	public interface IBasicMenuItem
	{
		string ID { get; }

		bool Visible { get; }

		string CssClass { get; set; }

		string getMenuItemHtml(IMenu parent, bool includeImgSpan, string suggestedTemplate);

		void setupMenuItem(IMenu parent);

		void onLoadMenuSetup(IMenu parent);

		bool RenderInItemLayers { get; set; }
	}

	public enum ChildLayoutType { UseSubMenus, AsSiblings, AsSingleSubMenu, AsThisItem };
	public interface IMenuItem : IBasicMenuItem
	{
		ChildLayoutType ChildLayout { get; set; }

		string Text { get; set; }

		string Description { get; set; }

		string Target { get; set; }

		string NavigateUrl { get; set; }

		string ImageUrl { get; set; }

		string ClientClick { get; set; }

		bool MustHaveVisibleChildrenToBeVisible { get; set; }

		List<IBasicMenuItem> Items { get; }

		string getCurrentNavigateUrl();

		bool RenderOwnSubMenu { get; }

		void renderSubMenu(IMenu parent, System.Web.UI.HtmlTextWriter writer);
	}
}
