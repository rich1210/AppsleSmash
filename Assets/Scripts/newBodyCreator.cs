using UnityEngine;
using System.Collections;

public class newBodyCreator : MonoBehaviour
{

		public GameObject originalBody;
		public GameObject headObject;
	
		void OnTriggerEnter (Collider col)
		{		
				if (col.gameObject.name == ("Apple")) {
						//Instantiate (originalBody);
				}
		}
}
