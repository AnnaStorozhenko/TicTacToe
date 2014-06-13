using UnityEngine;
using System.Collections;

public class scoreScript : MonoBehaviour {
	
	private PlayerData pd;
	
	// Use this for initialization
	void Start () {
		GameObject go = GameObject.Find ("Player");
		if (go == null)
			go = GameObject.Find ("Player1");
		
		if (go)	{
			pd = go.GetComponent<PlayerData>();
		}
	}
	
	// Update is called once per frame
	void OnGUI() {
		
		if (pd)	{
			int score = pd.GetScore();
			this.gameObject.GetComponent<GUIText>().text = score.ToString ();
		}
		
	}
}
