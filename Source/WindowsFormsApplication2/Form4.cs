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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }
        public string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Application.StartupPath + "/POS.accdb";
        public string OldTax = "";
        OleDbDataAdapter da;
        private BindingSource bindingSource = null;
        private OleDbCommandBuilder oleCommandBuilder = null;
        DataTable dataTable = new DataTable();

        private void DataRecordBind(string strSQL)
        {
            dataGridView1.DataSource = null;
            dataTable.Clear();

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
                dataGridView1.DataSource = bindingSource;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            connection.Close();
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
        
        private void Form4_Load(object sender, EventArgs e)
        {
            DataRecordBind("select CustomerName,Invoice,SaleDate,ProductName,PackName,Quanity,Invoice_Product.Tax,TradePrice,Sale.Tax,GrandTotal,RemainingAmount from saleinvoice");
            ComboItemsShow("ProductName", prname);
            ComboItemsShow("Packing",cPackingStock);

            
        }

        private void Invoice_Click(object sender, EventArgs e)
        {

        }

        private void SaleDate_ValueChanged(object sender, EventArgs e)
        {
            DataRecordBind("Select CustomerName,Invoice,SaleDate,ProductName,PackName,Quanity,Invoice_Product.Tax,TradePrice,Sale.Tax,GrandTotal,RemainingAmount from saleinvoice where SaleDate ='" + SaleDate.Value.ToShortDateString() + "'");
        }
        private void prname_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cPackingStock.SelectedItem != null)
            {
                DataRecordBind("select CustomerName,Invoice,SaleDate,ProductName,PackName,Quanity,Invoice_Product.Tax,TradePrice,Sale.Tax,GrandTotal,RemainingAmount from saleinvoice where ProductName='" + prname.SelectedItem.ToString() + "' and Packname='" + cPackingStock.SelectedItem.ToString() + "'");
            }
        }

        private void cPackingStock_SelectedValueChanged(object sender, EventArgs e)
        {
            if (prname.SelectedItem != null)
            {
                DataRecordBind("select CustomerName,Invoice,SaleDate,ProductName,PackName,Quanity,Invoice_Product.Tax,TradePrice,Sale.Tax,GrandTotal,RemainingAmount from saleinvoice where ProductName='" + prname.SelectedItem.ToString() + "' and Packname='" + cPackingStock.SelectedItem.ToString() + "'");
            }
        }

        private void CName_TextChanged(object sender, EventArgs e)
        {
            DataRecordBind("Select CustomerName,Invoice,SaleDate,ProductName,PackName,Quanity,Invoice_Product.Tax,TradePrice,Sale.Tax,GrandTotal,RemainingAmount from saleinvoice where CustomerName like '" + CName.Text + "%'");
        }

        private void tinvoice_TextChanged(object sender, EventArgs e)
        {
            if(!checkBox1.Checked)
                DataRecordBind("Select CustomerName,Invoice,SaleDate,ProductName,PackName,Quanity,Invoice_Product.Tax,TradePrice,Sale.Tax,GrandTotal,RemainingAmount from saleinvoice where invoice like '" + tinvoice.Text + "%'");
            else
                DataRecordBind("Select CustomerName,Invoice,SaleDate,ProductName,PackName,Quanity,Invoice_Product.Tax,TradePrice,Sale.Tax,GrandTotal,RemainingAmount from saleinvoice where invoice = '" + tinvoice.Text + "'");
        }

        private void button18_Click(object sender, EventArgs e)
        {
            printDocument1.Print();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Bitmap bm = new Bitmap(this.dataGridView1.Width, this.dataGridView1.Height);
            dataGridView1.DrawToBitmap(bm, new Rectangle(0, 0, this.dataGridView1.Width, this.dataGridView1.Height));
            e.Graphics.DrawImage(bm, 0, 0);
        }
    }
}
