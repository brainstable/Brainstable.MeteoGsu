namespace Brainstable.Filial
{
    partial class FormMain
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            Syncfusion.Windows.Forms.Tools.TreeNavigator.HeaderCollection headerCollection1 = new Syncfusion.Windows.Forms.Tools.TreeNavigator.HeaderCollection();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.groupBar2 = new Syncfusion.Windows.Forms.Tools.GroupBar();
            this.treeNavigator1 = new Syncfusion.Windows.Forms.Tools.TreeNavigator();
            this.groupBarItem1 = new Syncfusion.Windows.Forms.Tools.GroupBarItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.statusStripEx1 = new Syncfusion.Windows.Forms.Tools.StatusStripEx();
            this.statusStripLabel1 = new Syncfusion.Windows.Forms.Tools.StatusStripLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.фАЙЛToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.выходToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сПРАВКАToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.оПрограммеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splashControl1 = new Syncfusion.Windows.Forms.Tools.SplashControl();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupBar2)).BeginInit();
            this.groupBar2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.statusStripEx1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.Controls.Add(this.splitter1);
            this.panel2.Controls.Add(this.tabControl1);
            this.panel2.Controls.Add(this.groupBar2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(800, 404);
            this.panel2.TabIndex = 2;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(173, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(627, 404);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(619, 378);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Метеоданные";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.webBrowser1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(619, 378);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Карта";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(3, 3);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(613, 372);
            this.webBrowser1.TabIndex = 0;
            // 
            // groupBar2
            // 
            this.groupBar2.AllowCollapse = true;
            this.groupBar2.AllowDrop = true;
            this.groupBar2.AnimatedSelection = false;
            this.groupBar2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.groupBar2.BeforeTouchSize = new System.Drawing.Size(173, 404);
            this.groupBar2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(198)))), ((int)(((byte)(198)))));
            this.groupBar2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.groupBar2.CollapsedText = "Меню";
            this.groupBar2.CollapseImage = ((System.Drawing.Image)(resources.GetObject("groupBar2.CollapseImage")));
            this.groupBar2.Controls.Add(this.treeNavigator1);
            this.groupBar2.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBar2.ExpandButtonToolTip = "Click to expand Folder Pane";
            this.groupBar2.ExpandedWidth = 365;
            this.groupBar2.ExpandImage = ((System.Drawing.Image)(resources.GetObject("groupBar2.ExpandImage")));
            this.groupBar2.FlatLook = true;
            this.groupBar2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.groupBar2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.groupBar2.GroupBarDropDownToolTip = null;
            this.groupBar2.GroupBarItemHeight = 40;
            this.groupBar2.GroupBarItems.AddRange(new Syncfusion.Windows.Forms.Tools.GroupBarItem[] {
            this.groupBarItem1});
            this.groupBar2.HeaderBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.groupBar2.HeaderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.groupBar2.HeaderHeight = 35;
            this.groupBar2.IndexOnVisibleItems = true;
            this.groupBar2.Location = new System.Drawing.Point(0, 0);
            this.groupBar2.MinimizeButtonToolTip = null;
            this.groupBar2.Name = "groupBar2";
            this.groupBar2.NavigationPaneButtonWidth = 50;
            this.groupBar2.NavigationPaneHeight = 40;
            this.groupBar2.NavigationPaneTooltip = null;
            this.groupBar2.PopupClientSize = new System.Drawing.Size(330, 300);
            this.groupBar2.PopupResizeMode = Syncfusion.Windows.Forms.Tools.PopupResizeMode.Horizontal;
            this.groupBar2.SelectedItem = 0;
            this.groupBar2.ShowChevron = false;
            this.groupBar2.ShowNavigationPane = false;
            this.groupBar2.Size = new System.Drawing.Size(173, 404);
            this.groupBar2.SmartSizeBox = false;
            this.groupBar2.Splittercolor = System.Drawing.Color.Red;
            this.groupBar2.StackedMode = true;
            this.groupBar2.TabIndex = 1;
            this.groupBar2.Text = "groupBar2";
            this.groupBar2.ThemeName = "Office2016Colorful";
            this.groupBar2.ThemeStyle.CollapsedViewStyle.ItemStyle.SelectedForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.groupBar2.ThemeStyle.ItemStyle.SelectedForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.groupBar2.ThemeStyle.StackedViewStyle.CollapsedItemStyle.SelectedForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.groupBar2.VisualStyle = Syncfusion.Windows.Forms.VisualStyle.Office2016Colorful;
            // 
            // treeNavigator1
            // 
            headerCollection1.Font = new System.Drawing.Font("Arial", 8F);
            this.treeNavigator1.Header = headerCollection1;
            this.treeNavigator1.ItemBackColor = System.Drawing.SystemColors.Control;
            this.treeNavigator1.Location = new System.Drawing.Point(1, 36);
            this.treeNavigator1.MinimumSize = new System.Drawing.Size(150, 150);
            this.treeNavigator1.Name = "treeNavigator1";
            this.treeNavigator1.ShowHeader = false;
            this.treeNavigator1.Size = new System.Drawing.Size(171, 361);
            this.treeNavigator1.TabIndex = 0;
            this.treeNavigator1.Text = "treeNavigator1";
            // 
            // groupBarItem1
            // 
            this.groupBarItem1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.groupBarItem1.Client = this.treeNavigator1;
            this.groupBarItem1.InNavigationPane = true;
            this.groupBarItem1.Text = "Меню";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 404);
            this.panel1.TabIndex = 3;
            // 
            // statusStripEx1
            // 
            this.statusStripEx1.BackColor = System.Drawing.SystemColors.Control;
            this.statusStripEx1.BeforeTouchSize = new System.Drawing.Size(800, 22);
            this.statusStripEx1.Dock = Syncfusion.Windows.Forms.Tools.DockStyleEx.Bottom;
            this.statusStripEx1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusStripLabel1});
            this.statusStripEx1.Location = new System.Drawing.Point(0, 428);
            this.statusStripEx1.MetroColor = System.Drawing.SystemColors.Control;
            this.statusStripEx1.Name = "statusStripEx1";
            this.statusStripEx1.OfficeColorScheme = Syncfusion.Windows.Forms.Tools.ToolStripEx.ColorScheme.Managed;
            this.statusStripEx1.Size = new System.Drawing.Size(800, 22);
            this.statusStripEx1.TabIndex = 5;
            this.statusStripEx1.Text = "statusStripEx1";
            this.statusStripEx1.ThemeName = "Metro";
            this.statusStripEx1.VisualStyle = Syncfusion.Windows.Forms.Tools.StatusStripExStyle.Metro;
            // 
            // statusStripLabel1
            // 
            this.statusStripLabel1.Margin = new System.Windows.Forms.Padding(0, 3, 0, 2);
            this.statusStripLabel1.Name = "statusStripLabel1";
            this.statusStripLabel1.Size = new System.Drawing.Size(99, 15);
            this.statusStripLabel1.Text = "СОРТОУЧАСТКИ";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.фАЙЛToolStripMenuItem,
            this.сПРАВКАToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // фАЙЛToolStripMenuItem
            // 
            this.фАЙЛToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.updateMapToolStripMenuItem,
            this.toolStripSeparator1,
            this.выходToolStripMenuItem});
            this.фАЙЛToolStripMenuItem.Name = "фАЙЛToolStripMenuItem";
            this.фАЙЛToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.фАЙЛToolStripMenuItem.Text = "ФАЙЛ";
            // 
            // updateMapToolStripMenuItem
            // 
            this.updateMapToolStripMenuItem.Name = "updateMapToolStripMenuItem";
            this.updateMapToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.updateMapToolStripMenuItem.Text = "Обновить карту";
            this.updateMapToolStripMenuItem.Click += new System.EventHandler(this.updateMapToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(158, 6);
            // 
            // выходToolStripMenuItem
            // 
            this.выходToolStripMenuItem.Name = "выходToolStripMenuItem";
            this.выходToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.выходToolStripMenuItem.Text = "Выход";
            this.выходToolStripMenuItem.Click += new System.EventHandler(this.выходToolStripMenuItem_Click);
            // 
            // сПРАВКАToolStripMenuItem
            // 
            this.сПРАВКАToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.оПрограммеToolStripMenuItem});
            this.сПРАВКАToolStripMenuItem.Name = "сПРАВКАToolStripMenuItem";
            this.сПРАВКАToolStripMenuItem.Size = new System.Drawing.Size(73, 20);
            this.сПРАВКАToolStripMenuItem.Text = "СПРАВКА";
            // 
            // оПрограммеToolStripMenuItem
            // 
            this.оПрограммеToolStripMenuItem.Name = "оПрограммеToolStripMenuItem";
            this.оПрограммеToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.оПрограммеToolStripMenuItem.Text = "О программе";
            this.оПрограммеToolStripMenuItem.Click += new System.EventHandler(this.оПрограммеToolStripMenuItem_Click);
            // 
            // splashControl1
            // 
            this.splashControl1.AutoModeDisableOwner = true;
            this.splashControl1.FormIcon = ((System.Drawing.Icon)(resources.GetObject("splashControl1.FormIcon")));
            this.splashControl1.HostForm = this;
            this.splashControl1.SplashImage = ((System.Drawing.Image)(resources.GetObject("splashControl1.SplashImage")));
            this.splashControl1.Text = "";
            this.splashControl1.TimerInterval = 2000;
            this.splashControl1.TransparentColor = System.Drawing.Color.Empty;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(173, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 404);
            this.splitter1.TabIndex = 2;
            this.splitter1.TabStop = false;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStripEx1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormMain";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panel2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupBar2)).EndInit();
            this.groupBar2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.statusStripEx1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Syncfusion.Windows.Forms.Tools.GroupBarItem groupBarItem1;
        private Syncfusion.Windows.Forms.Tools.TreeNavigator treeNavigator1;
        private Syncfusion.Windows.Forms.Tools.GroupBar groupBar2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private Syncfusion.Windows.Forms.Tools.StatusStripEx statusStripEx1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private Syncfusion.Windows.Forms.Tools.StatusStripLabel statusStripLabel1;
        private System.Windows.Forms.ToolStripMenuItem фАЙЛToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem выходToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сПРАВКАToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem оПрограммеToolStripMenuItem;
        private Syncfusion.Windows.Forms.Tools.SplashControl splashControl1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.ToolStripMenuItem updateMapToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.Splitter splitter1;
    }
}

