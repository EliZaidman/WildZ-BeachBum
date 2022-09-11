using System;
using System.Windows;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public class CardManager : MonoBehaviour
{
    #region Instance
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
    #endregion

    #region List
    public List<Card> Deck;

    public List<Card> Board;

    public List<Card> RedCards;

    public List<Card> BlueCards;

    public List<Card> YellowCards;

    public List<Card> GreenCards;
    #endregion

    GameManager manager;
    AI _ai;
    Player _player;
    bool givePlayer = true;
    public int CardsInDeck;
    public int highestLayer = 1;

    private void Start()
    {
        _player = Player.Instance;
        _ai = AI.Instance;

        manager = GameManager.Instance;
        StartCoroutine(MakeDeck());
    }

    private void Update()
    {
        TopCard().TopCard = true;
        if (Board.Count > 0)
            BoardTopCard()._front.sortingOrder = 99;
        if (Board.Count > 1)
            UnderTopBoard()._front.sortingOrder = 98;
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
                newCard = Instantiate(RedCards[rndList], manager.DeckTray.position, Quaternion.identity);
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
                TopCard().BelongsTo = "Player";
                _player.PlayerCards.Add(TopCard());
                Deck.Remove(TopCard());
                TopCard().eve.StartGameEvent += TopCard().StartGame;
                EventManager.Instance.StartGameEvent?.Invoke(this, EventArgs.Empty);
                ToggleReciveCard();
                TopCard().TopCard = false;
            }
            else
            {
                TopCard().BelongsTo = "AI";
                _ai.AICards.Add(TopCard());
                Deck.Remove(TopCard());
                TopCard().eve.StartGameEvent += TopCard().StartGame;
                EventManager.Instance.StartGameEvent?.Invoke(this, EventArgs.Empty);
                ToggleReciveCard();
                TopCard().TopCard = false;

            }
        }
        manager.ToggleTurnOrder();
        StartCoroutine(_player.SortHand());
        TopCard()._front.sortingOrder = 1;
        StartCoroutine(TopCard().SortToBoard());
    }
    public Card TopCard()
    {
        return Deck[Deck.Count - 1];
    }
    public Card BoardTopCard()
    {
        return Board[Board.Count - 1];
    }
    public Card UnderTopBoard()
    {
        return Board[Board.Count - 2];

    }
}
