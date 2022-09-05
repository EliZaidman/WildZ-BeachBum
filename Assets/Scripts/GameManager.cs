    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public string Turn;


    [Header("Cards Pos")]
    public List<Card> PlayerCards;
    [Space]
    public List<Card> AICards;
    [Space]


    [Header("Cards Pos")]
    public List<Transform> AI_CardsPos;
    [Space]
    public List<Transform> Human_CardsPos;
}
