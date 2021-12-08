using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickyUppy : MonoBehaviour
{
    [SerializeField] float throwForce, holdDistance;

    private Camera cam;
    private Rigidbody grabbedObject;

    private Vector3 MouseWorldPos => transform.position + transform.forward * holdDistance;

    private void Awake ()
    {
        cam = GetComponent<Camera> ();
    }

    private void Update ()
    {
        if (Input.GetMouseButtonDown (0))
        {
            Ray mouseRay = new Ray (transform.position, transform.forward);
            if (Physics.Raycast (mouseRay, out RaycastHit hit, 50))
            {
                if (hit.collider.CompareTag ("Pickup") && hit.collider.TryGetComponent (out Rigidbody rb))
                {
                    grabbedObject = rb;
                    grabbedObject.useGravity = false;
                }
            }
        }
        else if (Input.GetMouseButtonUp (0) && grabbedObject != null)
        {
            grabbedObject.isKinematic = false;
            grabbedObject.useGravity = true;
            grabbedObject.AddForce (transform.forward * throwForce, ForceMode.Impulse);
            StartCoroutine(grabbedObject.GetComponent<Drink>().ReturnToPosition());
            grabbedObject = null;
        }

        if (Input.GetMouseButton (0) && grabbedObject != null)
        {
            grabbedObject.MovePosition (MouseWorldPos);
        }
    }
}
