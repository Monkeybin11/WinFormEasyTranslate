using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OfficeOpenXml;
using System.IO;
using YMSL.CS4.FMS.CSCOM;
using C1.Win.C1FlexGrid;
using System.Xml;

namespace WinFormEasyTranslate
{
    public partial class WinFormEasyTranslate : SearchForm
    {
        #region 変数
        private List<string> workPaths = new List<string>();

        private Dictionary<string, List<FileInfo>> fileNames = new Dictionary<string, List<FileInfo>>();

        /// <summary>
        /// ダイアログ
        /// </summary>
        private SaveFileDialog dlgSave = new SaveFileDialog();

        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public WinFormEasyTranslate()
        {
            InitializeComponent();
        }

        #region イベント

        /// <summary>
        /// 選択ボタン処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSelect_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "プロジェクトファイル(*.csproj)|*.csproj";

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                if (!txtProjPath.Text.Contains(dlg.FileName))
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine(string.Format("{0};", dlg.FileName));

                    txtProjPath.AppendText(sb.ToString());
                    FileInfo file = new FileInfo(dlg.FileName);
                    workPaths.Add(file.DirectoryName);

                    // XMLファイルの読み込み
                    XmlDom _XmlDom = new XmlDom();
                    _XmlDom.LoadFromXmlFile(System.Reflection.Assembly.GetExecutingAssembly().Location);
                    _XmlDom.SetToXml(txtProjPath.Text, "PROJECT_INFO", "PATH");

