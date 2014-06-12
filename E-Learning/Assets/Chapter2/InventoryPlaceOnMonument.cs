using UnityEngine;
using System.Collections;
	
public class InventoryPlaceOnMonument : MonoBehaviour {

	public int objectIndex;
	private InventoryMgr InventoryMgr;
	private GameObject monument;
	private bool attached;
		
	void Start () {
		InventoryMgr = GameObject.Find ("Player").GetComponent<InventoryMgr>();
		monument = GameObject.Find("Monument");
		attached = false;
		}
		
	void Update () {
		GameObject go = InventoryMgr.inventoryObjects[objectIndex].item;
		if ((monument) && (attached == false))
		{
			monument.GetComponent<MonumentMgr>().attachObjToMountPoint(go, objectIndex);
			attached = true;
		}
   }
}
