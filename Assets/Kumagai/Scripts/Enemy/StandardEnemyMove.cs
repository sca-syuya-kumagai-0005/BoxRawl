using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//壁、もしくはプレイヤーに衝突すると移動方向を変更するエネミーのスクリプトです
public class StandardEnemyMove : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    private bool moveChange;
    public float addMove;
    // Start is called before the first frame update
    void Start()
    {
        moveChange = false;   
    }

    // Update is called once per frame
    void Update()
    {
        StandardEnemyMover();
    }

    private void StandardEnemyMover()
    {
        enemy.transform.position +=new Vector3(addMove * Time.deltaTime,0,0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Player"))
        {
            addMove *= -1;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
     if(collision.gameObject.CompareTag("Wall"))
        {
            addMove *= -1;
        }
    }
}
