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

    void Start()
    {
        character = new Character();
        characterLevel = PlayerPrefs.GetFloat("LEVEL");
    }

    public void SetCharacterLevel(float level)
    {
        characterLevel = character.Level;
        character.Level += level;
        PlayerPrefs.SetFloat("LEVEL", characterLevel);
    }

    private void Update()
    {

        CharacterDamage.text = "Damage : " + character.Damage.ToString();
        CharacterHealth.text = "Health : " + character.Health.ToString();
        CharacterLevel.text = "Level  : " + characterLevel.ToString("F0");
    }
}
