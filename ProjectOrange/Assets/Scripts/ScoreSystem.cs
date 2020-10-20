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

    public TextMeshProUGUI multiplierText;

    public RawImage thumbsUp;

    private int score = 0;

    private int multiplier = 1;

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
        multiplierText.text = string.Format("x{0}", multiplier);
    }

    // Currently our fixed update is 0.02 per frame or 50fps
    void FixedUpdate() {
        if (!player.GetComponent<CharacterController>().grounded) {
            score += multiplier;
        }
    }

    static public void UpdateMultiplier(int delta) {
        instance.multiplier += delta;
    }

    static public void ResetMultiplier() {
        instance.multiplier = 1;
    }

    static public void UpdateScore(int delta) {
        instance.StartCoroutine(instance.UpdateScoreSlowly(delta));
        instance.StartCoroutine(instance.PulseScore());
        // instance.StartCoroutine(instance.PulseThumbsUp());
    }

    private IEnumerator UpdateScoreSlowly(int delta) {
        for (int i = 0; i < delta; i++) {
            score++;
            yield return new WaitForSeconds(0.01f);
        }
    }

    private IEnumerator PulseScore() {
        for (float i = 1f; i <= 1.5f; i += 0.01f) {
            scoreText.rectTransform.localScale = new Vector3(i, i, i);
            yield return new WaitForSeconds(0.001f);
        }

        for (float i = 1.5f; i >= 1f; i -= 0.01f) {
            scoreText.rectTransform.localScale = new Vector3(i, i, i);
            yield return new WaitForSeconds(0.001f);
        }
    }

    // private IEnumerator PulseThumbsUp() {
    //     RawImage thumb = Instantiate(thumbsUp, gameObject.transform);
    //     yield return new WaitForSeconds(0.001f);
    // }
}
