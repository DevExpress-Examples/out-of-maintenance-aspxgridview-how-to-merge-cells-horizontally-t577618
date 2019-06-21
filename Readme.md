<!-- default file list -->
*Files to look at*:

* **[Default.aspx](./CS/Default.aspx) (VB: [Default.aspx](./VB/Default.aspx))**
* [Default.aspx.cs](./CS/Default.aspx.cs) (VB: [Default.aspx.vb](./VB/Default.aspx.vb))
<!-- default file list end -->
# ASPxGridView - How to merge cells horizontally
<!-- run online -->
**[[Run Online]](https://codecentral.devexpress.com/t577618/)**
<!-- run online end -->


Starting with version v2016 vol 1 (v.16.1), it became possible to merge cells using functionality available out of the box. Refer to the <a href="https://demos.devexpress.com/ASPxGridViewDemos/Rows/CellMerging.aspx">Cell Merging</a> demo for more information. This one provides only vertical merging.<br><br>This example demonstrates a possible way of merging cells in the ASPxGridView control horizontally when adjacent cells in a row with the same values are merged. To do this, you can use the <a href="https://documentation.devexpress.com/AspNet/DevExpress.Web.ASPxGridView.HtmlRowPrepared.event">HtmlRowPrepared</a> and <a href="https://documentation.devexpress.com/AspNet/DevExpress.Web.ASPxGridView.HtmlDataCellPrepared.event">HtmlDataCellPrepared</a> events.<br>Please keep in mind that functionality such as batch edit mode and fixed columns may not work as expected.

<br/>


