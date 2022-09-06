using System;
using System.Windows;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public class CardManager : MonoBehaviour
{
    public static CardManager Instance { get; private set; }
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


    GameManager manager;
    AI _ai;
    Player _player;
    bool givePlayer = true;
    public int CardsInDeck;

    public List<Card> Deck;

    public List<Card> Board;

    public List<Card> RedCards;

    public List<Card> BlueCards;

    public List<Card> YellowCards;

    public List<Card> GreenCards;


    private void Start()
    {
        _player = Player.Instance;
        _ai = AI.Instance;

        manager = GameManager.Instance;
        StartCoroutine(MakeDeck());
    }

    private void ToggleReciveCard()
    {
        givePlayer = !givePlayer;
    }
    private IEnumerator MakeDeck()
    {


        for (int i = 0; i < CardsInDeck; i++)
        {
            Card newCard;
            int rnd = Random.Range(0, 4);
            int rndList = Random.Range(0, 10);
            if (rnd == 0)
            {
                newCard = Instantiate(RedCards[rndList],manager.DeckTray.position,Quaternion.identity);
                Deck.Add(newCard);
                newCard._front.sortingOrder = i;
            }
            if (rnd == 1)
            {
                newCard = Instantiate(BlueCards[rndList], manager.DeckTray.position, Quaternion.identity);
                Deck.Add(newCard);
                newCard._front.sortingOrder = i;
            }
            if (rnd == 2)
            {
                newCard = Instantiate(YellowCards[rndList], manager.DeckTray.position, Quaternion.identity);
                Deck.Add(newCard);
                newCard._front.sortingOrder = i;
            }
            if (rnd == 3)
            {
                newCard = Instantiate(GreenCards[rndList], manager.DeckTray.position, Quaternion.identity);
                newCard._front.sortingOrder = i;
                Deck.Add(newCard);
            }
        }

        yield return new WaitForSeconds(1);
        for (int i = 0; i < 20; i++)
        {
            if (_ai.AICards.Count == manager.MaxCardInHand && _player.PlayerCards.Count == manager.MaxCardInHand)
            {
                break;
            }
            else
                yield return new WaitForSeconds(0.35f);

            if (givePlayer)
            {
                _player.PlayerCards.Add(Deck[Deck.Count -1]);
                Deck[Deck.Count -1].BelongsTo = "Player";
                Deck.Remove(Deck[Deck.Count -1]);
                Deck[Deck.Count -1].eve.StartGameEvent += Deck[Deck.Count -1].StartGame;
                EventManager.Instance.StartGameEvent?.Invoke(this, EventArgs.Empty);
                ToggleReciveCard();
            }
            else
            {
                _ai.AICards.Add(Deck[Deck.Count -1]);
                Deck[Deck.Count -1].BelongsTo = "AI";
                Deck.Remove(Deck[Deck.Count -1]);
                Deck[Deck.Count -1].eve.StartGameEvent += Deck[Deck.Count -1].StartGame;
                EventManager.Instance.StartGameEvent?.Invoke(this, EventArgs.Empty);
                ToggleReciveCard();
            }
        }
        manager.ToggleTurnOrder();
        StartCoroutine(_player.SortHand());
    }


}
