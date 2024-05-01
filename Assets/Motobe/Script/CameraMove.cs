using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraMove : MonoBehaviour
{
    public static bool sway;
    // Start is called before the first frame update
    void Start()
    {
        sway = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (sway == true)
        {
            move();
            sway = false;
        }
    }
    public void move()
    {
        var sequence = DOTween.Sequence();

        sequence.Append(this.transform.DOMoveX(5f, 2f));
    }
}
