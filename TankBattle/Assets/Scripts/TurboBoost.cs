using UnityEngine;
using System.Collections;

public class TurboBoost : MonoBehaviour {
	public CharacterController controller;		//the tank's CharacterController
	public float boostSpeed = 20f;		//how fast is the boost
	public float boostLength = 0.5f;		//how long does the boost last
	public float startTime = -1f;		//when did the boost start
	
	
	//during the interface loop
	public void OnGUI() {
		//draw a button towards the bottom left
		Rect turboRect = new Rect(10, Screen.height - 220, 65, 65);
		if(GUI.Button(turboRect, "Turbo"))
			StartBoost();		//start boosting
	}
	
	//start the boost ability
	public void StartBoost() {
		if(startTime < 0)		//if the player is not boosting
			startTime = Time.time;		//set the time that the boost started
	}
	
	//during the normal frame loop
	public void Update() {
		if(startTime < 0) return;		//if the start time is less then zero, the player is not boosting
		if(controller == null) return;	//if there is no access to the CharacterController, the tank can't be moved
		
		//move the tank forward by the boost speed
		controller.Move(controller.transform.forward * boostSpeed * Time.deltaTime);
		
		//if it is at the beginning of the boost
		if(Time.time - startTime < 0.5f)
			//move towards a larger field of view
			Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 100, (Time.time - startTime) * 2);
		//else if it is at the end of the boost
		else if(Time.time - startTime > boostLength - 0.5f)
			//move towards a smaller field of view
			Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 50, (Time.time - startTime - boostLength + 0.5f) * 2);
		
		//if the boost has ended
		if(Time.time > startTime + boostLength)
			startTime = -1;		//less than zero means the player is not boosting
	}







//	public CharacterController controller;
//	public float boostSpeed = 50f;
//	public float boostLength = 1f;
//	public float startTime = -1f;
//
//
//	public void OnGUI()
//	{
//		Rect turboRect = new Rect(10, Screen.height - 220, 75, 75);
//		if(GUI.Button(turboRect, "Turbo"))
//			StartBoost();
//	}
//
//	public void StartBoost()
//	{
//		if(startTime < 0)
//		{
//			startTime = Time.time;
//		}
//
//	}
//
//	public void Update()
//	{
//		if(startTime < 0) return;
//		if(controller == null) return;
//
//		controller.Move(controller.transform.forward * boostSpeed * Time.deltaTime);
//		if(Time.time - startTime < 0.5f)
//		
//			Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 130, (Time.time - startTime * 2));
//
//		else if(Time.time - startTime > boostLength - 0.5f)
//		Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 60, (Time.time - startTime - boostLength + 0.5f) * 2f);
//		
//		if(Time.time > startTime + boostLength)
//		{
//			startTime = -1;
//		}
//	}
}
