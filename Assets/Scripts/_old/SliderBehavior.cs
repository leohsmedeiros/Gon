using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;// Required when using Event data.

public class SliderBehavior : MonoBehaviour,
IEventSystemHandler,
IPointerUpHandler,
IPointerDownHandler {

	public Slider slider;

	public void OnPointerDown(PointerEventData eventData)
	{
		if (eventData.button != PointerEventData.InputButton.Left)
			return;
		
		// Selection tracking
		EventSystem.current.SetSelectedGameObject(gameObject, eventData);
	}

	public void OnPointerUp (PointerEventData data) 
	{
//		data.
		slider.value = 0;
//		Debug.Log ("Deselected");
	}
	
}
