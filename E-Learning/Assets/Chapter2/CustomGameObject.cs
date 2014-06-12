using UnityEngine;
using System.Collections;

public class CustomGameObject : MonoBehaviour {
	
	public string displayName;
	public GameObject popUpInfo;
	public enum CustomObjectType
	{
		Invalid = 0,
		Coin = 1,
		Ruby = 2,
		Emerald = 3,
		Diamond = 4,
		flag = 5
	};
	
	public CustomObjectType objectType;
	
	public void validate()
	{
		if (displayName == "")
			displayName = "unnamed_object";
	}
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

