using UnityEngine;
using System.Collections;

//This script controls the player's ability to fire
public class FireControls : MonoBehaviour {

	public Transform muzzlePoint;		//the point to shoot from
	public Transform targetPoint;		//an object to indicate what was hit

	//during the interface loop
	public void OnGUI() {
		//draw a button in the lower right of the screen
		Rect fire = new Rect(Screen.width - 70, Screen.height - 220, 50, 50);
		if(GUI.Button(fire, "Fire")) {
			Fire();		//fire the cannon
		}
	}
	
	//fire the cannon
	public void Fire() {
		RaycastHit hit;		//this will hold the results of the raycast
		//if the shot hit something forward from the muzzle point
		if(Physics.Raycast(muzzlePoint.position, muzzlePoint.forward, out hit)) {
			//set the target point at the spot that was hit
			targetPoint.position = hit.point;
			//alert the object that it was hit
			hit.transform.root.gameObject.BroadcastMessage("Hit", hit.point, SendMessageOptions.DontRequireReceiver);
		}
		//otherwise set the shot indicator to an out of the way position
		else {
			targetPoint.position = Vector3.zero;
		}
	}
}
