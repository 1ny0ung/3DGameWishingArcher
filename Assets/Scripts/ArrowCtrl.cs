using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowCtrl : MonoBehaviour
{
    // ȭ���� ������
    public float damage = 20.0f;
    // ȭ���� ���ư��� �ӵ�
    public float speed = 1000.0f;
    // Start is called before the first frame update
    void Start()
    {
        // ȭ�� �߻�
        GetComponent<Rigidbody>().AddForce(transform.up * speed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
