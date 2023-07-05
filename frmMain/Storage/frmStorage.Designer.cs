namespace frmMain
{
    partial class frmStorage
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
            this.label3 = new System.Windows.Forms.Label();
            this.btnResetDGV = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtSearchInfo = new System.Windows.Forms.TextBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgvStorage = new System.Windows.Forms.DataGridView();
            this.MaMH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TenMH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TonKho = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TinhTrang = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TongSLN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TongSLX = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnOpenFrmIventory = new System.Windows.Forms.Button();
            this.txtTotalProducts = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.mnuStrip.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStorage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.mnuStrip);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1184, 28);
            this.panel1.TabIndex = 0;
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
            this.mnuStrip.Size = new System.Drawing.Size(1182, 26);
            this.mnuStrip.TabIndex = 0;
            this.mnuStrip.Text = "menuStrip1";
            // 
            // mnuSell
            // 
            this.mnuSell.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSelectProducts,
            this.mnuPdManage});
            this.mnuSell.Name = "mnuSell";
            this.mnuSell.Size = new System.Drawing.Size(70, 22);
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
            this.quảnLýKhoToolStripMenuItem.Size = new System.Drawing.Size(70, 22);
            this.quảnLýKhoToolStripMenuItem.Text = "Kho hàng";
            // 
            // mnuStorage
            // 
            this.mnuStorage.Name = "mnuStorage";
            this.mnuStorage.Size = new System.Drawing.Size(156, 22);
            this.mnuStorage.Text = "Tồn kho";
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
            this.thốngKêToolStripMenuItem.Size = new System.Drawing.Size(75, 22);
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
            this.nhânViênToolStripMenuItem.Size = new System.Drawing.Size(73, 22);
            this.nhânViênToolStripMenuItem.Text = "Nhân viên";
            // 
            // mnuStaffManage
            // 
            this.mnuStaffManage.Name = "mnuStaffManage";
            this.mnuStaffManage.Size = new System.Drawing.Size(180, 22);
            this.mnuStaffManage.Text = "Quản lý nhân viên";
            this.mnuStaffManage.Click += new System.EventHandler(this.mnuStaffManage_Click);
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
            this.trợGiúpToolStripMenuItem.Size = new System.Drawing.Size(62, 22);
            this.trợGiúpToolStripMenuItem.Text = "Trợ giúp";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.LightGray;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 28);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1184, 66);
            this.panel2.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1182, 64);
            this.label1.TabIndex = 0;
            this.label1.Text = "Quản lý tồn kho";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(329, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 18);
            this.label3.TabIndex = 0;
            this.label3.Text = "Từ khóa:";
            // 
            // btnResetDGV
            // 
            this.btnResetDGV.Image = global::frmMain.Properties.Resources.undo__1_;
            this.btnResetDGV.Location = new System.Drawing.Point(820, 6);
            this.btnResetDGV.Name = "btnResetDGV";
            this.btnResetDGV.Size = new System.Drawing.Size(58, 36);
            this.btnResetDGV.TabIndex = 3;
            this.btnResetDGV.UseVisualStyleBackColor = true;
            this.btnResetDGV.Click += new System.EventHandler(this.btnResetDGV_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Image = global::frmMain.Properties.Resources.find;
            this.btnSearch.Location = new System.Drawing.Point(751, 6);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(58, 36);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtSearchInfo
            // 
            this.txtSearchInfo.Location = new System.Drawing.Point(405, 13);
            this.txtSearchInfo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtSearchInfo.Name = "txtSearchInfo";
            this.txtSearchInfo.Size = new System.Drawing.Size(340, 24);
            this.txtSearchInfo.TabIndex = 1;
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.groupBox1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1180, 362);
            this.panel4.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgvStorage);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1178, 360);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Danh sách mặt hàng trong kho:";
            // 
            // dgvStorage
            // 
            this.dgvStorage.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStorage.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MaMH,
            this.TenMH,
            this.TonKho,
            this.TinhTrang,
            this.TongSLN,
            this.TongSLX});
            this.dgvStorage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvStorage.Location = new System.Drawing.Point(3, 20);
            this.dgvStorage.Name = "dgvStorage";
            this.dgvStorage.Size = new System.Drawing.Size(1172, 337);
            this.dgvStorage.TabIndex = 0;
            // 
            // MaMH
            // 
            this.MaMH.DataPropertyName = "MaMH";
            this.MaMH.HeaderText = "Mã";
            this.MaMH.Name = "MaMH";
            this.MaMH.Width = 120;
            // 
            // TenMH
            // 
            this.TenMH.DataPropertyName = "TenMH";
            this.TenMH.HeaderText = "Tên";
            this.TenMH.Name = "TenMH";
            this.TenMH.Width = 300;
            // 
            // TonKho
            // 
            this.TonKho.DataPropertyName = "TonKho";
            this.TonKho.HeaderText = "Tồn kho";
            this.TonKho.Name = "TonKho";
            this.TonKho.Width = 150;
            // 
            // TinhTrang
            // 
            this.TinhTrang.DataPropertyName = "TinhTrang";
            this.TinhTrang.HeaderText = "Tình trạng";
            this.TinhTrang.Name = "TinhTrang";
            this.TinhTrang.Width = 150;
            // 
            // TongSLN
            // 
            this.TongSLN.DataPropertyName = "TongSLN";
            this.TongSLN.HeaderText = "Tổng nhập tháng ";
            this.TongSLN.Name = "TongSLN";
            this.TongSLN.Width = 200;
            // 
            // TongSLX
            // 
            this.TongSLX.DataPropertyName = "TongSLX";
            this.TongSLX.HeaderText = "Tổng xuất tháng ";
            this.TongSLX.Name = "TongSLX";
            this.TongSLX.Width = 200;
            // 
            // btnOpenFrmIventory
            // 
            this.btnOpenFrmIventory.ForeColor = System.Drawing.Color.MediumSeaGreen;
            this.btnOpenFrmIventory.Image = global::frmMain.Properties.Resources.add;
            this.btnOpenFrmIventory.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOpenFrmIventory.Location = new System.Drawing.Point(47, 5);
            this.btnOpenFrmIventory.Name = "btnOpenFrmIventory";
            this.btnOpenFrmIventory.Size = new System.Drawing.Size(190, 36);
            this.btnOpenFrmIventory.TabIndex = 0;
            this.btnOpenFrmIventory.Text = "Tạo phiếu kiểm kho";
            this.btnOpenFrmIventory.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnOpenFrmIventory.UseVisualStyleBackColor = true;
            this.btnOpenFrmIventory.Click += new System.EventHandler(this.btnOpenFrmIventory_Click);
            // 
            // txtTotalProducts
            // 
            this.txtTotalProducts.BackColor = System.Drawing.Color.LightSkyBlue;
            this.txtTotalProducts.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTotalProducts.ForeColor = System.Drawing.SystemColors.Info;
            this.txtTotalProducts.Location = new System.Drawing.Point(229, 9);
            this.txtTotalProducts.Name = "txtTotalProducts";
            this.txtTotalProducts.ReadOnly = true;
            this.txtTotalProducts.Size = new System.Drawing.Size(100, 24);
            this.txtTotalProducts.TabIndex = 1;
            this.txtTotalProducts.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Italic | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(22, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(194, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = "Tổng sản phẩm trong kho:";
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 94);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer3);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1184, 477);
            this.splitContainer1.SplitterDistance = 418;
            this.splitContainer1.TabIndex = 5;
            // 
            // splitContainer3
            // 
            this.splitContainer3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.groupBox2);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.panel4);
            this.splitContainer3.Size = new System.Drawing.Size(1184, 418);
            this.splitContainer3.SplitterDistance = 48;
            this.splitContainer3.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.btnResetDGV);
            this.groupBox2.Controls.Add(this.txtSearchInfo);
            this.groupBox2.Controls.Add(this.btnSearch);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1180, 44);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Tìm kiếm:";
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.panel5);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.panel6);
            this.splitContainer2.Size = new System.Drawing.Size(1180, 51);
            this.splitContainer2.SplitterDistance = 624;
            this.splitContainer2.TabIndex = 0;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.label2);
            this.panel5.Controls.Add(this.txtTotalProducts);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(624, 51);
            this.panel5.TabIndex = 0;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.btnOpenFrmIventory);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel6.Location = new System.Drawing.Point(303, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(249, 51);
            this.panel6.TabIndex = 0;
            // 
            // frmStorage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 571);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmStorage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý tồn kho";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmStorage_FormClosed);
            this.Load += new System.EventHandler(this.frmStorage_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.mnuStrip.ResumeLayout(false);
            this.mnuStrip.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvStorage)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtSearchInfo;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgvStorage;
        private System.Windows.Forms.Button btnOpenFrmIventory;
        private System.Windows.Forms.TextBox txtTotalProducts;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnResetDGV;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaMH;
        private System.Windows.Forms.DataGridViewTextBoxColumn TenMH;
        private System.Windows.Forms.DataGridViewTextBoxColumn TonKho;
        private System.Windows.Forms.DataGridViewTextBoxColumn TinhTrang;
        private System.Windows.Forms.DataGridViewTextBoxColumn TongSLN;
        private System.Windows.Forms.DataGridViewTextBoxColumn TongSLX;
        private System.Windows.Forms.GroupBox groupBox2;
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
    }
}