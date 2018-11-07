using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SilverPerSecond : MonoBehaviour
{

    [SerializeField] Text cps;
    [SerializeField] SilverPerClick _CurrencyPerClick;
    [SerializeField] UpgradeManager[] Items;
    [SerializeField] float waitTime;

    void Start()
    {
        StartCoroutine("GenerateCurency");
    }

    private void Update()
    {
        UpdateInfo();
    }

    void UpdateInfo()
    {
        cps.text = GetSilverPerSec() + " Iron/sec";
    }

    public float GetSilverPerSec()
    {
        float tick = 0;
        foreach (var item in Items)
        {
            tick += item.count * item.valuePerSec;
        }
        return tick;
    }

    public void AutoSilverPerSec()
    {
        _CurrencyPerClick.iron += GetSilverPerSec();
    }

    IEnumerator GenerateCurency()
    {
        while (true)
        {
            AutoSilverPerSec();
            yield return new WaitForSeconds(waitTime);
        }
    }

}
