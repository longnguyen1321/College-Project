namespace frmMain.HRM
{
    partial class frmAccountManage
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
            this.panel1 = new System.Windows.Forms.Panel();
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
            this.mnuStaffManage = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAccountManage = new System.Windows.Forms.ToolStripMenuItem();
            this.trợGiúpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.txtQuyen = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtMaNV = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtMatKhau = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTenTK = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dtTaiKhoan = new System.Windows.Forms.DataGridView();
            this.TenTaiKhoan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MatKhau = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MaNV = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Quyen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnSkip = new System.Windows.Forms.Button();
            this.btnThoat = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnThem = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.mnuStrip.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtTaiKhoan)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.mnuStrip);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1184, 24);
            this.panel1.TabIndex = 1;
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
            this.mnuStrip.Size = new System.Drawing.Size(1182, 22);
            this.mnuStrip.TabIndex = 0;
            this.mnuStrip.Text = "menuStrip1";
            // 
            // mnuSell
            // 
            this.mnuSell.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSelectProducts,
            this.mnuPdManage});
            this.mnuSell.Name = "mnuSell";
            this.mnuSell.Size = new System.Drawing.Size(70, 18);
            this.mnuSell.Text = "Mặt hàng";
            // 
            // mnuSelectProducts
            // 
            this.mnuSelectProducts.Name = "mnuSelectProducts";
            this.mnuSelectProducts.Size = new System.Drawing.Size(124, 22);
            this.mnuSelectProducts.Text = "Bán hàng";
            this.mnuSelectProducts.Click += new System.EventHandler(this.mnuSelectProducts_Click);
            // 
            // mnuPdManage
            // 
            this.mnuPdManage.Name = "mnuPdManage";
            this.mnuPdManage.Size = new System.Drawing.Size(124, 22);
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
            this.quảnLýKhoToolStripMenuItem.Size = new System.Drawing.Size(70, 18);
            this.quảnLýKhoToolStripMenuItem.Text = "Kho hàng";
            // 
            // mnuStorage
            // 
            this.mnuStorage.Name = "mnuStorage";
            this.mnuStorage.Size = new System.Drawing.Size(156, 22);
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
            this.mnuCreImport.Size = new System.Drawing.Size(156, 22);
            this.mnuCreImport.Text = "Nhập xuất";
            // 
            // mnuImport
            // 
            this.mnuImport.Name = "mnuImport";
            this.mnuImport.Size = new System.Drawing.Size(171, 22);
            this.mnuImport.Text = "Nhập kho";
            this.mnuImport.Click += new System.EventHandler(this.mnuImport_Click);
            // 
            // mnuExport
            // 
            this.mnuExport.Name = "mnuExport";
            this.mnuExport.Size = new System.Drawing.Size(171, 22);
            this.mnuExport.Text = "Xuất kho";
            this.mnuExport.Click += new System.EventHandler(this.mnuExport_Click);
            // 
            // mnuTransactionsManage
            // 
            this.mnuTransactionsManage.Name = "mnuTransactionsManage";
            this.mnuTransactionsManage.Size = new System.Drawing.Size(171, 22);
            this.mnuTransactionsManage.Text = "Quản lý nhập xuất";
            this.mnuTransactionsManage.Click += new System.EventHandler(this.mnuTransactionsManage_Click);
            // 
            // mnuSuppliers
            // 
            this.mnuSuppliers.Name = "mnuSuppliers";
            this.mnuSuppliers.Size = new System.Drawing.Size(156, 22);
            this.mnuSuppliers.Text = "Nhà cung cấp";
            this.mnuSuppliers.Click += new System.EventHandler(this.mnuSuppliers_Click);
            // 
            // phiếuKiểmKhoToolStripMenuItem
            // 
            this.phiếuKiểmKhoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuStockIventory,
            this.mnuStockIventoryManage});
            this.phiếuKiểmKhoToolStripMenuItem.Name = "phiếuKiểmKhoToolStripMenuItem";
            this.phiếuKiểmKhoToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
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
            this.thốngKêToolStripMenuItem.Size = new System.Drawing.Size(75, 18);
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
            this.mnuStaffManage,
            this.mnuAccountManage});
            this.nhânViênToolStripMenuItem.Name = "nhânViênToolStripMenuItem";
            this.nhânViênToolStripMenuItem.Size = new System.Drawing.Size(73, 18);
            this.nhânViênToolStripMenuItem.Text = "Nhân viên";
            // 
            // mnuStaffManage
            // 
            this.mnuStaffManage.Name = "mnuStaffManage";
            this.mnuStaffManage.Size = new System.Drawing.Size(170, 22);
            this.mnuStaffManage.Text = "Quản lý nhân viên";
            this.mnuStaffManage.Click += new System.EventHandler(this.mnuStaffManage_Click);
            // 
            // mnuAccountManage
            // 
            this.mnuAccountManage.Name = "mnuAccountManage";
            this.mnuAccountManage.Size = new System.Drawing.Size(170, 22);
            this.mnuAccountManage.Text = "Quản lý tài khoản";
            // 
            // trợGiúpToolStripMenuItem
            // 
            this.trợGiúpToolStripMenuItem.Name = "trợGiúpToolStripMenuItem";
            this.trợGiúpToolStripMenuItem.Size = new System.Drawing.Size(62, 18);
            this.trợGiúpToolStripMenuItem.Text = "Trợ giúp";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 24);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1184, 63);
            this.panel2.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1184, 63);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tai Khoan";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitContainer1.Location = new System.Drawing.Point(0, 87);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.txtQuyen);
            this.splitContainer1.Panel1.Controls.Add(this.label5);
            this.splitContainer1.Panel1.Controls.Add(this.txtMaNV);
            this.splitContainer1.Panel1.Controls.Add(this.label4);
            this.splitContainer1.Panel1.Controls.Add(this.txtMatKhau);
            this.splitContainer1.Panel1.Controls.Add(this.label3);
            this.splitContainer1.Panel1.Controls.Add(this.txtTenTK);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dtTaiKhoan);
            this.splitContainer1.Size = new System.Drawing.Size(1184, 398);
            this.splitContainer1.SplitterDistance = 393;
            this.splitContainer1.TabIndex = 0;
            // 
            // txtQuyen
            // 
            this.txtQuyen.Location = new System.Drawing.Point(57, 301);
            this.txtQuyen.Name = "txtQuyen";
            this.txtQuyen.Size = new System.Drawing.Size(281, 24);
            this.txtQuyen.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(54, 271);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 18);
            this.label5.TabIndex = 6;
            this.label5.Text = "Quyen:";
            // 
            // txtMaNV
            // 
            this.txtMaNV.Location = new System.Drawing.Point(57, 235);
            this.txtMaNV.Name = "txtMaNV";
            this.txtMaNV.Size = new System.Drawing.Size(281, 24);
            this.txtMaNV.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(54, 205);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(108, 18);
            this.label4.TabIndex = 4;
            this.label4.Text = "Ma Nhan Vien :";
            // 
            // txtMatKhau
            // 
            this.txtMatKhau.Location = new System.Drawing.Point(57, 169);
            this.txtMatKhau.Name = "txtMatKhau";
            this.txtMatKhau.Size = new System.Drawing.Size(281, 24);
            this.txtMatKhau.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(54, 139);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 18);
            this.label3.TabIndex = 2;
            this.label3.Text = "Mat Khau :";
            // 
            // txtTenTK
            // 
            this.txtTenTK.Location = new System.Drawing.Point(57, 103);
            this.txtTenTK.Name = "txtTenTK";
            this.txtTenTK.Size = new System.Drawing.Size(281, 24);
            this.txtTenTK.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(54, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 18);
            this.label2.TabIndex = 0;
            this.label2.Text = "Ten Tai Khoan :";
            // 
            // dtTaiKhoan
            // 
            this.dtTaiKhoan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtTaiKhoan.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TenTaiKhoan,
            this.MatKhau,
            this.MaNV,
            this.Quyen});
            this.dtTaiKhoan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtTaiKhoan.Location = new System.Drawing.Point(0, 0);
            this.dtTaiKhoan.Name = "dtTaiKhoan";
            this.dtTaiKhoan.Size = new System.Drawing.Size(787, 398);
            this.dtTaiKhoan.TabIndex = 0;
            this.dtTaiKhoan.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dtTaiKhoan_CellClick);
            // 
            // TenTaiKhoan
            // 
            this.TenTaiKhoan.DataPropertyName = "TenTaiKhoan";
            this.TenTaiKhoan.HeaderText = "Ten Tai Khoan";
            this.TenTaiKhoan.Name = "TenTaiKhoan";
            this.TenTaiKhoan.Width = 200;
            // 
            // MatKhau
            // 
            this.MatKhau.DataPropertyName = "MatKhau";
            this.MatKhau.HeaderText = "Mat Khau";
            this.MatKhau.Name = "MatKhau";
            this.MatKhau.Width = 250;
            // 
            // MaNV
            // 
            this.MaNV.DataPropertyName = "MaNV";
            this.MaNV.HeaderText = "Ma Nhan Vien";
            this.MaNV.Name = "MaNV";
            this.MaNV.Width = 150;
            // 
            // Quyen
            // 
            this.Quyen.DataPropertyName = "Quyen";
            this.Quyen.HeaderText = "Quyen truy cap";
            this.Quyen.Name = "Quyen";
            this.Quyen.Width = 150;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnSkip);
            this.panel3.Controls.Add(this.btnThoat);
            this.panel3.Controls.Add(this.btnSua);
            this.panel3.Controls.Add(this.btnXoa);
            this.panel3.Controls.Add(this.btnThem);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel3.Location = new System.Drawing.Point(0, 485);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1184, 86);
            this.panel3.TabIndex = 1;
            // 
            // btnSkip
            // 
            this.btnSkip.Location = new System.Drawing.Point(712, 22);
            this.btnSkip.Name = "btnSkip";
            this.btnSkip.Size = new System.Drawing.Size(80, 42);
            this.btnSkip.TabIndex = 3;
            this.btnSkip.Text = "&Reset";
            this.btnSkip.UseVisualStyleBackColor = true;
            this.btnSkip.Click += new System.EventHandler(this.btnSkip_Click);
            // 
            // btnThoat
            // 
            this.btnThoat.Location = new System.Drawing.Point(865, 22);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(80, 42);
            this.btnThoat.TabIndex = 4;
            this.btnThoat.Text = "Thoat";
            this.btnThoat.UseVisualStyleBackColor = true;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // btnSua
            // 
            this.btnSua.Location = new System.Drawing.Point(557, 22);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(82, 42);
            this.btnSua.TabIndex = 2;
            this.btnSua.Text = "Sua";
            this.btnSua.UseVisualStyleBackColor = true;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.Location = new System.Drawing.Point(404, 22);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(80, 42);
            this.btnXoa.TabIndex = 1;
            this.btnXoa.Text = "Xoa";
            this.btnXoa.UseVisualStyleBackColor = true;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnThem
            // 
            this.btnThem.Location = new System.Drawing.Point(240, 22);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(91, 42);
            this.btnThem.TabIndex = 0;
            this.btnThem.Text = "Them";
            this.btnThem.UseVisualStyleBackColor = true;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // frmAccountManage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 571);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmAccountManage";
            this.Text = "Quản lý tài khoản";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmAccountManage_FormClosed);
            this.Load += new System.EventHandler(this.frmAccountManage_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.mnuStrip.ResumeLayout(false);
            this.mnuStrip.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtTaiKhoan)).EndInit();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
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
        private System.Windows.Forms.ToolStripMenuItem mnuStaffManage;
        private System.Windows.Forms.ToolStripMenuItem mnuAccountManage;
        private System.Windows.Forms.ToolStripMenuItem trợGiúpToolStripMenuItem;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox txtQuyen;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtMaNV;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtMatKhau;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTenTK;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dtTaiKhoan;
        private System.Windows.Forms.DataGridViewTextBoxColumn TenTaiKhoan;
        private System.Windows.Forms.DataGridViewTextBoxColumn MatKhau;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaNV;
        private System.Windows.Forms.DataGridViewTextBoxColumn Quyen;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnSkip;
        private System.Windows.Forms.Button btnThoat;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnThem;
    }
}