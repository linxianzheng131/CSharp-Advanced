using System;
using System.Collections;

namespace ArrayListAndBackpackDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            // --------------------------
            // 第一题：ArrayList 和数组的区别
            // --------------------------
            Console.WriteLine("===== 第一题：ArrayList 和数组的区别 =====");

            // 1. 数组：长度固定，类型固定
            int[] intArray = new int[3];
            intArray[0] = 1;
            intArray[1] = 2;
            intArray[2] = 3;
            // intArray[3] = 4; // 报错：数组长度固定，无法扩容

            // 2. ArrayList：长度动态可变，可存储不同类型
            ArrayList arrayList = new ArrayList();
            arrayList.Add(1);       // 添加整数
            arrayList.Add("苹果");   // 添加字符串
            arrayList.Add(3.14);     // 添加浮点数
            arrayList.Add(true);     // 添加布尔值
            arrayList.Add(4);        // 自动扩容，无需担心长度

            Console.WriteLine("数组长度：" + intArray.Length);
            Console.WriteLine("ArrayList长度：" + arrayList.Count);

            Console.WriteLine("\n数组元素：");
            foreach (int num in intArray)
                Console.Write(num + " ");

            Console.WriteLine("\nArrayList元素：");
            foreach (var item in arrayList)
                Console.Write(item + " ");

            Console.WriteLine("\n\n");

            // --------------------------
            // 第二题：背包管理类（使用 ArrayList 实现）
            // --------------------------
            Console.WriteLine("===== 第二题：背包管理类 =====");
            Backpack myBackpack = new Backpack();

            // 测试购买功能
            myBackpack.Buy("苹果", 2, 5);
            myBackpack.Buy("香蕉", 3, 2);
            myBackpack.Buy("苹果", 1, 5); // 累加苹果数量

            // 显示背包
            myBackpack.ShowItems();

            // 测试卖出功能
            myBackpack.Sell("苹果", 2);
            myBackpack.Sell("香蕉", 5); // 数量不足，无法卖出

            // 再次显示背包
            myBackpack.ShowItems();

            Console.ReadKey();
        }
    }

    // 背包管理类
    class Backpack
    {
        private ArrayList items = new ArrayList(); // 存储物品（每个元素是一个数组：[物品名, 数量]）
        private int money = 100; // 初始金钱

        // 购买物品
        public void Buy(string itemName, int count, int pricePerItem)
        {
            int totalCost = count * pricePerItem;
            if (totalCost > money)
            {
                Console.WriteLine("金钱不足，无法购买！");
                return;
            }

            // 检查物品是否已存在
            bool itemExists = false;
            foreach (object[] item in items)
            {
                if ((string)item[0] == itemName)
                {
                    item[1] = (int)item[1] + count; // 累加数量
                    itemExists = true;
                    break;
                }
            }

            // 物品不存在则添加新条目
            if (!itemExists)
                items.Add(new object[] { itemName, count });

            money -= totalCost;
            Console.WriteLine($"成功购买 {count} 个 {itemName}，剩余金钱：{money}");
        }

        // 卖出物品
        public void Sell(string itemName, int count)
        {
            foreach (object[] item in items)
            {
                if ((string)item[0] == itemName)
                {
                    if ((int)item[1] >= count)
                    {
                        item[1] = (int)item[1] - count;
                        money += count * 5; // 假设卖出单价为5
                        Console.WriteLine($"成功卖出 {count} 个 {itemName}，剩余金钱：{money}");

                        // 如果数量为0则移除物品
                        if ((int)item[1] == 0)
                            items.Remove(item);
                    }
                    else
                    {
                        Console.WriteLine("物品数量不足，无法卖出！");
                    }
                    return;
                }
            }
            Console.WriteLine("背包中没有该物品！");
        }

        // 显示背包物品
        public void ShowItems()
        {
            Console.WriteLine("\n当前背包物品：");
            if (items.Count == 0)
            {
                Console.WriteLine("背包为空");
                return;
            }

            foreach (object[] item in items)
                Console.WriteLine($"物品：{item[0]}，数量：{item[1]}");
        }
    }
}