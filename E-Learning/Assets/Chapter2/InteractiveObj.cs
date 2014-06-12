using UnityEngine;
using System.Collections;

public class InteractiveObj : MonoBehaviour {

	public Vector3 rotAxis;
	public float rotSpeed;
	public bool billboard;
	
	private CustomGameObject gameObjectInfo;
	public ObjectInteraction OnCloseEnough;
	private bool activated = true;
	
	void Start ()
	{
		gameObjectInfo = this.gameObject.GetComponent<CustomGameObject>();
		if (gameObjectInfo)
			gameObjectInfo.validate();
	}
	
	// Update is called once per frame
	void Update() {
		
		if (billboard == true)
		{
			GameObject player = GameObject.Find ("Player1");
			if (player != null)
			{
				this.transform.LookAt (player.transform.position);
				this.transform.localEulerAngles = new Vector3(0.0f, this.transform.localEulerAngles.y, 0.0f);
			}
		}
		else
			transform.Rotate (rotAxis, rotSpeed * Time.deltaTime);
	}	
	
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			if (activated == true)
			{
				if (OnCloseEnough != null)
					OnCloseEnough.HandleInteraction();
				activated = false;
			}
		}
	}
}
