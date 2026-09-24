namespace Addition2_属性
{
    class Bank
    {
        public int money { get; set; }
        //等价与
        //private int money;
        //public int Money    
        //注意：属性的命名规范是首字母大写（Pascal命名法），而字段的命名规范是首字母小写（camel命名法）
        //为了区分属性和字段，通常会将属性命名为首字母大写，而字段命名为首字母小写
        //{
        //    get { return money; }
        //    set { money = value; }
        // }
        //下面这个是死循环，因为属性Money的get和set访问器中都在访问Money属性本身，导致无限递归调用
        //相当于上面着等价中的Money = money;  money = Money; 这两行代码互相调用，导致无限递归调用，最终导致栈溢出异常
        /*
        public int amout
        {
            get
            {
                 return amout;
            }
            set
            {
                if (value < 0)
                {
                    Console.WriteLine("金额不能为负数");
                }
                else
                {
                    amout = value;
                }
            }
        }
        */
        //应该改为
        private int amount { get;set; }
        public int Amount
        {
            get
            {
                return amount;
            }
            set
            {
                if (value < 0)
                {
                    Console.WriteLine("金额不能为负数");
                }
                else
                {
                    amount = value;
                }
            }
        }
        //属性就是一个特殊函数
        //外部通过访问属性来调用这个函数，属性内部通过get和set访问器来实现对字段的访问和修改

    }
    internal class Program
    {
        static void Main(string[] args)
        {
            
        }
    }
}
