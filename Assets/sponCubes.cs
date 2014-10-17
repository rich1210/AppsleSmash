using UnityEngine;
using System.Collections;

public class sponCubes : MonoBehaviour {

	public GameObject myCube;
	public GameObject myLight;
	//Vector3[] spawnSpots = new Vector3[25];
	
	void Start() {
		for (int z = 0; z < 25; z+= 5) 
		{
			for( int x = 0; x < 25; x+= 5)
			{
						GameObject cubeSpawn = (GameObject)Instantiate (myCube, new Vector3 (x, -1, z), transform.rotation);
						GameObject lightSpawn = (GameObject)Instantiate (myLight, new Vector3 (x, 3, z), transform.rotation);
			}
		}
	}
	
	void Update() {
	}
}


