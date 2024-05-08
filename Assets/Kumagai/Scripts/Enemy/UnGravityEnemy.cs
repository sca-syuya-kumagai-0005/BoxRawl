using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class UnGravityEnemy : MonoBehaviour
{
    //�d�͂��������A�v���C���[�Ɍ������Ĉړ�����G�l�~�[�ł�
    // Start is called before the first frame update
    [SerializeField] private GameObject m_target;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetVec();
    }

    void GetVec()
    {
        Vector3 target=m_target.transform.position-this.gameObject.transform.position;
        target=target.normalized;
        this.transform.position += target * Time.deltaTime;
    }
}
