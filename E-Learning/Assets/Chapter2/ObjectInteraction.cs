using UnityEngine;
using System.Collections;

public class ObjectInteraction : MonoBehaviour {

	public enum InteractionAction
	{
		Invalid = -1,
		PutInInventory = 0,
		Use = 1,
	};
	public enum InteractionType
	{
		Invalid = -1,
		Unique = 0,
		Accumulate = 1
	};

	public InteractionAction interaction;
	public InteractionType interactionType;
	public Texture tex;

	void Start () {
	
	}

	public void HandleInteraction()
	{
		InventoryMgr iMgr = null;
     	GameObject player = GameObject.Find ("Player");
		if (player == null)
			player = GameObject.Find ("Player1");
	
		if (player)
		iMgr = player.GetComponent<InventoryMgr>();

		if (interaction == InteractionAction.PutInInventory)
		{
			if (iMgr)
			iMgr.Add(this.gameObject.GetComponent<InteractiveObj>());
		}

	}

	// Update is called once per frame
	void Update () {
	
	}
}
