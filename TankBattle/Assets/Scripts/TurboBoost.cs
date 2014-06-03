using UnityEngine;
using System.Collections;

public class TurboBoost : MonoBehaviour {
	public CharacterController controller;		
	public float boostSpeed = 25f;		
	public float boostLength = 1f;		
	public float startTime = -1f;		
	
	

	public void OnGUI() {

		Rect turboRect = new Rect(10, Screen.height - 220, 65, 65);
		if(GUI.Button(turboRect, "Turbo"))
			StartBoost();		
	}
	

	public void StartBoost() {
		if(startTime < 0)	
			startTime = Time.time;	
	}
	

	public void Update() {
		if(startTime < 0) return;		
		if(controller == null) return;	
		
	
		controller.Move(controller.transform.forward * boostSpeed * Time.deltaTime);
	
		if(Time.time - startTime < 0.5f)

			Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 100, (Time.time - startTime) * 2);

		else if(Time.time - startTime > boostLength - 0.5f)

			Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 40, (Time.time - startTime - boostLength + 0.5f) * 2);
	
		if(Time.time > startTime + boostLength)
			startTime = -1;		
	}






}
