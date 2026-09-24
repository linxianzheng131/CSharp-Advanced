using System;

#region 泛型方法实现：判断类型并返回名称与字节数
/// <summary>
/// 泛型方法工具类
/// 功能：判断传入的泛型类型，返回类型名称与占有的字节数
/// 仅处理 int、char、float、string，其余返回“其它类型”
/// </summary>
public static class TypeChecker
{
    /// <summary>
    /// 泛型方法：判断类型并返回描述信息
    /// </summary>
    /// <typeparam name="T">待判断的类型</typeparam>
    /// <returns>类型描述字符串</returns>
    public static string GetTypeDescription<T>()
    {
        // 获取当前泛型参数的类型对象
        Type type = typeof(T);

        // 1. 判断是否为int（整形，4字节）
        if (type == typeof(int))
        {
            return string.Format("整形，{0}字节", sizeof(int));
        }
        // 2. 判断是否为char（字符，2字节）
        else if (type == typeof(char))
        {
            return string.Format("字符，{0}字节", sizeof(char));
        }
        // 3. 判断是否为float（单精度浮点数，4字节）
        else if (type == typeof(float))
        {
            return string.Format("单精度浮点数，{0}字节", sizeof(float));
        }
        // 4. 判断是否为string（字符串，引用类型，字节数取决于运行时环境）
        else if (type == typeof(string))
        {
            return "字符串，引用类型（4字节/8字节指针）";
        }
        // 5. 其它类型
        else
        {
            return "其它类型";
        }
    }
}
#endregion

#region 测试主程序
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("===== 泛型类型判断测试 =====");

        // 测试int类型
        string resultInt = TypeChecker.GetTypeDescription<int>();
        Console.WriteLine($"int -> {resultInt}");

        // 测试char类型
        string resultChar = TypeChecker.GetTypeDescription<char>();
        Console.WriteLine($"char -> {resultChar}");

        // 测试float类型
        string resultFloat = TypeChecker.GetTypeDescription<float>();
        Console.WriteLine($"float -> {resultFloat}");

        // 测试string类型
        string resultString = TypeChecker.GetTypeDescription<string>();
        Console.WriteLine($"string -> {resultString}");

        // 测试其它类型（double）
        string resultOther = TypeChecker.GetTypeDescription<double>();
        Console.WriteLine($"double -> {resultOther}");

        Console.ReadLine(); // 防止控制台闪退
    }
}
#endregion