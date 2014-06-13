using UnityEngine;
using System.Collections;

public class GameCam : MonoBehaviour {
	
	public GameObject trackObj;
	public GameObject lookObj;
	public float height;
	public float desiredDistance;
	public float rotDamp;
	public float heightDamp;
	public float kh;
	
	// Use this for initialization
	void Start () {
		// cache object references and initialize class variables here
	}
	
	public void LookUp() {
		GameObject go = GameObject.Find ("lookupTarget");
		if (go)
			lookObj = go;
	}
	
	public void LookPlayer() {
		if (trackObj)
			lookObj = trackObj;
	}
	
	void updateRotAndTrans() {
		if (trackObj)
		{
			float DesiredRotationAngle = trackObj.transform.eulerAngles.y;
			float DesiredHeight = trackObj.transform.position.y + height;
			
			float RotAngle = transform.eulerAngles.y;
			float Height = transform.position.y;

			RotAngle = Mathf.LerpAngle (RotAngle, DesiredRotationAngle, rotDamp);
			Height = Mathf.Lerp (Height, DesiredHeight, heightDamp * Time.deltaTime);

			Quaternion CurrentRotation = Quaternion.Euler (0.0f, RotAngle, 0.0f);	
			Vector3 desiredPos = trackObj.transform.position;
			desiredPos -= CurrentRotation * Vector3.forward * desiredDistance;
			desiredPos.y = Height;
			transform.position += kh * (desiredPos - transform.position);
			
	
			transform.LookAt (lookObj.transform.position);
		}
		else
			Debug.Log ("GameCam::Error, no trackObj reference in Inspector. Please add object to track");
	}
	
	void Update () {
		updateRotAndTrans();
	}
}