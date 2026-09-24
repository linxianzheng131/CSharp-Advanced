#define Fun
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Lesson21_特性
{

    #region 知识点一 特性是什么
    //特性是一种允许我们向程序的程序集添加元数据的语言结构
    //它是用于保存程序结构信息的某种特殊类型的类

    //特性提供功能强大的方法以将声明信息与 C# 代码（类型、方法、属性等）相关联。
    //特性与程序实体关联后，即可在运行时使用反射查询特性信息

    //特性的目的是告诉编译器把程序结构的某组元数据嵌入程序集中
    //它可以放置在几乎所有的声明中（类、变量、函数等等声明）

    //说人话：
    //特性本质是个类
    //我们可以利用特性类为元数据添加额外信息
    //比如一个类、成员变量、成员方法等等为他们添加更多的额外信息
    //之后可以通过反射来获取这些额外信息
    #endregion

    #region 知识点二 自定义特性
    // 自定义特性需要继承特性基类 Attribute
    public class MyCustomAttribute : Attribute
    {
        //特性中的成员 一般根据需求来写
        public string info;

        //构造函数 用于初始化特性参数
        public MyCustomAttribute(string info)
        {
            this.info = info;
        }

        //特性中可以定义方法
        public void TestFun()
        {
            Console.WriteLine("特性的方法");
        }
    }
    #endregion

    #region 知识点四 限制自定义特性的使用范围
    //通过为特性类 加特性 限制其使用范围
    [AttributeUsage(/* 位或 这里发挥和的作用 底层二进制识别表示为和*/
        AttributeTargets.Class | AttributeTargets.Struct, //参数一：特性能够用在哪些地方
        AllowMultiple = true,                             //参数二：是否允许多个特性实例用在同一个目标上 加了之后同个特性可以有3个以上
        Inherited = true)]                                //参数三：特性是否能被派生类和重写成员继承
    public class MyCustom2Attribute : Attribute
    {

    }
    #endregion

    #region 知识点三 特性的使用
    //基本语法：
    //[特性名(参数列表)]
    //本质上 就是在调用特性类的构造函数
    //写在哪里？
    //类、函数、变量上一行，表示他们具有该特性信息

    [MyCustom("这个是我自己写的一个用于计算的类")]
    class MyClass
    {
        [MyCustom("这是一个成员变量")]
        public int value;

        [MyCustom("这是一个用于计算加法的函数")]
        public void TestFun([MyCustom("函数参数")] int a)
        {

        }
    }
    #endregion

    #region 知识点五 系统自带特性—过时特性
    //过时特性
    //Obsolete
    //用于提示用户 使用的方法等成员已经过时 建议使用新方法
    //一般加在函数前的特性
    class TestClass
    {
        //参数一：调用过时方法时 提示的内容
        //参数二：true-使用该方法时会报错 false-使用该方法时直接警告
        [Obsolete("OldSpeak方法已经过时了，请使用Speak方法", false)]
        public void OldSpeak(string str)
        {
            Console.WriteLine(str);
        }

        public void Speak()
        {

        }

        //调用者信息特性的使用示例
        public void SpeakCaller(string str,
            [CallerFilePath] string fileName = "",
            [CallerLineNumber] int line = 0,
            [CallerMemberName] string target = "")//这里实例话的三个参数都是 占位参数 他们的值会在编译器编译时自动替换成实际的调用信息
        {
            Console.WriteLine(str);
            Console.WriteLine("调用文件路径：" + fileName);
            Console.WriteLine("调用代码行号：" + line);
            Console.WriteLine("调用成员名称：" + target);
        }
    }
    #endregion

    #region 知识点六 系统自带特性—调用者信息特性
    //哪个文件调用？
    //CallerFilePath特性
    //哪一行调用？
    //CallerLineNumber特性
    //哪个函数调用？
    //CallerMemberName特性

    //需要引用命名空间 using System.Runtime.CompilerServices;
    //一般作为函数参数的特性
    #endregion

    #region 知识点七 系统自带特性—条件编译特性
    //条件编译特性
    //Conditional
    //它会和预处理指令 #define配合使用

    //需要引用命名空间using System.Diagnostics;
    //主要可以用在一些调试代码上
    //有时想执行有时不想执行的代码
    #endregion

    #region 知识点八 系统自带特性—外部Dll包函数特性
    //DllImport
    //用来标记非.Net(C#)的函数，表明该函数在一个外部的DLL中定义。
    //一般用来调用 C或者C++的DLL包写好的方法
    //需要引用命名空间 using System.Runtime.InteropServices
    #endregion

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("========== 特性知识点演示 ==========\n");

            // 反射获取自定义特性信息
            MyClass mc = new MyClass();
            Type t = mc.GetType();

            // 判断类型是否定义了指定特性
            //参数一:特性的类型
            //参数二:是否搜索继承链(属性和事件忽略此参数)
            //属性和事件不考虑继承链 虽然他们能被继承
            //但是属性继承是完全覆盖 不需要看父代 事件继承是完全不覆盖 同步进行不太能看为继承 所以一般不考虑继承链 
            if (t.IsDefined(typeof(MyCustomAttribute), false))
            {
                Console.WriteLine("该类型应用了MyCustom自定义特性");
            }

            // 获取所有自定义特性并遍历
            object[] array = t.GetCustomAttributes(true);
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] is MyCustomAttribute attr)
                {
                    Console.WriteLine("特性备注信息：" + attr.info);
                    attr.TestFun();
                }
            }

            Console.WriteLine("\n---------- 过时特性演示 ----------");
            TestClass tc = new TestClass();
            tc.OldSpeak("测试过时方法调用");

            Console.WriteLine("\n---------- 调用者信息特性演示 ----------");
            tc.SpeakCaller("测试调用者信息");

            Console.WriteLine("\n---------- 条件编译特性演示 ----------");
            Fun();
        }

        [Conditional("Fun")]
        static void Fun()
        {
            Console.WriteLine("Conditional条件编译方法执行成功");
        }

        [DllImport("Test.dll")]
        public static extern int Add(int a, int b);

        //java ：extend 延申 implement 实施（接口实现）
    }

    /*
     * 总结：
     * 特性是用于 为元数据再添加更多的额外信息（类、变量、方法、参数等等）
     * 可以通过反射运行时获取这些额外数据，做逻辑判断、框架配置、权限控制等处理
     * 自定义特性固定写法：继承 Attribute 基类，可添加构造函数、字段、方法
     * 可用 [AttributeUsage] 限制特性的适用位置、是否重复、是否继承
     * 
     * 常用系统自带特性：
     * 1. [Obsolete] 标记过时成员，提示警告/报错
     * 2. Caller系列 自动获取调用文件、行号、方法名
     * 3. [Conditional] 配合宏定义，控制方法是否执行
     * 4. [DllImport] 导入外部C/C++ DLL函数
     * 
     * 实际开发/Unity中大量用到特性：序列化、编辑器拓展、框架路由、配置标记等
     */
}         