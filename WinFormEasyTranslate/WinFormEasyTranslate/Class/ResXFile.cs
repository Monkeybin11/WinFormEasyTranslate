using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Drawing;
using C1.Win.C1FlexGrid;
using System.Data;
using System.Collections;
using System.Text.RegularExpressions;

namespace WinFormEasyTranslate
{
    /// <summary>
    /// Resourceファイル操作クラス
    /// </summary>
    public static class ResXFileManager
    {
        #region 読込処理
        /// <summary>
        /// 画面リソースからリソース項目を抽出する
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static List<ResXRecord> ReadFormResource(string filename)
        {
            try
            {
                var result = new List<ResXRecord>();

                var listNodes = GetResourceNodes(filename);

                if (listNodes.Count() == 0) return result;

                var fc = new FontConverter();

                //全てのワードを取得する
                var dict = listNodes.GetEnumerator();

                while (dict.MoveNext())
                {
                    var node = dict.Current;

                    string valuetypename = node.GetValueTypeName((ITypeResolutionService)null);

                    if (valuetypename.Contains("System.Windows.Forms.Label") ||
                        valuetypename.Contains("System.String") ||
                        valuetypename.Contains("System.Drawing.Font"))
                    {
                        if (node.Name.EndsWith(".Text") ||
                           node.Name.EndsWith(".Font") ||
                           node.Name.EndsWith(".StyleInfo") ||
                           node.Name.EndsWith(".ColumnInfo"))
                        {
                            string nodevalue = null;

                            if (node.Name.EndsWith(".Font"))
                            {
                                Font fontValue = node.GetValue((ITypeResolutionService)null) as Font;
                                fc = new FontConverter();
                                nodevalue = fc.ConvertToInvariantString(fontValue);

                                result.Add(
                                    new ResXRecord()
                                    {
                                        Id = node.Name as string,
                                        Value = nodevalue,
                                    });
                            }
                            else if (node.Name.EndsWith(".ColumnInfo"))
                            {
                                nodevalue = node.GetValue((ITypeResolutionService)null) as string;
                                string grdControlName = node.Name.Split('.').ToList().First();
                                C1FlexGrid grdData = GetDummyGrid(listNodes, grdControlName);

                                foreach (Column colInfo in grdData.Cols)
                                {
                                    result.Add(
                                        new ResXRecord()
                                        {
                                            Id = string.Format("{0}.ColumnInfo.{1}.Caption", grdControlName, colInfo.Name),
                                            Value = colInfo.Caption,
                                            IsGridInfo = true,
                                        });

                                    result.Add(
                                        new ResXRecord()
                                        {
                                            Id = string.Format("{0}.ColumnInfo.{1}.Font", grdControlName, colInfo.Name),
                                            Value = colInfo.Style == null ? fc.ConvertToInvariantString(grdData.Styles.Normal.Font) : fc.ConvertToInvariantString(colInfo.Style.Font),
                                            IsGridInfo = true,
                                        });
                                }
                            }
                            else if (node.Name.EndsWith(".StyleInfo"))
                            {
                                nodevalue = node.GetValue((ITypeResolutionService)null) as string;
                                string grdControlName = node.Name.Split('.').ToList().First();
                                C1FlexGrid grdData = GetDummyGrid(listNodes, grdControlName);

                                foreach (CellStyle styleInfo in grdData.Styles)
                                {
                                    result.Add(
                                        new ResXRecord()
                                        {
                                            Id = string.Format("{0}.StyleInfo.{1}.Font", grdControlName, styleInfo.Name),
                                            Value = fc.ConvertToInvariantString(styleInfo.Font),
                                            IsGridInfo = true,
                                        });
                                }
                            }
                            else
                            {
                                nodevalue = node.GetValue((ITypeResolutionService)null) as string;
                                result.Add(
                                new ResXRecord()
                                {
                                    Id = node.Name as string,
                                    Value = nodevalue,
                                });
                            }
                        }
                        else
                        {
                            continue;
                        }
                    }
                    else
                    {
                        continue;
                    }
                }

                return result;
            }
            catch(Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// プロジェクトリソースからリソース項目を抽出する
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static List<ResXRecord> ReadResource(string filename)
        {
            try
            {
                var result = new List<ResXRecord>();
                using (var resx = new ResXResourceReader(filename))
                {
                    resx.UseResXDataNodes = true;
                    var dict = resx.GetEnumerator();
                    while (dict.MoveNext())
                    {
                        var node = dict.Value as ResXDataNode;
                        string nodevalue = null;
                        try
                        {
                            nodevalue = node.GetValue((ITypeResolutionService)null) as string;
                        }
                        catch (Exception)
                        {
                            continue;
                        }

                        result.Add(
                            new ResXRecord()
                            {
                                Id = dict.Key as string,
                                Value = nodevalue,
                            });
                    }

                    resx.Close();
                }

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region 書込み処理
        /// <summary>
        /// リソースファイルの書く処理
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="node"></param>
        public static void WriteResource(string filename, ResXRecord node)
        {
            var resourceEntries = new Hashtable();

            FileInfo inputfile = new FileInfo(filename);
            var reader = new ResXResourceReader(filename);
            if (inputfile.Length != 0)
            {
                foreach (DictionaryEntry d in reader)
                {
                    resourceEntries.Add(d.Key.ToString(), d.Value);
                }
            }

            reader.Close();

            if (resourceEntries.ContainsKey(node.Id))
            {
                if (node.Value.Length != 0)
                {
                    if (node.Id.EndsWith(".Font"))
                    {
                        var fc = new FontConverter();
                        Font f = fc.ConvertFromInvariantString(node.Value.ToString()) as Font;
                        resourceEntries[node.Id] = f;
                    }
                    else
                    {
                        resourceEntries[node.Id] = node.Value;
                    }
                }
                else
                {
                    resourceEntries.Remove(node.Id);
                }
            }
            else if (node.Value.Length != 0)
            {
                if (node.Id.EndsWith(".Font"))
                {
                    var fc = new FontConverter();
                    Font f = fc.ConvertFromInvariantString(node.Value.ToString()) as Font;
                    resourceEntries.Add(node.Id, f);
                }
                else
                {
                    resourceEntries.Add(node.Id, node.Value);
                }
            }

            var resourceWriter = new ResXResourceWriter(filename);
            foreach (String key in resourceEntries.Keys)
            {
                if (key.EndsWith(".Font"))
                {
                    Font f = resourceEntries[key] as Font;
                    resourceWriter.AddResource(key, f);
                }
                else
                {
                    resourceWriter.AddResource(key, resourceEntries[key]);
                }
            }

            resourceWriter.Generate();
            resourceWriter.Close();
        }

        /// <summary>
        /// リソースファイルのグリッド書く処理
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="node"></param>
        public static void WriteGridInfo(string filename, ResXRecord node)
        {
            if (node.Id.Contains(".ColumnInfo"))
            {
                string to_write = string.Format("{0}.ColumnInfo", node.Id.Split('.').First());
                var resourceEntries = new Hashtable();

                FileInfo inputfile = new FileInfo(filename);
                var reader = new ResXResourceReader(filename);
                if (inputfile.Length != 0)
                {
                    foreach (DictionaryEntry d in reader)
                    {
                        resourceEntries.Add(d.Key.ToString(), d.Value);
                    }
                }

                reader.Close();

                C1FlexGrid grdData = new C1FlexGrid();
                var fc = new FontConverter();
                if (resourceEntries.ContainsKey(to_write))
                {
                    SetGridColumnInfoFixNoEditor(grdData, Convert.ToString(resourceEntries[to_write]));
                    string colName = node.Id.Split('.').ToList()[node.Id.Split('.').Count() - 2];
                    if (node.Id.EndsWith(".Caption"))
                        grdData.Cols[colName].Caption = node.Value;
                    else if (node.Id.EndsWith(".Font"))
                    {
                        if (string.IsNullOrEmpty(node.Value))
                            grdData.Cols[colName].Style = null;
                        else
                            grdData.Cols[colName].Style.Font = fc.ConvertFromInvariantString(node.Value) as Font;

                    }
                    resourceEntries[to_write] = grdData.ColumnInfo;
                }
                else
                {
                    FileInfo f = new FileInfo(filename);
                    string default_filename = f.DirectoryName + @"\" + f.Name.Split('.').ToList().First() + "." + f.Name.Split('.').ToList().Last();
                    var reader_default = new ResXResourceReader(default_filename);
                    foreach (DictionaryEntry item in reader_default)
                    {
                        if (item.Key.ToString() == to_write)
                        {
                            SetGridColumnInfoFixNoEditor(grdData, item.Value.ToString());
                            resourceEntries.Add(item.Key.ToString(), grdData.ColumnInfo);
                            string colName = node.Id.Split('.').ToList()[node.Id.Split('.').Count() - 2];
                            if (node.Id.EndsWith(".Caption"))
                                grdData.Cols[colName].Caption = node.Value;
                            else if (node.Id.EndsWith(".Font"))
                            {
                                if (string.IsNullOrEmpty(node.Value))
                                    grdData.Cols[colName].Style = null;
                                else
                                    grdData.Cols[colName].Style.Font = fc.ConvertFromInvariantString(node.Value) as Font;
                            }
                            resourceEntries[to_write] = grdData.ColumnInfo;
                            break;
                        }
                    }
                }

                var resourceWriter = new ResXResourceWriter(filename);
                foreach (String key in resourceEntries.Keys)
                {
                    if (key.EndsWith(".Font"))
                    {
                        fc = new FontConverter();
                        Font f = resourceEntries[key] as Font;
                        resourceWriter.AddResource(key, f);
                    }
                    else
                    {
                        resourceWriter.AddResource(key, resourceEntries[key]);
                    }
                }

                resourceWriter.Generate();
                resourceWriter.Close();
            }
            else if (node.Id.Contains(".StyleInfo"))
            {
                string to_write = string.Format("{0}.StyleInfo", node.Id.Split('.').First());
                var resourceEntries = new Hashtable();

                FileInfo inputfile = new FileInfo(filename);
                var reader = new ResXResourceReader(filename);
                if (inputfile.Length != 0)
                {
                    foreach (DictionaryEntry d in reader)
                    {
                        resourceEntries.Add(d.Key.ToString(), d.Value);
                    }
                }

                reader.Close();

                C1FlexGrid grdData = new C1FlexGrid();
                var fc = new FontConverter();
                if (resourceEntries.ContainsKey(to_write))
                {
                    grdData.StyleInfo = Convert.ToString(resourceEntries[to_write]);
                    string styleName = node.Id.Split('.').ToList()[2];
                    grdData.Styles[styleName].Font = fc.ConvertFromInvariantString(node.Value) as Font;
                    resourceEntries[to_write] = grdData.StyleInfo;
                }
                else
                {
                    FileInfo f = new FileInfo(filename);
                    string default_filename = f.DirectoryName + @"\" + f.Name.Split('.').ToList().First() + "." + f.Name.Split('.').ToList().Last();
                    var reader_default = new ResXResourceReader(default_filename);
                    foreach (DictionaryEntry item in reader_default)
                    {
                        if (item.Key.ToString() == to_write)
                        {
                            grdData.StyleInfo = item.Value.ToString();
                            resourceEntries.Add(item.Key.ToString(), grdData.StyleInfo);
                            string styleName = node.Id.Split('.').ToList()[2];
                            grdData.Styles[styleName].Font = fc.ConvertFromInvariantString(node.Value) as Font;
                            resourceEntries[to_write] = grdData.StyleInfo;
                            break;
                        }
                    }
                }

                var resourceWriter = new ResXResourceWriter(filename);
                foreach (String key in resourceEntries.Keys)
                {
                    if (key.EndsWith(".Font"))
                    {
                        fc = new FontConverter();
                        Font f = resourceEntries[key] as Font;
                        resourceWriter.AddResource(key, f);
                    }
                    else
                    {
                        resourceWriter.AddResource(key, resourceEntries[key]);
                    }
                }

                resourceWriter.Generate();
                resourceWriter.Close();
            }
        }
        #endregion

        #region メソッド
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static List<ResXDataNode> GetResourceNodes(string filename)
        {
            var result = new List<ResXDataNode>();

            using (var resx = new ResXResourceReader(filename))
            {
                FileInfo inputfile = new FileInfo(filename);
                if (inputfile.Length == 0) return result;

                resx.UseResXDataNodes = true;

                //全てのワードを取得する
                var dict = resx.GetEnumerator();
                while (dict.MoveNext())
                {
                    var node = dict.Value as ResXDataNode;

                    result.Add(node);
                }

                resx.Close();
            }

            return result;
        }

        /// <summary>
        /// 文字列からグリッドにプロパティをセットする
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="grdControlName"></param>
        /// <returns></returns>
        public static C1FlexGrid GetDummyGrid(List<ResXDataNode> nodes, string grdControlName)
        {
            C1FlexGrid grdData = null;
            if (nodes.Where(r => r.Name == grdControlName + ".ColumnInfo" ||
                                 r.Name == grdControlName + ".StyleInfo" ||
                                 r.Name == grdControlName + ".Font").Any())
            {
                grdData = new C1FlexGrid();

                var fontWord = nodes.Where(r => r.Name == grdControlName + ".Font").FirstOrDefault();
                if (fontWord != null)
                {
                    Font fontValue = fontWord.GetValue((ITypeResolutionService)null) as Font;
                    grdData.Font = fontValue;
                }

                var columninfoWord = nodes.Where(r => r.Name == grdControlName + ".ColumnInfo").FirstOrDefault();
                if (columninfoWord != null)
                {
                    string columnInfo = columninfoWord.GetValue((ITypeResolutionService)null) as string;
                    SetGridColumnInfoFixNoEditor(grdData, columnInfo);
                }

                var styleinfoWord = nodes.Where(r => r.Name == grdControlName + ".StyleInfo").FirstOrDefault();
                if (styleinfoWord != null)
                {
                    grdData.StyleInfo = styleinfoWord.GetValue((ITypeResolutionService)null) as string;
                }
            }

            return grdData;
        }

        /// <summary>
        /// リソースファイルの書く処理
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="entries"></param>
        /// <param name="options"></param>
        public static void Write(string filename, IEnumerable<ResXRecord> entries)
        {
            using (var resx = new ResXResourceWriter(filename))
            {
                foreach (var entry in entries)
                {
                    var node = new ResXDataNode(entry.Id, entry.Value.Replace("\r", string.Empty).Replace("\n", Environment.NewLine));

                    resx.AddResource(node);
                }

                resx.Close();
            }
        }

        /// <summary>
        /// Editorがある列にEditor消えるバグを修正するため、メソッドを作る
        /// </summary>
        /// <param name="grdData"></param>
        /// <param name="columnInfo"></param>
        private static void SetGridColumnInfoFixNoEditor(C1FlexGrid grdData, string columnInfo)
        {
            grdData.ColumnInfo = columnInfo;

            Dictionary<string, string> EditorDic = GetEditorDic(columnInfo);

            foreach (var colName in EditorDic.Keys)
            {
                grdData.Cols[colName].Editor = new System.Windows.Forms.Control() { Name = EditorDic[colName] };
            }
        }

        /// <summary>
        /// 全てのEditorを取得する
        /// </summary>
        /// <param name="columninfo"></param>
        /// <returns></returns>
        private static Dictionary<string, string> GetEditorDic(string columninfo)
        {
            try
            {
                Dictionary<string, string> editors = new Dictionary<string, string>();

                List<string> colPropertyStrs = columninfo.Split(',').ToList();
                string allPro = "";
                for (int i = 0; i < colPropertyStrs.Count; i++)
                {
                    if (i >= 6)
                    {
                        allPro += colPropertyStrs[i];
                    }
                }

                List<string> colunmStrs = allPro.Split('\t').ToList();

                foreach (var columnStr in colunmStrs)
                {
                    if (columnStr.Trim().Length == 0) continue;

                    List<string> ColProperties = Regex.Match(columnStr, @"\{(.*)\}", RegexOptions.Singleline).Groups[1].Value.Split(';').ToList();
                    if (!ColProperties.Any(r => r.Trim().Length != 0)) continue;
                    string colName = ColProperties.Where(r => r.Split(':').ToList()[0] == "Name").Select(r => r.Split(':').ToList()[1]).FirstOrDefault().Trim('"');
                    foreach (var Colproperty in ColProperties)
                    {
                        string propertyName = Colproperty.Split(':').ToList()[0];
                        if (propertyName == "Editor")
                        {
                            string propertyValue = Colproperty.Split(':').ToList()[1].Trim('"');
                            editors.Add(colName, propertyValue);
                        }
                    }
                }


                return editors;
            }
            catch(Exception ex)
            {
                throw;
            }
        }
        #endregion

    }
}