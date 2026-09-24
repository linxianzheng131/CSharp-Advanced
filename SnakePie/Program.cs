namespace SnakePie{
    internal class Program{
        static int isRunning = 1;
        static int Xpos = 10;
        static int Ypos = 10;
        static int direction = 1;
        static readonly object consoleLock = new object();
        static void Main(string[] args){
            Console.CursorVisible = false;
            Console.SetWindowSize(40, 20);
            Console.SetBufferSize(40, 20);            
            Thread inputThread = new Thread(ListenForKeyInput){IsBackground = true};           
            inputThread.Start();
            while(Interlocked.CompareExchange(ref isRunning, 1, 1) == 1){
                lock(consoleLock){
                    Console.SetCursorPosition(Xpos, Ypos);
                    Console.Write("  ");
                }
                switch (Volatile.Read(ref direction)){
                    case 0:
                        Ypos = Math.Max(0, Ypos - 1);
                        break;
                    case 1:
                        Xpos = Math.Min(39, Xpos + 1);
                        break;
                    case 2:
                        Ypos = Math.Min(19, Ypos + 1);
                        break;
                    case 3:
                        Xpos = Math.Max(0, Xpos - 1);
                        break;
                }
                lock (Console.Out){
                    Console.SetCursorPosition(Xpos, Ypos);
                    Console.Write("■");
                }                
                Thread.Sleep(200);
            }
            lock (consoleLock){
                Console.SetCursorPosition(15, 10);
                Console.Write("Game Over!");
            }
        }
        private static void ListenForKeyInput(){
            while(Interlocked.CompareExchange(ref isRunning, 1, 1) == 1){
                if(Console.KeyAvailable){               
                    var key = Console.ReadKey(true);
                    int nowDir = Volatile.Read(ref direction);
                    switch (key.Key)
                    {
                        case ConsoleKey.W when nowDir != 2:
                            Volatile.Write(ref direction, 0);
                            break;                        
                        case ConsoleKey.D when nowDir != 3:
                            Volatile.Write(ref direction, 1);
                            break;
                        case ConsoleKey.S when nowDir != 0:
                            Interlocked.Exchange(ref direction, 2);
                            break;
                        case ConsoleKey.A when nowDir != 1:
                            Interlocked.Exchange(ref direction, 3);
                             break;
                    }
                }
                Thread.Sleep(150);
            }            
        }
    }
    
}
