using UnityEngine;
using System.Collections;

public class ClassicControls : MonoBehaviour {

	public CharacterController characterControl;
	public float moveSpeed = 10f;
	public float rotateSpeed = 45f;

	public Renderer rightTread;
	public Renderer leftTread;

	private float rightOffset = 0;
	private float leftOffset = 0;

	public void OnGUI()
	{
		Rect fore = new Rect(50, Screen.height - 150, 50, 50);
		if(GUI.RepeatButton(fore, "f"))
		{
			MoveTank(moveSpeed);
		}

		Rect back = new Rect(50, Screen.height - 50, 50, 50);
		if(GUI.RepeatButton(back, "b"))
		{
			MoveTank(-moveSpeed);
		}

		Rect left = new Rect(50, Screen.height - 100, 50, 50);
		if(GUI.RepeatButton(left, "l"))
		{
			RotateTank(-rotateSpeed);
		}

		Rect right = new Rect(100, Screen.height - 100, 50,50);
		if(GUI.RepeatButton(right, "r"))
		{
			RotateTank(rotateSpeed);
		}
	}

	public void MoveTank(float moveSpeed)
	{
		Vector3 move = characterControl.transform.forward * moveSpeed * Time.deltaTime;
		move.y -= 9.8f * Time.deltaTime;
		characterControl.Move(move);

		rightOffset += (moveSpeed * Time.deltaTime) / 2;
		rightTread.material.mainTextureOffset = new Vector2(rightOffset, 0);
		leftOffset += (moveSpeed * Time.deltaTime) / 2;
		leftTread.material.mainTextureOffset = new Vector2(leftOffset, 0);
	}

	public void RotateTank(float rotateSpeed)
	{
		Vector3 rotate = Vector3.up * rotateSpeed * Time.deltaTime;
		characterControl.transform.Rotate(rotate);

		if(rotateSpeed > 0)
		{
			rightOffset -= (moveSpeed * Time.deltaTime) / 5;
			rightTread.material.mainTextureOffset = new Vector2(rightOffset, 0);
			leftOffset += (moveSpeed * Time.deltaTime) / 5;
			leftTread.material.mainTextureOffset = new Vector2(leftOffset, 0);
		}
		else if(rotateSpeed < 0)
		{
			rightOffset += (moveSpeed * Time.deltaTime) / 5;
			rightTread.material.mainTextureOffset = new Vector2(rightOffset, 0);
			leftOffset -= (moveSpeed * Time.deltaTime) / 5;
			leftTread.material.mainTextureOffset = new Vector2(leftOffset, 0);
		}
	}

}
