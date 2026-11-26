using UnityEngine;
namespace DirtyCity {
[RequireComponent(typeof(CharacterController),typeof(CoastalPlayer))]
public sealed class VehicleInteraction:MonoBehaviour{
 [SerializeField] Camera view;
 [SerializeField] LayerMask vehicleLayers,solidLayers;
 [SerializeField] GameObject characterVisual,walkingControls,drivingControls;
 [SerializeField] float radius=3.5f;
 CoastalPlayer player;CharacterController capsule;CoastalVehicle active;
 Vector3 cameraVel;
 void Awake(){player=GetComponent<CoastalPlayer>();capsule=GetComponent<CharacterController>();if(!view)view=Camera.main;}
 void Update(){
#if UNITY_EDITOR || UNITY_STANDALONE
 if(Input.GetKeyDown(KeyCode.E))Interact();
 if(active){active.SetGas(Input.GetAxisRaw("Vertical"));active.SetSteer(Input.GetAxisRaw("Horizontal"));active.SetBrake(Input.GetKey(KeyCode.Space)?1:0);}
#endif
 }
 public void Interact(){if(active){Exit();return;}CoastalVehicle best=null;float dist=radius*radius;foreach(var hit in Physics.OverlapSphere(transform.position,radius,vehicleLayers,QueryTriggerInteraction.Ignore)){var v=hit.GetComponentInParent<CoastalVehicle>();if(!v||!v.Seat||!v.ExitPoint)continue;float d=(v.Seat.position-transform.position).sqrMagnitude;if(d<dist){dist=d;best=v;}}if(!best)return;active=best;player.enabled=false;capsule.enabled=false;if(characterVisual)characterVisual.SetActive(false);active.SetOccupied(true);Toggle(true);}
 void Exit(){if(active.SpeedKph>4)return;Vector3 p=active.ExitPoint.position;float half=Mathf.Max(capsule.radius,capsule.height*.5f);Vector3 c=p+capsule.center;float stem=half-capsule.radius;if(Physics.CheckCapsule(c+Vector3.up*stem,c-Vector3.up*stem,capsule.radius,solidLayers,QueryTriggerInteraction.Ignore))return;active.SetOccupied(false);transform.SetPositionAndRotation(p,Quaternion.Euler(0,active.transform.eulerAngles.y,0));active=null;if(characterVisual)characterVisual.SetActive(true);capsule.enabled=true;player.enabled=true;Toggle(false);}
 void Toggle(bool driving){if(walkingControls)walkingControls.SetActive(!driving);if(drivingControls)drivingControls.SetActive(driving);}
 public void Gas(float value){if(active)active.SetGas(value);}public void Steer(float value){if(active)active.SetSteer(value);}public void Brake(float value){if(active)active.SetBrake(value);}
 void LateUpdate(){if(!active)return;transform.position=active.Seat.position;if(!view)return;Vector3 target=active.transform.position+Vector3.up*1.6f;Vector3 offset=active.transform.rotation*new Vector3(0,2.3f,-6.5f);if(Physics.SphereCast(target,.25f,offset.normalized,out RaycastHit hit,offset.magnitude,solidLayers,QueryTriggerInteraction.Ignore))offset=offset.normalized*Mathf.Max(.3f,hit.distance-.25f);Vector3 desired=target+offset;view.transform.position=Vector3.SmoothDamp(view.transform.position,desired,ref cameraVel,.10f);view.transform.LookAt(target);}
}
}