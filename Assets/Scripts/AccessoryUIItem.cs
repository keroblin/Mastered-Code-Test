using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AccessoryUIItem : UIItem
{
    public Accessory accessory;
    public TextMeshProUGUI buttonLabel;

    public override void SetData(Purchaseable purchaseable) //was going to show description and stuff, probs will set later
    {
        base.SetData(purchaseable);
        accessory = (Accessory)purchaseable;
        buttonLabel.text = "Add " + accessory.displayName + " (£" + accessory.price.ToString() + ")";
    }
}
