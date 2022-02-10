using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class Movement : MonoBehaviour
{
	public SteamVR_Action_Vector2 input;
	public float speed = 10;
	public bool useGravity = false;
	private CharacterController charCont;
	public Transform head;

	// Use this for initialization
	void Start()
	{
		charCont = GetComponent<CharacterController>();
	}

	// Update is called once per frame
	void Update()
	{
		if (Player.instance.leftHand != null)
        {
			Vector3 dir = Player.instance.leftHand.transform.TransformDirection(new Vector3(input.axis.x, 0, input.axis.y));
			Vector3 velocity = speed * Time.deltaTime * Vector3.ProjectOnPlane(dir, Vector3.up);
			charCont.Move(velocity);
		}
		
		Vector3 gravity = Vector3.down * 9.81f * Time.deltaTime;
		if (useGravity) charCont.Move(gravity);

		Vector3 headpos = head.localPosition;
		charCont.center = new Vector3(headpos.x, 1, headpos.z);
	}
}
