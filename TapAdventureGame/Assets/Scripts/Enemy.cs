using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemys/Enemy")]

public class Enemy : ScriptableObject {

    public string Name;
    public float Health;
    public float Damage;
    public int level;

    // add something that will make drops from the enemy
}
