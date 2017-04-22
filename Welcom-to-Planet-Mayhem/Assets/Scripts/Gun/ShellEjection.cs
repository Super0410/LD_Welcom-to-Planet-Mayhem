using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
[RequireComponent (typeof(BoxCollider))]
public class ShellEjection : MonoBehaviour
{
	
	#region Public Members

	public Rigidbody m_rigidbody;
	public float minForce;
	public float maxForce;

	public float lifeTime = 2;
	public float fadeTime = 1;

	#endregion

	Material shellMat;

	#region Unity Methods

	void Awake ()
	{
		shellMat = GetComponent<Renderer> ().material;
	}

	void Start ()
	{
		float force = Random.Range (minForce, maxForce);
		m_rigidbody.AddForce (transform.right * force);
		m_rigidbody.AddTorque (Random.insideUnitSphere * force);

		StartCoroutine (fadeAndDistroy ());
	}

	#endregion

	#region Private Methods

	IEnumerator fadeAndDistroy ()
	{
		yield return new WaitForSeconds (lifeTime);

		float percent = 0;
		float fadeSpeed = 1 / fadeTime;

		while (percent < 1) {
			percent += fadeSpeed * Time.deltaTime;

			shellMat.color = Color.Lerp (shellMat.color, Color.clear, percent);
			yield return null;
		}

		Destroy (gameObject);
	}

	#endregion
}
