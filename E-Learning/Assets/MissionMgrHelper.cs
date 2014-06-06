using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class MissionMgrHelper : MonoBehaviour {

	public string MissionName;
	public bool setActivated;
	public bool setVisible;
	private MissionMgr missionMgr;

	// Use this for initialization
	void Start () {
		missionMgr = GameObject.Find("Game").GetComponent<MissionMgr>();
	}
	
	// Update is called once per frame
	void Update () {
		for(int i=0; i<missionMgr.missions.Count; i++) {
			Mission m =  missionMgr.missions[i];
			if(m.displayName == MissionName) {
				m.activated = setActivated;
				m.visible = setVisible;
			}
		}
	}
}
