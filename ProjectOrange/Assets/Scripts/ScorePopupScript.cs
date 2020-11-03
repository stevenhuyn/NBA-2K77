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

    // Start is called before the first frame update
    void Start()
    {
        timeAlive = 0;
        tmp = transform.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate() {
        timeAlive++;
        tmp.color = new Color(1, 1, 1, 1.0f - (float) timeAlive / lifeLength);
        tmp.rectTransform.localPosition += new Vector3(0, 1f, 0);
        if (timeAlive >= lifeLength) {
            Destroy(this.gameObject);
        }
    }
}
