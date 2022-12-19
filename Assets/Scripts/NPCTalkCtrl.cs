using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCTalkCtrl : MonoBehaviour
{
    public GameObject talk;
    GameObject gameManager;
    GameManager gameMng;
    int index = 0;

    string[] npc0 = { "���� ã�ƿ��Ŵٴ� �� ����? ��ٸ��� �־����ϴ�.",
        "������ 3 ���� ������� ���� ������ �ҿ��� �̷�� �شٴ� ���� ã�ƿ��� ����� ���Ҵµ�. ���� �ö󰡴� ����Ʈ�� ���̵� ������ ������� �߱��� ���� �� ���� �ƾ��.",
        "���� �⺻���� ���۹����� �˷� �帱�Կ�.",
        "�����¿� �̵��� ���ؼ��� wasd�� ȭ��ǥ�� ���� �ּ���. �������� �ݰų� ��ȭ�� �ϰ� ���� ���� P�� �����ø� �˴ϴ�. ",
        "�׸��� �⺻ ������ �Ͻ� ���� ������ Ctrl�� ���� �ּ���. Ư�� ������ sp�� 30 �����ϸ�, q�� ���� ����Ͻ� �� �־��.",
        "����Ʈ�� ���� ������ ������ ����� �ִ� â�� �о� ���ô� �� ���� �ſ���!",
        "ù ��° ����Ʈ�� �����Ͻ� ���� �� ���� ��Ż �� ���� ���� Ÿ�� ���ø� �ȴ�ϴ�.",
        "�׷� ����� ���ϴ�!"};

    string[] quest1 =
    {
        "������ �ɵ��� ���� ����ġ�� ���� ������ ��� ���̳���?",
        "�ٸ� �����ó�� ���ļ� ���ƿñ� �� �����ߴµ�, ����̶�� �� ����Ʈ���� ��� �Ϸ��ؼ� �ҿ��� �̷� �� �������� �𸣰ڳ׿�.",
        "���� ���� ����Ʈ�� �˷� �帱�Կ�. ���� ����Ʈ���� �� ������ �����ϼž� �ؿ�!"
    };
    string[] quest2 =
    {
        "�̰ͱ��� �س��� ���� ���� �������.",
        "�ٸ� ������� �� ���� ������ ���� �����ŵ��. �Ʊ� �� ����Ʈ�� �������� ���� �������� Į�� ���̽���?",
        "���������� Ȱ���� ������ �ҿ��� �̷� �� �ִٰ� �ؿ�.",
        "�� ���Ϳ� �ο��� ù ��° ��Ż�� �̵��ϸ�, ���� ��ٸ��� ���� �ſ���.",
        "Ȱ�� �� ���� ��Ű�� �־��."
    };

    string[] quest3 =
    {
        "�����մϴ�!!"
    };

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        gameMng = gameManager.GetComponent<GameManager>();  
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onClick()
    {
        switch (gameMng.stage)
        {
            case Stage.NPC0:
                if (index <= npc0.Length - 1)
                {
                    talk.GetComponent<Text>().text = npc0[index];
                    index++;
                }
                else if (index == npc0.Length)
                {
                    gameMng.stage = Stage.QUEST1;
                    index = 0;
                    gameMng.initialSet();
                    gameObject.SetActive(false);
                    talk.GetComponent<Text>().text = "zzz... zzz... zzz...";
                }
                break;
            case Stage.QUEST1:
                if (index <= npc0.Length - 1)
                {
                    talk.GetComponent<Text>().text = npc0[index];
                    index++;
                }
                else if (index == npc0.Length)
                {
                    index = 0;
                    gameObject.SetActive(false);
                    talk.GetComponent<Text>().text = "zzz... zzz... zzz...";
                }
                break;
            case Stage.QUEST1_s:
                if (index <= quest1.Length - 1)
                {
                    talk.GetComponent<Text>().text = quest1[index];
                    index++;
                }
                else if (index == quest1.Length)
                {
                    gameMng.questItem = 0;
                    gameMng.stage = Stage.QUEST2;
                    index = 0;
                    gameMng.initialSet();
                    gameObject.SetActive(false);
                    talk.GetComponent<Text>().text = "zzz... zzz... zzz...";
                }
                break;
            case Stage.QUEST2:
                if (index <= quest1.Length - 1)
                {
                    talk.GetComponent<Text>().text = quest1[index];
                    index++;
                }
                else if (index == quest1.Length)
                {
                    index = 0;
                    gameObject.SetActive(false);
                    talk.GetComponent<Text>().text = "zzz... zzz... zzz...";
                }
                break;
            case Stage.QUEST2_s:
                if (index <= quest2.Length - 1)
                {
                    talk.GetComponent<Text>().text = quest2[index];
                    index++;
                }
                else if (index == quest2.Length)
                {
                    gameMng.stage = Stage.QUEST3;
                    gameMng.questItem = 0;
                    index = 0;
                    gameMng.initialSet();
                    gameObject.SetActive(false);
                    talk.GetComponent<Text>().text = "zzz... zzz... zzz...";
                }
                break;
            case Stage.QUEST3:
                if (index <= quest2.Length - 1)
                {
                    talk.GetComponent<Text>().text = quest2[index];
                    index++;
                }
                else if (index == quest2.Length)
                {
                    index = 0;
                    gameObject.SetActive(false);
                    talk.GetComponent<Text>().text = "zzz... zzz... zzz...";
                }
                break;
            case Stage.QUEST3_s:
                if(index <= quest2.Length - 1)
                {
                    talk.GetComponent<Text>().text = quest2[index];
                    index++;
                }
                else if (index == quest2.Length)
                {
                    index = 0;
                    gameObject.SetActive(false);
                    talk.GetComponent<Text>().text = "zzz... zzz... zzz...";
                }
                break;
        }
    }
}
