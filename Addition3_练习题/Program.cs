using System;
using System.Collections.Generic;
using System.Linq;

#region Program 主类：仅包含 Main 方法和 Lambda 示例（Lambda 只能写在方法内）
class Program
{
    static void Main(string[] args)
    {
        // ==============================================
        // 1. Lambda 表达式：(参数) => 表达式
        // 作用：定义匿名方法，赋值给委托（仅能写在方法/局部作用域内）
        // ==============================================

        // ✅ 用 => 的写法（Lambda 运算符原生用法）
        Func<int, int> lambda1 = x => x * 2;

        // ✅ 等价写法（不用 =>，用传统匿名方法）
        Func<int, int> noLambda1 = delegate (int x)
        {
            return x * 2;
        };


        // ✅ 多参数 Lambda
        Func<int, int, int> lambda2 = (a, b) => a + b;

        // ✅ 等价写法
        Func<int, int, int> noLambda2 = delegate (int a, int b)
        {
            return a + b;
        };


        // ✅ 无参数 Lambda
        Action lambda3 = () => Console.WriteLine("Hello from Lambda");

        // ✅ 等价写法
        Action noLambda3 = delegate ()
        {
            Console.WriteLine("Hello from Anonymous Method");
        };


        // ==============================================
        // 2. 表达式体方法：方法名() => 表达式
        // 作用：简写只有一行 return 的方法（C# 7.0+ 支持本地函数写法）
        // ==============================================

        // ✅ 用 => 的本地函数写法
        int Add(int a, int b) => a + b;

        // ✅ 等价普通本地函数（不用 =>）
        int Add_NoArrow(int a, int b)
        {
            return a + b;
        }


        // ==============================================
        // 6. 特别重要：字典初始化正确语法（绝对不能用 =>）
        // ==============================================

        // ✅ 正确写法1：C# 6.0+ 索引初始化器（用 =，不是 =>）
        var dict = new Dictionary<int, string>()
        {
            [1] = "A",
            [2] = "B"
        };

        // ✅ 正确写法2：传统集合初始化（兼容所有 C# 版本）
        var dict2 = new Dictionary<int, string>()
        {
            { 1, "A" },
            { 2, "B" }
        };


        // ==============================================
        // 测试调用（消除未使用变量警告）
        // ==============================================
        Console.WriteLine($"Lambda1(5) = {lambda1(5)}，等价写法结果：{noLambda1(5)}");
        Console.WriteLine($"Lambda2(3,5) = {lambda2(3, 5)}，等价写法结果：{noLambda2(3, 5)}");
        lambda3();
        noLambda3();
        Console.WriteLine($"Add(4,6) = {Add(4, 6)}，等价写法结果：{Add_NoArrow(4, 6)}");

        Console.WriteLine("\n字典遍历结果：");
        foreach (var kvp in dict)
        {
            Console.WriteLine($"Key: {kvp.Key}, Value: {kvp.Value}");
        }

        // 测试 TestClass 成员
        Console.WriteLine("\n=== TestClass 成员测试 ===");
        TestClass testObj = new TestClass();
        Console.WriteLine($"Value 属性（=> 写法）：{testObj.Value}，等价属性：{testObj.Value_NoArrow}");
        Console.WriteLine($"索引器[2]（=> 写法）：{testObj[2]}，等价索引器：{testObj.GetIndexValue_NoArrow(2)}");

        Console.WriteLine("\n程序执行完毕，按任意键退出");
        Console.ReadKey();
    }
}
#endregion

#region TestClass 类：专门存放表达式体成员（属性/构造/索引器，不能写在 Main 里）
// 类成员（属性、构造函数、索引器）必须定义在类内部，不能写在 Main 方法中
class TestClass
{
    private int[] arr = { 1, 2, 3 };

    // ==============================================
    // 3. 表达式体只读属性：属性 => 值
    // 作用：简写只有 get 且一行的属性
    // ==============================================

    // ✅ 用 => 的表达式体属性写法
    public int Value => 100;

    // ✅ 等价普通属性（不用 =>，完整 get 访问器）
    public int Value_NoArrow
    {
        get
        {
            return 100;
        }
    }


    // ==============================================
    // 4. 表达式体构造函数
    // ==============================================

    // ✅ 用 => 的表达式体构造函数写法
    public TestClass() => Console.WriteLine("TestClass 构造函数（=> 写法）执行");

    // ✅ 等价普通构造函数（不用 =>，完整代码块）
    // public TestClass()
    // {
    //     Console.WriteLine("TestClass 构造函数（传统写法）执行");
    // }


    // ==============================================
    // 5. 表达式体索引器
    // ==============================================

    // ✅ 用 => 的表达式体索引器写法
    public string this[int i] => $"索引{i}对应的值：{arr[i]}";

    // ✅ 等价普通索引器（不用 =>，完整 get 访问器）
    // 注意：C# 不允许类内重复定义同名索引器，因此用方法替代等价写法
    public string GetIndexValue_NoArrow(int i)
    {
        
            return $"索引{i}对应的值：{arr[i]}";
        
    }
}
#endregion