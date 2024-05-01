using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraMove : MonoBehaviour
{
    public static bool sway;
    public GameObject Player;
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

        sequence.Append(this.transform.DOMoveY(Player.transform.position.y + -1f + 2f, 0.025f));
        sequence.Join  (this.transform.DOMoveX(Player.transform.position.x - 2, 0.025f));
        sequence.Append(this.transform.DOMoveY(Player.transform.position.y + 1f + 2f, 0.025f));
        sequence.Join  (this.transform.DOMoveX(Player.transform.position.x - 1.5f, 0.025f));
        sequence.Append(this.transform.DOMoveY(Player.transform.position.y + -0.75f + 2f, 0.025f));
        sequence.Join  (this.transform.DOMoveX(Player.transform.position.x + 1f, 0.025f));
        sequence.Append(this.transform.DOMoveY(Player.transform.position.y + 0.5f + 2f, 0.025f));
        sequence.Join  (this.transform.DOMoveX(Player.transform.position.x + 0.5f, 0.025f));
        sequence.Append(this.transform.DOMoveY(Player.transform.position.y + -0.25f + 2f, 0.025f));
        sequence.Join  (this.transform.DOMoveX(Player.transform.position.x - 0.25f, 0.025f));
        sequence.Append(this.transform.DOMoveY(Player.transform.position.y + 0f + 2f, 0.05f));
        sequence.Join  (this.transform.DOMoveX(Player.transform.position.x + 0, 0.025f));
    }
}
