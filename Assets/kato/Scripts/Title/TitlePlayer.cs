using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitlePlayer : MonoBehaviour
{
    public static bool isJump;
    [SerializeField] GameObject EnemyObj;

    Rigidbody2D rg;
    int moveNum;
    // Start is called before the first frame update
    void Start()
    {
        rg = this.gameObject.GetComponent<Rigidbody2D>();
        moveNum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (EnemyObj.transform.position.x - this.gameObject.transform.position.x < 2 && !isJump)
        {
            rg.velocity = new Vector2(0, 8.0f) * 1;
            isJump = true;
            moveNum = Random.Range(0, 2);

            if (moveNum == 1)
            {
                StartCoroutine(playerAttack());
            }
        }
    }

    IEnumerator playerAttack()
    {
        yield return new WaitForSeconds(0.4f);
        rg.gravityScale = 20.0f;

        yield return new WaitForSeconds(0.5f);
        rg.gravityScale = 1.0f;
    }
}
