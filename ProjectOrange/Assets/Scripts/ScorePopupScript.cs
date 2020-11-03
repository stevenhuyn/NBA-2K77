using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Coffee.UIExtensions;
using TMPro;

public class ScorePopupScript : MonoBehaviour
{

    public int lifeLength;

    private int timeAlive;

    private TextMeshProUGUI tmp;

    public Transform score;

    private float velocity;

    public float startingVelocity = 0;

    public float acceleration = 0;

    public float jerk = 0;

    public int startFadeFrame;

    // Start is called before the first frame update
    void Start()
    {
        timeAlive = 0;
        tmp = transform.GetComponent<TextMeshProUGUI>();
        velocity = startingVelocity;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate() {
        velocity += acceleration;
        acceleration += jerk;
        timeAlive++;

        tmp.rectTransform.localPosition = Vector3.MoveTowards(tmp.rectTransform.localPosition, score.localPosition, velocity);

        if (timeAlive >= lifeLength) {
            Destroy(this.gameObject);
        }

        if (timeAlive >= startFadeFrame) {
            tmp.color = new Color(1, 1, 1, 1.0f - (float) (timeAlive - startFadeFrame) / (lifeLength - startFadeFrame));
        }
    }
}
