using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraMove : MonoBehaviour
{
    public static bool dropSway;
    public static bool damageSway;
    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        damageSway = false;
        dropSway = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (dropSway == true)
        {
            Drop();
            dropSway = false;
        }
        if (damageSway == true) 
        {
            Damage();
            damageSway = false;
        }
    }
    public void Drop()
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
        sequence.Append(this.transform.DOMoveY(Player.transform.position.y + 0f + 2f, 0.025f));
        sequence.Join  (this.transform.DOMoveX(Player.transform.position.x + 0f, 0.025f));
        sequence.AppendCallback(() => normal());
    }

    public void normal()
    {
        var sequence = DOTween.Sequence();
        sequence.Append(this.transform.DOMoveY(Player.transform.position.y + 0f + 2f, 0f));
        sequence.Join(this.transform.DOMoveX(Player.transform.position.x + 0f, 0f));
    }

    public void Damage()
    {
        var sequence = DOTween.Sequence();

        sequence.Append(this.transform.DOMoveX(Player.transform.position.x - 1, 0.025f));
        sequence.Append(this.transform.DOMoveX(Player.transform.position.x +0.75f, 0.025f));
        sequence.Append(this.transform.DOMoveX(Player.transform.position.x - 0.5f, 0.025f));
        sequence.Append(this.transform.DOMoveX(Player.transform.position.x + 0.25f, 0.025f));
        sequence.Append(this.transform.DOMoveX(Player.transform.position.x , 0.025f));
        sequence.AppendCallback(() => normal());
    }
}