                    // XMLファイルを保存する
                    _XmlDom.SaveToXmlFile(System.Reflection.Assembly.GetExecutingAssembly().Location);
                }
                else
                {
                    MessageBox.Show("選択するプロジェクトは既に選択済みです、再度選択できません。");
                    return;
                }
            }

            InitCboCI();
        }

        private void cmdClear_Click(object sender, EventArgs e)
        {
            try
            {
                txtProjPath.Clear();
                workPaths.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
        }

        private void cmdDictSelect_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "辞書を選択してください。";
            dlg.Filter = "辞書ファイル(*.xlsm)|*.xlsm|辞書ファイル(*.xlsx)|*.xlsx";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                lblDictPath.Text = dlg.FileName;

                // XMLファイルの読み込み
                XmlDom _XmlDom = new XmlDom();
                _XmlDom.LoadFromXmlFile(System.Reflection.Assembly.GetExecutingAssembly().Location);
                _XmlDom.SetToXml(lblDictPath.Text, "DICTIONARY_INFO", "PATH");

                // XMLファイルを保存する
                _XmlDom.SaveToXmlFile(System.Reflection.Assembly.GetExecutingAssembly().Location);
            }
        }

        /// <summary>
        /// 辞書登録
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdImport_Click(object sender, EventArgs e)
        {
            if(lblDictPath.Text.Length == 0)
            {
                MessageBox.Show("辞書を選択してください。");
                return;
            }

            DataTable loadedDictionary = ExcelToDt(lblDictPath.Text, true);
            if (loadedDictionary == null)
            {
                MessageBox.Show("辞書ロードエラーが出しました。辞書フォマードを確認してください。");
                return;
            }
            UpdateWordsToGridFromLoadedDic(loadedDictionary);

            grdData.AutoSizeRows();
        }

        /// <summary>
        /// 辞書エクスポート
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdExport_Click(object sender, EventArgs e)
        {
            dlgSave.AddExtension = true;
            dlgSave.OverwritePrompt = true;
            dlgSave.Filter = "辞書ファイル(*.xlsx)|*.xlsx";

            if (dlgSave.FileName.Length == 0)
            {
                //dlgSave.FileName = string.Format("{0}_辞書", Path.GetFileNameWithoutExtension(file.Name));
            }
            else
            {
                //拡張子をとってしまう
                dlgSave.FileName = System.IO.Path.ChangeExtension(dlgSave.FileName, null);
            }

            if (dlgSave.ShowDialog(this.TopLevelControl) == DialogResult.OK)
            {
                //Excel(ヘッダ付)
                grdData.SaveExcel(dlgSave.FileName, "Sheet1", FileFlags.VisibleOnly | FileFlags.IncludeFixedCells | FileFlags.IncludeMergedRanges | FileFlags.AsDisplayed);

                // 出力完了メッセージ表示
                MessageBox.Show(this, "辞書出力完了しました。", this.Text,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 各翻訳ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CmdTranslate_Click(object sender, EventArgs e)
        {
            try
            {
                string language = cboTransTo.SelectedValue.ToString();

                var updateTarget = (from Row r in grdData.Rows.Cast<Row>()
                                    let rng = grdData.GetCellRange(r.Index, grdData.Cols[string.Format("{0}_value", language)].Index)
                                    where r.Index >= grdData.Rows.Fixed &&
                                            (rng.StyleNew.ForeColor == Color.Blue ||
                                            rng.StyleNew.BackColor == Color.Red)
                                    select r);
                if(!updateTarget.Any())
                {
                    MessageBox.Show("変更箇所がありません。");

                    return;
                }

                var gettor = updateTarget.GetEnumerator();
                List<ResourceDto> write_infos = new List<ResourceDto>();

                while (gettor.MoveNext())
                {
                    var currentRow = gettor.Current;

                    string value = null;
                    value = Convert.ToString(currentRow[string.Format("{0}_value", language)]);

                    string file_name = GetFileNameByClassNameAndLanguage(Convert.ToString(currentRow["class_name"]), language);
                    string delault_language_file_name = GetFileNameByClassNameAndLanguage(Convert.ToString(currentRow["class_name"]), lblTransFrom.Tag.ToString());
                    string file_path = fileNames[Convert.ToString(currentRow["ci"])].Where(r => r.Name == delault_language_file_name).First().DirectoryName + "\\" + file_name;

                    if (!File.Exists(file_path))
                    {
                        File.Create(file_path).Close();
                    }

                    write_infos.Add(
                    new ResourceDto()
                    {
                        is_grid_info = Convert.ToBoolean(currentRow["is_grdInfo"]),
                        language = language,
                        file_name = file_path,
                        class_name = Convert.ToString(currentRow["class_name"]),
                        resource_key = Convert.ToString(currentRow["resource_key"]),
                        value = value,
                    });
                }

                //グリッド情報ではない
                var ResourceWordsExsist = write_infos.Where(r => !r.is_grid_info);

                if (ResourceWordsExsist.Any())
                {
                    UpdateResourceByGrid(language, ResourceWordsExsist.ToList());
                }

                //グリッド情報
                var ResourceGridWordsExsist = write_infos.Where(r => r.is_grid_info);

                if (ResourceGridWordsExsist.Any())
                {
                    UpdateGridInfoByGrid(language, ResourceGridWordsExsist.ToList());
                }

                cmdShow.PerformClick();

                MessageBox.Show("リソースファイル翻訳完了しました。手動でファイルをプロジェクトに含めてください。");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
        }

        /// <summary>
        /// グリッド編集後
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GrdData_AfterEdit(object sender, RowColEventArgs e)
        {
            try
            {
                CellRange rng = grdData.GetCellRange(e.Row, e.Col);

                if (Convert.ToString(grdData[e.Row, e.Col]) == "")
                {
                    rng.StyleNew.BackColor = Color.Red;
                }
                else
                {
                    rng.StyleNew.ForeColor = Color.Blue;
                }

                grdData.AutoSizeRow(e.Row);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
        }

        private void grdData_ChangeEdit(object sender, EventArgs e)
        {
            using (Graphics g = grdData.CreateGraphics())
            {
                // measure text height
                StringFormat sf = new StringFormat();
                int wid = grdData.Cols[grdData.Col].WidthDisplay - 2;
                string text = grdData.Editor.Text;
                SizeF sz = g.MeasureString(text, grdData.Font, wid, sf);

                // adjust row height if necessary
                C1.Win.C1FlexGrid.Row row = grdData.Rows[grdData.Row];
                if (sz.Height + 4 > row.HeightDisplay)
                    row.HeightDisplay = (int)sz.Height + 4;
            }
        }
        #endregion

        #region メソッド
        public override void SetEnabledCtrl(FormState sts)
        {
            base.SetEnabledCtrl(sts);

            try
            {
                switch (sts)
                {
                    case FormState.Busy:
                        cmdSelect.Enabled = false;
                        cboType.Enabled = false;
                        cmdImport.Enabled = false;
                        cmdTranslate.Enabled = false;
                        cmdDictSelect.Enabled = false;
                        break;
                    case FormState.BeforeShow:
                        cmdSelect.Enabled = true;
                        cboType.Enabled = true;
                        cmdImport.Enabled = false;
                        cmdTranslate.Enabled = false;
                        cmdDictSelect.Enabled = true;
                        break;
                    case FormState.AfterShowNoData:
                        cmdSelect.Enabled = true;
                        cboType.Enabled = true;
                        cmdImport.Enabled = true;
                        cmdTranslate.Enabled = false;
                        cmdDictSelect.Enabled = true;
                        break;
                    case FormState.AfterShowSomeData:
                        cmdSelect.Enabled = true;
                        cboType.Enabled = true;
                        cmdImport.Enabled = true;
                        cmdTranslate.Enabled = true;
                        cmdDictSelect.Enabled = true;
                        break;
                    case FormState.RaiseError:
                        cmdSelect.Enabled = false;
                        cboType.Enabled = false;
                        cmdImport.Enabled = false;
                        cmdTranslate.Enabled = false;
                        cmdDictSelect.Enabled = false;
                        break;
                    default:
                        break;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// イベントハンドラ
        /// </summary>
        public override void SetEventHandler()
        {
            base.SetEventHandler();
            try
            {
                cmdTranslate.Click += CmdTranslate_Click;

                cboTransTo.SelectedIndexChanged += SearchCondition_Changed;

                cboCI.Enter += SearchCondition_Enter;
                cboClass.Enter += SearchCondition_Enter;
                txtProjPath.TextChanged += SearchCondition_Enter;

                cboType.SelectedIndexChanged += SearchCondition_Changed;
                cboType.Enter += SearchCondition_Enter;

                grdData.AfterEdit += GrdData_AfterEdit;
            }
            catch (Exception)
            {

                throw;
            }
        }


        /// <summary>
        /// 検索条件のチェック
        /// </summary>
        /// <returns></returns>
        public override bool CheckSearchCondition()
        {
            try
            {
                if (workPaths.Count == 0)
                {
                    MessageBox.Show("プロジェクトを選択してください。");
                    return false;
                }

                fileNames.Clear();
                foreach (string workPath in workPaths)
                {
                    var files = new DirectoryInfo(workPath).GetFiles("*.resx", SearchOption.AllDirectories).ToList();

                    List<string> selectdFiles = new List<string>();

                    if (!files.Where(r => r.Name.Contains(".en") || r.Name.Contains(string.Format(".{0}", cboTransTo.SelectedValue))).Any())
                    {
                        MessageBox.Show(this, "多言語構成を生成するため、Virtual Studioでプロジェクトに多言語を指定して、\r\n二つ言語ファイルを先に生成してください。", "事前ワークチェック", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return false;
                    }
                    fileNames.Add(Path.GetFileNameWithoutExtension(workPath), files);
                }

                if (string.IsNullOrEmpty(lblTransFrom.Text))
                {
                    MessageBox.Show("翻訳元の言語種類が見つかりません。\r\nSettring.xmlファイルを直してください。");
                    return false;
                }


                if (string.IsNullOrEmpty(cboTransTo.Text))
                {
                    MessageBox.Show(this
                                  , YMSL.CS4.FMS.CSCOM.Properties.Resources.SearchForm_Check_Input_Must.Replaces("翻訳先言語種類")
                                  , this.Name
                                  , MessageBoxButtons.OK
                                  , MessageBoxIcon.Warning);
                    return false;
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// グリッドの初期化
        /// </summary>
        public override void InitGrid()
        {
            base.InitGrid();

            grdData.Cols["type"].DataMap = new MultiColumnDictionary("");
            grdData.Cols["type"].DataMap.Add("Text", "文字");
            grdData.Cols["type"].DataMap.Add("Font", "フォント");

            DataTable types = new DataTable();
            types.Columns.Add("type_code");
            types.Columns.Add("type_name");

            DataRow new_row = types.NewRow();
            new_row["type_code"] = "All";
            new_row["type_name"] = "全て";
            types.Rows.Add(new_row);

            new_row = types.NewRow();
            new_row["type_code"] = "Text";
            new_row["type_name"] = "文字";
            types.Rows.Add(new_row);

            new_row = types.NewRow();
            new_row["type_code"] = "Font";
            new_row["type_name"] = "フォント";
            types.Rows.Add(new_row);

            cboType.DataSource = types;
            cboType.ValueMember = "type_code";
            cboType.DisplayMember = "type_name";
        }

        /// <summary>
        /// 検索条件の初期化
        /// </summary>
        public override void InitSearchCondition()
        {
            try
            {
                txtProjPath.Clear();
                lblTransFrom.Text = "";
                cboTransTo.Text = "";
                cboCI.Text = "";
                cboClass.Text = "";

                InitLanguageByXmlFile();

                #region クラスコンボボックスの初期化

                DataTable dtClass = new DataTable();
                dtClass.Columns.Add("key", typeof(int));
                dtClass.Columns.Add("display_member", typeof(string));

                dtClass.Rows.Add((int)0, "リソース");
                dtClass.Rows.Add((int)1, "リソース以外");

                cboClass.DataSource = dtClass;
                cboClass.ValueMember = "key";
                cboClass.DisplayMember = "display_member";
                cboClass.SelectedIndex = -1;

                #endregion

                // XMLファイルの読み込み
                XmlDom _XmlDom = new XmlDom();
                _XmlDom.LoadFromXmlFile(System.Reflection.Assembly.GetExecutingAssembly().Location);
                lblDictPath.Text = _XmlDom.GetFromXml("DICTIONARY_INFO", "PATH");
                txtProjPath.Text = _XmlDom.GetFromXml("PROJECT_INFO", "PATH");

                if (!string.IsNullOrEmpty(txtProjPath.Text))
                {
                    workPaths = txtProjPath.Text.Replace("\n", "").Replace("\r\n", "").Split(';').ToList();
                    workPaths = workPaths.Where(r => r.Trim() != "").Select(r => Path.GetDirectoryName(r)).ToList();
                    InitCboCI();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void InitLanguageByXmlFile()
        {
            try
            {
                string path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\xml\Settings.xml";

                XmlDocument xmlDoc = new XmlDocument();

                //XMLファイルの存在チェック
                if (System.IO.File.Exists(path))
                {
                    //XMLファイルが存在する場合
                    //-XMLファイルの読み込み
                    xmlDoc.Load(path);
                }

                #region 翻訳先情報のデータテーブル初期化

                DataTable dtTransToInfo = new DataTable();

                dtTransToInfo.Columns.Add("CODE", typeof(string));
                dtTransToInfo.Columns.Add("NAME", typeof(string));

                #endregion

                //対象ノードの情報を取得
                XmlNode node = xmlDoc.SelectSingleNode("/ROOT");

                foreach (XmlNode childNode in node.ChildNodes)
                {
                    if (childNode != null)
                    {
                        grdData.Cols.Count++;
                        grdData.Cols[grdData.Cols.Count - 1].Caption = string.Format("{0}値({1})", childNode["NAME"].InnerText, childNode["CODE"].InnerText);
                        grdData.Cols[grdData.Cols.Count - 1].Name = string.Format("{0}_value", childNode["CODE"].InnerText);
                        grdData.Cols[grdData.Cols.Count - 1].TextAlignFixed = TextAlignEnum.CenterCenter;
                        grdData.Cols[grdData.Cols.Count - 1].Width = 300;

                        switch (childNode.Name)
                        {
                            case "TRANSLATE_FROM":
                                lblTransFrom.Text = string.Format("{0} ({1})", childNode["NAME"].InnerText, childNode["CODE"].InnerText);
                                lblTransFrom.Tag = childNode["CODE"].InnerText;
                                break;

                            case "TRANSLATE_TO":
                                DataRow dr = dtTransToInfo.NewRow();
                                dr["CODE"] = Convert.ToString(childNode["CODE"].InnerText);
                                dr["NAME"] = Convert.ToString(string.Format("{0} ({1})", childNode["NAME"].InnerText, childNode["CODE"].InnerText));
                                dtTransToInfo.Rows.Add(dr);
                                break;
                        }
                    }
                }

                cboTransTo.DataSource = dtTransToInfo;
                cboTransTo.ValueMember = "CODE";
                cboTransTo.DisplayMember = "NAME";
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 検索条件を再セットする
        /// </summary>
        /// <param name="sender"></param>
        public override void ReSetSearchCondition(object sender)
        {
            try
            {
                if (sender.Equals(cboTransTo))
                {
                    for (int col = grdData.Cols["resource_key"].Index + 1; col < grdData.Cols.Count; col++)
                    {
                        if (cboTransTo.SelectedValue + "_value" == grdData.Cols[col].Name)
                        {
                            grdData.Cols[col].AllowEditing = true;
                            grdData.Cols[col].StyleNew.BackColor = SystemColors.Window;
                        }
                        else
                        {
                            grdData.Cols[col].AllowEditing = false;
                            grdData.Cols[col].StyleNew.BackColor = Color.Gainsboro;
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void InitCboCI()
        {
            try
            {
                if (string.IsNullOrEmpty(txtProjPath.Text))
                {
                    cboCI.Text = "";
                }
                else
                {
                    var list = txtProjPath.Text.Replace("\n", "").Replace("\r\n", "").Split(';').ToList();

                    var reader = list.GetEnumerator();

                    cboCI.Items.Clear();

                    while(reader.MoveNext())
                    {
                        var proj = reader.Current;
                        if (string.IsNullOrEmpty(proj.Trim())) continue;
                        cboCI.Items.Add(Path.GetFileNameWithoutExtension(proj));
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 検索処理
        /// </summary>
        /// <returns></returns>
        public override int ShowData()
        {
            try
            {
                List<ResourceDto> allWords = new List<ResourceDto>();

                foreach (var ci in fileNames.Keys.Where(r => string.IsNullOrEmpty(cboCI.Text) ? true : r == cboCI.Text))
                {
                    foreach (var filename in fileNames[ci])
                    {
                        if (string.IsNullOrEmpty(cboClass.Text))
                        {
                            if (filename.Name.Contains("Resources"))
                            {
                                allWords.AddRange(GetResource(ci, filename));
                            }
                            else
                            {
                                allWords.AddRange(GetFormResource(ci, filename));
                            }
                        }
                        else
                        {
                            if (filename.Name.Contains("Resources") && (int)cboClass.SelectedValue == 0)
                            {
                                allWords.AddRange(GetResource(ci, filename));
                            }

                            if (!filename.Name.Contains("Resources") && (int)cboClass.SelectedValue == 1)
                            {
                                allWords.AddRange(GetFormResource(ci, filename));
                            }
                        }
                    }
                }

                var gettor = allWords.GetEnumerator();
                var selectedlanguages = cboTransTo.Items.OfType<string>().ToList();
                while (gettor.MoveNext())
                {
                    var current = gettor.Current;

                    string strtype = "Font";
                    if (!current.resource_key.EndsWith(".Font"))
                    {
                        strtype = "Text";
                    }

                    if (Convert.ToString(cboType.SelectedValue) != "All" && Convert.ToString(cboType.SelectedValue) != strtype) continue;

                    var check_exist = (from Row row in grdData.Rows.Cast<Row>()
                                       where row.Index >= grdData.Rows.Fixed &&
                                             Convert.ToString(row["class_name"]) == current.class_name &&
                                             Convert.ToString(row["resource_key"]) == current.resource_key
                                       select row);

                    string language_colname = string.Format("{0}_value", current.language);

                    if (!grdData.Cols.Contains(language_colname)) continue;

                    if (check_exist.Any())
                    {
                        var update_row = check_exist.FirstOrDefault();

                        update_row[language_colname] = current.value;
                    }
                    else
                    {
                        grdData.Rows.Count++;
                        grdData[grdData.Rows.Count - 1, "ci"] = current.ci;
                        grdData[grdData.Rows.Count - 1, "class_name"] = current.class_name;
                        grdData[grdData.Rows.Count - 1, "resource_key"] = current.resource_key;
                        grdData[grdData.Rows.Count - 1, "type"] = strtype;
                        grdData[grdData.Rows.Count - 1, language_colname] = current.value;
                        grdData[grdData.Rows.Count - 1, "is_grdInfo"] = current.is_grid_info;
                    }
                }

                grdData.AutoSizeCols(0, grdData.Cols["resource_key"].Index, 0);
                grdData.AutoSizeRows();
                return grdData.Rows.Count - grdData.Rows.Fixed;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        /// <summary>
        /// Resourcesの取得
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public List<ResourceDto> GetResource(string ci, FileInfo fileinfo)
        {
            try
            {
                List<ResourceDto> wordDtos = new List<ResourceDto>();

                string from_filepath = fileinfo.FullName;

                List<ResXEntry> WordInfos = ResXFile.ReadResource(from_filepath, ResXFile.Option.None);
                string language = GetLanguageByFileName(fileinfo.Name);

                foreach (var word in WordInfos)
                {
                    wordDtos.Add(
                        new ResourceDto()
                        {
                            ci = ci,
                            language = language,
                            file_name = from_filepath,
                            class_name = "Resource",
                            resource_key = word.Id,
                            value = word.Value,
                            is_grid_info = word.IsGridInfo,
                        });
                }

                return wordDtos;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// 画面のリソースファイルの取得処理
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public List<ResourceDto> GetFormResource(string ci, FileInfo fileinfo)
        {
            try
            {
                List<ResourceDto> wordDtos = new List<ResourceDto>();

                string from_filepath = fileinfo.FullName;

                List<ResXEntry> WordInfos = ResXFile.ReadFormResource(from_filepath, ResXFile.Option.None);

                string class_name = GetClassNameByFileName(fileinfo.Name);
                string language = GetLanguageByFileName(fileinfo.Name);

                foreach (var word in WordInfos)
                {
                    wordDtos.Add(
                        new ResourceDto()
                        {
                            ci = ci,
                            language = language,
                            file_name = from_filepath,
                            class_name = class_name,
                            resource_key = word.Id,
                            value = word.Value,
                            is_grid_info = word.IsGridInfo,
                        });
                }

                return wordDtos;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// クラス名の取得
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public string GetClassNameByFileName(string filename)
        {
            List<string> strs = filename.Split('.').ToList();

            return strs[0];
        }

        /// <summary>
        /// ファイル名の取得
        /// </summary>
        /// <param name="classname"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public string GetFileNameByClassNameAndLanguage(string classname, string language)
        {
            if (classname == "Resource") classname = "Resources";
            if (language == lblTransFrom.Tag.ToString())
                return string.Format("{0}.resx", classname);
            else
                return string.Format("{0}.{1}.resx", classname, language);
        }

        /// <summary>
        /// 言語名の取得
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public string GetLanguageByFileName(string filename)
        {
            try
            {
                List<string> strs = filename.Split('.').ToList();
                string language = strs[strs.Count - 2];

                if (strs.Count <= 2)
                    return lblTransFrom.Tag.ToString();
                else
                    return language;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// 辞書よりグリッド情報更新
        /// </summary>
        /// <param name="datatable"></param>
        public void UpdateWordsToGridFromLoadedDic(DataTable datatable)
        {
            try
            {
                var gettor = datatable.AsEnumerable().GetEnumerator();

                while (gettor.MoveNext())
                {
                    var current = gettor.Current;

                    var check_exist = (from Row row in grdData.Rows.Cast<Row>()
                                       where row.Index >= grdData.Rows.Fixed &&
                                             Convert.ToString(row["ci"]).ToLower() == Convert.ToString(current["ci"]).ToLower() &&
                                             Convert.ToString(row["class_name"]).ToLower() == Convert.ToString(current["class_name"]).ToLower() &&
                                             (Convert.ToString(row["resource_key"]).ToLower() == Convert.ToString(current["resource_key"]).ToLower() ||
                                              Convert.ToString(row["resource_key"]).ToLower() == (Convert.ToString(current["resource_key"]) + ".Caption").ToLower())
                                       select row);

                    if (check_exist.Any())
                    {
                        var update_row = check_exist.FirstOrDefault();
                        var targetColName = string.Format("{0}_value", cboTransTo.SelectedValue);
                        if (!string.IsNullOrEmpty(Convert.ToString(current[targetColName])) &&
                            Convert.ToString(update_row[targetColName]) != Convert.ToString(current[targetColName]).Replace("\\r\\n", "\r\n"))
                        {
                            update_row[targetColName] = Convert.ToString(current[targetColName]).Replace("\\r\\n", "\r\n");
                            CellRange rng = grdData.GetCellRange(update_row.Index, grdData.Cols[targetColName].Index);
                            rng.StyleNew.ForeColor = Color.Blue;
                        }
                    }
                    else
                    {
                        //grdData.Rows.Count++;
                        //grdData[grdData.Rows.Count - 1, "class_name"] = Convert.ToString(current[grdData.Cols["class_name"].Caption]);
                        //grdData[grdData.Rows.Count - 1, "resource_key"] = Convert.ToString(current[grdData.Cols["resource_key"].Caption]);
                        //grdData[grdData.Rows.Count - 1, "jp_value"] = Convert.ToString(current[grdData.Cols["jp_value"].Caption]).Replace("\n", "\r\n");
                        //grdData[grdData.Rows.Count - 1, "en_value"] = Convert.ToString(current[grdData.Cols["en_value"].Caption]).Replace("\n", "\r\n");
                        //grdData[grdData.Rows.Count - 1, "zh-CHT_value"] = Convert.ToString(current[grdData.Cols["zh-CHT_value"].Caption]).Replace("\n", "\r\n");
                        //grdData.Rows[grdData.Rows.Count - 1].StyleNew.ForeColor = Color.Blue;
                        continue;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// グリッド情報よりリソースファイル更新
        /// </summary>
        /// <param name="language"></param>
        /// <param name="words"></param>
        public void UpdateResourceByGrid(string language, List<ResourceDto> words)
        {
            try
            {
                var gettor = words.GetEnumerator();

                while (gettor.MoveNext())
                {
                    var current = gettor.Current;

                    string filename = current.file_name;

                    ResXFile.Write(filename, new ResXEntry() { Id = current.resource_key, Value = current.value });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// グリッド情報より画面リソースファイル更新
        /// </summary>
        /// <param name="language"></param>
        /// <param name="words"></param>
        public void UpdateGridInfoByGrid(string language, List<ResourceDto> words)
        {
            try
            {
                var gettor = words.GetEnumerator();

                while (gettor.MoveNext())
                {
                    var current = gettor.Current;

                    string filename = current.file_name;

                    ResXFile.WriteGridInfo(filename, new ResXEntry() { Id = current.resource_key, Value = current.value });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// 辞書ロード処理
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="isSkipFirstRow"></param>
        /// <returns></returns>
        public static DataTable ExcelToDt(string filePath, bool isSkipFirstRow)
        {
            if (!string.IsNullOrEmpty(filePath))
            {
                try
                {
                    DataTable dt = new DataTable();

                    Stream stream = new FileStream(filePath, FileMode.Open);
                    using (stream)
                    {
                        ExcelPackage package = new ExcelPackage(stream);
                        ExcelWorksheet sheet = package.Workbook.Worksheets[2];
                        int startRowIndx = sheet.Dimension.Start.Row + (isSkipFirstRow ? 1 : 0);

                        for (int col = 1; col <= sheet.Dimension.End.Column; col++)
                        {
                            dt.Columns.Add(sheet.Cells[1, col].Value.ToString(), Type.GetType("System.String"));
                        }

                        for (int r = startRowIndx; r <= sheet.Dimension.End.Row; r++)
                        {
                            DataRow dr = dt.NewRow();
                            for (int c = sheet.Dimension.Start.Column; c <= sheet.Dimension.End.Column; c++)
                            {
                                if (sheet.Cells[r, c].Style.Numberformat.Format.IndexOf("yyyy") > -1
                                    && sheet.Cells[r, c].Value != null)
                                {

                                    dr[c - 1] = sheet.Cells[r, c].GetValue<DateTime>();
                                }
                                else
                                    dr[c - 1] = (sheet.Cells[r, c].Value ?? DBNull.Value);
                            }
                            dt.Rows.Add(dr);
                        }
                    }
                    return dt;
                }
                catch (Exception )
                {
                }
            }
            return null;
        }

        #endregion



    }
}
