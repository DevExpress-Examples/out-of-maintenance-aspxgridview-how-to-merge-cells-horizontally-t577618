Option Infer On

Imports DevExpress.Web
Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Web.UI.WebControls

Partial Public Class _Default
	Inherits System.Web.UI.Page

	Private cellColumnSpans As New Dictionary(Of TableCell, Integer)()
	Private cellsToRemove As New List(Of TableCell)()
	Private mergedCell As TableCell = Nothing

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
		Grid.DataSource = GetTable()
		Grid.KeyFieldName = "ID"
		Grid.DataBind()
	End Sub

	Private Function GetTable() As DataTable
		Dim table As DataTable = DirectCast(ViewState("Table"), DataTable)
		If table IsNot Nothing Then
			Return table
		End If

		table = New DataTable()
		table.Columns.Add("ID", GetType(Integer))
		table.Columns.Add("Column1", GetType(Integer))
		table.Columns.Add("Column2", GetType(Integer))
		table.Columns.Add("Column3", GetType(Integer))
		table.Columns.Add("Column4", GetType(String))
		table.Columns.Add("Column5", GetType(String))
		table.Columns.Add("Column6", GetType(String))

		table.PrimaryKey = New DataColumn() { table.Columns("ID") }

		table.Rows.Add(1, 1, 2, 2, "A", "B", "C")
		table.Rows.Add(2, 4, 4, 4, "B", "G", "G")
		table.Rows.Add(3, 3, 3, 5, "B", "B", "B")
		table.Rows.Add(4, 4, 5, 6, "C", "C", "E")
		table.Rows.Add(5, 5, 2, 1, "E", "F", "G")

		ViewState("Table") = table

		Return table
	End Function

	Protected Sub Grid_HtmlDataCellPrepared(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridViewTableDataCellEventArgs)
		e.Cell.HorizontalAlign = HorizontalAlign.Center
		If cellColumnSpans.ContainsKey(e.Cell) Then
			e.Cell.ColumnSpan = cellColumnSpans(e.Cell)
			e.Cell.BackColor = System.Drawing.Color.LightYellow
		End If
	End Sub

	Protected Sub Grid_HtmlRowPrepared(ByVal sender As Object, ByVal e As ASPxGridViewTableRowEventArgs)
		For i As Integer = 0 To e.Row.Cells.Count - 2
			Dim dataCell1 As DevExpress.Web.Rendering.GridViewTableDataCell = TryCast(e.Row.Cells(i), DevExpress.Web.Rendering.GridViewTableDataCell)
			Dim dataCell2 As DevExpress.Web.Rendering.GridViewTableDataCell = TryCast(e.Row.Cells(i + 1), DevExpress.Web.Rendering.GridViewTableDataCell)
			If dataCell1 IsNot Nothing AndAlso dataCell2 IsNot Nothing Then
				Dim fieldName1 As String = dataCell1.DataColumn.FieldName
				Dim fieldName2 As String = dataCell2.DataColumn.FieldName
				If IsSameData(e.VisibleIndex, fieldName1, fieldName2) Then
					If mergedCell Is Nothing Then
						mergedCell = e.Row.Cells(i)
					End If
					If Not cellColumnSpans.ContainsKey(mergedCell) Then
						cellColumnSpans(mergedCell) = 1
					End If
					cellColumnSpans(mergedCell) += 1
					cellsToRemove.Add(e.Row.Cells(i + 1))
				Else
					mergedCell = Nothing
				End If
			End If
		Next i
		RemoveUnnecessaryCells(e.Row)
	End Sub

	Private Sub RemoveUnnecessaryCells(ByVal row As TableRow)
		For Each cell In cellsToRemove
			row.Cells.Remove(cell)
		Next cell
		cellsToRemove.Clear()
	End Sub

	Private Function IsSameData(ByVal visibleIndex As Integer, ByVal fieldName1 As String, ByVal fieldName2 As String) As Boolean
		If fieldName1 = Grid.KeyFieldName OrElse fieldName2 = Grid.KeyFieldName Then
			Return False
		End If
		Return Equals(Grid.GetRowValues(visibleIndex, fieldName1), Grid.GetRowValues(visibleIndex, fieldName2))
	End Function
End Class