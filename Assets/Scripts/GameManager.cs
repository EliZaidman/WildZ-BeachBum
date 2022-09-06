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

    public int MaxCardInHand;

    public Transform CardsTray;
    public Transform DeckTray;



    public string Turn;
    public float SortDurasion;





    [Header("Cards Pos")]
    public List<Transform> AI_CardsPos;
    [Space]
    public List<Transform> Player_CardsPos;

    private void Start()
    {
        MaxCardInHand = AI_CardsPos.Count;
    }
    private void Update()
    {


    }

    public void ToggleTurnOrder()
    {
        if (Turn == "Human")
        {
            Turn = "AI";
        }

        else if (Turn == "AI")
        {
            Turn = "Human";
        }

        else
        {
            Turn = "AI";
        }
    }
}
