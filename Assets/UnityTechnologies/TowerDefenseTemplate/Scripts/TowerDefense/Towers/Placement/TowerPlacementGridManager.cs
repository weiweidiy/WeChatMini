using Core.Utilities;
using TowerDefense.UI.HUD;
using UnityEngine;

namespace TowerDefense.Towers.Placement
{
    public class TowerPlacementGridManager : MonoBehaviour
    {
        public TowerPlacementGrid[] grids;

        public SingleTowerPlacementArea[] singleGrids;

        public (TowerPlacementGrid, PlacementTile, IntVector2) GetPlacementTile(int number)
        {
            foreach (var grid in grids)
            {
                var result = grid.GetTileByNumber(number);
                var tile = result.Item1;
                var vec = result.Item2;
                if (vec.x != -1)
                    return (grid, tile, vec);
            }
            return (null, null, new IntVector2(-1, -1));
        }
    }
}