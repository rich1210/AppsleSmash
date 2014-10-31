using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class snakeBodyController : MonoBehaviour
{

		// Reference to First Cube in Snake
		public GameObject snakeBodyCube;
		// Reference to Last Cube in Snake
		public GameObject snakeTailCube;
		// Reference to Player
		public GameObject player;
		// Reference to Player Transform
		public Transform playerTransform;

		// Detect Collision
		void OnTriggerEnter (Collider col)
		{
				// if Collision with Apple
				if (col.gameObject.name == ("Apple")) {
							
						//add new Snake Body
						growSnake ();
				}
		}
		
		void growSnake ()
		{
				// create new snake body object
				// set it's front object and rear object
				// increase snake length variable
		
		}
		
}
	