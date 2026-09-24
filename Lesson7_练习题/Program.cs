using System;
using System.Collections.Generic;
using System.Collections;

#region 题目1：请描述List和ArrayList的区别
/*
List<T> 和 ArrayList 的核心区别（C# 知识点）：
1.  泛型 vs 非泛型
    - List<T> 是泛型集合（.NET Framework 2.0+ 引入），类型安全，存储指定类型 T 的元素，无需装箱拆箱
    - ArrayList 是非泛型集合（.NET Framework 1.x 遗留），存储 object 类型，任何类型都可存入，存在装箱拆箱开销
2.  类型安全
    - List<T> 编译时检查类型，类型不匹配直接报错，避免运行时类型转换异常
    - ArrayList 编译时不检查类型，可存入任意类型，取出时需强制类型转换，易引发 InvalidCastException
3.  性能
    - List<T> 无装箱拆箱，性能更高，内存占用更优
    - ArrayList 每次存值类型都会装箱、取值都会拆箱，性能损耗大
4.  适用场景
    - List<T> 是现代 C# 推荐用法，适用于所有强类型场景
    - ArrayList 仅用于兼容老旧 .NET 1.x 代码，新项目禁止使用
5.  命名空间
    - List<T> 位于 System.Collections.Generic
    - ArrayList 位于 System.Collections
*/
#endregion

#region 题目2：建立一个整形List，为它添加10~1，删除List中第五个元素，遍历剩余元素并打印
class ListOperationDemo
{
    public static void Main1(string[] args)
    {
        // 1. 创建整型List<int>
        List<int> numList = new List<int>();

        // 2. 添加10~1的元素（从10到1倒序添加）
        for (int i = 10; i >= 1; i--)
        {
            numList.Add(i);
        }

        // 3. 删除List中第五个元素（注意：C# List索引从0开始，第五个元素索引为4）
        // 原List添加后顺序为：[10,9,8,7,6,5,4,3,2,1]，索引4对应元素6
        numList.RemoveAt(4);

        // 4. 遍历剩余元素并打印
        Console.WriteLine("删除第五个元素后的List内容：");
        foreach (int num in numList)
        {
            Console.Write(num + " ");
        }
        Console.WriteLine();
    }
}
#endregion

#region 题目3：一个Monster基类，Boss和Gablin类继承它。在怪物类的构造函数中，将其存储到一个怪物List中，遍历列表可以让Boss和Gablin对象产生不同攻击
// 怪物基类
abstract class Monster
{
    // 静态List，存储所有创建的怪物实例
    protected static List<Monster> monsterList = new List<Monster>();

    // 构造函数：将当前实例加入怪物列表
    public Monster()
    {
        monsterList.Add(this);
    }

    // 抽象攻击方法，子类必须实现不同的攻击逻辑
    public abstract void Attack();

    // 静态方法：遍历所有怪物，执行攻击
    public static void AllMonsterAttack()
    {
        Console.WriteLine("所有怪物发起攻击：");
        foreach (Monster monster in monsterList)
        {
            monster.Attack();
        }
    }

    // 清空怪物列表（用于测试重置，避免多次运行重复添加）
    public static void ClearMonsterList()
    {
        monsterList.Clear();
    }
}

// Boss类，继承Monster
class Boss : Monster
{
    // 构造函数：调用基类构造，自动加入列表
    public Boss() : base() { }

    // 重写攻击方法，实现Boss专属攻击
    public override void Attack()
    {
        Console.WriteLine("Boss发动全屏必杀技！造成巨额伤害！");
    }
}

// Gablin（哥布林）类，继承Monster
class Gablin : Monster
{
    // 构造函数：调用基类构造，自动加入列表
    public Gablin() : base() { }

    // 重写攻击方法，实现哥布林专属攻击
    public override void Attack()
    {
        Console.WriteLine("哥布林挥起木棒挥砍！造成小额伤害！");
    }
}

// 怪物类测试入口
class MonsterTest
{
    public static void Main1(string[] args)
    {
        // 清空列表（避免多次运行重复添加）
        Monster.ClearMonsterList();

        // 创建Boss和哥布林实例，构造函数自动加入列表
        Boss boss = new Boss();
        Gablin goblin1 = new Gablin();  
        Gablin goblin2 = new Gablin();

        // 遍历列表，让所有怪物执行不同攻击
        Monster.AllMonsterAttack();

        Console.WriteLine("\n程序执行完毕，按任意键退出");
        Console.ReadKey();
    }
}
#endregion

#region 统一主程序入口（合并两个测试，保证一个项目可直接运行）
class Program
{
    static void Main(string[] args)
    {
        // 执行题目2：List操作测试
        ListOperationDemo.Main1(args);

        // 执行题目3：怪物类测试
        MonsterTest.Main1(args);
    }
}
#endregion