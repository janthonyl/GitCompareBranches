using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Reflection;

namespace GitCompareBranches.Models
{
    /// <summary>
    /// Exports a collection to Excel.
    /// </summary>
    public class ExcelExport<T>
    {


        private List<CellFormat> ExtraCellFormats;
        private string strCustomDateFormat1;
        private string strCustomDateFormat2;
        private string strCustomNumberFormat1;
        private string strCustomNumberFormat2;
        /// <param name="ExtraCellFormats">Optional. Extra Styles, if defaults seem insufficient. Not valid for modifying date format. </param>
        /// <param name="strCustomDateFormat1">Optional. The default is 'mm/dd/yyyy h:mm AM/PM'. Must use Excel syntax. Apply via StyleIndex=4 </param>
        /// <param name="strCustomDateFormat2">Optional. The default is 'mm/dd/yyyy'. Must use Excel syntax. Apply via StyleIndex=5</param>
        /// <param name="strCustomNumberFormat1">Optional. The default is '#,###.#######'. Must use Excel syntax. Apply via StyleIndex=6</param>
        /// <param name="strCustomNumberFormat2">Optional. The default is '#,###.00'. Must use Excel syntax. Apply via StyleIndex=7</param>
        public ExcelExport(List<CellFormat> ExtraCellFormats = null, string strCustomDateFormat1 = null, string strCustomDateFormat2 = null,
            string strCustomNumberFormat1 = null, string strCustomNumberFormat2 = null)
        { //Leave as null if the foud default cell-styles are sufficient. If you do add
          //more CellFormats/Styles, the next one in the array will be  at StyleIndex = 4. 
            this.ExtraCellFormats = ExtraCellFormats ?? new List<CellFormat>(); //Extra styles. 
            this.strCustomDateFormat1 = strCustomDateFormat1;
            this.strCustomDateFormat2 = strCustomDateFormat2;
            this.strCustomNumberFormat1 = strCustomNumberFormat1;
            this.strCustomNumberFormat2 = strCustomNumberFormat2;
        }
        public enum DataTypes { Text, Date, Number }

        public class objColumn
        {
            public string Name;
            public DataTypes DataType;
            public UInt32? StyleIndex;
            public PropertyInfo propInfo;
            public FieldInfo fieldInfo;
            /// <param name="StyleIndex">The index of the CellFormat, if you created any ExtraCellFormats</param>
            public objColumn(string Name, DataTypes DataType, UInt32? StyleIndex = null)
            {
                this.Name = Name;
                this.DataType = DataType;
                this.StyleIndex = StyleIndex;
            }
        }

        public void ExportToExcel(Stream outputStream, List<T> Rows, string SheetName, List<objColumn> Columns)
        {
            CreateDictionaries(Columns);
            using (SpreadsheetDocument spreadDoc = SpreadsheetDocument.Create(outputStream, DocumentFormat.OpenXml.SpreadsheetDocumentType.Workbook))
            {
                WorkbookPart workbookPart = spreadDoc.AddWorkbookPart();
                workbookPart.Workbook = new Workbook();
                CreateStylesheet(spreadDoc);
                WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                SheetData sheetData = new SheetData();
                worksheetPart.Worksheet = new Worksheet(sheetData);
                Sheet sheet = new Sheet()
                {
                    Id = workbookPart.GetIdOfPart(worksheetPart),  //Note that sheet.Id is separate from sheet.SheetId;
                    Name = SheetName
                };
                //Add the sheet to the collection of Sheets -  well first create the sheet collection if it doesn't exist. 
                if (workbookPart.Workbook.Sheets == null) workbookPart.Workbook.AppendChild<Sheets>(new Sheets());
                sheet.SheetId = (UInt32)workbookPart.Workbook.Sheets.ChildElements.Count + 1;
                workbookPart.Workbook.Sheets.AppendChild(sheet);
                Row headersRow = new Row(); //First row has column headers.
                foreach (objColumn col in Columns)
                {
                    Cell c = AddCellWithText(col.Name);
                    c.StyleIndex = 1; //Bold-text style. 
                    headersRow.AppendChild(c); //header
                }
                sheetData.AppendChild(headersRow);
                int rowindex = 2; //The 2nd row begins the employee data.
                foreach (T row in Rows)
                {
                    Row excelRow = new Row();
                    excelRow.RowIndex = (UInt32)rowindex++;
                    foreach (objColumn col in Columns)
                    {
                        Cell cell = AddCell(col, row);
                        excelRow.AppendChild(cell);
                    }
                    sheetData.AppendChild(excelRow);
                }
                workbookPart.Workbook.Save();
            }
        }

