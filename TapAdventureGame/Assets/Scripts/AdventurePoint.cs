using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdventurePoint : MonoBehaviour
{

    public int AdventurePoints, AdventurePointsPerSec;
    public Text AdventurePointsText, AdventurePointsPerSecond;
    public float waitTime; //this will be decreased with items from upgrade shop. At the beggining player will get 1 adventure point per 1 hour;
    

    // Use this for initialization
    void Start()
    {

        waitTime =  600; //600; // 3600 seconds = 1hour;
        AdventurePoints = PlayerPrefs.GetInt("ADPOINTS");
        //AdventurePoints = 50;
        InvokeRepeating("GenerateAdventurePointsPerSec", 0, waitTime);
        AdventurePointsPerSec = 1;
    }

    void Update()
    {
        AdventurePointsUI();
    }


    void AdventurePointsUI()
    {
        AdventurePointsText.text = "AP" + " " + AdventurePoints.ToString();
        AdventurePointsPerSecond.text = AdventurePointsPerSec.ToString() + " " + "AP/ " + waitTime + " Sec ";
    }



    //Here i will implement incrementation to adventurepointsperclick while the app is closed
}
