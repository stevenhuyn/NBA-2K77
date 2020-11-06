using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuButtons : MonoBehaviour
{
    public Button DoneButton;
    public Image CreditsImage;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PressPlay() {
        SceneManager.LoadScene("lvl1", LoadSceneMode.Single);
        MenuScript.isSandbox = true;
        LevelManager.ChangeLevel(1);
    }

    public void Credits() {
        CreditsImage.gameObject.SetActive(true);
        DoneButton.gameObject.SetActive(true);
    }

    public void Exit() {
        Application.Quit();
    }

    public void Done() {
        CreditsImage.gameObject.SetActive(false);
        DoneButton.gameObject.SetActive(false);
    }
}
