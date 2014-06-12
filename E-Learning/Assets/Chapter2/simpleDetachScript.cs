using UnityEngine;
using System.Collections;

public class simpleDetachScript : MonoBehaviour {
	
	public Vector3 newPos;

	// Use this for initialization
	void Start () {
		this.transform.parent = null;
		this.transform.position = newPos;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
