using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{

    AI _ai;
    Player _player;
    CardManager _cardman;
    [HideInInspector] public EventManager eve;
    public SpriteRenderer _front;
    public SpriteRenderer _back;
    GameManager manager;
    public int FindSlot;
    private float duration;
    bool Special;
    public string BelongsTo;
    public bool interactable = false;

    private void Awake()
    {
        _player = Player.Instance;
        _ai = AI.Instance;
        manager = GameManager.Instance;
        eve = EventManager.Instance;
    }
    public string _color;

    //1 = Red 2 = Green 3 = Blue = 4 Yellow
    public enum eColor
    {
        Red,
        Green,
        Blue,
        Yellow,
    }

    public int CardNum;
    private void Start()
    {
        _cardman = CardManager.Instance;
        eve.StartGameEvent += StartGame;
        eve.SortHand += SortHandEve;
        duration = manager.SortDurasion;
    }


    private void OnMouseDown()
    {
        if (this.BelongsTo == "Player" || manager.Turn == "AI")
        {
            if (_cardman.Board.Count == 0)
            {
                print("Inside");
                StartCoroutine(SortToBoard());
            }
            manager.ToggleTurnOrder();
        }

        else if (this.BelongsTo == "Player" || manager.Turn == "AI")
        {
            //CALL EVENT FOR AI ALGORITHEM
            manager.ToggleTurnOrder();

        }
    }


    #region Sort Corutines
    bool sorted = false;

    IEnumerator PlayerSort()
    {
        CardIndexPreSort();
        FlipCard();
        float t = 0;
        while (t < duration)
        {
            t += Time.deltaTime / duration;
            transform.position = Vector2.MoveTowards(transform.position, manager.Player_CardsPos[CardIndexPreSort()].position, t / duration);
            yield return null;
        }
    }

    IEnumerator AISort()
    {
        CardIndexPreSort();
        FlipCard();
        float t = 0;
        while (t < duration)
        {
            t += Time.deltaTime / duration;
            transform.position = Vector2.MoveTowards(transform.position, manager.AI_CardsPos[CardIndexPreSort()].position, t / duration);
            yield return null;
        }
    }

    IEnumerator SortToBoard()
    {
        CardIndexPreSort();
        _cardman.Board.Add(this);
        float t = 0;
        while (t < duration)
        {
            t += Time.deltaTime / duration;
            transform.position = Vector2.MoveTowards(transform.position, manager.CardsTray.position, t / duration);
            yield return null;
        }
    }

    IEnumerator SortInHand()
    {
        if (BelongsTo == "Player")
        {
            float t = 0;
            while (t < duration)
            {
                t += Time.deltaTime / duration;
                transform.position = Vector2.MoveTowards(transform.position, manager.Player_CardsPos[CardIndex()].position, t / duration);
                yield return null;
            }
        }
    }

    #endregion

    private void FlipCard()
    {
        print("Card Flipped");
        _front.gameObject.SetActive(true);
        _back.gameObject.SetActive(false);
    }
    int CardIndexPreSort()
    {
        if (BelongsTo == "Player")
        {
            return _player.PlayerCards.IndexOf(this);
        }
        if (BelongsTo == "AI")
        {
            return _ai.AICards.IndexOf(this);
        }
        return CardIndexPreSort();
    }

    int CardIndex()
    {
        return _player.sortedHand.IndexOf(this);
    }

    public void StartGame(object sender, EventArgs e)
    {

        if (BelongsTo == "Player" && !sorted)
        {
            StartCoroutine(PlayerSort());
        }

        if (BelongsTo == "AI" && !sorted)
        {
            StartCoroutine(AISort());
        }
    }
    public void SortHandEve(object sender, EventArgs e)
    {
        StartCoroutine(SortInHand());
    }


}
