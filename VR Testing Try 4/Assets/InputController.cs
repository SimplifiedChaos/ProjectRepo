using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class InputController : MonoBehaviour
{

    public SteamVR_Action_Boolean PickupLine;
    public SteamVR_Action_Boolean GrabGrip;
    public SteamVR_Input_Sources handSource;
    public Hand hand;
    public Transform pointer;
    public LayerMask grabMask;
    public Hand.AttachmentFlags attachmentFlags;
    public LineRenderer lineRenderer;
    public float targetLength = 5.0f;

    GameObject attachedObject;

    




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 endPosition = pointer.position + (pointer.transform.forward * targetLength);
        lineRenderer.SetPosition(0, pointer.transform.position);
        lineRenderer.SetPosition(1, endPosition);
        lineRenderer.enabled = false;
        if ((PickupLine[handSource].state) && (hand.currentAttachedObject == null))
        {
            RaycastHit hit;
            Debug.DrawRay(pointer.position, pointer.forward, Color.green);
            lineRenderer.enabled = true;
            if(Physics.Raycast(pointer.position, pointer.forward, out hit, 10f, grabMask))
            {
                Interactable interactable = hit.collider.gameObject.GetComponent<Interactable>();

                if((interactable != null) && (GrabGrip[handSource].state))
                {
                    attachedObject = interactable.gameObject;
                    Transform attachmentOffset = attachedObject.GetComponent<Throwable>().attachmentOffset;
                    if (attachmentOffset != null)
                    {
                        hand.AttachObject(attachedObject, GrabTypes.Grip, attachmentFlags, attachmentOffset);
                        Debug.Log("OFFSET GRAB");
                    }
                    else
                    {
                        hand.AttachObject(attachedObject, GrabTypes.Grip);
                        Debug.Log("no offset grab");
                    }

                }
            }
            
        }
    }
}
