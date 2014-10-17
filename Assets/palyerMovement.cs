/*
 * The left turn brakes the program
 * Right turn works just fine!!!
*/
using UnityEngine;
using System.Collections;

public class palyerMovement : MonoBehaviour {

	int rotLeft = -1,
		rotRight = 1,
		counterRot  = 0,
		turnX,
		turnZ,
		counterTurn = 0;

	bool leftTurn = false,
		rightTurn = false,
			 turn = false;


	//public static int turnX, turnY;

	Vector3 pos; 

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		pos = transform.position;

		if (pos.x%1 >= 0 && pos.x%1 <= .015 && pos.z%1 >= 0 && pos.z%1 <= .015  && rightTurn || leftTurn) 
		{
			Debug.Log( " turn = true" + " RT :" + rightTurn + " LT :" + leftTurn  );
			turn = true;

		} 
		else if( leftTurn == false && rightTurn == false)
		{
			turn = false;
			//Debug.Log( " turn = false" );
		}

		if (leftTurn && turn ) 
		{
			//Debug.Log( " should be turning left" );
			counterRot++;

			transform.Rotate(0, rotLeft, 0);

			if( counterRot >= 90 )
			{
				counterRot = 0;
				leftTurn = false;
			}

		}
		else if (rightTurn && turn) 
		{
			Debug.Log( " should be turning righ" );

			counterRot++;
			
			transform.Rotate(0, rotRight, 0);
			
			if( counterRot >= 90 )
			{
				counterRot = 0;
				rightTurn = false;
			}
			
		}
		else
		{
			counterTurn = 0;
			transform.Translate (Vector3.forward * Time.deltaTime);
		}
	}

	void OnGUI() {

		if(GUI.Button (new Rect(0, 0, Screen.width/8, Screen.height/8), "left"))
		{
			if( rightTurn == false)
			{
				leftTurn = true;
				//Debug.Log ( " leftTurn " ); 
			}
		}

		if(GUI.Button (new Rect(Screen.width - Screen.width/8, 0, Screen.width/8, Screen.height/8), "right"))
		{
			if( leftTurn == false)
			{
				rightTurn = true;
				Debug.Log ( " rightTurn " ); 
			}
		}
	}


}
