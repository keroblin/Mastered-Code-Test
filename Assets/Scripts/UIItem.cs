using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class UIItem : MonoBehaviour
{
    public Button button;
    public TextMeshProUGUI displayName;
    public TextMeshProUGUI description;
    public MeshFilter model;
    public TextMeshProUGUI price;

    public virtual void SetData(Purchaseable purchaseable)
    {
        if (displayName)
        {
            displayName.text = purchaseable.displayName;
        }
        if (model)
        {
            model.mesh = purchaseable.model;
        }
        if (price)
        {
            price.text = purchaseable.price.ToString();
        }
        if (description)
        {
            description.text = purchaseable.description;
        }
    }
}
