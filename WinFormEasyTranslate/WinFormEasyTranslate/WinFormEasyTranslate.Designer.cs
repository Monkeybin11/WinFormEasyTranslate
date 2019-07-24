namespace WinFormEasyTranslate
{
    partial class WinFormEasyTranslate
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WinFormEasyTranslate));
            this.cmdEnTranslate = new System.Windows.Forms.Button();
            this.cmdzhChtTranslate = new System.Windows.Forms.Button();
            this.txtProjPath = new System.Windows.Forms.TextBox();
            this.cmdSelect = new System.Windows.Forms.Button();
            this.labelPYMAC1 = new YMSL.CS4.FMS.CSCOM.LabelPYMAC(this.components);
            this.labelPYMAC2 = new YMSL.CS4.FMS.CSCOM.LabelPYMAC(this.components);
            this.cboType = new System.Windows.Forms.ComboBox();
            this.cmdJpTranslate = new System.Windows.Forms.Button();
            this.cmdImport = new System.Windows.Forms.Button();
            this.cmdExport = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).BeginInit();
            this.SuspendLayout();
            // 
            // grdData
            // 
            this.grdData.AllowEditing = true;
            this.grdData.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.SingleColumn;
            this.grdData.ColumnInfo = resources.GetString("grdData.ColumnInfo");
            this.grdData.Location = new System.Drawing.Point(0, 83);
            this.grdData.Rows.Count = 1;
            this.grdData.Rows.DefaultSize = 21;
            this.grdData.Size = new System.Drawing.Size(792, 429);
            // 
            // cmdShow
            // 
            this.cmdShow.Location = new System.Drawing.Point(680, 52);
            // 
            // cmdEnTranslate
            // 
            this.cmdEnTranslate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdEnTranslate.BackColor = System.Drawing.SystemColors.Control;
            this.cmdEnTranslate.Location = new System.Drawing.Point(362, 518);
            this.cmdEnTranslate.Name = "cmdEnTranslate";
            this.cmdEnTranslate.Size = new System.Drawing.Size(100, 30);
            this.cmdEnTranslate.TabIndex = 0;
            this.cmdEnTranslate.Tag = "en";
            this.cmdEnTranslate.Text = "英語化";
            this.cmdEnTranslate.UseVisualStyleBackColor = false;
            // 
            // cmdzhChtTranslate
            // 
            this.cmdzhChtTranslate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdzhChtTranslate.BackColor = System.Drawing.SystemColors.Control;
            this.cmdzhChtTranslate.Location = new System.Drawing.Point(468, 518);
            this.cmdzhChtTranslate.Name = "cmdzhChtTranslate";
            this.cmdzhChtTranslate.Size = new System.Drawing.Size(100, 30);
            this.cmdzhChtTranslate.TabIndex = 0;
            this.cmdzhChtTranslate.Tag = "zh-CHT";
            this.cmdzhChtTranslate.Text = "繁体語化";
            this.cmdzhChtTranslate.UseVisualStyleBackColor = false;
            // 
            // txtProjPath
            // 
            this.txtProjPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtProjPath.Location = new System.Drawing.Point(107, 27);
            this.txtProjPath.Name = "txtProjPath";
            this.txtProjPath.ReadOnly = true;
            this.txtProjPath.Size = new System.Drawing.Size(461, 22);
            this.txtProjPath.TabIndex = 1;
            // 
            // cmdSelect
            // 
            this.cmdSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSelect.Location = new System.Drawing.Point(574, 26);
            this.cmdSelect.Name = "cmdSelect";
            this.cmdSelect.Size = new System.Drawing.Size(75, 23);
            this.cmdSelect.TabIndex = 3;
            this.cmdSelect.Text = "選択";
            this.cmdSelect.UseVisualStyleBackColor = true;
            this.cmdSelect.Click += new System.EventHandler(this.cmdSelect_Click);
            // 
            // labelPYMAC1
            // 
            this.labelPYMAC1.BackColor = System.Drawing.Color.Teal;
            this.labelPYMAC1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelPYMAC1.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPYMAC1.ForeColor = System.Drawing.Color.White;
            this.labelPYMAC1.Location = new System.Drawing.Point(3, 27);
            this.labelPYMAC1.Margin = new System.Windows.Forms.Padding(3);
            this.labelPYMAC1.MustInput = false;
            this.labelPYMAC1.Name = "labelPYMAC1";
            this.labelPYMAC1.Size = new System.Drawing.Size(100, 23);
            this.labelPYMAC1.TabIndex = 0;
            this.labelPYMAC1.Text = "プロジェクト";
            this.labelPYMAC1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelPYMAC2
            // 
            this.labelPYMAC2.BackColor = System.Drawing.Color.Teal;
            this.labelPYMAC2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelPYMAC2.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPYMAC2.ForeColor = System.Drawing.Color.White;
            this.labelPYMAC2.Location = new System.Drawing.Point(3, 54);
            this.labelPYMAC2.Margin = new System.Windows.Forms.Padding(3);
            this.labelPYMAC2.MustInput = false;
            this.labelPYMAC2.Name = "labelPYMAC2";
            this.labelPYMAC2.Size = new System.Drawing.Size(100, 23);
            this.labelPYMAC2.TabIndex = 0;
            this.labelPYMAC2.Text = "类型";
            this.labelPYMAC2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cboType
            // 
            this.cboType.FormattingEnabled = true;
            this.cboType.Location = new System.Drawing.Point(107, 53);
            this.cboType.Name = "cboType";
            this.cboType.Size = new System.Drawing.Size(127, 23);
            this.cboType.TabIndex = 8;
            // 
            // cmdJpTranslate
            // 
            this.cmdJpTranslate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdJpTranslate.BackColor = System.Drawing.SystemColors.Control;
            this.cmdJpTranslate.Location = new System.Drawing.Point(256, 518);
            this.cmdJpTranslate.Name = "cmdJpTranslate";
            this.cmdJpTranslate.Size = new System.Drawing.Size(100, 30);
            this.cmdJpTranslate.TabIndex = 0;
            this.cmdJpTranslate.Tag = "jp";
            this.cmdJpTranslate.Text = "日本語化";
            this.cmdJpTranslate.UseVisualStyleBackColor = false;
            this.cmdJpTranslate.Visible = false;
            // 
            // cmdImport
            // 
            this.cmdImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdImport.BackColor = System.Drawing.SystemColors.Control;
            this.cmdImport.Location = new System.Drawing.Point(3, 518);
            this.cmdImport.Name = "cmdImport";
            this.cmdImport.Size = new System.Drawing.Size(100, 30);
            this.cmdImport.TabIndex = 9;
            this.cmdImport.Text = "辞書読込";
            this.cmdImport.UseVisualStyleBackColor = false;
            this.cmdImport.Click += new System.EventHandler(this.cmdImport_Click);
            // 
            // cmdExport
            // 
            this.cmdExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdExport.BackColor = System.Drawing.SystemColors.Control;
            this.cmdExport.Location = new System.Drawing.Point(107, 518);
            this.cmdExport.Name = "cmdExport";
            this.cmdExport.Size = new System.Drawing.Size(112, 30);
            this.cmdExport.TabIndex = 9;
            this.cmdExport.Text = "辞書エクスポート";
            this.cmdExport.UseVisualStyleBackColor = false;
            this.cmdExport.Click += new System.EventHandler(this.cmdExport_Click);
            // 
            // WinFormEasyTranslate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 573);
            this.Controls.Add(this.cmdExport);
            this.Controls.Add(this.cmdImport);
            this.Controls.Add(this.cboType);
            this.Controls.Add(this.labelPYMAC2);
            this.Controls.Add(this.labelPYMAC1);
            this.Controls.Add(this.cmdSelect);
            this.Controls.Add(this.txtProjPath);
            this.Controls.Add(this.cmdzhChtTranslate);
            this.Controls.Add(this.cmdJpTranslate);
            this.Controls.Add(this.cmdEnTranslate);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.LogFormLoad = false;
            this.Name = "WinFormEasyTranslate";
            this.Text = "多言語対応便利ツール(WinFormEasyTranslate)";
            this.VersionVisible = true;
            this.Controls.SetChildIndex(this.cmdEnTranslate, 0);
            this.Controls.SetChildIndex(this.cmdJpTranslate, 0);
            this.Controls.SetChildIndex(this.cmdzhChtTranslate, 0);
            this.Controls.SetChildIndex(this.txtProjPath, 0);
            this.Controls.SetChildIndex(this.cmdSelect, 0);
            this.Controls.SetChildIndex(this.labelPYMAC1, 0);
            this.Controls.SetChildIndex(this.labelPYMAC2, 0);
            this.Controls.SetChildIndex(this.cboType, 0);
            this.Controls.SetChildIndex(this.cmdShow, 0);
            this.Controls.SetChildIndex(this.grdData, 0);
            this.Controls.SetChildIndex(this.cmdClose, 0);
            this.Controls.SetChildIndex(this.cmdReset, 0);
            this.Controls.SetChildIndex(this.lblVersion, 0);
            this.Controls.SetChildIndex(this.cmdImport, 0);
            this.Controls.SetChildIndex(this.cmdExport, 0);
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdEnTranslate;
        private System.Windows.Forms.Button cmdzhChtTranslate;
        private System.Windows.Forms.TextBox txtProjPath;
        private System.Windows.Forms.Button cmdSelect;
        private YMSL.CS4.FMS.CSCOM.LabelPYMAC labelPYMAC1;
        private YMSL.CS4.FMS.CSCOM.LabelPYMAC labelPYMAC2;
        private System.Windows.Forms.ComboBox cboType;
        private System.Windows.Forms.Button cmdJpTranslate;
        private System.Windows.Forms.Button cmdImport;
        private System.Windows.Forms.Button cmdExport;
    }
}

