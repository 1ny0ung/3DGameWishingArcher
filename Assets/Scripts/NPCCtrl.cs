using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCCtrl : MonoBehaviour
{
    Transform playerTr;
    public GameObject NPCPanel;
    // Start is called before the first frame update
    void Start()
    {
        playerTr = GameObject.FindGameObjectWithTag("PLAYER").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if ((playerTr.position - transform.position).magnitude <= 5.0f)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                NPCPanel.SetActive(true);
            }
        }
    }
}
