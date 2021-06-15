using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public DoorController connectedDoor;
    public DoorController secondaryDoor;

    public bool stayPressed = false;
    bool buttonOn = false;
    bool pressed = false;
    public AudioSource audioSource;
    public AudioClip buttonPress;
    public AudioClip buttonUnPress;

    List<GameObject> touchers;

    public GameObject buttonModel;
    float buttonTravel = .1f;

    public Color pressedColor;
    public Color unpressedColor;

    // Start is called before the first frame update
    void Start()
    {
        touchers = new List<GameObject>();
        buttonModel.GetComponent<MeshRenderer>().material.color = unpressedColor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        
        // Only take actions if no one is already on the button
        if (!pressed) {
            pressed = true;
            // Move the button, play a sound, send a message to all connected doors that the button has been pressed
            buttonModel.transform.position = new Vector3(transform.position.x, transform.position.y - buttonTravel, transform.position.z);
            audioSource.PlayOneShot(buttonPress);
            ToggleDoors();

            // Check the button type (Toggle or momentary)
            if (stayPressed) {
                // Toggle type

                // Check the current button state
                if (buttonOn) {
                    // Button is on, turn it off
                    // Switch the colour to unpressed
                    buttonModel.GetComponent<MeshRenderer>().material.color = unpressedColor;
                    buttonOn = false;
                } else {
                    // Button is off, turn it on
                    // Switch the colour to pressed
                    buttonModel.GetComponent<MeshRenderer>().material.color = pressedColor;
                    buttonOn = true;
                }
            } else {
                // Momentary type
                // Switch the colour to "pressed"
                buttonModel.GetComponent<MeshRenderer>().material.color = pressedColor;
            }
        }

        // Keep track of who is pushing the button
        touchers.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other) {
        // Keep track of who is touching the button
        touchers.Remove(other.gameObject);

        // Only take actions if the button is currently pressed and no one is left standing on it
        if (pressed && touchers.Count == 0) {
            pressed = false;

            // Play the unpress sound and move the button back into unpressed position
            audioSource.PlayOneShot(buttonUnPress);
            buttonModel.transform.position = new Vector3(transform.position.x, transform.position.y + buttonTravel, transform.position.z);

            // Check the button type, toggle or momentary
            if (stayPressed) {
                // Toggle type
                // Keeps the colour and doesn't trigger anything
            } else {
                // Momentary type
                // Send a signal to all connected doors, colour the button as unpressed
                ToggleDoors();
                buttonModel.GetComponent<MeshRenderer>().material.color = unpressedColor;
            }
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Vector3 startPos = transform.position;
        Vector3 mainDoor = new Vector3(connectedDoor.gameObject.transform.position.x, 2f, connectedDoor.gameObject.transform.position.z);
        Gizmos.DrawLine(startPos, mainDoor);
        if (secondaryDoor != null) {
            Vector3 secondDoor = new Vector3(secondaryDoor.gameObject.transform.position.x, 2f, secondaryDoor.gameObject.transform.position.z);
            Gizmos.DrawLine(startPos, secondDoor);
        }
    }

    private void ToggleDoors() {
        connectedDoor.Toggle();

        if (secondaryDoor != null) {
            secondaryDoor.Toggle();
        }
    }
}
