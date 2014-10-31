using UnityEngine;
using System.Collections;

public class bodyController : MonoBehaviour
{

		public GameObject player;
		public Transform headPosition;
		public float headDirection;
		public Transform myPosition;
		
		int nextTurnX = -1;
		int nextTurnZ = -1;
		char nextTurn = 'n';
		public float x, z, yRotation;
		float speed;
		int counter;
		int index;
		// Use this for initialization
		void Start ()
		{
				//speed = float(player.GetComponent ("PlayerMovement").GetComponent ("Speed"));
				//index = player.GetComponent ("PlayerMovement").GetComponent ("Length"));
				// get the initial position
				x = headPosition.position.x;
				z = headPosition.position.z;
				// Rotate the appropriate angle
				yRotation = headPosition.rotation.y;
				myPosition.Rotate (0, yRotation, 0);
				// move the body to the right position
				moveTo (x, z);
		}
	
		// Update is called once per frame
		void Update ()
		{
				float tempx = myPosition.position.x % 1.0f;
				float tempz = myPosition.position.z % 1.0f;
		
				if ((tempx > 0.98f || tempx < 0.02f) && (tempz > 0.98f || tempz < 0.02f)) {
						if (nextTurnX == (int)x && nextTurnZ == (int)z) {
								if (nextTurn == 'l') {
										turnLeft ();
								} else if (nextTurn == 'r') {
										turnRight ();
								} else {
										moveForward ();
								}
						}
				}
		}
		void moveForward ()
		{
				myPosition.Translate (Vector3.forward * Time.deltaTime);
		}
		void turnLeft ()
		{
				myPosition.Rotate (0, -90, 0);
		}
		void turnRight ()
		{
				myPosition.Rotate (0, 90, 0);
		}
		void moveTo (float x, float y)
		{
				x = x - myPosition.position.x;
				z = z - myPosition.position.z;
				myPosition.Translate (new Vector3 (x, 0, z));
		
		}
}
