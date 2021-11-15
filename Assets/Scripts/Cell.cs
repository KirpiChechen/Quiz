using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;

public class Cell : MonoBehaviour
{
    private CardData thisCard;
    private CardData correctCard;

    private SpawnManager spawnManager;
    private FadeWindow fadeWindow;

    private void Start()
    {
        spawnManager = FindObjectOfType<SpawnManager>();
        fadeWindow = FindObjectOfType<FadeWindow>();
    }

    public void SetCard(CardData card, CardData correctCard)
    {
        thisCard = card;
        this.correctCard = correctCard;
    }
    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;

        if (thisCard != correctCard)
        {
            Tween tween = transform.DOShakeScale(2f, .5f);
        }
        else
        {
            if (spawnManager.SecondSlot == null) spawnManager.SpawnSecondSlot();
            else if (spawnManager.SecondSlot != null && spawnManager.ThirdSlot == null) spawnManager.SpawnThirdSlot();
            else if (spawnManager.ThirdSlot != null) fadeWindow.FadeIn(1f);
        }
    }
}
