using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public enum UserInput
{
	None, Left, Up, Right, Down
}

public class InputController : MonoBehaviour {

	public static InputController instance = null;

	Queue<UserInput> inputsQueue = new Queue<UserInput> ();

	bool isTouchStart = false;
	Vector2 startTouchPos = Vector2.zero;
	float minTouchMoveDis = 16F;

	Text inputText;

	Player player;

	void Awake ()
	{
		instance = this;
	}

	void Start ()
	{
		inputText = GameObject.Find ("InputText").GetComponent<Text> ();
		player = GameObject.Find ("Player").GetComponent<Player> ();

	}

	public void SetInput (UserInput ui)
	{
		inputsQueue.Enqueue (ui);
	}

	public UserInput GetInput ()
	{
		if (inputsQueue.Count > 0)
		{
			return inputsQueue.Dequeue ();
		}

		return UserInput.None;
	}

	void GetTouchInput ()
	{
		if (Input.touchCount <= 0)
			return;

		if (Input.GetTouch (0).phase == TouchPhase.Began && isTouchStart == false)
		{
			isTouchStart = true;
			startTouchPos = Input.GetTouch (0).position;
		}

		if (Input.GetTouch (0).phase == TouchPhase.Moved && isTouchStart == true)
		{
			Vector2 endTouchPos = Input.GetTouch (0).position;
			float xMoveDis = Mathf.Abs (endTouchPos.x - startTouchPos.x);
			float yMoveDis = Mathf.Abs (endTouchPos.y - startTouchPos.y);

			if (xMoveDis > minTouchMoveDis || yMoveDis > minTouchMoveDis)
			{
				// move left or right
				if (xMoveDis > yMoveDis * 1.1f)
				{
					isTouchStart =false;

					if (endTouchPos.x - startTouchPos.x > 0)
					{
						// move right
						inputsQueue.Enqueue (UserInput.Right);
						inputText.text = "Right";
					}
					else
					{
						// move left
						inputsQueue.Enqueue (UserInput.Left);
						inputText.text = "Left";
					}
				}
				else	// move up or down
				{
					if (endTouchPos.y - startTouchPos.y > 0)
					{
						// move up
						inputsQueue.Enqueue (UserInput.Up);
						inputText.text = "Up";
					}
					else
					{
						// move down
						inputsQueue.Enqueue (UserInput.Down);
						inputText.text = "Down";
					}
				}
			}
		}
	}

	void GetKeyInput ()
	{
		if (Input.GetKeyUp (KeyCode.UpArrow))
		{
			inputsQueue.Enqueue (UserInput.Up);
		}
		else if (Input.GetKeyUp (KeyCode.LeftArrow))
		{
			inputsQueue.Enqueue (UserInput.Left);
		}
		else if (Input.GetKeyUp (KeyCode.RightArrow))
		{
			inputsQueue.Enqueue (UserInput.Right);
		}
		else if (Input.GetKeyUp (KeyCode.DownArrow))
		{
			inputsQueue.Enqueue (UserInput.Down);
		}
	}

	// Update is called once per frame
	void Update () 
	{
		if (player.GetCurState () == States.Preparing || player.GetCurState () == States.TurningAround)
		{
			return;
		}

		GetTouchInput ();
		GetKeyInput ();
	}
}