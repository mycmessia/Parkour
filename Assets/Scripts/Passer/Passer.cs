using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Passer : MonoBehaviour {

	List<PasserState> statesList = new List<PasserState> ();

	void Awake ()
	{
		statesList.Add (GetComponent<PasserRunningState> ());
		statesList.Add (GetComponent<PasserWalkingState> ());
	}

	public Animator GetAnimator ()
	{
		return GetComponent<Animator> ();
	}
}