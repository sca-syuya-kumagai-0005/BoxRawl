using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    private int count;
    private Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private bool OnGround;
    private bool moveVec;//�ǂ���̕����ɓ����������߂�t���O�Bfalse�Ȃ獶�Atrue�Ȃ�E

    void Start()
    {
        count = 0;
        rb = GetComponent<Rigidbody2D>();
        moveVec = false;
        OnGround = false;
    }

    // Update is called once per frame
    void Update()
    {
        EnemyMove();
    }

    void EnemyMove()
    {
        Vector3 pos = this.transform.position;
        float x = pos.x;
        if (count == 3)
        {
            moveVec = !moveVec;
            count = 0;
        }
        if (moveVec)
        {
            x += speed * Time.deltaTime;
        }
        if (!moveVec)
        {
            x -= speed * Time.deltaTime;
        }
        this.transform.position = new Vector3(x, pos.y, pos.z);
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Collider2D>().CompareTag("Ground"))
        {
            count++;
            //this.transform.position = new Vector3(this.transform.position.x, collision.collider.transform.position.y + 1, 0);
            rb.AddForce(new Vector3(0, 100, 0));
            OnGround = true;
            Debug.Log("A");
        }
    }

}
