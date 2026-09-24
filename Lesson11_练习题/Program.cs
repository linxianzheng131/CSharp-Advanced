using System;
using System.Collections.Generic;
using System.Linq;

namespace ContainerSelectionDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("===== C# 常见存储容器选择指南 =====");

            #region 1. 数组（Array）
            /*
             * 适用场景：
             * 1. 数据长度固定，且创建后不会频繁增删
             * 2. 需要极高的随机访问性能（按索引直接读取/修改）
             * 3. 存储单一类型的同构数据
             *
             * 优点：
             * - 内存连续，随机访问速度最快（O(1)）
             * - 占用内存最小，没有额外的对象开销
             *
             * 缺点：
             * - 长度固定，无法动态扩容
             * - 插入/删除中间元素需要移动大量数据，效率低（O(n)）
             *
             * 示例：存储一周的7个日期、已知数量的配置项
             */
            int[] numbers = new int[5] { 1, 2, 3, 4, 5 };
            Console.WriteLine($"数组示例：第3个元素是 {numbers[2]}");
            #endregion

            #region 2. List<T>
            /*
             * 适用场景：
             * 1. 数据长度不固定，需要频繁增删元素
             * 2. 主要按索引访问，顺序遍历场景多
             * 3. 存储单一类型的同构数据
             *
             * 优点：
             * - 动态扩容，使用方便
             * - 随机访问快（O(1)），尾部增删效率高（中间增删效率低，本质上是数组）
             * - 提供大量内置方法（Add/Remove/Find/Sort等）
             *
             * 缺点：
             * - 本质是数组封装，插入/删除中间元素仍需移动数据（O(n)）
             * - 有少量额外的内存开销
             *
             * 示例：用户列表、商品列表、日志集合
             */
            List<string> names = new List<string> { "张三", "李四", "王五" };
            names.Add("赵六");
            Console.WriteLine($"List示例：共有 {names.Count} 个名字");
            #endregion

            #region 3. Dictionary<TKey, TValue>
            /*
             * 适用场景：
             * 1. 需要通过键（Key）快速查找值（Value）
             * 2. 键值对映射关系（如用户ID对应用户对象、字典翻译）
             * 3. 不关心元素顺序，只关心查找效率
             *
             * 优点：
             * - 基于哈希表实现，查找、插入、删除效率极高（O(1)平均）
             * - 键值对结构清晰，语义明确
             *
             * 缺点：
             * - 元素无序（.NET Core 3.0+ 插入顺序不保证）
             * - 键必须唯一，且需要实现 GetHashCode 和 Equals
             * - 内存占用较高
             *
             * 示例：缓存数据、配置映射、用户信息查询
             */
            Dictionary<int, string> userDict = new Dictionary<int, string>();
            userDict.Add(1001, "管理员");
            userDict.Add(1002, "普通用户");
            Console.WriteLine($"Dictionary示例：ID 1001 对应的角色是 {userDict[1001]}");
            #endregion

            #region 4. Stack<T>
            /*
             * 适用场景：
             * 1. 后进先出（LIFO）的业务逻辑
             * 2. 撤销/恢复操作、递归调用模拟、表达式求值
             * 3. 括号匹配、路径回溯等场景
             *
             * 优点：
             * - 结构简单，操作高效（Push/Pop/Peek 均为 O(1)）
             * - 天然保证后进先出的顺序
             *
             * 缺点：
             * - 只能访问栈顶元素，不支持随机访问
             * - 遍历顺序是从栈顶到栈底，和添加顺序相反
             *
             * 示例：浏览器前进后退、编辑器撤销、函数调用栈
             */
            Stack<int> stack = new Stack<int>();
            stack.Push(1);
            stack.Push(2);
            stack.Push(3);
            Console.WriteLine($"Stack示例：弹出栈顶元素 {stack.Pop()}（后进先出，结果为3）");
            #endregion

            #region 5. Queue<T>
            /*
             * 适用场景：
             * 1. 先进先出（FIFO）的业务逻辑
             * 2. 任务排队、消息队列、生产者-消费者模式
             * 3. 广度优先搜索（BFS）
             *
             * 优点：
             * - 入队/出队/查看队首效率高（Enqueue/Dequeue/Peek 均为 O(1)）
             * - 天然保证先进先出的顺序
             *
             * 缺点：
             * - 只能访问队首和队尾，不支持随机访问
             *
             * 示例：打印任务队列、消息处理队列、请求排队
             */
            Queue<string> queue = new Queue<string>();
            queue.Enqueue("任务1");
            queue.Enqueue("任务2");
            queue.Enqueue("任务3");
            Console.WriteLine($"Queue示例：取出队首元素 {queue.Dequeue()}（先进先出，结果为任务1）");
            #endregion

            #region 6. LinkedList<T>
            /*
             * 适用场景：
             * 1. 需要频繁在任意位置插入/删除元素
             * 2. 不依赖索引访问，主要通过节点引用遍历
             * 3. 实现自定义的双端队列、LRU缓存等
             *
             * 优点：
             * - 插入/删除任意位置元素效率高（O(1)，前提是已有节点引用）
             * - 内存无需连续，动态扩容无性能损耗
             *
             * 缺点：
             * - 随机访问效率低（O(n)，需要从头遍历）
             * - 每个节点有额外的对象开销，内存占用高
             * - 缓存不友好，遍历速度慢于数组/List
             *
             * 示例：频繁增删的列表、链表结构的自定义数据结构
             */
            LinkedList<int> linkedList = new LinkedList<int>();
            var firstNode = linkedList.AddFirst(10);
            linkedList.AddAfter(firstNode, 20);
            linkedList.AddLast(30);
            Console.WriteLine($"LinkedList示例：链表共有 {linkedList.Count} 个节点，第一个节点值为 {linkedList.First.Value}");
            #endregion

            Console.WriteLine("\n===== 选择总结 =====");
            Console.WriteLine("1. 长度固定、追求极致性能 → 数组");
            Console.WriteLine("2. 动态增删、按索引访问为主 → List<T>");
            Console.WriteLine("3. 键值对快速查找 → Dictionary<TKey, TValue>");
            Console.WriteLine("4. 后进先出（撤销/回溯） → Stack<T>");
            Console.WriteLine("5. 先进先出（任务排队） → Queue<T>");
            Console.WriteLine("6. 频繁任意位置增删 → LinkedList<T>");

            Console.ReadKey();
        }
    }
}