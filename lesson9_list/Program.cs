using System;
// List<T> 所在命名空间，必须引用
using System.Collections.Generic;

namespace Lesson7_List
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("List");

            #region 知识点一 List的本质
            //常用的泛型集合类，提供了类型安全的动态数组功能
            //是Arraylist的泛型版本，性能更好，类型安全
            //一般不用ArrayList了，推荐使用List<T>
            // List<T> 是一个泛型类，T是类型参数，可以替换为任何类型

            // List是一个C#为我们封装好的类，
            // 它的本质是一个可变类型的泛型数组，
            // List类帮助我们实现了很多方法，
            // 比如泛型数组的增删查改
            #endregion

            #region 知识点二 申明
            //需要引用命名空间
            //using System.Collections.Generic
            List<int> list = new List<int>();
            List<string> list2 = new List<string>();
            List<bool> list3 = new List<bool>();
            #endregion

            #region 知识点三 增删查改

            #region 增
            // 1. Add：在列表末尾添加单个元素
            list.Add(1);
            list.Add(2);
            list.Add(3);
            list.Add(4);

            list2.Add("123");

            List<string> listStr = new List<string>();
            listStr.Add("123");
            // 2. AddRange：在列表末尾添加另一个集合的所有元素
            list2.AddRange(listStr);

            // 3. Insert：在指定索引位置插入单个元素
            list.Insert(0, 999);
            Console.WriteLine(list[0]); // 输出：999
            #endregion

            #region 删
            //1.移除指定元素（按值删除，只删除第一个匹配项）
            list.Remove(1);
            //2.移除指定位置的元素（按索引删除）
            list.RemoveAt(0);
            //3.清空列表所有元素
            list.Clear();

            // 重新添加元素用于后续操作
            list.Add(1);
            list.Add(2);
            list.Add(3);
            list.Add(4);
            #endregion

            #region 查
            //1.得到指定位置的元素（通过索引访问，和数组一致）
            Console.WriteLine(list[0]);
            //2.查看元素是否存在
            if (list.Contains(1))
            {
                Console.WriteLine("存在元素 1");
            }
            //3.正向查找元素位置
            // 找到返回位置 找不到 返回-1
            int index = list.IndexOf(5);
            Console.WriteLine(index);
            //4.反向查找元素位置
            // 找到返回位置 找不到 返回-1
            index = list.LastIndexOf(2);
            Console.WriteLine(index);
            #endregion

            #region 改
            // 直接通过索引赋值修改元素
            Console.WriteLine(list[0]); // 修改前：1
            list[0] = 99;
            Console.WriteLine(list[0]); // 修改后：99
            #endregion

            #endregion

            #region 知识点四 遍历
            //长度（实际存储的元素个数）
            Console.WriteLine(list.Count);
            //容量（List内部数组的长度，自动扩容，避免频繁分配内存）
            //避免产生垃圾
            Console.WriteLine(list.Capacity);

            Console.WriteLine("********************");
            // 1. for循环遍历（通过索引访问，可修改元素）
            for (int i = 0; i < list.Count; i++)
            {
                Console.WriteLine(list[i]);
            }

            Console.WriteLine("********************");
            // 2. foreach遍历（只读遍历，语法简洁，推荐用于只读场景）
            foreach (int item in list)
            {
                Console.WriteLine(item);
            }
            #endregion
        }
    }
}