using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Battle_Tactics.Battle
{
    public class ClickDetector : MonoBehaviour, IPointerDownHandler, IPointerClickHandler,
        IPointerUpHandler, IPointerEnterHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log("Clicked: " + eventData.pointerCurrentRaycast.gameObject.name);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log("Mouse Down: " + eventData.pointerCurrentRaycast.gameObject.name);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            Debug.Log("Mouse Enter");
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            Debug.Log("Mouse Up");
        }
    }
}
