using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetCode : MonoBehaviour {
    public Rigidbody planetbody;
    private Vector3 planetPos;
    public Vector3 speedRot;

    // Start is called before the first frame update
    void Start() {
        planetPos = GetComponent<Rigidbody>().position;
    }

    // Update is called once per frame
    void Update() {
        GetComponent<Rigidbody>().position = planetPos;
        rotational(speedRot);
    }

    void rotational(Vector3 vect) {
        planetbody.transform.Rotate((planetbody.transform.rotation.x + speedRot.x) * Time.deltaTime, (planetbody.transform.rotation.y + speedRot.y) * Time.deltaTime, (planetbody.transform.rotation.z + speedRot.z) * Time.deltaTime, Space.Self);
    }
}
