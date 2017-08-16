using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iFramework.Util
{
    public static class SetClassExtension
    {
        public static void AddRange<TKey, TValue>(this Dictionary<TKey, TValue> Self, Dictionary<TKey, TValue> Values)
        {
            if (Values != null && Self != null)
            {
                foreach (var theItem in Values)
                {
                    Self.Add(theItem.Key, theItem.Value);
                }
            }
        }
        public static Dictionary<TKey, TValue> SetKV<TKey, TValue>(this Dictionary<TKey, TValue> Self, TKey Key, TValue Value)
        {
            if (Self == null)
            {
                Self = new Dictionary<TKey, TValue>();
            }
            if (Self.ContainsKey(Key))
            {
                Self[Key] = Value;
            }
            else
            {
                Self.Add(Key, Value);
            }
            return Self;
        }
        public static TValue GetKV<TKey, TValue>(this Dictionary<TKey, TValue> Self, TKey Key)
        {
            if (Self == null)
            {
                return default(TValue);
            }
            if (Self.ContainsKey(Key))
            {
                return Self[Key];
            }
            return default(TValue);
        }
        
        public static Dictionary<string,List<string>> ToComboList(this string Value, string SplitChar1 = ",", string SplitChar2 = " ")
        {
            var theRet = new Dictionary<string, List<string>>();
            if(!string.IsNullOrEmpty(Value))
            {
                var theList = Value.Split(new string[]{SplitChar1},StringSplitOptions.RemoveEmptyEntries);
                if(theList!=null)
                {
                    foreach(var theL in theList)
                    {
                        var theFields = theL.Split(new string[]{SplitChar2},StringSplitOptions.RemoveEmptyEntries);
                        if(theFields!=null && theFields.Length==2)
                        {
                            if(theRet.ContainsKey(theFields[0]))
                            {
                                theRet[theFields[0]].Add(theFields[1]);
                            }
                            else
                            {
                                theRet.Add(theFields[0], new List<string>() { theFields[1] });
                            }
                        }
                    }
                }
            }
            return theRet;
            
        }
    }
}
