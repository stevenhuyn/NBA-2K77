using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MessagePopup : MonoBehaviour
{
    public Vector3 start;

    public Vector3 end;

    private TextMeshProUGUI tmp;


    public int lifeLength;

    private int timeAlive;
    public int startFadeFrame;
    // Start is called before the first frame update
    void Start()
    {
        tmp = transform.GetComponent<TextMeshProUGUI>();
        tmp.rectTransform.position = start;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        tmp.rectTransform.position = Vector3.MoveTowards(tmp.rectTransform.position, end, 10);
    }
}
