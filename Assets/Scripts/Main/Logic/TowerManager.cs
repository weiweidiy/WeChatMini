using System.Collections.Generic;
using TowerDefense.Towers;

public class TowerManager
{
    Dictionary<int, Tower> towers = new Dictionary<int, Tower>();

    public void AddTower(int number, Tower tower)
    {
        if (!towers.ContainsKey(number))
            towers.Add(number, tower);
    }

    public Tower GetTower(int number)
    {
        if (towers.ContainsKey(number))
            return towers[number];

        return null;
    }

    public Tower GetTower(string userId)
    {
        foreach (var key in towers.Keys)
        {
            if (towers[key].userId.Equals(userId))
                return towers[key];
        }
        return null;
    }

    public void RemoveTower(int number)
    {

    }

    public void Clear()
    {
        towers.Clear();
    }
}
