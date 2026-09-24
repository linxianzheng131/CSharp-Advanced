namespace Addition_using
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*改用「直接引用DLL」的方式
 
            1. 先编译  PlayerClassLib  项目：右键它 → 生成

            2. 找到它的输出DLL文件： PlayerClassLib\bin\Debug\你的版本\PlayerClassLib.dll 

            3. 右键  Test  项目 → 添加 → 引用 → 浏览 → 选择上面的DLL文件

            4. 确定后重新生成，就能直接引用命名空间了*/

            //不同项目 直接引用DLL的方式 然后就能直接使用了 或

            //不同项目 比如各个lesson之间 不能直接用 using 来引用命名空间了 只能用反射了

        }
    }
}
