using System;

namespace Lesson5_泛型
{
    #region 知识点一 泛型是什么
    //泛型实现了类型参数化，达到代码重用目的
    //通过类型参数化来实现同一份代码上操作多种类型
    //泛型相当于类型占位符
    //定义类或方法时使用替代符代表变量类型
    //当真正使用类或者方法时再具体指定类型
    #endregion

    #region 知识点二 泛型分类
    //泛型类和泛型接口
    //基本语法：
    //class 类名<泛型占位字母>
    //interface 接口名<泛型占位字母>

    //泛型函数
    //基本语法：函数名<泛型占位字母>(参数列表)
    //注意：泛型占位字母可以有多个，用逗号分开
    #endregion

    #region 知识点三 泛型类和接口
    //单泛型参数类
    class TestClass<T>
    {
        public T value;
    }

    //多泛型参数类（支持多个不同占位符）
    class TestClass2<T1, T2, K, M, LL, Key, Value>
    {
        public T1 value1;
        public T2 value2;
        public K value3;
        public M value4;
        public LL value5;
        public Key value6;
        public Value value7;
    }

    //泛型接口（示例）
    interface TestInterface<T>
    {
        T Value
        {
            get;
            set;
        }
    }

    //泛型接口实现类
    class Test : TestInterface<int>
    {
        //显式实现接口属性（原代码补全）
        public int Value
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }
    }
    #endregion

    #region 知识点四 泛型方法
    //1.普通类中的泛型方法
    class Test2
    {
        //泛型返回值方法
        public T TestFun<T>(string v)
        {
            return default(T);
        }

        //单参数泛型方法
        public void TestFun<T>(T value)
        {
            Console.WriteLine(value);
        }

        //无参数泛型方法（含逻辑处理）
        public void TestFun<T>()
        {
            //用泛型类型 在里面做一些逻辑处理
            T t = default(T);
        }

        //多泛型参数方法
        public void TestFun<T, K, M>(T t, K k, M m)
        {
            //可实现多类型参数的通用逻辑
            Console.WriteLine($"T:{t}, K:{k}, M:{m}");
        }
    }

    //2.泛型类中的泛型方法（重点区分：类泛型 vs 方法泛型）
    class Test3<T>
    {
        public T value;

        //普通方法（使用类的泛型T）
        public void TestFun(T t)
        {
            //T是类声明时指定的类型，此处无法动态变更
            Console.WriteLine(t);
        }

        //泛型方法（自身定义泛型K，与类的T无关）
        public void TestFun<K>(K k)
        {
            Console.WriteLine(k);
        }
    }
    #endregion

    #region 知识点五 泛型的作用
    //1.不同类型对象的相同逻辑处理就可以选择泛型
    //2.使用泛型可以一定程度避免装箱拆箱
    //举例：优化ArrayList（非泛型集合存在装箱拆箱，泛型集合无此问题）
    class ArrayList<T>
    {
        private T[] array;

        public void Add(T value)
        {
            //添加逻辑（示例）
        }

        public void Remove(T value)
        {
            //删除逻辑（示例）
        }
    }
    #endregion

    #region 总结
    //1.申明泛型时 它只是一个类型的占位符
    //2.泛型真正起作用的时候 是在使用它的时候
    //3.泛型占位字母可以有n个用逗号分开
    //4.泛型占位字母一般是大写字母
    //5.不确定泛型类型时 获取默认值 可以使用default(占位字符)
    //6.看到<>包括的字母 那肯定是泛型
    #endregion

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("泛型");

            #region 泛型类使用示例
            //单泛型类测试
            TestClass<int> t = new TestClass<int>();
            t.value = 10;
            Console.WriteLine(t.value);

            TestClass<string> t2 = new TestClass<string>();
            t2.value = "123123";
            Console.WriteLine(t2.value);

            //多泛型类测试（补全参数）
            TestClass2<int, string, float, double, TestClass<int>, uint, string> t3 = new TestClass2<int, string, float, double, TestClass<int>, uint, string>();
            t3.value1 = 1;
            t3.value2 = "test";
            t3.value3 = 1.5f;
            #endregion

            #region 泛型方法使用示例
            //普通类泛型方法
            Test2 test2 = new Test2();
            test2.TestFun<string>("123123"); //调用泛型方法
            test2.TestFun(10); //类型推断调用

            //泛型类中的泛型方法
            Test3<int> test3 = new Test3<int>();
            test3.value = 100;
            test3.TestFun(200); //调用类普通方法（T=int）
            test3.TestFun<string>("泛型方法测试"); //调用类泛型方法（K=string，与T无关）
            #endregion

            Console.WriteLine("泛型演示结束");
        }
    }
}