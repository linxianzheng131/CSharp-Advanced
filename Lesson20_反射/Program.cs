using System;
using System.Reflection;

namespace Lesson20_反射
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("===== C#反射学习 =====");
            Console.WriteLine("反射的核心：程序运行时，查看/操作自身或其他程序集的元数据");

            // =====================================
            // 知识点回顾（编译器、程序集、元数据）
            // =====================================
            #region 知识点回顾
            // 编译器是一种翻译程序
            // 它用于将源语言程序翻译为目标语言程序
            // 源语言程序：某种程序设计语言写成的，比如C#、C、C++、Java等语言写的程序
            // 目标语言程序：二进制数表示的伪机器代码写的程序

            // 程序集是经由编译器编译得到的，供进一步编译执行的那个中间产物
            // 在WINDOWS系统中，它一般表现为后缀为 .dll（库文件）或者是 .exe（可执行文件）的格式
            // 说人话：程序集就是我们写的一个代码集合，我们现在写的所有代码，最终都会被编译器翻译为一个程序集供别人使用
            // 比如一个代码库文件（dll）或者一个可执行文件（exe）

            // 元数据就是用来描述数据的数据
            // 这个概念不仅仅用于程序上，在别的领域也有元数据
            // 说人话：程序中的类，类中的函数、变量等等信息就是程序的元数据
            // 有关程序以及类型的数据被称为元数据，它们保存在程序集中

            // 反射的概念
            // 程序正在运行时，可以查看其它程序集或者自身的元数据
            // 一个运行的程序查看本身或者其它程序的元数据的行为就叫做反射
            // 说人话：在程序运行时，通过反射可以得到其它程序集或者自己程序集代码的各种信息
            // 类，函数，变量，对象等等，实例化它们，执行它们，操作它们

            // 反射的作用
            // 因为反射可以在程序编译后获得信息，所以它提高了程序的拓展性和灵活性
            // 1. 程序运行时得到所有元数据，包括元数据的特性
            // 2. 程序运行时，实例化对象，操作对象
            // 3. 程序运行时创建新对象，用这些对象执行任务
            #endregion

            // =====================================
            // 知识点五：语法相关（Type、Assembly、Activator）
            // =====================================
            #region Type（反射的核心）
            // Type（类的信息类）
            // 它是反射功能的基础！
            // 它是访问元数据的主要方式
            // 使用 Type 的成员获取有关类型声明的信息
            // 有关类型的成员（如构造函数、方法、字段、属性和类的事件）

            // 获取Type的三种方式
            // 1. 万物之父object中的 GetType() 可以获取对象的Type
            int a = 42;
            Type type = a.GetType();
            Console.WriteLine($"\n1. 通过对象实例.GetType()获取Type: {type}");

            // 2. 通过typeof关键字 传入类名 也可以得到对象的Type
            Type type2 = typeof(int);
            Console.WriteLine($"2. 通过typeof关键字获取Type: {type2}");

            // 3. 通过类的名字 也可以获取类型（注意：类名必须包含命名空间，不然找不到）
            Type type3 = Type.GetType("System.Int32");
            Console.WriteLine($"3. 通过完全限定名获取Type: {type3}");
            #endregion

            #region 得到类的程序集信息
            // 可以通过Type得到类型所在程序集信息
            Console.WriteLine($"\ntype1所在程序集: {type.Assembly}");
            Console.WriteLine($"type2所在程序集: {type2.Assembly}");
            Console.WriteLine($"type3所在程序集: {type3.Assembly}");
            #endregion

            #region 获取类中的所有公共成员
            // 首先得到Type
            Type t = typeof(Test);
            // 然后得到所有公共成员
            // 需要引用命名空间 using System.Reflection;
            MemberInfo[] infos = t.GetMembers();
            Console.WriteLine("\n===== Test类的所有公共成员 =====");
            for (int i = 0; i < infos.Length; i++)
            {
                Console.WriteLine(infos[i]);
            }
            #endregion

            #region 获取类的公共构造函数并调用
            Console.WriteLine("\n===== 反射调用构造函数 =====");
            // 1. 获取所有构造函数
            // 2. 获取其中一个构造函数 并执行
            // 得构造函数传入 Type数组 数组中内容按顺序是参数类型
            // 执行构造函数传入 object数组 表示按顺序传入的参数

            // 2-1 得到无参构造
            ConstructorInfo info = t.GetConstructor(new Type[0]);
            // 执行无参构造 无参构造 没有参数 传null
            Test obj = info.Invoke(null) as Test;
            Console.WriteLine($"无参构造实例的j值: {obj.j}");

            // 2-2 得到有参构造（带int参数）
            ConstructorInfo info2 = t.GetConstructor(new Type[] { typeof(int) });
            obj = info2.Invoke(new object[] { 2 }) as Test;
            Console.WriteLine($"int构造实例的str值: {obj.str}");

            // 2-3 得到全参构造（带int+string参数）
            ConstructorInfo info3 = t.GetConstructor(new Type[] { typeof(int), typeof(string) });
            obj = info3.Invoke(new object[] { 4, "44444" }) as Test;
            Console.WriteLine($"全参构造实例的str值: {obj.str}");
            #endregion

            #region 获取类的公共成员变量
            Console.WriteLine("\n===== 反射操作字段 =====");
            // 1. 得到所有成员变量
            FieldInfo[] fieldInfos = t.GetFields();
            for (int i = 0; i < fieldInfos.Length; i++)
            {
                Console.WriteLine(fieldInfos[i]);
            }

            // 2. 得到指定名称的公共成员变量
            FieldInfo infoJ = t.GetField("j");
            Console.WriteLine($"\n指定字段信息: {infoJ}");

            // 3. 通过反射获取和设置对象的值
            Test test = new Test();
            test.j = 99;
            test.str = "2222";

            // 3-1 通过反射 获取对象的某个变量的值
            Console.WriteLine($"直接赋值后，反射获取的j值: {infoJ.GetValue(test)}");

            // 3-2 通过反射 设置指定对象的某个变量的值
            infoJ.SetValue(test, 100);
            Console.WriteLine($"反射修改后，j值变为: {infoJ.GetValue(test)}");
            #endregion

            #region 获取类的公共成员方法
            Console.WriteLine("\n===== 反射调用方法 =====");
            // 通过Type类中的 GetMethod方法 得到类中的方法
            // MethodInfo 是方法的反射信息
            Type strType = typeof(string);

            // 1. 获取所有公共方法
            MethodInfo[] methods = strType.GetMethods();
            for (int i = 0; i < methods.Length; i++)
            {
                Console.WriteLine(methods[i]);
            }

            // 1-1 如果存在方法重载，用Type数组表示参数类型，获取指定方法
            MethodInfo subStr = strType.GetMethod("Substring", new Type[] { typeof(int), typeof(int) });

            // 2. 调用该方法
            // 注意：如果是静态方法 Invoke中的第一个参数传null即可
            string str = "Hello,World!";
            object result = subStr.Invoke(str, new object[] { 7, 5 });
            Console.WriteLine($"\n调用Substring(7,5)的结果: {result}");
            #endregion

            #region 其他反射相关（枚举、事件、接口、属性）
            // 得枚举
            // GetEnumName
            // GetEnumNames

            // 得事件
            // GetEvent
            // GetEvents

            // 得接口
            // GetInterface
            // GetInterfaces

            // 得属性
            // GetProperty
            // GetProperties
            // 等等
            #endregion

            #region Activator（快速实例化对象）
            Console.WriteLine("\n===== Activator快速实例化 =====");  
            // 用于快速实例化对象的类
            // 用于将Type对象快捷实例化为对象
            // 先得到Type，然后快速实例化一个对象

            Type testType = typeof(Test);
            // 1. 无参构造
            Test testObj = Activator.CreateInstance(testType) as Test;
            Console.WriteLine($"无参实例的str值: {testObj.str}");

            // 2. 有参数构造（带int）
            testObj = Activator.CreateInstance(testType, 99) as Test;
            Console.WriteLine($"int参数实例的j值: {testObj.j}");

            // 3. 全参数构造（带int+string）
            testObj = Activator.CreateInstance(testType, 55, "111222") as Test;
            Console.WriteLine($"全参实例的j值: {testObj.j}");
            #endregion

            #region Assembly（加载外部程序集）
            Console.WriteLine("\n===== Assembly加载外部程序集 =====");
            // 程序集类
            // 主要用来加载其它程序集，加载后才能用Type来使用其它程序集中的信息
            // 如果想要使用不是自己程序集中的内容，需要先加载程序集（比如dll文件）
            // 简单的把库文件看成一种代码仓库，它提供给使用者一些可以直接拿来用的变量、函数或类

            // 三种加载程序集的函数
            // 1. Assembly.Load("程序集名称");
            // 2. Assembly.LoadFrom("包含程序集清单的文件的名称或路径");
            // 3. Assembly.LoadFile("要加载的文件的完全限定路径");

            // 示例：加载外部程序集（需替换为你的实际dll路径）
            /*
            Assembly assembly = Assembly.LoadFrom(@"C:\Users\MECHREVO\Desktop\CSharp进阶教学\Lesson18_练习题\bin\Debug\netcoreapp3.1\Lesson18_练习题.dll");
            // 2. 再加载程序集中的一个类对象，之后才能使用反射
            Type icon = assembly.GetType("Lesson18_练习题.Icon");
            MemberInfo[] members = icon.GetMembers();
            for (int i = 0; i < members.Length; i++)
            {
                Console.WriteLine(members[i]);
            }

            // 通过反射实例化一个icon对象
            // 首先得到枚举Type，来得到可以传入的参数
            Type moveDir = assembly.GetType("Lesson18_练习题.E_MoveDir");
            FieldInfo right = moveDir.GetField("Right");
            // 直接实例化对象
            object iconObj = Activator.CreateInstance(icon, 10, 5, right.GetValue(null));
            // 得到对象中的方法，通过反射
            MethodInfo move = icon.GetMethod("Move");
            MethodInfo draw = icon.GetMethod("Draw");
            MethodInfo clear = icon.GetMethod("Clear");

            Console.Clear();
            while (true)
            {
                System.Threading.Thread.Sleep(1000);
                clear.Invoke(iconObj, null);
                move.Invoke(iconObj, null);
                draw.Invoke(iconObj, null);
            }
            */

            //类库文件的创建方法：新建一个类库项目，写好代码，编译后在bin文件夹下找到生成的dll文件
            #endregion

            #region 反射总结
            Console.WriteLine("\n===== 反射总结 =====");
            Console.WriteLine("// 反射");
            Console.WriteLine("// 在程序运行时，通过反射可以得到其他程序集或者自己的程序集代码的各种信息");
            Console.WriteLine("// 类、函数、变量、对象等等，实例化他们，执行他们，操作他们");

            Console.WriteLine("\n// 关键类");
            Console.WriteLine("// Type");
            Console.WriteLine("// Assembly");
            Console.WriteLine("// Activator");

            Console.WriteLine("\n// 对于我们的意义");
            Console.WriteLine("// 在初中级阶段，基本不会使用反射");
            Console.WriteLine("// 所以目前对于大家来说，了解反射可以做什么就行");
            Console.WriteLine("// 很长时间内都不会用到反射相关知识点");

            Console.WriteLine("\n// 为什么要学反射");
            Console.WriteLine("// 为了之后学习Unity引擎的基本工作原理做铺垫");
            Console.WriteLine("// Unity引擎的基本工作机制，就是建立在反射的基础上");
            #endregion

            Console.WriteLine("\n===== 反射学习笔记运行完毕 =====");
            Console.ReadKey();
        }
    }

    // 测试用的Test类
    class Test
    {
        private int i = 1;
        public int j = 0;
        public string str = "123";

        // 无参构造
        public Test() { }

        // 带int参数的构造
        public Test(int i)
        {
            this.i = i;
        }

        // 带int+string参数的构造（链式调用）
        public Test(int i, string str) : this(i)
        {
            this.str = str;
        }

        // 测试方法
        public void Speak()
        {
            Console.WriteLine(i);
        }
    }
}