using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowCtrl : MonoBehaviour
{
    // 화살의 데미지
    public float damage = 20.0f;
    // 화살의 날아가는 속도
    public float speed = 1000.0f;
    // Start is called before the first frame update
    void Start()
    {
        // 화살 발사
        GetComponent<Rigidbody>().AddForce(transform.up * speed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
