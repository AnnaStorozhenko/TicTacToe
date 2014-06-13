using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MissionMgr : MonoBehaviour {
	
	public List<Mission> missions;
	public List<MissionToken> missionTokens = new List<MissionToken>();
	
	// Use this for initialization
	void Start () {

	}


	public void Add(MissionToken mt) {

		bool uniqueToken = true;
		if (missionTokens != null) {
			for (int i = 0; i < missionTokens.Count; i++) {
				if (missionTokens[i].id == mt.id) {
					uniqueToken = false;
					break;
				}
			}
		}
		if (uniqueToken) {
			missionTokens.Add (mt);
		}
	}


	public bool isMissionComplete(int missionid) {
		bool rval = false;
		if (missionid < missions.Count)	{
			if (missions[missionid].status == Mission.MissionStatus.MS_Completed)
				rval = true;
		}
		return rval;
	}


	public bool Validate(Mission m) {

		bool missionComplete = true;
		
		if (m.tokens.Count <= 0)
			missionComplete = false;
		
		for (int i = 0; i < m.tokens.Count; i++){
			bool tokenFound = false;
			for (int j = 0; j < missionTokens.Count; j++) {
				if (missionTokens[j] != null && (m.tokens[i].id == missionTokens[j].id)) {	
					tokenFound = true;
					break;
				}
			}
			
			if (tokenFound == false) {
				missionComplete = false;
				break;
			}
		}
		
		if (missionComplete == true) {
			GameObject go = GameObject.Find ("Player");
			if (go == null)
				go = GameObject.Find ("Player1");
			if (go)	{
				PlayerData pd = go.GetComponent<PlayerData>();
				if (pd)	{
					pd.AddScore (m.points);
				}
			}
		}
		return missionComplete;
	}


	void ValidateAll() {
		
		for (int i = 0; i < missions.Count; i++)
		{
			Mission m = missions[i];
			if (m.status == Mission.MissionStatus.MS_ForceComplete) {
				m.InvokeReward ();
				m.status = Mission.MissionStatus.MS_Invalid;
			}
			if ((m.status != Mission.MissionStatus.MS_Completed) && (m.status != Mission.MissionStatus.MS_Invalid)) {
				bool missionSuccess = Validate(m);
				
				if (missionSuccess == true)
				{
					m.InvokeReward();
				}
			}
		}
	}


	void Update () {
		ValidateAll ();
	}
}