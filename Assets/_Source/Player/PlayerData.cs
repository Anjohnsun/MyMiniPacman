using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public int Hp { get; set; }
    public int Points { get; set; }

    public PlayerData(int hp, int points)
    {
        Hp = hp;
        Points = points;
    }
}
