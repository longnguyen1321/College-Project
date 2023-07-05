namespace frmMain.HRM
{
    partial class frmStaffManage
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnTimKiem = new System.Windows.Forms.Button();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.btnSkip = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.txtMaNV = new System.Windows.Forms.TextBox();
            this.dtNgaySinh = new System.Windows.Forms.DateTimePicker();
            this.txtDiaChi = new System.Windows.Forms.TextBox();
            this.ckbGioi = new System.Windows.Forms.CheckBox();
            this.txtTenNV = new System.Windows.Forms.TextBox();
            this.mstSDT = new System.Windows.Forms.MaskedTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.MaNV = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GioiTinh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TenNV = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LienLacNV = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DiaChiNV = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NgaySinhNV = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel3 = new System.Windows.Forms.Panel();
            this.mnuStrip = new System.Windows.Forms.MenuStrip();
            this.mnuSell = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSelectProducts = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPdManage = new System.Windows.Forms.ToolStripMenuItem();
            this.quảnLýKhoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuStorage = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCreImport = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuImport = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuExport = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTransactionsManage = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSuppliers = new System.Windows.Forms.ToolStripMenuItem();
            this.phiếuKiểmKhoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuStockIventory = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuStockIventoryManage = new System.Windows.Forms.ToolStripMenuItem();
            this.thốngKêToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuProfit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuProfitDay = new System.Windows.Forms.ToolStripMenuItem();
            this.tuầnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.thángToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuProfitChart = new System.Windows.Forms.ToolStripMenuItem();
            this.nhânViênToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuInfoStaff = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAccountManage = new System.Windows.Forms.ToolStripMenuItem();
            this.trợGiúpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel3.SuspendLayout();
            this.mnuStrip.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.panel2.Controls.Add(this.btnTimKiem);
            this.panel2.Controls.Add(this.txtTimKiem);
            this.panel2.Controls.Add(this.btnSkip);
            this.panel2.Controls.Add(this.btnClose);
            this.panel2.Controls.Add(this.btnSave);
            this.panel2.Controls.Add(this.btnUpdate);
            this.panel2.Controls.Add(this.btnDelete);
            this.panel2.Controls.Add(this.btnAdd);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel2.Location = new System.Drawing.Point(0, 454);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1184, 117);
            this.panel2.TabIndex = 5;
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.Location = new System.Drawing.Point(335, 73);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(136, 37);
            this.btnTimKiem.TabIndex = 14;
            this.btnTimKiem.Text = "Tìm";
            this.btnTimKiem.UseVisualStyleBackColor = true;
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click);
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.Location = new System.Drawing.Point(484, 83);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Size = new System.Drawing.Size(365, 23);
            this.txtTimKiem.TabIndex = 13;
            // 
            // btnSkip
            // 
            this.btnSkip.Location = new System.Drawing.Point(982, 18);
            this.btnSkip.Name = "btnSkip";
            this.btnSkip.Size = new System.Drawing.Size(75, 37);
            this.btnSkip.TabIndex = 5;
            this.btnSkip.Text = "&Bỏ qua";
            this.btnSkip.UseVisualStyleBackColor = true;
            this.btnSkip.Click += new System.EventHandler(this.btnSkip_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(811, 18);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 37);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "&Đóng";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(640, 18);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 37);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "&Lưu";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(469, 18);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 37);
            this.btnUpdate.TabIndex = 2;
            this.btnUpdate.Text = "&Sửa";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(298, 18);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 37);
            this.btnDelete.TabIndex = 1;
            this.btnDelete.Text = "&Xóa";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(127, 18);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 37);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "&Thêm";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitContainer1.Location = new System.Drawing.Point(0, 96);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.txtMaNV);
            this.splitContainer1.Panel1.Controls.Add(this.dtNgaySinh);
            this.splitContainer1.Panel1.Controls.Add(this.txtDiaChi);
            this.splitContainer1.Panel1.Controls.Add(this.ckbGioi);
            this.splitContainer1.Panel1.Controls.Add(this.txtTenNV);
            this.splitContainer1.Panel1.Controls.Add(this.mstSDT);
            this.splitContainer1.Panel1.Controls.Add(this.label7);
            this.splitContainer1.Panel1.Controls.Add(this.label6);
            this.splitContainer1.Panel1.Controls.Add(this.label5);
            this.splitContainer1.Panel1.Controls.Add(this.label4);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dataGridView1);
            this.splitContainer1.Size = new System.Drawing.Size(1184, 358);
            this.splitContainer1.SplitterDistance = 404;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 6;
            // 
            // txtMaNV
            // 
            this.txtMaNV.Location = new System.Drawing.Point(190, 55);
            this.txtMaNV.Name = "txtMaNV";
            this.txtMaNV.Size = new System.Drawing.Size(156, 23);
            this.txtMaNV.TabIndex = 12;
            // 
            // dtNgaySinh
            // 
            this.dtNgaySinh.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtNgaySinh.Location = new System.Drawing.Point(190, 281);
            this.dtNgaySinh.Name = "dtNgaySinh";
            this.dtNgaySinh.Size = new System.Drawing.Size(156, 23);
            this.dtNgaySinh.TabIndex = 11;
            // 
            // txtDiaChi
            // 
            this.txtDiaChi.Location = new System.Drawing.Point(190, 185);
            this.txtDiaChi.Name = "txtDiaChi";
            this.txtDiaChi.Size = new System.Drawing.Size(156, 23);
            this.txtDiaChi.TabIndex = 10;
            // 
            // ckbGioi
            // 
            this.ckbGioi.AutoSize = true;
            this.ckbGioi.Location = new System.Drawing.Point(190, 140);
            this.ckbGioi.Name = "ckbGioi";
            this.ckbGioi.Size = new System.Drawing.Size(59, 21);
            this.ckbGioi.TabIndex = 9;
            this.ckbGioi.Text = "Nam";
            this.ckbGioi.UseVisualStyleBackColor = true;
            // 
            // txtTenNV
            // 
            this.txtTenNV.Location = new System.Drawing.Point(190, 97);
            this.txtTenNV.Name = "txtTenNV";
            this.txtTenNV.Size = new System.Drawing.Size(156, 23);
            this.txtTenNV.TabIndex = 8;
            // 
            // mstSDT
            // 
            this.mstSDT.Location = new System.Drawing.Point(190, 233);
            this.mstSDT.Name = "mstSDT";
            this.mstSDT.Size = new System.Drawing.Size(156, 23);
            this.mstSDT.TabIndex = 6;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(59, 281);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(90, 17);
            this.label7.TabIndex = 5;
            this.label7.Text = "Ngày sinh :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(59, 233);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(114, 17);
            this.label6.TabIndex = 4;
            this.label6.Text = "Số điện thoại :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(59, 191);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 17);
            this.label5.TabIndex = 3;
            this.label5.Text = "Địa chỉ :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(59, 141);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 17);
            this.label4.TabIndex = 2;
            this.label4.Text = "Giới Tính :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(59, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(122, 17);
            this.label3.TabIndex = 1;
            this.label3.Text = "Tên nhân viên :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(59, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "Mã nhân viên :";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MaNV,
            this.GioiTinh,
            this.TenNV,
            this.LienLacNV,
            this.DiaChiNV,
            this.NgaySinhNV});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(775, 358);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // MaNV
            // 
            this.MaNV.DataPropertyName = "MaNV";
            this.MaNV.HeaderText = "Mã Nhân Viên";
            this.MaNV.Name = "MaNV";
            // 
            // GioiTinh
            // 
            this.GioiTinh.DataPropertyName = "GioiTinh";
            this.GioiTinh.HeaderText = "Gioi Tinh";
            this.GioiTinh.Name = "GioiTinh";
            // 
            // TenNV
            // 
            this.TenNV.DataPropertyName = "TenNV";
            this.TenNV.HeaderText = "Tên Nhân Viên";
            this.TenNV.Name = "TenNV";
            this.TenNV.Width = 150;
            // 
            // LienLacNV
            // 
            this.LienLacNV.DataPropertyName = "LienLacNV";
            this.LienLacNV.HeaderText = "Liên lạc";
            this.LienLacNV.Name = "LienLacNV";
            // 
            // DiaChiNV
            // 
            this.DiaChiNV.DataPropertyName = "DiaChiNV";
            this.DiaChiNV.HeaderText = "Địa chỉ ";
            this.DiaChiNV.Name = "DiaChiNV";
            // 
            // NgaySinhNV
            // 
            this.NgaySinhNV.DataPropertyName = "NgaySinhNV";
            this.NgaySinhNV.HeaderText = "Ngày Sinh";
            this.NgaySinhNV.Name = "NgaySinhNV";
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.mnuStrip);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1184, 26);
            this.panel3.TabIndex = 7;
            // 
            // mnuStrip
            // 
            this.mnuStrip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mnuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSell,
            this.quảnLýKhoToolStripMenuItem,
            this.thốngKêToolStripMenuItem,
            this.nhânViênToolStripMenuItem,
            this.trợGiúpToolStripMenuItem});
            this.mnuStrip.Location = new System.Drawing.Point(0, 0);
            this.mnuStrip.Name = "mnuStrip";
            this.mnuStrip.Size = new System.Drawing.Size(1182, 24);
            this.mnuStrip.TabIndex = 1;
            this.mnuStrip.Text = "menuStrip1";
            // 
            // mnuSell
            // 
            this.mnuSell.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSelectProducts,
            this.mnuPdManage});
            this.mnuSell.Name = "mnuSell";
            this.mnuSell.Size = new System.Drawing.Size(70, 20);
            this.mnuSell.Text = "Mặt hàng";
            // 
            // mnuSelectProducts
            // 
            this.mnuSelectProducts.Name = "mnuSelectProducts";
            this.mnuSelectProducts.Size = new System.Drawing.Size(180, 22);
            this.mnuSelectProducts.Text = "Bán hàng";
            this.mnuSelectProducts.Click += new System.EventHandler(this.mnuSelectProducts_Click);
            // 
            // mnuPdManage
            // 
            this.mnuPdManage.Name = "mnuPdManage";
            this.mnuPdManage.Size = new System.Drawing.Size(180, 22);
            this.mnuPdManage.Text = "Quản lý";
            this.mnuPdManage.Click += new System.EventHandler(this.mnuPdManage_Click);
            // 
            // quảnLýKhoToolStripMenuItem
            // 
            this.quảnLýKhoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuStorage,
            this.mnuCreImport,
            this.mnuSuppliers,
            this.phiếuKiểmKhoToolStripMenuItem});
            this.quảnLýKhoToolStripMenuItem.Name = "quảnLýKhoToolStripMenuItem";
            this.quảnLýKhoToolStripMenuItem.Size = new System.Drawing.Size(70, 20);
            this.quảnLýKhoToolStripMenuItem.Text = "Kho hàng";
            // 
            // mnuStorage
            // 
            this.mnuStorage.Name = "mnuStorage";
            this.mnuStorage.Size = new System.Drawing.Size(180, 22);
            this.mnuStorage.Text = "Tồn kho";
            this.mnuStorage.Click += new System.EventHandler(this.mnuStorage_Click);
            // 
            // mnuCreImport
            // 
            this.mnuCreImport.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuImport,
            this.mnuExport,
            this.mnuTransactionsManage});
            this.mnuCreImport.Name = "mnuCreImport";
            this.mnuCreImport.Size = new System.Drawing.Size(180, 22);
            this.mnuCreImport.Text = "Nhập xuất";
            // 
            // mnuImport
            // 
            this.mnuImport.Name = "mnuImport";
            this.mnuImport.Size = new System.Drawing.Size(180, 22);
            this.mnuImport.Text = "Nhập kho";
            this.mnuImport.Click += new System.EventHandler(this.mnuImport_Click);
            // 
            // mnuExport
            // 
            this.mnuExport.Name = "mnuExport";
            this.mnuExport.Size = new System.Drawing.Size(180, 22);
            this.mnuExport.Text = "Xuất kho";
            this.mnuExport.Click += new System.EventHandler(this.mnuExport_Click);
            // 
            // mnuTransactionsManage
            // 
            this.mnuTransactionsManage.Name = "mnuTransactionsManage";
            this.mnuTransactionsManage.Size = new System.Drawing.Size(180, 22);
            this.mnuTransactionsManage.Text = "Quản lý nhập xuất";
            this.mnuTransactionsManage.Click += new System.EventHandler(this.mnuTransactionsManage_Click);
            // 
            // mnuSuppliers
            // 
            this.mnuSuppliers.Name = "mnuSuppliers";
            this.mnuSuppliers.Size = new System.Drawing.Size(180, 22);
            this.mnuSuppliers.Text = "Nhà cung cấp";
            this.mnuSuppliers.Click += new System.EventHandler(this.mnuSuppliers_Click);
            // 
            // phiếuKiểmKhoToolStripMenuItem
            // 
            this.phiếuKiểmKhoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuStockIventory,
            this.mnuStockIventoryManage});
            this.phiếuKiểmKhoToolStripMenuItem.Name = "phiếuKiểmKhoToolStripMenuItem";
            this.phiếuKiểmKhoToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.phiếuKiểmKhoToolStripMenuItem.Text = "Phiếu kiểm kho";
            // 
            // mnuStockIventory
            // 
            this.mnuStockIventory.Name = "mnuStockIventory";
            this.mnuStockIventory.Size = new System.Drawing.Size(208, 22);
            this.mnuStockIventory.Text = "Tạo phiếu kiểm kho";
            this.mnuStockIventory.Click += new System.EventHandler(this.mnuStockIventory_Click);
            // 
            // mnuStockIventoryManage
            // 
            this.mnuStockIventoryManage.Name = "mnuStockIventoryManage";
            this.mnuStockIventoryManage.Size = new System.Drawing.Size(208, 22);
            this.mnuStockIventoryManage.Text = "Thông kê phiếu kiểm kho";
            this.mnuStockIventoryManage.Click += new System.EventHandler(this.mnuStockIventoryManage_Click);
            // 
            // thốngKêToolStripMenuItem
            // 
            this.thốngKêToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuProfit,
            this.mnuProfitChart});
            this.thốngKêToolStripMenuItem.Name = "thốngKêToolStripMenuItem";
            this.thốngKêToolStripMenuItem.Size = new System.Drawing.Size(75, 20);
            this.thốngKêToolStripMenuItem.Text = "Doanh thu";
            // 
            // mnuProfit
            // 
            this.mnuProfit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuProfitDay,
            this.tuầnToolStripMenuItem,
            this.thángToolStripMenuItem});
            this.mnuProfit.Name = "mnuProfit";
            this.mnuProfit.Size = new System.Drawing.Size(136, 22);
            this.mnuProfit.Text = "Thống kê";
            // 
            // mnuProfitDay
            // 
            this.mnuProfitDay.Name = "mnuProfitDay";
            this.mnuProfitDay.Size = new System.Drawing.Size(107, 22);
            this.mnuProfitDay.Text = "Ngày";
            // 
            // tuầnToolStripMenuItem
            // 
            this.tuầnToolStripMenuItem.Name = "tuầnToolStripMenuItem";
            this.tuầnToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.tuầnToolStripMenuItem.Text = "Tuần ";
            // 
            // thángToolStripMenuItem
            // 
            this.thángToolStripMenuItem.Name = "thángToolStripMenuItem";
            this.thángToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.thángToolStripMenuItem.Text = "Tháng";
            // 
            // mnuProfitChart
            // 
            this.mnuProfitChart.Name = "mnuProfitChart";
            this.mnuProfitChart.Size = new System.Drawing.Size(136, 22);
            this.mnuProfitChart.Text = "Tạo biểu đồ";
            // 
            // nhânViênToolStripMenuItem
            // 
            this.nhânViênToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuInfoStaff,
            this.mnuAccountManage});
            this.nhânViênToolStripMenuItem.Name = "nhânViênToolStripMenuItem";
            this.nhânViênToolStripMenuItem.Size = new System.Drawing.Size(73, 20);
            this.nhânViênToolStripMenuItem.Text = "Nhân viên";
            // 
            // mnuInfoStaff
            // 
            this.mnuInfoStaff.Name = "mnuInfoStaff";
            this.mnuInfoStaff.Size = new System.Drawing.Size(180, 22);
            this.mnuInfoStaff.Text = "Quản lý nhân viên";
            // 
            // mnuAccountManage
            // 
            this.mnuAccountManage.Name = "mnuAccountManage";
            this.mnuAccountManage.Size = new System.Drawing.Size(180, 22);
            this.mnuAccountManage.Text = "Quản lý tài khoản";
            this.mnuAccountManage.Click += new System.EventHandler(this.mnuAccountManage_Click);
            // 
            // trợGiúpToolStripMenuItem
            // 
            this.trợGiúpToolStripMenuItem.Name = "trợGiúpToolStripMenuItem";
            this.trợGiúpToolStripMenuItem.Size = new System.Drawing.Size(62, 20);
            this.trợGiúpToolStripMenuItem.Text = "Trợ giúp";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 26);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1184, 70);
            this.panel1.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1184, 70);
            this.label1.TabIndex = 1;
            this.label1.Text = "DANH MỤC NHÂN VIEN ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmStaffManage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 571);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmStaffManage";
            this.Text = "Quản lý nhân viên";
            this.Load += new System.EventHandler(this.frmStaffManage_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.mnuStrip.ResumeLayout(false);
            this.mnuStrip.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnTimKiem;
        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.Button btnSkip;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox txtMaNV;
        private System.Windows.Forms.DateTimePicker dtNgaySinh;
        private System.Windows.Forms.TextBox txtDiaChi;
        private System.Windows.Forms.CheckBox ckbGioi;
        private System.Windows.Forms.TextBox txtTenNV;
        private System.Windows.Forms.MaskedTextBox mstSDT;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaNV;
        private System.Windows.Forms.DataGridViewTextBoxColumn GioiTinh;
        private System.Windows.Forms.DataGridViewTextBoxColumn TenNV;
        private System.Windows.Forms.DataGridViewTextBoxColumn LienLacNV;
        private System.Windows.Forms.DataGridViewTextBoxColumn DiaChiNV;
        private System.Windows.Forms.DataGridViewTextBoxColumn NgaySinhNV;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.MenuStrip mnuStrip;
        private System.Windows.Forms.ToolStripMenuItem mnuSell;
        private System.Windows.Forms.ToolStripMenuItem mnuSelectProducts;
        private System.Windows.Forms.ToolStripMenuItem mnuPdManage;
        private System.Windows.Forms.ToolStripMenuItem quảnLýKhoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuStorage;
        private System.Windows.Forms.ToolStripMenuItem mnuCreImport;
        private System.Windows.Forms.ToolStripMenuItem mnuImport;
        private System.Windows.Forms.ToolStripMenuItem mnuExport;
        private System.Windows.Forms.ToolStripMenuItem mnuTransactionsManage;
        private System.Windows.Forms.ToolStripMenuItem mnuSuppliers;
        private System.Windows.Forms.ToolStripMenuItem phiếuKiểmKhoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuStockIventory;
        private System.Windows.Forms.ToolStripMenuItem mnuStockIventoryManage;
        private System.Windows.Forms.ToolStripMenuItem thốngKêToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuProfit;
        private System.Windows.Forms.ToolStripMenuItem mnuProfitDay;
        private System.Windows.Forms.ToolStripMenuItem tuầnToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem thángToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuProfitChart;
        private System.Windows.Forms.ToolStripMenuItem nhânViênToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuInfoStaff;
        private System.Windows.Forms.ToolStripMenuItem mnuAccountManage;
        private System.Windows.Forms.ToolStripMenuItem trợGiúpToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
    }
}