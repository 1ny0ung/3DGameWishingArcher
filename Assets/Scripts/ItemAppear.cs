using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAppear : MonoBehaviour
{
    GameObject gamemanager;
    GameManager gameMng;
    public GameObject item1;
    public GameObject item2;
    public GameObject item3;
    public GameObject light;
    public GameObject gameClear; 

    // Start is called before the first frame update
    void Start()
    {
        gamemanager = GameObject.Find("GameManager");
        gameMng = gamemanager.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (gameMng.stage)
        {
            case Stage.QUEST1_s:
                item1.SetActive(true);
                break;
            case Stage.QUEST2:
                item1.SetActive(true);
                break;
            case Stage.QUEST2_s:
                item1.SetActive(true);
                item2.SetActive(true);
                break;
            case Stage.QUEST3:
                item1.SetActive(true);
                item2.SetActive(true);
                break;
            case Stage.QUEST3_s:
                item1.SetActive(true);
                item2.SetActive(true); 
                item3.SetActive(true);
                gameClear.SetActive(true);
                Invoke("lightEffect", 4);
                break;
        }
    }

    void lightEffect()
    {
        GameObject tmp = Instantiate(light);
        Destroy(tmp, 2);
    }
}
