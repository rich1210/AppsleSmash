using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class snakeBodyType
{
		public Vector3 targetPosition;
		public float speed;
		public bool isTurning;
		public GameObject myCube;
		public snakeBodyType (GameObject myNewCube, Vector3 newTarget, bool isTurn, float newSpeed)
		{
				myCube = myNewCube;
				speed = newSpeed;
				setNewTarget (newTarget, isTurn);
		}
		public void move ()
		{
				float step = speed * Time.deltaTime;
				Vector3 targetDir = targetPosition - myCube.transform.position;
				Vector3 newDir = Vector3.RotateTowards (myCube.transform.forward, targetDir, step * 5, 0.0F);			
				myCube.transform.rotation = Quaternion.LookRotation (newDir);
				myCube.transform.position = Vector3.MoveTowards (myCube.transform.position, targetPosition, step);

	
		}
		public void setNewTarget (Vector3 newTarget, bool isTurn)
		{
				targetPosition = newTarget;
				isTurning = isTurn;
		}
		public void adjustMotion ()
		{			
				// if target is along x axis
				if (myCube.transform.rotation.y == 0 || myCube.transform.rotation.y == 180) {
						Debug.Log ("X Adjusting");
						float zVariation = Mathf.Round (myCube.transform.position.z) - myCube.transform.position.z;
						myCube.transform.Translate (0, 0, zVariation);
				}
				// if target is along z axis
				else if (myCube.transform.rotation.y == 90 || myCube.transform.rotation.y == 270) {
						Debug.Log ("z Adjusting");
			
						float xVariation = Mathf.Round (myCube.transform.position.x) - myCube.transform.position.x;
						myCube.transform.Translate (xVariation, 0, 0);
				}
		}
};
public class snakeBodyController : MonoBehaviour
{
		public GameObject headCube;
		public Transform playerTransform;
		bool locked = false;
		
		public PlayerMovement PlayerMovementScript;
		
		// Array of Body Segments forming the Body of the Snake	
		private List<snakeBodyType> bodySegments = new List<snakeBodyType> ();	
		// update function
		void Start ()
		{
				// get the movement script
				PlayerMovementScript = GetComponentInParent<PlayerMovement> ();
				growSnake ();
		}
		void Update ()
		{
				// if on the grid position	
				if (onGrid ()) {
						Debug.Log ("ON GRID ! UPDATING POSITIONS");	
						// update all the segmetn positions
						updatePositions ();
				}
				// move the segments
				for (int i =0; i < bodySegments.Count; i++) {
						bodySegments [i].speed = PlayerMovementScript.speed;
						bodySegments [i].adjustMotion ();
						bodySegments [i].move ();
				}
		}
		// Detect Collision
		void OnTriggerEnter (Collider col)
		{
				// if Collision with Apple
				if (col.gameObject.name == ("Apple")) {
						// grow snake proportional to current length
						if (bodySegments.Count == 0) {
								growSnake ();
						} else {
								for (int i = 0; i < bodySegments.Count+1; i++) {
										growSnake ();
								}
						}

				}
		}
		public void reset ()
		{
				while (bodySegments.Count > 0) {
						Destroy (bodySegments [0].myCube);
						bodySegments.RemoveAt (0);
				}
		
		}
		bool onGrid ()
		{
				//Debug.Log ("On Grid Check Function");
				float xVariation = Mathf.Abs (playerTransform.position.x - Mathf.Round (playerTransform.position.x));
				float zVariation = Mathf.Abs (playerTransform.position.z - Mathf.Round (playerTransform.position.z));
				bool insideTolerance = (xVariation < .04 && zVariation < .04);
				// if not inside tolerance, turn of the lock, return false
				if (!insideTolerance) {
						locked = false;
						//Debug.Log ("not in tolerance");
						return false;
				}
			// if inside tolerace but not locked, NEW GRID POS! -> lock it and return true
			else if (insideTolerance && !locked) {
						locked = true;
						//Debug.Log ("On new Grid!");
						return true;
				}
			// if inside tolerance but already locked, do nothing and return false...(called within a turn cycle)
			else if (insideTolerance && locked) {
						//Debug.Log ("Multiple Call");
						return false;
				} else {
						return false;
				}
		}
		void updatePositions ()
		{		
				if (bodySegments.Count > 0) {
						// set the first segment data to the player data		
						bodySegments [0].setNewTarget (playerTransform.position, PlayerMovementScript.turn);
					
						// loop through all but the first
						for (int i = 1; i < bodySegments.Count; i++) {
								// give the ith segment a new target : the position of the cube in front of it
								bodySegments [i].setNewTarget (bodySegments [i - 1].myCube.transform.position, bodySegments [i - 1].isTurning);	
								
						}
				}
		}

		void growSnake ()
		{
				// if first cube - get the position and rotation from the player transform
				if (bodySegments.Count == 0) {
				
						GameObject newCube = Instantiate (headCube, playerTransform.position, playerTransform.rotation) as GameObject;
						bool isTurn = PlayerMovementScript.turn;
						float newSpeed = PlayerMovementScript.speed;
						newCube.name = ("Body Segment #" + bodySegments.Count);
						bodySegments.Add (new snakeBodyType (newCube, playerTransform.position, isTurn, newSpeed));
						bodySegments [0].setNewTarget (playerTransform.position, isTurn);
			
				}				
			// else if not first cube - get the position and rotation from the last cube in the snake
			else {
						snakeBodyType lastCube = bodySegments [bodySegments.Count - 1];
						// CREATE NEW Game Object	
						GameObject newCube = Instantiate (headCube, lastCube.myCube.transform.position, lastCube.myCube.transform.rotation) as GameObject;
						bool isTurn = lastCube.isTurning;
						float newSpeed = lastCube.speed;
						// Give it a new name
						newCube.name = ("Body Segment #" + bodySegments.Count);
						// CREATE NEW BODY SEGMENT WITH THE NEW CUBE
						bodySegments.Add (new snakeBodyType (newCube, lastCube.myCube.transform.position, isTurn, newSpeed));
						bodySegments [bodySegments.Count - 1].setNewTarget (lastCube.myCube.transform.position, isTurn);
			
				}
				updatePositions ();
		}
		void OnGUI ()
		{		
				float screenWUnit = Screen.width / 50;
				float screenHUnit = Screen.height / 50;
				// IF GAME IS OVER, Display message
				if (GUI.Button (new Rect (screenWUnit * 15, screenHUnit * 0, screenWUnit * 10, screenHUnit * 5), "<size=15>TAP HERE TO ADD MORE BODIES</size>")) {
						growSnake ();
				
				}
			
		} 
		
}
	