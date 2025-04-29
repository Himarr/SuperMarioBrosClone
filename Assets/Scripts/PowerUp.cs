using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    Player player;
    public bool canMove = false;

    public Player Player { get => player; set => player = value; }
}