        private Cell AddCell(objColumn col, T row)
        {
            object value = col.fieldInfo == null ? col.propInfo.GetValue(row) : col.fieldInfo.GetValue(row);
            Cell cell = null;
            switch (col.DataType)
            {
                case (DataTypes.Text): cell = AddCellWithText(value?.ToString(), col.StyleIndex); break;
                case (DataTypes.Date): cell = AddCellWithDate(Convert.ToDateTime(value), col.StyleIndex); break;
                case (DataTypes.Number): cell = AddCellWithDecimal(Convert.ToDecimal(value), col.StyleIndex); break;
            }
            return cell;
        }

        private Cell AddCellWithText(string str, UInt32? StyleIndex = null)
        {
            Cell c1 = new Cell();
            c1.DataType = CellValues.InlineString;
            InlineString inlineString = new InlineString();
            Text oText = new Text { Text = str };
            inlineString.AppendChild(oText);
            c1.AppendChild(inlineString);
            if (StyleIndex != null) c1.StyleIndex = StyleIndex;
            return c1;
        }

        private Cell AddCellWithDecimal(decimal d, UInt32? StyleIndex = null)
        {
            Cell c1 = new Cell();
            c1.DataType = CellValues.Number;
            c1.CellValue = new CellValue(d.ToString());
            c1.StyleIndex = 3;
            if (StyleIndex != null) c1.StyleIndex = StyleIndex;
            return c1;
        }


        private Cell AddCellWithDate(DateTime d, UInt32? StyleIndex = null)
        {
            Cell c1 = new Cell();
            c1.CellValue = new CellValue(d);
            c1.DataType = new EnumValue<CellValues>(CellValues.Date);
            c1.StyleIndex = Convert.ToUInt32(2);
            if (StyleIndex != null) c1.StyleIndex = StyleIndex;
            return c1;
        }

