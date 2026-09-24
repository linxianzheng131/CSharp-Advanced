using System;

namespace DelegatePrintDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("===== 闭包打印1~10 演示 =====");
            Print1To10Demo.Run();
        }
    }

    #region 题目：有一个函数，会返回一个委托函数，这个委托函数中只有一句打印代码；之后执行返回的委托函数时，可以打印出1~10
    /// <summary>
    /// 核心知识点：
    /// 1. 闭包特性：Lambda 表达式会捕获外层函数的局部变量，并且保留其状态
    /// 2. 高阶函数：一个函数返回另一个函数（委托）
    /// 3. 变量捕获：被捕获的变量不会随着外层函数执行结束而消失，会一直被保留
    /// </summary>
    public static class Print1To10Demo
    {
        public static void Run()
        { 
            // 1. 调用外层函数，得到一个被“记住了计数器”的委托
            Action printAction = null;

            // 2. 连续执行10次这个委托，就能打印出1~10
            for (int i = 0; i < 10; i++)
            { 
                int temp = i;
                printAction += ()=>
                {
                   // 这里的 i 是被捕获的变量，每次循环都会更新它的值
                    // 当 printAction 被调用时，会使用当时 i 的值
                    Console.WriteLine(temp + 1); // 打印 i+1，输出 1~10
                };
            }
            printAction?.Invoke();
        }
    }
}



    #endregion
