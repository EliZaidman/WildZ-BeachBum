using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    #region Instance
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
    #endregion

    [Header("Cards Pos")]
    public List<Transform> AI_CardsPos;
    [Space]
    public List<Transform> Player_CardsPos;

    [SerializeField] TextMeshProUGUI turnTPM;
    public Transform CardsTray;
    public Transform DeckTray;
    public int MaxCardInHand;
    public string Turn;
    public float SortDurasion;

    private void Start()
    {
        MaxCardInHand = AI_CardsPos.Count;
    }
    private void Update()
    {
        turnTPM.text = Turn;
        
    }
    public void ToggleTurnOrder()
    {
        if (Turn == "Player")
        {
            Turn = "AI";
        }

        else if (Turn == "AI")
        {
            Turn = "Player";
        }

        else
        {
            Turn = "Player";
        }
    }
}
