using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public float zOffset;
	public float zPreOffset;

	public float yOffset;
	public float yPreOffset;

	public float xRotate;
	public float xPreRotate;

	private Vector3 cameraOffset;
	private Quaternion cameraQuaternion;

	public Transform target;
	public Vector3 targetPos;
	public float lerpRate;

	void Awake ()
	{
		zOffset = Config.Camera.Z_PLAYER_OFFSET;
		yOffset = Config.Camera.Y_PLAYER_OFFSET;
		xRotate = Config.Camera.X_ROTATE;

		zPreOffset = Config.Camera.Z_PREPARE_PLAYER_OFFSET;
		yPreOffset = Config.Camera.Y_PREPARE_PLAYER_OFFSET;
		xPreRotate = Config.Camera.X_PREPARE_ROTATE;

		targetPos = Vector3.zero;

		lerpRate = 12f;
	}

	void OnEnable ()
	{
		cameraOffset = new Vector3 (0, yPreOffset, zPreOffset);
		cameraQuaternion = Quaternion.Euler (new Vector3 (xPreRotate, 0f, 0f));
	}

	void FixedUpdate () 
	{
		if (target != null) 
		{
			targetPos = target.position + cameraOffset;
		}

		transform.position = Vector3.Lerp (transform.position, targetPos, lerpRate * Time.deltaTime);
		transform.localRotation = Quaternion.Lerp (transform.localRotation, cameraQuaternion, lerpRate * Time.fixedDeltaTime);
	}

	public void SetTarget (Transform tar)
	{
		target = tar;
	}

	IEnumerator Prepare2Run ()
	{
		yield return new WaitForSeconds (0.2f);

		int p2rFrames = 60;
		int framesCounter = p2rFrames;
		float cameraOffsetY = Config.Camera.Y_PREPARE_PLAYER_OFFSET;
		float cameraOffsetZ = Config.Camera.Z_PREPARE_PLAYER_OFFSET;
		float cameraXRotate = Config.Camera.X_PREPARE_ROTATE;

		while (framesCounter > 0f)
		{
			cameraOffsetZ += (Config.Camera.Z_PLAYER_OFFSET - Config.Camera.Z_PREPARE_PLAYER_OFFSET) / p2rFrames;
			cameraOffset.z = cameraOffsetZ;

			cameraOffsetY += (Config.Camera.Y_PLAYER_OFFSET - Config.Camera.Y_PREPARE_PLAYER_OFFSET) / p2rFrames;
			cameraOffset.y = cameraOffsetY;

			cameraXRotate += (Config.Camera.X_ROTATE - Config.Camera.X_PREPARE_ROTATE) / p2rFrames;
			cameraQuaternion = Quaternion.Euler (new Vector3 (cameraXRotate, 0f, 0f));

			framesCounter--;
			yield return new WaitForFixedUpdate ();
		}
	}

	public void StartPrepare2Run ()
	{
//		Debug.Log ("StartPrepare2Run");

		StartCoroutine ("Prepare2Run");
	}
}
