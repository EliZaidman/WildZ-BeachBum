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
    public bool TopCard = false;

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

    private void Update()
    {
        if (!_cardman.Deck.Contains(this))
        {
            TopCard = false;
        }
        if (_cardman.Deck.Contains(this) && !TopCard)
        {
            GetComponent<BoxCollider2D>().enabled = false;

        }
        if (TopCard)
        {
            GetComponent<BoxCollider2D>().enabled = true;
        }
    }
    private void OnMouseDown()
    {
        if (TopCard)
        {
            StartCoroutine(DrawCard());
            print("DrawnCard");
        }

        if (CanPlayCard() && !_cardman.Deck.Contains(this) && !_cardman.Board.Contains(this) && !TopCard)
        {
            if (manager.Turn == "Player" && this.BelongsTo == "Player" || manager.Turn == "AI" && this.BelongsTo == "AI")
            {
                StartCoroutine(SortToBoard());
                manager.ToggleTurnOrder();
                _front.sortingOrder = _cardman.highestLayer + 2;
                _cardman.highestLayer += 1;
            }
            else
            {
                print("ITS NOT YOUR TURN SIR!");
            }
        }
    }

    private bool CanPlayCard()
    {
        if (_cardman.BoardTopCard()._color == this._color)
        {
            return true;
        }
        else if (_cardman.BoardTopCard().CardNum == this.CardNum)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    #region Sort Corutines
    bool sorted = false;

    IEnumerator PlayerSort()
    {
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
        FlipCard();
        //BelongsTo = "AI";
        float t = 0;
        while (t < duration)
        {
            t += Time.deltaTime / duration;
            transform.position = Vector2.MoveTowards(transform.position, manager.AI_CardsPos[CardIndexPreSort()].position, t / duration);
            yield return null;
        }
    }

    public IEnumerator SortToBoard()
    {
        FlipCard();
        _cardman.Board.Add(this);
        if (_cardman.Deck.Contains(this))
            _cardman.Deck.Remove(this);

        if (_player.PlayerCards.Contains(this))
        {
            _player.PlayerCards.Remove(this);
            _player.sortedHand.Remove(this);
        }

        if (_ai.AICards.Contains(this))
            _ai.AICards.Remove(this);
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

    public IEnumerator DrawCard()
    {

        if (manager.Turn == "Player")
        {
            FlipCard();
            BelongsTo = "Player";
            yield return new WaitForSeconds(0.2f);
            _cardman.Deck.Remove(this);
            _player.PlayerCards.Add(this);
            _player.sortedHand.Add(this);
            EventManager.Instance.SortHand?.Invoke(this, EventArgs.Empty);
            manager.ToggleTurnOrder();
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
