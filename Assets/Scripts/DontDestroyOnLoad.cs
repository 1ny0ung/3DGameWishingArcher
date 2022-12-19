using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    private static DontDestroyOnLoad instance = null;
    // Start is called before the first frame update

    void Awake()
    {
        // ��ũ��Ʈ�� ������ ���� ������Ʈ�� ������ �� ������ �� 
        DontDestroyOnLoad(transform.gameObject);
        if (instance)
        {
            DestroyImmediate(this.gameObject);
            return;
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