        private WorkbookStylesPart CreateStylesheet(SpreadsheetDocument document)
        {//From: https://stackoverflow.com/questions/55612337/openxml-change-excel-cell-format-date-and-number-when-exporting-from-datagri
            WorkbookStylesPart workbookStylesPart = document.WorkbookPart.AddNewPart<WorkbookStylesPart>();
            Stylesheet workbookstylesheet = new Stylesheet();
            //Create some styles to add to this Stylesheet. 
            DocumentFormat.OpenXml.Spreadsheet.Font font0 = new DocumentFormat.OpenXml.Spreadsheet.Font(); // Default font
            FontName arial = new FontName() { Val = "Arial" };
            FontSize size = new FontSize() { Val = 10 };
            font0.Append(arial);
            font0.Append(size);

            DocumentFormat.OpenXml.Spreadsheet.Font font1 = new DocumentFormat.OpenXml.Spreadsheet.Font(); // Bold font
            Bold bold = new Bold();
            font1.Append(bold);

            // Append both fonts
            Fonts fonts = new Fonts();
            fonts.Append(font0);
            fonts.Append(font1);

            //Create an empty array of Fills. 
            Fills fills = new Fills();
            Fill fill0 = new Fill(); //Creates one Fill for the array
            fills.Append(fill0); //Adds it to the array

            // Append borders - a must, in my case just default
            Border border0 = new Border();     // Default border
            Borders borders = new Borders();
            borders.Append(border0);


            //Create two custom date-formats, and two custom number-formats. The caller can modify these four options. 
            if (string.IsNullOrWhiteSpace(strCustomDateFormat1)) strCustomDateFormat1 = "mm/dd/yyyy h:mm AM/PM";
            NumberingFormat customDate1 = new NumberingFormat//Custom DateFormats are tricky.
            {
                NumberFormatId = UInt32Value.FromUInt32(164),  // Built-in number formats are numbered 0 - 163. Custom formats must start at 164.
                FormatCode = StringValue.FromString(strCustomDateFormat1)
            };

            if (string.IsNullOrWhiteSpace(strCustomDateFormat2)) strCustomDateFormat2 = "mm/dd/yyyy";
            NumberingFormat customDate2 = new NumberingFormat//Custom DateFormats are tricky.
            {
                NumberFormatId = UInt32Value.FromUInt32(165),  // Built-in number formats are numbered 0 - 163. Custom formats must start at 164.
                FormatCode = StringValue.FromString(strCustomDateFormat2)
            };

            if (string.IsNullOrWhiteSpace(this.strCustomNumberFormat1)) this.strCustomNumberFormat1 = "#,###.#######";
            NumberingFormat customNumber1 = new NumberingFormat
            {
                NumberFormatId = UInt32Value.FromUInt32(166),  // Built-in number formats are numbered 0 - 163. Custom formats must start at 164.
                FormatCode = StringValue.FromString(strCustomNumberFormat1)
            };

            if (string.IsNullOrWhiteSpace(this.strCustomNumberFormat2)) this.strCustomNumberFormat2 = "#,###.00";
            NumberingFormat customNumber2 = new NumberingFormat
            {
                NumberFormatId = UInt32Value.FromUInt32(167),  // Built-in number formats are numbered 0 - 163. Custom formats must start at 164.
                FormatCode = StringValue.FromString(strCustomNumberFormat2)
            };
            //Add the four custom formats as "NumberingFormats" here. Below we also have to add them as "CellFormats". 
            workbookstylesheet.NumberingFormats = new NumberingFormats(customDate1, customDate2, customNumber1, customNumber2);

            // Create an array of basic CellFormats (and also our 4 custom formats). Each format is accessible to the caller via an array index called Cell.StyleIndex.
            //A given cell on the spreadsheet can have only one style (one StyleIndex). 
            CellFormats cellformats = new CellFormats();//The empty array. Next, create the array items. 
            CellFormat cellformat0 = new CellFormat() {   FontId = 0, FillId = 0, BorderId = 0, NumberFormatId = 0, FormatId = 0 }; //Requird, but actually ignored.
            CellFormat bolded_format = new CellFormat() { FontId = 1 };
            CellFormat date_format = new CellFormat() { BorderId = 0, FillId = 0, FontId = 0, NumberFormatId = 14, FormatId = 0, ApplyNumberFormat = true };
            CellFormat number_format = new CellFormat() { BorderId = 0, FillId = 0, FontId = 0, NumberFormatId = 4, FormatId = 0, ApplyNumberFormat = true }; // format like "#,##0.00"
            CellFormat customDateFormat1 = new CellFormat() { NumberFormatId = customDate1.NumberFormatId ,   FormatId = 0, ApplyNumberFormat = true };
            CellFormat customDateFormat2 = new CellFormat() { NumberFormatId = customDate2.NumberFormatId, FormatId = 0, ApplyNumberFormat = true };
            CellFormat customNumberFormat1 = new CellFormat() { NumberFormatId = customNumber1.NumberFormatId, FormatId = 0, ApplyNumberFormat = true };
            CellFormat customNumberFormat2 = new CellFormat() { NumberFormatId = customNumber2.NumberFormatId, FormatId = 0, ApplyNumberFormat = true };


            //Push the above cell formats into the empty array.
            cellformats.Append(cellformat0); //StyleIndex will be 0. 
            cellformats.Append(bolded_format);//StyleIndex will be 1
            cellformats.Append(date_format);//StyleIndex will be 2
            cellformats.Append(number_format); //StyleIndex will be 3
            cellformats.Append(customDateFormat1);//StyleIndex will be 4
            cellformats.Append(customDateFormat2);//StyleIndex will be 5
            cellformats.Append(customNumberFormat1);//StyleIndex will be 6
            cellformats.Append(customNumberFormat2);//StyleIndex will be 7
            cellformats.Append(ExtraCellFormats);
           
            //Append all the styles above to the stylesheet  - Preserve the ORDER !
            workbookstylesheet.Append(fonts);
            workbookstylesheet.Append(fills);
            workbookstylesheet.Append(borders);
            workbookstylesheet.Append(cellformats);
            workbookStylesPart.Stylesheet = workbookstylesheet;
            workbookStylesPart.Stylesheet.Save();
            return workbookStylesPart;
        }

        private Dictionary<string, System.Reflection.PropertyInfo> dicProperties = new Dictionary<string, System.Reflection.PropertyInfo>();

        private Dictionary<string, System.Reflection.FieldInfo> dicFields = new Dictionary<string, System.Reflection.FieldInfo>();
        private char space = ' ';

        private void CreateDictionaries(List<objColumn> columns)
        {
            //Build a dictionary of properties
            foreach (System.Reflection.PropertyInfo property in typeof(T).GetProperties())
            {
                string columnName = property.Name.ToString().Split(space)[0];
                dicProperties.Add(columnName, property);
            }
            //Build a dictionary of fields
            foreach (System.Reflection.FieldInfo Field in typeof(T).GetFields())
            {
                string columnName = Field.Name.ToString().Split(space)[0];
                dicFields.Add(columnName, Field);
            }
            foreach (objColumn col in columns)
            {
                if (!dicProperties.TryGetValue(col.Name, out col.propInfo)) dicFields.TryGetValue(col.Name, out col.fieldInfo);
                if (col.fieldInfo == null && col.propInfo == null) throw new Exception($"Excel export failed. The rows do not seem to contain a column named {col.Name}.");
            }

        }
    }
}