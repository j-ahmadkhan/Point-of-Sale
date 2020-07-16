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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        public string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Application.StartupPath + "/POS.accdb";
      
        OleDbDataAdapter da;
        private BindingSource bindingSource = null;
        private OleDbCommandBuilder oleCommandBuilder = null;
        DataTable dataTable = new DataTable();

        private void button2_Click(object sender, EventArgs e)
        {
            string strSQL = "";
            if (CName.Text != "" && CNIC.Text != "")
            {
                strSQL = "INSERT INTO Customer(CustomerName,CNIC,Address,Phone,Status,PrevAmount) VALUES ('" + CName.Text + "','" + CNIC.Text + "','" + Address.Text + "','" + Phone.Text + "','" + Status.Text + "','" + PrevAmount.Text + "')";

                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {

                    OleDbCommand command = new OleDbCommand(strSQL, connection);
                    try
                    {

                        connection.Open();
                        command.ExecuteReader();
                        connection.Close();
                        MessageBox.Show("New Customer Added");
                        CName.Clear();
                        CNIC.Clear();
                        Address.Clear();
                        Phone.Clear();
                        Status.Clear();
                        DataRecordBind("Select * from Customer");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void DataRecordBind(string strSQL)
        {
            dataGridView1.DataSource = null;
            dataTable.Clear();

            //string strSQL = "Select * from Customer";
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

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Update Running Stock?", "Warning", MessageBoxButtons.YesNo);
            if (dr == System.Windows.Forms.DialogResult.Yes)
            {
                //Form1.Backup();
                dataGridView1.EndEdit(); //very important step
                da.Update(dataTable);
                MessageBox.Show("Customer Record Updated");
                DataRecordBind("Select * from Customer");
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            DataRecordBind("Select * from Customer");
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Phone_TextChanged(object sender, EventArgs e)
        {

        }

        private void CName_TextChanged(object sender, EventArgs e)
        {
            DataRecordBind("Select * from Customer where CustomerName like '"+ CName.Text +"%'");
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
