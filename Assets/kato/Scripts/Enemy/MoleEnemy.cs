using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleEnemy : MonoBehaviour
{
    [SerializeField] ParticleSystem smokePrefab;
    public ParticleSystem smoke;

    public Rigidbody2D rg;
    float timer;

    Vector3 smokePos;
    Vector3 GroundPos;

    bool onGroun;

    int addPos_X;

    private void Start()
    {
        rg = this.gameObject.GetComponent<Rigidbody2D>();
        timer = 0;

        onGroun = false;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (smoke != null)
        {
            Destroy(smoke, 3.0f);
        }

        if (onGroun)
        {
            this.gameObject.transform.position = GroundPos;
        }

        if (timer > 4 && onGroun)
        {
            onGroun = false;
            rg.velocity = new Vector2(0, 9.5f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Ground")
        {
            rg.gravityScale = 0;
            addPos_X = Random.RandomRange(-10, 10);
            timer = 0;
            smokePos = new Vector3(this.gameObject.transform.position.x + addPos_X, this.gameObject.transform.position.y - 0.5f, this.gameObject.transform.position.z);
            this.gameObject.transform.DOMoveY(this.transform.position.y - 1, 1.0f).OnComplete(OnGround);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        rg.gravityScale = 1;
    }

    private void OnGround()
    {
        GroundPos = new Vector3(this.gameObject.transform.position.x + addPos_X, this.gameObject.transform.position.y, 0);
        smoke = Instantiate(smokePrefab, smokePos, Quaternion.identity);
        onGroun = true;
    }
}
