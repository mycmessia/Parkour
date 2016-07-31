using UnityEngine;
using System.Collections;

public class PreparingState : PlayerState {

	private Player player;
	private CameraFollow cameraFollow;

	void Awake ()
	{
		player = GetComponent<Player> ();
		cameraFollow = Camera.main.GetComponent <CameraFollow> ();
	}

	void OnEnable ()
	{
		StartCoroutine ("StartRun");
	}

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Space)) 
		{
			cameraFollow.StartPrepare2Run ();

			player.ChangeState (States.TurningAround);
		}
	}

	void OnDisable ()
	{
		StopAllCoroutines ();
	}

	IEnumerator StartRun ()
	{
		yield return new WaitForSeconds (3F);

		cameraFollow.StartPrepare2Run ();

		player.ChangeState (States.TurningAround);
	}
}
