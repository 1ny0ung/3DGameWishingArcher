using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    private static DontDestroyOnLoad instance = null;
    // Start is called before the first frame update

    void Awake()
    {
        // 스크립트를 적용한 게임 오브젝트를 삭제할 수 없도록 함 
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
