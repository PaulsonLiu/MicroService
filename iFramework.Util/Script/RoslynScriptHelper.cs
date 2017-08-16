using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;

/// <summary>
/// 参考：http://www.cnblogs.com/TianFang/p/6939723.html
/// </summary>
namespace iFramework.Util
{
    public class RoslynScriptHelper
    {
        private Script<object> Script { get; set; }
        private Globals _Globals { get; set; }
        public RoslynScriptHelper(string script, Globals globals)
        {
            _Globals = globals;
            Script = CSharpScript.Create(script,globalsType:typeof(Globals));
            Script.Compile();//编译脚本
        }


        public async Task<object> RunAsync()
        {
            return await Script.RunAsync(_Globals);
        }

        //异常处理
        private async void runScript()
        {
            try
            {
                Console.WriteLine(await CSharpScript.EvaluateAsync("2+2"));
            }
            catch (CompilationErrorException e)
            {
                Console.WriteLine(string.Join(Environment.NewLine, e.Diagnostics));
            }
        }

        //带上下文状态执行
        private async void RunContext()
        {
            var state = await CSharpScript.RunAsync("int x = 1;");
            state = await state.ContinueWithAsync("int y = 2;");
            state = await state.ContinueWithAsync("x+y");
            Console.WriteLine(state.ReturnValue);
        }

        //添加程序集引用
        private async void WithReferences()
        {
            var result = await CSharpScript.EvaluateAsync("System.Net.Dns.GetHostName()",
                                ScriptOptions.Default.WithReferences(typeof(System.Net.Dns).AssemblyQualifiedName));
        }

        //添加using导入
        private async void WithImports()
        {
            var result = await CSharpScript.EvaluateAsync("Sqrt(2)",
                                ScriptOptions.Default.WithImports("System.Math"));
        }

        //和属猪程序中的对象交互
        public async Task<object> EvaluateAsync(string scripts) {
            var globals = new Globals { Version = "1.0.0", Company = "ii" };
            return await CSharpScript.EvaluateAsync(scripts, globals: globals);
        }

    }

    public class Globals
    {
        public string Version;
        public string Company;
    }

}
