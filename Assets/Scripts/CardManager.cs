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

    bool givePlayer = true;

    private void Start()
    {
        MakeDeck();

    }

    private void ToggleCards()
    {
        givePlayer = !givePlayer;
    }
    private void MakeDeck()
    {


        for (int i = 0; i < 52; i++)
        {
            Card newCard;
            int rnd = Random.Range(0, 4);
            print(rnd);
            int rndList = Random.Range(0, 10);
            if (rnd == 0)
            {
                Deck.Add(RedCards[rndList]);
            }
            if (rnd == 1)
            {
                Deck.Add(BlueCards[rndList]);
            }
            if (rnd == 2)
            {
                Deck.Add(YellowCards[rndList]);
            }
            if (rnd == 3)
            {
                Deck.Add(GreenCards[rndList]);
            }
            newCard = Instantiate(Deck[^1], new Vector3(0, 0, 0), Quaternion.identity);
            newCard._sprite.sortingOrder = i;
        }



    }   
}
