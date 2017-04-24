using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterColors
{
	Red,
	Green,
	Yellow,
	Blue
}



public class SetCharacterColor : MonoBehaviour {

	public CharacterColors characterColor;

	public Material RedMaterial;
	public Material GreenMaterial;
	public Material YellowMaterial;
	public Material BlueMaterial;

	void Awake()
	{

		switch (characterColor)
		{
		case CharacterColors.Blue:
			GetComponentInChildren<SkinnedMeshRenderer>().material = BlueMaterial;
			break;
		case CharacterColors.Red:
			GetComponentInChildren<SkinnedMeshRenderer>().material = RedMaterial;
			break;
		case CharacterColors.Green:
			GetComponentInChildren<SkinnedMeshRenderer>().material = GreenMaterial;
			break;
		case CharacterColors.Yellow:
			GetComponentInChildren<SkinnedMeshRenderer>().material = YellowMaterial;
			break;
		default:
			break;
		}

	}
}
