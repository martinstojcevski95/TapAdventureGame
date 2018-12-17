using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleSystem : MonoBehaviour {

    [SerializeField] Canvas Enemy;

  // make anothyer class that will have all monsters sorted by level and names
	// Use this for initialization
	void Start () {
        Enemy.enabled = false;		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void ShowEnemy()
    {
        Enemy.enabled = true;
        Enemy enemy = new Enemy();
        
        Enemy.transform.GetChild(0).GetComponentInChildren<Text>().text = "Wild Ogre";  
    }
    public void GoToAdventureMode()
    {
        AdventurePoint adp = (AdventurePoint)FindObjectOfType(typeof(AdventurePoint));
        if (adp.AdventurePoints >= 10)
        {
            ShowEnemy();
            adp.AdventurePoints  -= 10;
        }
            
    }
    //make a function that will chose random enemys until the player reaches
    //different level ex. lvl 1-5 will have 5 different monsters that will be picked randomly
    //same for the other levels
    enum AdventurePointsBattleCounter
    {
          LowLevel, // from 10 to 30 adventure points
          MediumLevel, // from 40 - 60 adventure points
          HighLevel, // from 80 to 120 adventure points

    }
}
