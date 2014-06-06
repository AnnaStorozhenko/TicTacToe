using UnityEngine;
using System.Collections;

public class InventoryPlaceOnMonument : MonoBehaviour {

	public int objectIndex;
	private InventoryMgr inventoryMgr;
	private GameObject monument;
	private bool attached;

	void Start () {
		inventoryMgr = GameObject.Find ("Player").GetComponent<InventoryMgr>();
		monument = GameObject.Find("Monument");
		attached = false;
	}

	void Update () {
		GameObject go = inventoryMgr.inventoryObjects[objectIndex].item;
		go.SetActive(true);

		if ((monument) && (attached == false))
		{
			monument.GetComponent<MonumentMgr>().AttachObjToMountPoint(go, objectIndex);
			attached = true;
		}

	}
}
