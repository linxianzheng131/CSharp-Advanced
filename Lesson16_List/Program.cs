 using System;
using System.Collections.Generic;

namespace Lesson16_List排序
{
    /// <summary>
    /// 知识点二用到的自定义类：物品类
    /// 实现了 IComparable<Item> 接口，支持 List.Sort() 直接排序
    /// </summary>
    class Item : IComparable<Item>
    {
        public int money;

        public Item(int money)
        {
            this.money = money;
        }

        /// <summary>
        /// 实现接口的比较方法，用于定义排序规则
        /// 返回值含义：
        /// 小于0：this 排在 other 前面
        /// 等于0：位置不变
        /// 大于0：this 排在 other 后面
        /// </summary>
        /// <param name="other">要和当前对象比较的另一个 Item 对象</param>
        /// <returns>比较结果</returns>
        public int CompareTo(Item other)
        {
            // 返回值的含义
            // 小于0：放在传入对象的前面
            // 等于0：保持当前的位置不变
            // 大于0：放在传入对象的后面

            // 可以简单理解 传入对象的位置 就是0
            // 如果你的返回为负数 就放在它的左边 也就前面
            // 如果你返回正数 就放在它的右边 也就是后面

            //if (this.money > other.money)
            //{
            //    return 1;
            //}
            //else if (this.money < other.money)
            //{
            //    return -1;
            //}
            //else
            //{
            //    return 0;
            //}
            // 上面是升序排序的写法，如果想要降序排序，只需要调换一下返回值即可
            //下面是最简洁的写法
            return this.money.CompareTo(other.money);
        }
    }

    /// <summary>
    /// 知识点三用到的自定义类：商店物品类
    /// 没有实现 IComparable 接口，只能通过委托方式排序
    /// </summary>
    class ShopItem
    {
        public int id;

        public ShopItem(int id)
        {
            this.id = id;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("List排序");

            #region 知识点一 List自带排序方法
            // 系统自带的基础类型（int、float、double等）都可以直接使用 Sort()
            List<int> list = new List<int>();
            list.Add(3);
            list.Add(2);
            list.Add(6);
            list.Add(1);
            list.Add(4);
            list.Add(5);

            Console.WriteLine("排序前：");
            for (int i = 0; i < list.Count; i++)
            {
                Console.WriteLine(list[i]);
            }

            // List<T> 提供了默认的 Sort() 方法
            // 对于实现了 IComparable<T> 接口的类型（如 int），会按默认规则排序
            list.Sort();

            Console.WriteLine("***************");
            Console.WriteLine("排序后：");
            for (int i = 0; i < list.Count; i++)
            {
                Console.WriteLine(list[i]);
            }

            // 补充：ArrayList 中也有 Sort 排序方法，用法类似
            #endregion

            #region 知识点二 自定义类的排序（实现 IComparable 接口）
            Console.WriteLine("\n===== 自定义类排序（接口方式） =====");
            List<Item> itemList = new List<Item>();
            itemList.Add(new Item(45));
            itemList.Add(new Item(10));
            itemList.Add(new Item(99));
            itemList.Add(new Item(24));
            itemList.Add(new Item(100));
            itemList.Add(new Item(12));

            // 排序方法：直接调用 Sort()
            // 因为 Item 类实现了 IComparable<Item>，Sort() 会自动调用 CompareTo 方法
            itemList.Sort();

            for (int i = 0; i < itemList.Count; i++)
            {
                Console.WriteLine(itemList[i].money);
            }
            #endregion

            #region 知识点三 通过委托函数进行排序
            Console.WriteLine("\n===== 自定义类排序（委托方式） =====");
            List<ShopItem> shopItems = new List<ShopItem>();
            shopItems.Add(new ShopItem(2));
            shopItems.Add(new ShopItem(1));
            shopItems.Add(new ShopItem(4));
            shopItems.Add(new ShopItem(3));
            shopItems.Add(new ShopItem(6));
            shopItems.Add(new ShopItem(5));

            // 方法1：传入静态委托函数
            shopItems.Sort(SortShopItem);

            Console.WriteLine("排序后：");
            for (int i = 0; i < shopItems.Count; i++)
            {
                Console.WriteLine(shopItems[i].id);
            }

            // 方法2：匿名委托写法（和上面效果一样）
            /*
            shopItems.Sort(delegate (ShopItem a, ShopItem b)
            {
                if (a.id > b.id)
                {
                    return 1;
                }
                else if (a.id < b.id)
                {
                    return -1;
                }
                else
                {
                    return 0;
                }
            });
            */

            // 方法3：Lambda表达式写法（最简洁）
            /*
            shopItems.Sort((a, b) =>
            {
                return a.id > b.id ? 1 : (a.id < b.id ? -1 : 0);
            });
            */
            #endregion

            // ============ 总结 ============
            /*
            1. 系统自带的变量（int, float, double......）一般都可以直接 Sort
            2. 自定义类 Sort 有两种方式：
               ① 继承接口 IComparable<T>，实现 CompareTo 方法（全局规则）
               ② 在 Sort 中传入委托函数（临时规则，灵活修改）

            委托/CompareTo 的返回值规则是统一的：
            - 负数：a 排在 b 前面（升序）
            - 正数：a 排在 b 后面（降序）
            - 0：顺序不变
            */
        }

        /// <summary>
        /// 用于 ShopItem 排序的委托函数
        /// </summary>
        /// <param name="a">比较对象1</param>
        /// <param name="b">比较对象2</param>
        /// <returns>比较结果</returns>
        static int SortShopItem(ShopItem a, ShopItem b)
        {
            // 传入的两个对象 为列表中的两个对象
            // 进行两两的比较 用左边的和右边的条件 比较
            // 返回值规则 和之前一样 0做标准 负数在左（前） 正数在右（后）
            if (a.id > b.id)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }
    }
}