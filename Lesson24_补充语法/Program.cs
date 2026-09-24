using System;
using System.Threading;

namespace SwitchWhenDemo
{

    #region readonly
    // ======================================
    // 知识点1：readonly 字段的基本定义
    // 定义：readonly 只能在【声明时】或【构造函数中】赋值
    // 赋值后，运行时不能再修改
    // ======================================
    class ReadOnlyDemo
    {
        // 1. 在声明时直接赋值的 readonly 字段
        private readonly int _fixedValue = 100;

        // 2. 声明时不赋值，在构造函数中初始化的 readonly 字段
        private readonly string _runtimeInitStr;

        // 3. readonly 引用类型字段：只能修改引用本身，不能阻止对象内部修改
        private readonly int[] _fixedArray = { 1, 2, 3 };


        public ReadOnlyDemo(string initStr)
        {
            // ✅ 合法：构造函数中给 readonly 字段赋值
            _runtimeInitStr = initStr;

            // ❌ 错误：声明时已经赋值的 readonly 字段，不能在构造函数里二次赋值
            // _fixedValue = 200;
        }


        public void DemoMethod()
        {
            Console.WriteLine($"_fixedValue: {_fixedValue}");
            Console.WriteLine($"_runtimeInitStr: {_runtimeInitStr}");

            // ✅ 合法：readonly 数组的元素可以修改（引用不变，对象内容可变）
            _fixedArray[0] = 99;
            Console.WriteLine($"修改后的数组：{string.Join(", ", _fixedArray)}");

            // ❌ 错误：不能修改 readonly 字段的引用（不能让它指向新对象）
            // _fixedArray = new int[] { 4, 5, 6 };

            // ❌ 错误：不能在方法中修改 readonly 字段的值
            // _fixedValue = 300;
            // _runtimeInitStr = "new value";
        }


        // ======================================
        // 知识点2：readonly 与 const 的区别
        // const：编译时常量，隐式 static，声明时必须赋值
        // readonly：运行时常量，可实例级/静态级，可在构造函数赋值
        // ======================================
        private const int ConstValue = 10; // 编译时常量 只能用常量赋值
        private readonly int _readonlyValue; // 运行时常量

        public ReadOnlyDemo(int value)
        {
            _readonlyValue = value; // 运行时才确定值
        }

        public void PrintValues()
        {
            Console.WriteLine($"const 值：{ConstValue}");
            Console.WriteLine($"readonly 值：{_readonlyValue}");
        }


        // ======================================
        // 知识点3：静态 readonly 字段
        // 静态 readonly 只能在【静态构造函数】中赋值
        // 属于类级别，所有实例共享
        // ======================================
        private static readonly double Pi;

        static ReadOnlyDemo()
        {
            // ✅ 合法：静态构造函数中给静态 readonly 赋值
            Pi = 3.1415926;

            // ❌ 错误：不能在实例构造函数中修改静态 readonly
            // Pi = 3.14;
        }


        // ======================================
        // 知识点4：readonly 与多线程
        // readonly 字段一旦初始化完成，引用/值不会再变
        // 多线程下读取是安全的，无需额外锁
        // ======================================
        private readonly object _lockObj = new object();

        public void ThreadSafeOperation()
        {
            // 多线程中使用 readonly 锁对象，保证锁对象不会被修改
            lock (_lockObj)
            {
                // 线程安全的操作
            }
        }
    }

    #endregion

    class Program
    {
        // 模拟贪吃蛇方向变量
        private static int _direction = 1;

