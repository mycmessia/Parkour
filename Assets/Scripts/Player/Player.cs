using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum States {
	Preparing,
	TurningAround,
	Running,
	TurningLeft,
	TurningRight,
	Jumping,
	Sliding
};

public class Player : MonoBehaviour {

	public States m_currentState;

	private List<PlayerState> statesList = new List<PlayerState> ();
	private CharacterController controller;
    private Transform model;

    public Vector3 moveVector = Vector3.zero;
//	public float runSpeed;
	public float jumpSpeed;

	void Awake ()
	{
		statesList.Add (GetComponent<PreparingState> ());
		statesList.Add (GetComponent<TurningAroundState> ());
		statesList.Add (GetComponent<RunningState> ());
		statesList.Add (GetComponent<TurningLeftState> ());
		statesList.Add (GetComponent<TurningRightState> ());
		statesList.Add (GetComponent<JumpingState> ());
		statesList.Add (GetComponent<SlidingState> ());

		controller = GetComponent<CharacterController> ();
        model = transform.FindChild ("Model");

		jumpSpeed = 0;
	}

	public States GetCurState ()
	{
		return m_currentState;
	}

	public void ChangeState (States state)
	{
		DisableAllStates ();

		m_currentState = state;

		switch (state) 
		{
			case States.Preparing:
				GetComponent<PreparingState> ().enabled = true;
				break;
			case States.TurningAround:
				GetComponent<TurningAroundState> ().enabled = true;
				break;
			case States.Running:
				GetComponent<RunningState> ().enabled = true;
				break;
			case States.TurningLeft:
				GetComponent<TurningLeftState> ().enabled = true;
				break;
			case States.TurningRight:
				GetComponent<TurningRightState> ().enabled = true;
				break;
			case States.Jumping:
				GetComponent<JumpingState> ().enabled = true;
				break;
			case States.Sliding:
				GetComponent<SlidingState> ().enabled = true;
				break;
		}
	}

	void DisableAllStates ()
	{
		for (int i = 0; i < statesList.Count; i++) 
		{
			statesList [i].enabled = false;		
		}
	}

	void Update ()
	{
        if (model.localPosition.y > 0f)
        {
			jumpSpeed -= Config.GRAVITY * Time.deltaTime;
			moveVector.y = 0f;
        }
		else if (!controller.isGrounded)
        {
            moveVector.y -= 1.5f * Time.deltaTime;
        }
	}

	public void Move ()
	{
		controller.Move (moveVector);

		model.localPosition += new Vector3 (0f, jumpSpeed * Time.deltaTime, 0f);

        if (model.localPosition.y <= 0f)
        {
            model.localPosition = Vector3.zero;
			jumpSpeed = 0F;
        }

        if (controller.isGrounded)
        {
            moveVector.y = 0f;
        }
	}

	public Animator GetAnimator ()
	{
		return GetComponent<Animator> ();
	}
}
