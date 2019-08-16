namespace WindowsFormsApplication1
{
    partial class MainWindow
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
            this.components = new System.ComponentModel.Container();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.fileOpenButton = new System.Windows.Forms.Button();
            this.searchString = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.decSearch = new System.Windows.Forms.RadioButton();
            this.openedFileName = new System.Windows.Forms.TextBox();
            this.hexSearch = new System.Windows.Forms.RadioButton();
            this.relativeCheck = new System.Windows.Forms.CheckBox();
            this.searchButton = new System.Windows.Forms.Button();
            this.results = new System.Windows.Forms.DataGridView();
            this.pos = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.val = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.asciiSearch = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.resultsLabel = new System.Windows.Forms.Label();
            this.editCheck = new System.Windows.Forms.CheckBox();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.stopSearchButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.results)).BeginInit();
            this.SuspendLayout();
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog_FileOk);
            // 
            // fileOpenButton
            // 
            this.fileOpenButton.Location = new System.Drawing.Point(12, 84);
            this.fileOpenButton.Name = "fileOpenButton";
            this.fileOpenButton.Size = new System.Drawing.Size(75, 23);
            this.fileOpenButton.TabIndex = 5;
            this.fileOpenButton.Text = "Open File";
            this.toolTip.SetToolTip(this.fileOpenButton, "Opens file in which you want to search for data (if you want open in edit mode fi" +
        "rst check box under or reopen file with \"edit mode\" chcecked)");
            this.fileOpenButton.UseVisualStyleBackColor = true;
            this.fileOpenButton.Click += new System.EventHandler(this.openFileButton_Click);
            // 
            // searchString
            // 
            this.searchString.Location = new System.Drawing.Point(86, 13);
            this.searchString.Name = "searchString";
            this.searchString.Size = new System.Drawing.Size(161, 20);
            this.searchString.TabIndex = 2;
            this.toolTip.SetToolTip(this.searchString, "Input for searching data");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Search data:";
            // 
            // decSearch
            // 
            this.decSearch.AutoSize = true;
            this.decSearch.Location = new System.Drawing.Point(253, 37);
            this.decSearch.Name = "decSearch";
            this.decSearch.Size = new System.Drawing.Size(47, 17);
            this.decSearch.TabIndex = 3;
            this.decSearch.Text = "DEC";
            this.toolTip.SetToolTip(this.decSearch, "Searches for specified values that are input as decimal values (every value must " +
        "be comma-separated)");
            this.decSearch.UseVisualStyleBackColor = true;
            // 
            // openedFileName
            // 
            this.openedFileName.Location = new System.Drawing.Point(93, 86);
            this.openedFileName.Name = "openedFileName";
            this.openedFileName.ReadOnly = true;
            this.openedFileName.Size = new System.Drawing.Size(209, 20);
            this.openedFileName.TabIndex = 5;
            this.openedFileName.TabStop = false;
            // 
            // hexSearch
            // 
            this.hexSearch.AutoSize = true;
            this.hexSearch.Checked = true;
            this.hexSearch.Location = new System.Drawing.Point(253, 14);
            this.hexSearch.Name = "hexSearch";
            this.hexSearch.Size = new System.Drawing.Size(47, 17);
            this.hexSearch.TabIndex = 3;
            this.hexSearch.TabStop = true;
            this.hexSearch.Text = "HEX";
            this.toolTip.SetToolTip(this.hexSearch, "Searches for specified HEX values in file (every number have to be 2 digits long)" +
        "");
            this.hexSearch.UseVisualStyleBackColor = true;
            // 
            // relativeCheck
            // 
            this.relativeCheck.AutoSize = true;
            this.relativeCheck.Location = new System.Drawing.Point(86, 39);
            this.relativeCheck.Name = "relativeCheck";
            this.relativeCheck.Size = new System.Drawing.Size(95, 17);
            this.relativeCheck.TabIndex = 4;
            this.relativeCheck.Text = "relative search";
            this.toolTip.SetToolTip(this.relativeCheck, "This option searches for values that fufils pattern (eg. searching for 0A0B will " +
        "find every combination of bytes where second byte is one greather than first)");
            this.relativeCheck.UseVisualStyleBackColor = true;
            // 
            // searchButton
            // 
            this.searchButton.Location = new System.Drawing.Point(227, 112);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(75, 23);
            this.searchButton.TabIndex = 7;
            this.searchButton.Text = "Search";
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // results
            // 
            this.results.AllowUserToAddRows = false;
            this.results.AllowUserToDeleteRows = false;
            this.results.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.results.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.results.BackgroundColor = System.Drawing.SystemColors.Control;
            this.results.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.results.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.pos,
            this.val});
            this.results.Location = new System.Drawing.Point(12, 139);
            this.results.Name = "results";
            this.results.RowHeadersVisible = false;
            this.results.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.results.Size = new System.Drawing.Size(290, 205);
            this.results.TabIndex = 8;
            this.results.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.results_CellEndEdit);
            // 
            // pos
            // 
            this.pos.HeaderText = "Position";
            this.pos.Name = "pos";
            this.pos.ReadOnly = true;
            // 
            // val
            // 
            this.val.HeaderText = "Value";
            this.val.Name = "val";
            // 
            // asciiSearch
            // 
            this.asciiSearch.AutoSize = true;
            this.asciiSearch.Location = new System.Drawing.Point(253, 60);
            this.asciiSearch.Name = "asciiSearch";
            this.asciiSearch.Size = new System.Drawing.Size(52, 17);
            this.asciiSearch.TabIndex = 3;
            this.asciiSearch.Text = "ASCII";
            this.toolTip.SetToolTip(this.asciiSearch, "Searches for specified string in file (case sensitive)");
            this.asciiSearch.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(90, 116);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Search Results:";
            // 
            // resultsLabel
            // 
            this.resultsLabel.AutoSize = true;
            this.resultsLabel.Location = new System.Drawing.Point(167, 117);
            this.resultsLabel.Name = "resultsLabel";
            this.resultsLabel.Size = new System.Drawing.Size(13, 13);
            this.resultsLabel.TabIndex = 9;
            this.resultsLabel.Text = "0";
            this.resultsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // editCheck
            // 
            this.editCheck.AutoSize = true;
            this.editCheck.Location = new System.Drawing.Point(12, 116);
            this.editCheck.Name = "editCheck";
            this.editCheck.Size = new System.Drawing.Size(72, 17);
            this.editCheck.TabIndex = 6;
            this.editCheck.Text = "edit mode";
            this.toolTip.SetToolTip(this.editCheck, "Check it before opening a file (this option allows you to modify found values)");
            this.editCheck.UseVisualStyleBackColor = true;
            // 
            // toolTip
            // 
            this.toolTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTip.ToolTipTitle = "Tooltip";
            // 
            // stopSearchButton
            // 
            this.stopSearchButton.Location = new System.Drawing.Point(227, 112);
            this.stopSearchButton.Name = "stopSearchButton";
            this.stopSearchButton.Size = new System.Drawing.Size(75, 23);
            this.stopSearchButton.TabIndex = 7;
            this.stopSearchButton.Text = "Stop";
            this.stopSearchButton.UseVisualStyleBackColor = true;
            this.stopSearchButton.Visible = false;
            this.stopSearchButton.Click += new System.EventHandler(this.stopSearch_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(314, 358);
            this.Controls.Add(this.editCheck);
            this.Controls.Add(this.resultsLabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.results);
            this.Controls.Add(this.searchButton);
            this.Controls.Add(this.relativeCheck);
            this.Controls.Add(this.openedFileName);
            this.Controls.Add(this.asciiSearch);
            this.Controls.Add(this.hexSearch);
            this.Controls.Add(this.decSearch);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.searchString);
            this.Controls.Add(this.fileOpenButton);
            this.Controls.Add(this.stopSearchButton);
            this.MinimumSize = new System.Drawing.Size(330, 397);
            this.Name = "MainWindow";
            this.Text = "Binary Finder";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.results)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.TextBox searchString;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton decSearch;
        private System.Windows.Forms.TextBox openedFileName;
        private System.Windows.Forms.RadioButton hexSearch;
        private System.Windows.Forms.RadioButton asciiSearch;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox editCheck;
        private System.Windows.Forms.CheckBox relativeCheck;
        public System.Windows.Forms.DataGridView results;
        public System.Windows.Forms.Label resultsLabel;
        public System.Windows.Forms.Button fileOpenButton;
        public System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn pos;
        private System.Windows.Forms.DataGridViewTextBoxColumn val;
        private System.Windows.Forms.ToolTip toolTip;
        public System.Windows.Forms.Button stopSearchButton;
    }
}

