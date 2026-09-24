#define UNITY_5 // 模拟Unity5版本定义（可根据需求切换为UNITY_2017/UNITY_2020或注释）
//#define UNITY_2017 // 切换为Unity2017版本
//#define UNITY_2020 // 切换为Unity2020版本
using System;

namespace PreprocessorDirectivesDemo
{
    internal class Program
    {
        #region 第一题：列举至少4种C#预处理器指令
        /// <summary>
        /// 第一题：输出C#常用预处理器指令说明
        /// 指令说明：
        /// 1. #define：定义编译符号，用于条件编译
        /// 2. #undef：取消定义编译符号
        /// 3. #if/#elif/#else/#endif：条件编译核心指令，控制代码是否编译
        /// 4. #region/#endregion：代码折叠，仅IDE识别，不影响编译
        /// 5. #warning：生成编译警告
        /// 6. #error：生成编译错误，终止编译
        /// 7. #line：修改编译器报告的行号
        /// 8. #pragma：向编译器发送特殊指令（如禁用警告）
        /// </summary>
        private static void ListPreprocessorDirectives()
        {
            Console.WriteLine("===== 第一题：C#常用预处理器指令 =====");
            Console.WriteLine("1. #define：定义编译符号，配合#if使用");
            Console.WriteLine("2. #undef：取消已定义的编译符号");
            Console.WriteLine("3. #if/#elif/#else/#endif：条件编译核心逻辑");
            Console.WriteLine("4. #region/#endregion：代码折叠（IDE折叠用）");
            Console.WriteLine("5. #warning：生成编译时警告提示");
            Console.WriteLine("6. #error：生成编译时错误，终止编译");
            Console.WriteLine("======================================\n");
        }
        #endregion

        #region 第二题：Unity版本条件编译计算函数
        /// <summary>
        /// 第二题：根据Unity版本执行不同运算
        /// 规则：
        /// Unity5.x：执行加法 a + b
        /// Unity2017.x：执行乘法 a * b
        /// Unity2020.x：执行减法 a - b
        /// 其他版本：返回 0
        /// 依赖Unity全局符号：UNITY_5/UNITY_2017/UNITY_2020（Unity自动定义）
        /// </summary>
        /// <param name="a">第一个数值</param>
        /// <param name="b">第二个数值</param>
        /// <returns>对应版本运算结果，非目标版本返回0</returns>
        private static int CalculateByUnityVersion(int a, int b)
        {
            // 条件编译核心：根据定义的Unity版本符号执行对应逻辑
#if UNITY_5
            // Unity5版本：加法运算
            Console.WriteLine("当前运行环境：Unity 5.x 版本");
            return a + b;
#elif UNITY_2017
            // Unity2017版本：乘法运算
            Console.WriteLine("当前运行环境：Unity 2017.x 版本");
            return a * b;
#elif UNITY_2020
            // Unity2020版本：减法运算
            Console.WriteLine("当前运行环境：Unity 2020.x 版本");
            return a - b;
#else
            // 非目标版本：返回0
            Console.WriteLine("当前运行环境：非指定Unity版本（返回0）");
            return 0;
#endif
        }
        #endregion

        #region 程序入口
        private static void Main(string[] args)
        {
            // 执行第一题：列举预处理器指令
            ListPreprocessorDirectives();

            // 执行第二题：版本条件计算
            int num1 = 10;
            int num2 = 5;
            int result = CalculateByUnityVersion(num1, num2);

            // 输出计算结果
            Console.WriteLine($"计算结果（{num1} 和 {num2}）：{result}");

            // 暂停查看结果
            Console.WriteLine("\n按任意键退出...");
            Console.ReadKey(true);
        }
        #endregion
    }
}