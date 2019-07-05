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

namespace WinFormEasyTranslate
{
    public partial class WinFormEasyTranslate : SearchForm
    {
        #region 変数
        private string workpath = null;

        private List<FileInfo> fileNames = new List<FileInfo>();

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
                txtProjPath.Text = dlg.FileName;
                FileInfo file = new FileInfo(txtProjPath.Text);
                workpath = file.DirectoryName;
            }
        }

        /// <summary>
        /// 辞書登録
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "辞書を選択してください。";
            dlg.Filter = "辞書ファイル(*.xlsx)|*.xlsx";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                DataTable loadedDictionary = ExcelToDt(dlg.FileName, true);
                if(loadedDictionary == null)
                {
                    MessageBox.Show("辞書ロードエラーが出しました。辞書フォマードを確認してください。");
                    return;
                }
                UpdateWordsToGridFromLoadedDic(loadedDictionary);
            }
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
                FileInfo file = new FileInfo(txtProjPath.Text);

                dlgSave.FileName = string.Format("{0}_辞書", Path.GetFileNameWithoutExtension(file.Name));
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
                string language = ((Button)sender).Tag.ToString();

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
                    if (language == "jp")
                    {
                        value = Convert.ToString(currentRow["jp_value"]);
                    }
                    else if (language == "en")
                    {
                        value = Convert.ToString(currentRow["en_value"]);
                    }
                    else if (language == "zh-CHT")
                    {
                        value = Convert.ToString(currentRow["zh-CHT_value"]);
                    }

                    string file_name = GetFileNameByClassNameAndLanguage(Convert.ToString(currentRow["class_name"]), language);
                    string file_path = null;

                    if (Convert.ToString(currentRow["class_name"]) == "Resources")
                    {
                        file_path = string.Format(workpath + @"\Properties\{0}", file_name);
                    }
                    else
                    {
                        file_path = string.Format(workpath + @"\{0}", file_name);
                    }

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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
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
                        cmdExport.Enabled = false;
                        cmdJpTranslate.Enabled = false;
                        cmdEnTranslate.Enabled = false;
                        cmdzhChtTranslate.Enabled = false;
                        break;
                    case FormState.BeforeShow:
                        cmdSelect.Enabled = true;
                        cboType.Enabled = true;
                        cmdImport.Enabled = false;
                        cmdExport.Enabled = false;
                        cmdJpTranslate.Enabled = false;
                        cmdEnTranslate.Enabled = false;
                        cmdzhChtTranslate.Enabled = false;
                        break;
                    case FormState.AfterShowNoData:
                        cmdSelect.Enabled = true;
                        cboType.Enabled = true;
                        cmdImport.Enabled = true;
                        cmdExport.Enabled = false;
                        cmdJpTranslate.Enabled = false;
                        cmdEnTranslate.Enabled = false;
                        cmdzhChtTranslate.Enabled = false;
                        break;
                    case FormState.AfterShowSomeData:
                        cmdSelect.Enabled = true;
                        cboType.Enabled = true;
                        cmdImport.Enabled = true;
                        cmdExport.Enabled = true;
                        cmdJpTranslate.Enabled = true;
                        cmdEnTranslate.Enabled = true;
                        cmdzhChtTranslate.Enabled = true;
                        break;
                    case FormState.RaiseError:
                        cmdSelect.Enabled = false;
                        cboType.Enabled = false;
                        cmdImport.Enabled = false;
                        cmdExport.Enabled = false;
                        cmdJpTranslate.Enabled = false;
                        cmdEnTranslate.Enabled = false;
                        cmdzhChtTranslate.Enabled = false;
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
                cmdJpTranslate.Click += CmdTranslate_Click;
                cmdEnTranslate.Click += CmdTranslate_Click;
                cmdzhChtTranslate.Click += CmdTranslate_Click;

                txtProjPath.TextChanged += SearchCondition_Changed;

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
                if (workpath == null)
                {
                    MessageBox.Show("プロジェクトを選択してください。");
                    return false;
                }

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

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
        /// 検索処理
        /// </summary>
        /// <returns></returns>
        public override int ShowData()
        {
            try
            {
                fileNames = new DirectoryInfo(workpath).GetFiles("*.resx", SearchOption.AllDirectories).ToList();

                List<ResourceDto> allWords = new List<ResourceDto>();

                foreach (var filename in fileNames)
                {
                    if(filename.Name.Contains("Resources"))
                    {
                        allWords.AddRange(GetResource(filename.Name));
                    }
                    else
                    {
                        allWords.AddRange(GetFormResource(filename.Name));
                    }
                }

                var gettor = allWords.GetEnumerator();

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

                    string language_colname = null;
                    if (current.language == "jp")
                    {
                        language_colname = "jp_value";
                    }

                    if (current.language == "en")
                    {
                        language_colname = "en_value";
                    }

                    if (current.language == "zh-CHT")
                    {
                        language_colname = "zh-CHT_value";
                    }

                    if (check_exist.Any())
                    {
                        var update_row = check_exist.FirstOrDefault();

                        update_row[language_colname] = current.value;
                    }
                    else
                    {
                        grdData.Rows.Count++;
                        grdData[grdData.Rows.Count - 1, "class_name"] = current.class_name;
                        grdData[grdData.Rows.Count - 1, "resource_key"] = current.resource_key;
                        grdData[grdData.Rows.Count - 1, "type"] = strtype;
                        grdData[grdData.Rows.Count - 1, language_colname] = current.value;
                        grdData[grdData.Rows.Count - 1, "is_grdInfo"] = current.is_grid_info;
                    }
                }

                grdData.AutoSizeCols();
                grdData.AutoSizeRows();
                return grdData.Rows.Count - grdData.Rows.Fixed;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Resourcesの取得
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public List<ResourceDto> GetResource(string filename)
        {
            try
            {
                List<ResourceDto> wordDtos = new List<ResourceDto>();

                string from_filepath = string.Format(@"{0}\Properties\{1}", workpath, filename);

                List<ResXEntry> WordInfos = ResXFile.ReadResource(from_filepath, ResXFile.Option.None);
                string language = GetLanguageByFileName(filename);

                foreach (var word in WordInfos)
                {
                    wordDtos.Add(
                        new ResourceDto()
                        {
                            language = language,
                            file_name = from_filepath,
                            class_name = GetClassNameByFileName(filename),
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
        public List<ResourceDto> GetFormResource(string filename)
        {
            try
            {
                List<ResourceDto> wordDtos = new List<ResourceDto>();

                string from_filepath = string.Format(@"{0}\{1}", workpath, filename);

                List<ResXEntry> WordInfos = ResXFile.ReadFormResource(from_filepath, ResXFile.Option.None);

                string class_name = GetClassNameByFileName(filename);
                string language = GetLanguageByFileName(filename);

                foreach (var word in WordInfos)
                {
                    wordDtos.Add(
                        new ResourceDto()
                        {
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
            if (language == "jp")
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

                if (language == "en" || language == "zh-CHT")
                    return language;
                else
                    return "jp";
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
                var gettor = datatable.AsEnumerable().Where(r => cboType.Text == "全て" || Convert.ToString(r["種類"]) == cboType.Text).GetEnumerator();

                while (gettor.MoveNext())
                {
                    var current = gettor.Current;

                    var check_exist = (from Row row in grdData.Rows.Cast<Row>()
                                       where row.Index >= grdData.Rows.Fixed &&
                                             Convert.ToString(row["class_name"]) == Convert.ToString(current[grdData.Cols["class_name"].Caption]) &&
                                             Convert.ToString(row["resource_key"]) == Convert.ToString(current[grdData.Cols["resource_key"].Caption])
                                       select row);

                    if (check_exist.Any())
                    {
                        var update_row = check_exist.FirstOrDefault();
                        if (!string.IsNullOrEmpty(Convert.ToString(current[grdData.Cols["jp_value"].Caption])) &&
                            Convert.ToString(update_row["jp_value"]) != Convert.ToString(current[grdData.Cols["jp_value"].Caption]).Replace("\n", "\r\n"))
                        {
                            update_row["jp_value"] = Convert.ToString(current[grdData.Cols["jp_value"].Caption]).Replace("\n", "\r\n");
                            CellRange rng = grdData.GetCellRange(update_row.Index, grdData.Cols["jp_value"].Index);
                            rng.StyleNew.ForeColor = Color.Blue;
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(current[grdData.Cols["en_value"].Caption])) &&
                            Convert.ToString(update_row["en_value"]) != Convert.ToString(current[grdData.Cols["en_value"].Caption]).Replace("\n", "\r\n"))
                        {
                            update_row["en_value"] = Convert.ToString(current[grdData.Cols["en_value"].Caption]).Replace("\n", "\r\n");
                            CellRange rng = grdData.GetCellRange(update_row.Index, grdData.Cols["en_value"].Index);
                            rng.StyleNew.ForeColor = Color.Blue;
                        }

                        if (!string.IsNullOrEmpty(Convert.ToString(current[grdData.Cols["zh-CHT_value"].Caption])) &&
                            Convert.ToString(update_row["zh-CHT_value"]) != Convert.ToString(current[grdData.Cols["zh-CHT_value"].Caption]).Replace("\n", "\r\n"))
                        {
                            update_row["zh-CHT_value"] = Convert.ToString(current[grdData.Cols["zh-CHT_value"].Caption]).Replace("\n", "\r\n");
                            CellRange rng = grdData.GetCellRange(update_row.Index, grdData.Cols["zh-CHT_value"].Index);
                            rng.StyleNew.ForeColor = Color.Blue;
                        }
                    }
                    else
                    {
                        grdData.Rows.Count++;
                        grdData[grdData.Rows.Count - 1, "class_name"] = Convert.ToString(current[grdData.Cols["class_name"].Caption]);
                        grdData[grdData.Rows.Count - 1, "resource_key"] = Convert.ToString(current[grdData.Cols["resource_key"].Caption]);
                        grdData[grdData.Rows.Count - 1, "jp_value"] = Convert.ToString(current[grdData.Cols["jp_value"].Caption]).Replace("\n", "\r\n");
                        grdData[grdData.Rows.Count - 1, "en_value"] = Convert.ToString(current[grdData.Cols["en_value"].Caption]).Replace("\n", "\r\n");
                        grdData[grdData.Rows.Count - 1, "zh-CHT_value"] = Convert.ToString(current[grdData.Cols["zh-CHT_value"].Caption]).Replace("\n", "\r\n");
                        grdData.Rows[grdData.Rows.Count - 1].StyleNew.ForeColor = Color.Blue;
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
                        ExcelWorksheet sheet = package.Workbook.Worksheets[1];
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
