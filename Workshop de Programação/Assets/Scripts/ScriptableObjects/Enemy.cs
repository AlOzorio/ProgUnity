using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
public class Enemy : ScriptableObject
{
    public Sprite sprite;
    public int health;
    public int damage;
    public int value;
    public int speed;
}
