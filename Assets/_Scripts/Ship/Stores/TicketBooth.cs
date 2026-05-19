/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicketBooth : StoreBase
{
    [Header("Ticket Booth")]
    [SerializeField] private float expectedWaitingTime = 3;
    [Tooltip("Per second")]
    [SerializeField] private float ratingPenalty = 5;
    [SerializeField] private float penaltyInterval = 0.5f;

    private readonly Dictionary<Visitor, Coroutine> lineCoroutines = new();

    public override Zone Zone => Zone.TicketBooth;

    protected override void NewVisitor(Visitor visitor)
    {
        lineCoroutines[visitor] = StartCoroutine(WaitTimer(visitor));
    }

    protected override void AddVisitor(ChangeLocation e)
    {
        if (e.Visitor.CurrentZone != Zone && lineCoroutines.TryGetValue(e.Visitor, out Coroutine c))
        {
            StopCoroutine(c);
            lineCoroutines.Remove(e.Visitor);
        }

        base.AddVisitor(e);
    }

    private IEnumerator WaitTimer(Visitor visitor)
    {
        yield return new WaitForSeconds(expectedWaitingTime);

        var tick = new WaitForSeconds(penaltyInterval);
        float penaltyPerTick = ratingPenalty * penaltyInterval;

        while (Visitors.Contains(visitor))
        {
            SetRating(Rating - penaltyPerTick);
            yield return tick;
        }
    }

    private void SetRating(float newRating)
    {
        float clamped = Mathf.Clamp(newRating, 0, 100);
        if (Mathf.Approximately(clamped, Rating)) return;

        Rating = clamped;
        OnRatingChanged(Rating);
    }

    private void OnRatingChanged(float newRating)
    {
        Debug.Log($"[TicketBooth] Rating changed: {newRating:F1}");
    }
}*/