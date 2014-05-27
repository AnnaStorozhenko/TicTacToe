using UnityEngine;
using System.Collections;

//This script controls the behavior of the targets
public class Target : MonoBehaviour {

	public Animator animator;		//the Animator component
	
	public float maxIdleTime = 10f;		//the maximum amount of time for the target to be idle
	public float minIdleTime = 3f;		//the minimum amount of time for the target to be idle
	
	private int timeId = -1;			//the id of the time parameter
	private int wasHitId = -1;			//the id of the wasHit parameter
	private int inTheFrontId = -1;		//the id of the inTheFront parameter
	
	private int idleRetractId = -1;		//the id of the Idle_Retract state
	private int idleExtendId = -1;		//the id of the Idle_Extend state
	
	
	//initialize at the beginning of the level
	public void Awake() {
		//find the ids of the parameters
		timeId = Animator.StringToHash("time");
		wasHitId = Animator.StringToHash("wasHit");
		inTheFrontId = Animator.StringToHash("inTheFront");
		
		//find the ids of the states
		idleRetractId = Animator.StringToHash("Base Layer.Idle_Retract");
		idleExtendId = Animator.StringToHash("Base Layer.Idle_Extend");
	}
	
	//during the main game loop
	public void Update() {
		//find the id of the current state
		int currentStateId = animator.GetCurrentAnimatorStateInfo(0).nameHash;
		//if the target is currently idle
		if(currentStateId == idleRetractId || currentStateId == idleExtendId) {
			SubtractTime();		//update the time parameter
		}
		else {
			//if the target was hit
			if(animator.GetBool(wasHitId)) {
				ClearHit();		//reset the hit parameters
				ResetTime();	//reset the time
			}
			
			//if the time has dropped below zero
			if(animator.GetFloat(timeId) < 0) {
				ResetTime();	//reset the time
			}
		}
	}
	
	//reduce the time parameter
	public void SubtractTime() {
		//grab the current time parameter value
		float curTime = animator.GetFloat(timeId);
		//reduce the current time by the length of the frame
		curTime -= Time.deltaTime;
		//apply the new time
		animator.SetFloat(timeId, curTime);
	}
	
	//reset the hit parameters by setting them to false
	public void ClearHit() {
		animator.SetBool(wasHitId, false);
		animator.SetBool(inTheFrontId, false);
	}
	
	//reset the time parameter
	public void ResetTime() {
		//find a new random amount of time to idle
		float newTime = Random.Range(minIdleTime, maxIdleTime);
		//apply the new time value
		animator.SetFloat(timeId, newTime);
	}
	
	//respond to the call that the target has been hit
	public void Hit(Vector3 point) {
		//grab the current state id
		int currentStateId = animator.GetCurrentAnimatorStateInfo(0).nameHash;
		//if the target is not extended, exit the function
		if(currentStateId != idleExtendId) return;
		//tell the animator that the target was hit
		animator.SetBool(wasHitId, true);
		
		//convert the hit point to local space
		Vector3 localPoint = transform.InverseTransformPoint(point);
		//if it was on the positive x, it was from behind the target
		if(localPoint.x > 0) {
			animator.SetBool(inTheFrontId, false);		//tell the animator that it was from behind
			ScoreCounter.score += 5;		//give the player some score
		}
		//otherwise the shot hit in the front
		else {
			animator.SetBool(inTheFrontId, true);		//tell the animator that it was from the front
			ScoreCounter.score += 10;		//give the player some score
		}
	}
}
