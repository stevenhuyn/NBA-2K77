using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuHoop : MonoBehaviour
{
    private Vector3 startingPosition;
    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = startingPosition + Mathf.Sin(Time.time / 2f) * Vector3.up * 0.1f;
    }

    public void PressPlay() {
        SceneManager.LoadScene("lvl1");
    }
}
