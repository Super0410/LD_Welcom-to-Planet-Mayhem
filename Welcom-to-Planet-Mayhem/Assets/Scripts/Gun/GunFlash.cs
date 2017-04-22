using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFlash : MonoBehaviour
{
	
	#region Public Members

	public GameObject gunFlashParent;
	public Sprite[] spriteArr;
	public SpriteRenderer[] spriteRendererArr;
	public float flashTime = 0.05f;

	#endregion

	#region Unity Methods

	void Start ()
	{
		disActive ();
	}

	#endregion

	#region Public Methods

	public void Active ()
	{
		gunFlashParent.SetActive (true);

		int spriteIndex = Random.Range (0, spriteArr.Length);
		for (int i = 0; i < spriteRendererArr.Length; i++) {
			spriteRendererArr [i].sprite = spriteArr [spriteIndex];
		}

		Invoke ("disActive", flashTime);
	}

	#endregion

	#region Private Methods

	void disActive ()
	{
		gunFlashParent.SetActive (false);
	}

	#endregion
}
