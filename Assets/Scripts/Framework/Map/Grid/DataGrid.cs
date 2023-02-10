namespace HiplayGame
{
    public struct CellData
    {
        public int x;
        public int y;
        public int value;
    }

    /// <summary>
    /// 带用户数据的格子
    /// </summary>
    public class DataGrid : Grid
    {
        CellData[,] arrData = null;

        public DataGrid(int width, int height) : base(width, height)
        {
            arrData = new CellData[width, height];
        }

        public void SetData(CellData data)
        {
            arrData[data.x, data.y] = data;
        }

        public CellData GetData(int width, int height)
        {
            return arrData[width, height];
        }

    }
}
