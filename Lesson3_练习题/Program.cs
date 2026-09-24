using System;
using System.Collections.Generic; // 引入泛型集合命名空间，使用Queue<T>

#region 知识点 - 队列(Queue)的存储规则
/// <summary>
/// 队列(Queue)是一种**先进先出(FIFO, First In First Out)**的线性数据结构
/// 核心存储规则与特性：
/// 1. 操作限制：仅允许在两端进行操作
///   - 队尾(Rear/Tail)：只能进行插入操作（入队，Enqueue）
///   - 队头(Front/Head)：只能进行删除操作（出队，Dequeue）
/// 2. 存储顺序：先进入队列的元素先出队，后进入的后出队
///   - 例：Enqueue(1) → Enqueue(2) → Enqueue(3)，队列内顺序为 [1,2,3]，1在队头
/// 3. 核心方法：
///   - Enqueue(T item)：将元素添加到队尾
///   - Dequeue()：移除并返回队头元素（队空时调用会抛异常）
///   - Peek()：返回队头元素但不移除（队空时调用会抛异常）
///   - Count：获取队列中元素的数量
///   - Clear()：清空队列所有元素
/// 4. 常用实现：.NET中提供非泛型Queue和泛型Queue<T>，推荐使用Queue<T>保证类型安全
/// 5. 适用场景：消息队列、任务调度、打印队列、缓冲区处理、广度优先搜索(BFS)等
/// </summary>
public class QueueStorageRule
{
    /// <summary>
    /// 演示队列的存储规则：入队与出队操作，验证先进先出(FIFO)特性
    /// </summary>
    public static void DemoQueueRule()
    {
        Console.WriteLine("===== 队列(Queue)存储规则演示 =====");

        // 1. 初始化泛型队列，存储字符串类型
        Queue<string> queue = new Queue<string>();

        // 2. 入队操作(Enqueue)：元素添加到队尾
        Console.WriteLine("执行入队操作：A、B、C");
        queue.Enqueue("A"); // 队尾 → [A]
        queue.Enqueue("B"); // 队尾 → [A, B]
        queue.Enqueue("C"); // 队尾 → [A, B, C]

        Console.WriteLine($"队列元素数量：{queue.Count}"); // 输出：3
        Console.WriteLine($"查看队头元素(Peek)：{queue.Peek()}"); // 输出：A（先进来的在队头）

        // 3. 出队操作(Dequeue)：移除并返回队头元素
        Console.WriteLine("\n执行出队操作：");
        Console.WriteLine($"出队：{queue.Dequeue()}"); // 输出：A（队头元素先出）
        Console.WriteLine($"出队：{queue.Dequeue()}"); // 输出：B

        Console.WriteLine($"出队后队列元素数量：{queue.Count}"); // 输出：1
        Console.WriteLine($"当前队头元素：{queue.Peek()}"); // 输出：C

        Console.WriteLine("继续出队：");
        Console.WriteLine($"出队：{queue.Dequeue()}"); // 输出：C
        Console.WriteLine($"出队后队列是否为空：{queue.Count == 0}"); // 输出：True
    }
}
#endregion

#region 练习题 - 使用队列存储10条消息，定时打印并带停顿感
/// <summary>
/// 练习题：
/// 1. 使用队列(Queue)一次性存储10条自定义消息
/// 2. 循环出队并打印，每次打印前暂停指定时间（制造明显停顿感）
/// 3. 控制台输出需清晰展示存储与打印过程
/// </summary>
public class MessageQueuePrinter
{
    /// <summary>
    /// 存储并打印10条队列消息，每隔1秒打印一条
    /// </summary>
    public static void StoreAndPrintMessages()
    {
        Console.WriteLine("\n\n===== 队列消息打印练习 =====");
        Console.WriteLine("步骤1：一次性存入10条消息到队列...");

        // 1. 初始化队列存储字符串类型消息
        Queue<string> messageQueue = new Queue<string>();

        // 2. 一次性存入10条消息
        for (int i = 1; i <= 10; i++)
        {
            string message = $"系统消息_{i:D2}：这是第 {i} 条测试消息";
            messageQueue.Enqueue(message); // 入队
            Console.WriteLine($"已存入：{message}");
        }

        Console.WriteLine($"\n步骤2：队列存储完成，共{messageQueue.Count}条消息");
        Console.WriteLine("步骤3：开始定时打印消息（每1秒打印一条）...\n");

        // 3. 循环出队并打印，带停顿感
        while (messageQueue.Count > 0)
        {
            // 出队获取队头消息
            string currentMessage = messageQueue.Dequeue();

            // 打印消息
            Console.WriteLine($"【打印消息】{DateTime.Now:HH:mm:ss} -> {currentMessage}");

            // 制造明显停顿感：暂停1000毫秒(1秒)
            System.Threading.Thread.Sleep(1000);
        }

        Console.WriteLine("\n所有消息打印完成！");
    }
}
#endregion

#region 主程序：测试队列规则与消息打印
class Program
{
    static void Main(string[] args)
    {
        // 测试队列存储规则
        QueueStorageRule.DemoQueueRule();

        // 测试队列消息存储与定时打印
        MessageQueuePrinter.StoreAndPrintMessages();

        Console.ReadLine(); // 防止控制台程序直接退出
    }
}
#endregion