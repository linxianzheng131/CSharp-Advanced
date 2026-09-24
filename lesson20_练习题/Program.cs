using System;
using System.Reflection;
using System.IO;

namespace ReflectPlayerDemo
{
    //提前编译类库 生成PlayerClassLib.dll
    //然后该文件复制路径
    internal class Program
    {
        #region 主函数（反射入口）
        private static void Main(string[] args)
        {
            Console.WriteLine("===== 开始反射调用类库 =====");

            // 1. 定义DLL路径（请替换为你实际的DLL路径）
            // 方式A：绝对路径（推荐测试用）
            string dllPath = /*或@ 就不用\\*/"D:\\Users\\Carl\\Desktop\\C#\\Csharp进阶教程\\Csharp进阶教程\\PlayerClassLib\\bin\\Debug\\net8.0\\PlayerClassLib.dll";
           

            // 方式B：相对路径（推荐工程用，将DLL复制到控制台输出目录）
            //string dllName = "PlayerClassLib.dll";
           //string dllPath = Path.Combine(AppContext.BaseDirectory, dllName);

            // 2. 加载程序集
            #region 加载程序集（异常处理）
            Assembly assembly = null;
            try
            {
                assembly = Assembly.LoadFrom(dllPath);
                Console.WriteLine($"✅ 成功加载程序集：{assembly.FullName}");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("❌ 错误：未找到DLL文件，请检查路径！");
                return;
            }
            catch (BadImageFormatException)
            {
                Console.WriteLine("❌ 错误：文件不是有效的程序集！");
                return;
            }
            #endregion

            // 3. 反射核心：获取Type对象
            // 格式：命名空间.类名
            Type playerType = assembly.GetType("PlayerClassLib.Player");
            if (playerType == null)
            {
                Console.WriteLine("❌ 错误：未找到Player类");
                return;
            }
            Console.WriteLine($"✅ 成功获取Type：{playerType.FullName}");

            // 4. 实例化对象（核心练习题要求）
            #region 实例化Player对象（两种方式）
            Console.WriteLine("\n----- 方式1：调用无参构造函数 -----");
            // 方式1：使用 Activator.CreateInstance（最常用）
            object playerObj = Activator.CreateInstance(playerType);

            // 方式2：使用 GetConstructor + Invoke（更精准）
            // ConstructorInfo ctor = playerType.GetConstructor(Type.EmptyTypes);
            // object playerObj = ctor.Invoke(null);
            #endregion

            // 5. 反射调用方法（演示）
            #region 反射调用方法
            if (playerObj != null)
            {
                Console.WriteLine("\n----- 反射调用Player方法 -----");

                // 调用 Move 方法
                MethodInfo moveMethod = playerType.GetMethod("Move");
                moveMethod.Invoke(playerObj, new object[] { 10, 20 }); // 传入参数 (10,20)

                // 调用 Attack 方法
                MethodInfo attackMethod = playerType.GetMethod("AttackTarget");
                attackMethod.Invoke(playerObj, null); // 无参数

                // 读取/修改属性
                #region 反射操作属性
                Console.WriteLine("\n----- 反射操作Player属性 -----");
                PropertyInfo nameProp = playerType.GetProperty("Name");
                // 修改姓名
                nameProp.SetValue(playerObj, "反射玩家小明");
                // 读取姓名
                string playerName = (string)nameProp.GetValue(playerObj);
                Console.WriteLine($"修改后的玩家姓名：{playerName}");

                // 读取血量
                PropertyInfo hpProp = playerType.GetProperty("HP");
                int currentHp = (int)hpProp.GetValue(playerObj);
                Console.WriteLine($"当前玩家血量：{currentHp}");
                #endregion
            }
            #endregion

            Console.WriteLine("\n===== 反射执行完毕 =====");
            Console.WriteLine("按任意键退出...");
            Console.ReadKey();
        }
        #endregion
    }
}