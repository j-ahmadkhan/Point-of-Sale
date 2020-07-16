namespace WindowsFormsApplication2
{
    partial class Form3
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.Product = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Packing = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.RetailPrice = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Quantity = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Price = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SalesTaxInc = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ST = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.STQ = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.itemid = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CustomerId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label10 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.retail = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.UnitPrice = new System.Windows.Forms.TextBox();
            this.pname = new System.Windows.Forms.ComboBox();
            this.SaleDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tproduct = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.Quantiy = new System.Windows.Forms.TextBox();
            this.cPackingStock = new System.Windows.Forms.ListBox();
            this.stock = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.PID = new System.Windows.Forms.Label();
            this.TradePrice = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ProductList = new System.Windows.Forms.ListBox();
            this.label5 = new System.Windows.Forms.Label();
            this.Invoice = new System.Windows.Forms.Label();
            this.Customer = new System.Windows.Forms.GroupBox();
            this.tname = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.Status = new System.Windows.Forms.Label();
            this.cnic = new System.Windows.Forms.ListBox();
            this.CAddress = new System.Windows.Forms.Label();
            this.printcnic = new System.Windows.Forms.CheckBox();
            this.CID = new System.Windows.Forms.Label();
            this.unpaid = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.cname = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.CustomerNameslist = new System.Windows.Forms.ListBox();
            this.GrandTotal = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.Paid = new System.Windows.Forms.TextBox();
            this.GrandTax = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.Qty = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.prntformat = new System.Windows.Forms.CheckBox();
            this.WithoutColor = new System.Windows.Forms.CheckBox();
            this.A4 = new System.Windows.Forms.RadioButton();
            this.A5 = new System.Windows.Forms.RadioButton();
            this.LandScape = new System.Windows.Forms.CheckBox();
            this.DontLoad = new System.Windows.Forms.CheckBox();
            this.logo = new System.Windows.Forms.ComboBox();
            this.label20 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.Customer.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(839, 222);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(235, 35);
            this.button1.TabIndex = 6;
            this.button1.Text = "Add Items To List";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // listView1
            // 
            this.listView1.CheckBoxes = true;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Product,
            this.Packing,
            this.RetailPrice,
            this.Quantity,
            this.Price,
            this.SalesTaxInc,
            this.ST,
            this.STQ,
            this.itemid,
            this.CustomerId});
            this.listView1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.Location = new System.Drawing.Point(24, 286);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(1057, 217);
            this.listView1.TabIndex = 2;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // Product
            // 
            this.Product.Text = "Product";
            this.Product.Width = 240;
            // 
            // Packing
            // 
            this.Packing.Text = "Packing";
            this.Packing.Width = 126;
            // 
            // RetailPrice
            // 
            this.RetailPrice.Text = "Retail Price";
            this.RetailPrice.Width = 100;
            // 
            // Quantity
            // 
            this.Quantity.Text = "Quantity";
            this.Quantity.Width = 86;
            // 
            // Price
            // 
            this.Price.Text = "Trade Price";
            this.Price.Width = 94;
            // 
            // SalesTaxInc
            // 
            this.SalesTaxInc.Text = "Total Value(Inc. ST)";
            this.SalesTaxInc.Width = 130;
            // 
            // ST
            // 
            this.ST.Text = "S.T.@";
            this.ST.Width = 120;
            // 
            // STQ
            // 
            this.STQ.Text = "Sales Tax";
            this.STQ.Width = 157;
            // 
            // itemid
            // 
            this.itemid.Width = 0;
            // 
            // CustomerId
            // 
            this.CustomerId.Width = 1;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(469, 68);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(57, 16);
            this.label10.TabIndex = 27;
            this.label10.Text = "Packing";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(250, 108);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(64, 16);
            this.label7.TabIndex = 26;
            this.label7.Text = "Quantity";
            // 
            // retail
            // 
            this.retail.Enabled = false;
            this.retail.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.retail.Location = new System.Drawing.Point(120, 68);
            this.retail.MaxLength = 100;
            this.retail.Name = "retail";
            this.retail.Size = new System.Drawing.Size(100, 26);
            this.retail.TabIndex = 25;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(15, 75);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 16);
            this.label6.TabIndex = 24;
            this.label6.Text = "Retail Price";
            // 
            // UnitPrice
            // 
            this.UnitPrice.Enabled = false;
            this.UnitPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UnitPrice.Location = new System.Drawing.Point(572, 76);
            this.UnitPrice.MaxLength = 5;
            this.UnitPrice.Name = "UnitPrice";
            this.UnitPrice.Size = new System.Drawing.Size(100, 26);
            this.UnitPrice.TabIndex = 23;
            // 
            // pname
            // 
            this.pname.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pname.FormattingEnabled = true;
            this.pname.Location = new System.Drawing.Point(531, 19);
            this.pname.Name = "pname";
            this.pname.Size = new System.Drawing.Size(256, 26);
            this.pname.TabIndex = 2;
            this.pname.SelectedIndexChanged += new System.EventHandler(this.pname_SelectedIndexChanged);
            this.pname.SelectedValueChanged += new System.EventHandler(this.pname_SelectedValueChanged);
            this.pname.Click += new System.EventHandler(this.pname_Click);
            // 
            // SaleDate
            // 
            this.SaleDate.CustomFormat = "dd-MM-yyyy";
            this.SaleDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SaleDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.SaleDate.Location = new System.Drawing.Point(839, 157);
            this.SaleDate.Name = "SaleDate";
            this.SaleDate.Size = new System.Drawing.Size(235, 22);
            this.SaleDate.TabIndex = 21;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(469, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 16);
            this.label1.TabIndex = 17;
            this.label1.Text = "Product";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(836, 138);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 16);
            this.label3.TabIndex = 20;
            this.label3.Text = "Sale Date";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tproduct);
            this.groupBox1.Controls.Add(this.label19);
            this.groupBox1.Controls.Add(this.Quantiy);
            this.groupBox1.Controls.Add(this.cPackingStock);
            this.groupBox1.Controls.Add(this.stock);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.PID);
            this.groupBox1.Controls.Add(this.TradePrice);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.pname);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.retail);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.UnitPrice);
            this.groupBox1.Controls.Add(this.ProductList);
            this.groupBox1.Location = new System.Drawing.Point(19, 141);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(811, 140);
            this.groupBox1.TabIndex = 33;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Products";
            // 
            // tproduct
            // 
            this.tproduct.Location = new System.Drawing.Point(120, 23);
            this.tproduct.Name = "tproduct";
            this.tproduct.Size = new System.Drawing.Size(323, 20);
            this.tproduct.TabIndex = 47;
            this.tproduct.TextChanged += new System.EventHandler(this.tproduct_TextChanged);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(16, 25);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(100, 16);
            this.label19.TabIndex = 48;
            this.label19.Text = "Search Product";
            // 
            // Quantiy
            // 
            this.Quantiy.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Quantiy.Location = new System.Drawing.Point(337, 103);
            this.Quantiy.MaxLength = 7;
            this.Quantiy.Name = "Quantiy";
            this.Quantiy.Size = new System.Drawing.Size(106, 26);
            this.Quantiy.TabIndex = 5;
            this.Quantiy.Text = "0";
            this.Quantiy.Click += new System.EventHandler(this.Quantiy_Click);
            this.Quantiy.TextChanged += new System.EventHandler(this.Quantiy_TextChanged_1);
            this.Quantiy.Leave += new System.EventHandler(this.Quantiy_Leave);
            // 
            // cPackingStock
            // 
            this.cPackingStock.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cPackingStock.FormattingEnabled = true;
            this.cPackingStock.ItemHeight = 18;
            this.cPackingStock.Location = new System.Drawing.Point(533, 48);
            this.cPackingStock.Name = "cPackingStock";
            this.cPackingStock.Size = new System.Drawing.Size(254, 76);
            this.cPackingStock.TabIndex = 3;
            this.cPackingStock.SelectedValueChanged += new System.EventHandler(this.cPackingStock_SelectedValueChanged_1);
            // 
            // stock
            // 
            this.stock.Enabled = false;
            this.stock.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stock.Location = new System.Drawing.Point(120, 106);
            this.stock.MaxLength = 100;
            this.stock.Name = "stock";
            this.stock.Size = new System.Drawing.Size(100, 26);
            this.stock.TabIndex = 46;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(15, 113);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(42, 16);
            this.label14.TabIndex = 45;
            this.label14.Text = "Stock";
            // 
            // PID
            // 
            this.PID.AutoSize = true;
            this.PID.Location = new System.Drawing.Point(751, 119);
            this.PID.Name = "PID";
            this.PID.Size = new System.Drawing.Size(15, 13);
            this.PID.TabIndex = 44;
            this.PID.Text = "id";
            this.PID.Visible = false;
            // 
            // TradePrice
            // 
            this.TradePrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TradePrice.Location = new System.Drawing.Point(337, 68);
            this.TradePrice.MaxLength = 7;
            this.TradePrice.Name = "TradePrice";
            this.TradePrice.Size = new System.Drawing.Size(106, 26);
            this.TradePrice.TabIndex = 4;
            this.TradePrice.Text = "0";
            this.TradePrice.Click += new System.EventHandler(this.TradePrice_Click);
            this.TradePrice.TextChanged += new System.EventHandler(this.TradePrice_TextChanged);
            this.TradePrice.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TradePrice_KeyPress);
            this.TradePrice.Leave += new System.EventHandler(this.TradePrice_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(250, 71);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 16);
            this.label4.TabIndex = 33;
            this.label4.Text = "T. Price";
            // 
            // ProductList
            // 
            this.ProductList.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.ProductList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ProductList.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProductList.FormattingEnabled = true;
            this.ProductList.ItemHeight = 16;
            this.ProductList.Location = new System.Drawing.Point(120, 47);
            this.ProductList.Name = "ProductList";
            this.ProductList.Size = new System.Drawing.Size(323, 82);
            this.ProductList.TabIndex = 51;
            this.ProductList.Visible = false;
            this.ProductList.SelectedValueChanged += new System.EventHandler(this.ProductList_SelectedValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(838, 189);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 16);
            this.label5.TabIndex = 34;
            this.label5.Text = "INVOICE #";
            // 
            // Invoice
            // 
            this.Invoice.AutoSize = true;
            this.Invoice.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Invoice.Location = new System.Drawing.Point(917, 189);
            this.Invoice.Name = "Invoice";
            this.Invoice.Size = new System.Drawing.Size(44, 16);
            this.Invoice.TabIndex = 35;
            this.Invoice.Text = "OAS-";
            // 
            // Customer
            // 
            this.Customer.Controls.Add(this.tname);
            this.Customer.Controls.Add(this.label18);
            this.Customer.Controls.Add(this.label16);
            this.Customer.Controls.Add(this.label17);
            this.Customer.Controls.Add(this.Status);
            this.Customer.Controls.Add(this.cnic);
            this.Customer.Controls.Add(this.CAddress);
            this.Customer.Controls.Add(this.printcnic);
            this.Customer.Controls.Add(this.CID);
            this.Customer.Controls.Add(this.unpaid);
            this.Customer.Controls.Add(this.label11);
            this.Customer.Controls.Add(this.label9);
            this.Customer.Controls.Add(this.cname);
            this.Customer.Controls.Add(this.label8);
            this.Customer.Controls.Add(this.CustomerNameslist);
            this.Customer.Location = new System.Drawing.Point(24, 12);
            this.Customer.Name = "Customer";
            this.Customer.Size = new System.Drawing.Size(1057, 123);
            this.Customer.TabIndex = 36;
            this.Customer.TabStop = false;
            this.Customer.Text = "Customer";
            // 
            // tname
            // 
            this.tname.Location = new System.Drawing.Point(110, 16);
            this.tname.Name = "tname";
            this.tname.Size = new System.Drawing.Size(365, 20);
            this.tname.TabIndex = 1;
            this.tname.TextChanged += new System.EventHandler(this.tname_TextChanged);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(477, 20);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(48, 16);
            this.label18.TabIndex = 51;
            this.label18.Text = "Name:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(8, 89);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(45, 16);
            this.label16.TabIndex = 49;
            this.label16.Text = "Status";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(8, 63);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(59, 16);
            this.label17.TabIndex = 48;
            this.label17.Text = "Address";
            // 
            // Status
            // 
            this.Status.AutoSize = true;
            this.Status.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Status.Location = new System.Drawing.Point(108, 89);
            this.Status.Name = "Status";
            this.Status.Size = new System.Drawing.Size(45, 16);
            this.Status.TabIndex = 47;
            this.Status.Text = "Status";
            // 
            // cnic
            // 
            this.cnic.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cnic.FormattingEnabled = true;
            this.cnic.ItemHeight = 16;
            this.cnic.Location = new System.Drawing.Point(528, 47);
            this.cnic.Name = "cnic";
            this.cnic.Size = new System.Drawing.Size(254, 68);
            this.cnic.TabIndex = 46;
            this.cnic.SelectedValueChanged += new System.EventHandler(this.cnic_SelectedValueChanged);
            // 
            // CAddress
            // 
            this.CAddress.AutoSize = true;
            this.CAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CAddress.Location = new System.Drawing.Point(108, 63);
            this.CAddress.Name = "CAddress";
            this.CAddress.Size = new System.Drawing.Size(59, 16);
            this.CAddress.TabIndex = 45;
            this.CAddress.Text = "Address";
            // 
            // printcnic
            // 
            this.printcnic.AutoSize = true;
            this.printcnic.Location = new System.Drawing.Point(828, 52);
            this.printcnic.Name = "printcnic";
            this.printcnic.Size = new System.Drawing.Size(130, 17);
            this.printcnic.TabIndex = 44;
            this.printcnic.Text = "Print CNIC on Receipt";
            this.printcnic.UseVisualStyleBackColor = true;
            this.printcnic.CheckedChanged += new System.EventHandler(this.printcnic_CheckedChanged);
            // 
            // CID
            // 
            this.CID.AutoSize = true;
            this.CID.Location = new System.Drawing.Point(1023, 92);
            this.CID.Name = "CID";
            this.CID.Size = new System.Drawing.Size(15, 13);
            this.CID.TabIndex = 43;
            this.CID.Text = "id";
            this.CID.Visible = false;
            // 
            // unpaid
            // 
            this.unpaid.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.unpaid.Location = new System.Drawing.Point(931, 19);
            this.unpaid.MaxLength = 20;
            this.unpaid.Name = "unpaid";
            this.unpaid.Size = new System.Drawing.Size(107, 26);
            this.unpaid.TabIndex = 42;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(825, 20);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(100, 16);
            this.label11.TabIndex = 41;
            this.label11.Text = "Unpaid Amount";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(481, 51);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(40, 16);
            this.label9.TabIndex = 39;
            this.label9.Text = "NIC #";
            // 
            // cname
            // 
            this.cname.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cname.FormattingEnabled = true;
            this.cname.Location = new System.Drawing.Point(528, 15);
            this.cname.Name = "cname";
            this.cname.Size = new System.Drawing.Size(254, 26);
            this.cname.TabIndex = 0;
            this.cname.SelectedValueChanged += new System.EventHandler(this.cname_SelectedValueChanged);
            this.cname.TextChanged += new System.EventHandler(this.cname_TextChanged);
            this.cname.Click += new System.EventHandler(this.cname_Click);
            this.cname.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cname_KeyPress);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(13, 20);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(91, 16);
            this.label8.TabIndex = 38;
            this.label8.Text = "Search Name";
            // 
            // CustomerNameslist
            // 
            this.CustomerNameslist.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.CustomerNameslist.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CustomerNameslist.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CustomerNameslist.FormattingEnabled = true;
            this.CustomerNameslist.ItemHeight = 16;
            this.CustomerNameslist.Location = new System.Drawing.Point(110, 38);
            this.CustomerNameslist.Name = "CustomerNameslist";
            this.CustomerNameslist.Size = new System.Drawing.Size(365, 82);
            this.CustomerNameslist.TabIndex = 50;
            this.CustomerNameslist.Visible = false;
            this.CustomerNameslist.Click += new System.EventHandler(this.CustomerNameslist_Click);
            this.CustomerNameslist.SelectedValueChanged += new System.EventHandler(this.CustomerNameslist_SelectedValueChanged);
            // 
            // GrandTotal
            // 
            this.GrandTotal.AutoSize = true;
            this.GrandTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GrandTotal.Location = new System.Drawing.Point(871, 518);
            this.GrandTotal.Name = "GrandTotal";
            this.GrandTotal.Size = new System.Drawing.Size(28, 16);
            this.GrandTotal.TabIndex = 38;
            this.GrandTotal.Text = "0.0";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(823, 518);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(45, 16);
            this.label13.TabIndex = 37;
            this.label13.Text = "Total :";
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(24, 509);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(276, 35);
            this.button2.TabIndex = 39;
            this.button2.Text = "Remove Selected Items From List";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Location = new System.Drawing.Point(319, 509);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(83, 35);
            this.button3.TabIndex = 40;
            this.button3.Text = "Refresh";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("Monotype Corsiva", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.Location = new System.Drawing.Point(998, 509);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(83, 62);
            this.button4.TabIndex = 41;
            this.button4.Text = "SOLD";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(826, 550);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(42, 16);
            this.label12.TabIndex = 42;
            this.label12.Text = "Paid :";
            // 
            // Paid
            // 
            this.Paid.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Paid.Location = new System.Drawing.Point(874, 545);
            this.Paid.MaxLength = 20;
            this.Paid.Name = "Paid";
            this.Paid.Size = new System.Drawing.Size(106, 26);
            this.Paid.TabIndex = 6;
            this.Paid.Text = "0";
            this.Paid.Click += new System.EventHandler(this.Paid_Click);
            // 
            // GrandTax
            // 
            this.GrandTax.AutoSize = true;
            this.GrandTax.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GrandTax.Location = new System.Drawing.Point(677, 518);
            this.GrandTax.Name = "GrandTax";
            this.GrandTax.Size = new System.Drawing.Size(28, 16);
            this.GrandTax.TabIndex = 44;
            this.GrandTax.Text = "0.0";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(602, 518);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(71, 16);
            this.label15.TabIndex = 43;
            this.label15.Text = "Total Tax :";
            // 
            // Qty
            // 
            this.Qty.AutoSize = true;
            this.Qty.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Qty.Location = new System.Drawing.Point(547, 519);
            this.Qty.Name = "Qty";
            this.Qty.Size = new System.Drawing.Size(16, 16);
            this.Qty.TabIndex = 45;
            this.Qty.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(483, 518);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 16);
            this.label2.TabIndex = 46;
            this.label2.Text = "Total Qty:";
            // 
            // prntformat
            // 
            this.prntformat.AutoSize = true;
            this.prntformat.Checked = true;
            this.prntformat.CheckState = System.Windows.Forms.CheckState.Checked;
            this.prntformat.Location = new System.Drawing.Point(24, 554);
            this.prntformat.Name = "prntformat";
            this.prntformat.Size = new System.Drawing.Size(141, 17);
            this.prntformat.TabIndex = 47;
            this.prntformat.Text = "Print Full Format Receipt";
            this.prntformat.UseVisualStyleBackColor = true;
            // 
            // WithoutColor
            // 
            this.WithoutColor.AutoSize = true;
            this.WithoutColor.Checked = true;
            this.WithoutColor.CheckState = System.Windows.Forms.CheckState.Checked;
            this.WithoutColor.Location = new System.Drawing.Point(171, 554);
            this.WithoutColor.Name = "WithoutColor";
            this.WithoutColor.Size = new System.Drawing.Size(125, 17);
            this.WithoutColor.TabIndex = 48;
            this.WithoutColor.Text = "Format Without Color";
            this.WithoutColor.UseVisualStyleBackColor = true;
            // 
            // A4
            // 
            this.A4.AutoSize = true;
            this.A4.Location = new System.Drawing.Point(303, 554);
            this.A4.Name = "A4";
            this.A4.Size = new System.Drawing.Size(38, 17);
            this.A4.TabIndex = 49;
            this.A4.Text = "A4";
            this.A4.UseVisualStyleBackColor = true;
            // 
            // A5
            // 
            this.A5.AutoSize = true;
            this.A5.Checked = true;
            this.A5.Location = new System.Drawing.Point(347, 554);
            this.A5.Name = "A5";
            this.A5.Size = new System.Drawing.Size(38, 17);
            this.A5.TabIndex = 50;
            this.A5.TabStop = true;
            this.A5.Text = "A5";
            this.A5.UseVisualStyleBackColor = true;
            // 
            // LandScape
            // 
            this.LandScape.AutoSize = true;
            this.LandScape.Checked = true;
            this.LandScape.CheckState = System.Windows.Forms.CheckState.Checked;
            this.LandScape.Location = new System.Drawing.Point(391, 555);
            this.LandScape.Name = "LandScape";
            this.LandScape.Size = new System.Drawing.Size(81, 17);
            this.LandScape.TabIndex = 51;
            this.LandScape.Text = "LandScape";
            this.LandScape.UseVisualStyleBackColor = true;
            // 
            // DontLoad
            // 
            this.DontLoad.AutoSize = true;
            this.DontLoad.Location = new System.Drawing.Point(478, 555);
            this.DontLoad.Name = "DontLoad";
            this.DontLoad.Size = new System.Drawing.Size(179, 17);
            this.DontLoad.TabIndex = 52;
            this.DontLoad.Text = "Do Not Show Receipts for Prints";
            this.DontLoad.UseVisualStyleBackColor = true;
            // 
            // logo
            // 
            this.logo.FormattingEnabled = true;
            this.logo.Items.AddRange(new object[] {
            "logo1",
            "logo2",
            "logo3",
            "logo4",
            "logo5"});
            this.logo.Location = new System.Drawing.Point(714, 552);
            this.logo.Name = "logo";
            this.logo.Size = new System.Drawing.Size(99, 21);
            this.logo.TabIndex = 53;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(673, 555);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(39, 15);
            this.label20.TabIndex = 54;
            this.label20.Text = "Logo";
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1112, 583);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.logo);
            this.Controls.Add(this.DontLoad);
            this.Controls.Add(this.LandScape);
            this.Controls.Add(this.A5);
            this.Controls.Add(this.A4);
            this.Controls.Add(this.WithoutColor);
            this.Controls.Add(this.prntformat);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Qty);
            this.Controls.Add(this.GrandTax);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.Paid);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.GrandTotal);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.Invoice);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.SaleDate);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Customer);
            this.Name = "Form3";
            this.Text = "SALE";
            this.Load += new System.EventHandler(this.Form3_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.Customer.ResumeLayout(false);
            this.Customer.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader Product;
        private System.Windows.Forms.ColumnHeader Packing;
        private System.Windows.Forms.ColumnHeader Quantity;
        private System.Windows.Forms.ColumnHeader Price;
        private System.Windows.Forms.ColumnHeader itemid;
        private System.Windows.Forms.ColumnHeader RetailPrice;
        private System.Windows.Forms.ColumnHeader SalesTaxInc;
        private System.Windows.Forms.ColumnHeader ST;
        private System.Windows.Forms.ColumnHeader CustomerId;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.TextBox retail;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.TextBox UnitPrice;
        private System.Windows.Forms.ComboBox pname;
        private System.Windows.Forms.DateTimePicker SaleDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox TradePrice;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label Invoice;
        private System.Windows.Forms.GroupBox Customer;
        private System.Windows.Forms.ComboBox cname;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label CID;
        public System.Windows.Forms.TextBox unpaid;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label PID;
        private System.Windows.Forms.Label GrandTotal;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox Paid;
        private System.Windows.Forms.Label GrandTax;
        private System.Windows.Forms.Label label15;
        public System.Windows.Forms.TextBox stock;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.CheckBox printcnic;
        private System.Windows.Forms.Label CAddress;
        private System.Windows.Forms.Label Status;
        private System.Windows.Forms.ListBox cnic;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.ColumnHeader STQ;
        private System.Windows.Forms.Label Qty;
        private System.Windows.Forms.ListBox cPackingStock;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox Quantiy;
        private System.Windows.Forms.ListBox CustomerNameslist;
        private System.Windows.Forms.TextBox tname;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox tproduct;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.ListBox ProductList;
        private System.Windows.Forms.CheckBox prntformat;
        private System.Windows.Forms.CheckBox WithoutColor;
        private System.Windows.Forms.RadioButton A4;
        private System.Windows.Forms.RadioButton A5;
        private System.Windows.Forms.CheckBox LandScape;
        private System.Windows.Forms.CheckBox DontLoad;
        private System.Windows.Forms.ComboBox logo;
        private System.Windows.Forms.Label label20;
    }
}