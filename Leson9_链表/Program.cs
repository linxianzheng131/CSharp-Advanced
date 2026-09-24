#region 知识点一 数据结构
// 数据结构
// 数据结构是计算机存储、组织数据的方式（规则）
// 数据结构是指相互之间存在一种或多种特定关系的数据元素的集合
// 比如自定义的一个 类 也可以称为一种数据结构 自己定义的数据组合规则
// 不要把数据结构想的太复杂
// 简单点理解，就是人定义的 存储数据 和 表示数据之间关系 的规则而已
// 常用的数据结构（前辈总结和制定的一些经典规则）
// 数组、栈、队列、链表、树、图、堆、散列表
#endregion

#region 知识点二 线性表
// 线性表是一种数据结构，是由n个具有相同特性的数据元素的有限序列
// 比如数组、ArrayList、Stack、Queue、链表等等
#endregion

// 顺序存储和链式存储 是数据结构中两种 存储结构

#region 知识点三 顺序存储
// 数组、Stack、Queue、List、ArrayList — 顺序存储
// 只是 数组、Stack、Queue的 组织规则不同而已
// 顺序存储:
// 用一组地址连续的存储单元依次存储线性表的各个数据元素
#endregion

#region 知识点四 链式存储
// 单向链表、双向链表、循环链表 — 链式存储
// 链式存储(链接存储):
// 用一组任意的存储单元存储线性表中的各个数据元素
#endregion

#region 知识点五 自己实现一个最简单的单向链表
/// <summary>
/// 单向链表节点
/// </summary>
/// <typeparam name="T"></typeparam>
class LinkedNode<T>
{
    // 存储当前节点的数据
    public T value;
    // 这个存储下一个元素是谁 相当于钩子，用来串联链表
    public LinkedNode<T> nextNode;

    // 构造函数：初始化节点时必须传入数据
    public LinkedNode(T value)
    {
        this.value = value;
        // 新节点默认下一个节点为null
        this.nextNode = null;
    }
}

/// <summary>
/// 单向链表类 管理 节点 管理 添加等等
/// </summary>
/// <typeparam name="T"></typeparam>
class LindedList<T>
{
    // 链表头节点（链表的起点）
    public LinkedNode<T> head;
    // 链表尾节点（链表的终点，用于尾部快速添加）
    public LinkedNode<T> last;

    /// <summary>
    /// 向链表尾部添加元素
    /// </summary>
    /// <param name="value">要添加的元素</param>
    public void Add(T value)
    {
        // 添加节点 必然是new一个新的节点
        LinkedNode<T> node = new LinkedNode<T>(value);

        // 如果链表为空（头节点为null），说明是第一个元素
        if (head == null)
        {
            // 头尾都指向这个新节点
            head = node;
            last = node;
        }
        else
        {
            // 让当前尾节点的next指向新节点，完成串联
            last.nextNode = node;
            // 更新尾节点为新节点
            last = node;
        }
    }

    public void AddAfter(T value, T newValue)
    {
        // 从头节点开始遍历，找到第一个值为value的节点
        LinkedNode<T> node = head;
        while (node != null)
        {
            if (node.value.Equals(value))
            {
                // 找到目标节点，创建新节点
                LinkedNode<T> newNode = new LinkedNode<T>(newValue);

                //  ！！！  下面的两行代码的顺序不能颠倒，否则会丢失目标节点后面的链表部分  ！！！

                // 新节点的next指向目标节点的下一个节点
                newNode.nextNode = node.nextNode;
                // 目标节点的next指向新节点，完成插入
                node.nextNode = newNode;


                // 如果目标节点是尾节点，更新尾节点为新节点
                if (last == node)
                {
                    last = newNode;
                }
                break; // 插入完成后退出循环
            }
            node = node.nextNode; // 继续遍历下一个节点
        }
    }

