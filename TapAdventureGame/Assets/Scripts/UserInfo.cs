using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInfo : MonoBehaviour
{

    public Text CharacterDamage;
    public Text CharacterHealth;
    public Text CharacterLevel;
    Character character;
    public float characterLevel;
    int canStart;
    public static UserInfo _Instance;

    private void Awake()
    {
        _Instance = this;
    }

    void Start()
    {

         character = new Character();
        canStart = PlayerPrefs.GetInt("start");
        if(canStart == 1)
         character.Level = PlayerPrefs.GetFloat("LVL", character.Level);
    }

    public void SetCharacterLevel(float level)
    {
        character.Level += level;
        Debug.Log("level " + character.Level);
        PlayerPrefs.SetFloat("LVL", character.Level);
        canStart = 1;
        PlayerPrefs.SetInt("start", canStart);
    }


    public void RunFromEnemy()
    {
        character.Level -= 0.1f;
        PlayerPrefs.SetFloat("LVL", character.Level);
    }


    private void Update()
    {
        CharacterDamage.text = "Damage : " + character.Damage.ToString();
        CharacterHealth.text = "Health : " + character.Health.ToString();
        CharacterLevel.text = "Level  : " + character.Level.ToString();
    }
}
