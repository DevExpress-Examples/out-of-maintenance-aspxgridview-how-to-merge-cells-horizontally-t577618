using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    private Dictionary<TableCell, int> cellColumnSpans = new Dictionary<TableCell, int>();
    private List<TableCell> cellsToRemove = new List<TableCell>();
    private TableCell mergedCell = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        Grid.DataSource = GetTable();
        Grid.KeyFieldName = "ID";
        Grid.DataBind();
    }

    private DataTable GetTable()
    {
        DataTable table = (DataTable)ViewState["Table"];
        if (table != null) return table;

        table = new DataTable();
        table.Columns.Add("ID", typeof(int));
        table.Columns.Add("Column1", typeof(int));
        table.Columns.Add("Column2", typeof(int));
        table.Columns.Add("Column3", typeof(int));
        table.Columns.Add("Column4", typeof(string));
        table.Columns.Add("Column5", typeof(string));
        table.Columns.Add("Column6", typeof(string));

        table.PrimaryKey = new DataColumn[] { table.Columns["ID"] };

        table.Rows.Add(1, 1, 2, 2, "A", "B", "C");
        table.Rows.Add(2, 4, 4, 4, "B", "G", "G");
        table.Rows.Add(3, 3, 3, 5, "B", "B", "B");
        table.Rows.Add(4, 4, 5, 6, "C", "C", "E");
        table.Rows.Add(5, 5, 2, 1, "E", "F", "G");

        ViewState["Table"] = table;

        return table;
    }

    protected void Grid_HtmlDataCellPrepared(object sender, DevExpress.Web.ASPxGridViewTableDataCellEventArgs e)
    {
        e.Cell.HorizontalAlign = HorizontalAlign.Center;
        if (cellColumnSpans.ContainsKey(e.Cell))
        {
            e.Cell.ColumnSpan = cellColumnSpans[e.Cell];
            e.Cell.BackColor = System.Drawing.Color.LightYellow;
        }
    }

    protected void Grid_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        for (int i = 0; i < e.Row.Cells.Count - 1; i++)
        {
            DevExpress.Web.Rendering.GridViewTableDataCell dataCell1 = e.Row.Cells[i] as DevExpress.Web.Rendering.GridViewTableDataCell;
            DevExpress.Web.Rendering.GridViewTableDataCell dataCell2 = e.Row.Cells[i + 1] as DevExpress.Web.Rendering.GridViewTableDataCell;
            if (dataCell1 != null && dataCell2 != null)
            {
                string fieldName1 = dataCell1.DataColumn.FieldName;
                string fieldName2 = dataCell2.DataColumn.FieldName;
                if (IsSameData(e.VisibleIndex, fieldName1, fieldName2))
                {
                    if (mergedCell == null) mergedCell = e.Row.Cells[i];
                    if (!cellColumnSpans.ContainsKey(mergedCell))
                        cellColumnSpans[mergedCell] = 1;
                    cellColumnSpans[mergedCell]++;
                    cellsToRemove.Add(e.Row.Cells[i + 1]);
                }
                else mergedCell = null;
            }
        }
        RemoveUnnecessaryCells(e.Row);
    }

    private void RemoveUnnecessaryCells(TableRow row)
    {
        foreach (var cell in cellsToRemove)
        {
            row.Cells.Remove(cell);
        }
        cellsToRemove.Clear();
    }

    private bool IsSameData(int visibleIndex, string fieldName1, string fieldName2)
    {
        if (fieldName1 == Grid.KeyFieldName || fieldName2 == Grid.KeyFieldName) return false;
        return Equals(Grid.GetRowValues(visibleIndex, fieldName1), Grid.GetRowValues(visibleIndex, fieldName2));
    }
}