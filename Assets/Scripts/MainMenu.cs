using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class MainMenu : MonoBehaviour
{
    [SerializeField] List<RectTransform> buttons = new List<RectTransform>();
    [SerializeField] List<Material> buttonMaterials = new List<Material>();

    private Dictionary<RectTransform, Vector2> originalPositions = new Dictionary<RectTransform, Vector2>();
    private Dictionary<RectTransform, Tween> activeTweens = new Dictionary<RectTransform, Tween>();
    private Dictionary<RectTransform, Material> buttonMaterialMap = new Dictionary<RectTransform, Material>();

    void Awake()
    {
        if (buttonMaterials.Count != buttons.Count)
        {
            Debug.LogError("Button materials count doesn't match buttons count!");
            return;
        }


        for (int i = 0; i < buttons.Count; i++)
        {
            buttonMaterialMap[buttons[i]] = buttonMaterials[i];
            buttonMaterials[i].SetFloat("_Fade_Right_Amount", 0f);
        }

        foreach (RectTransform rect in buttons)
        {
            Vector2 startPos = rect.anchoredPosition;
            startPos.x = -850f;
            rect.anchoredPosition = startPos;

            originalPositions[rect] = new Vector2(-350f, startPos.y);
            
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
        
        EventTrigger.Entry entryEnter = new EventTrigger.Entry();
        entryEnter.eventID = EventTriggerType.PointerEnter;
        entryEnter.callback.AddListener((data) => OnHoverEnter(rect));
        trigger.triggers.Add(entryEnter);
        
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

    void AnimateButtonEffect(RectTransform button)
    {
        if (buttonMaterialMap.TryGetValue(button, out Material mat))
        {
            mat.SetFloat("_Fade_Right_Amount", 0f);
            
            DOTween.To(() => mat.GetFloat("_Fade_Right_Amount"), 
                     x => mat.SetFloat("_Fade_Right_Amount", x), 
                     5f, 6f)
                  .SetEase(Ease.OutQuad);
        }
    }

    private void OnDestroy()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            buttonMaterialMap[buttons[i]] = buttonMaterials[i];
            buttonMaterials[i].SetFloat("_Fade_Right_Amount", 1.0f);
        }
    }

    public void Play()
    {
        if (buttons.Count > 0) AnimateButtonEffect(buttons[0]);
    }
    
    public void Settings()
    {
        if (buttons.Count > 1) AnimateButtonEffect(buttons[1]);
    }
    
    public void Credits()
    {
        if (buttons.Count > 2) AnimateButtonEffect(buttons[2]);
    }
    
    public void Exit()
    {
        if (buttons.Count > 3) AnimateButtonEffect(buttons[3]);
    }
}