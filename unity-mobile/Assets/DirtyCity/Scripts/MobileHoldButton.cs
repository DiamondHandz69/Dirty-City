using UnityEngine;using UnityEngine.Events;using UnityEngine.EventSystems;
namespace DirtyCity{
public sealed class MobileHoldButton:MonoBehaviour,IPointerDownHandler,IPointerUpHandler,IPointerExitHandler{
 [SerializeField] UnityEvent<float> onValue;[SerializeField] float heldValue=1;int pointer=int.MinValue;
 public void OnPointerDown(PointerEventData e){if(pointer!=int.MinValue)return;pointer=e.pointerId;onValue?.Invoke(heldValue);}
 public void OnPointerUp(PointerEventData e){if(e.pointerId==pointer)Release();}
 public void OnPointerExit(PointerEventData e){if(e.pointerId==pointer)Release();}
 void OnDisable(){Release();}void OnApplicationFocus(bool focused){if(!focused)Release();}
 void Release(){if(pointer==int.MinValue)return;pointer=int.MinValue;onValue?.Invoke(0);}
}
}