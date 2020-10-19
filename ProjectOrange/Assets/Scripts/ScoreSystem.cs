using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour
{
    static public ScoreSystem instance;
    public Transform player;
    public TextMeshProUGUI scoreText;

    private int score = 0;

    // Called when an instance awakes in the game
    void Awake() {
        instance = this;
    }


    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        scoreText.text = score.ToString("0");
    }

    static public void UpdateScore(int delta) {
        instance.StartCoroutine(instance.UpdateScoreSlowly(delta));
    }

    private IEnumerator UpdateScoreSlowly(int delta) {
        for (int i = 0; i < delta; i++) {
            score++;
            yield return new WaitForSeconds(0.01f);
        }
    }
}
