using System;
using System.Threading;

namespace InterlockedStudy
{
    class InterlockedDemo
    {
        // 共享全局变量，多线程并发访问
        private static int _count = 0;
        private static int _data = 100;

        // 64位长整型变量（用于 Interlocked.Read 示例）
        private static long _longNum = 9999;

        public void AllInterlockedFunc()
        {
            // ========== 1. Increment() 原子自增 +1 ==========
            // 功能：线程安全的 i++ 操作
            // 参数：ref 引用传递变量
            Interlocked.Increment(ref _count);


            // ========== 2. Decrement() 原子自减 -1 ==========
            // 功能：线程安全的 i-- 操作
            Interlocked.Decrement(ref _count);


            // ========== 3. Add() 原子加减任意数值 ==========
            // 给变量加上指定整数，可正可负
            Interlocked.Add(ref _data, 20);  // +20
            Interlocked.Add(ref _data, -10); // -10


            // ========== 4. Exchange() 原子赋值/交换 ==========
            // 格式：Exchange(ref 变量, 新值)
            // 1. 把新值赋给变量
            // 2. 返回【旧值】
            int oldVal = Interlocked.Exchange(ref _data, 200);
            // oldVal = 赋值之前的原值


            // ========== 5. CompareExchange() 比较交换（CAS核心） ==========
            // 格式：CompareExchange(ref 目标变量, 新值, 对比旧值)
            // 逻辑：
            // 如果 变量 == 对比旧值 → 替换成新值
            // 不相等 → 不修改
            // 无论是否替换，都返回【变量原始值】
            int returnOld = Interlocked.CompareExchange(ref _data, 300, 200);//returnOld = 200，_data被修改成300


            // ========== 6. Read() 原子读取64位整数 ==========
            // 专门读取 long / ulong 64位变量，保证读取原子性
            // ✅ 这里必须写在方法内部，不能直接写在类里！
            long res = Interlocked.Read(ref _longNum);


            // ========== 7. MemoryBarrier() 内存屏障（关键修正） ==========
            // 错误写法：Interlocked.MemoryBarrier();
            // 正确写法：Thread.MemoryBarrier();
            // 作用：禁止指令重排，保证执行顺序
            Thread.MemoryBarrier();
            // 屏障！前面的代码必须全部执行完，才能执行后面的


            Console.WriteLine("所有 Interlocked 方法执行完成");
        }

        // ===================== 实战多线程累加示例 =====================
        public void SafeAddTest()
        {
            // 重置计数器
            _count = 0;

            // 开启10个线程，每个累加1000次
            for (int i = 0; i < 10; i++)
            {
                new Thread(() =>
                {
                    for (int j = 0; j < 1000; j++)
                    {
                        // 原子自增，不会丢数据
                        Interlocked.Increment(ref _count);
                    }
                }).Start();
            }

            // 等待所有线程完成
            Thread.Sleep(2000);
            // 最终结果一定是 10000，绝对准确
            Console.WriteLine($"原子累加结果：{_count}");
        }
    }
}