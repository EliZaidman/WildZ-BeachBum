using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{


    public List<Card> Deck;
    GameManager manager;
    public List<Card> RedCards;
    public List<Card> BlueCards;
    public List<Card> YellowCards;
    public List<Card> GreenCards;

    bool givePlayer = true;

    private void Start()
    {
        manager = GameManager.Instance;
        StartCoroutine(MakeDeck());
    }

    private void ToggleReciveCard()
    {
        givePlayer = !givePlayer;
    }
    private IEnumerator MakeDeck()
    {


        for (int i = 0; i < 52; i++)
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
                Deck.Add(newCard);
                newCard._sprite.sortingOrder = i;
            }
        }

        yield return new WaitForSeconds(1);
        for (int i = 0; i < 20; i++)
        {
            if (manager.AICards.Count == 6 && manager.PlayerCards.Count == 6)
            {
                break;
            }
            else
                yield return new WaitForSeconds(0.35f);

            if (givePlayer)
            {
                manager.PlayerCards.Add(Deck[^1]);
                Deck[^1].BelongsTo = "Player";
                Deck.Remove(Deck[^1]);
                ToggleReciveCard();
            }
            else
            {
                manager.AICards.Add(Deck[^1]);
                Deck[^1].BelongsTo = "AI";
                Deck.Remove(Deck[^1]);
                ToggleReciveCard();
            }
        }

    }
}
