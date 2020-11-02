using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static int level = 1;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R)) {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name, LoadSceneMode.Single);
        }

        if(Input.GetKeyDown(KeyCode.N)) {
            level++;
            SceneManager.LoadScene(getLevelName(), LoadSceneMode.Single);
        }

        if(Input.GetKeyDown(KeyCode.B)) {
            level--;
            SceneManager.LoadScene(getLevelName(), LoadSceneMode.Single);
        }
    }

    public static string getLevelName() {
        return String.Format("lvl{0}", level);
    }
}
