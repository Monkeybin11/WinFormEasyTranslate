﻿using System;

namespace WinFormEasyTranslate
{
    /// <summary>
    /// リソース項目文字化標準クラス
    /// </summary>
    public class ResXEntry : IComparable
    {
        /// <summary>
        /// リソースID
        /// </summary>
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// 値
        /// </summary>
        public string Value { get; set; } = string.Empty;
        /// <summary>
        /// コメント
        /// </summary>
        public string Comment { get; set; } = string.Empty;

        /// <summary>
        /// グリッド情報かどうか
        /// </summary>
        public bool IsGridInfo { get; set; } = false;

        public int CompareTo(object obj)
        {
            if (obj is ResXEntry)
            {
                return this.Id.CompareTo((obj as ResXEntry).Id);
            }

            return this.Id.CompareTo(obj.ToString());
        }
    }
}