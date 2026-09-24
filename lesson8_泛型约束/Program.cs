namespace Lesson6_泛型约束
{
    #region 知识回顾
    //泛型类
    class TestClass<T, U>
    {
        public T t;
        public U u;

        // 泛型方法（复用类泛型参数）
        public U TestFun(T t)
        {
            return default(U);
        }

        //泛型函数（独立泛型参数）
        public V TestFun<K, V>(K k)
        {
            return default(V);
        }
    }

    // 补充基础类，用于后续约束演示
    abstract class Test1
    {
        public Test1()
        {

        }
    }

    class Test2
    {
        public Test2(int a)
        {

        }
    }

    class Test3 : Test1
    {

    }

    interface IFly
    {

    }

    interface IMove : IFly
    {

    }

    class Test4 : IFly
    {

    }
    #endregion

    #region 知识点一 什么是泛型约束
    //让泛型的类型有一定的限制
    //关键字: where
    //泛型约束一共有6种
    //1.值类型                  where 泛型字母:struct
    //2.引用类型                where 泛型字母:class
    //3.存在无参公共构造函数    where 泛型字母:new()
    //4.某个类本身或者其派生类  where 泛型字母:类名
    //5.某个接口的派生类型      where 泛型字母:接口名
    //6.另一个泛型类型本身或者派生类型 where 泛型字母:另一个泛型字母

    // where 泛型字母:(约束的类型)
    #endregion

    #region 知识点二 各泛型约束讲解

    #region 值类型约束
    class Test1<T> where T : struct
    {
        public T value;

        public void TestFun<K>(K v) where K : struct
        {

        }
    }
    #endregion

    #region 引用类型约束
    class Test2<T> where T : class
    {
        public T value;

        public void TestFun<K>(K k) where K : class
        {

        }
    }
    #endregion

    #region 公共无参构造约束
    class Test3<T> where T : new()
    {
        public T value;

        public void TestFun<K>(K k) where K : new()
        {
            // 满足new()约束，可直接实例化
            K instance = new K();
        }
    }

    // 补充：Test2因只有带参构造，无法满足new()约束
    // class Test2
    // {
    //     public Test2(int a)
    //     {
    //
    //     }
    // }
    #endregion

    #region 类约束
    class Test4<T> where T : Test1
    {
        public T value;

        public void TestFun<K>(K k) where K : Test1
        {

        }
    }

    // class Test3:Test1
    // {
    //
    // }
    #endregion

    #region 接口约束
    // interface IFly
    // {
    //
    // }
    //
    // class Test4:IFly
    // {
    //
    // }

    class Test5<T> where T : IFly
    {
        public T value;

        public void TestFun<K>(K k) where K : IFly
        {

        }
    }
    #endregion

    #region 另一个泛型约束
    class Test6<T, U> where T : U
    {
        public T value;

        public void TestFun<K, V>(K k) where K : V
        {

        }
    }
    #endregion

    #endregion

    #region 知识点三 约束的组合使用
    class Test7<T> where T : class, new()
    {
        // 同时满足：引用类型 + 无参公共构造函数
    }
    #endregion

    #region 知识点四 多个泛型有约束
    class Test8<T, K> where T : class, new() where K : struct
    {
        // 多个泛型分别用where声明各自的约束
    }
    #endregion

    #region 总结
    //泛型约束: 让类型有一定限制
    //class
    //struct
    //new()
    //类名
    //接口名
    //另一个泛型字母

    //注意:
    //1.可以组合使用
    //2.多个泛型约束 用where连接即可
    #endregion

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("泛型约束");

            #region 泛型基础使用测试
            TestClass<string, int> t = new TestClass<string, int>();
            t.t = "1`23123";
            t.u = 10;

            t.TestFun("123");
            t.TestFun<float, double>(1.4f);

            Test1<int> t1 = new Test1<int>();
            t1.TestFun<float>(1.3f);

            Test2<Random> t2 = new Test2<Random>();
            t2.value = new Random();
            t2.TestFun<object>(new object());

            // Test3<int> t3 = new Test3<Test1>(); // 原截图笔误，已修正逻辑
            #endregion

            #region 泛型约束使用测试
            // 类约束测试：Test3是Test1的派生类，满足Test4<T>的约束
            Test4<Test3> t4 = new Test4<Test3>();

            // 接口约束测试：IMove是IFly的派生接口，满足Test5<T>的约束
            Test5<IMove> t5 = new Test5<IMove>();
            //t5.value = new Test4(); // Test4实现了IFly，可赋值，注释为原截图保留

            // 泛型间约束测试：Test4实现了IFly，满足T:U的约束
            Test6<Test4, IFly> t6 = new Test6<Test4, IFly>();
            #endregion
        }
    }
}