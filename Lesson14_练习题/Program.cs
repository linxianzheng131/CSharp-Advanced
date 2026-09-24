using System;

namespace ClosureMultiplierDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("===== 闭包乘法器演示 =====");
            ClosureDemo.Run();
        }
    }

    #region 题目：写一个函数传入一个整数，返回一个函数；执行返回的匿名函数时，传入的整数和之前的数相乘并返回结果
    /// <summary>
    /// 核心知识点：
    /// 1. 闭包：内层匿名函数可以“捕获”外层函数的变量，即使外层函数执行结束，变量依然被保留
    /// 2. 高阶函数：一个函数可以接收/返回另一个函数
    /// 3. Lambda 表达式：用来创建匿名函数，实现闭包逻辑
    /// </summary>
    public static class ClosureDemo
    {
        public static void Run()
        {
            // 1. 调用外层函数，传入整数 5，得到一个“乘以5”的函数
            Func<int, int> multiplyBy5 = CreateMultiplier(5);

            // 2. 调用返回的匿名函数，传入整数 10，和之前的 5 相乘
            int result = multiplyBy5(10);

            // 输出结果：10 * 5 = 50
            Console.WriteLine($"调用 CreateMultiplier(5) 得到的函数，传入 10 后结果为：{result}");
        }

        /// <summary>
        /// 外层函数：接收一个整数，返回一个新的函数
        /// </summary>
        /// <param name="n">要被记住的整数</param>
        /// <returns>接收一个整数，和 n 相乘后返回结果的函数</returns>
        public static Func<int, int> CreateMultiplier(int n)
        {
            // 这里的 Lambda 表达式就是匿名函数，它“捕获”了外层函数的变量 n
            // 即使 CreateMultiplier 执行结束，n 依然被这个匿名函数记住
            return (x) =>
            {
                Console.WriteLine($"匿名函数执行：传入 {x}，和之前的 {n} 相乘");
                return x * n;
            };
        }
    }
    #endregion
}