using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum PasserType
{
	Walk, Run
}

public class Passer : MonoBehaviour {

	private GameObject player;
	private List<PasserState> statesList = new List<PasserState> ();
	public PasserType type;
	private bool isEnableType;

	void Awake ()
	{
		statesList.Add (GetComponent<PasserRunningState> ());
		statesList.Add (GetComponent<PasserWalkingState> ());

		player = GameObject.FindGameObjectWithTag ("Player");

		isEnableType = false;
	}

	void OnEnable ()
	{
		
	}

	void FixedUpdate ()
	{
		if (!isEnableType &&
			transform.position.z - player.transform.position.z < Config.TRACK_LENGTH / 2f)
		{
			GetComponent<Animator> ().enabled = true;

			isEnableType = true;

			switch (type)
			{
			case PasserType.Run:
				GetComponent<PasserRunningState> ().enabled = true;
				break;
			case PasserType.Walk:
				GetComponent<PasserWalkingState> ().enabled = true;
				break;
			}
		}
	}

	public void RandomPasserType ()
	{
		float x = transform.localPosition.x;

		int ran = Random.Range (0, 2);

		if (ran == 0)
		{
			type = PasserType.Walk;

			if (x == Config.TRACK_WIDTh)
			{
				transform.localRotation = Quaternion.Euler (0f, 90f, 0f);
			}
			else if (x == -Config.TRACK_WIDTh)
			{
				transform.localRotation = Quaternion.Euler (0f, 270f, 0f);
			}
			else
			{
				int dirRan = Random.Range (0, 2);
				if (dirRan == 0)
				{
					transform.localRotation = Quaternion.Euler (0f, 90f, 0f);
				}
				else
				{
					transform.localRotation = Quaternion.Euler (0f, 270f, 0f);
				}
			}
		}
		else
		{
			type = PasserType.Run;
		}
	}

	public Animator GetAnimator ()
	{
		return GetComponent<Animator> ();
	}
}