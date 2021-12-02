using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float verticalRange;
    [SerializeField] private Vector2 sensitivity;

    private float pitch;

    private void Awake ()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update ()
    {
        float mouseX = Input.GetAxis ("Mouse X");
        float yRot = transform.rotation.eulerAngles.y + mouseX * sensitivity.x;

        float mouseY = Input.GetAxis ("Mouse Y");
        pitch = Mathf.Clamp (pitch - mouseY * sensitivity.y, -verticalRange, verticalRange);

        Vector3 newRot = transform.rotation.eulerAngles;
        newRot.y = yRot;
        newRot.x = pitch;
        transform.rotation = Quaternion.Euler (newRot);
    }
}
