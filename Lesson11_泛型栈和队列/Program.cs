#region 基础引用
using System;
using System.Collections.Generic;
#endregion

namespace Lesson11_泛型栈和队列
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("泛型栈和队列");

            #region 知识点一 回顾数据容器
            #region 变量
            //无符号
            //byte ushort uint ulong
            //有符号
            //sbyte short int long
            //浮点数
            //float double decimal
            //特殊
            //char bool string
            #endregion

            #region 复杂数据容器
            //枚举 enum
            //结构体 struct
            //数组（一维、二维、交错） [] [,] [][]
            //类
            #endregion

            #region 数据集合（非泛型，System.Collections）
            //using System.Collections;
            //ArrayList  object数据列表
            //Stack      栈    先进后出
            //Queue      队列  先进先出
            //Hashtable  哈希表 键值对
            #endregion

            #region 泛型数据集合（System.Collections.Generic）
            //using System.Collections.Generic;
            //List        列表    泛型队列（动态数组）
            //Dictionary  字典    泛型哈希表
            //LinkedList  双向链表
            //Stack<T>    泛型栈  （原代码笔误Statck已修正为Stack）
            //Queue<T>    泛型队列
            #endregion
            #endregion

            #region 知识点二 泛型栈和队列
            //命名空间：using System.Collections.Generic;
            //使用上 和之前的非泛型Stack和Queue一模一样
            //核心优势：类型安全，避免装箱拆箱，编译期类型检查

            // 泛型栈 Stack<T>：先进后出（LIFO），T为存储元素的类型
            Stack<int> stack = new Stack<int>();

            // 泛型队列 Queue<T>：先进先出（FIFO），T为存储元素的类型
            Queue<object> queue = new Queue<object>();

            // ---------------- 补充：泛型栈常用操作示例 ----------------
            // 1. 入栈 Push()
            stack.Push(10);
            stack.Push(20);
            stack.Push(30);
            Console.WriteLine("栈中元素数量：" + stack.Count); // 输出3

            // 2. 查看栈顶元素 Peek()（不移除）
            Console.WriteLine("栈顶元素：" + stack.Peek()); // 输出30

            // 3. 出栈 Pop()（移除并返回栈顶）
            int top = stack.Pop();
            Console.WriteLine("出栈元素：" + top); // 输出30
            Console.WriteLine("出栈后栈顶：" + stack.Peek()); // 输出20

            // ---------------- 补充：泛型队列常用操作示例 ----------------
            // 1. 入队 Enqueue()
            queue.Enqueue("第一个元素");
            queue.Enqueue(123);
            queue.Enqueue(true);
            Console.WriteLine("队列中元素数量：" + queue.Count); // 输出3

            // 2. 查看队首元素 Peek()（不移除）
            Console.WriteLine("队首元素：" + queue.Peek()); // 输出第一个元素

            // 3. 出队 Dequeue()（移除并返回队首）
            object first = queue.Dequeue();
            Console.WriteLine("出队元素：" + first); // 输出第一个元素
            Console.WriteLine("出队后队首：" + queue.Peek()); // 输出123
            #endregion
        }
    }
}