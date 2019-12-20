using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public Color color { get; }
    public Teams team;
    public Player(Color color, Teams team)
    {
        this.color = color;
        this.team = team;
    }
}
