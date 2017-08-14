using System.ComponentModel.DataAnnotations;

namespace iFramework.Util
{
    ///<summary>
    /// 中文
    /// </summary>
    public class ChineseAttribute : RegularExpressionAttribute
    {
        private const string RegexPattern = @"^[\u4e00-\u9fa5]*$";
        public ChineseAttribute()
            : base(RegexPattern)
        {
            ErrorMessage = "请输入中文";
        }
    }
}
