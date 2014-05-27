using UnityEngine;
using System.Collections;

public class TurretControls : MonoBehaviour {

	public Transform turretPivot;		
	public Transform cannonPivot;		

	public float turretSpeed = 45f;		
	public float cannonSpeed = 20f;		

	public float lowCannonLimit = 315f;	
	public float highCannonLimit = 359.9f;
	

	public void OnGUI() {

		Rect up = new Rect(Screen.width - 100, Screen.height - 150, 50, 50);
		if(GUI.RepeatButton(up, "u")) {
			RotateCannon(cannonSpeed);		
		}
		Rect down = new Rect(Screen.width - 100, Screen.height - 50, 50, 50);
		if(GUI.RepeatButton(down, "d")) {
			RotateCannon(-cannonSpeed);
		}
		Rect left = new Rect(Screen.width - 150, Screen.height - 100, 50, 50);
		if(GUI.RepeatButton(left, "l")) {
			RotateTurret(-turretSpeed);	
		}
		Rect right = new Rect(Screen.width - 50, Screen.height - 100, 50, 50);
		if(GUI.RepeatButton(right, "r")) {
			RotateTurret(turretSpeed);		
		}
	}
	

   public void RotateTurret(float speed) {
		Vector3 rotate = Vector3.up * speed * Time.deltaTime;
		turretPivot.Rotate(rotate);
	}
	

	public void RotateCannon(float speed) {

		float rotate = speed * Time.deltaTime;
		Vector3 euler = cannonPivot.localEulerAngles;
		euler.x = Mathf.Clamp(euler.x - rotate, lowCannonLimit, highCannonLimit);
		//euler.y = Mathf.Clamp(euler.y - rotate, lowCannonLimit, highCannonLimit);
		cannonPivot.localEulerAngles = euler;

	}
}
