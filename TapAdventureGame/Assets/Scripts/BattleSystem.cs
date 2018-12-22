using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleSystem : MonoBehaviour {


 

    public void GoToAdventureMode()
    {
        AdventurePoint adp = (AdventurePoint)FindObjectOfType(typeof(AdventurePoint));
        if (adp.AdventurePoints >= 10)
        {

            adp.AdventurePoints  -= 10;
        }
            
    }

    enum AdventurePointsBattleCounter
    {
          LowLevel, // from 10 to 30 adventure points
          MediumLevel, // from 40 - 60 adventure points
          HighLevel, // from 80 to 120 adventure points

    }
}
