using UnityEngine;

namespace hiplaygame
{
    public class Grid
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        /// <summary>
        /// 最大索引值
        /// </summary>
        public int Size => Width * Height;

        public Grid(int width, int height)
        {
            Width = width;
            Height = height;
        }

        /// <summary>
        /// 根据坐标点，返回索引
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int GetIndex(int x, int y)
        {
            return Width * y +x;
        }

        /// <summary>
        /// 根据索引返回坐标点
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Vector2 GetPosition(int index)
        {
            int y = index / Width;
            int x = index % Width;
            return new Vector2(x, y);
        }

        /// <summary>
        /// 是否是边缘点
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool IsEdge(int index)
        {
            var pos = GetPosition(index);
            var v = pos.y == 0 || pos.y == Height - 1;
            var h = pos.x == 0 || pos.x == Width - 1;
            return v || h;
        }     
    }
}
