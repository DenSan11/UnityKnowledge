using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    public Text ItemName;

    //更新说明文字
    public void UpdateItem(string name)
    {
        ItemName.text = name;
    }
}
