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
        waitTime = 3600; // 3600 seconds = 1hour;
        AdventurePoints = 10;
        AdventurePointsPerSec = 1;
        StartCoroutine(GenerateAdventurePointsPerSec());
    }

    // Update is called once per frame
    void Update()
    {
        IncreaseAdventurePoints();
    }

    void IncreaseAdventurePoints()
    {
        AdventurePointsText.text ="AP" + " " +  AdventurePoints.ToString();
        AdventurePointsPerSecond.text =  AdventurePointsPerSec.ToString() + " " + "AP/Sec";
    }

    IEnumerator GenerateAdventurePointsPerSec()
    {
        yield return new WaitForSeconds(waitTime);
        AdventurePoints += 1;
   
    }

    //Here i will implement incrementation to adventurepointsperclick while the app is closed
}
