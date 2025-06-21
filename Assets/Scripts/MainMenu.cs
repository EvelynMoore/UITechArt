using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class MainMenu : MonoBehaviour
{
    [SerializeField] List<RectTransform> buttons = new List<RectTransform>();

    private Dictionary<RectTransform, Vector2> originalPositions = new Dictionary<RectTransform, Vector2>();
    private Dictionary<RectTransform, Tween> activeTweens = new Dictionary<RectTransform, Tween>();

    void Awake()
    {
        foreach (RectTransform rect in buttons)
        {
            Vector2 startPos = rect.anchoredPosition;
            startPos.x = -850f;
            rect.anchoredPosition = startPos;

            originalPositions[rect] = new Vector2(-350f, startPos.y);

            // Add event handlers
            AddHoverEvents(rect);
        }

        StartCoroutine(TweenMenuIntoStartPos());
    }

    IEnumerator TweenMenuIntoStartPos()
    {
        foreach (RectTransform rect in buttons)
        {
            rect.DOAnchorPosX(-350f, 0.5f);
            yield return new WaitForSeconds(0.1f);
        }
    }

    void AddHoverEvents(RectTransform rect)
    {
        EventTrigger trigger = rect.gameObject.GetComponent<EventTrigger>();
        if (trigger == null)
            trigger = rect.gameObject.AddComponent<EventTrigger>();

        // PointerEnter
        EventTrigger.Entry entryEnter = new EventTrigger.Entry();
        entryEnter.eventID = EventTriggerType.PointerEnter;
        entryEnter.callback.AddListener((data) => OnHoverEnter(rect));
        trigger.triggers.Add(entryEnter);

        // PointerExit
        EventTrigger.Entry entryExit = new EventTrigger.Entry();
        entryExit.eventID = EventTriggerType.PointerExit;
        entryExit.callback.AddListener((data) => OnHoverExit(rect));
        trigger.triggers.Add(entryExit);
    }

    void OnHoverEnter(RectTransform rect)
    {
        activeTweens.TryGetValue(rect, out Tween tween);
        tween?.Kill();

        Vector2 targetPos = originalPositions[rect] + new Vector2(100f, 0);
        activeTweens[rect] = rect.DOAnchorPos(targetPos, 0.25f).SetEase(Ease.OutQuad);
    }

    void OnHoverExit(RectTransform rect)
    {
        activeTweens.TryGetValue(rect, out Tween tween);
        tween?.Kill();

        Vector2 targetPos = originalPositions[rect];
        activeTweens[rect] = rect.DOAnchorPos(targetPos, 0.25f).SetEase(Ease.OutQuad);
    }
    
    public void Play()
    {
        
    }
    
    public void Settings()
    {
        
    }
    
    public void Credits()
    {
        
    }
    
    public void Exit()
    {
        
    }
}
