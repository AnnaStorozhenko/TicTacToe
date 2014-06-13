using UnityEngine;
using System.Collections;

public class MainMenuScript : MonoBehaviour {

	private GameMgr gm;
	private GameObject GameObj;

	// Use this for initialization
	void Start () {
		GameObj = GameObject.Find("Game");
		if (GameObj) {
			gm = GameObj.GetComponent<GameMgr>();
		}

	}

	 
	void OnMouseDown() {
		if (gm) {
		gm.SetState(GameMgr.eGameState.eGS_Level1);
		this.gameObject.SetActive(false);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
