using System;
using System.Collections;
using System.Collections.Generic;

namespace CustomForeachDemo
{
    // 自定义类：演示如何让自定义类支持 foreach 遍历
    public class CustomClass : IEnumerable<int>
    {
        // 内部存储数据（示例：用List存储整数）
        private readonly List<int> _data = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        #region 方式一：基于集合的迭代（推荐，简单高效）
        /// <summary>
        /// 实现 IEnumerable<int> 接口的 GetEnumerator 方法
        /// 直接返回内部集合的迭代器，无需手动实现
        /// </summary>
        /// <returns>内部List的迭代器</returns>
        public IEnumerator<int> GetEnumerator()
        {
            // 直接委托给内部List的遍历，这是最推荐的方式
            return _data.GetEnumerator();
        }
        #endregion

        #region 方式二：手动实现迭代器（自定义遍历逻辑）
        /// <summary>
        /// 手动实现 IEnumerator<int>，自定义遍历规则（示例：仅遍历偶数）
        /// </summary>
        private class CustomEnumerator : IEnumerator<int>
        {
            private int _current = -1;
            private readonly CustomClass _owner;

            public CustomEnumerator(CustomClass owner)
            {
                _owner = owner;
            }

            // 当前元素
            public int Current => _current;

            // 非泛型接口的Current（显式实现）
            object IEnumerator.Current => Current;

            /// <summary>
            /// 移动Next：仅移动到下一个偶数
            /// </summary>
            /// <returns>是否还有下一个元素</returns>
            public bool MoveNext()
            {
                // 从当前位置往后找，直到找到偶数或遍历结束
                do
                {
                    _current++;
                } while (_current < _owner._data.Count && _owner._data[_current] % 2 != 0);

                // 如果找到偶数，返回true；否则遍历结束
                return _current < _owner._data.Count;
            }

            /// <summary>
            /// 重置迭代器（回到初始状态）
            /// </summary>
            public void Reset()
            {
                _current = -1;
            }

            /// <summary>
            /// 释放资源（无需要手动释放，此处空实现）
            /// </summary>
            public void Dispose() { }
        }
        #endregion

        #region 核心选择：决定使用哪种遍历方式
        /// <summary>
        /// 自定义类的遍历入口：
        /// 这里可以选择返回方式一（基于集合）或方式二（手动迭代）
        /// 示例：默认使用方式一，若需自定义遍历，可修改此处
        /// </summary>
        /// <returns>迭代器</returns>
        IEnumerator<int> IEnumerable<int>.GetEnumerator()
        {
            // 【切换遍历方式】
            // 方式一：return _data.GetEnumerator(); （推荐，默认）
            // 方式二：return new CustomEnumerator(this); （自定义遍历规则）
            return new CustomEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<int>)this).GetEnumerator();
        }
        #endregion
    }

    #region 测试代码
    /// <summary>
    /// 测试类：验证两种遍历方式是否正常工作
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("===== 自定义类 foreach 遍历演示 =====");
            CustomClass customObj = new CustomClass();

            // 遍历自定义类（自动使用方式一或方式二，取决于GetEnumerator的实现）
            foreach (int item in customObj)
            {
                Console.WriteLine($"遍历元素: {item}");
            }

            Console.WriteLine("\n按任意键退出...");
            Console.ReadKey();
        }
    }
    #endregion
}