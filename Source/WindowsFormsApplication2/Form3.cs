using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;


namespace WindowsFormsApplication2
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        public string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Application.StartupPath + "/POS.accdb";

        public string Company = "";
        public string Address = "";
        public string Phone = "";
        public string NTN = "";
        public string STN = "";


        OleDbDataAdapter da;
        private BindingSource bindingSource = null;
        private OleDbCommandBuilder oleCommandBuilder = null;
        DataTable dataTable = new DataTable();
        public string SaleTax = "";
        double taxx = 0;
        double PrevUnpaidAmount = 0;

        double GetDecimal(string txt)
        {
            try
            {
                double d;
                double.TryParse(txt, out d);
                return d;
            }
            catch
            {
                //MessageBox.Show("Value " + txt + " is invalid" );
                return 0; 
            }
            
        }

        void AddItemsToList()
        {
            if (Quantiy.Text == "" || GetDecimal(TradePrice.Text) == 0)
            {
                MessageBox.Show("Trade Price or Quantity not Proper!");
            }
            else if (stock.Text == "" || stock.Text == "0" || GetDecimal(stock.Text) < 0)
            {
                MessageBox.Show("This Item is Not Present in Stock! \n Please Update Stock First");
            }
            else if (CID.Text != "id" && PID.Text != "id")
            {

                // double finaltax = GetDecimal(GrandTax.Text);
                double taxRetQuan = taxx * (GetDecimal(retail.Text) * GetDecimal(Quantiy.Text));
                double totaltrade = GetDecimal(Quantiy.Text) * GetDecimal(TradePrice.Text);
                double taxIncVal = totaltrade; /*+ taxRetQuan*/;

                double TaxRetail = taxx * (GetDecimal(retail.Text));
                double SalesTax = GetDecimal(Quantiy.Text) * TaxRetail;

                // finaltax += taxIncVal;

                double grndTotal = GetDecimal(GrandTotal.Text);
                grndTotal += taxIncVal;
                GrandTotal.Text = grndTotal.ToString();

                double grndTax = GetDecimal(GrandTax.Text);
                grndTax += SalesTax;
                GrandTax.Text = grndTax.ToString();

                double quan = GetDecimal(Qty.Text);
                quan += GetDecimal(Quantiy.Text);
                Qty.Text = quan.ToString();

                double stk = GetDecimal(stock.Text);
                quan = GetDecimal(Quantiy.Text);
                stk = stk - quan;
                stock.Text = stk.ToString();
                if (stk < 0)
                { 
                    MessageBox.Show("Quantity Entered is Greater than the Stock \nPlease Add Required items to Stock First", "Warning");
                    stk = stk + quan;
                    stock.Text = stk.ToString();
                    return;
                }

                Paid.Text = grndTotal.ToString();

                string[] row = { pname.SelectedItem.ToString(), cPackingStock.SelectedItem.ToString(), retail.Text, Quantiy.Text, TradePrice.Text, taxIncVal.ToString(), TaxRetail.ToString(), SalesTax.ToString(), PID.Text, CID.Text };
                ListViewItem lv = new ListViewItem(row);
                listView1.Items.Add(lv);
            }
            else
                MessageBox.Show("Select Customer and Items First!");
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddItemsToList();
   
        }

        void ComboItemsShow(string tablename, ComboBox cb)
        {
            string strSQL = "";
            if (tablename == "ProductName")
                strSQL = "Select distinct productname from " + tablename;
            else
                strSQL = "Select * from " + tablename;
            cb.Items.Clear();
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                OleDbCommand command = new OleDbCommand(strSQL, connection);
                try
                {
                    connection.Open();
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (tablename == "ProductName")
                                cb.Items.Add(reader[0].ToString());
                            else
                                cb.Items.Add(reader[1].ToString());
                        }
                    }
                    connection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        void InvoiceGen()
        {
            string strSQL = "Select max(id + 1) from Sale";

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                OleDbCommand command = new OleDbCommand(strSQL, connection);
                try
                {
                    connection.Open();
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        reader.Read();
                        if (reader.HasRows)
                        {
                            string idd = reader[0].ToString();
                            if (idd == "")
                                idd = "1";
                            Invoice.Text = "OAS-" + idd;
                        }
                        else
                            Invoice.Text = "OAS-1";
                    }
                    connection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            InvoiceGen();
            listView1.Columns[6].Text += SaleTax.ToString() + "%";
            taxx =  GetDecimal(SaleTax) / 100;
            ComboItemsShow("Customer", cname);
            ComboItemsShow("ProductName", pname);
            tname.Focus();
            logo.SelectedIndex = 0;
            //ComboItemsShow("Packing", cPackingStock);
        }

        private void cname_Click(object sender, EventArgs e)
        {
            //if (cname.Text == "")
            {
                ComboItemsShow("Customer", cname);
            }
            //else {

            //    cname.Items.Clear();
            //    string strSQL = "Select CustomerName from Customer where CustomerName like '"+ cname.Text +"%'";

            //    using (OleDbConnection connection = new OleDbConnection(connectionString))
            //    {
            //        OleDbCommand command = new OleDbCommand(strSQL, connection);
            //        try
            //        {
            //            connection.Open();
            //            using (OleDbDataReader reader = command.ExecuteReader())
            //            {
                            
            //                while(reader.Read())
            //                {
            //                    cname.Items.Add(reader[0].ToString());
            //                }
                           
            //            }
            //            connection.Close();
            //        }
            //        catch (Exception ex)
            //        {
            //            MessageBox.Show(ex.Message);
            //        }
            //    }
            
            //}
        }

        private void pname_Click(object sender, EventArgs e)
        {
            ComboItemsShow("ProductName", pname);
        }

        private void cPackingStock_Click(object sender, EventArgs e)
        {
            //ComboItemsShow("Packing", cPackingStock);
        }

        void GetStock(string product, string pack)
        {
            string strSQL = "Select Quantity from RunningStock where ProductName='" + product + "' and PackName='" + pack +"'";
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                OleDbCommand command = new OleDbCommand(strSQL, connection);
                try
                {
                    connection.Open();
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            stock.Text = reader[0].ToString();
                        }
                        
                    }
                    connection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void cname_SelectedValueChanged(object sender, EventArgs e)
        {
            //string strSQL = "Select id,CNIC,PrevAmount,Address from Customer where CustomerName='" + cname.SelectedItem.ToString() + "'";
            if (CustomerNameslist.Visible == true)
                return;
            string strSQL = "Select CNIC from Customer where CustomerName='" + cname.SelectedItem.ToString() + "'";
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                OleDbCommand command = new OleDbCommand(strSQL, connection);
                try
                {
                    //cnic.Items.Clear();
                    Refresh();
                    connection.Open();
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            //CID.Text = reader[0].ToString();
                            cnic.Items.Add(reader[0].ToString());
                            //unpaid.Text = reader[2].ToString();
                            //CAddress.Text = reader[3].ToString();
                           
                        }
                        connection.Close();
                        if (cnic.Items.Count == 1)
                        {
                            string strSQL1 = "Select id,PrevAmount,Address,Status from Customer where CustomerName='" + cname.SelectedItem.ToString() + "' and CNIC='" + cnic.Items[0].ToString() + "'";
                            GetCustomerRecord(strSQL1);    
                        }
                    }
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        void GetProd(string product, string pack)
        {
            string strSQL = "Select id,UnitPrice,Retail from ProductName where ProductName='" + product + "' and PackName='" + pack +"'";
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                OleDbCommand command = new OleDbCommand(strSQL, connection);
                try
                {
                    connection.Open();
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            PID.Text = reader[0].ToString();
                            UnitPrice.Text = reader[1].ToString();
                            retail.Text = reader[2].ToString();
                        }
                    }
                    connection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void cPackingStock_SelectedValueChanged(object sender, EventArgs e)
        {
            cname.Enabled = false;
            retail.Clear();
            UnitPrice.Clear();
            stock.Clear();
            if (pname.SelectedItem != null)
            {
                GetProd(pname.SelectedItem.ToString(), cPackingStock.SelectedItem.ToString());
                GetStock(pname.SelectedItem.ToString(), cPackingStock.SelectedItem.ToString());
            }
        }

        private void pname_SelectedValueChanged(object sender, EventArgs e)
        {
            if (ProductList.Visible == true)
            { return; }
            if (cname.SelectedItem == null)
            {
                MessageBox.Show("Please Select Customer First!");
                return;
            }
            cname.Enabled = false;
            retail.Clear();
            UnitPrice.Clear();
            stock.Clear();
            cPackingStock.Items.Clear();
            string strSQL = "select packname from productname where productname='"+pname.SelectedItem.ToString()+"'";
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                OleDbCommand command = new OleDbCommand(strSQL, connection);
                try
                {
                    connection.Open();
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cPackingStock.Items.Add(reader[0].ToString());
                        }

                        if (reader.HasRows)
                        {
                            cPackingStock.SelectedIndex = 0;
                        }
                        if (cPackingStock.Items.Count == 1)
                        {
                            cname.Enabled = false;
                            retail.Clear();
                            UnitPrice.Clear();
                            stock.Clear();
                            if (pname.SelectedItem != null)
                            {
                                GetProd(pname.SelectedItem.ToString(), cPackingStock.SelectedItem.ToString());
                                GetStock(pname.SelectedItem.ToString(), cPackingStock.SelectedItem.ToString());
                            }
                        }
                    }
                    connection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            if (cPackingStock.SelectedItem != null)
            {
                GetProd(pname.SelectedItem.ToString(), cPackingStock.SelectedItem.ToString());
                GetStock(pname.SelectedItem.ToString(), cPackingStock.SelectedItem.ToString());
            }
        }

        void Refresh()
        {
            cname.Enabled = true;
            //cname.Items.Clear();
            cPackingStock.Items.Clear();
            //cPackingStock.Text = "";
            //pname.Items.Clear();
            //pname.Text = "";
            stock.Text = "";
            GrandTotal.Text = "0.0";
            GrandTax.Text = "0.0";
            Paid.Text = "0.0";
            cnic.Items.Clear();
            CAddress.Text = "";
            Status.Text = "";
            unpaid.Clear();
            retail.Clear();
            UnitPrice.Clear();
            Quantiy.Text = "0";
            TradePrice.Text = "0";
            PID.Text = "id";
            CID.Text = "id";
            Qty.Text = "0";
            listView1.Items.Clear();
            tname.Focus();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listView1.CheckedItems)
            {
                double grndTotal = GetDecimal(GrandTotal.Text);
                grndTotal -= GetDecimal(item.SubItems[5].Text);
                GrandTotal.Text = grndTotal.ToString();

                double grndTax = GetDecimal(GrandTax.Text);
                grndTax -= GetDecimal(item.SubItems[7].Text);
                GrandTax.Text = grndTax.ToString();

                double quan = GetDecimal(Qty.Text);
                quan -= GetDecimal(item.SubItems[3].Text);
                Qty.Text = quan.ToString();

                double stk = GetDecimal(stock.Text);
                quan = GetDecimal(item.SubItems[3].Text);
                stk = stk + quan;
                stock.Text = stk.ToString();

                Paid.Text = grndTotal.ToString();
                listView1.Items[item.Index].Remove();
            }
           
        }

        private void TradePrice_Click(object sender, EventArgs e)
        {
            TradePrice.Clear();
        }

        private void Paid_Click(object sender, EventArgs e)
        {
            //Paid.Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (listView1.Items.Count > 0)
            {
                DialogResult dr = MessageBox.Show("Commit Transaction?", "Warning", MessageBoxButtons.YesNo);
                if (dr == System.Windows.Forms.DialogResult.Yes)
                {
                    double RemainingAmt = GetDecimal(GrandTotal.Text) - GetDecimal(Paid.Text);
          
                    double prevAmt = 0;
     
                    OleDbCommand command = null;
                    OleDbTransaction tr = null;
                    string strSQL1 = "INSERT INTO Sale(invoice,tax,saledate,grandtotal) VALUES('" + Invoice.Text + "','" + GrandTax.Text + "','" + SaleDate.Value.ToShortDateString() + "','" + GrandTotal.Text + "')";
                    string strSQL2 = "INSERT INTO Invoice_Customer(invoice_id,CustomerId,RemainingAmount) VALUES('" + Invoice.Text + "','" + CID.Text + "','" + RemainingAmt.ToString() + "')";
                    using (OleDbConnection connection = new OleDbConnection(connectionString))
                    {
                        try
                        {
                            connection.Open();
                            //tr = connection.BeginTransaction();
                            //tr.Begin();
                            command = new OleDbCommand(strSQL1, connection);
                            command.ExecuteReader();
                            command = new OleDbCommand(strSQL2, connection);
                            command.ExecuteReader();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            //tr.Rollback();
                            connection.Close();
                            return;
                        }

                        foreach (ListViewItem item in listView1.Items)
                        {
                            string strSQL3 = "INSERT INTO Invoice_Product(invoice,ProductId,Quanity,TradePrice,Tax) VALUES('" + Invoice.Text + "','" + item.SubItems[8].Text + "','" + item.SubItems[3].Text + "','" + item.SubItems[4].Text + "','" + item.SubItems[6].Text + "')";

                            try
                            {  
                                command = new OleDbCommand(strSQL3, connection);
                                command.ExecuteReader();
                                string strSQL6 = "Select Quantity from RunningStock where ProductName='" + item.SubItems[0].Text + "' and PackName='" + item.SubItems[1].Text + "'";
         
                                command = new OleDbCommand(strSQL6, connection);
                                double stk = 0;
                                using (OleDbDataReader reader = command.ExecuteReader())
                                {
                                     while (reader.Read())
                                    {
                                       stk = GetDecimal(reader[0].ToString());
                                    }
                        
                                }

                                stk = stk - GetDecimal(item.SubItems[3].Text);
                                string strSQL7 = "Update RunningStock SET Quantity =" + stk.ToString() + " where ProductName ='" + item.SubItems[0].Text + "' and PackName='" + item.SubItems[1].Text + "'";
                                command = new OleDbCommand(strSQL7, connection);
                                command.ExecuteReader();
                   
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                                //tr.Rollback();
                                connection.Close();
                                return;
                            }


                        }//foreach
   
                        string strSQL5 = "Select PrevAmount from Customer where ID=" + CID.Text;
                        
                        command = new OleDbCommand(strSQL5, connection);
                        try
                        {
                            using (OleDbDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    prevAmt = GetDecimal(reader[0].ToString());
                                }
                            }
                            prevAmt += RemainingAmt;
                            string strSQL4 = "Update Customer SET PrevAmount ='" + prevAmt.ToString() + "' where ID =" + CID.Text;
                            command = new OleDbCommand(strSQL4, connection);
                            command.ExecuteReader();
                            
                            MessageBox.Show("Transaction Successfull");

                            if (prntformat.Checked)
                                PrintFormatReceipt();
                            else
                                PrintReceipt();

                            Refresh();
                            //tr.Commit();
                            connection.Close();
                            InvoiceGen();
                            //Form1 f1 = new Form1();
                            ////f1.UpdateStock();
                            //f1.Dispose();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            //tr.Rollback();
                            connection.Close();
                        }

                      
                       
                    }
                }
            }
            else
                MessageBox.Show("No Items in List to Sell");
        }


        void GetCustomerRecord(string strSQL)
        {
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                OleDbCommand command = new OleDbCommand(strSQL, connection);
                try
                {
                    connection.Open();
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            CID.Text = reader[0].ToString();
                            unpaid.Text = reader[1].ToString();
                            CAddress.Text = reader[2].ToString();
                            Status.Text = reader[3].ToString();
                            PrevUnpaidAmount = GetDecimal(unpaid.Text);
                        }
                    }
                    connection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void cnic_SelectedValueChanged(object sender, EventArgs e)
        {
            string strSQL = "Select id,PrevAmount,Address,Status from Customer where CustomerName='" + cname.SelectedItem.ToString() + "' and CNIC='"+ cnic.SelectedItem.ToString() +"'";
            GetCustomerRecord(strSQL);    
        }

        void AddToCell(Microsoft.Office.Interop.Word.Table table, int rowindex, int colindex,float fontsize, int bold, string text)
        {
            table.Cell(rowindex, colindex).Range.Text = text;
            table.Cell(rowindex, colindex).Range.Font.Bold = bold;
            table.Cell(rowindex, colindex).Range.Font.Name = "verdana";
            table.Cell(rowindex, colindex).Range.Font.Size = fontsize;
            table.Cell(rowindex, colindex).Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;
        }
        void PrintFormatReceipt()
        {
            try
            {
                //Create an instance for word app  
                Microsoft.Office.Interop.Word.Application winword = new Microsoft.Office.Interop.Word.Application();

                //Set animation status for word application  


                //Set status for word application is to be visible or not.  
                winword.Visible = false;

                //Create a missing variable for missing value  
                object missing = System.Reflection.Missing.Value;

                //Create a new document  
                Microsoft.Office.Interop.Word.Document document = winword.Documents.Add(ref missing, ref missing, ref missing, ref missing);
                if (LandScape.Checked == false)
                    document.Sections.PageSetup.TopMargin = 30.0f;
                else
                {
                    document.Sections.PageSetup.TopMargin = 15.0f;
                    document.Sections.PageSetup.HeaderDistance = 10.0f;
                }
                document.Sections.PageSetup.RightMargin = 30.0f;
                document.Sections.PageSetup.LeftMargin = 30.0f;
                document.Sections.PageSetup.BottomMargin = 40.0f;
                document.Sections.PageSetup.FooterDistance = 10f;
                //document.SpellingChecked = true;
                //document.DisableFeatures = true;
                document.ShowSpellingErrors = false;
                document.ShowGrammaticalErrors = false;
                if(A5.Checked)
                    document.PageSetup.PaperSize = Microsoft.Office.Interop.Word.WdPaperSize.wdPaperA5;
                else
                    document.PageSetup.PaperSize = Microsoft.Office.Interop.Word.WdPaperSize.wdPaperA4;
                if(LandScape.Checked == true)
                document.PageSetup.Orientation = Microsoft.Office.Interop.Word.WdOrientation.wdOrientLandscape;
                //document.Sections[0].PageSetup.Orientation = PageOrientation.Landscape;
                foreach (Microsoft.Office.Interop.Word.Section section in document.Sections)
                {
                    //Get the header range and add the header details.  
                    Microsoft.Office.Interop.Word.Range headerRange = section.Headers[Microsoft.Office.Interop.Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
                    headerRange.Fields.Add(headerRange, Microsoft.Office.Interop.Word.WdFieldType.wdFieldPage);
                    headerRange.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    headerRange.Font.ColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdWhite;
                    headerRange.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    Microsoft.Office.Interop.Word.InlineShape ish = headerRange.InlineShapes.AddPicture(Application.StartupPath  + "/" +logo.SelectedItem.ToString() + ".png");
                   
                }  
                foreach (Microsoft.Office.Interop.Word.Section wordSection in document.Sections)
                {
                    //Get the footer range and add the footer details.  
                    Microsoft.Office.Interop.Word.Range footerRange = wordSection.Footers[Microsoft.Office.Interop.Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
                    footerRange.Font.ColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdDarkRed;
                    footerRange.Font.Size = 9;
                    footerRange.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;
                    footerRange.Text = "No Expiry Claim will be Compensated.\nDistribution is not responsible for any personal matter between Sales Staff and Customer.";
                    //footerRange.InsertAfter("Distribution is not responsible for any personal matter between Sales Staff and Customer.");
                }
                
                //adding text to document  
                
                document.Content.SetRange(0, 0);


               
                //Add paragraph with Heading 1 style  
                Microsoft.Office.Interop.Word.Paragraph para1 = document.Content.Paragraphs.Add(ref missing);
                //object styleHeading1 = "Heading 1";
                //para1.Range.set_Style(ref styleHeading1);
                para1.Range.Font.Size = 14F;
                para1.Range.Font.Name = "Arial Black";
                if(WithoutColor.Checked == false)
                para1.Range.Font.Color = Microsoft.Office.Interop.Word.WdColor.wdColorBlueGray;
                para1.Range.Text = Company;
                
     
                //Application.Selection.InlineShapes.AddPicture(Application.StartupPath + "/logo1.png");
                para1.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                para1.Range.InsertParagraphAfter();

                Microsoft.Office.Interop.Word.Table firstTable1 = document.Tables.Add(para1.Range, 6, 4, ref missing, ref missing);
                firstTable1.Borders.Enable = 0;
                firstTable1.Rows.HeightRule = Microsoft.Office.Interop.Word.WdRowHeightRule.wdRowHeightExactly;
                //firstTable1.Rows.SetHeight(0.1f,Microsoft.Office.Interop.Word.WdRowHeightRule.wdRowHeightAuto);

                firstTable1.Columns[1].Width = 65f;
                firstTable1.Columns[2].Width = 270f;
                firstTable1.Columns[3].Width = 55f;
                firstTable1.Columns[4].Width = 150f;

                //foreach (Microsoft.Office.Interop.Word.Column col in firstTable1.Columns)
              
               
                AddToCell(firstTable1, 1, 1, 8.3F, 1, "Address:");
                AddToCell(firstTable1, 1, 2, 8.3F, 0, Address);
                AddToCell(firstTable1, 1, 3, 8.3F, 1, "NTN:");
                AddToCell(firstTable1, 1, 4, 8.3F, 0, NTN);
                AddToCell(firstTable1, 2, 1, 8.3F, 1, "Mobile:");
                AddToCell(firstTable1, 2, 2, 8.3F, 0, Phone);
                AddToCell(firstTable1, 2, 3, 8.3F, 1, "STN:");
                AddToCell(firstTable1, 2, 4, 8.3F, 0, STN);
                if(WithoutColor.Checked == false)
                firstTable1.Rows[3].Cells.Shading.BackgroundPatternColor = Microsoft.Office.Interop.Word.WdColor.wdColorGray25;
                AddToCell(firstTable1, 3, 2, 11F, 1, "BILL/INVOICE");
                AddToCell(firstTable1, 4, 1, 9F, 1, "Customer:");
                AddToCell(firstTable1, 4, 2, 9F, 0, cname.SelectedItem.ToString());
                AddToCell(firstTable1, 4, 3, 9F, 1, "Date:");
                AddToCell(firstTable1, 4, 4, 9F, 0, SaleDate.Value.Day.ToString() + "/" + SaleDate.Value.Month.ToString() + "/" + SaleDate.Value.Year.ToString());
                AddToCell(firstTable1, 5, 1, 9F, 1, "Address:");
                AddToCell(firstTable1, 5, 2, 9F, 0, CAddress.Text);
                AddToCell(firstTable1, 5, 3, 9F, 1, "CNIC:");
                if (printcnic.Checked)
                {
                    if (cnic.SelectedItem == null)
                    {
                        cnic.SelectedIndex = 0;
                    }
                    AddToCell(firstTable1, 5, 4, 10F, 0, cnic.SelectedItem.ToString());
                }
                AddToCell(firstTable1, 6, 1, 9F, 1, "Status:");
                AddToCell(firstTable1, 6, 2, 9F, 0, Status.Text);
                AddToCell(firstTable1, 6, 3, 9F, 1, "Invoice:");
                AddToCell(firstTable1, 6, 4, 9F, 0, Invoice.Text);
                //para1.Range.InsertBreak(ref missing);

                Microsoft.Office.Interop.Word.Paragraph para2 = document.Content.Paragraphs.Add(ref missing);
                para2.Range.Text = " ";
                para2.Range.InsertParagraphAfter();

               Microsoft.Office.Interop.Word.Table firstTable = document.Tables.Add(para2.Range, listView1.Items.Count+2, 9, ref missing, ref missing);
               firstTable.Borders.Enable = 0;
               firstTable.Columns[1].Width = 25f;
               if(A4.Checked)
                firstTable.Columns[2].Width = 118f;
               else
                firstTable.Columns[2].Width = 108f;
               firstTable.Columns[3].Width = 65f;
               firstTable.Columns[4].Width = 55f;
               firstTable.Columns[5].Width = 55f;
               firstTable.Columns[6].Width = 55f;
               firstTable.Columns[7].Width = 55f;
               firstTable.Columns[8].Width = 65f;
               if (A4.Checked)
                   firstTable.Columns[9].Width = 58f;
               else
                   firstTable.Columns[9].Width = 55f;

               if (WithoutColor.Checked == false)
               firstTable.Rows[1].Cells.Shading.BackgroundPatternColor = Microsoft.Office.Interop.Word.WdColor.wdColorGray25;
               firstTable.Rows[1].Cells.VerticalAlignment = Microsoft.Office.Interop.Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;
               if(LandScape.Checked == false)
                   firstTable.Rows.HeightRule = Microsoft.Office.Interop.Word.WdRowHeightRule.wdRowHeightAuto;
               else
                   firstTable.Rows.HeightRule = Microsoft.Office.Interop.Word.WdRowHeightRule.wdRowHeightExactly;
               firstTable.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;

               

               AddToCell(firstTable, 1, 1, 9f, 1, "Sr.");
               AddToCell(firstTable, 1, 2, 9f, 1, "Product");
               AddToCell(firstTable, 1, 3, 9f, 1, "Pack");
               AddToCell(firstTable, 1, 4, 9f, 1, "R.Pr");
               AddToCell(firstTable, 1, 5, 9f, 1, "Qty.");
               AddToCell(firstTable, 1, 6, 9f, 1, "T.Pr");
               AddToCell(firstTable, 1, 7, 9f, 1, "T.V(I.S)");
               AddToCell(firstTable, 1, 8, 9f, 1, "S.T@" + SaleTax.ToString()+"%");
               AddToCell(firstTable, 1, 9, 9f, 1, "S.Tax");
               
               int count = 1;
               int rowindx = 2;
               foreach (ListViewItem item in listView1.Items)
               {
                   //for (int j = 1; j <= 9; j++)
                   //foreach (Microsoft.Office.Interop.Word.Cell cell in firstTable.Rows[count+1].Cells)
                   {
                      AddToCell(firstTable, rowindx, 1, 8.5f, 0, count.ToString());
                      AddToCell(firstTable, rowindx, 2, 8.5f, 0, item.SubItems[0].Text);
                      AddToCell(firstTable, rowindx, 3, 8.5f, 0, item.SubItems[1].Text);
                      AddToCell(firstTable, rowindx, 4, 8.5f, 0, item.SubItems[2].Text);
                      AddToCell(firstTable, rowindx, 5, 8.5f, 0, item.SubItems[3].Text);
                      AddToCell(firstTable, rowindx, 6, 8.5f, 0, item.SubItems[4].Text);
                      AddToCell(firstTable, rowindx, 7, 8.5f, 0, item.SubItems[5].Text);
                      AddToCell(firstTable, rowindx, 8, 8.5f, 0, item.SubItems[6].Text);
                      AddToCell(firstTable, rowindx, 9, 8.5f, 0, item.SubItems[7].Text);
                      count += 1;
                      rowindx += 1;
                   }
               }

               //firstTable.Rows[rowindx].Cells[5].Borders.Enable = 1;
               //firstTable.Rows[rowindx].Cells[7].Borders.Enable = 1;
               //firstTable.Rows[rowindx].Cells[9].Borders.Enable = 1;
               //firstTable.Rows[rowindx].Borders.JoinBorders = false;
               //firstTable.Rows[rowindx].Cells[5].Range.Font.Italic = 1;
               //firstTable.Rows[rowindx].Cells[7].Range.Font.Italic = 1;
               //firstTable.Rows[rowindx].Cells[9].Range.Font.Italic = 1;
               if (WithoutColor.Checked == false)
               {
                   firstTable.Rows[rowindx].Cells[5].Shading.BackgroundPatternColor = Microsoft.Office.Interop.Word.WdColor.wdColorGray25;
                   firstTable.Rows[rowindx].Cells[7].Shading.BackgroundPatternColor = Microsoft.Office.Interop.Word.WdColor.wdColorGray25;
                   firstTable.Rows[rowindx].Cells[9].Shading.BackgroundPatternColor = Microsoft.Office.Interop.Word.WdColor.wdColorGray25;
               }
               rowindx += 1;
               AddToCell(firstTable, rowindx, 4, 9f, 1, "Total:");
               AddToCell(firstTable, rowindx, 5, 8.5f, 1, Qty.Text);
               AddToCell(firstTable, rowindx, 7, 8.5f, 1, GrandTotal.Text);
               AddToCell(firstTable, rowindx, 9, 8.5f, 1, GrandTax.Text);

               firstTable.Rows[1].Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderBottom].LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
               firstTable.Rows[rowindx - 1].Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderTop].LineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;

               Microsoft.Office.Interop.Word.Paragraph para3 = document.Content.Paragraphs.Add(ref missing);
               para3.Range.Text = " ";
               para3.Range.InsertParagraphAfter();

               Microsoft.Office.Interop.Word.Table firstTable2 = document.Tables.Add(para2.Range, 5, 3, ref missing, ref missing);
               firstTable2.Borders.Enable = 0;
               firstTable2.Rows[1].Cells.VerticalAlignment = Microsoft.Office.Interop.Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;
               if(A5.Checked && LandScape.Checked)
                firstTable2.Rows.HeightRule = Microsoft.Office.Interop.Word.WdRowHeightRule.wdRowHeightExactly;
               else
                   firstTable2.Rows.HeightRule = Microsoft.Office.Interop.Word.WdRowHeightRule.wdRowHeightAuto;
               firstTable2.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;
               firstTable2.Columns[1].Width = 310f;
               firstTable2.Columns[2].Width = 125f;
               if(A4.Checked)
                   firstTable2.Columns[3].Width = 118f;
               else
                   firstTable2.Columns[3].Width = 112f;

               AddToCell(firstTable2, 1, 2, 10f, 0, "Sales Tax Inc.");
               AddToCell(firstTable2, 1, 3, 10f, 0, GrandTax.Text);
               AddToCell(firstTable2, 2, 2, 10f, 0, "Payable Amount.");
               AddToCell(firstTable2, 2, 3, 10f, 0, GrandTotal.Text);
               if (unpaid.Text == "")
                   unpaid.Text = "0";
               AddToCell(firstTable2, 3, 2, 10f, 0, "Prev. Bill.");
               AddToCell(firstTable2, 3, 3, 10f, 0, unpaid.Text);
               AddToCell(firstTable2, 4, 2, 10f, 1, "Net Payable.");
               firstTable2.Cell(5, 1).Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;
               AddToCell(firstTable2, 5, 1, 8.5f, 0, "Customer Sig.");
               double NetPayable = GetDecimal(GrandTotal.Text) + GetDecimal(unpaid.Text);
               AddToCell(firstTable2, 4, 3, 10f, 1, NetPayable.ToString());
               if (WithoutColor.Checked == false)
               {
                   firstTable2.Rows[1].Cells[3].Shading.BackgroundPatternColor = Microsoft.Office.Interop.Word.WdColor.wdColorLightGreen;
                   firstTable2.Rows[2].Cells[3].Shading.BackgroundPatternColor = Microsoft.Office.Interop.Word.WdColor.wdColorLightGreen;
                   firstTable2.Rows[3].Cells[3].Shading.BackgroundPatternColor = Microsoft.Office.Interop.Word.WdColor.wdColorLightGreen;
                   firstTable2.Rows[4].Cells[3].Shading.BackgroundPatternColor = Microsoft.Office.Interop.Word.WdColor.wdColorAqua;
               }
               
               //para2.Range.InsertAfter("")
               
               
           
                //Save the document  
               string dir = Application.StartupPath + "/" + SaleDate.Value.ToLongDateString();
               string dt = dir + "/" + Invoice.Text + ".doc";
            
               DirectoryInfo di = new DirectoryInfo(dir);
               if (!di.Exists)
                   di.Create();
                object filename = dt;
                document.SaveAs(ref filename);
                if (DontLoad.Checked)
                {
                    document.Close(ref missing, ref missing, ref missing);
                    document = null;
                    winword.Quit(ref missing, ref missing, ref missing);
                    winword = null;
                }
                else
                {
                    object readOnly = true;
                    object isVisible = true;
                    winword.Documents.Open(ref filename, ReadOnly: readOnly, Visible: isVisible);
                }
                //object missing1 = System.Reflection.Missing.Value;
                //Microsoft.Office.Interop.Word.Application wrd = new Microsoft.Office.Interop.Word.Application { Visible = true };
                //Microsoft.Office.Interop.Word.Document doc = wrd.Documents.Open(filename, ReadOnly: readOnly, Visible: isVisible);
                //MessageBox.Show("Document created successfully !");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }  
        }

        public void PrintReceipt()
        {
            try
            {
                string dir = Application.StartupPath + "/" + SaleDate.Value.ToLongDateString();
                string dt = dir + "/" + Invoice.Text + ".doc";
                //File.Copy(Application.StartupPath + "/POS.accdb", dt, true);
                
                DirectoryInfo di = new DirectoryInfo(dir);
                if (!di.Exists)
                    di.Create();

                FileStream fs = File.Create(dt);
                fs.Close();
                
                StreamWriter sw = new StreamWriter(dt); //(Application.StartupPath + "/receipt.doc");
                
                //sw.WriteLine("{0,-20}",Company);
                //sw.WriteLine("{0,-20}",Company);
                //File.Create(Application.StartupPath + "/" + Invoice.Text + ".txt");

                sw.WriteLine(String.Format("{0,-30}{1,-20}","", Company));
                string line = "_";
                for (int i = 0; i <= Company.Length; i++)
                    line += "_";
                sw.WriteLine(String.Format("{0,-29}{1,-20}", " ", line));

                sw.WriteLine(String.Format("\n\nAddress: {0,-45}NTN: {1,-15}", Address, NTN));
                sw.WriteLine(String.Format("Mobile: {0,-45} STN: {1,-15}", Phone, STN));
                sw.WriteLine(String.Format("____________________________________________________________________________"));
                sw.WriteLine(String.Format("\nCustomer: {0,-34}Date: {1,-15}", cname.SelectedItem.ToString(), SaleDate.Value.Day.ToString() + "/" + SaleDate.Value.Month.ToString() + "/" + SaleDate.Value.Year.ToString()));
                if (printcnic.Checked)
                {
                    if (cnic.SelectedItem == null)
                    {
                        cnic.SelectedIndex = 0; 
                    }
                    if(cnic.Items.Count == 0)
                        sw.WriteLine(String.Format("Address: {0,-35}CNIC: {1,-15}", CAddress.Text, "00000000000"));
                    else
                        sw.WriteLine(String.Format("Address: {0,-35}CNIC: {1,-15}", CAddress.Text, cnic.SelectedItem.ToString()));
                }
                else
                   sw.WriteLine(String.Format("Address: {0,-35}CNIC:", CAddress.Text));

                sw.WriteLine(String.Format("Status: {0,-35} Invoice: {1,-30}", Status.Text, Invoice.Text));

                sw.WriteLine("\n____________________________________________________________________" );
                sw.WriteLine(String.Format("Sr.{0,-1}Prod:{1,-9}Pack{2,-7}R.Pr{3,-2}Qty{4,-2}T.Pr{5,-2}TV(i.s){6,-1}S.T@" + SaleTax.ToString() + "% S.Tx", " ", " ", " ", " ", " ", " ", " "));
                sw.WriteLine("____________________________________________________________________");

                int count = 1;
                foreach (ListViewItem item in listView1.Items)
                {
                    sw.WriteLine(String.Format("{0,-3}{1,-15}{2,-10} {3,-5} {4,-4} {5,-5} {6,-7} {7,-7} {8,-5}", count.ToString(), item.SubItems[0].Text, item.SubItems[1].Text, item.SubItems[2].Text,
                        item.SubItems[3].Text, item.SubItems[4].Text, item.SubItems[5].Text, item.SubItems[6].Text, item.SubItems[7].Text, item.SubItems[8].Text));
                   
                    count += 1;
                }
                sw.WriteLine("_____________________________________________________________________");
                sw.WriteLine(String.Format("{0,-2}{1,-9}{2,-10}{3,-6} Total:  {4,-5}{5,-5}{6,-5}{7,-10}{8,-5}", " ", " ", " ", " ", Qty.Text, " ", GrandTotal.Text," ",GrandTax.Text));
                sw.WriteLine(String.Format("\n{0,-35}{1,-20}{2,-10}"," " ,"Sales Tax Inc. ", GrandTax.Text));
                sw.WriteLine(String.Format("{0,-35}{1,-20}{2,-10}", " ", "Payable Amount. ", GrandTotal.Text));
                if (unpaid.Text == "")
                    unpaid.Text = "0";
                sw.WriteLine(String.Format("{0,-35}{1,-20}{2,-10}", " ", "Prev. Bill. ", unpaid.Text));
                double NetPayable = GetDecimal(GrandTotal.Text) + GetDecimal(unpaid.Text);
                sw.WriteLine(String.Format("{0,-35}{1,-20}{2,-10}", " ", "Net Payable. ", NetPayable.ToString()));
                sw.WriteLine("Note:");
                sw.WriteLine("No Expiry Claim will be Compensated. ");
                sw.WriteLine("Distribution is not responsible for any personal matter between Sales Staff and Customer.");
                //sw.WriteLine(inv);
                sw.Dispose();



                object readOnly = true;
                object isVisible = true;
                object missing = System.Reflection.Missing.Value;
                Microsoft.Office.Interop.Word.Application wrd = new Microsoft.Office.Interop.Word.Application { Visible = true };
                Microsoft.Office.Interop.Word.Document doc = wrd.Documents.Open(dt, ReadOnly: readOnly, Visible: isVisible);
                //doc.PrintOut();
            
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void OpenReceipt()
        {
 
        }

        private void cname_KeyPress(object sender, KeyPressEventArgs e)
        {
            /*if(e.KeyChar == 13)
            {
                cname.Items.Clear();
                string strSQL = "Select CustomerName from Customer where CustomerName like '"+ cname.Text +"%'";

                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    OleDbCommand command = new OleDbCommand(strSQL, connection);
                    try
                    {
                        connection.Open();
                        using (OleDbDataReader reader = command.ExecuteReader())
                        {
                            
                            while(reader.Read())
                            {
                                cname.Items.Add(reader[0].ToString());
                            }
                            if (reader.HasRows)
                            {
                                cname.Text = cname.Items[0].ToString();
                                cname.SelectedIndex = 0;
                            }
                           
                        }
                        connection.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            
            }*/
        
        }

        private void cPackingStock_SelectedValueChanged_1(object sender, EventArgs e)
        {
            cname.Enabled = false;
            retail.Clear();
            UnitPrice.Clear();
            stock.Clear();
            if (pname.SelectedItem != null)
            {
                GetProd(pname.SelectedItem.ToString(), cPackingStock.SelectedItem.ToString());
                GetStock(pname.SelectedItem.ToString(), cPackingStock.SelectedItem.ToString());
            }
        }

        private void Quantiy_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void Quantiy_Click(object sender, EventArgs e)
        {
            Quantiy.Text = "";
        }

        private void TradePrice_TextChanged(object sender, EventArgs e)
        {

        }

        private void Quantiy_TextChanged_1(object sender, EventArgs e)
        {
            
        }

        private void Quantiy_Leave(object sender, EventArgs e)
        {
            Quantiy.Text = GetDecimal(Quantiy.Text).ToString();
        }

        private void TradePrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 13)
                AddItemsToList();
        }

        private void TradePrice_Leave(object sender, EventArgs e)
        {
            TradePrice.Text = GetDecimal(TradePrice.Text).ToString();
        }

        private void cname_TextChanged(object sender, EventArgs e)
        {
            

        }

        private void CustomerNameslist_Click(object sender, EventArgs e)
        {
            
        }

        private void CustomerNameslist_SelectedValueChanged(object sender, EventArgs e)
        {

            string[] ids = CustomerNameslist.SelectedItem.ToString().Split('-');
            string strSQL = "Select CNIC, CustomerName from Customer where ID=" + ids[0];
            
           
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                OleDbCommand command = new OleDbCommand(strSQL, connection);
                try
                {
                    //cnic.Items.Clear();
                    Refresh();
                    connection.Open();
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        cname.Items.Clear();
                        while (reader.Read())
                        {
                            //CID.Text = reader[0].ToString();
                            cnic.Items.Add(reader[0].ToString());
                            cname.Items.Add(reader[1].ToString());
                            //unpaid.Text = reader[2].ToString();
                            //CAddress.Text = reader[3].ToString();
                        }
                        cname.SelectedIndex = 0;
                       
                        if (cnic.Items.Count == 1)
                        {
                            string strSQL1 = "Select id,PrevAmount,Address,Status from Customer where CustomerName='" + cname.SelectedItem.ToString() + "' and CNIC='" + cnic.Items[0].ToString() + "'";
                            GetCustomerRecord(strSQL1);
                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                connection.Close();
                CustomerNameslist.Visible = false;
            }
        }

        private void printcnic_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void tname_TextChanged(object sender, EventArgs e)
        {
            if (tname.Text != "")
            {
                string strSQL = "Select ID,CustomerName,Address from Customer where CustomerName like '" + tname.Text + "%'";
                CustomerNameslist.Items.Clear();
                CustomerNameslist.BringToFront();
                //Customer.SendToBack();
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    OleDbCommand command = new OleDbCommand(strSQL, connection);
                    try
                    {
                        connection.Open();
                        using (OleDbDataReader reader = command.ExecuteReader())
                        {

                            while (reader.Read())
                            {

                                CustomerNameslist.Items.Add(reader[0].ToString() + "-" + reader[1].ToString() + " | " + reader[2].ToString());
                            }
                            if (reader.HasRows)
                            {
                                CustomerNameslist.Visible = true;
                            }
                            else
                                CustomerNameslist.Visible = false;

                        }
                        connection.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }


            }
            else
                CustomerNameslist.Visible = false;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pname_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tproduct_TextChanged(object sender, EventArgs e)
        {
            if (tproduct.Text != "")
            {
                string strSQL = "Select ProductName,PackName from ProductName where ProductName like '" + tproduct.Text + "%'";
                ProductList.Items.Clear();
                ProductList.BringToFront();
                //Customer.SendToBack();
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    OleDbCommand command = new OleDbCommand(strSQL, connection);
                    try
                    {
                        connection.Open();
                        using (OleDbDataReader reader = command.ExecuteReader())
                        {

                            while (reader.Read())
                            {

                                ProductList.Items.Add(reader[0].ToString() + "|" + reader[1].ToString());
                            }
                            if (reader.HasRows)
                            {
                                ProductList.Visible = true;
                            }
                            else
                                ProductList.Visible = false;

                        }
                        connection.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }


            }
            else
                ProductList.Visible = false;
        }

        private void ProductList_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                cname.Enabled = false;
                retail.Clear();
                UnitPrice.Clear();
                stock.Clear();
                string[] ids = ProductList.SelectedItem.ToString().Split('|');
                pname.Items.Clear();
                cPackingStock.Items.Clear();
                pname.Items.Add(ids[0]);
                cPackingStock.Items.Add(ids[1]);
                pname.SelectedIndex = 0;
                cPackingStock.SelectedIndex = 0;

                if (cname.SelectedItem != null)
                {
                    GetProd(pname.SelectedItem.ToString(), cPackingStock.SelectedItem.ToString());
                    GetStock(pname.SelectedItem.ToString(), cPackingStock.SelectedItem.ToString());
                }
                else
                {
                    MessageBox.Show("Please Select the Customer");
                    cname.Enabled = true;
                }
            }
            catch { }
            ProductList.Visible = false;
        }
       

       
    }
}
