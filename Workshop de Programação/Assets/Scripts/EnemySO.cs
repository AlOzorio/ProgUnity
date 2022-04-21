using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
public class EnemySO : ScriptableObject
{
    public Sprite sprite;
    public int hp;
    public int damage;
    public int reward;
    public float speed;
}
