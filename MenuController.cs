using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    float fadeTime = 4f;
    bool fading = false;
    float fader = 1f;
    AudioSource audioSource;
    public Image panel;

    public Button startGame;
    public Button quitGame;

    bool startButtonSelected;

    // Start is called before the first frame update
    void Start()
    {
        startButtonSelected = true;
        audioSource = GetComponent<AudioSource>();
        panel.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (fading) {
            fader -= Time.deltaTime / fadeTime;
            audioSource.volume = fader;
            panel.color = new Color(0, 0, 0, 1-fader);
        }

        if (fader < 0) {
            SceneManager.LoadScene("SampleScene");
            fading = false;
        }

        if (Input.GetAxis("Vertical") > 0) {
            if (!startButtonSelected) {
                startButtonSelected = true;
            }
        }
        if (Input.GetAxis("Vertical") <0) {
            if (startButtonSelected) {
                startButtonSelected = false;
            }
        }

        if (Input.GetButtonUp("Fire1")) {
            if (startButtonSelected) {
                StartGame();
            } else {
                QuitGame();
            }
        }
    }

    public void StartGame() {
        fading = true;
        panel.color = new Color(0, 0, 0, 1 - fader);
        audioSource.volume = fader;
        panel.gameObject.SetActive(true);
    }

    public void QuitGame() {
        Application.Quit();
    }
}
