using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Car:Purchaseable
{
    public float basePrice = 100.00f;
    public Color currentColor = Color.white;
    public List<Accessory> accessories;
    public Material material;
    [System.Serializable]
    public struct stats {
        [Range(0,5)]
        public float coolness;
        [Range(0, 5)]
        public float speed;
        [Range(0, 5)]
        public float handling;
    };
    public stats baseStats;
    public stats totalStats;

    public void AccessoryAdded(AccessoryUIItem accessoryButton)
    {
        accessoryButton.button.interactable = false;
        accessories.Add(accessoryButton.accessory);
        price = basePrice;
        totalStats = baseStats;
        foreach (Accessory acc in accessories)
        {
            price += acc.price;
            totalStats.coolness += acc.coolModifier;
            totalStats.speed += acc.speedModifier;
            totalStats.handling += acc.handlingModifier;
        }
    }
}
