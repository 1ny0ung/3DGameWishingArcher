using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragonCtrl : MonoBehaviour
{
    // �� ĳ������ �� ���� ������ enum
    public enum state
    {
        DIE,
        IDLE,
        TRACE,
        ATTACK
    }
    // �� ĳ������ ���¸� ������ ����
    public state monsterState;
    // ���̾� �� �߻��� ����
    public Transform firePos;
    // ���̾ 1 ~ 3 ������Ʈ
    public GameObject fire1;
    public GameObject fire2;
    public GameObject fire3;
    // �� ĳ���Ͱ� ������ ����� ���� ������Ʈ
    public GameObject reward;
    // �� ĳ���Ͱ� ���� ���� �� ������ �� ������Ʈ
    public GameObject bloodEffect;
    // �� ĳ������ hpBar ������
    public GameObject hpBarPrefab;
    // �������� ������ ������ hpBar ������Ʈ
    GameObject hpBar;
    // hpBar�� ������
    public Vector3 hpBarOffset = new Vector3(0, 9.5f, 0);
    // hpBar�� ��ġ�� uiCanvas
    private Canvas uiCanvas;
    // hpBar�� fill Image
    private Image hpBarImage;

    // �� ĳ���Ͱ� �ִ� damage 
    public float damage = 10.0f;
    // �÷��̾� ������Ʈ�� Transform
    Transform playerTr;
    // �� ĳ���� �ڽ��� Transform
    Transform enemyTr;
    // �� ĳ���� �ڽ��� MoveAgent ��ũ��Ʈ
    MoveAgent moveAgent;
    // ���� ���� �ð��� �����ϴ� ����
    float nextFire = 0.0f;
    // ���� �ֱ⸦ �����ϴ� ����
    float fireRate = 0.5f;
    float damping = 10.0f;
    // ���� ���� ���� ������ �����ϴ� ����
    float attackDist = 20.0f;
    // ���� ���� ���� ������ �����ϴ� ����
    float traceDist = 40.0f;
    // �� ĳ������ ��� ���� ����
    public bool isDie = false;
    WaitForSeconds ws;
    // �÷��̾���� �Ÿ� ������ ����
    float dist;

    float hp = 100.0f;
    // Start is called before the first frame update
    void Start()
    {
        // playerTr�� �÷��̾��� Transform ������ ����
        playerTr = GameObject.FindGameObjectWithTag("PLAYER").transform;
        // enemyTr�� �� ĳ������ Transform ������ ����
        enemyTr = GetComponent<Transform>();
        // �� ĳ���� �ڽ��� ���¸� IDLE�� �����ϰ� ����
        monsterState = state.IDLE;
        // moveAgent ������ �ڱ� �ڽſ��� ����� MoveAgent ��ũ��Ʈ ������ ����
        moveAgent = GetComponent<MoveAgent>();
        // �ڱ� �ڽ��� HP Bar�� ������ �������� �Լ� ����
        setHpBar();
        // HP Bar�� �ڽ��� HP �����ϴ� �Լ� ����
        displayHpBar();
        ws = new WaitForSeconds(1.0f);
    }

    private void OnEnable()
    {
        StartCoroutine(Action());
    }

    // Update is called once per frame
    void Update()
    {
        // dist�� �ڱ� �ڽŰ� �÷��̾� ������ �Ÿ� ����
        dist = Vector3.Distance(playerTr.position, enemyTr.position);
        // ��� ó�� 
        if (hp <= 0.0f)
        {
            monsterState = state.DIE;
        }
        // ���� ���� ���� ���� ��� �������� ���� ��ȯ
        else if (dist <= attackDist)
            monsterState = state.ATTACK;
        // ���� ���� ���� ���� ��� �������� ���� ��ȯ
        else if (dist <= traceDist)
            monsterState = state.TRACE;
        // �̿��� ��쿡�� IDLE ���� ����
        else
            monsterState = state.IDLE;

        // ���� ���� ������ ���
        if (monsterState == state.ATTACK)
        {
            // �÷��̾�� �ڽ� ������ Quaternion ���
            Quaternion rot = Quaternion.LookRotation(playerTr.position - enemyTr.position);
            // rot �� ������ �÷��̾� ������ ���� Ʋ���� ����
            enemyTr.rotation = Quaternion.Slerp(enemyTr.rotation, rot, Time.deltaTime * damping);
        }
    }

    private void OnCollisionEnter(Collision coll)
    {
        // �÷��̾ �� Arrow�� �浹�ϴ� ���
        if (coll.collider.tag == "ARROW")
        {
            // ȭ�� ������Ʈ ����
            Destroy(coll.gameObject);
            // �� ĳ������ hp arrow�� �ִ� damage��ŭ ����
            hp -= coll.gameObject.GetComponent<ArrowCtrl>().damage;
            // HpBar�� �� ĳ������ hp ����
            displayHpBar();
            // blood ������Ʈ ���� 
            GameObject blood = Instantiate(bloodEffect, enemyTr.position + new Vector3(0, 4, 0), enemyTr.rotation);
            // 0.8�� �� blood ������Ʈ ����
            Destroy(blood, 0.8f);

            // ���� ���� ���� hp�� 0�� �Ǵ� ���
            if (hp <= 0.0f && hpBar)
            {
                // hpBar�� ĥ���� �κ��� ��� �����ϰ� ó��
                hpBarImage.GetComponentsInParent<Image>()[1].color = Color.clear;
                // GameManager ���� ��
                GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
                // �÷��̾��� sp ����ó���� ���� killCount (�÷��̾ �� ĳ���͸� �󸶳� �����ߴ��� ����) 1 ����
                gm.killCount++;
                // hpBar ����
                Destroy(hpBar);
                // �� ĳ���� ������Ʈ ����
                Destroy(gameObject, 5);
            }
        }
    }

    // ���� �Լ�
    void Fire()
    {
        // fire1~3 ����
        GameObject _fire1 = Instantiate(fire1, firePos.position, firePos.rotation);
        GameObject _fire2 = Instantiate(fire2, firePos.position, firePos.rotation);
        GameObject _fire3 = Instantiate(fire3, firePos.position, firePos.rotation);
        // fire1~3�� damage�� �� ĳ���Ͱ� �� �� �ִ� damage�� ������ damage ���� �� ����
        _fire1.GetComponent<damageCtrl>().damage = this.damage;
        _fire2.GetComponent<damageCtrl>().damage = this.damage;
        _fire3.GetComponent<damageCtrl>().damage = this.damage;
        // 3�� �� fire1~3 ����
        Destroy(_fire1, 3.0f);
        Destroy(_fire2, 3.0f);
        Destroy(_fire3, 3.0f);
    }

    void getReward()
    {
        // �� ĳ���� �Ҹ� �� ���� ������Ʈ ��Ÿ������ ��
        Instantiate(reward, transform.position, transform.rotation);
    }

    // �ڱ� �ڽ��� hpBar �����ϴ� �Լ�
    void setHpBar()
    {
        // ���� uiCanvas ������
        uiCanvas = GameObject.Find("UICanvas").GetComponent<Canvas>();
        // hpBarPrefab�� ������ uiCanvas�� ��ġ�� hpBar ����
        hpBar = Instantiate(hpBarPrefab, uiCanvas.transform);
        // hpBarImage�� ������ hpBar�� fill Image ����
        hpBarImage = hpBar.GetComponentsInChildren<Image>()[1];
        // ������ hpBar ������Ʈ�� EnemyHpBar ��ũ��Ʈ ������ ����
        EnemyHpBar bar = hpBar.GetComponent<EnemyHpBar>();
        // bar�� ���� Ÿ���� ���� �� ������Ʈ�� ����
        bar.targetTr = gameObject.transform;
        // bar�� �������� hpBarOffset ������ ����
        bar.offset = hpBarOffset;
    }

    // ���� ü���� hpBar�� hpBarImage�� ä���� ������ �ݿ�
    void displayHpBar()
    {
        float ratio = hp / 50.0f;
        hpBarImage.fillAmount = ratio;
    }

    IEnumerator Action()
    {
        // �� ĳ���Ͱ� ������� ���� ���
        while (!isDie)
        {
            yield return ws;
            switch (monsterState)
            {
                // IDLE �� ���� ����
                case state.IDLE:
                    moveAgent.Stop();
                    break;
                // ���� ������ ���� moveAgent ��ũ��Ʈ�� ���� player�� Transform ����
                case state.TRACE:
                    moveAgent.SetTraceTarget(playerTr.position);
                    break;
                // ���� ������ ��
                case state.ATTACK:
                    // ����
                    moveAgent.Stop();
                    // nextFire �ð��� �Ѿ�� �Ǹ� �����Լ� Fire ���� / nextFire�� ���� �ð� + 0.5~0.9�ʸ� ���� ����
                    if (Time.time >= nextFire)
                    {
                        Fire();
                        nextFire = Time.time + fireRate + Random.Range(0.5f, 0.9f);
                    }
                    break;
                    // ��� ������ ��
                case state.DIE:
                    // ����
                    moveAgent.Stop();
                    // reward ������Ʈ ����
                    getReward();
                    // ���� �� ������Ʈ ����
                    Destroy(gameObject);
                    break;
            }
        }
    }
}
