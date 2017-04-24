using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetDetector : MonoBehaviour
{
	void OnCollisionEnter (Collision collision)
	{
		print ("ENTER");
		if (collision.collider.name == "Planet") {
			Destroy (GetComponent<GravityBody> ());
			Destroy (GetComponent<PlanetDetector> ());
		}
	}
}
