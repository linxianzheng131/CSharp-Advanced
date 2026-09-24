using System;

namespace Lesson15_lambda表达式
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("lambda表达式");

            #region 知识点一 什么是lambda表达式
            // 可以将lambda表达式 理解为 匿名函数的简写
            // 它除了写法不同外
            // 使用上和匿名函数一模一样
            // 都是和委托或者事件 配合使用的
            #endregion

            #region 知识点二 lambda表达式语法
            // 匿名函数 写法
            // delegate (参数列表)
            // {
            //     // 函数逻辑
            // };

            // lambda表达式 写法
            // (参数列表) =>
            // {
            //     // 函数体
            // };
            #endregion

            #region 知识点三 使用
            // 1.无参无返回
            Action a = () =>
            {
                Console.WriteLine("无参无返回值的lambda表达式");
            };
            a();

            // 2.有参（显式指定参数类型）
            Action<int> a2 = (int value) =>
            {
                Console.WriteLine("有参数Lambda表达式:{0}", value);
            };
            a2(100);

            // 3.省略参数类型（参数类型和委托容器一致时可省略）
            Action<int> a3 = (value) =>
            {
                Console.WriteLine("省略参数类型的写法:{0}", value);
            };
            a3(200);

            // 4.有返回值
            Func<string, int> a4 = (value) =>
            {
                Console.WriteLine("有返回值有参数的lambda表达式:{0}", value);
                return 1;
            };
            Console.WriteLine(a4("123123"));

            // 其它传参使用等和匿名函数一样
            // 缺点也是和匿名函数一样的

            Test t = new Test();
            t.DoSomthing();
            #endregion

            #region 知识点四 闭包
            // 内层的函数可以引用包含在它外层的函数的变量
            // 即使外层函数的执行已经终止
            // 注意:
            // 该变量提供的值并非变量创建时的值，而是在父函数范围内的最终值。
            #endregion
        }
    }

    class Test
    {
        public Action action;

        public Test()
        {
            int value = 10;
            // 这里就形成了闭包
            // 因为 当构造函数执行完毕时 其中声明的临时变量value的声明周期被改变了
            action = () =>
            {
                Console.WriteLine(value);
            };

            // 循环中闭包常见问题演示
            for (int i = 0; i < 10; i++)
            {
                // 此index 非彼index：每次循环创建独立变量，避免捕获循环变量的共享状态
                int index = i;
                action += () =>
                {
                    Console.WriteLine(index);
                };
            }
        }

        public void DoSomthing()
        {
            action();
        }
    }

    #region 总结
    // 总结
    // 匿名函数的特殊写法 就是 lambda表达式
    // 固定写法 就是 (参数列表)=>{}
    // 参数列表 可以直接省略参数类型
    // 主要在 委托传递和存储时 为了方便可以直接使用匿名函数或者lambda表达式
    // 缺点: 无法指定移除单个lambda匿名函数，只能整体清空委托链
    #endregion
}