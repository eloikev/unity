using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionAvoider : MonoBehaviour {
    public Rigidbody cameraBody;
    private float yPos = 0f;

    void Start() {
        yPos = cameraBody.transform.position.y;
    }

    void Update() {
        if (cameraBody.transform.position.y != yPos) {
            Vector3 pos = new Vector3(cameraBody.transform.position.x, yPos, cameraBody.transform.position.z);
            cameraBody.transform.position = pos;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.GetComponent<Collider>().tag == "Body") {
            Physics.IgnoreCollision(other, GetComponent<Collider>(), true);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.GetComponent<Collider>().tag == "Body") {
            Physics.IgnoreCollision(other, GetComponent<Collider>(), false);
        }
    }

}
