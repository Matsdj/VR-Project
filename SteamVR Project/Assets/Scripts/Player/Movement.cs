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
    private float rayLength = 1.1f;

    void Start()
    {
        charCont = GetComponent<CharacterController>();
    }

    void Update()
    {
        Vector3 dir = Vector3.zero;
        if (Player.instance.leftHand != null) dir = Player.instance.leftHand.transform.TransformDirection(new Vector3(input.axis.x, 0, input.axis.y));

        if (dir.magnitude > .1f)
        {
            Vector3 velocity = speed * Time.deltaTime * Vector3.ProjectOnPlane(dir, Vector3.up);
            if (!RayCast(rayLength, charCont.center + transform.position)) velocity += Vector3.down * 9.81f;
            charCont.Move(velocity);
        }
        else
        {
            if (!RayCast(rayLength, charCont.center + transform.position)) transform.position += Vector3.down * 9.81f * Time.deltaTime;
        }

        Vector3 headpos = head.localPosition;
        charCont.center = new Vector3(headpos.x, 1, headpos.z);

        if (transform.position.y < -3) transform.position = Vector3.zero;
    }

    public static bool RayCast(float rayLength, Vector3 origin)
    {
        RaycastHit hit;
        // Bit shift the index of the layer (11) to get a bit mask
        int layerMask = 1 << 11;

        // Does the ray intersect any objects excluding the player layer
        var hasHit = Physics.Raycast(origin, Vector3.down, out hit, rayLength, layerMask);
        if (hasHit)
        {
            Debug.DrawRay(origin, Vector3.down * hit.distance, Color.green);
        }
        else
        {
            Debug.DrawRay(origin, Vector3.down * rayLength, Color.red);
        }
        return hasHit;
    }
}
