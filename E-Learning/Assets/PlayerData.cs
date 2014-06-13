using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerData : MonoBehaviour {
	
	public int score;
    public GameMgr.eGameState level;
	public List<int> flagChoices; 

	public void AddScore(int dScore) {
		score += dScore;
	}
	
	public int GetScore() {
		return score;
	}

	public void StoreProgress(GameMgr.eGameState lvl) {
		level = lvl;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
