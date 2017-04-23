using UnityEngine;
using System.Collections;

public class XLine : MonoBehaviour
{
	public GameObject line;
	public GameObject hitEffect;

	void Update ()
	{
		RaycastHit hit;
		Vector3 lineScale;// 变换大小
		lineScale.x = 1f;
		lineScale.z = 1f;

		if (Physics.Raycast (transform.position, this.transform.forward, out hit)) {
			Debug.DrawLine (this.transform.position, hit.point);
			lineScale.y = hit.distance;
			hitEffect.transform.position = hit.point;
			hitEffect.SetActive (true);
		} else {
			lineScale.y = 500;
			hitEffect.SetActive (false);
		}
			
		line.transform.localScale = lineScale;
            
	}
}
