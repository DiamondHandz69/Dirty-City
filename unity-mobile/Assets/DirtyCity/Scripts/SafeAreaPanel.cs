using UnityEngine;
namespace DirtyCity{
[RequireComponent(typeof(RectTransform))]
public sealed class SafeAreaPanel:MonoBehaviour{
 RectTransform panel;Rect last;int w,h;
 void Awake(){panel=GetComponent<RectTransform>();Apply();}
 void Update(){if(last!=Screen.safeArea||w!=Screen.width||h!=Screen.height)Apply();}
 void Apply(){last=Screen.safeArea;w=Screen.width;h=Screen.height;if(w<=0||h<=0)return;panel.anchorMin=new Vector2(last.xMin/w,last.yMin/h);panel.anchorMax=new Vector2(last.xMax/w,last.yMax/h);panel.offsetMin=panel.offsetMax=Vector2.zero;}
}
}