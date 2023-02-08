using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreItem : ScriptableObject
{
    public int Id;
    public string Name;
    public int Price;
    public Sprite Icon;
    public GameObject Prefab;
}
