using UnityEngine;
namespace DirtyCity {
[RequireComponent(typeof(Rigidbody))]
public sealed class CoastalVehicle : MonoBehaviour {
 [System.Serializable] public class Axle { public WheelCollider left,right; public Transform leftMesh,rightMesh; public bool steering,driven; }
 [SerializeField] Axle[] axles;
 [SerializeField] Transform centerOfMass;
 [SerializeField] float engineTorque=1400, brakeTorque=2600, maxSteer=32, maxSpeed=38;
 [SerializeField] Transform seat, exitPoint;
 Rigidbody body;
 float gas,steer,brake; bool occupied;
 public Transform Seat => seat;
 public Transform ExitPoint => exitPoint;
 public float SpeedKph => body ? body.linearVelocity.magnitude*3.6f : 0;
 void Awake(){body=GetComponent<Rigidbody>();if(centerOfMass)body.centerOfMass=transform.InverseTransformPoint(centerOfMass.position);body.collisionDetectionMode=CollisionDetectionMode.ContinuousDynamic;body.interpolation=RigidbodyInterpolation.Interpolate;}
 public void SetOccupied(bool value){occupied=value;gas=steer=brake=0;}
 public void SetGas(float value){gas=Mathf.Clamp(value,-1,1);}
 public void SetSteer(float value){steer=Mathf.Clamp(value,-1,1);}
 public void SetBrake(float value){brake=Mathf.Clamp01(value);}
 void OnApplicationFocus(bool focused){if(!focused){gas=steer=0;brake=1;}}
 void FixedUpdate(){
 float v=Vector3.Dot(body.linearVelocity,transform.forward);
 float a=occupied?gas:0,b=occupied?brake:1,s=occupied?steer:0;
 if(Mathf.Abs(v)>1&&Mathf.Sign(a)!=Mathf.Sign(v)&&Mathf.Abs(a)>.01f){b=Mathf.Max(b,Mathf.Abs(a));a=0;}
 if(Mathf.Abs(v)>=maxSpeed&&Mathf.Sign(v)==Mathf.Sign(a))a=0;
 int driven=0;foreach(var axle in axles)if(axle.driven)driven+=(axle.left?1:0)+(axle.right?1:0);
 foreach(var axle in axles){Apply(axle.left,axle.leftMesh,axle,a,b,s,v,driven);Apply(axle.right,axle.rightMesh,axle,a,b,s,v,driven);}
 }
 void Apply(WheelCollider wheel,Transform mesh,Axle axle,float a,float b,float s,float speed,int driven){
 if(!wheel)return;
 wheel.steerAngle=axle.steering?s*Mathf.Lerp(maxSteer,10,Mathf.Clamp01(Mathf.Abs(speed)/maxSpeed)):0;
 wheel.motorTorque=axle.driven&&b<.1f?a*engineTorque/Mathf.Max(1,driven):0;
 wheel.brakeTorque=b*brakeTorque;
 if(mesh){wheel.GetWorldPose(out Vector3 p,out Quaternion q);mesh.SetPositionAndRotation(p,q);}
 }
}
}