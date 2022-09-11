using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class AI : MonoBehaviour
{
    #region Instance
    public static AI Instance { get; private set; }
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

    [SerializeField] float FakeTurnDuration;
    public List<Card> AICards;
    bool playedCard = false;

    private void Update()
    {
        if (GameManager.Instance.Turn == "AI" && !playedCard)
        {
            StartCoroutine(PlayCard());
        }
    }
    IEnumerator PlayCard()
    {
        playedCard = true;
        yield return new WaitForSeconds(FakeTurnDuration);
        GameManager.Instance.ToggleTurnOrder();

        var suggestedlist = AICards.Where(c => c._color == CardManager.Instance.BoardTopCard()._color || c.CardNum == CardManager.Instance.BoardTopCard().CardNum).ToList();

        if (suggestedlist.Count == 0)
        {
            StartCoroutine(CardManager.Instance.TopCard().AIDrawCard());
            yield return new WaitForSeconds(0.25f);
            EventManager.Instance.SortHand?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            StartCoroutine(suggestedlist[0].SortToBoard());
            print(suggestedlist[0]);
        }
        playedCard = false;
    }


}
