using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SilverPerClick : MonoBehaviour
{

    [SerializeField] Text CurrencyText;
    [SerializeField] Text CurrentTime;
    [SerializeField] Text spc;
    public float iron = 5;
    public float ironPerClick = 1f;
     public float mineWaitTime;
     public float oldMineWaitTime;
    [SerializeField] Color affordableColor,waitingColor;
    Slider tempSlider;
    [SerializeField] Button MinePerClick;
    bool canStart;
 
    private void Start()
    {

            iron = PlayerPrefs.GetFloat("I");
            ironPerClick = PlayerPrefs.GetFloat("IPC");
        mineWaitTime = PlayerPrefs.GetFloat("time");
    }

    // Update is called once per frame
    void Update()
    {
        UpdateInfo();


        if (canStart)
        {
            if (oldMineWaitTime <= mineWaitTime)
            {
                MinePerClick.interactable = false;
                oldMineWaitTime += Time.deltaTime;
                MinePerClick.GetComponentInChildren<Text>().text = "On Cooldown...";
                MinePerClick.GetComponent<Image>().color = waitingColor;
            }
            else
            {
                MinePerClick.interactable = true;
                canStart = false;
                MinePerClick.GetComponentInChildren<Text>().text = "Mine";
                MinePerClick.GetComponent<Image>().color = affordableColor;

            }
        }

    }

    void UpdateInfo()
    {
        CurrencyText.text = iron.ToString("F2") + " Iron";
        spc.text = ironPerClick.ToString("F2") + " Iron/click ";
        var Hour = System.DateTime.Now.Hour;
        var Minute = System.DateTime.Now.Minute;
        CurrentTime.text = "Time : " + Hour + " : " + Minute;
        PlayerPrefs.SetFloat("IPC", ironPerClick);
        PlayerPrefs.SetFloat("I", iron);
    }

 
    public void OnClick()
    {
        iron += ironPerClick;
        Debug.Log("iron " + iron);
        oldMineWaitTime = 0;
        mineWaitTime += 0.25f;
        PlayerPrefs.SetFloat("time", mineWaitTime);
        canStart = true;
    }
}
