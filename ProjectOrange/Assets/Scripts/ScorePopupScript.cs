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

    // Start is called before the first frame update
    void Start()
    {
        timeAlive = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate() {
        timeAlive++;
        transform.GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, 1.0f - (float) timeAlive / lifeLength);
        if (timeAlive >= lifeLength) {
            Destroy(this.gameObject);
        }
    }
}
