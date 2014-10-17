using UnityEngine;
using System.Collections;

public class sectionalMovement : MonoBehaviour {
	int counter = 0;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (counter < 35) 
		{
			transform.Translate (Vector3.forward * Time.deltaTime);
			counter++;
		}
	}
}
