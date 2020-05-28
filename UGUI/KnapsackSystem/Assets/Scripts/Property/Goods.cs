using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Goods
{
    [Header("编号")]
    public int id = 0;
    [Header("名字")]
    public string name;
    [Header("描述")]
    public string description;
    [Header("售价")]
    public int buyPrice;
    [Header("二手价")]
    public int sellPrice;
    [Header("图标")]
    public string icon;
    [Header("物品类型")]
    public ItemType itemType = ItemType.W;

    

    private static Dictionary<int, Goods> goodsDictionary = new Dictionary<int, Goods>();

    public static Goods Get(int id)
    {
        Goods goods;
        if (goodsDictionary.TryGetValue(id,out goods))
        {
            return goods;
        }
        return null;
    }

    public static Dictionary<int,Goods> GetDictionary()
    {
        return goodsDictionary;
    }
}

public enum ItemType
{
    W = 1,
    A = 2,
    C = 3
}