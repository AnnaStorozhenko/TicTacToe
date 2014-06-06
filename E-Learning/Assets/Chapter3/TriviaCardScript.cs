using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class TriviaCardScript : MonoBehaviour {

	public float texWidth;
	public float texHeight;
	private float width;
	private float height;
	private GUITexture t;
	
	// Use this for initialization
	void Start () {
		width = Screen.width;
		height = Screen.height;
		t = GetComponent<GUITexture>();
		t.pixelInset = new Rect((width/2.0f) - (texWidth/2.0f), (height/2.0f) - (texHeight/2.0f), texWidth, texHeight);
		
	}

//	Rect r = new Rect(Screen.width / 4.0f , Screen.height/4.0f, Screen.width/2, Screen.height/2); 
	//Texture t = new Texture();

}