    /// <summary>
    /// 从链表中移除指定值的第一个匹配节点
    /// </summary>
    /// <param name="value">要移除的元素值</param>
    public void Remove(T value)
    {
        // 链表为空，直接返回
        if (head == null)
        {
            return;
        }

        // 情况1：要删除的是头节点
        if (head.value.Equals(value))
        {
            // 头节点指向下一个节点，完成头节点删除
            head = head.nextNode;
            // 如果头节点 被移除 发现头节点变空
            // 证明只有一个节点 那尾也要清空
            if (head == null)
            {
                last = null;
            }
            return;
        }

        // 情况2：要删除的是非头节点，从第二个节点开始遍历
        LinkedNode<T> node = head;
        // 遍历到倒数第二个节点为止（保证nextNode不为null）
        while (node.nextNode != null)
        {
            // 找到要删除的节点（当前节点的下一个节点就是目标）
            if (node.nextNode.value.Equals(value))
            {
                // 让当前找到的这个元素的 上一个节点
                // 指向 自己的下一个节点，跳过目标节点，完成删除
                node.nextNode = node.nextNode.nextNode;

                // 如果删除的是尾节点，需要更新last指针
                if (node.nextNode == null)
                {
                    last = node;
                }
                break;
            }
            // 关键：遍历指针后移，写在if语句块后，避免漏写导致死循环
            node = node.nextNode;
        }
    }
}
#endregion

#region 知识点六 顺序存储和链式存储的优缺点
// 从增删查改的角度去思考
// 增：链式存储 计算上 优于顺序存储 （中间插入时链式不用像顺序一样去移动位置）
// 删：链式存储 计算上 优于顺序存储 （中间删除时链式不用像顺序一样去移动位置）
// 查：顺序存储 使用上 优于链式存储 （数组可以直接通过下标得到元素，链式需要遍历）
// 改：顺序存储 使用上 优于链式存储 （数组可以直接通过下标得到元素，链式需要遍历）
#endregion

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("顺序存储和链式存储");

        #region 链式存储手动串联示例
        // 手动创建节点并串联（理解链表本质）
        LinkedNode<int> node = new LinkedNode<int>(1);
        LinkedNode<int> node2 = new LinkedNode<int>(2);
        node.nextNode = node2;
        node2.nextNode = new LinkedNode<int>(3);
        node2.nextNode.nextNode = new LinkedNode<int>(4);
        #endregion

        #region 自定义链表完整测试
        // 实例化自定义链表
        LindedList<int> link = new LindedList<int>();

        // 1. 测试添加元素
        link.Add(1);
        link.Add(2);
        link.Add(3);
        link.Add(4);

        // 遍历打印链表（初始状态：1 2 3 4）
        Console.WriteLine("=== 初始添加后链表 ===");
        LinkedNode<int> currentNode = link.head;
        while (currentNode != null)
        {
            Console.WriteLine(currentNode.value);
            currentNode = currentNode.nextNode;
        }

        // 2. 测试删除中间元素（删除2）
        link.Remove(2);
        Console.WriteLine("\n=== 删除元素2后链表 ===");
        currentNode = link.head;
        while (currentNode != null)
        {
            Console.WriteLine(currentNode.value);
            currentNode = currentNode.nextNode;
        }

        // 3. 测试删除头元素（删除1）
        link.Remove(1);
        Console.WriteLine("\n=== 删除元素1后链表 ===");
        currentNode = link.head;
        while (currentNode != null)
        {
            Console.WriteLine(currentNode.value);
            currentNode = currentNode.nextNode;
        }

        // 4. 测试尾部添加元素（添加99）
        link.Add(99);
        Console.WriteLine("\n=== 添加元素99后链表 ===");
        currentNode = link.head;
        while (currentNode != null)
        {
            Console.WriteLine(currentNode.value);
            currentNode = currentNode.nextNode;
        }

        // 5. 测试在中间添加元素（在元素3后添加88）
        link.AddAfter(3, 88);
        Console.WriteLine("\n=== 在元素3后添加88后链表 ===");
        currentNode = link.head;
        while (currentNode != null)
        {
            Console.WriteLine(currentNode.value);
            currentNode = currentNode.nextNode;
        }

        #endregion
    }
}