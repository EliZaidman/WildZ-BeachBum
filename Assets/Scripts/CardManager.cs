using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public class CardManager : MonoBehaviour
{
    public int CardsInDeck;
    AI _ai;
    Player _player;
    public List<Card> Deck;
    GameManager manager;
    public List<Card> RedCards;

    public List<Card> BlueCards;

    public List<Card> YellowCards;

    public List<Card> GreenCards;

    bool givePlayer = true;

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
                newCard = Instantiate(RedCards[rndList]);
                Deck.Add(newCard);
                newCard._sprite.sortingOrder = i;
            }
            if (rnd == 1)
            {
                newCard = Instantiate(BlueCards[rndList]);
                Deck.Add(newCard);
                newCard._sprite.sortingOrder = i;
            }
            if (rnd == 2)
            {
                newCard = Instantiate(YellowCards[rndList]);
                Deck.Add(newCard);
                newCard._sprite.sortingOrder = i;
            }
            if (rnd == 3)
            {
                newCard = Instantiate(GreenCards[rndList]);
                newCard._sprite.sortingOrder = i;
                Deck.Add(newCard);
            }
        }

        yield return new WaitForSeconds(1);
        for (int i = 0; i < 20; i++)
        {
            if (_ai.AICards.Count == 6 && _player.PlayerCards.Count == 6)
            {
                break;
            }
            else
                yield return new WaitForSeconds(0.35f);

            if (givePlayer)
            {
                _player.PlayerCards.Add(Deck[^1]);
                Deck[^1].BelongsTo = "Player";
                Deck.Remove(Deck[^1]);
                Deck[^1].eve.StartGameEvent += Deck[^1].StartGame;
                EventManager.Instance.StartGameEvent?.Invoke(this, EventArgs.Empty);
                ToggleReciveCard();
            }
            else
            {
                _ai.AICards.Add(Deck[^1]);
                Deck[^1].BelongsTo = "AI";
                Deck.Remove(Deck[^1]);
                Deck[^1].eve.StartGameEvent += Deck[^1].StartGame;
                EventManager.Instance.StartGameEvent?.Invoke(this, EventArgs.Empty);
                ToggleReciveCard();
            }
        }
        manager.ToggleTurnOrder();

    }
}
