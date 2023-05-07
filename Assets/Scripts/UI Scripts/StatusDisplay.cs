using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusDisplay : MonoBehaviour
{
    private List<ConsumableDisplayData> consumableDisplays = new List<ConsumableDisplayData>();

    private void AddNewConsumable(string _name, int _count, Sprite _image)
    {
        consumableDisplays.Add(new ConsumableDisplayData(_name, _count, _image));
    }

}

public class ConsumableDisplayData : MonoBehaviour
{
    private string consumableName;
    private int consumableCount;
    private Sprite consumableIcon;
    
    internal ConsumableDisplayData(string _name, int _count, Sprite _image)
    {
        consumableName = _name;
        consumableCount = _count;
        consumableIcon = _image;
    }

}
