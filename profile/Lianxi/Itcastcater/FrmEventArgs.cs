using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Itcastcater
{
    /// <summary>
    /// 窗口传值用的类
    /// </summary>
    public class FrmEventArgs:EventArgs
    {
        /// <summary>
        /// 标识
        /// </summary>
        public int Temp { get; set; }
        /// <summary>
        /// 对象
        /// </summary>
        public object obj { get; set; }
        /// <summary>
        /// 存钱
        /// </summary>
        public decimal Money { get; set; }
        /// <summary>
        /// 名字
        /// </summary>
        public string Name { get; set; }
    }
}
