using System;
using System.Collections; // 用于ArrayList对比

#region 第一题：用泛型实现一个单例模式基类
/// <summary>
/// 泛型单例模式基类
/// 功能：封装单例逻辑，让子类直接继承即可获得单例特性
/// 约束：where T : class 确保子类是引用类型，new() 约束子类必须有私有无参构造函数（配合私有构造实现单例）
/// </summary>
/// <typeparam name="T">要实现单例的子类类型</typeparam>
public abstract class SingletonBase<T> where T : class
{
    // 1. 定义一个静态只读的私有实例变量
    // 利用 Lazy<T> 实现线程安全的延迟初始化（推荐写法）
    private static readonly Lazy<T> _instance;

    // 2. 静态构造函数：确保只执行一次
    static SingletonBase()
    {
        // 通过反射创建子类实例（强制要求子类有私有无参构造函数）
        _instance = new Lazy<T>(() =>
        {
            // 创建实例
            var instance = Activator.CreateInstance(typeof(T), true);
            return instance as T;
        });
    }

    /// <summary>
    /// 获取单例实例的公共访问点
    /// </summary>
    public static T Instance => _instance.Value;
}

/// <summary>
/// 单例测试类（示例）
/// 继承自泛型单例基类，自动获得单例特性
/// </summary>
public class TestSingleton : SingletonBase<TestSingleton>
{
    // 私有构造函数：防止外部通过 new 创建实例
    private TestSingleton()
    {
        Console.WriteLine("TestSingleton 实例被创建（单例，仅执行一次）");
    }

    // 测试自定义方法
    public void ShowMessage()
    {
        Console.WriteLine("我是单例模式的测试类实例");
    }
}
#endregion

#region 第二题：利用泛型仿造ArrayList实现不确定数组类型类
/// <summary>
/// 仿 ArrayList 的泛型动态数组类
/// 功能：实现增、删、查、改，支持任意类型（类型安全，无装箱拆箱）
/// 对比 ArrayList：ArrayList 存储 object，本类存储 T，类型安全
/// </summary>
/// <typeparam name="T">数组存储的元素类型</typeparam>
public class GenericArrayList<T>
{
    // 1. 定义内部数组（存储实际数据）
    private T[] _items;

    // 2. 定义当前元素数量（用于判断有效数据长度）
    private int _count;

    // 3. 公开的元素数量属性
    public int Count => _count;

    // 4. 索引器：支持通过索引快速访问/修改元素
    public T this[int index]
    {
        get
        {
            // 校验索引合法性
            if (index < 0 || index >= _count)
                throw new ArgumentOutOfRangeException(nameof(index), "索引超出范围");
            return _items[index];
        }
        set
        {
            if (index < 0 || index >= _count)
                throw new ArgumentOutOfRangeException(nameof(index), "索引超出范围");
            _items[index] = value;
        }
    }

    // 5. 构造函数：初始化默认容量
    public GenericArrayList(int initialCapacity = 4)
    {
        // 初始化内部数组，默认容量4
        _items = new T[initialCapacity];
        _count = 0;
    }

    #region 核心方法：添加元素 (Add)
    /// <summary>
    /// 添加元素到数组末尾
    /// 自动扩容：当数组满时，容量翻倍
    /// </summary>
    /// <param name="item">要添加的元素</param>
    public void Add(T item)
    {
        // 1. 判断是否需要扩容
        if (_count == _items.Length)
        {
            // 容量翻倍
            int newCapacity = _items.Length == 0 ? 4 : _items.Length * 2;
            T[] newArray = new T[newCapacity];

            // 2. 复制原数据到新数组
            Array.Copy(_items, newArray, _count);

            // 3. 替换内部数组
            _items = newArray;
        }

        // 4. 添加元素到末尾，计数+1
        _items[_count] = item;
        _count++;
    }
    #endregion

    #region 核心方法：移除元素 (Remove)
    /// <summary>
    /// 根据元素值移除第一个匹配项
    /// </summary>
    /// <param name="item">要移除的元素</param>
    /// <returns>是否移除成功</returns>
    public bool Remove(T item)
    {
        // 1. 查找元素索引
        int index = IndexOf(item);

        // 2. 不存在则返回false
        if (index == -1)
            return false;

        // 3. 从索引处开始，将后续元素向前移动一位
        Array.Copy(_items, index + 1, _items, index, _count - index - 1);

        // 4. 清除最后一个元素（避免内存泄漏），计数-1
        _items[_count - 1] = default(T);
        _count--;

        return true;
    }
    #endregion

    #region 核心方法：查找元素索引 (IndexOf)
    /// <summary>
    /// 查找元素第一次出现的索引
    /// </summary>
    /// <param name="item">要查找的元素</param>
    /// <returns>索引，未找到返回-1</returns>
    public int IndexOf(T item)
    {
        // 遍历有效元素
        for (int i = 0; i < _count; i++)
        {
            // 处理值类型和引用类型的相等比较
            if (EqualityComparer<T>.Default.Equals(_items[i], item))
                return i;
        }
        return -1;
    }
    #endregion

    #region 核心方法：获取所有元素 (ToArray)
    /// <summary>
    /// 将有效元素转换为数组返回
    /// </summary>
    /// <returns>元素数组</returns>
    public T[] ToArray()
    {
        T[] result = new T[_count];
        Array.Copy(_items, result, _count);
        return result;
    }
    #endregion

    #region 辅助方法：清空数组 (Clear)
    /// <summary>
    /// 清空所有元素
    /// </summary>
    public void Clear()
    {
        // 对于引用类型，手动置空有助于GC回收
        Array.Clear(_items, 0, _count);
        _count = 0;
    }
    #endregion
}
#endregion

#region 主程序：测试所有功能
class Program
{
    static void Main(string[] args)
    {
        // ================= 测试 1：泛型单例模式基类 =================
        Console.WriteLine("===== 测试单例模式 =====");
        // 获取单例实例
        TestSingleton singleton1 = TestSingleton.Instance;
        singleton1.ShowMessage();

        // 再次获取，验证是否为同一个实例
        TestSingleton singleton2 = TestSingleton.Instance;
        Console.WriteLine($"两个实例是否相同：{ReferenceEquals(singleton1, singleton2)}"); // 输出True

        Console.WriteLine("\n");

        // ================= 测试 2：仿 ArrayList 的泛型数组类 =================
        Console.WriteLine("===== 测试泛型数组类 =====");
        // 1. 创建存储int类型的泛型数组
        GenericArrayList<int> intList = new GenericArrayList<int>();

        // 2. 测试添加
        intList.Add(10);
        intList.Add(20);
        intList.Add(30);
        Console.WriteLine($"添加3个元素后，数量：{intList.Count}"); // 输出3

        // 3. 测试修改（索引器）
        intList[1] = 200;
        Console.WriteLine($"修改索引1的元素：{intList[1]}"); // 输出200

        // 4. 测试查找
        int index = intList.IndexOf(30);
        Console.WriteLine($"查找元素30的索引：{index}"); // 输出2

        // 5. 测试移除
        bool isRemoved = intList.Remove(200);
        Console.WriteLine($"移除元素200是否成功：{isRemoved}"); // 输出True
        Console.WriteLine($"移除后数量：{intList.Count}"); // 输出2

        // 6. 测试遍历
        Console.Write("遍历元素：");
        for (int i = 0; i < intList.Count; i++)
        {
            Console.Write(intList[i] + " "); // 输出10 30
        }

        Console.ReadLine(); // 防止控制台闪退
    }
}
#endregion