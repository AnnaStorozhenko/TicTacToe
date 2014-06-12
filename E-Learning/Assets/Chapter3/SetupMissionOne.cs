using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SetupMissionOne : MonoBehaviour {
	
	const int kNumFlagMounts = 5;
	public List<GameObject> flagPrefabs;
	private List<GameObject> flagPrefabsBackup;
	public List<GameObject> flagInstances;
	
	public List<GameObject> spawnPoints;
	private List<GameObject> spawnPointsBackup;
	public List<GameObject> activeSpawnPoints;
	
	public MissionMgr missionManager;
	private bool isInitialized = false;
	
	void Start () {
		// turn on the start popup
		GameObject g = GameObject.Find ("MainCamera");
		if (g)
			g.GetComponent<PopupMgr>().Level1Start.SetActive(true);
	}
	
	void SetupMission()
	{
		spawnPointsBackup = new List<GameObject>();
		flagPrefabsBackup = new List<GameObject>();
		
		for (int i = 0; i < spawnPoints.Count; i++)
			spawnPointsBackup.Add (spawnPoints[i]);
		
		for (int i = 0; i < flagPrefabs.Count; i++)
			flagPrefabsBackup.Add (flagPrefabs[i]);
		
		activeSpawnPoints = new List<GameObject>();
		for (int k = 0; k < kNumFlagMounts; k++)
		{
			int index = Random.Range (0, spawnPoints.Count);
			GameObject flagSpawnPoint = spawnPoints[index];
			spawnPoints.RemoveAt(index);
			activeSpawnPoints.Add (flagSpawnPoint);
		}
		
		if (missionManager)
		{
			Mission m = missionManager.missions[0];
			m.activated = true;
			m.visible = true;
			m.status = Mission.MissionStatus.MS_Acquired;
			m.displayName = "MissionOne";
			m.description = "collect the 5 randomly placed flags";
			m.tokens.Clear();
			
//			PlayerData pd = null;
//			GameObject p = GameObject.Find ("Player1");
//			if (p == null)
//				p = GameObject.Find ("Player");
//			if (p != null)
//			{
//				pd = p.GetComponent<PlayerData>();
//				if (pd.flagChoices.Count > 0)
//					pd.flagChoices.Clear();
//			}
			 
			flagInstances = new List<GameObject>();
			for (int k = 0; k < kNumFlagMounts; k++)
			{
				int index = Random.Range (0,flagPrefabs.Count);
				GameObject flagPrefab = flagPrefabs[index];
				flagPrefabs.RemoveAt (index);
				
				Vector3 flagPos = activeSpawnPoints[k].transform.position;
				flagPos.y += 2.0f;
				GameObject flagInstance = (GameObject)Instantiate (flagPrefab, flagPos, new Quaternion(0.0f, 0.0f, 0.0f, 1.0f));
				flagInstance.SetActive(true);
				
//				if (pd != null)
//					pd.flagChoices.Add (index);
				
				m.tokens.Add (flagInstance.GetComponent<MissionToken>());
			}
		}
		
		if (missionManager)
		{
			Mission m = missionManager.missions[1];
			m.activated = false;
			m.visible = false;
			m.status = Mission.MissionStatus.MS_Acquired;
			m.displayName = "MissionTwo";
			m.description = "return the flags to the flagstand";
			
			m.tokens.AddRange (missionManager.missions[0].tokens);
		}
	}
	
	
	public void SetupPlayer()
	{
		GameObject go = GameObject.Find ("Player1");
		if (go)
		{
			go.GetComponent<PlayerControls>().EnablePlayerRenderer(true);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
		if (isInitialized == false)
		{
			GameObject go = GameObject.Find ("Game");
			if (go)
			{
				missionManager = go.GetComponent<MissionMgr>();
				if (missionManager)
				{
					SetupMission ();
					SetupPlayer();
					isInitialized = true;
				}
			}
		}
	}
}
