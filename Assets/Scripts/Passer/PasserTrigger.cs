using UnityEngine;
using System.Collections;

public class PasserTrigger : MonoBehaviour {

	void OnTriggerEnter (Collider other)
	{
//		Debug.Log (other.gameObject.tag);
		if (other.gameObject.tag == "Player" && other.GetComponent<Player> ().GetCurState () != States.Jumping)
		{
			other.GetComponent<Player> ().ChangeState (States.Die);
		}
	}
}
