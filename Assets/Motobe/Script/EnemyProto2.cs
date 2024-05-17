using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProto2 : MonoBehaviour
{
    Rigidbody2D rb;
    int rota;
    public GameObject EnemySkin;
    GameObject PlayerObj;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        PlayerObj = GameObject.FindGameObjectWithTag("Player");
        rota =1;
    }

    // Update is called once per frame
    void Update()
    {
        if (!PlayerMove.PlayerDead)
        {
            EnemySkin.transform.Rotate(0, 0, 750 * rota * Time.deltaTime);

            if (Vector2.Distance(transform.position, new Vector2(PlayerObj.transform.position.x, PlayerObj.transform.position.y)) < 0.1f)
            {

            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position,
                    new Vector2(PlayerObj.transform.position.x, PlayerObj.transform.position.y), 5 * Time.deltaTime);
            }
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Drop"))
        {
            Destroy(this.gameObject);
        }

        if (other.gameObject.CompareTag("DestroyObj"))
        {
            Destroy(this.gameObject);
        }

        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }
}
