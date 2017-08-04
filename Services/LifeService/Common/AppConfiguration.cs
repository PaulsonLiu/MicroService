using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LifeService.Common
{
    public class AppConfiguration
    {
        #region Propertys
        /// <summary>
        /// 文件上传路径
        /// </summary>
        public string FileUpPath { get; set; }
        /// <summary>
        /// 允许文件上传格式
        /// </summary>
        public string AttachExtension { get; set; }
        /// <summary>
        /// 允许上传图片最大值KB
        /// </summary>
        public int AttachImageSize { get; set; }
        /// <summary>
        /// 服务器连接字符串
        /// </summary>
        public string ConnectionString { get; set; }
        #endregion
    }
}
