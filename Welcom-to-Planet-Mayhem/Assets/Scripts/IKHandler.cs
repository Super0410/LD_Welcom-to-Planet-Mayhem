using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKHandler : MonoBehaviour {

	Animator anim;

	public Transform rightHandIKTarget;
	public float rightHandIKWeight;

	public Transform leftHandIKTarget;
	public float leftHandIKWeight;

	Transform aimHelper;

	// Use this for initialization
	void Start ()
	{
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	void OnAnimatorIK(int layerIndex)
	{
		Debug.Log("called");
		anim.SetIKPosition(AvatarIKGoal.LeftHand,leftHandIKTarget.position);



	}

}
