using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{

    AI _ai;
    Player _player;
    [HideInInspector]public EventManager eve;
    public SpriteRenderer _sprite;
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
    public enum color
    {
        Red,
        Blue,
        Green,
        Yellow,
    }
    public int CardNum;
    private void Start()
    {
        eve.StartGameEvent += StartGame;
        duration = manager.SortDurasion;
    }


    private void OnMouseDown()
    {
        
    }


    #region Sort Corutines
    bool sorted = false;

    IEnumerator PlayerSort()
    {
        CardIndex();
        float t = 0;
        while (t < duration)
        {
            t += Time.deltaTime / duration;
            transform.position = Vector2.MoveTowards(transform.position, manager.Player_CardsPos[CardIndex()].position, t / duration);
            yield return sorted = true;
        }
    }

    IEnumerator AISort()
    {
        CardIndex();
        float t = 0;
        while (t < duration)
        {
            t += Time.deltaTime / duration;
            transform.position = Vector2.MoveTowards(transform.position, manager.AI_CardsPos[CardIndex()].position, t / duration);
            yield return sorted = true;
        }
    }

    #endregion

    int CardIndex()
    {
        if (BelongsTo == "Player")
        {
            return _player.PlayerCards.IndexOf(this);
        }
        if (BelongsTo == "AI")
        {
            return _ai.AICards.IndexOf(this);
        }
        return CardIndex();
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

}
