using System;
using System.Threading;

class VolatileClassDemo
{
    // 普通字段，不加volatile关键字
    private bool _isStop;
    private int _num;

    public void Test()
    {
        // ================== 1. Volatile.Write 强制写入主内存 ==================
        // 作用：等效给变量写操作加volatile语义
        // 写完立刻刷新到主内存，禁止指令重排
        Volatile.Write(ref _isStop, true);
        Volatile.Write(ref _num, 100);


        // ================== 2. Volatile.Read 强制读取主内存 ==================
        // 作用：每次都从主内存读最新值，不走CPU缓存
        bool stopFlag = Volatile.Read(ref _isStop);
        int value = Volatile.Read(ref _num);


        // ================== 3. 常用重载（全部类型通用） ==================
        // 布尔型
        Volatile.Write(ref _isStop, false);
        Volatile.Read(ref _isStop);

        // 整型
        Volatile.Write(ref _num, 200);
        Volatile.Read(ref _num);

        // 引用类型
        object obj = new object();
        Volatile.Write(ref obj, null);
        Volatile.Read(ref obj);
    }

    // ================== 实战用法：替代 volatile 关键字 ==================
    private void ThreadWork()
    {
        // 不用volatile修饰字段，用Volatile.Read读最新状态
        while (!Volatile.Read(ref _isStop))
        {
            // 业务逻辑
        }
    }
}