using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
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
    }


    public void Credits() {
        Debug.Log("NBA 2K");
    }

    public void Exit() {
        Application.Quit();
    }
}
