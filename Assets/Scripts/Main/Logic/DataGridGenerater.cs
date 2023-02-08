using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hiplaygame
{
    /// <summary>
    /// grid数据生成器
    /// </summary>
    public class DataGridGenerater
    {
        public DataGrid Generater(int width, int height)
        {
            var grid = new DataGrid(width, height);
            for(int index = 0; index < grid.Size; index ++)
            {
                var cellData = new CellData();
                var pos = grid.GetPosition(index);
                cellData.x = (int)pos.x;
                cellData.y = (int)pos.y;
                cellData.value = grid.IsEdge(index) ? 1 : 0;
                grid.SetData(cellData);
            }
            return grid;
        }


    }
}
