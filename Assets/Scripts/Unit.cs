using System;
using UnityEngine;

[Serializable]
public class Unit
{
    [SerializeField] float health;
    [SerializeField] float attack;
    [SerializeField] float defense;
    [SerializeField] float speed;

    public Unit(float health, float attack, float defense, float speed)
    {
        this.health = health;
        this.attack = attack;
        this.defense = defense;
        this.speed = speed;
    }

    public float Health { get { return health; } set { health = value; } }
    public float Attack { get { return attack; } set { attack = value; } }
    public float Defense { get { return defense; } set { defense = value; } }
    public float Speed { get { return speed; } set { speed = value; } }
}
