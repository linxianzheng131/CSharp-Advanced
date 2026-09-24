using System;
using System.Collections.Generic;



namespace DelegateMultipleReturnValuesDemo
{
    class Program
    {
        #region 方式1：遍历GetInvocationList获取所有返回值（核心方案）
        // 定义有返回值的委托（匹配int参数、int返回值）
        public delegate int CalculateDelegate(int x, int y);

        static void Main(string[] args)
        {
            // 初始化委托并绑定多个方法
            CalculateDelegate calcDel = Add;
            calcDel += Subtract; // 追加减法方法
            calcDel += Multiply; // 追加乘法方法

            // 调用委托时，默认只获取最后一个方法的返回值
            int defaultResult = calcDel(10, 5);
            Console.WriteLine($"默认调用获取的最后一个返回值: {defaultResult}\n");

            // 核心操作：通过GetInvocationList获取所有委托实例，逐个调用并收集返回值
            List<int> allResults = GetAllDelegateResults(calcDel, 10, 5);

            // 输出所有返回值
            Console.WriteLine("===== 所有方法的返回值结果 =====");
            for (int i = 0; i < allResults.Count; i++)
            {
                Console.WriteLine($"第{i + 1}个方法结果: {allResults[i]}");
            }

            Console.ReadLine();
        }

        /// <summary>
        /// 遍历委托调用列表，逐个执行并收集所有返回值
        /// </summary>
        /// <param name="multiDelegate">多播委托实例</param>
        /// <param name="x">参数x</param>
        /// <param name="y">参数y</param>
        /// <returns>所有方法的返回值列表</returns>
        private static List<int> GetAllDelegateResults(CalculateDelegate multiDelegate, int x, int y)
        {
            List<int> results = new List<int>();
            if (multiDelegate == null) return results;

            // 获取委托调用列表（返回委托数组，顺序为方法添加顺序）
            Delegate[] delegateArray = multiDelegate.GetInvocationList();

            // 遍历每个委托实例，逐个执行并收集结果
            foreach (CalculateDelegate del in delegateArray)
            {
                int result = del(x, y);
                results.Add(result);
            }

            return results;
        }

        // 测试方法1：加法
        private static int Add(int x, int y)
        {
            Console.WriteLine($"执行加法方法: {x} + {y}");
            return x + y;
        }

        // 测试方法2：减法
        private static int Subtract(int x, int y)
        {
            Console.WriteLine($"执行减法方法: {x} - {y}");
            return x - y;
        }

        // 测试方法3：乘法
        private static int Multiply(int x, int y)
        {
            Console.WriteLine($"执行乘法方法: {x} * {y}");
            return x * y;
        }
        #endregion
    }
}