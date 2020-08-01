using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{

    public Vector3 move = new Vector3(0, 0, 0);
    public Vector3 rotation = new Vector3(0, 0, 0);
    public Vector3 mouseMove = new Vector3(0, 0, 0);
    public Vector3 cameraHolder1Move = new Vector3(0, 0, 0);
    public Vector3 cameraHolder2Move = new Vector3(0, 0, 0);

    public float camXSpeed;
    public float camYSpeed;
    public float speedRotation = 1.0f;
    public float speed = 1.0f;

    public Rigidbody rigidbody;
    public Transform CameraH1;
    public Transform CameraH2;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButton(1))
        {
            MoveMouse();
            MovePPM();
        }
        else if (Input.GetMouseButton(0))
        {
            MoveCamera();
        }
        else
        {
            HeroMoveNormal();
        }
    }

    private void MoveCamera()
    {
        Cursor.visible = false;
        cameraHolder1Move.y = Input.GetAxis("Mouse X");
        CameraH1.Rotate(cameraHolder1Move, camXSpeed);

        cameraHolder2Move.x = Input.GetAxis("Mouse Y") * -1f;
        CameraH2.Rotate(cameraHolder2Move, camYSpeed);

    }

    private void MovePPM()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if ((v > 0) && (h > 0)) move = transform.forward + transform.right;
        else if ((v > 0) && (h < 0)) move = transform.forward + transform.right * -1f;
        else if ((v < 0) && (h > 0)) move = transform.forward * -1f + transform.right;
        else if ((v < 0) && (h < 0)) move = transform.forward * -1f + transform.right * -1f;
        else if (v > 0) move = transform.forward;
        else if (v < 0) move = transform.forward * -.8f;
        else if (h > 0) move = transform.right;
        else if (h < 0) move = transform.right * -1f;
        else move = Vector3.zero;

        rigidbody.MovePosition(transform.localPosition + move * Time.deltaTime * speed);

    }

    private void MoveMouse()
    {
        Cursor.visible = false;
        mouseMove.y = Input.GetAxis("Mouse X");
        Quaternion mouseRot = Quaternion.Euler(mouseMove * speed);
        rigidbody.MoveRotation(rigidbody.rotation * mouseRot);
    }

    void HeroMoveNormal()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if (v > 0) move = transform.forward;
        else if (v < 0) move = transform.forward * -1.0f;
        else move = Vector3.zero;

        if (h != 0)
        {
            rotation.y = h;
            Quaternion moveRot = Quaternion.Euler(rotation * speedRotation);
            rigidbody.MoveRotation(rigidbody.rotation * moveRot);
        }

        rigidbody.MovePosition(transform.localPosition + move * speed * Time.deltaTime);
    }
}
