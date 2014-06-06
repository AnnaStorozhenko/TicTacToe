using UnityEngine;
using System.Collections;

public class InventoryPlaceOnMonument : MonoBehaviour {

	public int objectIndex;
	private InventoryMgr _inventoryMgr;
	private GameObject _monument;
	private bool attached;

	// Use this for initialization
	void Start () {
		_inventoryMgr = GameObject.Find ("Player").GetComponent<InventoryMgr>();
		_monument = GameObject.Find("Monument");
		attached = false;
	}

	// Update is called once per frame
	void Update () {
		GameObject go = _inventoryMgr.inventoryObjects[objectIndex].item;
		if ((_monument) && (attached == false))
		{
			_monument.GetComponent<MonumentMgr>().attachObjToMountPoint(go, objectIndex);
			attached = true;
		}

	}
}
