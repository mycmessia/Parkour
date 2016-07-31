using UnityEngine;
using System.Collections;

public class Config {

	public class Camera
	{		
		public const float PLAYER_MOVE_FOLLOW_PERSENT = 16f;
		public const float PLAYER_FLY_FOLLOW_PERSENT = 5f;

		public const float Z_PREPARE_PLAYER_OFFSET = -1f;				// 准备阶段，即玩家在舞台上的时候
		public const float Z_PLAYER_OFFSET = -1.8f;						// old 1.2
		public const float Z_FLY_PLAYER_OFFSET = -1f;					// 飞起来再把摄像机往后拉 -1f 米

		public const float Y_PREPARE_PLAYER_OFFSET = 0.6f;
		public const float Y_PLAYER_OFFSET = 1.2f;						// old 1.4

		public const float X_PREPARE_ROTATE = 10f;
		public const float X_ROTATE = 10f;								// old 25

		public const float Y_DOWN_OFFSET = 0.6f;						// 摄像机经过隧道的下压的距离
		public const float X_DOWN_ROTATE = 20f;							// 摄像机经过隧道的下压掉的角度
	}

	public static float TRACK_WIDTh = 1f;
	public static int TRACK_COUNT = 4;
	public static int TRACK_LENGTH = 32;
	public static float TRACK_SPEED = 4f;

	public class Player
	{
		public static float JUMP_SPEED = 6f;

		public static int TURN_LR_FRAMES = 20;
	}

	public static float GRAVITY = 16f;
}
