using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetDetector : MonoBehaviour
{
	void OnCollisionEnter (Collision collision)
	{
		print ("ENTER");
		if (collision.collider.name == "Planet") {
			GetComponent<GravityBody> ().enabled = false;
			GetComponent<PlanetDetector> ().enabled = false;
		}
	}
}
