using System;

namespace DelegateWaterHeaterDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("===== 热水器温度监控演示 =====");
            WaterHeaterDemo.Run();
        }
    }

    #region 题目：热水器温度监控（超过95度触发报警和显示更新）
    /// <summary>
    /// 核心知识点：
    /// 1. 委托/事件实现“发布-订阅”模式：热水器是发布者，报警器和显示器是订阅者
    /// 2. 当水温达到阈值时，热水器触发委托，所有订阅的设备自动执行对应逻辑
    /// 3. 解耦设计：热水器本身不需要知道报警器和显示器的具体实现，只需要定义好委托即可
    /// 4. 空条件运算符 ?.Invoke() 的安全调用
    /// </summary>
    public static class WaterHeaterDemo
    {
        // 1. 定义委托：代表水温过高时要执行的操作（可以带参数传递当前温度）
        public delegate void WaterTemperatureTooHighHandler(int currentTemperature);

        public static void Run()
        {
            // 2. 实例化各个组件
            WaterHeater heater = new WaterHeater();
            Alarm alarm = new Alarm();
            Display display = new Display();

            // 3. 订阅：把报警器和显示器的方法注册到委托上
            WaterTemperatureTooHighHandler handler = null;
            handler += alarm.AlarmAction;   // 报警器的报警方法
            handler += display.ShowMessage;  // 显示器的显示方法

            // 4. 热水器通电加热，绑定好温度过高时的处理委托
            heater.StartHeating(handler);
        }

        // 热水器类（发布者）
        public class WaterHeater
        {
            // 模拟加热过程，温度从0度逐步上升
            public void StartHeating(WaterTemperatureTooHighHandler highTempHandler)
            {
                Console.WriteLine("热水器通电，开始加热...");

                for (int temp = 80; temp <= 100; temp++)
                {
                    Console.WriteLine($"当前水温：{temp}℃");

                    // 当水温超过95度时，触发委托
                    if (temp >= 95)
                    {
                        // 安全调用：如果委托不为null，就执行所有订阅的方法
                        highTempHandler?.Invoke(temp);
                    }

                    // 模拟加热延迟，方便观察过程
                    System.Threading.Thread.Sleep(200);
                }
            }
        }

        // 报警器类（订阅者）
        public class Alarm
        {
            // 报警方法：接收当前温度，发出语音提示
            public void AlarmAction(int currentTemp)
            {
                Console.WriteLine($"【报警器】嘀嘀嘀！水温已达{currentTemp}℃，超过安全温度！");
            }
        }

        // 显示器类（订阅者）
        public class Display
        {
            // 显示方法：接收当前温度，更新提示信息
            public void ShowMessage(int currentTemp)
            {
                Console.WriteLine($"【显示器】水温：{currentTemp}℃，水已经烧开了，请关闭加热！");
            }
        }
    }
    #endregion
}