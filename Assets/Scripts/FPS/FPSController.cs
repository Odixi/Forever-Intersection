using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour
{
    public float sensitivity = 2f;
    private float speed = 5f;
    private new Camera camera;
    private CharacterController controller;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        camera = Camera.main;
        controller = transform.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        MouseLook();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");
        var moveDirection = Vector3.ClampMagnitude(transform.forward * y + transform.right * x, 1);
        controller.SimpleMove(moveDirection * speed);
    }

    void MouseLook()
    {
        var y = -Input.GetAxis("Mouse Y") * sensitivity;
        var x = Input.GetAxis("Mouse X") * sensitivity;
        camera.transform.Rotate(y, 0, 0);
        transform.Rotate(0, x, 0);
    }
}
