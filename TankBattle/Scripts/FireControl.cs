using UnityEngine;
using System.Collections;

public class FireControl : MonoBehaviour {

	public Transform muzzlePoint;
	public Transform targetPoint;

	public void OnGUI()
	{
		Rect fire = new Rect(Screen.width - 70, Screen.height - 220, 50, 50);
	    if(GUI.Button(fire, "Fire"))
		{
			Fire();
		}
	}

	public void Fire()
	{
		RaycastHit hit;
		if(Physics.Raycast(muzzlePoint.position, muzzlePoint.forward , out hit))
		{
			targetPoint.position = hit.point;
			hit.transform.root.gameObject.SendMessage("Hit", hit.point, SendMessageOptions.DontRequireReceiver);
		}
		else
		{
			targetPoint.position = Vector3.zero;
		}
	}
}
