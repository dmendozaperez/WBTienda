
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
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Security.Permissions;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


[assembly: WebResource("SyrinxMenu.script.Menu.js", "application/x-javascript")]
namespace Syrinx.Gui.AspNet
{
	[AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Minimal)]
	[Themeable(true)]
	[ParseChildren(true, "Items")]
	public class Menu : UserControl, IMenu
	{
		public enum SubMenuShowEffect { display, fade, slide, toggle };

		protected Orientation m_orient = Orientation.Horizontal;
		protected SubMenuShowEffect m_subMenuShowEffect = SubMenuShowEffect.display;
		protected int m_numLayers, m_itemNumLayers, m_subMenuNumLayers, m_subMenuItemNumLayers;
		protected string m_className, m_itemClassName, m_subMenuClassName, m_subMenuItemClassName;
		protected List<IBasicMenuItem> m_items = new List<IBasicMenuItem>();
		protected string m_externalLinkDefaultTarget;
		protected string m_currentPageBaseUrl;
		protected string m_horzSubMenuIndicatorClass, m_vertSubMenuIndicatorClass;
		protected string m_defaultMenuItemTemplate = null;

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			string scriptUrl = Page.ClientScript.GetWebResourceUrl(typeof(Menu), "SyrinxMenu.script.Menu.js");
			Page.ClientScript.RegisterClientScriptInclude("Swaf.MenuScript", scriptUrl);
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
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
		[Category("Appearance")]
		public string HorzSubMenuIndicatorClass { get { return m_horzSubMenuIndicatorClass; } set { m_horzSubMenuIndicatorClass = value; } }

		[Category("Appearance")]
		public string VertSubMenuIndicatorClass { get { return m_vertSubMenuIndicatorClass; } set { m_vertSubMenuIndicatorClass = value; } }


		[Category("Appearance")]
		public Orientation Orientation { get { return m_orient; } set { m_orient = value; } }

		[Category("Appearance")]
		public SubMenuShowEffect MenuShowEffect { get { return m_subMenuShowEffect; } set { m_subMenuShowEffect = value; } }

		public List<IBasicMenuItem> Items { get { return m_items; } }

		[Category("Appearance")]
		public int NumLayers { get { return m_numLayers; } set { m_numLayers = value; } }

		[Category("Appearance")]
		public int ItemNumLayers { get { return m_itemNumLayers; } set { m_itemNumLayers = value; } }

		[Category("Appearance")]
		public int SubMenuNumLayers { get { return m_subMenuNumLayers; } set { m_subMenuNumLayers = value; } }

		[Category("Appearance")]
		public int SubMenuItemNumLayers { get { return m_subMenuItemNumLayers; } set { m_subMenuItemNumLayers = value; } }

		[Category("Appearance")]
		public string MenuItemTemplate { get { return m_defaultMenuItemTemplate; } set { m_defaultMenuItemTemplate = value; } }

		[Category("Appearance")]
		public string CssClass { get { return m_className; } set { m_className = value; } }

		[Category("Appearance")]
		public string ItemCssClass { get { return m_itemClassName; } set { m_itemClassName = value; } }

		[Category("Appearance")]
		public string SubMenuCssClass { get { return m_subMenuClassName; } set { m_subMenuClassName = value; } }

		[Category("Appearance")]
		public string SubMenuItemCssClass { get { return m_subMenuItemClassName; } set { m_subMenuItemClassName = value; } }

		public string ExternalLinkDefaultTarget { get { return m_externalLinkDefaultTarget; } set { m_externalLinkDefaultTarget = value; } }

		protected override void Render(HtmlTextWriter writer)
		{
			renderMenuArea(writer, Items, Orientation, NumLayers, CssClass, ItemNumLayers, ItemCssClass, ClientID);
		}

		protected virtual void renderMenuArea(HtmlTextWriter writer, List<IBasicMenuItem> items, Orientation orient, int numLayers, string cssClass, int itemNumLayers, string itemCssClass, string firstDivId)
		{
			renderStartLayers(writer, numLayers, cssClass, null, firstDivId);
			writer.Write("<table border='0' cellspacing='0' cellpadding='0'>");
			if (orient == Orientation.Horizontal)
				writer.Write("<tr>");
			bool includeImgSpan = shouldIncludeImageSpanInSubmenu(items);

			renderMenuItems(writer, items, orient, numLayers, cssClass, itemNumLayers, itemCssClass, includeImgSpan);

			if (orient == Orientation.Horizontal)
				writer.Write("</tr>");
			writer.Write("</table>");
			renderEndLayers(writer, numLayers);
		}

		protected virtual void renderMenuItems(HtmlTextWriter writer, List<IBasicMenuItem> items, Orientation orient, int numLayers, string cssClass, int itemNumLayers, string itemCssClass, bool includeImgSpan)
		{
			for (int pos = 0; pos < items.Count; pos++)
			{
				IBasicMenuItem bmi = items[pos];
				bmi.setupMenuItem(this);
				if (bmi.Visible)
				{
					IMenuItem mi = bmi as IMenuItem;
					if (orient == Orientation.Vertical)
						writer.Write("<tr>");

					string tdClass = "";
					if (mi != null && mi.Items.Count != 0)
						tdClass = string.Format(" class='{0}'", orient == Orientation.Horizontal ? HorzSubMenuIndicatorClass : VertSubMenuIndicatorClass);

					writer.Write("<td{0}>", tdClass);
					if (mi != null && mi.Items.Count != 0)
						writer.Write("<div class='{0} SubMenuParent' style='width:100%;position:relative;'>", orient == Orientation.Horizontal ? "VertMenu" : "HorzMenu");
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
						renderStartLayers(writer, itemNumLayers, itemCssClass, extraClass, null);
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
							writer.Write("</div><div class='SubMenu {0}' style='z-index:1000;position:absolute;display:none;'>", extraSubMenuClass);
							if (mi.RenderOwnSubMenu)
							{
								renderStartLayers(writer, SubMenuNumLayers, SubMenuCssClass, null, null);
								mi.renderSubMenu(this, writer);
								renderEndLayers(writer, SubMenuNumLayers);

							}
							else
								renderMenuArea(writer, mi.Items, Orientation.Vertical, SubMenuNumLayers, SubMenuCssClass, SubMenuItemNumLayers, SubMenuItemCssClass, null);
							writer.Write("</div>");
						}
						else if (mi.ChildLayout == ChildLayoutType.AsSiblings)
						{
							renderMenuItems(writer, mi.Items, orient, numLayers, cssClass, itemNumLayers, itemCssClass, includeImgSpan);
						}
					}
					writer.Write("</td>");
					if (orient == Orientation.Vertical)
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
				clientClick = string.Format(" onclick='{0}'", clientClick);

			return string.Format("<a href='{0}'{2}{3} style='display:block;white-space:nowrap;'><span style='white-space:nowrap;cursor:pointer;width:100%;display:block;'>{1}</span></a>",
							mi.getCurrentNavigateUrl() == null ? "javascript:;" : getMenuItemNavUrl(mi), menuItemHtml, target, clientClick);
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
				if (m_currentPageBaseUrl == null)
				{
					m_currentPageBaseUrl = Page.Request.RawUrl;
					int i = m_currentPageBaseUrl.IndexOf('?');
					if (i > 0)
						m_currentPageBaseUrl = m_currentPageBaseUrl.Substring(0, i);
				}
				return m_currentPageBaseUrl;
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
			return navUrl == null ? null : navUrl.Replace("'", "\"");
		}

		protected virtual string calcTargetName(IMenuItem mi)
		{
			string target = mi.Target;
			if ((target == null || target.Length == 0) &&
				 mi.NavigateUrl != null && mi.NavigateUrl.IndexOf(':') != -1 && ExternalLinkDefaultTarget != null)
				target = ExternalLinkDefaultTarget;
			return target == null ? "" : target;
		}

		protected static void renderStartLayers(HtmlTextWriter writer, int numLayers, string cssClass, string extraCssClass, string firstDivId)
		{
			string firstClass = cssClass;
			if (extraCssClass != null)
				firstClass += " " + extraCssClass;

			if (firstDivId != null)
				writer.Write("<div id='{1}' class='{0}'>", firstClass, firstDivId);
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

		protected static void renderEndLayers(HtmlTextWriter writer, int numLayers)
		{
			writer.Write("</div>");
			for (int pos = 0; pos < numLayers; pos++)
				writer.Write("</div>");
		}
	}


	[Serializable]
	[ParseChildren(true, "Items")]
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

		public virtual bool RenderInItemLayers { get { return m_renderInItemLayers; } set { m_renderInItemLayers = value; } }

		public List<IBasicMenuItem> Items { get { return m_items; } }

		public virtual bool RenderOwnSubMenu { get { return false; } }
		public virtual void renderSubMenu(IMenu parent, HtmlTextWriter writer) { }


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

	[Serializable]
	[ParseChildren(true, "Items")]
	public class MenuItemTable : MenuItem
	{
		protected string m_cols = "", m_subMenuCssClass = "";
		protected string m_lev0Template, m_lev1Template;
		protected string m_spans;
		public override bool RenderOwnSubMenu { get { return true; } }

		public virtual string Columns { get { return m_cols; } set { m_cols = value; } }

		public virtual string SubMenuCssClass { get { return m_subMenuCssClass; } set { m_subMenuCssClass = value; } }

		public virtual string Level0Template { get { return m_lev0Template; } set { m_lev0Template = value; } }

		public virtual string Level1Template { get { return m_lev1Template; } set { m_lev1Template = value; } }

		public virtual string CellSpans { get { return m_spans; } set { m_spans = value; } }

		protected void pushItemIntoCol(List<ItemInfo> col, IBasicMenuItem item, int curDepth)
		{
			col.Add(new ItemInfo(item, curDepth));
			if (item is IMenuItem)
				foreach (IBasicMenuItem childItem in ((IMenuItem)item).Items)
					pushItemIntoCol(col, childItem, curDepth + 1);
		}

		public override void renderSubMenu(IMenu parent, HtmlTextWriter writer)
		{
			if (Items.Count > 0)
			{
				List<List<ItemInfo>> coll = new List<List<ItemInfo>>();
				Stack<string> colSizes = null;
				if (Columns != null && Columns.Length > 0)
				{
					string[] cstr = Columns.Split(',');
					Array.Reverse(cstr);
					colSizes = new Stack<string>(cstr);
				}
				int curColCount = 0;
				int lastNumCol = 1;
				List<ItemInfo> col = null;
				foreach (IBasicMenuItem item in Items)
				{
					if (curColCount == 0)
					{
						coll.Add(new List<ItemInfo>());
						col = coll[coll.Count - 1];
						if (colSizes == null || colSizes.Count == 0)
							curColCount = lastNumCol;
						else
							curColCount = lastNumCol = int.Parse(colSizes.Pop());
					}
					item.setupMenuItem(parent);
					pushItemIntoCol(col, item, 0);
					curColCount--;
				}
				int maxRows = 0;
				foreach (List<ItemInfo> i in coll)
					if (i.Count > maxRows)
						maxRows = i.Count;


				writer.Write("<div class='{0}'><table border='0'>", SubMenuCssClass);
				int curRow = 0;
				bool wroteRowItem;
				do
				{
					wroteRowItem = false;
					writer.Write("<tr>");
					foreach (List<ItemInfo> ccol in coll)
					{
						if (curRow < ccol.Count)
						{
							int rowspan = calcRowSpan(ccol[curRow].Item, curRow, maxRows);
							string suggestedTemplate = ccol[curRow].Depth == 0 ? Level0Template : Level1Template;
							string tdClass = "SSMHead";
							if (ccol[curRow].Item is IMenuItem)
							{
								if (ccol[curRow].Depth > 0)
									tdClass = "SSMI";
							}
							else
								tdClass = "SSMO";

							writer.Write("<td class='{0}' rowspan='{1}'>", tdClass, rowspan);
							string itemHtml = ccol[curRow].Item.getMenuItemHtml(parent, false, suggestedTemplate);

							if (ccol[curRow].Item is IMenuItem && ((IMenuItem)ccol[curRow].Item).NavigateUrl != null)
								writer.Write(parent.generateMenuItemAnchor((IMenuItem)ccol[curRow].Item, itemHtml));
							else
								writer.Write(itemHtml);
							writer.Write("</td>");
							wroteRowItem = true;
						}
						else
							writer.Write("<td class='SSMS'></td>");

						if (ccol != coll[coll.Count - 1])
							writer.Write("<td class='SSMS'></td>");
					}
					curRow++;
					writer.Write("</tr>");
				}
				while (wroteRowItem);

				writer.Write("<tr>");
				foreach (List<ItemInfo> ccol in coll)
					writer.Write("<td class='SSMS' style='height:100%'>&nbsp;</td>");
				writer.Write("</tr>");


				writer.Write("</table>");
			}
		}

		protected int calcRowSpan(IBasicMenuItem bmi, int curRow, int totalRows)
		{
			int span = 1;
			if (CellSpans != null && bmi.ID != null)
			{
				Match m = new Regex(bmi.ID + @"\(([\d+*]),([\d+*])\)").Match(CellSpans);
				if (m.Success)
				{
					string col = m.Groups[1].Value;
					string row = m.Groups[2].Value;
					if (row == "*")
						span = (totalRows - curRow) + 2;
					else
						span = int.Parse(row);
				}
			}
			return span;
		}

		protected struct ItemInfo
		{
			public int Depth;
			public IBasicMenuItem Item;
			public ItemInfo(IBasicMenuItem i, int d)
			{
				Item = i;
				Depth = d;
			}
		}
	}
}
