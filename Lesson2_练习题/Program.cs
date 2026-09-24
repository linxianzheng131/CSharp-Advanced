using System;
using System.Collections;

#region 知识点 - 栈(Stack)的存储规则
/// <summary>
/// 栈（Stack）是一种**后进先出（LIFO, Last In First Out）**的线性数据结构
/// 核心存储规则与特性：
/// 1. 操作限制：仅允许在栈的**一端（栈顶，Top）**进行插入（Push）和删除（Pop）操作
///    - 栈底（Bottom）：固定不动，无法直接操作
/// 2. 存储顺序：先入栈的元素在下方，后入栈的元素在上方
///    - 例：Push(1) → Push(2) → Push(3)，栈内顺序为 [1, 2, 3]（3在栈顶）
/// 3. 核心方法：
/// - Push(object value)：将元素压入栈顶
/// - Pop()：移除并返回栈顶元素（栈空时调用会抛异常）
/// - Peek()：返回栈顶元素但不移除（栈空时调用会抛异常）
/// - Count：获取栈中元素的数量
/// 4. 常见实现：.NET 中使用 Stack（非泛型，可存任意类型）、Stack<T>（泛型，类型安全）
/// 5. 适用场景：需要“后进先出”逻辑的场景，如：进制转换、函数调用栈、撤销操作、括号匹配等
/// </summary>
public class StackStorageRule
{
    /// <summary>
    /// 演示栈的存储规则：压入/弹出元素，验证后进先出特性
    /// </summary>
    public static void DemoStackRule()
    {
        // 1. 初始化栈（非泛型 Stack，可存储任意类型）
        Stack stack = new Stack();

        // 2. 压入元素（Push）：遵循“后进先出”，新元素在栈顶
        Console.WriteLine("===== 栈的存储规则演示 =====");
        stack.Push(10);  // 栈底 → [10]
        stack.Push(20);  // 栈顶 → [10, 20]
        stack.Push(30);  // 栈顶 → [10, 20, 30]
        Console.WriteLine($"压入元素后，栈中元素数量：{stack.Count}"); // 输出：3

        // 3. 查看栈顶元素（Peek）：不移除，仅获取
        Console.WriteLine($"当前栈顶元素：{stack.Peek()}"); // 输出：30（最后压入的元素）

        // 4. 弹出元素（Pop）：移除栈顶元素，返回被弹出的值
        Console.WriteLine("弹出元素：" + stack.Pop()); // 输出：30（栈顶元素被弹出）
        Console.WriteLine($"弹出后，栈顶元素：{stack.Peek()}"); // 输出：20（剩余栈顶为20）
        Console.WriteLine($"弹出后，栈中元素数量：{stack.Count}"); // 输出：2

        // 5. 清空栈后演示
        stack.Clear();
        Console.WriteLine($"清空后，栈中元素数量：{stack.Count}"); // 输出：0
    }
}
#endregion

#region 练习题 - 计算任意数的二进制并通过栈存储打印
/// <summary>
/// 练习题：通过“除2取余法”将十进制数转为二进制，利用栈存储余数，最终逆序打印
/// 核心逻辑：
/// 1. 十进制转二进制：除2取余，余数依次入栈（后得到的余数在栈顶）
/// 2. 弹出栈中元素：栈顶到栈底的顺序即为二进制的高位到低位，逆序打印得到正确二进制
/// </summary>
public class BinaryConverter
{
    /// <summary>
    /// 计算任意十进制数的二进制，通过栈存储并打印
    /// </summary>
    /// <param name="decimalNum">待转换的十进制整数（支持非负数）</param>
    /// <returns>转换后的二进制字符串（输入为0时返回"0"）</returns>
    public static string ConvertToBinaryAndPrint(int decimalNum)
    {
        // 特殊情况：输入为0时，二进制直接为0
        if (decimalNum == 0)
        {
            Console.WriteLine("二进制结果：0");
            return "0";
        }

        // 1. 初始化栈，用于存储除2取余得到的余数
        Stack stack = new Stack();
        // 临时变量存储输入值，避免修改原数据
        int tempNum = decimalNum;

        // 2. 除2取余，余数入栈（核心转换逻辑）
        Console.WriteLine($"===== 十进制 {decimalNum} 转二进制过程 =====");
        while (tempNum > 0)
        {
            // 取余数（0或1）
            int remainder = tempNum % 2;
            // 余数压入栈顶
            stack.Push(remainder);
            Console.WriteLine($"除2取余：{tempNum} ÷ 2 = {tempNum / 2} 余 {remainder}（余数入栈）");
            // 更新被除数为商，继续循环
            tempNum = tempNum / 2;
        }

        // 3. 弹出栈中元素，拼接得到二进制字符串（栈顶→栈底对应高位→低位）
        string binaryResult = "";
        Console.Write("二进制结果（从栈顶弹出元素）：");
        while (stack.Count > 0)
        {
            // 弹出栈顶元素，转为字符串拼接
            binaryResult += stack.Pop();
        }

        // 4. 打印并返回结果
        Console.WriteLine(binaryResult);
        return binaryResult;
    }
}
#endregion

#region 主程序：测试所有功能
class Program
{
    static void Main(string[] args)
    {
        // 测试栈的存储规则
        StackStorageRule.DemoStackRule();

        Console.WriteLine("\n");

        // 测试二进制转换（可替换任意非负整数测试）
        BinaryConverter.ConvertToBinaryAndPrint(25); // 测试25的二进制
        Console.WriteLine("\n");
        BinaryConverter.ConvertToBinaryAndPrint(0);  // 测试0的二进制
        Console.WriteLine("\n");
        BinaryConverter.ConvertToBinaryAndPrint(100); // 测试100的二进制
    }
}
#endregion