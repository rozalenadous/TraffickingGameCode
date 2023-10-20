using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollToBottom : MonoBehaviour
{
    private RectTransform rectTransform;
    private ScrollRect scrollRect;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        scrollRect = GetComponent<ScrollRect>();
    }

    private void FixedUpdate()
    {
        // rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, 0);
        // scrollRect.normalizedPosition = new Vector2(scrollRect.normalizedPosition.x, 0);
        scrollRect.normalizedPosition = new Vector2(0, 0);
    }
}
