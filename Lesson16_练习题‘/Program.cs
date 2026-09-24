using System;
using System.Collections.Generic;
using System.Linq;

// 题目1：怪物类
class Monster
{
    public int Att { get; set; }
    public int Def { get; set; }
    public int Hp { get; set; }

    public Monster(int att, int def, int hp)
    {
        Att = att;
        Def = def;
        Hp = hp;
    }

    public void ShowInfo()
    {
        Console.WriteLine($"攻击:{Att,3} 防御:{Def,3} 血量:{Hp,3}");
    }
}

// 题目2：物品类
class Item
{
    public string Type { get; set; }
    public string Name { get; set; }
    public int Quality { get; set; }

    public Item(string type, string name, int quality)
    {
        Type = type;
        Name = name;
        Quality = quality;
    }

    public void ShowInfo()
    {
        Console.WriteLine($"类型:{Type,-5} 品质:{Quality} 名字:{Name}");
    }
}

class Program
{
    static void Main(string[] args)
    {
        #region 题目1：怪物排序/反转
        Console.WriteLine("===== 题目1：怪物排序 =====");
        Random r = new Random();
        List<Monster> monsters = new List<Monster>();

        // 创建10个怪物
        for (int i = 0; i < 10; i++)
        {
            monsters.Add(new Monster(
                r.Next(1, 101),
                r.Next(1, 101),
                r.Next(1, 101)));
        }

        Console.WriteLine("请输入操作：1攻击 2防御 3血量 4反转");
        if (int.TryParse(Console.ReadLine(), out int choose))
        {
            if (choose == 4)
            {
                monsters.Reverse();
            }
            else if (choose >= 1 && choose <= 3)
            {
                monsters.Sort((a, b) =>
                {
                    return choose switch
                    {
                        1 => a.Att.CompareTo(b.Att),
                        2 => a.Def.CompareTo(b.Def),
                        3 => a.Hp.CompareTo(b.Hp),
                        _ => 0
                    };
                });
            }

            foreach (var m in monsters)
                m.ShowInfo();
        }
        #endregion

        Console.WriteLine("\n------------------------\n");

        #region 题目2：物品多权重排序
        Console.WriteLine("===== 题目2：物品多权重排序 =====");
        string[] types = { "武器", "防具", "消耗品" };
        string[] names = { "铁剑", "皮甲", "红药", "木剑", "布甲", "蓝药", "钢剑", "锁子甲", "解毒剂", "短剑" };
        List<Item> items = new List<Item>();

        for (int i = 0; i < 10; i++)
        {
            items.Add(new Item(
                types[r.Next(types.Length)],
                names[i],
                r.Next(1, 6)));
        }

        // 按 类型>品质>名字 排序
        items.Sort((a, b) =>
        {
            int typeCompare = a.Type.CompareTo(b.Type);
            if (typeCompare != 0) return typeCompare;

            int qualityCompare = b.Quality.CompareTo(a.Quality);
            if (qualityCompare != 0) return qualityCompare;

            return a.Name.CompareTo(b.Name);
        });

        foreach (var item in items)
            item.ShowInfo();
        #endregion

        Console.WriteLine("\n------------------------\n");

        #region 题目3：Dictionary排序
        Console.WriteLine("===== 题目3：Dictionary排序 =====");
        Dictionary<int, int> playerScores = new Dictionary<int, int>()
        {
            { 101, 85 },
            { 102, 92 },
            { 103, 78 },
            { 104, 95 },
            { 105, 88 }
        };

        List<KeyValuePair<int, int>> sortedScores = playerScores.ToList();
        sortedScores.Sort((a, b) => b.Value.CompareTo(a.Value));

        foreach (var kvp in sortedScores)
        {
            Console.WriteLine($"玩家ID:{kvp.Key} 分数:{kvp.Value}");
        }
        #endregion

        Console.WriteLine("\n------------------------\n");

        #region 题目4：List<Dictionary> 与 一对多对比
        Console.WriteLine("===== 题目4：一对多字典 =====");
        // 一对多结构：一个键对应多个值
        Dictionary<int, List<int>> oneToManyDict = new Dictionary<int, List<int>>();
        for (int key = 0; key < 5; key++)
        {
            oneToManyDict[key] = new List<int>();
            for (int j = 0; j < 10; j++)
            {
                oneToManyDict[key].Add(r.Next(1, 101));
            }
        }

        // 输出一对多数据
        foreach (var kvp in oneToManyDict)
        {
            Console.WriteLine($"键{kvp.Key}的所有值：{string.Join(", ", kvp.Value)}");
        }
        #endregion
    }
}