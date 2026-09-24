using System;
using System.Collections; // 必须引用：Hashtable 所在命名空间

namespace Lesson4_Hashtable
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hashtable 演示开始");

            #region 知识点一 Hashtable的本质
            //Hashtable (又称散列表) 是基于键的哈希代码组织起来的 键/值对
            //它的主要作用是提高数据查询的效率
            //使用键来访问集合中的元素
            #endregion

            #region 知识点二 申明
            //需要引用命名空间 System.Collections
            Hashtable hashtable = new Hashtable();
            #endregion

            #region 知识点三 增删查改

            #region 增
            hashtable.Add(1, "123");
            hashtable.Add("123", 2);
            hashtable.Add(true, false);
            hashtable.Add(false, false);
            //注意： 不能出现相同键，否则会直接报错
            #endregion

            #region 删
            //1.只能通过键去删除
            hashtable.Remove(1);
            //2.删除不存在的键 无反应（不会报错）
            hashtable.Remove(2);

            //3.或者直接清空 集合内所有元素
            //hashtable.Clear(); 
            #endregion

            #region 查
            //1.通过键查看值
            // 找不到会返回空（null）
            Console.WriteLine("键1对应的值：" + hashtable[1]); // 已被删除，输出空
            Console.WriteLine("键4对应的值：" + hashtable[4]); // null
            Console.WriteLine("键\"123\"对应的值：" + hashtable["123"]); // 输出2

            //2.查看是否存在
            //根据键检测
            if (hashtable.Contains(2))
            {
                Console.WriteLine("存在键为2的键值对");
            }
            //这两个用法一样，记住ContainsKey就行了
            //Contains是Hashtable特有的（旧版本遗留），Dictionary没有
            if (hashtable.ContainsKey(2))
            {
                Console.WriteLine("存在键为2的键值对（ContainsKey）");
            }

            //根据值检测
            if (hashtable.ContainsValue(12))
            {
                Console.WriteLine("存在值为12的键值对");
            }
            if (hashtable.ContainsValue(false))
            {
                Console.WriteLine("存在值为false的键值对");
            }
            #endregion

            #region 改
            //只能改 键对应的值内容 无法修改键（键是唯一标识，无法直接修改）
            //先查看原数值
            Console.WriteLine("修改前键1对应的值：" + hashtable[1]);
            //重新赋值即可修改（注意：键1已被删除，此处实际是新增键1）
            hashtable[1] = 100.5f;
            Console.WriteLine("修改后键1对应的值：" + hashtable[1]);
            #endregion

            #endregion

            #region 知识点四 遍历

            //得到键值对 总数
            Console.WriteLine("键值对总数：" + hashtable.Count);

            //1.遍历所有键
            Console.WriteLine("\n=====遍历所有键=====");
            foreach (object item in hashtable.Keys)
            {
                Console.WriteLine("键：" + item);
                Console.WriteLine("值：" + hashtable[item]);
            }

            //2.遍历所有值
            Console.WriteLine("\n=====遍历所有值=====");
            foreach (object item in hashtable.Values)
            {
                Console.WriteLine("值：" + item);
            }

            //3.键值对一起遍历
            Console.WriteLine("\n=====键值对一起遍历=====");
            foreach (DictionaryEntry item in hashtable)
            {
                Console.WriteLine("键：" + item.Key + "  值：" + item.Value);
            }

            //4.迭代器遍历法（底层遍历逻辑）
            Console.WriteLine("\n=====迭代器遍历法=====");
            IDictionaryEnumerator myEnumerator = hashtable.GetEnumerator();
            bool flag = myEnumerator.MoveNext();
            while (flag)
            {
                Console.WriteLine("键：" + myEnumerator.Key + "  值：" + myEnumerator.Value);
                flag = myEnumerator.MoveNext();
            }
            #endregion

            #region 知识点五 装箱拆箱
            //由于Hashtable存储数据的类型是object（万物之父），自然存在装箱拆箱
            //当往其中进行值类型存储时就是在装箱（值类型 → 引用类型）
            //当将值类型对象取出来转换使用时，就存在拆箱（引用类型 → 值类型）

            //示例：装箱（int → object）
            int num = 10;
            hashtable["num"] = num;

            //示例：拆箱（object → int）
            int result = (int)hashtable["num"];
            #endregion

            Console.WriteLine("\nHashtable 演示结束");
        }
    }
}