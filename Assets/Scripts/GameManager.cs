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
                questTitle.GetComponent<Text>().text = "�ҿ��� �̷�� ���� ������ ����";
                questSummary.GetComponent<Text>().text = "��� ��ó�� �� P�� ���� ����Ʈ�鿡 ���� ������ ��� ������.";
                questDetail.GetComponent<Text>().text = "�������� �ؾ� �� ���� �˷� �� �ſ���.";
                break;

            case Stage.QUEST1:
                questTitle.GetComponent<Text>().text = "����Ʈ 1";
                questSummary.GetComponent<Text>().text = "�� ���͸� ����� ���� ���� 5 ���� ��������.";
                questDetail.GetComponent<Text>().text = "����Ʈ ���� ��Ȳ: ���� ����(0 / 7)";
                requirednum = 7;
                break;
            case Stage.QUEST2:
                questTitle.GetComponent<Text>().text = "����Ʈ 2";
                questSummary.GetComponent<Text>().text = "�ذ��� ����� ���� ���� 5 ���� ��������.";
                questDetail.GetComponent<Text>().text = "����Ʈ ���� ��Ȳ: ���� ����(0 / 5)";
                requirednum = 5;
                break;
            case Stage.QUEST3:
                questTitle.GetComponent<Text>().text = "������ ����Ʈ";
                questSummary.GetComponent<Text>().text = "��� �ο� ���� ���� 1 ���� ��������.";
                questDetail.GetComponent<Text>().text = "����Ʈ ���� ��Ȳ: ���� ����(0 / 1)";
                requirednum = 1;
                break;
        }
    }

    public void questSuccess()
    {
        switch (stage)
        {
            case Stage.QUEST1:
                questSummary.GetComponent<Text>().text = "Quest1 ����! ��Ż�� Ÿ�� �ٽ� ������ ���ư�����.";
                break;
            case Stage.QUEST2:
                questSummary.GetComponent<Text>().text = "Quest2 ����! ��Ż�� Ÿ�� �ٽ� ������ ���ư�����.";
                break;
            case Stage.QUEST3:
                questSummary.GetComponent<Text>().text = "Quest3 ����! ��Ż�� Ÿ�� �ٽ� ������ ���ư�����.";
                break;
        }
    }

    public void itemGet()
    {
        switch (stage)
        {
            case Stage.QUEST1:
                questDetail.GetComponent<Text>().text = "����Ʈ ���� ��Ȳ: ���� ����(" + questItem + " / " + requirednum + ")";
                break;
            case Stage.QUEST2:
                questDetail.GetComponent<Text>().text = "����Ʈ ���� ��Ȳ: ���� ����(" + questItem + " / " + requirednum + ")";
                break;
            case Stage.QUEST3:
                questDetail.GetComponent<Text>().text = "����Ʈ ���� ��Ȳ: ���� ����(" + questItem + " / " + requirednum + ")";
                break;
        }
    }

    public void InittoStart()
    {
        SceneManager.LoadScene("StartStage");
    }
}
