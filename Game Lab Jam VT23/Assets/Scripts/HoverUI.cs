using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Vector3 hoverScaleIncrease;

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale += hoverScaleIncrease;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale -= hoverScaleIncrease;
    }
}
