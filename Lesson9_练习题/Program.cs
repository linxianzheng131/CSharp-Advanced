namespace Lesson9_练习题
{
    #region 第一题：常用的数据结构总结
    /// <summary>
    /// 常用数据结构枚举（用于总结，非业务代码）
    /// </summary>
    public enum CommonDataStructure
    {
        // 线性结构
        Array = 1,          // 数组
        LinkedList = 2,     // 链表（单向/双向/循环）
        Stack = 3,          // 栈
        Queue = 4,          // 队列
        // 树形结构
        BinaryTree = 10,    // 二叉树
        BST = 11,           // 二叉搜索树
        AVLTree = 12,       // 平衡二叉树
        Heap = 13,          // 堆（大顶堆/小顶堆）
        // 图形结构
        Graph = 20,         // 图（无向图/有向图）
        // 哈希结构
        HashTable = 30,     // 哈希表
        Dictionary = 31     // 字典（C#原生哈希表实现）
    }
    #endregion

    #region 第二题：顺序存储与链式存储的区别（注释说明）
    /*
     顺序存储与链式存储的核心区别如下：
     1. 存储方式
        - 顺序存储：用连续的内存块存储数据（如数组），元素物理地址相邻
        - 链式存储：用不连续的内存节点存储数据（如链表），通过指针/引用关联节点
     2. 访问效率
        - 顺序存储：支持随机访问，通过索引直接访问（时间复杂度O(1)）
        - 链式存储：不支持随机访问，需从头节点遍历查找（时间复杂度O(n)）
     3. 插入/删除效率
        - 顺序存储：中间/头部插入删除需移动元素（时间复杂度O(n)），需提前分配容量
        - 链式存储：已知节点指针时，插入删除仅修改引用（时间复杂度O(1)），无需提前分配
     4. 空间利用率
        - 顺序存储：空间连续，无额外开销，但需固定容量（易浪费/溢出）
        - 链式存储：按需分配节点，但需额外存储指针/引用（空间开销略高）
     5. 适用场景
        - 顺序存储：频繁查询、随机访问、数据量固定的场景
        - 链式存储：频繁插入/删除、数据量动态变化的场景
    */
    #endregion

    #region 练习题三：实现一个双向链表
    class Doublelink<T>
    {
        public  T value;
        public Doublelink<T> nextNode;
        public Doublelink<T> preNode;
        public Doublelink(T value)
        {
            this.value = value;
            this.nextNode = null;
            this.preNode = null;

        }
    }

    class DoubleLinkList<T>
    {
        public Doublelink<T> head;
        public Doublelink<T> tail;

        public void Add(T value)
        {
            Doublelink<T> Node = new Doublelink<T>(value);
            if (head == null)
            {
                head = Node;
                tail = Node;
            }
            else
            {
                tail.nextNode = Node;
                Node.preNode = tail;
                tail = Node;
            }
        }

        public void AddAfter(T value ,T newvalue)
        {
            Doublelink<T> current = head;
            while(current != null)
            {
                if(current.value.Equals(value))
                {
                    Doublelink<T> newNode = new Doublelink<T>(newvalue);
                    if (current.nextNode != null)
                    {
                        newNode.nextNode = current.nextNode;
                        newNode.preNode = current;
                        current.nextNode.preNode = newNode;
                        current.nextNode = newNode;
                        
                    }
                    else
                    {
                        current.nextNode = newNode;
                        current.preNode = current;
                    }   

                }
                current = current.nextNode;
            }
        }

        public void AddBefore(T value, T newvalue)
        {
            Doublelink<T> current = head;
            while (current != null)
            {
                if (current.value.Equals(value))
                {
                    Doublelink<T> newNode = new Doublelink<T>(newvalue);
                    if (current.preNode != null)
                    {
                        newNode.preNode = current.preNode;
                        newNode.nextNode = current;
                        current.preNode.nextNode = newNode;
                        current.preNode = newNode;
                    }
                    else
                    {
                        newNode.nextNode = current;
                        current.preNode = newNode;
                        head = newNode;
                    }
                }
                current = current.nextNode;
            }
        }

        public void Remove(T value)
        {
            Doublelink<T> current = head;
            while (current != null) {
                if (current.value.Equals(value))
                {
                    if (current.preNode != null)
                    {
                        current.preNode.nextNode = current.nextNode;
                    }
                    else
                    {
                        head = current.nextNode;
                    }
                    if (current.nextNode != null)
                    {
                        current.nextNode.preNode = current.preNode;
                    }
                    else
                    {
                        tail = current.preNode;
                    }
                }
                current = current.nextNode;
             }
}
    }
    #endregion 
    internal class Program
    {
        static void Main(string[] args)
        {


            #region 测试练习题三
            DoubleLinkList<int> list = new DoubleLinkList<int>();
            list.Add(1);
            list.Add(2);
            list.Add(3);
            Console.WriteLine("初始链表：");
            Doublelink<int> current1 = list.head;
            while (current1 != null)
            {
                Console.WriteLine(current1.value);
                current1 = current1.nextNode;
            }

            list.AddAfter(2, 4);
            list.AddBefore(2, 5);

            Console.WriteLine("添加中间元素后链表：");
            Doublelink<int> current2 = list.head;
            while (current2 != null)
            {
                Console.WriteLine(current2.value);
                current2 = current2.nextNode;
            }

            list.Remove(3);
            list.Remove(4);
            list.Remove(1);

            Console.WriteLine("删除元素后链表：");
            Doublelink<int> current3 = list.head;
            while (current3 != null)
            {
                Console.WriteLine(current3.value);
                current3 = current3.nextNode;
            }
            list.AddBefore(5, 0);
            list.AddAfter(2, 1);

            Console.WriteLine("添加前后元素后链表：");
            Doublelink<int> current4 = list.head;
            while (current4 != null)
            {
                Console.WriteLine(current4.value);
                current4 = current4.nextNode;
            }
            #endregion


        }
    }
}
