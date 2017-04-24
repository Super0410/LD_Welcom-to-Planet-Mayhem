using System.Collections;
using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
[RequireComponent (typeof(BoxCollider))]
public class SimpleShell : MonoBehaviour
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

	void OnEnable ()
	{
		float force = Random.Range (minForce, maxForce);
		m_rigidbody.AddForce (transform.right * force);
		m_rigidbody.AddTorque (Random.insideUnitSphere * force);

		StartCoroutine (fadeAndDestroy ());
	}

	#endregion

	#region Private Methods

	IEnumerator fadeAndDestroy ()
	{
		yield return new WaitForSeconds (lifeTime);

		float percent = 0;
		float fadeSpeed = 1 / fadeTime;

		while (percent < 1) {
			percent += fadeSpeed * Time.deltaTime;

			shellMat.color = Color.Lerp (shellMat.color, Color.clear, percent);
			yield return null;
		}

		ObjectPooler.ReturnInstance (GetType ().Name, gameObject);
	}

	#endregion
}
