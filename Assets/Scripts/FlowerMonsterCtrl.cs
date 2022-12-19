using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* DragonCtrl과 세부 설정 값 외에는 코드 대부분 유사 */
public class FlowerMonsterCtrl : MonoBehaviour
{
    enum state
    {
        DIE,
        IDLE,
        ATTACK
    }

    state monsterState;
    public Transform shotPos;
    public GameObject fire;
    public GameObject reward1;
    public GameObject reward2;
    public GameObject bloodEffect;
    public GameObject hpBarPrefab;

    GameObject hpBar;
    public Vector3 hpBarOffset = new Vector3(0, 5f, 0);
    private Canvas uiCanvas;
    private Image hpBarImage;

    public float damage = 10.0f;
    Transform playerTr;
    Transform enemyTr;
    float nextFire = 0.0f;
    float fireRate = 0.5f;
    float damping = 10.0f;

    float hp = 50.0f;
    // Start is called before the first frame update
    void Start()
    {
        playerTr = GameObject.FindGameObjectWithTag("PLAYER").transform;
        enemyTr = GetComponent<Transform>();
        monsterState = state.IDLE;
        setHpBar();
        displayHpBar();
    }

    // Update is called once per frame
    void Update()
    {
        switch (monsterState)
        {
            case state.DIE:
                getReward();
                Destroy(gameObject);
                break;
            case state.IDLE:
                break;
            case state.ATTACK:
                Quaternion rot = Quaternion.LookRotation(playerTr.position - enemyTr.position);
                enemyTr.rotation = Quaternion.Slerp(enemyTr.rotation, rot, Time.deltaTime * damping);
                if (Time.time >= nextFire)
                {
                    Fire();
                    nextFire = Time.time + fireRate + Random.Range(0.5f, 0.9f);
                }
                break;
        }
        if (hp <= 0.0f)
        {
            monsterState = state.DIE;
        }
        else if ((playerTr.position - enemyTr.position).magnitude <= 15.0f)
        {
            monsterState = state.ATTACK;
        }
        else
        {
            monsterState = state.IDLE;
        }
    }

    private void OnCollisionEnter(Collision coll)
    {
        if(coll.collider.tag == "ARROW")
        {
            Destroy(coll.gameObject);
            hp -= coll.gameObject.GetComponent<ArrowCtrl>().damage;
            displayHpBar();
            GameObject blood = Instantiate(bloodEffect, enemyTr.position + new Vector3(0, 4, 0), enemyTr.rotation);
            Destroy(blood, 0.8f);

            if (hp <= 0.0f && hpBar)
            {
                hpBarImage.GetComponentsInParent<Image>()[1].color = Color.clear;
                GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
                gm.killCount++;
                Destroy(hpBar);
                Destroy(gameObject, 5);
            }
        }
    }

    void Fire()
    {
        GameObject _fire = Instantiate(fire, shotPos.position, shotPos.rotation);
        _fire.GetComponent<damageCtrl>().damage = this.damage;
        Destroy(_fire, 3.0f);
    }

    void getReward()
    {
        float chance = Random.Range(0.0f, 10.0f);
        if (chance <= 2.0f)
        {
            Instantiate(reward1, transform.position, transform.rotation);
        }
        else
        {
            Instantiate(reward2, transform.position, transform.rotation);
        }
    }

    void setHpBar()
    {
        uiCanvas = GameObject.Find("UICanvas").GetComponent<Canvas>();
        hpBar = Instantiate(hpBarPrefab, uiCanvas.transform);
        hpBarImage = hpBar.GetComponentsInChildren<Image>()[1];
        EnemyHpBar bar = hpBar.GetComponent<EnemyHpBar>();
        bar.targetTr = gameObject.transform;
        bar.offset = hpBarOffset;
    }

    void displayHpBar()
    {
        float ratio = hp / 50.0f;
        hpBarImage.fillAmount = ratio;
    }
}
