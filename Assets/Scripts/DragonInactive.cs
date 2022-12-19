using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonInactive : MonoBehaviour
{
    public GameObject dragon;
    GameObject gameManager;
    GameManager gameMng;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        gameMng = gameManager.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // ���� QUEST3 ���� �ܰ��� ��� -> �� ĳ���� Ȱ��ȭ
        // �׷��� ���� ��� -> ��Ȱ��ȭ
        if (gameMng.stage == Stage.QUEST3)
        {
            dragon.SetActive(true);
        }
        else
        {
            dragon.SetActive(false);
        }
    }
}
