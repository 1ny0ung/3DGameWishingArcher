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

    string[] npc0 = { "오늘 찾아오신다던 분 맞죠? 기다리고 있었습니다.",
        "전에는 3 가지 보상들을 전부 모으면 소원을 이루어 준다는 말에 찾아오는 사람이 많았는데. 점점 올라가는 퀘스트의 난이도 때문에 사람들의 발길이 끊긴 지 오래 됐어요.",
        "먼저 기본적인 조작법부터 알려 드릴게요.",
        "상하좌우 이동을 위해서는 wasd나 화살표를 눌러 주세요. 아이템을 줍거나 대화를 하고 싶을 때는 P를 누르시면 됩니다. ",
        "그리고 기본 공격을 하실 때는 좌측의 Ctrl을 눌러 주세요. 특수 공격은 sp를 30 차감하며, q를 눌러 사용하실 수 있어요.",
        "퀘스트에 대한 설명은 좌측에 띄워져 있는 창을 읽어 보시는 게 빠를 거예요!",
        "첫 번째 퀘스트를 수행하실 곳은 두 개의 포탈 중 작은 쪽을 타고 가시면 된답니다.",
        "그럼 행운을 빕니다!"};

    string[] quest1 =
    {
        "정말로 꽃들을 전부 물리치고 꽃의 수정을 모아 오셨나요?",
        "다른 사람들처럼 다쳐서 돌아올까 봐 걱정했는데, 당신이라면 이 퀘스트들을 모두 완료해서 소원을 이룰 수 있을지도 모르겠네요.",
        "이제 다음 퀘스트를 알려 드릴게요. 저번 퀘스트보다 더 각별히 주의하셔야 해요!"
    };
    string[] quest2 =
    {
        "이것까지 해내실 줄은 정말 몰랐어요.",
        "다른 사람들은 한 번도 성공한 적이 없었거든요. 아까 두 퀘스트의 보상으로 얻은 마법서와 칼이 보이시죠?",
        "마지막으로 활까지 있으면 소원을 이룰 수 있다고 해요.",
        "꽃 몬스터와 싸웠던 첫 번째 포탈로 이동하면, 용이 기다리고 있을 거예요.",
        "활은 그 용이 지키고 있어요."
    };

    string[] quest3 =
    {
        "축하합니다!!"
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
