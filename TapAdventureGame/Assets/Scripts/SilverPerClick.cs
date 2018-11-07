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

    private void Start()
    {
        iron = PlayerPrefs.GetFloat("S");
        ironPerClick = PlayerPrefs.GetFloat("IPC");
    }

    // Update is called once per frame
    void Update()
    {
        UpdateInfo();
      
    }

    void UpdateInfo()
    {
        CurrencyText.text = iron.ToString("F2") + " Iron";
        spc.text = ironPerClick.ToString("F2") + " Iron/click ";
        var Hour = System.DateTime.Now.Hour;
        var Minute = System.DateTime.Now.Minute;
        CurrentTime.text = "Time : " + Hour + " : " + Minute;
        PlayerPrefs.SetFloat("IPC", ironPerClick);
        PlayerPrefs.SetFloat("S", iron);
    }

    public void OnClick()
    {
        iron += ironPerClick;
    }
}
