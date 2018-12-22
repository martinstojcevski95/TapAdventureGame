using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour {

    public SilverPerClick _CurrencyPerClick;
    [SerializeField] Text ItemInfo;
    [SerializeField] CurrencyValue currencyValue;
    public float cost;
    public int count = 0;
    public string ItemName;
    private float baseCost;
    public float valuePerSec;
    [SerializeField] Color standardColor, affordableColor;
    Slider tempSlider;

    private void Start()
    {
        baseCost = cost;
        var slider = this.gameObject.transform.GetChild(0).gameObject.GetComponent<Slider>();
        tempSlider = slider;
    }

    // Update is called once per frame
    void Update () {
        UpdateInfo();
	}

    void UpdateInfo()
    {
        gameObject.name = ItemName;
        if(currencyValue == CurrencyValue.Iron)
            ItemInfo.text = ItemName + "\nCost: " + cost + " " + currencyValue;

        if (currencyValue == CurrencyValue.Silver)
            ItemInfo.text = ItemName + "\nCost: " + cost + " " + currencyValue;
        if(currencyValue == CurrencyValue.Gold)
            ItemInfo.text = ItemName + "\nCost: " + cost + currencyValue;

        if (tempSlider != null)
            tempSlider.value = _CurrencyPerClick.iron /cost * 100;

    }

    public void PurchasedUpgrade()
    {
        if (_CurrencyPerClick.iron >= cost)
        {
            _CurrencyPerClick.iron -= cost;
            count += 1;
            cost = Mathf.Round(baseCost * Mathf.Pow(1.15f, count));
        }
    }

    enum CurrencyValue
    {
        Iron,
        Silver,
        Gold,
        Platinum
    }
}
