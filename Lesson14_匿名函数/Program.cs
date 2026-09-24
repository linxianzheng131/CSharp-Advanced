using System;

namespace Lesson14_匿名函数
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("匿名函数");

            #region 知识点一 什么是匿名函数
            // 顾名思义，就是没有名字的函数
            // 匿名函数的使用主要是配合委托和事件进行使用
            // 脱离委托和事件 是不会使用匿名函数的
            #endregion

            #region 知识点二 基本语法
            // delegate (参数列表)
            // {
            //     //函数逻辑
            // };
            // 何时使用?
            // 1.函数中传递委托参数时
            // 2.委托或事件赋值时
            #endregion

            #region 知识点三 使用
            // 1.无参无返回
            // 这样申明匿名函数 只是在申明函数而已 还没有调用
            // 真正调用它的时候 是这个委托容器啥时候调用 就啥时候调用这个匿名函数
            Action a = delegate ()
            {
                Console.WriteLine("匿名函数逻辑");
            };
            // 执行匿名函数
            a();

            // 2.有参
            // 定义有参匿名函数（配合Action泛型委托）
            Action<int, string> b = delegate (int a, string b)
            {
                Console.WriteLine(a);
                Console.WriteLine(b);
            };
            // 执行有参匿名函数
            b(100, "123");

            // 3.有返回值
            // 定义有返回值匿名函数（配合Func泛型委托）
            Func<string> c = delegate ()
            {
                return "123123";
            };
            // 执行并输出返回值
            Console.WriteLine(c());

            // 4.一般情况会作为函数参数传递 或者 作为函数返回值
            // 参数传递
            Test t = new Test();
            t.Dosomthing(100, delegate ()
            {
                Console.WriteLine("随参数传入的匿名函数逻辑");
            });

            // 返回值
            // 方式1：先接收委托返回值，再执行
            Action ac2 = t.GetFun();
            ac2();
            // 方式2：一步到位 直接调用返回的 委托函数
            t.GetFun()();
            #endregion

            #region 知识点四 匿名函数的缺点
            // 添加到委托或事件容器中后 不记录 无法单独移除
            Action ac3 = delegate ()
            {
                Console.WriteLine("匿名函数一");
            };
            // 追加第二个匿名函数
            ac3 += delegate ()
            {
                Console.WriteLine("匿名函数二");
            };
            // 执行所有委托链中的函数
            ac3();

            // 因为匿名函数没有名字 所以没有办法指定移除某一个匿名函数
            // 此匿名函数 非彼匿名函数 不能通过看逻辑是否一样 就证明是一个
            // 无法单独移除指定匿名函数的示例（注释版）
            //ac3 -= delegate ()
            //{
            //    Console.WriteLine("匿名函数一");
            //};

            // 只能整体清空委托链（赋值为null）
            ac3 = null;
            // 执行后无输出（委托链已清空）
            //ac3();
            #endregion
        }
    }

    /// <summary>
    /// 测试类：用于演示匿名函数作为参数/返回值的使用场景
    /// </summary>
    class Test
    {
        public Action action;

        // 作为参数传递时
        public void Dosomthing(int a, Action fun)
        {
            Console.WriteLine(a);
            fun();
        }

        // 作为返回值
        public Action GetFun()
        {
            // 直接返回匿名函数（无需额外命名方法）
            return delegate ()
            {
                Console.WriteLine("函数内部返回的一个匿名函数逻辑");
            };
        }

        // 备用命名方法（原代码中定义，可被GetFun引用）
        public void TestTTTT()
        {
            // 可补充具体逻辑
        }


        // 备用静态方法（原代码中定义）
        static void TestFun()
        {

            // 可补充具体逻辑

        }
    }
}

#region 总结
// 匿名函数 就是没有名字的函数
// 固定写法
// delegate(参数列表){}
// 主要是在 委托传递和存储时 为了方便可以直接使用匿名函数
// 缺点是 没有办法指定移除单个匿名函数，只能清空整个委托链
#endregion