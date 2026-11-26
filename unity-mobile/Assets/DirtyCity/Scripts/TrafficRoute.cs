using UnityEngine;
namespace DirtyCity{
[RequireComponent(typeof(Rigidbody))]
public sealed class TrafficRoute:MonoBehaviour{
 [SerializeField] Transform[] waypoints;[SerializeField] LayerMask obstacles;[SerializeField] float maxSpeed=10,stopDistance=3,turnRate=70;
 Rigidbody body;int next;float speed;
 void Awake(){body=GetComponent<Rigidbody>();body.isKinematic=true;body.interpolation=RigidbodyInterpolation.Interpolate;body.collisionDetectionMode=CollisionDetectionMode.ContinuousSpeculative;}
 void FixedUpdate(){if(waypoints==null||waypoints.Length<2||!waypoints[next])return;float dt=Time.fixedDeltaTime;Vector3 target=waypoints[next].position-body.position;target.y=0;if(target.sqrMagnitude<1){next=(next+1)%waypoints.Length;return;}Vector3 dir=target.normalized;Vector3 origin=body.position+Vector3.up*.65f+body.rotation*Vector3.forward*1.9f;float look=stopDistance+speed*.65f;bool blocked=Physics.BoxCast(origin,new Vector3(.9f,.5f,.2f),body.rotation*Vector3.forward,out RaycastHit hit,body.rotation,look,obstacles,QueryTriggerInteraction.Ignore);speed=Mathf.MoveTowards(speed,blocked?0:maxSpeed,dt*(blocked?12:3));Quaternion turn=Quaternion.RotateTowards(body.rotation,Quaternion.LookRotation(dir),turnRate*dt);Vector3 delta=turn*Vector3.forward*(speed*dt);if(!Physics.BoxCast(body.position+Vector3.up*.65f,new Vector3(.95f,.55f,2.15f),delta.normalized,out _,body.rotation,delta.magnitude+.06f,obstacles,QueryTriggerInteraction.Ignore)&&delta.sqrMagnitude>0){body.MoveRotation(turn);body.MovePosition(body.position+delta);}}
}
}