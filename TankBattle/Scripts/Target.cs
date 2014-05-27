using UnityEngine;
using System.Collections;

public class Target : MonoBehaviour {

	public Animator animator;
	public float maxIdleTime = 10f;
	public float minIdleTime = 3f;

	private int timeId = -1;
	private int wasHitId = -1;
	private int inTheFrontId = -1;

	private int idleRetractId = -1;
	private int idleExtendId = -1;

	public void Awake()
	{
		timeId = Animator.StringToHash("time");
		wasHitId = Animator.StringToHash("wasHit");
		inTheFrontId = Animator.StringToHash("inTheFront");
		idleRetractId = Animator.StringToHash("inTheFront");
		idleExtendId = Animator.StringToHash("BaseLayer.ArmatureAction");
	}

	public void Update()
	{
		int currentStateId = animator.GetCurrentAnimatorStateInfo(0).nameHash;
		if(currentStateId == idleRetractId ||
		   currentStateId == idleExtendId)
		{
			SubtractTime();
		}
		else
		{
			if(animator.GetBool(wasHitId))
			{
				ClearHit();
				ResetTime();
			}
			if(animator.GetFloat(timeId) < 0)
			{
				ResetTime();
			}
		}
	}

	public void SubtractTime()
	{
		float currTime = animator.GetFloat(timeId);
		currTime -= Time.deltaTime;
		animator.SetFloat(timeId, currTime);
	}

	public void ClearHit()
	{
		animator.SetBool(wasHitId, false);
		animator.SetBool(inTheFrontId, false);
	}

	public void ResetTime()
	{
		float newTime = Random.Range(minIdleTime, maxIdleTime);
		animator.SetFloat(timeId, newTime);
	}

	public void Hit(Vector3 point)
	{
		int currentStateId = animator.GetCurrentAnimatorStateInfo(0).nameHash;
		if (currentStateId != idleExtendId)
			return;
		animator.SetBool(wasHitId, true);

		Vector3 localPoint = transform.InverseTransformPoint(point);
		if(localPoint.x > 0)
		{
			animator.SetBool(inTheFrontId, false);
			ScoreCounter.score += 5;
		}
		else
		{
			animator.SetBool(inTheFrontId, true);
			ScoreCounter.score += 10;
		}
	}
}