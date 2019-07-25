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
            YMSL.CS4.FMS.CSCOM.Cols cols2 = new YMSL.CS4.FMS.CSCOM.Cols();
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
            this.lblTrans = new YMSL.CS4.FMS.CSCOM.LabelPYMAC(this.components);
            this.lblTransFromName = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.sqlTransToCode = new YMSL.CS4.FMS.CSCOM.ComboBoxSQL(this.components);
            this.lblTransToName = new System.Windows.Forms.Label();
            this.lblTransFromCode = new System.Windows.Forms.Label();
            this.cmdClear = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).BeginInit();
            this.SuspendLayout();
            // 
            // grdData
            // 
            this.grdData.AllowEditing = true;
            this.grdData.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.SingleColumn;
            this.grdData.ColumnInfo = resources.GetString("grdData.ColumnInfo");
            this.grdData.Location = new System.Drawing.Point(0, 140);
            this.grdData.Rows.Count = 1;
            this.grdData.Rows.DefaultSize = 21;
            this.grdData.Size = new System.Drawing.Size(792, 372);
            this.grdData.TabIndex = 6;
            // 
            // cmdShow
            // 
            this.cmdShow.Location = new System.Drawing.Point(680, 111);
            this.cmdShow.TabIndex = 5;
            // 
            // cmdClose
            // 
            this.cmdClose.TabIndex = 13;
            // 
            // cmdReset
            // 
            this.cmdReset.TabIndex = 12;
            // 
            // cmdEnTranslate
            // 
            this.cmdEnTranslate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdEnTranslate.BackColor = System.Drawing.SystemColors.Control;
            this.cmdEnTranslate.Location = new System.Drawing.Point(362, 518);
            this.cmdEnTranslate.Name = "cmdEnTranslate";
            this.cmdEnTranslate.Size = new System.Drawing.Size(100, 30);
            this.cmdEnTranslate.TabIndex = 10;
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
            this.cmdzhChtTranslate.TabIndex = 11;
            this.cmdzhChtTranslate.Tag = "zh-CHT";
            this.cmdzhChtTranslate.Text = "繁体語化";
            this.cmdzhChtTranslate.UseVisualStyleBackColor = false;
            // 
            // txtProjPath
            // 
            this.txtProjPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtProjPath.Location = new System.Drawing.Point(107, 27);
            this.txtProjPath.Multiline = true;
            this.txtProjPath.Name = "txtProjPath";
            this.txtProjPath.ReadOnly = true;
            this.txtProjPath.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtProjPath.Size = new System.Drawing.Size(567, 52);
            this.txtProjPath.TabIndex = 1;
            // 
            // cmdSelect
            // 
            this.cmdSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSelect.Location = new System.Drawing.Point(680, 56);
            this.cmdSelect.Name = "cmdSelect";
            this.cmdSelect.Size = new System.Drawing.Size(100, 23);
            this.cmdSelect.TabIndex = 2;
            this.cmdSelect.Text = "追加";
            this.cmdSelect.UseVisualStyleBackColor = true;
            this.cmdSelect.Click += new System.EventHandler(this.cmdSelect_Click);
            // 
            // labelPYMAC1
            // 
            this.labelPYMAC1.BackColor = System.Drawing.Color.Teal;
            this.labelPYMAC1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelPYMAC1.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPYMAC1.ForeColor = System.Drawing.Color.Yellow;
            this.labelPYMAC1.Location = new System.Drawing.Point(3, 27);
            this.labelPYMAC1.Margin = new System.Windows.Forms.Padding(3);
            this.labelPYMAC1.MustInput = true;
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
            this.labelPYMAC2.Location = new System.Drawing.Point(3, 111);
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
            this.cboType.Location = new System.Drawing.Point(107, 111);
            this.cboType.Name = "cboType";
            this.cboType.Size = new System.Drawing.Size(100, 23);
            this.cboType.TabIndex = 4;
            // 
            // cmdJpTranslate
            // 
            this.cmdJpTranslate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdJpTranslate.BackColor = System.Drawing.SystemColors.Control;
            this.cmdJpTranslate.Location = new System.Drawing.Point(256, 518);
            this.cmdJpTranslate.Name = "cmdJpTranslate";
            this.cmdJpTranslate.Size = new System.Drawing.Size(100, 30);
            this.cmdJpTranslate.TabIndex = 9;
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
            this.cmdImport.TabIndex = 7;
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
            this.cmdExport.TabIndex = 8;
            this.cmdExport.Text = "辞書エクスポート";
            this.cmdExport.UseVisualStyleBackColor = false;
            this.cmdExport.Click += new System.EventHandler(this.cmdExport_Click);
            // 
            // lblTrans
            // 
            this.lblTrans.BackColor = System.Drawing.Color.Teal;
            this.lblTrans.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTrans.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTrans.ForeColor = System.Drawing.Color.Yellow;
            this.lblTrans.Location = new System.Drawing.Point(3, 82);
            this.lblTrans.Margin = new System.Windows.Forms.Padding(3);
            this.lblTrans.MustInput = true;
            this.lblTrans.Name = "lblTrans";
            this.lblTrans.Size = new System.Drawing.Size(100, 23);
            this.lblTrans.TabIndex = 0;
            this.lblTrans.Text = "翻訳";
            this.lblTrans.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTransFromName
            // 
            this.lblTransFromName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTransFromName.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblTransFromName.Location = new System.Drawing.Point(178, 82);
            this.lblTransFromName.Name = "lblTransFromName";
            this.lblTransFromName.Size = new System.Drawing.Size(100, 23);
            this.lblTransFromName.TabIndex = 0;
            this.lblTransFromName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(284, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 23);
            this.label2.TabIndex = 0;
            this.label2.Text = "⇒";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // sqlTransToCode
            // 
            this.sqlTransToCode.BackColor = System.Drawing.Color.White;
            this.sqlTransToCode.Cols = cols2;
            this.sqlTransToCode.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.sqlTransToCode.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sqlTransToCode.ForeColor = System.Drawing.SystemColors.WindowText;
            this.sqlTransToCode.FormattingEnabled = true;
            this.sqlTransToCode.Location = new System.Drawing.Point(310, 82);
            this.sqlTransToCode.Name = "sqlTransToCode";
            this.sqlTransToCode.Size = new System.Drawing.Size(65, 23);
            this.sqlTransToCode.TabIndex = 3;
            // 
            // lblTransToName
            // 
            this.lblTransToName.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblTransToName.Location = new System.Drawing.Point(381, 82);
            this.lblTransToName.Name = "lblTransToName";
            this.lblTransToName.Size = new System.Drawing.Size(399, 23);
            this.lblTransToName.TabIndex = 0;
            this.lblTransToName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTransFromCode
            // 
            this.lblTransFromCode.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTransFromCode.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblTransFromCode.Location = new System.Drawing.Point(107, 82);
            this.lblTransFromCode.Name = "lblTransFromCode";
            this.lblTransFromCode.Size = new System.Drawing.Size(65, 23);
            this.lblTransFromCode.TabIndex = 0;
            this.lblTransFromCode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmdClear
            // 
            this.cmdClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdClear.Location = new System.Drawing.Point(680, 27);
            this.cmdClear.Name = "cmdClear";
            this.cmdClear.Size = new System.Drawing.Size(100, 23);
            this.cmdClear.TabIndex = 14;
            this.cmdClear.Text = "クリア";
            this.cmdClear.UseVisualStyleBackColor = true;
            this.cmdClear.Click += new System.EventHandler(this.cmdClear_Click);
            // 
            // WinFormEasyTranslate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 573);
            this.Controls.Add(this.cmdClear);
            this.Controls.Add(this.lblTransFromCode);
            this.Controls.Add(this.lblTransToName);
            this.Controls.Add(this.sqlTransToCode);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblTransFromName);
            this.Controls.Add(this.lblTrans);
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
            this.Controls.SetChildIndex(this.cmdImport, 0);
            this.Controls.SetChildIndex(this.cmdExport, 0);
            this.Controls.SetChildIndex(this.lblTrans, 0);
            this.Controls.SetChildIndex(this.lblTransFromName, 0);
            this.Controls.SetChildIndex(this.cmdShow, 0);
            this.Controls.SetChildIndex(this.grdData, 0);
            this.Controls.SetChildIndex(this.cmdClose, 0);
            this.Controls.SetChildIndex(this.cmdReset, 0);
            this.Controls.SetChildIndex(this.lblVersion, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.sqlTransToCode, 0);
            this.Controls.SetChildIndex(this.lblTransToName, 0);
            this.Controls.SetChildIndex(this.lblTransFromCode, 0);
            this.Controls.SetChildIndex(this.cmdClear, 0);
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
        private YMSL.CS4.FMS.CSCOM.LabelPYMAC lblTrans;
        private System.Windows.Forms.Label lblTransFromName;
        private System.Windows.Forms.Label label2;
        private YMSL.CS4.FMS.CSCOM.ComboBoxSQL sqlTransToCode;
        private System.Windows.Forms.Label lblTransToName;
        private System.Windows.Forms.Label lblTransFromCode;
        private System.Windows.Forms.Button cmdClear;
    }
}

