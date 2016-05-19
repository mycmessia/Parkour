using UnityEngine;
using System.Collections;

public class TrackMove : MonoBehaviour {

	private Transform track;
	int passedPlayerCount;

	void Awake ()
	{
		passedPlayerCount = 0;

		track = transform.parent.parent;
	}

	void OnTriggerExit (Collider col)
	{
		if (col.CompareTag ("Player")) 
		{
			passedPlayerCount++;

			if (passedPlayerCount == 2) 
			{
				track.position = new Vector3 (
					track.position.x, track.position.y, 
					track.position.z + Config.TRACK_COUNT * Config.TRACK_LENGTH
				);
				passedPlayerCount = 0;
			}
		}
	}
}