        static void Main(string[] args)
        {
            ConsoleKey key = ConsoleKey.UpArrow;

            // 静默读取按键，不打印到控制台
            var keyInfo = Console.ReadKey(true);

            #region 知识点：switch case when 语法详解
            // 【核心知识点：case when】
            // 1. case 先匹配：switch表达式 和 case常量 是否相等
            // 2. when 是额外附加条件，必须为 true 才能进入当前case分支
            // 3. 进入分支的必要条件：case匹配成功  &&  when条件为true
            // 4. when 可以写任意布尔表达式：普通判断、方法调用、原子操作都可以
            // 5. 逻辑等价：case匹配后 内嵌一层 if (when条件)
            

            switch (key)
            {
                // 第一步：case匹配 判断按键是不是上箭头
                // 第二步：when 附加条件：执行原子操作，判断当前方向不是向下(2)
                case ConsoleKey.UpArrow
                when Interlocked.CompareExchange(ref _direction, 0, 2) != 2:

                    // 能执行到这里，必须同时满足两个条件：
                    // 条件1：按下的是上箭头
                    // 条件2：when后面的表达式结果为true（当前方向≠向下）

                    // 原子赋值：把方向安全改成 0 向上
                    Interlocked.Exchange(ref _direction, 0);
                    Console.WriteLine("按下上箭头，允许换向：已设置为向上");
                    break;


                case ConsoleKey.DownArrow
                when Interlocked.CompareExchange(ref _direction, 2, 0) != 0:
                    Interlocked.Exchange(ref _direction, 2);
                    Console.WriteLine("按下下箭头，允许换向：已设置为向下");
                    break;

                default:
                    Console.WriteLine("其他按键，不处理");
                    break;
            }

            Console.WriteLine($"\n当前方向值：{_direction}");
            #endregion

            #region Interlocked.Exchange
            // ==============================================
            // Interlocked.Exchange 原子交换
            // 语法：Exchange(ref 变量, 新值)
            // 作用：
            // 1. 原子操作：读旧值 + 写新值 整过程不可拆分，不会被线程打断
            // 2. 把【新值】赋值给变量
            // 3. 返回值：返回赋值之前的【旧值】
            // 等价普通逻辑（非线程安全）：
            // int old = _direction;
            // _direction = 2;
            // return old;
            // ==============================================

            _direction = 0; // 初始方向：0=上

            // 执行原子赋值：把 _direction 改成 2
            // 接收返回值：操作前的旧值
            int oldValue = Interlocked.Exchange(ref _direction, 2);

            Console.WriteLine($"Exchange 演示：");
            Console.WriteLine($"修改前旧值：{oldValue}");
            Console.WriteLine($"修改后新值：{_direction}\n");

            // 若不接收返回值，旧值直接丢弃
            Interlocked.Exchange(ref _direction, 1);
            #endregion

            #region Interlocked.CompareExchange 
            // ==============================================
            // Interlocked.CompareExchange 原子比较并交换
            // 语法：CompareExchange(ref 变量, 新值, 预期旧值)
            // 逻辑规则：
            // 如果 变量当前值 == 预期旧值
            //     就把变量改成 新值
            // 否则
            //     不做任何修改
            // 返回值：永远返回【操作前变量的真实值】
            // 等价普通逻辑（非线程安全）：
            // int realOld = _direction;
            // if (realOld == 预期旧值)
            // {
            //     _direction = 新值;
            // }
            // return realOld;
            // ==============================================

            _direction = 2; // 先赋值：2=下

            // 含义：如果当前 _direction 等于 2，就改成 0
            // 返回真实旧值
            int realOld = Interlocked.CompareExchange(
                ref _direction,   // 要操作的变量
                0,                // 满足条件要改成的新值
                2                 // 预期的旧值
            );

            Console.WriteLine($"CompareExchange 演示：");
            Console.WriteLine($"操作前真实值：{realOld}");
            Console.WriteLine($"操作后变量值：{_direction}\n");
            #endregion

            #region 知识点：Volatile.Read
            // 1. Volatile.Read 作用
            //    原子、线程安全【只读取】共享变量，**只读不改**
            //    禁止CPU缓存、禁止编译器优化，每次都从内存读最新值

            // 2. 为什么不用普通 int nowDir = _direction;
            //    多线程下：普通读取可能读到缓存旧值，不是内存最新方向
            //    导致判断反向逻辑失效，蛇乱掉头

            // 3. 和Interlocked区别
            //    Volatile.Read：只做【安全读】，不修改变量
            //    Interlocked：读+改、比较交换，会修改变量

            // 4. 关键用法
            //    适合：多线程下 只想安全读最新值、不想改动原变量 的场景
            

            // ========== 核心：安全读取当前最新方向，不修改 _direction ==========
            int nowDir = Volatile.Read(ref _direction);
            #endregion

            #region 检查输入
            // 非阻塞检测是否有按键输入
            if (Console.KeyAvailable) { }
            #endregion

        }

    }
}