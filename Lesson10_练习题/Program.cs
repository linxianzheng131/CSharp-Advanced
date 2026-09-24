using System;
using System.Collections.Generic; // LinkedList<T> 所需命名空间

namespace LinkedListRandomDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region 任务：使用LinkedList<T>，添加10个随机整数，正反向遍历打印
            // 1. 初始化LinkedList<int>链表实例
            LinkedList<int> linkedList = new LinkedList<int>();

            // 2. 初始化随机数生成器，保证每次运行生成不同随机数
            Random random = new Random();

            // 3. 向链表中添加10个随机整数（范围：1~100，可自行修改）
            for (int i = 0; i < 10; i++)
            {
                // 生成1~100之间的随机整数
                int randomNum = random.Next(1, 101);
                // 将随机数添加到链表尾部
                linkedList.AddLast(randomNum);
            }

            // 4. 正向遍历链表并打印所有元素
            Console.WriteLine("===== 正向遍历结果 =====");
            // 方式1：foreach遍历（最简洁，正向遍历）
            foreach (int num in linkedList)
            {
                Console.Write(num + " ");
            }
            Console.WriteLine(); // 换行

            // 方式2：节点遍历（从头节点到尾节点，正向）
            // LinkedListNode<int> currentNode = linkedList.First;
            // while (currentNode != null)
            // {
            //     Console.Write(currentNode.Value + " ");
            //     currentNode = currentNode.Next;
            // }
            // Console.WriteLine();

            // 5. 反向遍历链表并打印所有元素
            Console.WriteLine("===== 反向遍历结果 =====");
            // 从尾节点开始，通过Previous属性向前遍历
            LinkedListNode<int> reverseNode = linkedList.Last;
            while (reverseNode != null)
            {
                Console.Write(reverseNode.Value + " ");
                reverseNode = reverseNode.Previous;
            }
            Console.WriteLine(); // 换行

            // 等待用户输入，防止控制台闪退
            Console.ReadKey();
            #endregion
        }
    }
}