using System;
using System.Collections.Generic;

namespace Lesson23_特殊语法
{
    class Person
    {
        private int money;
        public bool sex;

        // 知识点补充：属性的表达式简化写法（=> 语法）
        public string Name
        {
            get => "唐老狮";
            set => sex = true;
        }

        // 构造函数
        public Person(int money)
        {
            this.money = money;
        }

        // 方法的表达式简化写法
        public int Add(int x, int y) => x + y;
        public void Speak(string str) => Console.WriteLine(str);
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("特殊语法");

            #region 知识点一 var隐式类型
            // var是一种特殊的变量类型
            // 它可以用来表示任意类型的变量
            // 注意:
            // 1. var不能作为类的成员，只能用于临时变量申明时
            //    也就是 一般写在函数语句块中
            // 2. var必须初始化
            var i = 5;
            var s = "123";
            var array = new int[] { 1, 2, 3, 4 };
            var list = new List<int>();
            #endregion

            #region 知识点二 设置对象初始值
            // 创建对象时，可以直接在大括号里给属性赋值，无需调用构造函数单独赋值
            //先进行构造函数赋值，再进行属性赋值 （如果构造函数和属性有同名参数，属性赋值会覆盖构造函数赋值）
            Person p = new Person(100)
            {
                sex = true,
                // 也可以给自动属性赋值，如 public int Age {get;set;}
                // Age = 18
            };
            #endregion

            #region 知识点三 设置集合初始值
            // 创建集合/字典时，可以直接在大括号里添加元素，无需反复调用Add方法
            List<int> intList = new List<int>() { 1, 2, 3, 4, 5 };
            Dictionary<int, string> dic = new Dictionary<int, string>()
            {
                { 1, "123" },
                { 2, "222" }
            };
            #endregion

            #region 知识点四 匿名类型
            // var变量可以申明为自定义的匿名类型
            // 匿名类型由编译器自动生成类，只能用var接收，且属性是只读的
            var v = new { age = 10, money = 11, name = "小明" };
            Console.WriteLine(v.age);
            Console.WriteLine(v.name);
            #endregion

            #region 知识点五 可空类型 & 空值运算符（?.）
            // 1. 值类型是不能赋值为 null 的
            // int c = null;  // 报错
            // 2. 申明时 在值类型后面加? 可以赋值为空
            int? c = 3;
            // 可空类型本质上是一个结构体
            //是通过Nullable<T>结构体实现的，T是值类型，包含一个值和一个是否有值的标志
            //是通过这个是否有值的标志来判断是否为null的，而不是直接判断值是否为null
            //也就是说 可空类型的值为null时，不是值为null，而是有一个特殊的状态，表示没有值
            // 3. 判断是否为空
            if (c.HasValue)
            {
                Console.WriteLine(c);
                Console.WriteLine(c.Value);
            }
            // 4. 安全获取可空类型值
            int? value = null;
            // 4-1. 如果为空，默认返回值类型的默认值（int返回0，bool返回false等）
            Console.WriteLine(value.GetValueOrDefault());
            // 4-2. 也可以指定一个默认值
            Console.WriteLine(value.GetValueOrDefault(100));

            // 其他可空值类型
            float? f = null;
            double? d = null;

            // 空值运算符（?.）：语法糖，自动判断是否为null，为null时不执行后续代码也不报错
            object o = null;
            if (o != null)
            {
                Console.WriteLine(o.ToString());
            }
            // 等价于上面的if判断，o为null时不会调用ToString()
            Console.WriteLine(o?.ToString());

            // 数组的空值运算符
            int[] arrryInt = null;
            Console.WriteLine(arrryInt?[0]);

            // 委托/事件的空值运算符（避免NullReferenceException）
            Action action = null;
            // if (action != null)
            // {
            //     action();
            // }
            // 等价于上面的if判断，action为null时不会调用Invoke()
            action?.Invoke();
            #endregion

            #region 知识点六 空合并操作符（??）
            // 空合并操作符 ??
            // 语法：左边值 ?? 右边值
            // 逻辑：如果左边值为null，就返回右边值；否则返回左边值
            // 只要是可以为null的类型都能用（可空值类型、引用类型、字符串等）
            int? intV = null;
            // 等价于：int intI = intV == null ? 100 : intV.Value;
            int intI = intV ?? 100;
            Console.WriteLine(intI);

            string str = null;
            str = str ?? "hahah";
            Console.WriteLine(str);
            #endregion

            #region 知识点七 内插字符串
            // 关键符号：$
            // 用$来构造字符串，让字符串中可以拼接变量，无需使用+或string.Format
            string name = "唐老狮";
            int age = 18;
            Console.WriteLine($"好好学习, {name}, 年龄: {age}");
            #endregion

            #region 知识点八 单句逻辑简略写法
            // 当循环或者if语句中只有 一句代码时，大括号可以省略
            if (true)
                Console.WriteLine("123123");

            for (int j = 0; j < 10; j++)
                Console.WriteLine(j);

            while (true)
                Console.WriteLine("123123");
            #endregion
        }
    }
}