using UnityEngine;
using System.Collections;


public class ObjectInteraction : MonoBehaviour {
	
	public enum InteractionAction
	{
		Invalid = -1,
		PutInInventory = 0,
		Use = 1,
		AddMissionToken = 2,
		Instantiate = 3,
		CameraLookUp = 4,
		CameraLookPlayer = 5
	};
	public enum InteractionType
	{
		Invalid = -1,
		Unique = 1,
		Accumulate = 2
	};
	
	public InteractionAction interaction;
	public InteractionType interactionType;
	public GameObject prefab;
	public Texture tex;
	
	void Start () {
		
	}
	
	void handleUse()
	{
	}
	
	public void HandleInteraction()
	{
		InventoryMgr iMgr = null;
		GameObject player = GameObject.Find ("Player");
		if (player == null)
			player = GameObject.Find ("Player1");
		if (player)
			iMgr = player.GetComponent<InventoryMgr>();
		
		if (interaction == InteractionAction.Use)
			this.handleUse ();
		
		else if (interaction == InteractionAction.PutInInventory)
		{
			if (iMgr)
				iMgr.Add(this.gameObject.GetComponent<InteractiveObj>());
		}
		else if (interaction == InteractionAction.Instantiate)
		{
			GameObject go = (GameObject)Instantiate (prefab, Vector3.zero, Quaternion.identity);
//			QuizNpcHelper q = this.gameObject.GetComponent<QuizNpcHelper>();
//			if (q != null)
//			{
//				if (q.prefabReference != null)
//					go.GetComponent<PopupPanel>().SetCorrectButtonPopup(q.prefabReference);
//			}
		}
		else if (interaction == InteractionAction.CameraLookUp)
		{
			Camera.mainCamera.gameObject.GetComponent<GameCam>().LookUp();
		}
		else if (interaction == InteractionAction.CameraLookPlayer)
		{
			Camera.mainCamera.gameObject.GetComponent<GameCam>().LookPlayer();
		}
		else if (interaction == InteractionAction.AddMissionToken)
		{
			GameObject mm = GameObject.Find ("Game");
			if (mm)
			{
				MissionMgr mmgr = mm.GetComponent<MissionMgr>();
				if (mmgr)
				{
					mmgr.Add (this.GetComponent<MissionToken>());
				}
			}
		}
	}
	
	void Update () {
		
	}
}

