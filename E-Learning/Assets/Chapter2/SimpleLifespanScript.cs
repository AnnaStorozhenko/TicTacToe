using UnityEngine;
using System.Collections;

public class SimpleLifespanScript : MonoBehaviour {
	
	public float seconds;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		seconds -= Time.deltaTime;
		if (seconds <= 0.0f)
			GameObject.Destroy (this.gameObject);
	}
}
