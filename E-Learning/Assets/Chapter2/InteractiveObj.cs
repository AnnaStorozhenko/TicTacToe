using UnityEngine;
using System.Collections;

public class InteractiveObj : MonoBehaviour {
	
	public Vector3 rotAxis;
	public float rotSpeed;
	private CustomGameObject gameObjectInfo;
	public ObjectInteraction OnCloseEnough;

	void Start ()
	{
		gameObjectInfo = this.gameObject.GetComponent<CustomGameObject>();
		if (gameObjectInfo)
			gameObjectInfo.validate();
	}
	
	// Update is called once per frame
	void Update() {
	    transform.Rotate (rotAxis, rotSpeed * Time.deltaTime);
	}	

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			if (OnCloseEnough != null)
				OnCloseEnough.HandleInteraction();
		}
	}
}
