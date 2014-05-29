using UnityEngine;
using System.Collections;

public class IndicatorControl : MonoBehaviour {
	
	public GameObject indicatorPrefab;
	private static IndicatorControl control;	
	
	public static void CreateSlice(Transform target) {
		if(control == null) {
			control = Object.FindObjectOfType(typeof(IndicatorControl)) as IndicatorControl;
		}
		if(control != null) {
			control.NewSlice(target);
		}
	}

	public void NewSlice(Transform target) {
		GameObject slice = Instantiate(indicatorPrefab) as GameObject;
		slice.transform.parent = transform;
		slice.transform.localPosition = Vector3.zero;
		slice.SendMessage("SetTarget", target);
	}
}
