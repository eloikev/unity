using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {
    public float jukeVol = 1f;
    public AudioSource jukebox;

    public AudioClip movementAudio;
    public AudioClip teleportationAudio;
    public AudioClip alarmAudio;
    public AudioClip cameraAudio;

    // Start is called before the first frame update
    void Start() {
        jukebox.volume = jukeVol;
    }

    // Update is called once per frame
    void Update() {
        movementSound();
        teleportationSound();
        cameraSound();
    }

    void movementSound() {
        if ((Input.GetKeyDown("joystick button 1")) || (Input.GetKeyDown("joystick button 10")) || (Input.GetKeyDown("joystick button 2")) || (Input.GetKeyDown("joystick button 11"))) {
            jukebox.clip = movementAudio;
            jukebox.Play();
        } else if ((Input.GetKeyUp("joystick button 1")) || (Input.GetKeyUp("joystick button 10")) || (Input.GetKeyUp("joystick button 2")) || (Input.GetKeyUp("joystick button 11"))) {
            jukebox.Stop();
        }
    }

    void teleportationSound() {
        if (Input.GetKeyDown("q") || Input.GetKeyDown("joystick button 6") || Input.GetKeyDown("joystick button 8")) {
            jukebox.clip = teleportationAudio;
            jukebox.PlayOneShot(teleportationAudio);
        }
    }

    void cameraSound() {
        if (Input.GetKeyDown("e") || Input.GetKeyDown("joystick button 7") || Input.GetKeyDown("joystick button 9")) {
            jukebox.clip = cameraAudio;
            jukebox.PlayOneShot(cameraAudio);
        }
    }
}
