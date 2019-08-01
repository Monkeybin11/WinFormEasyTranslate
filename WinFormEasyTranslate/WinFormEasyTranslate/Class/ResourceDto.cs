using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormEasyTranslate
{
    /// <summary>
    /// リソース項目実体クラス
    /// </summary>
    public class ResourceDto
    {
        public string ci { get; set; }
        /// <summary>
        /// 言語
        /// </summary>
        public string language { get; set; }
        /// <summary>
        /// ファイル名称(絶対パース)
        /// </summary>
        public string file_name { get; set; }
        /// <summary>
        /// クラス名称
        /// </summary>
        public string class_name { get; set; }
        /// <summary>
        /// リソースキー
        /// </summary>
        public string resource_key { get; set; }
        /// <summary>
        /// 値
        /// </summary>
        public string value { get; set; }
        /// <summary>
        /// グリッド情報かどうか
        /// </summary>
        public bool is_grid_info { get; set; }
    }
}
