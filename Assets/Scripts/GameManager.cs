using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum Stage
{
    NPC0,
    QUEST1,
    QUEST1_s,
    QUEST2,
    QUEST2_s,
    QUEST3,
    QUEST3_s
}
public class GameManager : MonoBehaviour
{
    public int killCount = 0;
    GameObject player;
    public Stage stage;
    public int questItem;
    public int requirednum;

    public GameObject questPanel;
    public GameObject questTitle;
    public GameObject questSummary;
    public GameObject questDetail;
    public AudioSource buffSound;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("PLAYER");
        stage = Stage.NPC0;
        questItem = 0;
        initialSet();
    }

    // Update is called once per frame
    void Update()
    {
        if (killCount >= 3)
        {
            buffSound.Play();
            if (player.GetComponent<PlayerCtrl>().sp <= 60)
            {
                player.GetComponent<PlayerCtrl>().sp += 40;
            }
            else
            {
                player.GetComponent<PlayerCtrl>().sp = 100;
            }
            player.GetComponent<PlayerCtrl>().spGetEffect();
            player.GetComponent<PlayerCtrl>().displaySPbar();
            killCount = 0;
        }
    }

    public void initialSet()
    {
        switch (stage)
        {
            case Stage.NPC0:
                questTitle.GetComponent<Text>().text = "소원을 이루기 위한 모험의 시작";
                questSummary.GetComponent<Text>().text = "기사 근처로 가 P를 눌러 퀘스트들에 대한 설명을 들어 보세요.";
                questDetail.GetComponent<Text>().text = "여러분이 해야 할 일을 알려 줄 거예요.";
                break;

            case Stage.QUEST1:
                questTitle.GetComponent<Text>().text = "퀘스트 1";
                questSummary.GetComponent<Text>().text = "꽃 몬스터를 사냥해 꽃의 수정 5 개를 얻으세요.";
                questDetail.GetComponent<Text>().text = "퀘스트 진행 상황: 꽃의 수정(0 / 7)";
                requirednum = 7;
                break;
            case Stage.QUEST2:
                questTitle.GetComponent<Text>().text = "퀘스트 2";
                questSummary.GetComponent<Text>().text = "해골을 사냥해 뼈의 수정 5 개를 얻으세요.";
                questDetail.GetComponent<Text>().text = "퀘스트 진행 상황: 뼈의 수정(0 / 5)";
                requirednum = 5;
                break;
            case Stage.QUEST3:
                questTitle.GetComponent<Text>().text = "마지막 퀘스트";
                questSummary.GetComponent<Text>().text = "용과 싸워 용의 수정 1 개를 얻으세요.";
                questDetail.GetComponent<Text>().text = "퀘스트 진행 상황: 용의 수정(0 / 1)";
                requirednum = 1;
                break;
        }
    }

    public void questSuccess()
    {
        switch (stage)
        {
            case Stage.QUEST1:
                questSummary.GetComponent<Text>().text = "Quest1 성공! 포탈을 타고 다시 마을로 돌아가세요.";
                break;
            case Stage.QUEST2:
                questSummary.GetComponent<Text>().text = "Quest2 성공! 포탈을 타고 다시 마을로 돌아가세요.";
                break;
            case Stage.QUEST3:
                questSummary.GetComponent<Text>().text = "Quest3 성공! 포탈을 타고 다시 마을로 돌아가세요.";
                break;
        }
    }

    public void itemGet()
    {
        switch (stage)
        {
            case Stage.QUEST1:
                questDetail.GetComponent<Text>().text = "퀘스트 진행 상황: 꽃의 수정(" + questItem + " / " + requirednum + ")";
                break;
            case Stage.QUEST2:
                questDetail.GetComponent<Text>().text = "퀘스트 진행 상황: 뼈의 수정(" + questItem + " / " + requirednum + ")";
                break;
            case Stage.QUEST3:
                questDetail.GetComponent<Text>().text = "퀘스트 진행 상황: 용의 수정(" + questItem + " / " + requirednum + ")";
                break;
        }
    }

    public void InittoStart()
    {
        SceneManager.LoadScene("StartStage");
    }
}
