using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : Movement {
    private bool idlePeriod;
    public float idlePeriodSec = 10f;
    private float startTime;
    private float elapsedTime;

    public float changeFactor = 20f;
    public float boostFactor = 5f;

    public CharacterController controller;
    public Transform cameraTransformBody;
    private float xRotDir = 0f;

    // Start is called before the first frame update
    void Start() {
        idlePeriod = false;
        startTime = Time.time;
        elapsedTime = 0;
    }

    // Update is called once per frame
    void Update() {
        movement();
        rotational();
        teleportation();

        timer();
        if (idlePeriod) {
            draw();
            alignment();
        }
    }

    void timer() {
        if ((Input.GetKey("e")) || (Input.GetKey("joystick button 7")) || (Input.GetKey("joystick button 9"))) {
            startTime = -2*idlePeriodSec;
            idlePeriod = false;
        }

        if ((Input.GetAxis("Horizontal") != 0) || (Input.GetAxis("Vertical") != 0) || (Input.GetAxis("RightJoystickHorizontal") != 0) || (Input.GetKey("joystick button 1")) || (Input.GetKey("joystick button 2")) || (Input.GetKey("z")) || (Input.GetKey("x")) || (Input.GetKey("q")) || (Input.GetKey("joystick button 6")) || (Input.GetKey("joystick button 8"))) {
            startTime = Time.time;
            elapsedTime = 0;
        }

        elapsedTime = Time.time - startTime;

        if (elapsedTime >= idlePeriodSec) {
            if (!idlePeriod) {
                setup();
            }
            idlePeriod = true;
        } else {
            idlePeriod = false;
        }
    }

    void movement() {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        //controller.Move(move * changeFactor);

        if ((Input.GetKey("joystick button 1")) || (Input.GetKey("joystick button 10"))) {
            controller.Move(move * changeFactor * boostFactor * Time.deltaTime);
        } else {
            controller.Move(move * changeFactor * Time.deltaTime);
        }
    }

    void rotational() {
        float y = Input.GetAxis("RightJoystickHorizontal");

        if (Input.GetKey("z") || (y < 0)) {
            xRotDir -= 0.01f*changeFactor;
        } else if (Input.GetKey("x") || (y > 0)) {
            xRotDir += 0.01f*changeFactor;
        } else {
            xRotDir = 0;
        }

        if ((Input.GetKey("joystick button 2")) || (Input.GetKey("joystick button 11"))) {
            cameraTransformBody.Rotate(Vector3.up * xRotDir * boostFactor * Time.deltaTime);
        } else {
            cameraTransformBody.Rotate(Vector3.up * xRotDir * Time.deltaTime);
        }
    }

    void teleportation() {
        if (Input.GetKey("q") || Input.GetKey("joystick button 6") || Input.GetKey("joystick button 8")) {
            Vector3 restartPos = new Vector3(0, 0, -150);
            Quaternion restartRot = Quaternion.Euler(0, 0, 0);

            cameraTransformBody.transform.position = restartPos;
            cameraTransformBody.transform.rotation = restartRot;
        }
    }
}
