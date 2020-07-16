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
    public partial class Form6 : Form
    {
        public Form6()
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


        private void Form6_Load(object sender, EventArgs e)
        {
            DataRecordBind("select Invoice, CustomerName, ProductName, PackName, Quantity, reimursedamount as ReturnedAmount, ReturnDate from returnrecord");
        }

        private void SaleDate_ValueChanged(object sender, EventArgs e)
        {
            DataRecordBind("select Invoice, CustomerName, ProductName, PackName, Quantity, reimursedamount as ReturnedAmount, ReturnDate from returnrecord where ReturnDate='"+ ReturnDate.Value.ToShortDateString() +"'");
        }

        private void button18_Click(object sender, EventArgs e)
        {
            DataRecordBind("select Invoice, CustomerName, ProductName, PackName, Quantity, reimursedamount as ReturnedAmount, ReturnDate from returnrecord where Invoice='" + tinvoice.Text + "'");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            printDocument1.Print();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Bitmap bm = new Bitmap(this.dataGridView1.Width, this.dataGridView1.Height);
            dataGridView1.DrawToBitmap(bm, new Rectangle(0, 0, this.dataGridView1.Width, this.dataGridView1.Height));
            e.Graphics.DrawImage(bm, 0, 0);
        }

        private void tinvoice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 13)
                DataRecordBind("select Invoice, CustomerName, ProductName, PackName, Quantity, reimursedamount as ReturnedAmount, ReturnDate from returnrecord where Invoice='" + tinvoice.Text + "'");
        }
    }
}
