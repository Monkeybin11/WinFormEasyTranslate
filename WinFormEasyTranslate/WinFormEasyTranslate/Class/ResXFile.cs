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

namespace WinFormEasyTranslate
{
    /// <summary>
    /// Resourceファイル操作クラス
    /// </summary>
    public static class ResXFile
    {
        /// <summary>
        /// 选项
        /// </summary>
        [Flags]
        public enum Option
        {
            /// <summary>
            /// 全部
            /// </summary>
            None = 0,

            /// <summary>
            /// 跳过comment
            /// </summary>
            SkipComments = 1,
        }

        /// <summary>
        /// 画面リソースからリソース項目を抽出する
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static List<ResXEntry> ReadFormResource(string filename, Option options = Option.None)
        {
            var result = new List<ResXEntry>();

            var listNodes = GetResourceNodes(filename, options);

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
                                new ResXEntry()
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
                                    new ResXEntry()
                                    {
                                        Id = string.Format("{0}.Columns[{1}].Caption", grdControlName, colInfo.Index),
                                        Value = colInfo.Caption,
                                        IsGridInfo = true,
                                    });

                                result.Add(
                                    new ResXEntry()
                                    {
                                        Id = string.Format("{0}.Columns[{1}].Font", grdControlName, colInfo.Index),
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
                                    new ResXEntry()
                                    {
                                        Id = string.Format("{0}.Styles.{1}.Font", grdControlName, styleInfo.Name),
                                        Value = fc.ConvertToInvariantString(styleInfo.Font),
                                        IsGridInfo = true,
                                    });
                            }
                        }
                        else
                        {
                            nodevalue = node.GetValue((ITypeResolutionService)null) as string;
                            result.Add(
                            new ResXEntry()
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

        public static List<ResXDataNode> GetResourceNodes(string filename, Option options = Option.None)
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

        public static C1FlexGrid GetDummyGrid(List<ResXDataNode> nodes, string grdControlName)
        {
            C1FlexGrid grdData = null;
            if(nodes.Where(r => r.Name == grdControlName + ".ColumnInfo" ||
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
                if(columninfoWord != null)
                {
                    grdData.ColumnInfo = columninfoWord.GetValue((ITypeResolutionService)null) as string;
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
        /// プロジェクトリソースからリソース項目を抽出する
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static List<ResXEntry> ReadResource(string filename, Option options = Option.None)
        {
            var result = new List<ResXEntry>();
            using (var resx = new ResXResourceReader(filename))
            {
                resx.UseResXDataNodes = true;
                var dict = resx.GetEnumerator();
                while (dict.MoveNext())
                {
                    var node = dict.Value as ResXDataNode;
                    string nodevalue = node.GetValue((ITypeResolutionService)null) as string;

                    result.Add(
                        new ResXEntry()
                        {
                            Id = dict.Key as string,
                            Value = nodevalue,
                        });
                }

                resx.Close();
            }

            return result;
        }

        /// <summary>
        /// リソースファイルの書く処理
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="entries"></param>
        /// <param name="options"></param>
        public static void Write(string filename, IEnumerable<ResXEntry> entries, Option options = Option.None)
        {
            using (var resx = new ResXResourceWriter(filename))
            {
                foreach (var entry in entries)
                {
                    var node = new ResXDataNode(entry.Id, entry.Value.Replace("\r", string.Empty).Replace("\n", Environment.NewLine));

                    if (!options.HasFlag(Option.SkipComments) && !string.IsNullOrWhiteSpace(entry.Comment))
                    {
                        node.Comment = entry.Comment.Replace("\r", string.Empty).Replace("\n", Environment.NewLine);
                    }

                    resx.AddResource(node);
                }

                resx.Close();
            }
        }

        /// <summary>
        /// リソースファイルの書く処理
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="node"></param>
        public static void Write(string filename, ResXEntry node)
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
        public static void WriteGridInfo(string filename, ResXEntry node)
        {
            if (node.Id.Contains(".Columns"))
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
                    grdData.ColumnInfo = Convert.ToString(resourceEntries[to_write]);
                    int colIndex = Convert.ToInt32(node.Id.Split('.').ToList()[1].Replace("Columns", "").Replace("[", "").Replace("]", ""));
                    if (node.Id.EndsWith(".Caption"))
                        grdData.Cols[colIndex].Caption = node.Value;
                    else if (node.Id.EndsWith(".Font"))
                        grdData.Cols[colIndex].Style.Font = fc.ConvertFromInvariantString(node.Value) as Font;
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
                            grdData.ColumnInfo = item.Value.ToString();
                            resourceEntries.Add(item.Key.ToString(), grdData.ColumnInfo);
                            int colIndex = Convert.ToInt32(node.Id.Split('.').ToList()[1].Replace("Columns", "").Replace("[", "").Replace("]", ""));
                            if (node.Id.EndsWith(".Caption"))
                                grdData.Cols[colIndex].Caption = node.Value;
                            else if (node.Id.EndsWith(".Font"))
                                grdData.Cols[colIndex].Style.Font = fc.ConvertFromInvariantString(node.Value) as Font;
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
            else if(node.Id.Contains(".Styles"))
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

        /// <summary>
        /// Generates a public C# designer class.
        /// </summary>
        /// <param name="resXFile">The source resx file.</param>
        /// <param name="className">The base class name.</param>
        /// <param name="namespaceName">The namespace for the generated code.</param>
        /// <returns>false if generation of at least one property couldn't be generated.</returns>
        public static bool GenerateDesignerFile(string resXFile, string className, string namespaceName)
        {
            return GenerateDesignerFile(resXFile, className, namespaceName, false);
        }

        /// <summary>
        /// Generates a C# designer class.
        /// </summary>
        /// <param name="resXFile">The source resx file.</param>
        /// <param name="className">The base class name.</param>
        /// <param name="namespaceName">The namespace for the generated code.</param>
        /// <param name="internalClass">Specifies if the class has internal or public access level.</param>
        /// <returns>false if generation of at least one property failed.</returns>
        public static bool GenerateDesignerFile(string resXFile, string className, string namespaceName, bool internalClass)
        {
            if (!File.Exists(resXFile))
            {
                throw new FileNotFoundException($"The file '{resXFile}' could not be found");
            }

            if (string.IsNullOrEmpty(className))
            {
                throw new ArgumentException($"The class name must not be empty or null");
            }

            if (string.IsNullOrEmpty(namespaceName))
            {
                throw new ArgumentException($"The namespace name must not be empty or null");
            }

            string[] unmatchedElements;
            var codeProvider = new Microsoft.CSharp.CSharpCodeProvider();
            System.CodeDom.CodeCompileUnit code =
                System.Resources.Tools.StronglyTypedResourceBuilder.Create(
                    resXFile,
                    className,
                    namespaceName,
                    codeProvider,
                    internalClass,
                    out unmatchedElements);

            var designerFileName = Path.Combine(Path.GetDirectoryName(resXFile), $"{className}.Designer.cs");
            using (StreamWriter writer = new StreamWriter(designerFileName, false, System.Text.Encoding.UTF8))
            {
                codeProvider.GenerateCodeFromCompileUnit(code, writer, new System.CodeDom.Compiler.CodeGeneratorOptions());
            }

            return unmatchedElements.Length == 0;
        }
    }
}