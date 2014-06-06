using UnityEngine;
using System.Collections;

public class PlayerControls : MonoBehaviour {

	public Vector3 moveDirection = Vector3.zero;
	public float rotateSpeed;
	private float movespeed = 0.0f;
	public float speedSmoothing = 10.0f;
	private int idleState;
	private int runState;

	void UpdateMovement()
	{
		Vector3 cameraForward = Camera.main.transform.TransformDirection(Vector3.forward);
		cameraForward.y = 0.0f;
		cameraForward.Normalize();
		Vector3 cameraRight = new Vector3(cameraForward.z, 0.0f, -cameraForward.x);
		float v = Input.GetAxisRaw("Vertical");
		float h = Input.GetAxisRaw("Horizontal");
		Vector3 targetDirection = h * cameraRight + v * cameraForward;

		if (targetDirection != Vector3.zero) {
			moveDirection = Vector3.RotateTowards (moveDirection, targetDirection, rotateSpeed * Mathf.Deg2Rad * Time.deltaTime, 1000);
			moveDirection = moveDirection.normalized;
		}

		float curSmooth = speedSmoothing * Time.deltaTime;
		float targetSpeed = Mathf.Min(targetDirection.magnitude, 1.0f);
		movespeed = Mathf.Lerp (movespeed, targetSpeed, curSmooth);

		Vector3 displacement = moveDirection * movespeed * Time.deltaTime;
		GetComponent<CharacterController>().Move(displacement);
		transform.rotation = Quaternion.LookRotation (moveDirection);
	}
	
	// Update is called once per frame
	void Update () {

		UpdateMovement();
	}
}
