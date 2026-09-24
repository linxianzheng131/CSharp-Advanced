using System;
using System.Collections.Generic;
using System.Linq;

#region 题目1：数字转大写汉字（0-9对应壹贰叁肆伍陆柒捌玖拾，支持不超过三位的数）
/*
题目要求：
1. 使用字典存储0~9的数字对应的大写文字
2. 提示用户输入一个不超过三位的数，提供一个方法，返回数的大写
3. 例如：306，返回叁零陆
*/
class NumberToChinese
{
    // 存储0-9对应的大写汉字的字典
    private static readonly Dictionary<int, string> _numberDict = new Dictionary<int, string>()
    {
        {0, "零"},
        {1, "壹"},
        {2, "贰"},
        {3, "叁"},
        {4, "肆"},
        {5, "伍"},
        {6, "陆"},
        {7, "柒"},
        {8, "捌"},
        {9, "玖"}
    };

    /// <summary>
    /// 将不超过三位的数字转换为大写汉字
    /// </summary>
    /// <param name="num">输入的数字（0-999）</param>
    /// <returns>大写汉字字符串</returns>
    public static string ConvertToChinese(int num)
    {
        // 校验输入范围：不超过三位（0-999）
        if (num < 0 || num > 999)
        {
            throw new ArgumentOutOfRangeException(nameof(num), "输入数字必须在0-999之间");
        }

        string result = "";
        // 将数字转为字符串，逐位转换
        foreach (char c in num.ToString())
        {
            int digit = int.Parse(c.ToString());
            result += _numberDict[digit];
        }
        return result;
    }

    // 测试方法
    public static void RunTest()
    {
        Console.WriteLine("===== 题目1：数字转大写汉字 =====");
        Console.Write("请输入一个不超过三位的数字（0-999）：");
        if (int.TryParse(Console.ReadLine(), out int inputNum))
            // 这句代码的意思：
            // 1. 读取用户输入
            // 2. 尝试把它变成整数
            // 3. 如果变成了，把结果放进 inputNum 里，并返回 true
            // 4. 如果没变成，返回 false
            //if (int.TryParse(Console.ReadLine(), out int inputNum))
            //{
                // 只有成功了，才会进入这里，且 inputNum 里有了正确的数字
            //}
        {
            try
            {
                string chinese = ConvertToChinese(inputNum);
                Console.WriteLine($"转换结果：{chinese}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"错误：{ex.Message}");
            }
        }
        else
        {
            Console.WriteLine("输入无效，请输入合法的整数");
        }
        Console.WriteLine();
    }
}
#endregion

#region 题目2：统计字符串中每个字母出现的次数（Welcome to Unity World!，不区分大小写）
/*
题目要求：
1. 计算每个字母出现的次数 "Welcome to Unity World! "
2. 使用字典存储，最后遍历整个字典，不区分大小写
*/
class LetterCount
{
    /// <summary>
    /// 统计字符串中每个字母的出现次数（不区分大小写）
    /// </summary>
    /// <param name="inputStr">输入字符串</param>
    /// <returns>字母-次数字典</returns>
    public static Dictionary<char, int> CountLetters(string inputStr)
    {
        Dictionary<char, int> countDict = new Dictionary<char, int>();

        // 遍历字符串，只统计字母，不区分大小写
        foreach (char c in inputStr)
        {
            // 判断是否为字母
            if (char.IsLetter(c))
            {
                // 转为小写，实现不区分大小写
                char lowerC = char.ToLower(c);
                if (countDict.ContainsKey(lowerC))
                {
                    countDict[lowerC]++;
                }
                else
                {
                    countDict[lowerC] = 1;
                }
            }
        }

        return countDict;
    }

    // 测试方法
    public static void RunTest()
    {
        Console.WriteLine("===== 题目2：字母出现次数统计 =====");
        string input = "Welcome to Unity World! ";
        Console.WriteLine($"输入字符串：{input}");

        Dictionary<char, int> resultDict = CountLetters(input);

        // 遍历字典，输出结果
        Console.WriteLine("各字母出现次数（不区分大小写）：");
        // 按字母顺序排序输出
        foreach (var item in resultDict.OrderBy(kv => kv.Key))
        {
            Console.WriteLine($"{item.Key}：{item.Value}次");
        }
        Console.WriteLine();
    }
}
#endregion

#region 统一主程序入口（保证一个入口，无CS0017错误）
class Program
{
    static void Main(string[] args)
    {
        // 执行题目1测试
        NumberToChinese.RunTest();

        // 执行题目2测试
        LetterCount.RunTest();

        Console.WriteLine("程序执行完毕，按任意键退出");
        Console.ReadKey();
    }
}
#endregion