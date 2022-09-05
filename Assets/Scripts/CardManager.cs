using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{


    public List<Card> Deck;

    public List<Card> RedCards;
    public List<Card> BlueCards;
    public List<Card> YellowCards;
    public List<Card> GreenCards;


    private void Start()
    {
        MakeDeck();

    }


    private void MakeDeck()
    {


        for (int i = 0; i < 52; i++)
        {
            int rnd = Random.Range(0, 4);
            print(rnd);
            int rndList = Random.Range(0, 10);
            if (rnd == 0)
            {
                Deck.Add(RedCards[rndList]);
                Deck[^1]._sprite.sortingOrder += 1;
            }
            if (rnd == 1)
            {
                Deck.Add(BlueCards[rndList]);
                Deck[^1]._sprite.sortingOrder += 1;
            }
            if (rnd == 2)
            {
                Deck.Add(YellowCards[rndList]);
                Deck[^ 1]._sprite.sortingOrder += 1;
            }
            if (rnd == 3)
            {
                Deck.Add(GreenCards[rndList]);
                Deck[^ 1]._sprite.sortingOrder += 1;
            }
        }
    }
}
