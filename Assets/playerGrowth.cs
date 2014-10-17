using UnityEngine;
using System.Collections;

public class playerGrowth : MonoBehaviour {

	public GameObject[] snakeSec = new GameObject[100];
	Vector3 mainPos;
	Vector3 furitPos;
	Vector3 negOne = new Vector3(-1, -1, -1);
	Vector3 posOne = new Vector3(1, 1, 1);

	public GameObject headCube;
	public GameObject snakeCubes;
	public GameObject furit;
	

	private int numbCubes = 0, xPosF, zPosF, xPosM, zPosM;

	private bool makeBlock = false;
	// make a static int for number of cubes snake has and multiply that number by the length of the snake 
	// to print behind it 


	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
		mainPos = headCube.transform.position;
		furitPos = furit.transform.position;

		xPosF = (int)furitPos.x;
		xPosM = (int)mainPos.x;
		zPosF = (int)furitPos.z;
		zPosM = (int)mainPos.z;


		// if x and z conponents
	
		if( xPosM == xPosF && zPosM == zPosF )
		{
			//test variables

			numbCubes++;
			mainPos = headCube.transform.position;
			mainPos.z = mainPos.z - numbCubes + 0.1f;

			furit.transform.position = new Vector3(Random.Range(2, 16), 0.7f, Random.Range(2, 16));
			
			// do not use first postion of array
			snakeSec [numbCubes] = (GameObject)Instantiate (snakeCubes, mainPos, transform.rotation);
		}

		
	}

		
	

}
