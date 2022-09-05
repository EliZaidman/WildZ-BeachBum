using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{

    AI _ai;
    Player _player;
    public SpriteRenderer _sprite;
    GameManager manager;
    public int FindSlot;
    public float duration;
    bool Special;
    public string BelongsTo;

    bool sorted = false;
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
        _player = Player.Instance;
        _ai = AI.Instance;
        manager = GameManager.Instance;
    }
    private void Update()
    {
        if (BelongsTo == "Player")
        {
            FindSlot = _player.PlayerCards.IndexOf(this);
        }
        if (BelongsTo == "AI")
        {
            FindSlot = _ai.AICards.IndexOf(this);
        }


    }
    private void LateUpdate()
    {
        //Only if you are inside aslot sort yourself
        if (BelongsTo == "Player" && !sorted)
        {
            StartCoroutine(PlayerSort());
        }

        if (BelongsTo == "AI" && !sorted)
        {
            StartCoroutine(AISort());
        }
    }



    IEnumerator PlayerSort()
    {
        //print("Sorting" + this);
        float t = 0;
        print("Sorting Player");
        while (t < duration)
        {
            t += Time.deltaTime / duration;
            transform.position = Vector2.MoveTowards(transform.position, manager.Player_CardsPos[FindSlot].position, t / duration);
            //yield return new WaitUntil(() => transform.position == mag.MagazineSlots[FindSlot].position);
            yield return sorted = true;
        }

    }

    IEnumerator AISort()
    {
        //print("Sorting" + this);
        print("Sorting AI");

        float t = 0;
        while (t < duration)
        {
            t += Time.deltaTime / duration;
            transform.position = Vector2.MoveTowards(transform.position, manager.AI_CardsPos[FindSlot].position, t / duration);
            //yield return new WaitUntil(() => transform.position == mag.MagazineSlots[FindSlot].position);
            yield return sorted = true;
        }
    }
}
