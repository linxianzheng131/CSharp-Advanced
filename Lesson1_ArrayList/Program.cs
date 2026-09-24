using System;
// 知识点二：申明ArrayList需要引用的命名空间
using System.Collections;

namespace Lesson1_ArrayList
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("ArrayList");

            #region 练习题回顾
            // C#核心中 索引器的练习题
            // 自定义一个整型数组类，该类中有一个整型数组变量
            // 为它封装增删查改的方法
            #endregion

            #region 知识点一 ArrayList的本质
            // ArrayList是一个C#为我们封装好的类，
            // 它的本质是一个object类型的数组，
            // ArrayList类帮助我们实现了很多方法，
            // 比如数组的增删查改
            #endregion

            #region 知识点二 申明
            // 需要引用命名空间using System.Collections;
            ArrayList array = new ArrayList();
            #endregion

            #region 知识点三 增删查改
            #region 增
            array.Add(1);
            array.Add("123");
            array.Add(true);
            array.Add(new object());
            // 注：Test类需自行定义，示例中仅保留语法
            // array.Add(new Test()); 
            array.Add(1);

            ArrayList array2 = new ArrayList();
            array2.Add(123);
            // 范围增加（批量增加 把另一个list容器里面的内容加到后面）
            array.AddRange(array2);

            // 在指定索引位置插入元素
            array.Insert(1, "12345676");
            Console.WriteLine(array[1]);
            #endregion

            #region 删
            // 移除指定元素 从头找 找到删


            //删1是从头开始，找到第一个删完结束，不会全删
            //找  也是
            //其他容量类也差不多


            array.Remove(1);
            // 移除指定位置的元素
            array.RemoveAt(2);
            // 清空所有元素
            array.Clear();
            #endregion

            #region 查
            // 得到指定位置的元素
            Console.WriteLine(array[0]);

            // 查看元素是否存在
            if (array.Contains("1234"))
            {
                Console.WriteLine("存在123");
            }

            // 正向查找元素位置
            // 找到的返回值 是位置 找不到 返回值 是-1
            int index = array.IndexOf(true);
            Console.WriteLine(index);
            Console.WriteLine(array.IndexOf(false));

            // 反向查找元素位置
            // 返回时从头开始的索引数
            index = array.LastIndexOf(true);
            Console.WriteLine(index);
            #endregion

            #region 改
            // 直接通过索引赋值修改元素
            array[0] = "修改后的元素";
            #endregion

            #region 遍历
            // 获取实际元素个数（Count）
            Console.WriteLine(array.Count);
            // 获取容量（Capacity，底层数组长度）
            Console.WriteLine(array.Capacity);

            Console.WriteLine("********************");
            // for循环遍历
            for (int i = 0; i < array.Count; i++)
            {
                Console.WriteLine(array[i]);
            }

            Console.WriteLine("********************");
            // 迭代器遍历（foreach）
            foreach (object item in array)
            {
                Console.WriteLine(item);
            }
            #endregion
            #endregion

            #region 知识点四 装箱拆箱
            // ArrayList本质上是一个可以自动扩容的object数组，
            // 由于用万物之父来存储数据，自然存在装箱拆箱。
            // 当往其中进行值类型存储时就是在装箱，当将值类型对象取出来转换使用时，就存在拆箱。
            // 所以ArrayList尽量少用，之后我们会学习更好的数据容器。

            int k = 1;
            array[0] = k; // 装箱：将值类型int转为引用类型object
            k = (int)array[0]; // 拆箱：将引用类型object转回值类型int
            #endregion
        }
    }

    // 练习题：自定义整型数组类，封装增删查改
    class IntArray
    {
        // 底层存储数组（房间容量）
        private int[] array;
        // 容量
        private int capacity;
        // 当前实际元素数量
        private int length;

        // 构造函数
        public IntArray()
        {
            // 初始化容量（可自定义初始容量）
            capacity = 4;
            array = new int[capacity];
            length = 0;
        }

        #region 增
        public void Add(int value)
        {
            // 扩容逻辑（容量不足时翻倍）
            if (length >= capacity)
            {
                capacity *= 2;
                Array.Resize(ref array, capacity);
            }
            array[length++] = value;
        }
        #endregion

        #region 删
        // 移除指定值（从头删第一个匹配项）
        public void Remove(int value)
        {
            int index = IndexOf(value);
            if (index != -1)
            {
                RemoveAt(index);
            }
        }

        // 移除指定索引元素
        public void RemoveAt(int index)
        {
            if (index < 0 || index >= length)
                return;

            // 后续元素前移
            for (int i = index; i < length - 1; i++)
            {
                array[i] = array[i + 1];
            }
            length--;
        }
        #endregion

        #region 查
        // 正向查找索引
        public int IndexOf(int value)
        {
            for (int i = 0; i < length; i++)
            {
                if (array[i] == value)
                    return i;
            }
            return -1;
        }

        // 反向查找索引
        public int LastIndexOf(int value)
        {
            for (int i = length - 1; i >= 0; i--)
            {
                if (array[i] == value)
                    return i;
            }
            return -1;
        }

        // 检查是否包含元素
        public bool Contains(int value)
        {
            return IndexOf(value) != -1;
        }
        #endregion

        #region 改（索引器）
        public int this[int index]
        {
            get
            {
                if (index < 0 || index >= length)
                    throw new IndexOutOfRangeException();
                return array[index];
            }
            set
            {
                if (index < 0 || index >= length)
                    throw new IndexOutOfRangeException();
                array[index] = value;
            }
        }
        #endregion
    }
}