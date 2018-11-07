using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="New Item", menuName ="Items/Item")]
public class Item : ScriptableObject
{
    public new string _ItemName;
    public float _ItemCost;
    public float _ClickPower;
    public float _Damage;
    public Sprite ItemImage;
}
