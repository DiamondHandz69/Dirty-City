using UnityEngine;
using UnityEngine.Events;
namespace DirtyCity{
public sealed class DeliveryMission:MonoBehaviour{
 [SerializeField] Transform trackedPlayer;[SerializeField] Transform[] pickups;[SerializeField] Transform destination;
 [SerializeField] float timeLimit=120,pickupRadius=2.5f;
 [SerializeField] int reward=150;
 [SerializeField] UnityEvent onStarted,onCompleted,onFailed;
 public float SecondsLeft{get;private set;}public int Collected{get;private set;}public bool Running{get;private set;}
 bool[] collected;
 public int Total=>pickups==null?0:pickups.Length;
 public int XP=>PlayerPrefs.GetInt("DirtyCity.XP",0);
 public void Begin(){if(!trackedPlayer||!destination||Total==0)return;foreach(var p in pickups)if(!p)return;collected=new bool[Total];Collected=0;SecondsLeft=timeLimit;Running=true;foreach(var p in pickups)p.gameObject.SetActive(true);destination.gameObject.SetActive(false);onStarted?.Invoke();}
 void Update(){if(!Running)return;SecondsLeft=Mathf.Max(0,SecondsLeft-Time.deltaTime);if(SecondsLeft<=0){Running=false;Hide();onFailed?.Invoke();return;}for(int i=0;i<Total;i++){if(collected[i])continue;if((trackedPlayer.position-pickups[i].position).sqrMagnitude<=pickupRadius*pickupRadius){collected[i]=true;Collected++;pickups[i].gameObject.SetActive(false);}}if(Collected!=Total)return;destination.gameObject.SetActive(true);if((trackedPlayer.position-destination.position).sqrMagnitude<=pickupRadius*pickupRadius){Running=false;Hide();PlayerPrefs.SetInt("DirtyCity.XP",XP+reward);PlayerPrefs.Save();onCompleted?.Invoke();}}
 void Hide(){foreach(var p in pickups)p.gameObject.SetActive(false);destination.gameObject.SetActive(false);}
}
}