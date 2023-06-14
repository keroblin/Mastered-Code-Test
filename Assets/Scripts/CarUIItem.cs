using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CarUIItem : UIItem
{
    public TextMeshProUGUI carStat1;
    public Car currentCar;
    public MeshRenderer meshRenderer;
    public int carIndex;

    public override void SetData(Purchaseable purchaseable)
    {
        base.SetData(purchaseable);
        price.text = "£" + purchaseable.price.ToString();
        currentCar = (Car)purchaseable;
        meshRenderer.material.color = currentCar.currentColor;
    }
}
