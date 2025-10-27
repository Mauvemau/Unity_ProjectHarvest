using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonFeedback : MonoBehaviour,
    ISelectHandler,
    ISubmitHandler,
    IPointerClickHandler,
    IPointerEnterHandler {
    
    protected virtual void OnButtonClicked() {}
    protected virtual void OnButtonSelected() {}

    public void OnSelect(BaseEventData eventData) => OnButtonSelected();
    public void OnPointerEnter(PointerEventData eventData) => OnButtonSelected();
    public void OnSubmit(BaseEventData eventData) => OnButtonClicked();
    public void OnPointerClick(PointerEventData eventData) => OnButtonClicked();
}
