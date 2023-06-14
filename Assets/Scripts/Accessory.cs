using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Accessory:Purchaseable
{
    //stats
    [Range(-1, 1)]
    public float coolModifier;
    [Range(-1, 1)]
    public float speedModifier;
    [Range(-1, 1)]
    public float handlingModifier;
}
