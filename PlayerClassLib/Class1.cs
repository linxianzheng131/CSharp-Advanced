using System;

namespace PlayerClassLib
{    
    /// <summary>
    /// 练习题核心：Player类，包含姓名、血量、攻击力、防御力、位置等信息
    /// </summary>
    public class Player
    {
        #region 字段与属性（根据知识点要求）
        // 信息字段
        public string Name { get; set; }
        public int HP { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public (int X, int Y) Position { get; set; }
        #endregion

        #region 无参构造函数（必须包含）
        /// <summary>
        /// 无参构造函数：初始化默认值
        /// </summary>
        public Player()
        {
            Name = "默认玩家";
            HP = 100;
            Attack = 10;
            Defense = 5;
            Position = (0, 0);
            Console.WriteLine("【无参构造】执行：Player对象已创建（默认值）");
        }
        #endregion

        #region 可选：有参构造（方便测试，非必须）
        /// <summary>
        /// 有参构造（可选）
        /// </summary>
        /// <param name="name">姓名</param>
        /// <param name="hp">血量</param>
        /// <param name="attack">攻击力</param>
        /// <param name="defense">防御力</param>
        public Player(string name, int hp, int attack, int defense)
        {
            Name = name;
            HP = hp;
            Attack = attack;
            Defense = defense;
            Console.WriteLine($"【有参构造】执行：玩家{name}已创建");
        }
        #endregion

        #region 方法（用于演示反射调用）
        /// <summary>
        /// 移动方法
        /// </summary>
        /// <param name="x">x坐标</param>
        /// <param name="y">y坐标</param>
        public void Move(int x, int y)
        {
            Position = (x, y);
            Console.WriteLine($"【{Name}】移动到位置：({x}, {y})");
        }

        /// <summary>
        /// 攻击方法
        /// </summary>
        public void AttackTarget()
        {
            Console.WriteLine($"【{Name}】发动了攻击！攻击力：{Attack}");
        }
        #endregion
    }
}