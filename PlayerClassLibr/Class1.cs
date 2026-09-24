using System;

namespace PlayerClassLibr
{
    #region 第一题：自定义特性 ProtectAttribute（给字段加标记）
    /// <summary>
    /// 自定义特性：标记字段不允许被反射随意修改
    /// 只允许标记在字段上（AttributeTargets.Field）
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)] // 限定仅作用于字段
    public class ProtectAttribute : Attribute
    {
        // 特性本体，仅作为标记使用
    }
    #endregion

    #region 第二题：Player 类（含无参构造 + 带特性的字段）
    /// <summary>
    /// 玩家类，包含姓名、血量、攻击力、位置信息
    /// </summary>
    public class Player
    {
        #region 字段定义（Name 加自定义特性 [Protect]）
        /// <summary>
        /// 姓名字段：添加自定义特性 [Protect]，禁止反射随意修改
        /// </summary>
        [Protect]
        public string Name;

        /// <summary>
        /// 血量字段
        /// </summary>
        public int HP;

        /// <summary>
        /// 攻击力字段
        /// </summary>
        public int Attack;

        /// <summary>
        /// 位置字段（X,Y 坐标）
        /// </summary>
        public (int X, int Y) Position;
        #endregion

        #region 无参构造函数（满足题目要求）
        /// <summary>
        /// 无参构造函数：初始化默认值
        /// </summary>
        public Player()
        {
            Name = "默认玩家";
            HP = 100;
            Attack = 10;
            Position = (0, 0);
            Console.WriteLine("【无参构造】Player 对象已创建（默认值）");
        }
        #endregion

        #region 重写 ToString（方便打印玩家信息）
        /// <summary>
        /// 重写ToString，快速获取玩家信息
        /// </summary>
        public override string ToString()
        {
            return $"玩家信息：姓名={Name}，血量={HP}，攻击力={Attack}";
        }
        #endregion

        #region 可选：移动方法（满足反射调用方法要求）
        /// <summary>
        /// 移动方法：修改位置
        /// </summary>
        public void Move(int x, int y)
        {
            Position = (x, y);
            Console.WriteLine($"【{Name}】移动到位置：({x}, {y})");
        }
        #endregion

        #region 可选：攻击方法（满足反射调用方法要求）
        /// <summary>
        /// 攻击方法：展示攻击力
        /// </summary>
        public void AttackTarget()
        {
            Console.WriteLine($"【{Name}】发动攻击！攻击力：{Attack}");
        }
        #endregion
    }
    #endregion
}