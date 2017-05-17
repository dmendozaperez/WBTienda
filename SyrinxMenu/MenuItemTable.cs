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
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;

namespace Syrinx2
{
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

        public override void renderSubMenu(IMenu parent, TextWriter writer)
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
