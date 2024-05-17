using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxBoss : MonoBehaviour
{
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Rotate(0, 0, 100*Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //Ground‚É‚Ó‚ê‚½‚Æ‚«
        if (other.gameObject.CompareTag("Ground"))
        {
            rb.velocity = new Vector3(0, 13, 0);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        //Ground‚©‚ç—£‚ê‚½
        if (other.gameObject.CompareTag("Ground"))
        {
        }
    }

    public void BossMove()
    {
        var sequence = DOTween.Sequence();
    }
}
