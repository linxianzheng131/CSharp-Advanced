using System;
using System.Threading;

namespace SimpleSquareMove
{
    internal class Program
    {
        #region 共享状态与锁定义（线程安全核心）
        /// <summary>
        /// 方块X坐标
        /// </summary>
        private static int _posX = 10;

        /// <summary>
        /// 方块Y坐标
        /// </summary>
        private static int _posY = 5;

        /// <summary>
        /// 移动方向（0=上,1=右,2=下,3=左）
        /// </summary>
        private static int _direction = 1;

        /// <summary>
        /// 游戏运行标志（原子操作控制）
        /// </summary>
        private static int _isRunning = 1;

        /// <summary>
        /// 控制台输出锁对象（解决多线程输出冲突）
        /// </summary>
        private static readonly object _consoleLock = new object();
        #endregion

        #region 主函数（游戏入口）
        private static void Main(string[] args)
        {
            #region 初始化控制台配置
            // 隐藏光标，避免绘制干扰
            Console.CursorVisible = false;
            // 设置控制台窗口大小，适配移动范围
            Console.SetWindowSize(40, 20);
            Console.SetBufferSize(40, 20);
            #endregion

            #region 启动输入监听线程
            // 创建独立线程用于键盘输入检测
            Thread inputThread = new Thread(ListenForKeyInput)
            {
                // 设置为后台线程，主程序退出时自动销毁
                IsBackground = true
            };
            inputThread.Start();
            #endregion

            #region 主游戏循环（自动移动逻辑） 
            while (Interlocked.CompareExchange(ref _isRunning, 1, 1) == 1)
            {
                // 清除旧位置（加锁保证原子操作）
                lock (_consoleLock)
                {
                    Console.SetCursorPosition(_posX, _posY);
                    Console.Write(" ");
                }

                // 根据当前方向更新坐标
                UpdatePositionByDirection();

                // 绘制新位置（加锁保证输出安全）
                lock (_consoleLock)
                {
                    Console.SetCursorPosition(_posX, _posY);
                    Console.Write("■");
                }

                // 控制移动速度（单位：毫秒）
                Thread.Sleep(150);
            }
            #endregion

            #region 游戏结束收尾
            lock (_consoleLock)
            {
                Console.Clear();
                Console.SetCursorPosition(10, 5);
                Console.WriteLine("游戏结束！按任意键退出");
                Console.ReadKey(true);
            }
            #endregion
        }
        #endregion

        #region 输入监听逻辑（独立线程）
        /// <summary>
        /// 独立线程：监听键盘输入，修改方块移动方向
        /// </summary>
        private static void ListenForKeyInput()
        {
            while (Interlocked.CompareExchange(ref _isRunning, 1, 1) == 1)
            {
                // 非阻塞检测是否有按键输入
                if (Console.KeyAvailable)
                {
                    var keyInfo = Console.ReadKey(true);

                    // 原子安全读取当前方向，不修改任何东西
                    int nowDir = Volatile.Read(ref _direction);

                    switch (keyInfo.Key)//获取按键并根据当前方向决定是否更新（避免180度转向）
                    {
                        case ConsoleKey.UpArrow when nowDir != 2:
                            Interlocked.Exchange(ref _direction, 0);
                            break;

                        case ConsoleKey.DownArrow when nowDir != 0:
                            Interlocked.Exchange(ref _direction, 2);
                            break;

                        case ConsoleKey.RightArrow when nowDir != 3:
                            Interlocked.Exchange(ref _direction, 1);
                            break;
                            break;

                        case ConsoleKey.LeftArrow when nowDir != 1:
                            Interlocked.Exchange(ref _direction, 3);
                            break;
                    }
                }
                // 降低CPU占用，避免空轮询
                Thread.Sleep(150);
            }
        }
        #endregion

        #region 位置更新逻辑（根据方向移动）
        /// <summary>
        /// 根据当前方向更新方块坐标
        /// </summary>
        private static void UpdatePositionByDirection()
        {
            //下面这个写法比锁更简洁，性能更好，适合这种简单的原子读取场景
            switch (Interlocked.CompareExchange(ref _direction, 0, 0)//安全读取当前方向
            )
            {
                case 0: // 向上
                    _posY = Math.Max(0, _posY - 1);
                    break;
                case 1: // 向右
                    _posX = Math.Min(39, _posX + 1);
                    break;
                case 2: // 向下
                    _posY = Math.Min(19, _posY + 1);
                    break;
                case 3: // 向左
                    _posX = Math.Max(0, _posX - 1);
                    break;
            }
        }
        #endregion
    }
}