using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldController : MonoBehaviour
{
    public GameObject human;
    public GameObject kitten;
    PlayerController kittenController;
    PlayerController humanController;
    public CamController camController;
    GameObject activeCharacter;
    bool charsJoined = true;
    float joinDistance = 3f;
    float kittenDropOffset = 1.5f;
    int humanLayer = 9;
    int kittenLayer = 11;
    int catgirlLayer = 13;

    public ParticleSystem sparkles;
    public ParticleSystem pop;
    public ParticleSystem unpop;
    int popAmount = 50;

    public Image joinPrompt;

    bool isPaused;

    public GameObject menuPanel;
    public Button resumeButton;
    public Button quitButton;
    bool resumeButtonSelected;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        menuPanel.SetActive(false);
        isPaused = false;
        activeCharacter = human;
        kittenController = kitten.GetComponent<PlayerController>();
        humanController = human.GetComponent<PlayerController>();
        resumeButtonSelected = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonUp("Pause")){
            if (isPaused) {
                UnPause();
            } else {
                Pause();
            }
        }

        if (isPaused) {
            if (Input.GetAxis("Vertical") > 0) {
                if (!resumeButtonSelected) {
                    resumeButtonSelected = true;
                }
            }
            if (Input.GetAxis("Vertical") < 0) {
                if (resumeButtonSelected) {
                    resumeButtonSelected = false;
                }
            }
            if (Input.GetButtonUp("Fire1")) {
                if (resumeButtonSelected) {
                    UnPause();
                } else {
                    Quit();
                }
            }
            if (Input.GetButtonUp("Fire2")) {
                UnPause();
            }
        } else {

            if (charsJoined) {
                // If the characters are joined then:
                // - don't display the join prompt
                // - set the human as the active character
                // - match the kitten's position to the human's
                joinPrompt.enabled = false;
                activeCharacter = human;
                sparkles.gameObject.SetActive(true);

                humanController.isActive = true;
                humanController.isJoined = true;
                humanController.SetJoinedModel();
                human.layer = catgirlLayer;

                kittenController.isActive = true;
                kittenController.isJoined = true;
                kittenController.SetJoinedModel();
                kitten.layer = catgirlLayer;

                kitten.transform.position = human.transform.position;

                if (Input.GetButtonUp("Fire2")) {
                    // If the player presses the join button then they separate
                    sparkles.gameObject.SetActive(false);
                    pop.Emit(popAmount);
                    charsJoined = false;

                    humanController.isActive = true;
                    humanController.isJoined = false;
                    humanController.SetUnjoinedModel();
                    human.layer = humanLayer;

                    kittenController.isActive = false;
                    kittenController.isJoined = false;
                    kittenController.SetUnjoinedModel();
                    kitten.layer = kittenLayer;
                    kitten.transform.position = new Vector3(human.transform.position.x, human.transform.position.y, human.transform.position.z - kittenDropOffset);
                }
            } else {
                // If the characters aren't joined...
                if (Vector3.Distance(human.transform.position, kitten.transform.position) < joinDistance) {
                    // Check if they're close enough to join, show the join prompt
                    joinPrompt.enabled = true;
                    if (Input.GetButtonUp("Fire2")) {
                        // If the player presses the join button then:
                        // - Play the joining particle effect
                        // - Start the catgirl sparkles
                        // - join the characters
                        // - Set the human as active
                        // - sets the camera to follow the human
                        // - Puts all characters on the catgirl layer
                        unpop.Emit(popAmount);
                        sparkles.gameObject.SetActive(true);
                        charsJoined = true;
                        kitten.transform.position = human.transform.position;

                        humanController.isActive = true;
                        humanController.isJoined = true;
                        humanController.SetJoinedModel();
                        human.layer = catgirlLayer;

                        kittenController.isActive = true;
                        kittenController.isJoined = true;
                        kittenController.SetJoinedModel();
                        kitten.layer = catgirlLayer;

                        activeCharacter = human;
                        camController.SetActiveCharacter(activeCharacter);
                    }
                } else {
                    // If the characters aren't close enough to join then hide the join prompt
                    joinPrompt.enabled = false;
                }

                // If the player presses space then switch characters so long as the camera isn't already moving
                if (Input.GetButtonUp("Fire1")) {
                    if (!camController.IsMoving()) {
                        if (activeCharacter == human) {
                            activeCharacter = kitten;
                            humanController.isActive = false;
                            kittenController.isActive = true;
                        } else {
                            activeCharacter = human;
                            humanController.isActive = true;
                            kittenController.isActive = false;
                        }
                        camController.SetActiveCharacter(activeCharacter);
                    }
                }
            }
        }
    }

    public void Pause() {
        humanController.Pause();
        kittenController.Pause();
        menuPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        isPaused = true;
    }

    public void UnPause() {
        humanController.UnPause();
        kittenController.UnPause();
        menuPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isPaused = false;
    }

    public void Quit() {
        Application.Quit();
    }

    public GameObject GetActiveCharacter() {
        return activeCharacter;
    }
}
