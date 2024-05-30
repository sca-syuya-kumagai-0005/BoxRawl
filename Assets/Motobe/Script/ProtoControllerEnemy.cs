using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtoControllerEnemy : MonoBehaviour
{
    float spawnTimer;
    private float speed = 2;
    // Start is called before the first frame update
    void Start()
    {
        SpawnDraw3();
        SpawnDraw3();
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer > 3)
        {
            int random = Random.Range(0, 10);
            if (random < 2)
            {
                SpawnDraw();
            }
            else
            if (random < 5)
            {
                SpawnDraw2();
            }
            else
            if (random < 8)
            {
                SpawnDraw3();
            }
            else
            if (random < 10)
            {
                SpawnDraw4();
            }
            spawnTimer = 0;
        }
    }
    private void ObjectEnemy(float x, float y)
    {
        GameObject Enemy_prefab = Resources.Load<GameObject>("EnemyProto");
        GameObject Enemy = Instantiate(Enemy_prefab, new Vector3(x, y, 0), Quaternion.identity);
        return;
    }
    private void ObjectEnemy1(float x, float y)
    {
        GameObject Enemy_prefab = Resources.Load<GameObject>("EnemyProto1");
        GameObject Enemy = Instantiate(Enemy_prefab, new Vector3(x, y, 0), Quaternion.identity);
        return;
    }
    private void ObjectEnemy2(float x, float y)
    {
        GameObject Enemy_prefab = Resources.Load<GameObject>("EnemyProto2");
        GameObject Enemy = Instantiate(Enemy_prefab, new Vector3(x, y, 0), Quaternion.identity);
        return;
    }
    private void ObjectEnemy3(float x, float y)
    {
        GameObject Enemy_prefab = Resources.Load<GameObject>("EnemyProto");
        GameObject Enemy = Instantiate(Enemy_prefab, new Vector3(x, y, 0), Quaternion.identity);
        return;
    }
    void SpawnDraw()
    {
        int random = Random.Range(0, 4);
        if (random == 0)
        {
            ObjectEnemy(40, 13);
        }
        if (random == 1)
        {
            ObjectEnemy1(40, 13);
        }
        if (random == 2)
        {
            ObjectEnemy2(40, 30);
        }
        if (random == 3)
        {
            ObjectEnemy3(40, 13);
        }

    }
    void SpawnDraw2()
    {
        int random = Random.Range(0, 4);
        if (random == 0)
        {
            ObjectEnemy(-40, 22);
        }
        if (random == 1)
        {
            ObjectEnemy1(-40, 22);
        }
        if (random == 2)
        {
            ObjectEnemy2(-40, 30);
        }
        if (random == 3)
        {
            ObjectEnemy3(-40, 22);
        }
    }
    void SpawnDraw3()
    {
        int random = Random.Range(0, 4);
        if (random == 0)
        {
            ObjectEnemy(0, 22);
        }
        if (random == 1)
        {
            ObjectEnemy1(0, 22);
        }
        if (random == 2)
        {
            ObjectEnemy2(0, 30);
        }
        if (random == 3)
        {
            ObjectEnemy3(0, 22);
        }
    }
    void SpawnDraw4()
    {
        SpawnDraw();
        SpawnDraw2();
        SpawnDraw3();
        ObjectEnemy2(20, 30);
        ObjectEnemy2(-20, 30);
    }
}
