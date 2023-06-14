using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CarShopMain : MonoBehaviour
{
    public List<Car> cars;
    public Car currentCar;
    public int currentCarIndex;

    public TextMeshProUGUI carName;
    public TextMeshProUGUI carDesc;
    public TextMeshProUGUI carPrice;

    public MeshFilter carObject;
    public MeshRenderer carRenderer;

    public GameObject allCarsUI;
    public GameObject carUIPrefab;
    List<CarUIItem> carUIItems = new List<CarUIItem>();

    public List<Accessory> allAccessories = new List<Accessory>();
    public GameObject accessoryPrefab;
    public GameObject accessoriesUI;
    List<AccessoryUIItem> accessoryButtons = new List<AccessoryUIItem>();

    public Slider redSlider;
    public Slider greenSlider;
    public Slider blueSlider;
    bool sliderEventEnabled; //less lines than disconnecting and reconnecting events uneccessarily

    public Slider coolSlider;
    public Slider speedSlider;
    public Slider handlingSlider;

    private void Start()
    {
        foreach (Accessory accessory in allAccessories)
        {
            GameObject instance = Instantiate(accessoryPrefab);
            AccessoryUIItem accUIItem = instance.GetComponent<AccessoryUIItem>();
            accUIItem.SetData(accessory);
            accessoryButtons.Add(accUIItem);
            instance.transform.SetParent(accessoriesUI.transform, false);
        }
        
        for(int i = 0; i < cars.Count; i++)
        {
            Car car = cars[i];
            car.material = new Material(Shader.Find("Standard"));
            car.material.color = car.currentColor;
            car.price = car.basePrice;
            car.totalStats = car.baseStats;
            GameObject instance = Instantiate(carUIPrefab);
            CarUIItem carUIItem = instance.GetComponent<CarUIItem>();
            carUIItem.SetData(car);
            carUIItem.carIndex = i;
            carUIItem.button.onClick.AddListener(delegate { OnCarSelected(car); });
            carUIItem.button.onClick.AddListener(HideAllCars);
            carUIItems.Add(carUIItem);
            instance.transform.SetParent(allCarsUI.transform.GetChild(0).transform, false);
        }
        OnCarSelected(cars[0]);
    }
    public void Swap(int direction)
    {
        currentCarIndex = (currentCarIndex + direction + cars.Count) % cars.Count;
        OnCarSelected(cars[currentCarIndex]);
    }

    public void OnCarSelected(Car car)
    {
        currentCar = car;
        carRenderer.material = currentCar.material;
        carObject.mesh = currentCar.model;
        carName.text = currentCar.displayName;
        carDesc.text = currentCar.description;
        UpdateCarStats();
        sliderEventEnabled = false;
        redSlider.value = currentCar.currentColor.r;
        greenSlider.value = currentCar.currentColor.g;
        blueSlider.value = currentCar.currentColor.b;
        sliderEventEnabled = true;
        //add in extra stats like cool factor etc later
        foreach(AccessoryUIItem accessory in accessoryButtons)
        {
            accessory.button.onClick.RemoveAllListeners();
            accessory.button.onClick.AddListener(delegate { currentCar.AccessoryAdded(accessory); UpdateCarStats(); });
            if (car.accessories.Contains(accessory.accessory))
            {
                accessory.button.interactable = false;
            }
            else
            {
                accessory.button.interactable = true;
            }
        }
    }

    public void SliderMoved()
    {
        if (sliderEventEnabled)
        {
            currentCar.currentColor = new Color(redSlider.value, greenSlider.value, blueSlider.value);
            UpdateCarColor();
        }
    }
    private void UpdateCarColor()
    {
        currentCar.material.color = currentCar.currentColor;
    }

    private void UpdateCarStats()
    {
        coolSlider.value = currentCar.totalStats.coolness;
        speedSlider.value = currentCar.totalStats.speed;
        handlingSlider.value = currentCar.totalStats.handling;
        carPrice.text = "Price: £" + currentCar.price.ToString("0.00");
    }

    public void ShowAllCars()
    {
        //update any data in the button that doesnt match
        foreach(CarUIItem carUI in carUIItems) //would be easier with scriptable objects
        {
            carUI.SetData(cars[carUI.carIndex]);
        }
        carObject.gameObject.SetActive(false);
        allCarsUI.SetActive(true);
    }
    public void HideAllCars()
    {
        allCarsUI.SetActive(false);
        carObject.gameObject.SetActive(true);
    }
}
