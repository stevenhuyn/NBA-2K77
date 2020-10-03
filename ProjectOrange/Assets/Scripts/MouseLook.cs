using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Thanks Brackeys: https://www.youtube.com/watch?v=_QajrabyTJc */

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;

    float xRotation = 0f;

    public Transform playerBody;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // I have no idea how this works so elegantly
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        playerBody.Rotate(Vector3.up * mouseX);
    }
}
