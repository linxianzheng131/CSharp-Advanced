using System;
using System.Collections;

namespace Lesson22_迭代器
{
    #region 知识点一 迭代器是什么
    // 迭代器（iterator）有时又称光标（cursor）
    // 是程序设计的软件设计模式
    // 迭代器模式提供一个方法顺序访问一个聚合对象中的各个元素
    // 而又不暴露其内部的标识

    // 在表现效果上看
    // 是可以在容器对象（例如链表或数组）上遍历访问的接口
    // 设计人员无需关心容器对象的内存分配的实现细节
    // 可以用foreach遍历的类，都是实现了迭代器的
    #endregion

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("迭代器");

            // 传统方式实现的自定义列表
            CustomList list = new CustomList();
            // foreach本质：
            // 1. 先获取in后面这个对象的 IEnumerator，会调用对象其中的GetEnumerator方法 来获取
            // 2. 执行得到这个IEnumerator对象中的 MoveNext方法
            // 3. 只要MoveNext方法的返回值为true，就会去得到Current，然后复制给 item
            foreach (int item in list)
            {
                Console.WriteLine(item);
            }

            // yield return实现的自定义列表
            CustomList2 list2 = new CustomList2();
            foreach (int item in list2)
            {
                Console.WriteLine(item);
            }

            // 泛型迭代器实现
            CustomList<string> list3 = new CustomList<string>("123", "321", "333", "555");
            foreach (string item in list3)
            {
                Console.WriteLine(item);
            }
        }
    }

    #region 知识点二 标准迭代器的实现方法
    // 关键接口：IEnumerator, IEnumerable
    // 命名空间：using System.Collections;
    // 可以通过同时继承IEnumerable和IEnumerator实现其中的方法
    class CustomList : IEnumerable, IEnumerator
    {
        private int[] list;
        // 从-1开始的光标 用于表示 数据得到了哪个位置
        private int position = -1;

        public CustomList()
        {
            list = new int[] { 1, 2, 3, 4, 5, 6, 7, 8 };
        }

        #region IEnumerable 成员
        public IEnumerator GetEnumerator()
        {
            Reset(); // 重置光标位置
            return this;
        }
        #endregion

        #region IEnumerator 成员
        // 获取当前光标指向的元素
        public object Current
        {
            get
            {
                return list[position];
            }
        }

        // 移动光标
        public bool MoveNext()
        {
            // 移动光标
            ++position;
            // 是否溢出 溢出就不合法
            return position < list.Length;
        }

        // reset是重置光标位置 一般写在获取 IEnumerator对象这个函数中
        // 用于第一次重置光标位置
        public void Reset()
        {
            position = -1;
        }
        #endregion
    }
    #endregion

    #region 知识点三 用yield return 语法糖实现迭代器
    // yield return 是C#提供给我们的语法糖
    // 所谓语法糖，也称糖衣语法
    // 主要作用就是将复杂逻辑简单化，可以增加程序的可读性
    // 从而减少程序代码出错的机会

    // 关键接口：IEnumerable
    // 命名空间：using System.Collections;
    // 让想要通过foreach遍历的自定义类实现接口中的方法GetEnumerator即可
    class CustomList2 : IEnumerable
    {
        private int[] list;

        public CustomList2()
        {
            list = new int[] { 1, 2, 3, 4, 5, 6, 7, 8 };
        }

        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < list.Length; i++)
            {
                // yield关键字 配合迭代器使用
                // 可以理解为 暂时返回 保留当前的状态
                // 一会还会在回来
                yield return list[i];
            }
        }
    }
    #endregion

    #region 知识点四 用yield return 语法糖为泛型类实现迭代器
    class CustomList<T> : IEnumerable
    {
        private T[] array;

        public CustomList(params T[] array)
        {
            this.array = array;
        }

        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < array.Length; i++)
            {
                yield return array[i];
            }
        }
    }
    #endregion

    #region 整体知识总结
    // 迭代器核心作用：
    // 让外部可以直接用foreach遍历自定义类中的元素，不用关心内部数据结构实现

    // 迭代器两种实现方式：
    // 1. 传统原生方式：同时继承 IEnumerable + IEnumerator
    //    手动实现 GetEnumerator、Current、MoveNext、Reset
    //    自己维护光标position位置，代码繁琐但底层原理清晰

    // 2. yield return 语法糖方式：只继承 IEnumerable
    //    不用手动实现迭代器状态机，编译器自动生成底层枚举逻辑
    //    执行到yield return会暂停保留状态，下次foreach继续往下执行
    //    代码简洁、开发效率高，日常开发首选

    // 拓展补充：
    // 泛型迭代器可以适配任意数据类型，复用性极强；
    // foreach遍历本质就是调用GetEnumerator + 循环执行MoveNext取值
    #endregion
}