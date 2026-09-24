using System;
using System.Collections; // 必须引入此命名空间以使用 Hashtable

#region 知识点 - Hashtable(哈希表)的存储规则
/// <summary>
/// 【Hashtable 哈希表存储规则详解】
/// 1. 核心结构：
///    - 采用“键值对(Key-Value)”的形式存储数据，每个元素包含一个Key和一个Value。
///    - Key 必须是唯一的（不可重复），Value 可以重复。
/// 2. 存储机制（哈希算法）：
///    - 内部通过计算 Key 的 GetHashCode() 得到哈希码，通过哈希码定位存储位置（桶/Bucket）。
///    - 当发生哈希冲突（不同Key得到相同哈希码）时，会自动使用链表或红黑树解决冲突。
/// 3. 读写特性：
///    - 查找速度快：通过Key直接计算索引，平均时间复杂度为 O(1)。
///    - 无序性：存储顺序不取决于插入顺序，也不按键的大小排序（完全取决于哈希计算结果）。
/// 4. 类型安全：
///    - 非泛型集合，Key 和 Value 均存储为 object 类型，因此可以存储任意数据类型，但存取时需要装箱/拆箱。
/// 5. 常用核心方法：
///    - Add(Key, Value)：添加元素，Key重复会抛异常。
///    - Remove(Key)：根据Key删除元素。
///    - ContainsKey(Key)：判断Key是否存在。
///    - this[Key]：索引器，通过Key获取或设置Value。
///    - Keys：获取所有Key的集合；Values：获取所有Value的集合。
/// </summary>
public class HashtableRuleDemo
{
    public static void ShowStorageRule()
    {
        Console.WriteLine("===== Hashtable 存储规则演示 =====");

        // 1. 初始化哈希表
        Hashtable hashtable = new Hashtable();

        // 2. 添加元素 (Add)
        // 这里 Key 是字符串， Value 是整数
        hashtable.Add("ID_001", 10);
        hashtable.Add("ID_002", 20);
        hashtable.Add("ID_003", 30);

        // 3. 访问元素 (索引器)
        Console.WriteLine("通过Key获取Value: ID_002 -> " + hashtable["ID_002"]);

        // 4. 遍历哈希表 (无序输出)
        Console.WriteLine("\n遍历哈希表 (注意顺序无序):");
        foreach (DictionaryEntry entry in hashtable)
        {
            Console.WriteLine($"Key: {entry.Key}, Value: {entry.Value}");
        }

        // 5. 检测Key存在性
        if (hashtable.ContainsKey("ID_001"))
        {
            Console.WriteLine("\nID_001 已存在");
        }

        // 6. 修改元素 (索引器赋值)
        hashtable["ID_001"] = 100;
        Console.WriteLine($"修改后 ID_001 的值: {hashtable["ID_001"]}");

        // 7. 删除元素
        hashtable.Remove("ID_003");
        Console.WriteLine($"删除 ID_003 后，元素数量: {hashtable.Count}");
    }
}
#endregion

#region 练习题 - 怪物管理器 (Monster Manager)
/// <summary>
/// 【怪物管理器需求】
/// 1. 使用 Hashtable 存储怪物集合，Key 为怪物唯一ID (int 或 string)，Value 为怪物实例。
/// 2. 提供创建怪物 (AddMonster) 方法，检查ID唯一性。
/// 3. 提供移除怪物 (RemoveMonster) 方法，根据ID删除。
/// 4. 提供显示所有怪物 (ShowMonsters) 方法，遍历并打印信息。
/// </summary>
public class MonsterManager
{
    // 声明私有哈希表实例，用于存储怪物
    private Hashtable _monsterTable;

    /// <summary>
    /// 构造函数：初始化哈希表
    /// </summary>
    public MonsterManager()
    {
        _monsterTable = new Hashtable();
    }

    #region 怪物实体类 (Monster Class)
    /// <summary>
    /// 怪物类，封装怪物基本信息
    /// </summary>
    public class Monster
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int HP { get; set; }

        // 重写ToString，方便控制台打印美观信息
        public override string ToString()
        {
            return $"[怪物ID:{ID}] 名称:{Name} 生命值:{HP}";
        }
    }
    #endregion

    #region 核心方法：创建/添加怪物
    /// <summary>
    /// 创建并添加怪物到管理器
    /// </summary>
    /// <param name="id">唯一ID</param>
    /// <param name="name">怪物名称</param>
    /// <param name="hp">初始血量</param>
    /// <returns>操作结果字符串</returns>
    public string AddMonster(int id, string name, int hp)
    {
        // 1. 检查ID是否已存在
        if (_monsterTable.ContainsKey(id))
        {
            return $"失败！怪物ID {id} 已存在，无法重复创建。";
        }

        // 2. 创建怪物对象并加入哈希表
        Monster newMonster = new Monster
        {
            ID = id,
            Name = name,
            HP = hp
        };
        _monsterTable.Add(id, newMonster);

        return $"成功！创建怪物 [{name}] (ID:{id})。";
    }
    #endregion

    #region 核心方法：移除怪物
    /// <summary>
    /// 根据唯一ID移除怪物
    /// </summary>
    /// <param name="id">待删除怪物ID</param>
    /// <returns>操作结果字符串</returns>
    public string RemoveMonster(int id)
    {
        // 1. 检查ID是否存在
        if (!_monsterTable.ContainsKey(id))
        {
            return $"失败！未找到ID为 {id} 的怪物。";
        }

        // 2. 移除怪物
        _monsterTable.Remove(id);
        return $"成功！已移除ID为 {id} 的怪物。";
    }
    #endregion

    #region 核心方法：显示所有怪物
    /// <summary>
    /// 遍历并打印队列中所有怪物信息
    /// </summary>
    public void ShowAllMonsters()
    {
        Console.WriteLine($"\n===== 当前怪物列表 (总数: {_monsterTable.Count}) =====");

        if (_monsterTable.Count == 0)
        {
            Console.WriteLine("(暂无怪物)");
            return;
        }

        // 遍历哈希表
        foreach (DictionaryEntry entry in _monsterTable)
        {
            // 由于Value存储的是object类型，需要强制转换为 Monster 类
            Monster monster = (Monster)entry.Value;
            Console.WriteLine(monster.ToString());
        }
    }
    #endregion
}
#endregion

#region 主程序：测试所有功能
class Program
{
    static void Main(string[] args)
    {
        // 1. 演示 Hashtable 存储规则
        HashtableRuleDemo.ShowStorageRule();

        Console.WriteLine("\n\n===============================================");
        Console.WriteLine("===== 开始测试怪物管理器 =====");

        // 2. 实例化怪物管理器
        MonsterManager manager = new MonsterManager();

        // 测试添加怪物
        Console.WriteLine(manager.AddMonster(101, "哥布林", 100));
        Console.WriteLine(manager.AddMonster(102, "史莱姆", 50));
        Console.WriteLine(manager.AddMonster(101, "巨龙", 999)); // 测试重复ID

        // 显示怪物
        manager.ShowAllMonsters();

        // 测试移除怪物
        Console.WriteLine(manager.RemoveMonster(102));
        Console.WriteLine(manager.RemoveMonster(999)); // 测试移除不存在的ID

        // 再次显示
        manager.ShowAllMonsters();

        Console.ReadLine(); // 防止控制台闪退
    }
}
#endregion