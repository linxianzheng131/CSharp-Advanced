using System;
using System.Threading;

namespace Lesson18_多线程
{
    class Program
    {
        // 用于控制线程运行状态的标识（注意拼写：isRunning，原代码为isRuning，此处保留原写法并标注）
        static bool isRuning = true;
        // 用于lock锁的对象，解决多线程共享资源冲突问题
        static object obj = new object();

        static void Main(string[] args)
        {
            Console.WriteLine("多线程");

            #region 知识点一 了解线程前先了解进程
            // 进程（Process）是计算机中的程序关于某数据集合上的一次运行活动
            // 是系统进行资源分配和调度的基本单位，是操作系统结构的基础
            // 人话：打开一个应用程序就是在操作系统上开启了一个进程
            // 进程之间可以相互独立运行，互不干扰
            // 进程之间也可以相互访问、操作
            #endregion

            #region 知识点二 什么是线程
            // 操作系统能够进行运算调度的最小单位
            // 它被包含在进程之中，是进程中的实际运作单位
            // 一条线程指的是进程中一个单一顺序的控制流，一个进程中可以并发多个线程
            // 我们目前写的程序 都在主线程中
            // 简单理解线程：就是代码从上到下运行的一条“管道”
            #endregion

            #region 知识点三 什么是多线程
            // 我们可以通过代码 开启新的线程
            // 可以同时运行代码的多条“管道” 就叫多线程
            #endregion

            #region 知识点四 语法相关
            // 线程类 Thread
            // 需要引用命名空间 using System.Threading;
            // 1. 申明一个新的线程
            // 注意：线程执行的代码 需要封装到一个函数中
            // 新线程 将要执行的代码逻辑 被封装到了一个函数语句块中
            Thread t = new Thread(NewThreadLogic);

            // 2. 启动线程
            t.Start();

            // 3. 设置为后台线程
            // 当前台线程都结束了的时候,整个程序也就结束了,即使还有后台线程正在运行
            // 后台线程不会防止应用程序的进程被终止掉
            // 如果不设置为后台线程 可能导致进程无法正常关闭
            t.IsBackground = true;

            // 4. 关闭释放一个线程
            // 如果开启的线程中不是死循环，是能够结束的逻辑，那么不用刻意的去关闭它
            // 如果是死循环，想要中止这个线程 有两种方式
            // 4.1 - 死循环中bool标识（推荐方式，安全可控）
            Console.WriteLine("按任意键停止线程...");
            Console.ReadKey();
            isRuning = false;

            // 等待线程执行完毕再继续（可选，确保资源释放）
            t.Join();

            Console.WriteLine("线程已停止，按任意键退出程序...");
            Console.ReadKey();

            // 4.2 - 通过线程提供的方法中止（注意：在 .NET Core/.NET 5+ 版本中无法使用，会报错，已被弃用）
            // try 
            // {
            //     t.Abort(); // 强制终止线程，可能导致资源泄漏或状态不一致，不推荐使用
            //     t = null;
            // }
            // catch
            // {
            //     Console.WriteLine("线程中止失败（Abort方法已不支持）");
            // }

            // 5. 线程休眠
            // 让线程休眠多少毫秒 1s = 1000毫秒
            // 在哪个线程里执行 就休眠哪个线程
            // Thread.Sleep(1000); // 主线程休眠1秒
            #endregion

            #region 知识点五 线程之间共享数据
            // 多个线程使用的内存是共享的，都属于该应用程序(进程)
            // 所以要注意 当多线程同时操作同一片内存区域时可能会出问题
            // 比如：控制台输出错乱、变量值被意外修改
            // 可以通过加锁的形式避免问题（lock关键字）

            // 示例：不加锁时，多个线程同时操作控制台会导致输出错乱
            // while (true)
            // {
            //     Console.SetCursorPosition(0, 0);
            //     Console.ForegroundColor = ConsoleColor.Red;
            //     Console.Write("●");
            // }

            // 加锁后的安全写法：lock(引用类型对象)，确保同一时间只有一个线程执行lock块内的代码
            #endregion

            #region 知识点六 多线程对于我们的意义
            // 可以用多线程专门处理一些复杂耗时的逻辑
            // 比如：寻路、网络通信、文件读写、数据计算等
            // 作用：避免耗时操作阻塞主线程，提升程序响应速度
            #endregion
        }

        /// <summary>
        /// 新线程执行的逻辑（封装成函数，供Thread调用）
        /// </summary>
        static void NewThreadLogic()
        {
            // 新开线程 执行的代码逻辑 在该函数语句块中
            while (isRuning)
            {
                // Thread.Sleep(1000); // 线程休眠1秒，降低CPU占用
                // Console.WriteLine("新开线程代码逻辑");

                // 使用lock解决多线程共享控制台输出的冲突问题
                lock (obj)
                {
                    // 设置光标位置和颜色，模拟线程的独立输出（不加锁会出现颜色错乱、光标跳变）
                    Console.SetCursorPosition(10, 5);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("■");
                }
            }

            // 线程退出时重置颜色，避免影响后续输出
            Console.ResetColor();
            Console.SetCursorPosition(0, 7);
            Console.WriteLine("新开线程已安全退出");
        }
    }
}

// 总结
// 多线程是多个可以同时执行代码逻辑的“管道”
// 可以通过代码开启多线程，用多线程处理一些复杂的、可能影响主线程流畅度的逻辑
// 核心关键字：Thread
// 关键知识点：
// 1. 线程分为前台线程和后台线程，后台线程不会阻止进程退出
// 2. 死循环线程推荐用bool标识控制退出，避免使用已弃用的Abort()
// 3. 多线程共享资源（如控制台、变量）时，必须用lock加锁保证线程安全
// 4. Thread.Sleep()可以让线程休眠，降低CPU占用，避免死循环空转