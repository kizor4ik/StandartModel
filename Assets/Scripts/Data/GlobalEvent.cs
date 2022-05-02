using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class GlobalEvent 
{
    public string ID;
    public Sprite Icon;
    [Header("Date in format yyyymmdd")]
    public int DateTime;
    public string Description;
}
