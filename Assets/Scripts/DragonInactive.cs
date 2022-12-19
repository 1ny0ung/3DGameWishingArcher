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
        // 현재 QUEST3 수행 단계인 경우 -> 용 캐릭터 활성화
        // 그렇지 않은 경우 -> 비활성화
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
