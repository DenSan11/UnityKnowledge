using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class AssetTool
{   
    [MenuItem("CreateAsset/Weapon")]
    private static void CreateWeaponAsset()
    {
        WuQiSerializableSet config = ScriptableObject.CreateInstance<WuQiSerializableSet>();
        
        string path = Application.dataPath;
        
        AssetDatabase.CreateAsset(config, "Assets/Resources/Asset/weapon.asset");
        
        //AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    [MenuItem("CreateAsset/Armor")]
    private static void CreateArmorAsset()
    {
        FangJuSerializableSet config = ScriptableObject.CreateInstance<FangJuSerializableSet>();

        string path = Application.dataPath;
        
        AssetDatabase.CreateAsset(config, "Assets/Resources/Asset/armor.asset");       
        
        //AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    [MenuItem("CreateAsset/Consumable")]
    private static void CreateConsumableAsset()
    {
        XiaoHaoPinSerializableSet config = ScriptableObject.CreateInstance<XiaoHaoPinSerializableSet>();

        string path = Application.dataPath;
        
        AssetDatabase.CreateAsset(config, "Assets/Resources/Asset/consumable.asset");      

        //AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}
