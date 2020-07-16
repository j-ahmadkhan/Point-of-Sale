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
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        public string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Application.StartupPath + "/POS.accdb";

        double GetDecimal(string txt)
        {
            try
            {
                double d;
                double.TryParse(txt, out d);
                return d;
            }
            catch
            { return 0; }

        }

        void GetInvoice()
        {
            string strSQL = "select distinct CustomerName,SaleDate,customerid from saleinvoice where invoice ='" + tinvoice.Text + "'";

            string strSQL1 = "select ProductName,PackName,Quanity,TradePrice,Invoice_Product.Tax,productid,customerid from saleinvoice where invoice ='" + tinvoice.Text + "'";

            
            listView1.Items.Clear();
            CustomerName.Text = "";
            SDate.Text = "";
            ProductName.Text = "";
            Unpaid.Text = "";
            Quantiy.Clear();
            ReturnedAmount.Clear();
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
                            button4.Enabled = true;
                            CustomerName.Text = reader[0].ToString();
                            SDate.Text = reader[1].ToString();
                            string pam = reader[2].ToString();
                            
                            command = new OleDbCommand(strSQL1, connection);
                            using (OleDbDataReader reader1 = command.ExecuteReader())
                            {
                                while (reader1.Read())
                                {
                                    string[] row = { reader1[0].ToString(), reader1[1].ToString(), reader1[2].ToString(), reader1[3].ToString(), reader1[4].ToString(), reader1[5].ToString(), reader1[6].ToString()};
                                    ListViewItem lv = new ListViewItem(row);
                                    listView1.Items.Add(lv);
                                }
                            }

                            string strSQL2 = "select PrevAmount from Customer where ID =" + pam + "";

                            command = new OleDbCommand(strSQL2, connection);
                            try
                            {
                                using (OleDbDataReader reader2 = command.ExecuteReader())
                                {
                                    while (reader2.Read())
                                    {
                                        Unpaid.Text = reader2[0].ToString();
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                                connection.Close();
                            }

                        }
                        else
                            MessageBox.Show("No Record Found",tinvoice.Text);
                    }
                    connection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void SaleDate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void Quantiy_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void TradePrice_TextChanged(object sender, EventArgs e)
        {

        }

        private void button18_Click(object sender, EventArgs e)
        {
            GetInvoice();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ProductName.Text = listView1.SelectedItems[0].SubItems[0].Text + " " + listView1.SelectedItems[0].SubItems[1].Text;
            Quantiy.Text = listView1.SelectedItems[0].SubItems[2].Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            CustomerName.Text = "";
            SDate.Text = "";
            ProductName.Text = "";
            Unpaid.Text = "";
            Quantiy.Clear();
            ReturnedAmount.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form6 f6 = new Form6();
            f6.Show();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            if(listView1.SelectedItems.Count > 0)
            {
                DialogResult dr = MessageBox.Show("Return Items Back to Stock?", "Warning", MessageBoxButtons.YesNo);
                if (dr == System.Windows.Forms.DialogResult.Yes)
                {
                    
                    double prevAmt = 0;
     
                    OleDbCommand command = null;
                    OleDbTransaction tr = null;
                    string strSQL1 = "INSERT INTO ReturnedProduct(invoice,productid,quantity,reimursedamount,returndate) VALUES('" + tinvoice.Text + "'," + listView1.SelectedItems[0].SubItems[5].Text + "," + Quantiy.Text + ",'" + ReturnedAmount.Text + "','" + ReturnDate.Value.ToShortDateString() + "')";
                    
                    using (OleDbConnection connection = new OleDbConnection(connectionString))
                    {
                        try
                        {
                            connection.Open();
                            command = new OleDbCommand(strSQL1, connection);
                            command.ExecuteReader();
                            
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            //tr.Rollback();
                            connection.Close();
                            return;
                        }

                    
                            try
                            {

                                string strSQL6 = "Select Quantity from RunningStock where ProductName='" + listView1.SelectedItems[0].SubItems[0].Text + "' and PackName='" + listView1.SelectedItems[0].SubItems[1].Text + "'";
         
                                command = new OleDbCommand(strSQL6, connection);
                                double stk = 0;
                                using (OleDbDataReader reader = command.ExecuteReader())
                                {
                                     while (reader.Read())
                                    {
                                       stk = GetDecimal(reader[0].ToString());
                                    }
                        
                                }

                                stk = stk + GetDecimal(Quantiy.Text);
                                string strSQL7 = "Update RunningStock SET Quantity =" + stk.ToString() + " where ProductName ='" + listView1.SelectedItems[0].SubItems[0].Text + "' and PackName='" + listView1.SelectedItems[0].SubItems[1].Text + "'";
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

                        dr = MessageBox.Show("Update Previous Unpaid Amount by Customer?", "Warning", MessageBoxButtons.YesNo);
                        if (dr == System.Windows.Forms.DialogResult.Yes)
                        {

                            string strSQL5 = "Select PrevAmount from Customer where ID=" + listView1.SelectedItems[0].SubItems[6].Text;

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
                                prevAmt -= GetDecimal(ReturnedAmount.Text);
                                string strSQL4 = "Update Customer SET PrevAmount ='" + prevAmt.ToString() + "' where ID =" + listView1.SelectedItems[0].SubItems[6].Text;
                                command = new OleDbCommand(strSQL4, connection);
                                command.ExecuteReader();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                                connection.Close();
                            }
                        }
                        MessageBox.Show("Items Returned Back to Stock");
                        connection.Close();
                        Refresh();
                       // Form1 f1 = new Form1();
                       // f1.UpdateStock();
                      
                       
                    }
                }
            }
            else
                MessageBox.Show("No Items selected to return back");
            }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Refresh();
        }

        void Refresh()
        {
            listView1.Items.Clear();
            CustomerName.Text = "";
            SDate.Text = "";
            ProductName.Text = "";
            Unpaid.Text = "0";
            Quantiy.Text = "0";
            ReturnedAmount.Text = "0";
            button4.Enabled = false;
        }

        private void tinvoice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            { GetInvoice(); }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                object readOnly = true;
                object isVisible = true;
                string std = SDate.Text;
                string[] ab = std.Split('/');
                int yr = int.Parse(ab[2]);
                int mn = int.Parse(ab[0]);
                int dy = int.Parse(ab[1]);

                DateTime dt = new DateTime(yr, mn, dy);
                string lng = dt.ToLongDateString();
                string dir = Application.StartupPath + "/" + lng + "/" + tinvoice.Text + ".doc";

                object missing = System.Reflection.Missing.Value;
                Microsoft.Office.Interop.Word.Application wrd = new Microsoft.Office.Interop.Word.Application { Visible = true };
                Microsoft.Office.Interop.Word.Document doc = wrd.Documents.Open(dir, ReadOnly: readOnly, Visible: isVisible);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Can Not Find the Receipt", "Error");

            }
        }
        
    }
}
