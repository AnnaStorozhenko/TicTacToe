using UnityEngine;
using System.Collections;

public class GameMgr : MonoBehaviour {
	
	public enum eGameState {
		eGS_Invalid = -1,
		eGS_MainMenu = 0,
		eGS_Level1 = 1,
		eGS_Level2 = 2,
		eGS_Level3 = 3
	};
	
	public eGameState gameState;
	private eGameState _prevGameState;

	// Use this for initialization
	void Start () {
		gameState = eGameState.eGS_MainMenu;
		_prevGameState = eGameState.eGS_MainMenu;
	}
	
	public void SetState(eGameState gs)	{
		gameState = gs;
	}

	public void ChangeState(eGameState gs)	{
		gameState = gs;
		Destroy (GameObject.Find ("_level1"));
		Destroy (GameObject.Find ("_level2"));
		Destroy (GameObject.Find ("_level3"));

		switch(gameState) {

			case(eGameState.eGS_MainMenu):
			{
				break;
			}
			case(eGameState.eGS_Level1):
			{
				Application.LoadLevelAdditive ("LEVEL_1");
				break;
			}
			case(eGameState.eGS_Level2):
			{
				Application.LoadLevelAdditive ("LEVEL_2");
				break;
			}
			case(eGameState.eGS_Level3):
			{
				Application.LoadLevelAdditive ("LEVEL_3");
				break;
			}
		}
	}

	// Update is called once per frame
	void Update () {

		if (gameState != _prevGameState) {
			ChangeState(gameState);
		}
		_prevGameState  = gameState;
	}
}
