using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MicroService.Models
{
    /// <summary>
    /// 可以为空的字典
    /// </summary>
    [Serializable]
    public class NullableDictionary:Dictionary<string,string>
    {
        public new string this[string key]
        {
            get
            {
                if (base.ContainsKey(key))
                {
                    return base[key];
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (base.ContainsKey(key))
                {
                    base[key] = value;
                }
                else
                {
                    base.Add(key, value);
                }
            }
        }
        
    }
}
