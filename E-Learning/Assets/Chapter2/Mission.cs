using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Mission {
	
	public enum missionStatus
	{
		MS_Invalid = -1,
		MS_Acquired = 0,
		MS_InProgress = 1,
		MS_Completed = 2
	};
	
	public missionStatus status;
	public string displayName;
	public string description;
	public List<MissionToken> tokens;	
	public int points;
	public GameObject reward;
	
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
	
	}
	
	public void InvokeReward()
	{
		if (reward != null)
			GameObject.Instantiate(reward);
		this.status = missionStatus.MS_Completed;
	}
}
