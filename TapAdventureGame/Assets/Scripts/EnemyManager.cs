using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour {


    [SerializeField] Canvas Enemy;
    [SerializeField] Text Name, Health, Damage, Level;
    [SerializeField] Text notEnoughADP;
    [SerializeField] Text BattleLoggerText;
    [SerializeField] Button AdventureButton;
    [Space(5)]
    [Header(" 1- 5 lvl enemys")]
    [SerializeField] List<Enemy> FirstToFifthLevelEnemys = new List<Enemy>();
    int enemyCounter = 0;
    public static EnemyManager Instance;
    FightMode fightMode;
    EnemyPowerLevel enemyPowerLevel;
    Character characterInfo;
    [SerializeField] UserInfo userInfo;
    [SerializeField] AudioSource toBattleSound;
    int adpPerBattle;
    float waitingCountDownForBattle;
    bool startCounter;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        characterInfo = new Character();
        fightMode = FightMode.None;
    }

    //increase the character leve from the enemy drop level; - done
    // check if the enemy damage or health are lower than the character, than win the battle - half done
    // check if player is bigger level than 5  change the enemypowerlevel to middle, and than make new enemy list - done
    //from 5- 10 level etc etc.

    //TODO -->  if the character choose to run instead of fightin, he will also have a change for that. If enemy stats are 
    //higher he will lose adp
    //TODO --> FightWithEnemy method will change monsters from lower to higher level

    private void Update()
    {
        if (characterInfo.Level < 5)
        {
            enemyPowerLevel = EnemyPowerLevel.Low;
            adpPerBattle = 10;
        }
        if (startCounter)
            CountDownForBattle();
    }

    void CountDownForBattle()
    {
        if(waitingCountDownForBattle >0)
        {
            waitingCountDownForBattle -= Time.deltaTime;
            BattleLoggerText.text = waitingCountDownForBattle.ToString("F0") + " seconds";
            if (waitingCountDownForBattle <= 0)
            {
                startCounter = false;
                waitingCountDownForBattle = 0;
                BattleLoggerText.text = "";
            }
        }

          
    }

    IEnumerator NotEnoughADP()
    {
        notEnoughADP.text = "Not enough adventure points, you need " + adpPerBattle + " to go into battle";
        yield return new WaitForSeconds(3f);
        notEnoughADP.text = "";
    }
    public void ShowEnemyDialog()
    {
        AdventurePoint adp = (AdventurePoint)FindObjectOfType(typeof(AdventurePoint));
        if (adp.AdventurePoints >= adpPerBattle)
        {
            toBattleSound.Play();
            Enemy.enabled = true;
            FightWithEnemy();
        }
        else
        {
            StartCoroutine(NotEnoughADP());
        }


    }

    public void BattleEnemy()
    {
        AdventurePoint adp = (AdventurePoint)FindObjectOfType(typeof(AdventurePoint));
        adp.AdventurePoints -= 10;
        waitingCountDownForBattle = 180f;
        if (enemyPowerLevel == EnemyPowerLevel.Low && fightMode == FightMode.InFight)
        {
            enemyCounter = Random.Range(1, 5);// search random enemy in the low level, this will continue in the other levels
            Debug.Log(enemyCounter);
            if (FirstToFifthLevelEnemys[enemyCounter].Damage < characterInfo.Damage || FirstToFifthLevelEnemys[enemyCounter].Health < characterInfo.Health)
            {
                userInfo.SetCharacterLevel(FirstToFifthLevelEnemys[enemyCounter].enemyLevelDrop);
                PlayerPrefs.SetInt("ADPOINTS",adp.AdventurePoints);
                StartCoroutine(WaitToBattleAgain());
            }
           
        }

    }

    IEnumerator WaitToBattleAgain()
    {
        //TODO --> the waiting time for adventure can be lowered down in the upgrade show so the player can fight more
        Enemy.enabled = false;
        AdventureButton.interactable = false;
        BattleLoggerText.text = "You have to wait 3 minutes for the next Adventure";
        yield return new WaitForSeconds(3f);
        BattleLoggerText.text = "";
        startCounter = true;
        yield return new WaitForSeconds(waitingCountDownForBattle);
        AdventureButton.interactable = true;

    }

    void FightWithEnemy()
    {   
        fightMode = FightMode.InFight;
        
        Name.text= "Name - " + FirstToFifthLevelEnemys[enemyCounter].name;
        Health.text = "Health - " +FirstToFifthLevelEnemys[enemyCounter].Health.ToString();
        Damage.text = "Damage - " + FirstToFifthLevelEnemys[enemyCounter].Damage.ToString();
        Level.text = "Level - " +FirstToFifthLevelEnemys[enemyCounter].level.ToString();
        Debug.Log("fighting enemy " + FirstToFifthLevelEnemys[enemyCounter].name);
    }
}


enum FightMode
{
    None,
    InFight
}


enum EnemyPowerLevel
{
    Low, // requires 10 ADP to battle
    Middle,// requires 20 ADP to battle
    High,// requires 30 ADP to battle
    Extreme // requires 50 ADP to battle
}