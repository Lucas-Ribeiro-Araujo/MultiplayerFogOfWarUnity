using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ResizeWindow : MonoBehaviour, IDragHandler
{
    [SerializeField] private RectTransform dragTransform;
    [SerializeField] private Vector2 minSize;
    [SerializeField] private Vector2 maxSize;

    public void OnDrag(PointerEventData eventData)
    {
            Vector2 sizeDelta = Vector2.zero;

            sizeDelta += Vector2.right * eventData.delta.x / GameManager.instance.Canvas.scaleFactor;
            sizeDelta += Vector2.down * eventData.delta.y / GameManager.instance.Canvas.scaleFactor;
           
            Vector2 finalSize = dragTransform.sizeDelta + sizeDelta;

            finalSize.x = Mathf.Clamp(finalSize.x, minSize.x, maxSize.x);
            finalSize.y = Mathf.Clamp(finalSize.y, minSize.y, maxSize.y);

            dragTransform.sizeDelta = finalSize;

    }
}
