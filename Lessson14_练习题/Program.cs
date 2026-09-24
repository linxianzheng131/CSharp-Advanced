using System;
using System.Collections.Generic;

// 1. 基类和派生类，写在Program类外面
class Animal { }
class Cat : Animal { }

// 2. 协变接口（out T），写在Program类外面
interface IReadOnlyCollection<out T>
{
    T GetItem(int index);
}

// 3. 逆变接口（in T），写在Program类外面
interface IProcessor<in T>
{
    void Process(T item);
}

// 4. 实现协变接口的类
class CatCollection : IReadOnlyCollection<Cat>
{
    private List<Cat> cats = new List<Cat> { new Cat(), new Cat() };
    public Cat GetItem(int index)
    {
        return cats[index];
    }
}

// 5. 实现逆变接口的类
class AnimalProcessor : IProcessor<Animal>
{
    public void Process(Animal item)
    {
        Console.WriteLine($"正在处理动物：{item.GetType().Name}");
    }
}

class Program
{
    static void Main(string[] args)
    {
        #region 题目1：协变和逆变的作用描述
        /*
         * 协变（Covariance）和逆变（Contravariance）是 C# 中针对泛型接口/委托的类型安全转换特性：
         *
         * 1. 协变（用 out 关键字）
         *    - 作用：允许将派生类型转换为基类型（向上转换）
         *    - 方向：泛型参数只能作为【输出】值（返回值），不能作为输入参数
         *    - 用途：保留继承链的赋值兼容性，让派生类型的集合可以赋值给基类型的集合
         *
         * 2. 逆变（用 in 关键字）
         *    - 作用：允许将基类型转换为派生类型（向下转换）
         *    - 方向：泛型参数只能作为【输入】参数，不能作为返回值
         *    - 用途：增强接口的复用性，让基类型的处理器可以处理派生类型的对象
         *
         * 3. 核心价值
         *    - 实现了泛型接口/委托的类型安全转换
         *    - 避免重复定义不同类型的接口，提升代码复用性
         *    - 让 LINQ、事件回调等场景的类型转换更安全、更简洁
         */
        Console.WriteLine("===== 题目1：协变和逆变的作用 =====");
        Console.WriteLine("协变（out）：派生类型 → 基类型，用于输出值");
        Console.WriteLine("逆变（in）：基类型 → 派生类型，用于输入参数");
        Console.WriteLine("两者共同实现了泛型接口/委托的类型安全转换，提升代码复用性\n");
        #endregion

        #region 题目2：通过代码说明协变和逆变的作用
        // ------------------------------
        // 协变演示：派生类型接口赋值给基类型接口
        // ------------------------------
        Console.WriteLine("===== 协变演示 =====");
        IReadOnlyCollection<Cat> catCol = new CatCollection();
        // 协变允许：IReadOnlyCollection<Cat> → IReadOnlyCollection<Animal>
        IReadOnlyCollection<Animal> animalCol = catCol;
        Console.WriteLine("协变成功：将 Cat 集合赋值给 Animal 集合");
        Console.WriteLine($"取出的元素类型：{animalCol.GetItem(0).GetType().Name}");

        // ------------------------------
        // 逆变演示：基类型接口赋值给派生类型接口
        // ------------------------------
        Console.WriteLine("\n===== 逆变演示 =====");
        IProcessor<Animal> animalProc = new AnimalProcessor();
        // 逆变允许：IProcessor<Animal> → IProcessor<Cat>
        IProcessor<Cat> catProc = animalProc;
        catProc.Process(new Cat());
        Console.WriteLine("逆变成功：用 Animal 处理器处理 Cat 对象");
        #endregion

        Console.ReadLine();
    }
}