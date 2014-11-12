using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour
{

		//GameObject otherObj;
		public float defaultSpeed;
		public float speed;
		public int length = 1;
		public float MAX_TOLERANCE = .03f,
				MIN_TOLERANCE = .97f;
		public float t1, t2;		
		public Texture2D leftIcon;
		public Texture2D rightIcon;		
		//public GameObject Apple; 
		//public GameObject lastObject;
		//public GameObject bodyCube; 
			
		public int rotRate = 2;
		
		int counterRot = 0;
		public snakeBodyController snakeBodyController;
	
		public bool leftTurn = false,
				rightTurn = false,
				turn = false,
				hitWall = false,
				hitApple = false,
				hitSnake = false;
	
		public int direction = 0;
		public float hoverHieght = .3f;
	
	
		Vector3 pos; 
		void Start ()
		{
				defaultSpeed = 4;
				rightTurn = true;
				
				snakeBodyController = GetComponentInParent<snakeBodyController> ();
				speed = defaultSpeed;
				adjustMotion ();
		}
	
		
		// Update is called once per frame
		void Update ()
		{
// SHOULD I BE TURNING?	
				adjustMotion ();				
		
				// Get PLAYER position
				pos = transform.position;
				// Determine if PLAYER wants to turn, and TURN is off
				if (canTurn () && !turn && (leftTurn || rightTurn)) {
						// Determin if Player can turn
						if (canTurn ()) {
								// PLAYER wants to turn, and is in the right position to begin turning
								turn = true;
						} 
				} 
		// OTHERWISE, IF PLAYER DOES NOT WANT TO TURN, SET TURN TO FALSE
		else if (!(leftTurn || rightTurn)) {
						turn = false;
				}
// TURNING
				// DETERMINE IF PLAYER WANTS TO TURN
				if (leftTurn && turn) {
						// increment the rotation counter
						counterRot++;
						// rotate right
						transform.Rotate (0, -rotRate, 0);
						// check turn ending condition
						if (endTurnCheck ()) {
								leftTurn = false;
						}
			
				} else if (rightTurn && turn) {
						// increment the rotation counter
						counterRot++;
						// rotate right
						transform.Rotate (0, rotRate, 0);
						// check turn ending condition
						if (endTurnCheck ()) {
								rightTurn = false;
						}
				}
// NOT TURNING? -----> MOVING!!!! 
				else {
						transform.Translate (Vector3.forward * Time.deltaTime * speed);
				}
		}
// YOU DIED? RESET POSITION AND FLAGS!!!!!!!!
		void reset ()
		{
				snakeBodyController.reset ();
				// move back to initial position
				moveTo (12, hoverHieght, 5);
				// set the speed to 1
				speed = defaultSpeed;
				// set the length to 0
				length = 0;
				// reset bools
				hitWall = false;
				hitSnake = false;
				hitApple = false;
				turn = false;
				leftTurn = false;
				rightTurn = false;
		
		
		}
// MORE OF A JUMP - TO FUNCTION, USED FOR RESET FUNCTION
		void moveTo (float x, float y, float z)
		{
				// this function rotates the player to look at the north wall (all in one line!!!!)
				transform.rotation = Quaternion.LookRotation (Vector3.forward);
				// adjust in case you're not on an integer
				adjustMotion ();
				// get current position
				Vector3 pos = transform.position;
				// get xoffset
				float xOffSet = x - pos.x;
				// get yOffSet
				float yOffSet = y - pos.y;
				//get zoffset
				float zOffSet = z - pos.z;
				Debug.Log ("TRANSLATING POSITION BY " + xOffSet + " " + yOffSet + " " + zOffSet);
				transform.Translate (xOffSet, yOffSet, zOffSet);
				adjustMotion ();
				
		}
// DETECTS ENTRY COLLISIONS (BEGINNING OF COLLISION) WITH TRIGGER OBJECTS (WALLS, APPLES, THE PLAYER)
		void OnTriggerEnter (Collider col)
		{
				Vector3 pos = transform.position;
				Debug.Log ("Collision Detected Name = " + col.gameObject.name);
				if (col.gameObject.name.Contains ("Wall")) {
						Debug.Log ("WALL Collision Detected GAME OVER");
						Debug.Log ("Collision Occurred At " + pos.x + " " + pos.y + " " + pos.z);
			
						hitWall = true;
						speed = 0;
				} else if (col.gameObject.name.Contains ("Apple")) {
						Debug.Log ("Apple Collision Detected");
						hitApple = true;
						length++;
				
				} else if (col.gameObject.name.Contains ("Body") && !col.gameObject.name.Contains ("#0")) {
						Debug.Log ("Player Collision Detected GAME OVER");
						hitSnake = true;
						speed = 0;
			
				}
		}
// DETECTS EXIT COLLISIONS (NO LONGER COLLIDING) WITH TRIGGER OBJECTS (WALLS, APPLES, THE PLAYER)
		void OnTriggerExit (Collider col)
		{
				if (col.gameObject.name == ("Apple")) {
						//Debug.Log ("Apple Collision Exited");
						hitApple = false;
				}
		}	
// RETURNS TRUE IF THE PLAYER IS ON AN INTEGER POSITION IN THE X AND Z (USED FOR TURNING)
		bool canTurn ()
		{
				float tempx, tempz;
				pos = transform.position;
				// get the non-integer remainder of the x and z positions				
				tempx = pos.x % 1.0f;
				tempz = pos.z % 1.0f;
				// if they are within tolerances	
				if (tempx < MAX_TOLERANCE || tempx > MIN_TOLERANCE) {
						if (tempz < MAX_TOLERANCE || tempz > MIN_TOLERANCE) {
								// return true
								return true;
						}
						// otherwise return false
						return false;
				}
				// otherwise return false
				return false;
		}
	
// THIS FUNCTION RETURNS A FLOAT INDICATING THE TRANSLATION REQUIRED TO MOVE AN OBJECT TO AN INTEGER POSITION
// A CHARACTER IS PASSED TO GET THE X,Y, OR Z OFFSET
		float getTranslation (char letter)
		{
				// Declare Variables
				float ret;
				// get the current position
				pos = transform.position;
				// if getting x translation
				if (letter == 'x') {
						ret = pos.x;
						ret = Mathf.Round (ret);
						ret = ret - pos.x;
			
				} else if (letter == 'y') {
						ret = hoverHieght - pos.y;
				}
				// otherwise getting z translation
				else {
						ret = pos.z;
						ret = Mathf.Round (ret);
						ret = ret - pos.z;
				}
				// return the result
				return ret;
		
		}
// this function checks to see if the player has completed a turn, and returns true if so
// 		it also resets the rotation counter and turn booleans and adjusts the player position to put it on the integer grid
		bool endTurnCheck ()
		{
				// if the turn is complete
				if (counterRot >= (90 / rotRate)) {
						//Debug.Log (" TURN COMPLETE!!!!!!!! Position = x: " + pos.x + " y: " + pos.y + " z: " + pos.z);
			
						// reset the rotation counter
						counterRot = 0;
			
						// adjust motion so that you're on an integer grid
						adjustMotion ();
			
						pos = transform.position;
						//Debug.Log (" MOTION ADJUSTED!!!!!!!! Position = x: " + pos.x + " y: " + pos.y + " z: " + pos.z);
			
						// change player direction variable
						if (rightTurn == true) {
								changePlayerDirection ('r');
						} else if (leftTurn == true) {
								changePlayerDirection ('l');
						}
			
						// reset all turn booleans to false
						turn = leftTurn = rightTurn = false;
			
						// return true to tell main the turn is complete
						return true;
				}
				// return false, the turn is not complete
				return false;
		}
// THIS FUNCTION KEEPS TRACK OF THE PLAYERS DIRECTION VARIABLE
		void changePlayerDirection (char letter)
		{
				if (letter == 'r') {
						if (direction == 3) {
								direction = 0;
						} else {
								direction++;
						}
				} else if (letter == 'l') {
						if (direction == 0) {
								direction = 3;
						} else {
								direction--;
						}			
				}
		
		}
// THIS FUNCTION MOVES THE PLAYER TO AN INTEGER POSITION IN THE DIRECTION THEY ARE TRAVELING
// ex: moving parralel to X axis, function will move player to nearest Y integer position
		void adjustMotion ()
		{
				float variation;
				float verticalVariation = getTranslation ('y');
				bool horizontal = false;
				// if even, horizontal motion (z changes, x constant)
				if (direction % 2 == 0) {
						variation = getTranslation ('x');
						horizontal = true;
				}
		// if odd, vertical motion (x changes, z constant)
		else {
						variation = getTranslation ('z');
				}
				// if outside tolerance
				if (Mathf.Abs (variation) > MAX_TOLERANCE) {
						// adjust the translation
						if (horizontal) {
								transform.Translate (variation, 0, 0);
						} else {
								transform.Translate (0, 0, variation);
						}
						// always corrent vertical variation
						transform.Translate (0, verticalVariation, 0);
			
				}
		}
// GUI WITH TURNING AND SPEED BOOST BUTTONS, AND SIMPLE GAME OVER + RESET
		void OnGUI ()
		{		
				float screenWUnit = Screen.width / 50;
				float screenHUnit = Screen.height / 50;
				// IF GAME IS OVER, Display message
				if (speed == 0 && hitSnake) {
						GUI.Box (new Rect (screenWUnit * 15, screenHUnit * 10, screenWUnit * 20, screenHUnit * 15), "<b><size=15><color=black> YOU HIT YOURSELF - GAME OVER</color></size></b>");
						if (GUI.Button (new Rect (screenWUnit * 18, screenHUnit * 14, screenWUnit * 14, screenHUnit * 10), "<size=15>TAP HERE TO RESET</size>")) {
								reset ();
						
						}
					
				} else if (speed == 0 && hitWall) {
						GUI.Box (new Rect (screenWUnit * 15, screenHUnit * 10, screenWUnit * 20, screenHUnit * 15), "<b><size=15><color=black> YOU HIT A WALL - GAME OVER</color></size></b>");
						if (GUI.Button (new Rect (screenWUnit * 18, screenHUnit * 14, screenWUnit * 14, screenHUnit * 10), "<size=15>TAP HERE TO RESET</size>")) {
								reset ();
						
						}
			   
				} 
				// Display Pause Menu if speed is 0, but haven't hit a wall or player
				else if (speed == 0 && !hitWall && !hitSnake) {
						GUI.Box (new Rect (screenWUnit * 15, screenHUnit * 10, screenWUnit * 20, screenHUnit * 30), "<b><size=15><color=black> PAUSED </color></size></b>");
						if (GUI.Button (new Rect (screenWUnit * 18, screenHUnit * 14, screenWUnit * 14, screenHUnit * 10), "<size=15>TAP HERE TO RESET</size>")) {
								reset ();
				
						} else if (GUI.Button (new Rect (screenWUnit * 18, screenHUnit * 28, screenWUnit * 14, screenHUnit * 10), "<size=15>TAP HERE TO CONTINUE</size>")) {
								speed = 2;
							
						}
				} 
				// OTHERWISE - Normal Game operation, ALL THE BUTTONS!!!!!!!!!!!!!!!!!!!!!!!!
				else {
						// PAUSE BUTTON
						if (GUI.Button (new Rect (Screen.width / 2 - Screen.width / 16, screenHUnit * 48, Screen.width / 8, Screen.height / 8), "Pause")) {
								speed = 0;
						}
					// TURN LEFT!!!!!!!!!
					else if (GUI.Button (new Rect (0, 0, Screen.width / 8, Screen.height / 8), leftIcon)) {
								// if not currently turning
								if (turn == false) {
										// set turn left to true
										leftTurn = true;
										// set right to false
										rightTurn = false;
								}
						}
					// TURN RIGHT!!!!!!
					else if (GUI.Button (new Rect (Screen.width - Screen.width / 8, 0, Screen.width / 8, Screen.height / 8), rightIcon)) {
								if (turn == false) {
										// set right to true
										rightTurn = true;
										// set left to false if its true
										leftTurn = false;
								}
						}
					// REPEAT BUTTON FOR CONTINUAL BOOSTING
					//else if ((GUI.RepeatButton (new Rect (0, screenHUnit * 40, screenWUnit * 10, screenHUnit * 15), "SPEED BOOST")) ||
					//			(GUI.RepeatButton (new Rect (screenWUnit * 43, screenHUnit * 40, screenWUnit * 10, screenHUnit * 15), "SPEED BOOST")
					//
					//		) && !hitWall) {
					//			speed = 5;
			///
					//	} 
					// IF DEAD, SPEED SHOULD BE 0
					else if (hitWall || hitSnake) {
								speed = 0;
						} 
					// RESET THE SPEED IF NOT PRESSING BUTTON & NOT DEAD
					else {
								speed = defaultSpeed;
						}	
				}
		}
}