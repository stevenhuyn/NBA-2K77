using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour
{
    public Transform player;
    public TextMeshProUGUI scoreText;

    private int score = 0;

    private int multiplier = 0;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = score.ToString("0");
    }

    public void UpdateScore(int delta)
    {
        StartCoroutine(UpdateScoreSlowly(delta));
    }

    private IEnumerator UpdateScoreSlowly(int delta)
    {
        for (int i = 0; i < delta; i++)
        {
            score++;
            yield return new WaitForSeconds(0.01f);
        }
    }
}
