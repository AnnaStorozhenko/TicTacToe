using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SetupMissionOne : MonoBehaviour {
	
	public List<GameObject> flagPrefabs;
	public List<GameObject> flagPrefabsBackup;
	public List<GameObject> flagInstances;
	public List<GameObject> spawPoints;
	public List<GameObject> spawPointsBackup;
	public List<GameObject> activeSpawnPoints;
	const int n = 5;
	
	public MissionMgr missionManager;

	void SetupMissiom() {
		
		if (missionManager)
		{
		Mission m = missionManager.missions[0];
		m.activated = true;
		m.visible = true ;
	    m.displayName = "MissionOne";
		m.description = "collect the 5 randomly placed flags";
		m.tokens.Clear();

			for (int k = 0; k < n; k++)	{
				int index = Random.Range (0,flagPrefabs.Count);
				GameObject flagPrefab = flagPrefabs[index];
				flagPrefabs.RemoveAt(index);
		        
				Vector3 flagPos = activeSpawnPoints[k].transform.position;
				GameObject flagInstance = (GameObject) Instantiate (flagPrefab, flagPos, new Quaternion (0.0f,0.0f, 0.0f, 1.0f));
	        	m.tokens.Add(flagInstance.GetComponent<MissionToken>());
		    }
		}
		if (missionManager)
		{
			Mission m = missionManager.missions[1];
			m.activated = false;
			m.visible = false;
			m.status = Mission.missionStatus.MS_Acquired;
			m.displayName = "MissionTwo";
			m.description = "return the flags to the flagstand";
			
			m.tokens.AddRange (missionManager.missions[0].tokens);
		}
	}
		
}
