using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{

    static string Color;
    static int Num;
    public SpriteRenderer _sprite;
    GameManager manager;
    public int FindSlot;
    public float duration;
    bool CanSort;
    public string BelongsTo;



    private void Start()
    {
        manager = GameManager.Instance;
    }
    private void Update()
    {
        if (BelongsTo == "Player")
        {
            FindSlot = manager.PlayerCards.IndexOf(this);
        }
        if (BelongsTo == "AI")
        {
            FindSlot = manager.AICards.IndexOf(this);
        }


    }
    private void LateUpdate()
    {
        //Only if you are inside aslot sort yourself
        if (BelongsTo == "Player")
        {
            StartCoroutine(PlayerSort());
        }

        if (BelongsTo == "AI")
        {
            StartCoroutine(AISort());
        }
    }



    IEnumerator PlayerSort()
    {
        //print("Sorting" + this);
        float t = 0;
        if (t < duration)
        {
            t += Time.deltaTime / duration;
            transform.position = Vector2.MoveTowards(transform.position, manager.Player_CardsPos[FindSlot].position, t / duration);
            //yield return new WaitUntil(() => transform.position == mag.MagazineSlots[FindSlot].position);
            yield return null;
        }

    }

    IEnumerator AISort()
    {
        //print("Sorting" + this);
        float t = 0;
        if (t < duration)
        {
            t += Time.deltaTime / duration;
            transform.position = Vector2.MoveTowards(transform.position, manager.AI_CardsPos[FindSlot].position, t / duration);
            //yield return new WaitUntil(() => transform.position == mag.MagazineSlots[FindSlot].position);
            yield return null;
        }

    }
}
