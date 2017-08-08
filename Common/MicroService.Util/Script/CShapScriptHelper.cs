using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MicroService.Util
{
    public class CShapScriptHelper
    {
        #region core不支持
        //private readonly string mainCalssName = "ParseScript";
        //public string ErrorMsg { get; set; }
        //public CompilerResults complierResult { get; set; }
        //public bool HasErrors
        //{
        //    get
        //    {
        //        return complierResult.Errors.HasErrors;
        //    }
        //}
        //public ScriptParseHelper(string script, Dictionary<string, string> golbalVariableDic = null)
        //{
        //    string golbalVariableStr = "";
        //    if (golbalVariableDic != null)
        //    {
        //        foreach (var item in golbalVariableDic)
        //        {
        //            golbalVariableStr += string.Format("public string {0} = \"{1}\";", item.Key, item.Value);
        //        }
        //    }
        //    var source =
        //        @"using System;
        //            public class " + mainCalssName + @"{
        //            " + golbalVariableStr + script + @"
        //        }";
        //    // 1.Create a new CSharpCodePrivoder instance
        //    CSharpCodeProvider objCSharpCodePrivoder = new CSharpCodeProvider();
        //    // 2.Sets the runtime compiling parameters by crating a new CompilerParameters instance
        //    CompilerParameters objCompilerParameters = new CompilerParameters();
        //    objCompilerParameters.ReferencedAssemblies.Add("System.dll");
        //    objCompilerParameters.GenerateInMemory = true;
        //    // 3.CompilerResults: Complile the code snippet by calling a method from the provider
        //    CompilerResults cr = objCSharpCodePrivoder.CompileAssemblyFromSource(objCompilerParameters, source);
        //    if (cr.Errors.HasErrors)
        //    {
        //        string strErrorMsg = cr.Errors.Count.ToString() + " Errors:";
        //        for (int x = 0; x < cr.Errors.Count; x++)
        //        {
        //            strErrorMsg = strErrorMsg + "/r/nLine: " +
        //                cr.Errors[x].Line.ToString() + " - " +
        //                cr.Errors[x].ErrorText;
        //        }
        //        ErrorMsg = strErrorMsg;
        //    }
        //    complierResult = cr;
        //}

        //public object InvokeMethod(string methodName, object[] objCodeParms)
        //{
        //    if (complierResult != null && !this.HasErrors)
        //    {
        //        // 4. Invoke the method by using Reflection
        //        Assembly objAssembly = complierResult.CompiledAssembly;
        //        object objClass = objAssembly.CreateInstance(mainCalssName);
        //        if (objClass == null)
        //        {
        //            ErrorMsg = "Error: " + "Couldn't load class ParseScriptHelper.";
        //            return false;
        //        }
        //        //input parmeters
        //        //object[] objCodeParms = new object[1];
        //        //objCodeParms[0] = "Allan.";
        //        object result = objClass.GetType().InvokeMember(methodName, BindingFlags.InvokeMethod, null, objClass, objCodeParms);
        //        return result;
        //    }
        //    return null;
        //}
        #endregion
    }
}