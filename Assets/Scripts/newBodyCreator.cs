using UnityEngine;
using System.Collections;

public class newBodyCreator : MonoBehaviour
{
		public GameObject snakeTailObject;
		public snakeBodyController* bodyController; 
		void OnTriggerEnter (Collider col)
		{		
				if (col.gameObject.name == ("Apple")) {
						makenewbody ();
				}
		}
		void makenewbody ()
		{
				GameObject newBody = (GameObject)Instantiate (Resources.Load ("bodyCube"));
				// Set the new body's head object as the snake's tail object
				newBody.GetComponent ("bodySegController");
				// set the new body as the snake's new tail object
		}
}
