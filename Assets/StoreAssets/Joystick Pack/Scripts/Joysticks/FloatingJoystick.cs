using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class FloatingJoystick : Joystick
{
    Image mask;
    protected override void Start()
    {
        base.Start();
        mask = gameObject.transform.parent.GetComponent<Image>();
        //background.gameObject.SetActive(false);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        background.anchoredPosition = ScreenPointToAnchoredPosition(eventData.position);
        background.gameObject.SetActive(true);
        mask.enabled=false;
        base.OnPointerDown(eventData);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        mask.enabled = true;
        base.OnPointerUp(eventData);
    }
}