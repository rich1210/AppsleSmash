using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class bodySegController : MonoBehaviour
{

		ArrayList nextTurns = new ArrayList ();
		public Transform myTransform;
		public GameObject *frontObject;
		public GameObject *rearObject;
		public float myX, myY, myZ;
		// Use this for initialization
		void Start ()
		{
				myX = myTransform.position.x;
				myY = myTransform.position.y;
				myZ = myTransform.position.z;
		}
	
		// Update is called once per frame
		void Update ()
		{
				myX = myTransform.position.x;
				myY = myTransform.position.y;
				myZ = myTransform.position.z;
				// if you don't turn
				if (!lookForTurn ()) {
						// move forwards
						moveForwards ();
				}
		}
		bool lookForTurn ()
		{
				// if on grid 
				if (isOnGridPoint ()) {
						myZ = Mathf.Round (myZ);
						myX = Mathf.Round (myX);
						/*
						// if grid point is next turn point
						if (nextTurns [0].x == myX && nextTurns [0].z == myZ) {
								// if turn direction is left
								if (nextTurns [0].direction == 'l') {
										// turn left
										turnLeft ();
								}
								// if turn direction is right
								else if (nextTurns [0].direction = 'r') {
										// turn right
										turnRight ();
								}
								//remove top of next turns
								
								// return true
								return true;
						}
				}*/
			
				}
				return false;
		
		}
		void moveForwards ()
		{
		
		}
		void turnRight ()
		{
			
			
		}
		void turnLeft ()
		{
		
		
		}
		void setSpeed ()
		{
		
		}
		void setFront (GameObject newFront)
		{
				frontObject = newFront;
		}
		void setBack (GameObject newBack)
		{
				rearObject = newBack;
		}
		bool isOnGridPoint ()
		{
				if ((myX % 1 > .97 || myX % 1 < .03) && (myZ % 1 > .97 || myZ % 1 < .03)) {
						return true;
				}
				return false;
		}
}
