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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //for 2007 Provider=Microsoft.Jet.OLEDB.4.0
        public string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Application.StartupPath + "/POS.accdb";
        public string OldTax = "";
        OleDbDataAdapter da;
        private BindingSource bindingSource = null;
        private OleDbCommandBuilder oleCommandBuilder = null;
        DataTable dataTable = new DataTable();

        OleDbDataAdapter da1;
        private BindingSource bindingSource1 = null;
        private OleDbCommandBuilder oleCommandBuilder1 = null;
        DataTable dataTable1 = new DataTable();

        OleDbDataAdapter da2;
        private BindingSource bindingSource2 = null;
        private OleDbCommandBuilder oleCommandBuilder2 = null;
        DataTable dataTable2 = new DataTable();


        bool IsDecimal(string txt)
        {
            try
            {
                double d;
                return (double.TryParse(txt, out d));
            }
            catch
            { return false; }
        }

        public static void Backup()
        {
            string dt = Application.StartupPath + "/Backup/POS.accdb_" + DateTime.Now.GetHashCode();
            File.Copy(Application.StartupPath + "/POS.accdb", dt, true);
            
        }

        void ManageCurrentStock(string product, string packing, string quantity, string retail)
        {
            string strSQL = "Select * from RunningStock where ProductName ='" + product + "' and PackName ='" + packing + "'";
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                OleDbCommand command = new OleDbCommand(strSQL, connection);
                try
                {
                    connection.Open();
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            {
                                decimal q, q1;

                                decimal.TryParse(reader[3].ToString(), out q);
                                decimal.TryParse(quantity, out q1);
                                q += q1;

                                string strSQL2 = "UPDATE RunningStock SET Quantity = '" + q.ToString() + "' where ProductName ='" + product + "' and PackName ='" + packing + "'";
                                command = new OleDbCommand(strSQL2, connection);
                                command.ExecuteReader();
                            }
                        }
                        else
                        {

                            string strSQL2 = "INSERT into RunningStock(ProductName,PackName,Quantity) VALUES('" + product + "','" + packing + "','" + quantity + "')";
                            command = new OleDbCommand(strSQL2, connection);
                            command.ExecuteReader();

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


        private void button1_Click(object sender, EventArgs e)
        {
            if (cProductStock.SelectedItem != null && cPackingStock.SelectedItem != null && cSupplierStock.SelectedItem != null && Quantity.Text != ""  && Tax.Text != "")
            {
                string strSQL = "INSERT INTO Stock(ProductName, UnitPrice, SupplierName,Retail,Quantity,Expiry,PurchaseDate,PackName) VALUES ('" + cProductStock.SelectedItem.ToString() + "','" + UnitPrice.Text + "','" + cSupplierStock.SelectedItem.ToString() + "','" + Tax.Text + "','" + Quantity.Text + "','" + Expiry.Value.ToShortDateString() + "','" + PurchaseDate.Value.ToShortDateString() + "','" + cPackingStock.SelectedItem.ToString() + "')";

                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    OleDbCommand command = new OleDbCommand(strSQL, connection);

                    try
                    {
                        connection.Open();
                        command.ExecuteReader();
                        connection.Close();
                        ManageCurrentStock(cProductStock.SelectedItem.ToString(), cPackingStock.SelectedItem.ToString(), Quantity.Text, Tax.Text);
                        //cPackingStock.Items.Clear();
                        //cProductStock.Items.Clear();
                        //cSupplierStock.Items.Clear();
                        //Tax.Clear();
                        //Quantity.Clear();
                        //UnitPrice.Clear();
                        //Tax.Enabled = false;
                        //UnitPrice.Enabled = false;
                        MessageBox.Show("Item Added to Stock");
                        DataBind();
                        tabControl1.SelectedIndex = 0;

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            else {
                MessageBox.Show("Item Details Missing. Please Add the details", "Failure!");
            }
        }

        
        void ComboItemsShow(string tablename, ComboBox cb)
        {
            string strSQL = "";
            if(tablename == "ProductName")
                strSQL = "Select distinct productname from " + tablename;
            else
                strSQL = "Select * from " + tablename;
            cb.Items.Clear();
            // Create a connection  
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                // Create a command and set its connection  
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
        private void cProductStock_Click(object sender, EventArgs e)
        {
            ComboItemsShow("ProductName", cProductStock);
        }

        private void cProdName_Click(object sender, EventArgs e)
        {
            //ComboItemsShow("ProductName", cProdName);

        }

        private void cProdName_SelectedValueChanged(object sender, EventArgs e)
        {
            
        }

        private void cSupplier_Click(object sender, EventArgs e)
        {
            ComboItemsShow("Supplier", cSupplier);
        }

        private void cSupplier_SelectedValueChanged(object sender, EventArgs e)
        {
            tSupplier.Text = cSupplier.SelectedItem.ToString();
            button4.Enabled = true;
            button7.Enabled = true;
            button5.Enabled = false;
        }

        private void cSupplierStock_Click(object sender, EventArgs e)
        {
            ComboItemsShow("Supplier", cSupplierStock);
        }

        void Supp_Pack_Operation(string tablename, string col ,string newvalue , string oldvalue, string op)
        {
            string strSQL = "";
            string strSQL2 = "";
            string strSQL3 = "";
            if(op == "add")
                strSQL = "INSERT INTO " + tablename + "(" + col + ") VALUES ('" + newvalue + "')";
            else if (op == "update")
            {
                Backup();
                strSQL = "UPDATE " + tablename + " SET " + col + " = '" + newvalue + "' WHERE " + col + " = '" + oldvalue + "'";
                strSQL2 = "UPDATE Stock SET " + col + " = '" + newvalue + "' WHERE " + col + " = '" + oldvalue + "'"; // update in stock too
                if(tablename == "Packing")
                    strSQL3 = "UPDATE ProductName SET " + col + " = '" + newvalue + "' WHERE " + col + " = '" + oldvalue + "'"; // update in Products too
               
            }
            else if (op == "delete")
            {
                Backup();
                strSQL = "DELETE FROM " + tablename + " WHERE " + col + " = '" + oldvalue + "'";
            }
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
       
                OleDbCommand command = new OleDbCommand(strSQL, connection);
                try
                {
                   
                    connection.Open();
                    command.ExecuteReader();
                    if (op == "update")
                    {
                        command = new OleDbCommand(strSQL2, connection);
                        command.ExecuteReader();
                    }
                    if (tablename == "Packing" && op == "update")
                    {
                        command = new OleDbCommand(strSQL3, connection);
                        command.ExecuteReader();
                    }
                    connection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        void GetURPrice(string product, string pack, TextBox up, TextBox rp)
        {
            string strSQL = "Select UnitPrice, Retail from ProductName WHERE ProductName ='" + product + "' and PackName ='" + pack + "'";
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                OleDbCommand command = new OleDbCommand(strSQL, connection);
                try
                {
                    connection.Open();
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        up.Clear();
                        rp.Clear();
                        while (reader.Read())
                        {
                            up.Text = reader[1].ToString();
                            rp.Text = reader[0].ToString();
                        }
                        if (!reader.HasRows)
                        {
                            MessageBox.Show(product + " " + pack + " does not exist, Please add this product first", "No Product");
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

        void Product_Operation(string name, string packing, string unit, string retail, string oldname, string oldpack, string op)
        {
            string strSQL = "";
            string strSQL2 = "";
            if (op == "add")
                strSQL = "INSERT INTO ProductName(ProductName,PackName,UnitPrice,Retail) VALUES ('" + name + "','" + packing + "','" + unit + "','" + retail + "')";
            else if (op == "update")
            {
                Backup();
                strSQL = "UPDATE ProductName SET ProductName = '" + name + "',PackName = '" + packing + "',UnitPrice = '" + unit + "',Retail = '" + retail + "' WHERE ProductName = '" + oldname + "' and PackName = '" + oldpack + "'";
                strSQL2 = "UPDATE Stock SET ProductName = '" + name + "',PackName = '" + packing + "',UnitPrice = '" + unit + "',Retail = '" + retail + "' WHERE ProductName = '" + oldname + "' and PackName = '" + oldpack + "'";// update in stock too
               
            }
            else if (op == "delete")
            {
                Backup();
                strSQL = "DELETE FROM ProductName WHERE ProductName = '" + oldname + "' and PackName = '" + oldpack + "'";
            }
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {

                OleDbCommand command = new OleDbCommand(strSQL, connection);
                try
                {

                    connection.Open();
                    command.ExecuteReader();
                    if (op == "update")
                    {
                        command = new OleDbCommand(strSQL2, connection);
                        command.ExecuteReader();
                    }
                    connection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
      
        private void button6_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Update Product?", "Warning", MessageBoxButtons.YesNo);
            if (dr == System.Windows.Forms.DialogResult.Yes)
            {
                Backup();
                ProductGrid.EndEdit(); //very important step
                da2.Update(dataTable2);
                MessageBox.Show("Product Updated");
                DataBindProduct();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
             DialogResult dr = MessageBox.Show("Update Product?", "Warning", MessageBoxButtons.YesNo);
             if (dr == System.Windows.Forms.DialogResult.Yes)
             {
                 
             }
            
             button2.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (tProductName.Text != "" && cPack.SelectedItem != null && tRPrice.Text != "")
            {
                Product_Operation(tProductName.Text, cPack.SelectedItem.ToString(), tUPrice.Text, tRPrice.Text, "", "", "add");
                MessageBox.Show("Product Added");
                DataBindProduct();
            }
            tProductName.Clear();
        }

        private void cPackingStock_Click(object sender, EventArgs e)
        {
            ComboItemsShow("Packing", cPackingStock);
        }


        private void cPackingStock_SelectedValueChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                if (cProductStock.SelectedItem != null)
                    DataRecordBind("Select * From Stock where ProductName='" + cProductStock.SelectedItem.ToString() + "' and PackName='" + cPackingStock.SelectedItem.ToString() + "'");
            }
            else if (cProductStock.SelectedItem != null)
            GetURPrice(cProductStock.SelectedItem.ToString(), cPackingStock.SelectedItem.ToString(), Tax, UnitPrice);
        }

        private void cPack_Click(object sender, EventArgs e)
        {
            ComboItemsShow("Packing", cPack);
        }

        private void coPacking_Click(object sender, EventArgs e)
        {
            ComboItemsShow("Packing", coPacking);
        }

        private void cPack_SelectedValueChanged(object sender, EventArgs e)
        {
           
        }

        private void cProductStock_SelectedValueChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                DataRecordBind("Select * From Stock where ProductName='" + cProductStock.SelectedItem.ToString() + "'");
            }
            else if (cPackingStock.SelectedItem != null)
                GetURPrice(cProductStock.SelectedItem.ToString(), cPackingStock.SelectedItem.ToString(), Tax, UnitPrice);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                Tax.Enabled = true;
                UnitPrice.Enabled = true;
            }
            else
            {
                Tax.Enabled = false;
                UnitPrice.Enabled = false;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Update Supplier?", "Warning", MessageBoxButtons.YesNo);
            if (dr == System.Windows.Forms.DialogResult.Yes && tSupplier.Text != "")
            {
                Supp_Pack_Operation("Supplier","SupplierName",tSupplier.Text,cSupplier.SelectedItem.ToString(),"update");
                tSupplier.Clear();
            }
            button4.Enabled = false;
            button7.Enabled = false;
            button5.Enabled = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (tSupplier.Text != "")
            {
                Supp_Pack_Operation("Supplier", "SupplierName", tSupplier.Text, "", "add");
                MessageBox.Show("Supplier Added");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Delete Supplier?", "Warning", MessageBoxButtons.YesNo);
            if (dr == System.Windows.Forms.DialogResult.Yes)
            {
                Supp_Pack_Operation("Supplier", "SupplierName", "", cSupplier.SelectedItem.ToString(), "delete");
                tSupplier.Clear();
            }
            button7.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = true;
        }

        private void Quantity_Click(object sender, EventArgs e)
        {
            Quantity.Clear();
        }

        private void tRPrice_Click(object sender, EventArgs e)
        {
            tRPrice.Clear();
        }

        private void tUPrice_Click(object sender, EventArgs e)
        {
            tUPrice.Clear();
        }

        private void coPacking_SelectedValueChanged(object sender, EventArgs e)
        {
            tPacking.Text = coPacking.SelectedItem.ToString();
            button8.Enabled = true;
            button10.Enabled = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           // DataSet1TableAdapters.SaleInvoiceTableAdapter SA = new DataSet1TableAdapters.SaleInvoiceTableAdapter();
           // SA.Connection.ConnectionString = connectionString;
            

            DataBind();
            DataBindProduct();
            tabControl1.TabPages[2].Hide();
            DataRecordBind("Select * From Stock where PurchaseDate='" + PurchaseDate.Value.ToShortDateString() + "'");
            string strSQL = "Select TValue from Tax";
            string strSQL1 = "Select * from CompanyInfo";
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            { 
                OleDbCommand command = new OleDbCommand(strSQL, connection);
                try
                {
                    connection.Open();
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        reader.Read();
                        {
                            SalesTax.Text = reader[0].ToString();
                            OldTax = reader[0].ToString();
                        }
                    }
                    command = new OleDbCommand(strSQL1, connection);
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        reader.Read();
                        {
                            Company.Text = reader[1].ToString();
                            Address.Text = reader[2].ToString();
                            Phone.Text = reader[3].ToString();
                            NTN.Text = reader[4].ToString();
                            STN.Text = reader[5].ToString();
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

        private void button8_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Update Packing?", "Warning", MessageBoxButtons.YesNo);
            if (dr == System.Windows.Forms.DialogResult.Yes && tPacking.Text != "")
            {
                Supp_Pack_Operation("Packing", "PackName", tPacking.Text, coPacking.SelectedItem.ToString(), "update");
                tPacking.Clear();
            }
            button8.Enabled = false;
            button10.Enabled = true;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (tPacking.Text != "")
            {
                Supp_Pack_Operation("Packing", "PackName", tPacking.Text, "", "add");
                MessageBox.Show("Pack Type Added");
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Update Sales Tax?", "Warning", MessageBoxButtons.YesNo);
            if (dr == System.Windows.Forms.DialogResult.Yes && SalesTax.Text != "" && IsDecimal(SalesTax.Text))
            {         
            string strSQL = "UPDATE Tax SET TValue ='" + SalesTax.Text + "' WHERE TValue ='" + OldTax + "'";
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                OleDbCommand command = new OleDbCommand(strSQL, connection);
                try
                {
                    connection.Open();
                    command.ExecuteReader();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
                
            }
        }

        private void DataBind()
        {
            RunningStockGrid.DataSource = null;

            dataTable.Clear();

            string strSQL = "SELECT * FROM RunningStock";

            OleDbConnection connection = new OleDbConnection(connectionString);
            connection.Open();
            OleDbCommand command = connection.CreateCommand();
            command.CommandText = strSQL;
            try
            {
                
                da = new OleDbDataAdapter(strSQL, connection);
                oleCommandBuilder = new OleDbCommandBuilder(da);
                da.Fill(dataTable);
                bindingSource = new BindingSource { DataSource = dataTable };
                RunningStockGrid.DataSource = bindingSource;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            connection.Close();
        }

        private void DataBindProduct()
        {
            ProductGrid.DataSource = null;

            dataTable2.Clear();

            string strSQL = "SELECT * FROM ProductName";

            OleDbConnection connection = new OleDbConnection(connectionString);
            connection.Open();
            OleDbCommand command = connection.CreateCommand();
            command.CommandText = strSQL;
            try
            {

                da2 = new OleDbDataAdapter(strSQL, connection);
                oleCommandBuilder2 = new OleDbCommandBuilder(da2);
                da2.Fill(dataTable2);
                bindingSource2 = new BindingSource { DataSource = dataTable2 };
                ProductGrid.DataSource = bindingSource2;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            connection.Close();
        }

        private void DataRecordBind(string strSQL)
        {
            StockRecordGrid.DataSource = null;
            dataTable1.Clear();

            OleDbConnection connection = new OleDbConnection(connectionString);
           
            try
            {
                connection.Open();
                OleDbCommand command = connection.CreateCommand();
                command.CommandText = strSQL;
                da1 = new OleDbDataAdapter(strSQL, connection);
                oleCommandBuilder1 = new OleDbCommandBuilder(da1);
                da1.Fill(dataTable1);
                bindingSource1 = new BindingSource { DataSource = dataTable1 };
                StockRecordGrid.DataSource = bindingSource1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            connection.Close();
        }

        public void UpdateStock()
        {
            try
            {
                Backup();
                RunningStockGrid.EndEdit(); //very important step
                da.Update(dataTable);
                MessageBox.Show("Running Stock Updated");
                DataBind();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void PrintRunningStock()
        {
            try
            {
                //Create an instance for word app  
                Microsoft.Office.Interop.Word.Application winword = new Microsoft.Office.Interop.Word.Application();
                winword.Visible = false;

                //Create a missing variable for missing value  
                object missing = System.Reflection.Missing.Value;

                //Create a new document  
                Microsoft.Office.Interop.Word.Document document = winword.Documents.Add(ref missing, ref missing, ref missing, ref missing);
                document.Sections.PageSetup.TopMargin = 30.0f;

                document.Sections.PageSetup.RightMargin = 30.0f;
                document.Sections.PageSetup.LeftMargin = 30.0f;
                document.Sections.PageSetup.BottomMargin = 40.0f;
                document.Sections.PageSetup.FooterDistance = 10f;
                //document.SpellingChecked = true;
                //document.DisableFeatures = true;
                document.ShowSpellingErrors = false;
                document.ShowGrammaticalErrors = false;
                document.PageSetup.PaperSize = Microsoft.Office.Interop.Word.WdPaperSize.wdPaperA5;
                document.PageSetup.Orientation = Microsoft.Office.Interop.Word.WdOrientation.wdOrientLandscape;

                document.Content.SetRange(0, 0);
                //Add paragraph with Heading 1 style  
                Microsoft.Office.Interop.Word.Paragraph para1 = document.Content.Paragraphs.Add(ref missing);
                para1.Range.Font.Size = 14F;
                para1.Range.Font.Name = "Arial Black";
               para1.Range.Font.Color = Microsoft.Office.Interop.Word.WdColor.wdColorBlueGray;
                para1.Range.Text = Company.Text;
                para1.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                para1.Range.InsertParagraphAfter();

               
                para1.Range.Font.Size = 11F;
                para1.Range.Font.Name = "Arial";
                para1.Range.InsertAfter ( "Stock State on " + DateTime.Now.ToLongDateString() + " at " + DateTime.Now.ToShortTimeString());
                //para1.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                para1.Range.InsertParagraphAfter();

                Microsoft.Office.Interop.Word.Table firstTable1 = document.Tables.Add(para1.Range, dataTable.Rows.Count, 4, ref missing, ref missing);
                firstTable1.Borders.Enable = 0;
                firstTable1.Rows.HeightRule = Microsoft.Office.Interop.Word.WdRowHeightRule.wdRowHeightAuto;
                //firstTable1.Rows.SetHeight(0.1f,Microsoft.Office.Interop.Word.WdRowHeightRule.wdRowHeightAuto);
                firstTable1.Columns[1].Width = 55f;
                firstTable1.Columns[2].Width = 170f;
                firstTable1.Columns[3].Width = 170f;
                firstTable1.Columns[4].Width = 55f;

                int r = 1;
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    //for (int j = 1; j < dataTable.Columns.Count; j++)
                    {
                        AddToCell(firstTable1, r, 1, 10f, 0, r.ToString());
                        AddToCell(firstTable1, r, 2, 10f, 1, dataTable.Rows[i][1].ToString());
                        AddToCell(firstTable1, r, 3, 10f, 1, dataTable.Rows[i][2].ToString());
                        AddToCell(firstTable1, r, 4, 10f, 0, dataTable.Rows[i][3].ToString());
                        r++;
                    }
                }

                string dir = Application.StartupPath + "/RunningStock";
                DirectoryInfo di = new DirectoryInfo(dir);
                if (!di.Exists)
                    di.Create();
                string dt = Application.StartupPath + "/RunningStock/Stock_" + DateTime.Now.ToLongDateString() + "_" + DateTime.Now.Hour.ToString() + "_" + DateTime.Now.Minute.ToString() + ".doc";
                object filename = dt;
                document.SaveAs(ref filename);
                //document.Close(ref missing, ref missing, ref missing);
                //document = null;
                //winword.Quit(ref missing, ref missing, ref missing);
                //winword = null;
                object readOnly = true;
                object isVisible = true;
                winword.Documents.Open(ref filename, ReadOnly: readOnly, Visible: isVisible);

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        void AddToCell(Microsoft.Office.Interop.Word.Table table, int rowindex, int colindex, float fontsize, int bold, string text)
        {
            table.Cell(rowindex, colindex).Range.Text = text;
            table.Cell(rowindex, colindex).Range.Font.Bold = bold;
            table.Cell(rowindex, colindex).Range.Font.Name = "verdana";
            table.Cell(rowindex, colindex).Range.Font.Size = fontsize;
            table.Cell(rowindex, colindex).Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Update Running Stock?", "Warning", MessageBoxButtons.YesNo);
            if (dr == System.Windows.Forms.DialogResult.Yes)
            {
                UpdateStock();
            }


        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                checkBox1.Hide();
                Quantity.Hide();
                button1.Hide();
                Tax.Hide();
                UnitPrice.Hide();
                label2.Hide();
                label6.Hide();
                label7.Hide();
                label16.Show();
                tabControl1.SelectedIndex = 2;
                tabControl1.TabPages[2].Show();
                
            }
            else
            {
                checkBox1.Show();
                Quantity.Show();
                button1.Show();
                Tax.Show();
                UnitPrice.Show();
                label2.Show();
                label6.Show();
                label7.Show();
                label16.Hide();
                tabControl1.SelectedIndex = 0;
                tabControl1.TabPages[2].Hide();
            }
        }

        private void cSupplierStock_SelectedValueChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                if (cProductStock.SelectedItem != null && cPackingStock.SelectedItem != null)
                    DataRecordBind("Select * From Stock where ProductName='" + cProductStock.SelectedItem.ToString() + "' and PackName='" + cPackingStock.SelectedItem.ToString() + "' and SupplierName ='" + cSupplierStock.SelectedItem.ToString() + "'");
                else
                    DataRecordBind("Select * From Stock where SupplierName ='" + cSupplierStock.SelectedItem.ToString() + "'");
            }
        }

        private void Expiry_ValueChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
                DataRecordBind("Select * From Stock where Expiry ='" + Expiry.Value.ToShortDateString() + "'");
        }

        private void PurchaseDate_ValueChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
                DataRecordBind("Select * From Stock where PurchaseDate ='" + PurchaseDate.Value.ToShortDateString() + "'");
        }

        private void button12_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Update Stock Record?", "Warning", MessageBoxButtons.YesNo);
            if (dr == System.Windows.Forms.DialogResult.Yes)
            {
                Backup();
                StockRecordGrid.EndEdit(); //very important step
                da1.Update(dataTable1);
                MessageBox.Show("Stock Record Updated");
                DataRecordBind("Select * from Stock");
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.Show();
        }

        private void RunningStockGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void RunningStockGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           // MessageBox.Show(sender.ToString()+" " +RunningStockGrid[e.ColumnIndex, e.RowIndex].Value.ToString());
            
        }

        private void button13_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3();
            f3.Company = Company.Text;
            f3.Address = Address.Text;
            f3.Phone = Phone.Text;
            f3.NTN = NTN.Text;
            f3.STN = STN.Text;
            f3.SaleTax = SalesTax.Text;
            f3.Show();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            Backup();
            MessageBox.Show("Backup Done!");
        }

        private void button15_Click(object sender, EventArgs e)
        {
            Form4 f4 = new Form4();
            f4.Show();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            printDocument1.Print();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Bitmap bm = new Bitmap(this.StockRecordGrid.Width, this.StockRecordGrid.Height);
            StockRecordGrid.DrawToBitmap(bm, new Rectangle(0, 0, this.StockRecordGrid.Width, this.StockRecordGrid.Height));
            e.Graphics.DrawImage(bm, 0, 0);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            Form5 f5 = new Form5();
            f5.Show();
        }

        private void tProductName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                string strSQL = "select productname from productname WHERE productname like '" + tProductName.Text + "%'";
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    OleDbCommand command = new OleDbCommand(strSQL, connection);
                    try
                    {
                        connection.Open();
                        using (OleDbDataReader reader = command.ExecuteReader())
                        {
                            reader.Read();
                            {
                                tProductName.Text = reader[0].ToString();
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
        }

        private void Company_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Update Company Info?", "Warning", MessageBoxButtons.YesNo);
            if (dr == System.Windows.Forms.DialogResult.Yes)
            {
                string strSQL = "UPDATE CompanyInfo SET CompanyName ='" + Company.Text + "', Address ='" + Address.Text + "', Phone ='" + Phone.Text + "', NTN ='" + NTN.Text + "', STN ='" + STN.Text + "' where id = 1";
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    OleDbCommand command = new OleDbCommand(strSQL, connection);
                    try
                    {
                        connection.Open();
                        command.ExecuteReader();
                        connection.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }

            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 2)
            {
                checkBox2.Checked = true;
            }
            else
                checkBox2.Checked = false;
        }

        private void checkBox3_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
                button19.Enabled = true;
            else
                button19.Enabled = false;
        }

        private void button19_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Clean All Data, Are you Sure?", "Warning", MessageBoxButtons.YesNo);
            if (dr == System.Windows.Forms.DialogResult.Yes)
            {
                dr = MessageBox.Show("Do you want to take a data Backup?", "Precaution", MessageBoxButtons.YesNo);
                if (dr == System.Windows.Forms.DialogResult.Yes)
                {
                    Backup();
                    MessageBox.Show("Backup Done, Now Cleaning up Data");
                }

                //string strSQL1 = "DELETE from CompanyInfo";
                string strSQL2 = "DELETE from customer";
                string strSQL3 = "DELETE from invoice_customer";
                string strSQL4 = "DELETE from invoice_product";
                string strSQL5 = "DELETE from sale";
                string strSQL6 = "DELETE from stock";
                string strSQL7 = "DELETE from packing";
                string strSQL8 = "DELETE from productname";
                string strSQL9 = "DELETE from returnedproduct";
                string strSQL10 = "DELETE from runningstock";
                string strSQL11 = "DELETE from supplier";

                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    //OleDbCommand command = new OleDbCommand(strSQL1, connection);
                    try
                    {
                        connection.Open();
                        OleDbCommand command;
                        //command.ExecuteReader();
                        //command = new OleDbCommand(strSQL1, connection);
                        //command.ExecuteReader();
                       
                        command = new OleDbCommand(strSQL2, connection);
                        dr = MessageBox.Show("DELETE CUSTOMER DATA?", "Precaution", MessageBoxButtons.YesNo);
                        if (dr == System.Windows.Forms.DialogResult.Yes)
                        command.ExecuteReader();
                        command = new OleDbCommand(strSQL3, connection);
                        dr = MessageBox.Show("DELETE CUSTOMER INVOICES?", "Precaution", MessageBoxButtons.YesNo);
                        if (dr == System.Windows.Forms.DialogResult.Yes)
                        command.ExecuteReader();
                        command = new OleDbCommand(strSQL4, connection);
                        dr = MessageBox.Show("DELETE PRODUCT INVOICES?", "Precaution", MessageBoxButtons.YesNo);
                        if (dr == System.Windows.Forms.DialogResult.Yes)
                        command.ExecuteReader();
                        command = new OleDbCommand(strSQL5, connection);
                        dr = MessageBox.Show("DELETE SALE DATE?", "Precaution", MessageBoxButtons.YesNo);
                        if (dr == System.Windows.Forms.DialogResult.Yes)
                        command.ExecuteReader();
                        command = new OleDbCommand(strSQL6, connection);
                        dr = MessageBox.Show("DELETE STOCK RECORD DATA?", "Precaution", MessageBoxButtons.YesNo);
                        if (dr == System.Windows.Forms.DialogResult.Yes)
                        command.ExecuteReader();
                        command = new OleDbCommand(strSQL7, connection);
                        dr = MessageBox.Show("DELETE PACKING TYPES?", "Precaution", MessageBoxButtons.YesNo);
                        if (dr == System.Windows.Forms.DialogResult.Yes)
                        command.ExecuteReader();
                        command = new OleDbCommand(strSQL8, connection);
                        dr = MessageBox.Show("DELETE PRODUCTS?", "Precaution", MessageBoxButtons.YesNo);
                        if (dr == System.Windows.Forms.DialogResult.Yes)
                        command.ExecuteReader();
                        command = new OleDbCommand(strSQL9, connection);
                        dr = MessageBox.Show("DELETE RETURNED ITEMS?", "Precaution", MessageBoxButtons.YesNo);
                        if (dr == System.Windows.Forms.DialogResult.Yes)
                        command.ExecuteReader();
                        command = new OleDbCommand(strSQL10, connection);
                        dr = MessageBox.Show("DELETE RUNNING STOCK?", "Precaution", MessageBoxButtons.YesNo);
                        if (dr == System.Windows.Forms.DialogResult.Yes)
                        command.ExecuteReader();
                        command = new OleDbCommand(strSQL11, connection);
                        dr = MessageBox.Show("DELETE SUPPLIER INFO?", "Precaution", MessageBoxButtons.YesNo);
                        if (dr == System.Windows.Forms.DialogResult.Yes)
                        command.ExecuteReader();
                        //MessageBox.Show("All Data is Formatted except CompanyInfo","Done");

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    connection.Close();

                }
            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            PrintRunningStock();
        }

       
       
    }
}
