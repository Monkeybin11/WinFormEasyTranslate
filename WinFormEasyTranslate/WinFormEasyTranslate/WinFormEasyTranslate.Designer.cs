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
            this.cmdTranslate = new System.Windows.Forms.Button();
            this.cmdSelect = new System.Windows.Forms.Button();
            this.lblProjPath = new YMSL.CS4.FMS.CSCOM.LabelPYMAC(this.components);
            this.lblType = new YMSL.CS4.FMS.CSCOM.LabelPYMAC(this.components);
            this.cboType = new System.Windows.Forms.ComboBox();
            this.cmdImport = new System.Windows.Forms.Button();
            this.lblTrans = new YMSL.CS4.FMS.CSCOM.LabelPYMAC(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.lblTransFrom = new System.Windows.Forms.Label();
            this.cmdClear = new System.Windows.Forms.Button();
            this.cboCI = new System.Windows.Forms.ComboBox();
            this.lblCI = new YMSL.CS4.FMS.CSCOM.LabelPYMAC(this.components);
            this.cboClass = new System.Windows.Forms.ComboBox();
            this.lblClass = new YMSL.CS4.FMS.CSCOM.LabelPYMAC(this.components);
            this.cboTransTo = new System.Windows.Forms.ComboBox();
            this.txtProjPath = new System.Windows.Forms.RichTextBox();
            this.lblDictPath = new System.Windows.Forms.Label();
            this.cmdDictSelect = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).BeginInit();
            this.SuspendLayout();
            // 
            // grdData
            // 
            this.grdData.AllowEditing = true;
            this.grdData.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.SingleColumn;
            this.grdData.AutoClipboard = true;
            this.grdData.ColumnInfo = resources.GetString("grdData.ColumnInfo");
            this.grdData.Location = new System.Drawing.Point(0, 140);
            this.grdData.Rows.Count = 1;
            this.grdData.Rows.DefaultSize = 21;
            this.grdData.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.CellRange;
            this.grdData.Size = new System.Drawing.Size(826, 376);
            this.grdData.StyleInfo = resources.GetString("grdData.StyleInfo");
            this.grdData.TabIndex = 8;
            this.grdData.ChangeEdit += new System.EventHandler(this.grdData_ChangeEdit);
            // 
            // cmdShow
            // 
            this.cmdShow.Location = new System.Drawing.Point(714, 111);
            this.cmdShow.TabIndex = 7;
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(714, 522);
            this.cmdClose.TabIndex = 15;
            // 
            // cmdReset
            // 
            this.cmdReset.Location = new System.Drawing.Point(608, 522);
            this.cmdReset.TabIndex = 14;
            // 
            // lblVersion
            // 
            this.lblVersion.Location = new System.Drawing.Point(608, 0);
            // 
            // cmdTranslate
            // 
            this.cmdTranslate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdTranslate.BackColor = System.Drawing.SystemColors.Control;
            this.cmdTranslate.Location = new System.Drawing.Point(502, 522);
            this.cmdTranslate.Name = "cmdTranslate";
            this.cmdTranslate.Size = new System.Drawing.Size(100, 30);
            this.cmdTranslate.TabIndex = 13;
            this.cmdTranslate.Tag = "zh-CHT";
            this.cmdTranslate.Text = "翻訳";
            this.cmdTranslate.UseVisualStyleBackColor = false;
            // 
            // cmdSelect
            // 
            this.cmdSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSelect.Location = new System.Drawing.Point(714, 27);
            this.cmdSelect.Name = "cmdSelect";
            this.cmdSelect.Size = new System.Drawing.Size(100, 23);
            this.cmdSelect.TabIndex = 1;
            this.cmdSelect.Text = "追加";
            this.cmdSelect.UseVisualStyleBackColor = true;
            this.cmdSelect.Click += new System.EventHandler(this.cmdSelect_Click);
            // 
            // lblProjPath
            // 
            this.lblProjPath.BackColor = System.Drawing.Color.Teal;
            this.lblProjPath.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblProjPath.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProjPath.ForeColor = System.Drawing.Color.Yellow;
            this.lblProjPath.Location = new System.Drawing.Point(3, 27);
            this.lblProjPath.Margin = new System.Windows.Forms.Padding(3);
            this.lblProjPath.MustInput = true;
            this.lblProjPath.Name = "lblProjPath";
            this.lblProjPath.Size = new System.Drawing.Size(100, 23);
            this.lblProjPath.TabIndex = 0;
            this.lblProjPath.Text = "プロジェクト";
            this.lblProjPath.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblType
            // 
            this.lblType.BackColor = System.Drawing.Color.Teal;
            this.lblType.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblType.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblType.ForeColor = System.Drawing.Color.White;
            this.lblType.Location = new System.Drawing.Point(440, 111);
            this.lblType.Margin = new System.Windows.Forms.Padding(3);
            this.lblType.MustInput = false;
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(100, 23);
            this.lblType.TabIndex = 0;
            this.lblType.Text = "類型";
            this.lblType.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cboType
            // 
            this.cboType.FormattingEnabled = true;
            this.cboType.Location = new System.Drawing.Point(544, 111);
            this.cboType.Name = "cboType";
            this.cboType.Size = new System.Drawing.Size(100, 23);
            this.cboType.TabIndex = 6;
            // 
            // cmdImport
            // 
            this.cmdImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdImport.BackColor = System.Drawing.SystemColors.Control;
            this.cmdImport.Location = new System.Drawing.Point(398, 522);
            this.cmdImport.Name = "cmdImport";
            this.cmdImport.Size = new System.Drawing.Size(100, 30);
            this.cmdImport.TabIndex = 9;
            this.cmdImport.Text = "辞書読込";
            this.cmdImport.UseVisualStyleBackColor = false;
            this.cmdImport.Click += new System.EventHandler(this.cmdImport_Click);
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
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(293, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 23);
            this.label2.TabIndex = 0;
            this.label2.Text = "⇒";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTransFrom
            // 
            this.lblTransFrom.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTransFrom.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblTransFrom.Location = new System.Drawing.Point(107, 82);
            this.lblTransFrom.Name = "lblTransFrom";
            this.lblTransFrom.Size = new System.Drawing.Size(180, 23);
            this.lblTransFrom.TabIndex = 0;
            this.lblTransFrom.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmdClear
            // 
            this.cmdClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdClear.Location = new System.Drawing.Point(714, 56);
            this.cmdClear.Name = "cmdClear";
            this.cmdClear.Size = new System.Drawing.Size(100, 23);
            this.cmdClear.TabIndex = 2;
            this.cmdClear.Text = "クリア";
            this.cmdClear.UseVisualStyleBackColor = true;
            this.cmdClear.Click += new System.EventHandler(this.cmdClear_Click);
            // 
            // cboCI
            // 
            this.cboCI.FormattingEnabled = true;
            this.cboCI.Location = new System.Drawing.Point(107, 111);
            this.cboCI.Name = "cboCI";
            this.cboCI.Size = new System.Drawing.Size(100, 23);
            this.cboCI.TabIndex = 4;
            // 
            // lblCI
            // 
            this.lblCI.BackColor = System.Drawing.Color.Teal;
            this.lblCI.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCI.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCI.ForeColor = System.Drawing.Color.White;
            this.lblCI.Location = new System.Drawing.Point(3, 111);
            this.lblCI.Margin = new System.Windows.Forms.Padding(3);
            this.lblCI.MustInput = false;
            this.lblCI.Name = "lblCI";
            this.lblCI.Size = new System.Drawing.Size(100, 23);
            this.lblCI.TabIndex = 0;
            this.lblCI.Text = "CI";
            this.lblCI.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cboClass
            // 
            this.cboClass.FormattingEnabled = true;
            this.cboClass.Location = new System.Drawing.Point(317, 111);
            this.cboClass.Name = "cboClass";
            this.cboClass.Size = new System.Drawing.Size(117, 23);
            this.cboClass.TabIndex = 5;
            // 
            // lblClass
            // 
            this.lblClass.BackColor = System.Drawing.Color.Teal;
            this.lblClass.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblClass.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClass.ForeColor = System.Drawing.Color.White;
            this.lblClass.Location = new System.Drawing.Point(213, 111);
            this.lblClass.Margin = new System.Windows.Forms.Padding(3);
            this.lblClass.MustInput = false;
            this.lblClass.Name = "lblClass";
            this.lblClass.Size = new System.Drawing.Size(100, 23);
            this.lblClass.TabIndex = 0;
            this.lblClass.Text = "クラス";
            this.lblClass.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cboTransTo
            // 
            this.cboTransTo.FormattingEnabled = true;
            this.cboTransTo.Location = new System.Drawing.Point(317, 82);
            this.cboTransTo.Name = "cboTransTo";
            this.cboTransTo.Size = new System.Drawing.Size(223, 23);
            this.cboTransTo.TabIndex = 17;
            // 
            // txtProjPath
            // 
            this.txtProjPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtProjPath.BackColor = System.Drawing.SystemColors.Control;
            this.txtProjPath.Location = new System.Drawing.Point(107, 27);
            this.txtProjPath.Name = "txtProjPath";
            this.txtProjPath.ReadOnly = true;
            this.txtProjPath.Size = new System.Drawing.Size(601, 52);
            this.txtProjPath.TabIndex = 18;
            this.txtProjPath.Text = "";
            // 
            // lblDictPath
            // 
            this.lblDictPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDictPath.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDictPath.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblDictPath.Location = new System.Drawing.Point(109, 527);
            this.lblDictPath.Name = "lblDictPath";
            this.lblDictPath.Size = new System.Drawing.Size(283, 23);
            this.lblDictPath.TabIndex = 0;
            this.lblDictPath.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmdDictSelect
            // 
            this.cmdDictSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdDictSelect.BackColor = System.Drawing.SystemColors.Control;
            this.cmdDictSelect.Location = new System.Drawing.Point(3, 522);
            this.cmdDictSelect.Name = "cmdDictSelect";
            this.cmdDictSelect.Size = new System.Drawing.Size(100, 30);
            this.cmdDictSelect.TabIndex = 9;
            this.cmdDictSelect.Text = "辞書選択";
            this.cmdDictSelect.UseVisualStyleBackColor = false;
            this.cmdDictSelect.Click += new System.EventHandler(this.cmdDictSelect_Click);
            // 
            // WinFormEasyTranslate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(826, 577);
            this.Controls.Add(this.txtProjPath);
            this.Controls.Add(this.cboTransTo);
            this.Controls.Add(this.cboClass);
            this.Controls.Add(this.lblClass);
            this.Controls.Add(this.cboCI);
            this.Controls.Add(this.lblCI);
            this.Controls.Add(this.cmdClear);
            this.Controls.Add(this.lblDictPath);
            this.Controls.Add(this.lblTransFrom);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblTrans);
            this.Controls.Add(this.cmdDictSelect);
            this.Controls.Add(this.cmdImport);
            this.Controls.Add(this.cboType);
            this.Controls.Add(this.lblType);
            this.Controls.Add(this.lblProjPath);
            this.Controls.Add(this.cmdSelect);
            this.Controls.Add(this.cmdTranslate);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.LogFormLoad = false;
            this.Name = "WinFormEasyTranslate";
            this.Text = "多言語対応便利ツール(WinFormEasyTranslate)";
            this.VersionVisible = true;
            this.Controls.SetChildIndex(this.cmdTranslate, 0);
            this.Controls.SetChildIndex(this.cmdSelect, 0);
            this.Controls.SetChildIndex(this.lblProjPath, 0);
            this.Controls.SetChildIndex(this.lblType, 0);
            this.Controls.SetChildIndex(this.cboType, 0);
            this.Controls.SetChildIndex(this.cmdImport, 0);
            this.Controls.SetChildIndex(this.cmdDictSelect, 0);
            this.Controls.SetChildIndex(this.lblTrans, 0);
            this.Controls.SetChildIndex(this.cmdShow, 0);
            this.Controls.SetChildIndex(this.grdData, 0);
            this.Controls.SetChildIndex(this.cmdClose, 0);
            this.Controls.SetChildIndex(this.cmdReset, 0);
            this.Controls.SetChildIndex(this.lblVersion, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.lblTransFrom, 0);
            this.Controls.SetChildIndex(this.lblDictPath, 0);
            this.Controls.SetChildIndex(this.cmdClear, 0);
            this.Controls.SetChildIndex(this.lblCI, 0);
            this.Controls.SetChildIndex(this.cboCI, 0);
            this.Controls.SetChildIndex(this.lblClass, 0);
            this.Controls.SetChildIndex(this.cboClass, 0);
            this.Controls.SetChildIndex(this.cboTransTo, 0);
            this.Controls.SetChildIndex(this.txtProjPath, 0);
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button cmdTranslate;
        private System.Windows.Forms.Button cmdSelect;
        private YMSL.CS4.FMS.CSCOM.LabelPYMAC lblProjPath;
        private YMSL.CS4.FMS.CSCOM.LabelPYMAC lblType;
        private System.Windows.Forms.ComboBox cboType;
        private System.Windows.Forms.Button cmdImport;
        private YMSL.CS4.FMS.CSCOM.LabelPYMAC lblTrans;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblTransFrom;
        private System.Windows.Forms.Button cmdClear;
        private System.Windows.Forms.ComboBox cboCI;
        private YMSL.CS4.FMS.CSCOM.LabelPYMAC lblCI;
        private System.Windows.Forms.ComboBox cboClass;
        private YMSL.CS4.FMS.CSCOM.LabelPYMAC lblClass;
        private System.Windows.Forms.ComboBox cboTransTo;
        private System.Windows.Forms.RichTextBox txtProjPath;
        private System.Windows.Forms.Label lblDictPath;
        private System.Windows.Forms.Button cmdDictSelect;
    }
}

