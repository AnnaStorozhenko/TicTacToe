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
	void Start ()
	{
	}


	public void LookUp()
	{
		GameObject go = GameObject.Find ("lookupTarget");
		if (go)
		lookObj = go;
	}


	public void LookPlayer()
	{
		if (trackObj)
		lookObj = trackObj;
	}


	void UpdateRotAndTrans()
	{
		if (trackObj) {
		}

		else {
			Debug.Log ("GameCam::Error, trackObj invalid");
		}
			float DesiredRotationAngle = trackObj.transform.eulerAngles.y;
			float DesiredHeight = trackObj.transform.position.y + height;
			
			float RotAngle = transform.eulerAngles.y;
			float Height = transform.position.y;

			RotAngle = Mathf.LerpAngle (RotAngle, DesiredRotationAngle, rotDamp);
			Height = Mathf.Lerp (Height, DesiredHeight, heightDamp * Time.deltaTime);

			Quaternion CurrentRotation = Quaternion.Euler(0.0f, RotAngle, 0.0f);	
			Vector3 pos = trackObj.transform.position;
			pos -= CurrentRotation * Vector3.forward * desiredDistance;
			pos.y = Height;
		    transform.position += kh * (pos - transform.position);

		    transform.LookAt(trackObj.transform.position);
	
	}


	void Update () {
		UpdateRotAndTrans();
	}
}
