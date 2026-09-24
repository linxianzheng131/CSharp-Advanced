using System;
using System.Reflection;
using System.IO;


namespace ReflectPlayerDemo
{
    [AttributeUsage(AttributeTargets.Field)] // 限定仅作用于字段
    public class ProtectAttribute : Attribute
    {
        // 特性本体，仅作为标记使用
    }
    internal class Program
    {
        #region 主函数（反射入口：加载 DLL + 实例化 + 特性拦截）
        /// <summary>
        /// 主函数：实现反射调用类库，根据自定义特性拦截非法修改
        /// </summary>
        private static void Main(string[] args)
        {
            Console.WriteLine("===== 反射 + 自定义特性 完整演示 =====");

            #region 步骤1：加载类库 DLL（无路径问题，自动查找输出目录）
            string dllName = "PlayerClassLib.dll";
            string dllPath = Path.Combine(AppContext.BaseDirectory, dllName);

            Assembly assembly = null;
            try
            {
                assembly = Assembly.LoadFrom(dllPath);
                Console.WriteLine($"✅ 成功加载程序集：{assembly.FullName}");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("❌ 错误：未找到 DLL 文件，请先生成类库项目！");
                return;
            }
            catch (BadImageFormatException)
            {
                Console.WriteLine("❌ 错误：文件不是有效的程序集！");
                return;
            }
            #endregion

            #region 步骤2：获取 Player 类型
            Type playerType = assembly.GetType("PlayerClassLib.Player");
            if (playerType == null)
            {
                Console.WriteLine("❌ 错误：未找到 Player 类");
                return;
            }
            Console.WriteLine($"✅ 成功获取 Type：{playerType.FullName}\n");
            #endregion

            #region 步骤3：反射实例化 Player（调用无参构造）
            object playerObj = Activator.CreateInstance(playerType);
            Console.WriteLine($"初始玩家状态：{playerObj}\n");
            #endregion

            #region 步骤4：反射赋值（测试带特性的 Name 字段）
            Console.WriteLine("----- 尝试修改【带特性】的 Name 字段 -----");
            SetFieldWithProtectCheck(playerType, playerObj, "Name", "反射修改的小明");
            #endregion

            #region 步骤5：反射赋值（测试普通字段 HP）
            Console.WriteLine("\n----- 尝试修改【普通】的 HP 字段 -----");
            SetFieldWithProtectCheck(playerType, playerObj, "HP", 200);
            #endregion

            #region 步骤6：反射调用方法（Move + AttackTarget）
            Console.WriteLine("\n----- 反射调用 Player 方法 -----");
            // 调用 Move 方法
            MethodInfo moveMethod = playerType.GetMethod("Move");
            moveMethod.Invoke(playerObj, new object[] { 15, 25 });

            // 调用 Attack 方法
            MethodInfo attackMethod = playerType.GetMethod("AttackTarget");
            attackMethod.Invoke(playerObj, null);
            #endregion

            #region 步骤7：输出最终结果
            Console.WriteLine("\n===== 最终玩家状态 =====");
            Console.WriteLine(playerObj);
            #endregion

            Console.WriteLine("\n按任意键退出...");
            Console.ReadKey();
        }
        #endregion

        #region 核心方法：根据自定义特性拦截反射赋值（满足题目要求）
        /// <summary>
        /// 安全设置字段值：
        /// 1. 检查字段是否有 [Protect] 特性
        /// 2. 如果有特性：打印“非法操作：随意修改XXX成员”并拦截
        /// 3. 如果没有特性：正常赋值
        /// </summary>
        private static void SetFieldWithProtectCheck(Type type, object obj, string fieldName, object value)
        {
            // 1. 获取字段
            FieldInfo field = type.GetField(fieldName);
            if (field == null)
            {
                Console.WriteLine($"❌ 错误：字段 {fieldName} 不存在");
                return;
            }

            // 2. 检查是否包含自定义特性 [Protect]
            bool isProtected = field.IsDefined(typeof(ProtectAttribute), false);

            if (isProtected)
            {
                // 3. 题目要求：打印非法操作提示
                Console.WriteLine($"🚫 非法操作：随意修改 {fieldName} 成员");
            }
            else
            {
                // 4. 正常赋值
                Console.WriteLine($"✅ 合法操作：修改 {fieldName} = {value}");
                field.SetValue(obj, value);
            }

        }
        #endregion
    }
}