using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script handles the movement of a satellite around a planet,
// by simulating their gravitational interaction.
public class Movement : MonoBehaviour {
    public float G = 0.1f; // gravitational constant

    public Rigidbody planetbody; // Planet's rigidbody
    protected Vector3 planetPos; // Planet's position in space
    public float planetMass; // Planet's mass

    public Rigidbody satellitebody; // Satellite's rigidbody
    public float satelliteMass; // Satellite's mass
    protected Vector3 satellitePos; // Satellite's position in space
    protected Vector3 satelliteVel; // Satellite's velocity vector
    public Vector3 speedRot; // Rotation speed of the satellite

    void Start() { 
        setup(); // set up the simulation
    }

    void Update() {
        draw(); // update the satellite's position and rotation
    }

    // set up the simulation
    protected void setup() {
        // get the initial positions of the planet and satellite
        planetPos = planetbody.transform.position;
        satellitePos = satellitebody.transform.position;

        // set the initial velocity of the satellite
        satelliteVel = new Vector3(0, 0, 0);
        //satelliteVel += velocityXY(satellitePos, planetPos, planetMass);
        satelliteVel += velocityXZ(satellitePos, planetPos, planetMass);
        //satelliteVel += velocityYZ(satellitePos, planetPos, planetMass);
    }

    // update the satellite's position and rotation
    protected void draw() {
        planetPos = planetbody.transform.position; // update the planet's position

        // update the satellite's position using the appropriate method
        //operateXY(1);
        operateXZ(1);
        //operateYZ(1);

        rotational(speedRot); // rotate the satellite according to the speedRot vector

        satellitebody.transform.position = satellitePos; // update the satellite's position in the scene
    }

    // rotate the satellite according to the speedRot vector
    protected void rotational(Vector3 vect) {
        satellitebody.transform.Rotate((satellitebody.transform.rotation.x + speedRot.x) * Time.deltaTime, (satellitebody.transform.rotation.y + speedRot.y) * Time.deltaTime, (satellitebody.transform.rotation.z + speedRot.z) * Time.deltaTime, Space.Self);
    }

    // rotates the satellite specifically to face the planet
    protected void alignment() {
        Vector3 direction = planetPos - satellitePos;
        Quaternion lookRotation = Quaternion.LookRotation(direction);

        satellitebody.transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
    }

    // This function calculates the velocity of a satellite moving around a planet in a 2D plane
    // based on its distance from the planet, the planet's mass, and the gravitational constant G
    protected Vector3 velocityXY(Vector3 sat, Vector3 pla, float m) {
        Vector3 r = pla - sat; // calculate distance between satellite and planet
        Vector3 t = new Vector3(-r.y, r.x); // calculate tangent to the planet's surface at satellite position
        Vector3 tangent = new Vector3((t.x / Mathf.Sqrt((t.x * t.x) + (t.y * t.y))), (t.y / Mathf.Sqrt((t.x * t.x) + (t.y * t.y)))); // normalize the tangent vector
        float vMag = Mathf.Sqrt(G * m / Mathf.Sqrt((r.x * r.x) + (r.y * r.y))); // calculate the magnitude of the velocity
        return tangent*vMag; // multiply the normalized tangent vector with the velocity magnitude to get the velocity vector
    }

    // This function calculates the acceleration of a satellite moving around a planet in a 2D plane
    // based on its distance from the planet, the planet's mass, and the gravitational constant G
    protected Vector3 accelerationXY(Vector3 sat, Vector3 pla, float mSat, float mPla) {
        Vector3 r = new Vector3((pla.x - sat.x), (pla.y - sat.y), (pla.z - sat.z)); // calculate distance between satellite and planet
        float rMag = Mathf.Sqrt((r.x * r.x) + (r.y * r.y)); // calculate the magnitude of the distance vector

        Vector3 R = new Vector3((r.x / Mathf.Sqrt((r.x * r.x) + (r.y * r.y))), (r.y / Mathf.Sqrt((r.x * r.x) + (r.y * r.y)))); // calculate the normalized direction vector of the distance vector
        Vector3 force = R * (G * mPla * mSat / (rMag * rMag)); // calculate the force of gravity acting on the satellite
        return force;
    }

    // This function updates the velocity and position of the satellite by calculating its acceleration and using a speed multiplier
    protected void operateXY(int multi) {
        satelliteVel += accelerationXY(satellitePos, planetPos, satelliteMass, planetMass) / (satelliteMass)*multi; // update satellite velocity using acceleration and speed multiplier
        satellitePos += satelliteVel*multi; // update satellite position using velocity and speed multiplier
    }

    protected Vector3 velocityXZ(Vector3 sat, Vector3 pla, float m) {
        Vector3 r = pla - sat;
        Vector3 t = new Vector3(-r.z, 0, r.x);
        Vector3 tangent = new Vector3((t.x / Mathf.Sqrt((t.x * t.x) + (t.z * t.z))), 0, (t.z / Mathf.Sqrt((t.x * t.x) + (t.z * t.z))));
        float vMag = Mathf.Sqrt(G * m / Mathf.Sqrt((r.x * r.x) + (r.z * r.z)));
        return tangent*vMag;
    }

    protected Vector3 accelerationXZ(Vector3 sat, Vector3 pla, float mSat, float mPla) {
        Vector3 r = new Vector3((pla.x - sat.x), (pla.y - sat.y), (pla.z - sat.z));
        float rMag = Mathf.Sqrt((r.x * r.x) + (r.z * r.z));

        Vector3 R = new Vector3((r.x / Mathf.Sqrt((r.x * r.x) + (r.z * r.z))), 0, (r.z / Mathf.Sqrt((r.x * r.x) + (r.z * r.z))));
        Vector3 force = R * (G * mPla * mSat / (rMag * rMag));
        return force;
    }


    protected void operateXZ(int multi) {
        satelliteVel += accelerationXZ(satellitePos, planetPos, satelliteMass, planetMass) / (satelliteMass)*multi;
        satellitePos += satelliteVel*multi;
    }

    protected Vector3 velocityYZ(Vector3 sat, Vector3 pla, float m) {
        Vector3 r = pla - sat;
        Vector3 t = new Vector3(0, -r.z, r.y);
        Vector3 tangent = new Vector3(0, (t.y / Mathf.Sqrt((t.y * t.y) + (t.z * t.z))), (t.z / Mathf.Sqrt((t.y * t.y) + (t.z * t.z))));
        float vMag = Mathf.Sqrt(G * m / Mathf.Sqrt((r.y * r.y) + (r.z * r.z)));
        return tangent*vMag;
    }

    protected Vector3 accelerationYZ(Vector3 sat, Vector3 pla, float mSat, float mPla) {
        Vector3 r = new Vector3((pla.x - sat.x), (pla.y - sat.y), (pla.z - sat.z));
        float rMag = Mathf.Sqrt((r.y * r.y) + (r.z * r.z));

        Vector3 R = new Vector3(0, (r.y / Mathf.Sqrt((r.y * r.y) + (r.z * r.z))), (r.z / Mathf.Sqrt((r.y * r.y) + (r.z * r.z))));
        Vector3 force = R * (G * mPla * mSat / (rMag * rMag));
        return force;
    }


    protected void operateYZ(int multi) {
        satelliteVel += accelerationYZ(satellitePos, planetPos, satelliteMass, planetMass) / (satelliteMass)*multi;
        satellitePos += satelliteVel*multi;
    }
}
