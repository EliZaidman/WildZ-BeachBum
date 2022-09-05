using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{

    static string Color;
    static int Num;
    public SpriteRenderer _sprite;

    private void Start()
    {
    }

    enum C_Color
    {
        Red,
        Blue,
        Green,
        Yellow,

    }
}
