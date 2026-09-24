using PlayerClassLib;

namespace Test
{
    class Person
    {
        public int money;
        public bool sex;
        public Person() { 
            money = 0;
            sex = false;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {

            char[] arr = new char[4] { 'a', 'b', 'c', 'c' };
            string[] arr2 = new string[] { "abcc" };
            Person p = new Person() { money = 100, sex = true };
            Console.WriteLine(p.money);
        }
    }
}
