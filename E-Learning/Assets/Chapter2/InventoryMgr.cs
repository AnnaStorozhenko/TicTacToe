using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class InventoryMgr : MonoBehaviour {

	public List<InventoryItem> inventoryObjects = new List<InventoryItem>();
	public int numCells;
	public float height;
	public float width;
	public float yPosition;
	
	private MissionMgr missionMgr;
	
	// Use this for initialization
	void Start () {
		
		GameObject go = GameObject.Find ("Game");
		if (go)
			missionMgr = go.GetComponent<MissionMgr>();
	}
	
	void insert(InteractiveObj iObj)
	{
		ObjectInteraction oi = iObj.OnCloseEnough;

		InventoryItem ii = new InventoryItem();
		ii.item = iObj.gameObject;
		ii.quantity = 1;
		ii.displayTexture = oi.tex;
		ii.item.SetActive (false);
		inventoryObjects.Add (ii);
		
		MissionToken mt = ii.item.GetComponent<MissionToken>();
		if (mt != null)
			missionMgr.Add(mt);
		
		Instantiate (ii.item.GetComponent<CustomGameObject>().popUpInfo, Vector3.zero, Quaternion.identity);
	}
	
	public void Add(InteractiveObj iObj)
	{
		ObjectInteraction oi = iObj.OnCloseEnough;

		switch(oi.interactionType)
		{
		case(ObjectInteraction.InteractionType.Unique):
		{
			insert(iObj);
		}
			break;
			
		case(ObjectInteraction.InteractionType.Accumulate):
		{
			bool inserted = false;
			
			CustomGameObject cgo = iObj.gameObject.GetComponent<CustomGameObject>();
			CustomGameObject.CustomObjectType ot = CustomGameObject.CustomObjectType.Invalid;
			if (cgo != null)
				ot = cgo.objectType;
			
			for (int i = 0; i < inventoryObjects.Count; i++)
			{
				//todo
				CustomGameObject cgoi = inventoryObjects[i].item.GetComponent<CustomGameObject>();
				CustomGameObject.CustomObjectType io = CustomGameObject.CustomObjectType.Invalid;
				if (cgoi != null)
					io = cgoi.objectType;
				
				if (ot == io)
				{
					inventoryObjects[i].quantity++;
					MissionToken mt = iObj.gameObject.GetComponent<MissionToken>();
					if (mt != null)
						missionMgr.Add(mt);
					
					iObj.gameObject.SetActive (false);
					inserted = true;
					break;
				}
			}
			

			if (!inserted)
				insert(iObj);
		}
			break;
		}
	}
	
	void DisplayInventory()
	{
		Texture t = null;
		
		float sw = Screen.width;
		float sh = Screen.height;
		
		int totalCellsToDisplay = inventoryObjects.Count;
		for (int i = 0; i < totalCellsToDisplay; i++)
		{
			InventoryItem ii = inventoryObjects[i];
			t = ii.displayTexture;
			int quantity = ii.quantity;
			
			float totalCellLength = sw - (numCells*width);
			Rect r = new Rect(totalCellLength - 0.5f*(totalCellLength) + (width*i), yPosition*sh, width, height);
			if (GUI.Button(r, t))
			{
				if (ii.popup == null)
				{
					ii.popup = (GameObject)Instantiate (ii.item.GetComponent<CustomGameObject>().popUpInfo, Vector3.zero, Quaternion.identity);
				}
				else
				{
					Destroy(ii.popup);
					ii.popup = null;
				}
			}
			
			Rect r2 = new Rect(totalCellLength - 0.5f*(totalCellLength) + (width*i), yPosition*sh, 0.5f*width, 0.5f*height);
			string s = quantity.ToString();
			GUI.Label(r2, s);
		}
	}
	
	void OnGUI()
	{
		DisplayInventory ();
	}
	
	void Update () {
		
	}
}
