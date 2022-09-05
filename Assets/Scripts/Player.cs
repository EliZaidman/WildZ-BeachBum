using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
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

    [Header("Decks")]
    public List<Card> PlayerCards;

    private void Interactable()
    {
        foreach (var item in PlayerCards)
        {
            item.interactable = true;
        }
    }
}
