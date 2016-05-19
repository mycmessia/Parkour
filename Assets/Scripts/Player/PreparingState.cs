using UnityEngine;
using System.Collections;

public class PreparingState : PlayerState {

	private Player player;

	void Awake ()
	{
		player = GetComponent<Player> ();
	}

	void OnEnable ()
	{
		StartCoroutine ("StartRun");
	}

	void OnDisable ()
	{
		StopAllCoroutines ();
	}

	IEnumerator StartRun ()
	{
		yield return new WaitForSeconds (3F);

		player.ChangeState (States.TurningAround);
	}
}
