using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }


    public string Turn;


    [Header("Decks")]
    public List<Card> PlayerCards;
    [Space]
    public List<Card> AICards;
    [Space]


    [Header("Cards Pos")]
    public List<Transform> AI_CardsPos;
    [Space]
    public List<Transform> Player_CardsPos;

    private void Update()
    {


    }
}
