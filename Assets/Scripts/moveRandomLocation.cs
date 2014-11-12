using UnityEngine;
using System.Collections;

public class moveRandomLocation : MonoBehaviour
{
		public Transform appleTransform;

		void OnTriggerEnter (Collider col)
		{
				//Debug.Log ("Collision Detected Name = " + col.gameObject.name);
				if (col.gameObject.name == ("Player")) {
						//Debug.Log ("Player Collision Detected Moving Apple");
						float randy;
						randy = Random.Range (0, 20);
						float deltaX = randy - appleTransform.position.x;
						
						float deltaY = 0.8f - appleTransform.position.y;
						randy = Random.Range (0, 20);
						float deltaZ = randy - appleTransform.position.z;
									
						appleTransform.Translate (new Vector3 (deltaX, deltaY, deltaZ));
			
				}
		}
}