using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInfo : MonoBehaviour {

    public Text CharacterDamage;
    public Text CharacterHealth;
    public Text CharacterLevel;

     Character character;
	void Start ()
    {
        character = new Character();
    }
    private void Update()
    {
       
        CharacterDamage.text =  "Damage : " +character.Damage.ToString();
        CharacterHealth.text =  "Health : " +character.Health.ToString();
        CharacterLevel.text =  "Level  : " + character.Level.ToString();
    }
}
