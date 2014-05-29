using UnityEngine;
using System.Collections;

public class TargetIndicator : MonoBehaviour {
	
	public Transform target;		
	public float range = 25;		
		

	public void SetTarget(Transform newTarget) {
		target = newTarget;		
	}
	

	public void LateUpdate() {
		if(target == null) {
			Destroy(gameObject);
			return;
		}
		float distance = Vector3.Distance(transform.position, target.position);
		float yScale = Mathf.Clamp01((range - distance) / range);
		transform.localScale = new Vector3(1, yScale, 1);
		Vector3 lookAt = target.position;
		lookAt.y = transform.position.y;
		transform.LookAt(lookAt);
	}
}
