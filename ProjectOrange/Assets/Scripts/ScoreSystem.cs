using System.Collections;
using System.Collections.Generic;
using Coffee.UIExtensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour
{
    static public ScoreSystem instance;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI multiplierText;
    private GameObject player;
    public Transform ScorePopup;
    public int score = 0;
    private int multiplier = 1;
    private int frameCounter = 0;
    private Color scoreColor;

    // Called when an instance awakes in the game
    void Awake() {
        instance = this;
    }


    // Start is called before the first frame update
    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update() {
        scoreText.text = score.ToString("0");
        multiplierText.text = string.Format("x{0}", multiplier);
    }

    // Currently our fixed update is 0.02 per frame or 50fps
    void FixedUpdate() {
        frameCounter++;
        if (!player.GetComponent<CharacterController>().Grounded) {
            if (frameCounter % 3 == 0) {
                score += multiplier;
            }
        } else {
            ResetMultiplier();
        }
    }

    static public void UpdateMultiplier(int delta) {
        instance.multiplier += delta;
        instance.StartCoroutine(instance.PulseText(instance.scoreText));
        instance.StartCoroutine(instance.PulseText(instance.multiplierText));
    }

    static public void ResetMultiplier() {
        instance.multiplier = 1;
    }

    static public void UpdateScore(int delta) {
        Transform scorePopup = Instantiate(instance.ScorePopup, instance.gameObject.transform);
        scorePopup.GetComponent<ScorePopupScript>().score = instance.scoreText.transform;
        scorePopup.transform.localScale = Mathf.Log10(delta/10f) * Vector3.one;
        scorePopup.GetComponent<TextMeshProUGUI>().text = string.Format("+{0}", delta);

        instance.StartCoroutine(instance.UpdateScoreSlowly(delta));
        instance.StartCoroutine(instance.PulseText(instance.scoreText));
        instance.StartCoroutine(instance.GlowText(instance.scoreText));
    }

    static public void Dunk(int amount, bool disabled) {
        instance.StartCoroutine(instance.ProcessDunk(amount, disabled));
    }

    private IEnumerator ProcessDunk(int amount, bool disabled) {
        int delta = disabled ? 50 : 200;
        for (int i = 0; i < amount; i++) {
            UpdateScore(delta);
            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator UpdateScoreSlowly(int delta) {
        for (int i = 0; i < delta; i++) {
            score += instance.multiplier;
            yield return new WaitForSeconds(0.01f);
        }
    }

    private IEnumerator PulseText(TextMeshProUGUI text) {
        for (float i = 1f; i <= 1.5f; i += 0.01f) {
            text.rectTransform.localScale = new Vector3(i, i, i);
            yield return new WaitForSeconds(0.001f);
        }

        for (float i = 1.5f; i >= 1f; i -= 0.01f) {
            text.rectTransform.localScale = new Vector3(i, i, i);
            yield return new WaitForSeconds(0.001f);
        }
    }

    private IEnumerator GlowText(TextMeshProUGUI text) {
        for (float i = 0.5f; i <= 1f; i += 0.01f) {
            text.GetComponent<UIShadow>().blurFactor = i;
            scoreColor = Color.HSVToRGB(.0722f, 1f, 1f);
            scoreColor.a = i;
            text.GetComponent<UIShadow>().effectColor = scoreColor;
            yield return new WaitForSeconds(0.001f);
        }
        text.GetComponent<UIShadow>().blurFactor = 0.5f;
        scoreColor = Color.HSVToRGB(.0722f, 1f, 1f);
        scoreColor.a = 0.5f;
        text.GetComponent<UIShadow>().effectColor = scoreColor;
        yield return new WaitForSeconds(0.001f);
    }
    public static int GetMultiplier() {
        return instance.multiplier;
    }

    public static int GetScore() {
        return instance.score;
    }
}
