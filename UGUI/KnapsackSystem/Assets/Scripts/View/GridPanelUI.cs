using UnityEngine;
using System.Collections;

/// <summary>
/// 背包格子
/// </summary>
public class GridPanelUI : MonoBehaviour
{
    public Transform[] Grids;

    //获取空格子
    public Transform GetEmptyGrid()
    {
        for (int i = 0; i < Grids.Length; i++)
        {
            if (Grids[i].childCount == 0)
                return Grids[i];
        }
        return null;
    }
}

