using UnityEngine;
using System.Collections;

public class PlayerControls : MonoBehaviour {

	public Vector3 moveDirection = Vector3.zero;
	public float rotateSpeed;
	public float speedSmoothing = 10.0f;
	public Vector3 initialPosition;
	public float gravity;
	public float maxSpeed;
	public Animator animator;
	public GameObject hat;
	public GameObject body;
    private float movespeed = 0.0f;
	private int idleState;
	private int runState;


	void Start() {
		this.transform.position = initialPosition;
		if(animator)
		{
			idleState = Animator.StringToHash("Base Layer.Idle");
			runState = Animator.StringToHash("Base Layer.Run");
		}
	}


	public void EnablePlayerRenderer(bool renderflag)
	{
		if (body)
		{
			body.GetComponent<SkinnedMeshRenderer>().enabled = renderflag;
		}
	}


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
		movespeed = Mathf.Lerp (movespeed, targetSpeed * maxSpeed, curSmooth);

		if(animator)
		{
			animator.SetFloat ("speed", movespeed);
		}

		Vector3 displacement = moveDirection * movespeed * Time.deltaTime;
		displacement.y = -gravity * Time.deltaTime;
		GetComponent<CharacterController>().Move(displacement);

		RaycastHit hitinfo;
		Ray r = new Ray(this.transform.position, -Vector3.up);
		Physics.Raycast( r, out hitinfo);
		this.transform.position = new Vector3(this.transform.position.x, hitinfo.point.y + (this.collider as CapsuleCollider).height, this.transform.position.z);

		transform.rotation = Quaternion.LookRotation (moveDirection);
	}
	

	void Update () {

		UpdateMovement();
	}
}
