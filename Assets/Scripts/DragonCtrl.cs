using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragonCtrl : MonoBehaviour
{
    // 적 캐릭터의 각 상태 정의할 enum
    public enum state
    {
        DIE,
        IDLE,
        TRACE,
        ATTACK
    }
    // 적 캐릭터의 상태를 저장할 변수
    public state monsterState;
    // 파이어 볼 발사할 지점
    public Transform firePos;
    // 파이어볼 1 ~ 3 오브젝트
    public GameObject fire1;
    public GameObject fire2;
    public GameObject fire3;
    // 적 캐릭터가 죽으면 드롭할 보상 오브젝트
    public GameObject reward;
    // 적 캐릭터가 공격 받을 시 생성할 피 오브젝트
    public GameObject bloodEffect;
    // 적 캐릭터의 hpBar 프리팹
    public GameObject hpBarPrefab;
    // 프리팹을 가져와 생성한 hpBar 오브젝트
    GameObject hpBar;
    // hpBar의 오프셋
    public Vector3 hpBarOffset = new Vector3(0, 9.5f, 0);
    // hpBar가 위치할 uiCanvas
    private Canvas uiCanvas;
    // hpBar의 fill Image
    private Image hpBarImage;

    // 적 캐릭터가 주는 damage 
    public float damage = 10.0f;
    // 플레이어 오브젝트의 Transform
    Transform playerTr;
    // 적 캐릭터 자신의 Transform
    Transform enemyTr;
    // 적 캐릭터 자신의 MoveAgent 스크립트
    MoveAgent moveAgent;
    // 다음 공격 시간을 저장하는 변수
    float nextFire = 0.0f;
    // 공격 주기를 저장하는 변수
    float fireRate = 0.5f;
    float damping = 10.0f;
    // 공격 상태 사정 범위를 저장하는 변수
    float attackDist = 20.0f;
    // 추적 상태 사정 범위를 저장하는 변수
    float traceDist = 40.0f;
    // 적 캐릭터의 사망 여부 변수
    public bool isDie = false;
    WaitForSeconds ws;
    // 플레이어와의 거리 저장할 변수
    float dist;

    float hp = 100.0f;
    // Start is called before the first frame update
    void Start()
    {
        // playerTr에 플레이어의 Transform 가져와 지정
        playerTr = GameObject.FindGameObjectWithTag("PLAYER").transform;
        // enemyTr에 적 캐릭터의 Transform 가져와 지정
        enemyTr = GetComponent<Transform>();
        // 적 캐릭터 자신의 상태를 IDLE로 저장하고 시작
        monsterState = state.IDLE;
        // moveAgent 변수에 자기 자신에게 적용된 MoveAgent 스크립트 가져와 저장
        moveAgent = GetComponent<MoveAgent>();
        // 자기 자신의 HP Bar를 생성해 가져오는 함수 실행
        setHpBar();
        // HP Bar에 자신의 HP 적용하는 함수 실행
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
        // dist에 자기 자신과 플레이어 사이의 거리 저장
        dist = Vector3.Distance(playerTr.position, enemyTr.position);
        // 사망 처리 
        if (hp <= 0.0f)
        {
            monsterState = state.DIE;
        }
        // 공격 사정 범위 내인 경우 공격으로 상태 변환
        else if (dist <= attackDist)
            monsterState = state.ATTACK;
        // 추적 사정 범위 내인 경우 추적으로 상태 변환
        else if (dist <= traceDist)
            monsterState = state.TRACE;
        // 이외의 경우에는 IDLE 상태 유지
        else
            monsterState = state.IDLE;

        // 만약 공격 상태인 경우
        if (monsterState == state.ATTACK)
        {
            // 플레이어와 자신 사이의 Quaternion 계산
            Quaternion rot = Quaternion.LookRotation(playerTr.position - enemyTr.position);
            // rot 값 가져와 플레이어 쪽으로 방향 틀도록 적용
            enemyTr.rotation = Quaternion.Slerp(enemyTr.rotation, rot, Time.deltaTime * damping);
        }
    }

    private void OnCollisionEnter(Collision coll)
    {
        // 플레이어가 쏜 Arrow와 충돌하는 경우
        if (coll.collider.tag == "ARROW")
        {
            // 화살 오브젝트 제거
            Destroy(coll.gameObject);
            // 적 캐릭터의 hp arrow가 주는 damage만큼 감소
            hp -= coll.gameObject.GetComponent<ArrowCtrl>().damage;
            // HpBar에 적 캐릭터의 hp 적용
            displayHpBar();
            // blood 오브젝트 생성 
            GameObject blood = Instantiate(bloodEffect, enemyTr.position + new Vector3(0, 4, 0), enemyTr.rotation);
            // 0.8초 후 blood 오브젝트 제거
            Destroy(blood, 0.8f);

            // 만약 공격 당해 hp가 0이 되는 경우
            if (hp <= 0.0f && hpBar)
            {
                // hpBar의 칠해진 부분을 모두 투명하게 처리
                hpBarImage.GetComponentsInParent<Image>()[1].color = Color.clear;
                // GameManager 가져 옴
                GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
                // 플레이어의 sp 버프처리를 위한 killCount (플레이어가 적 캐릭터를 얼마나 제거했는지 저장) 1 증가
                gm.killCount++;
                // hpBar 제거
                Destroy(hpBar);
                // 적 캐릭터 오브젝트 제거
                Destroy(gameObject, 5);
            }
        }
    }

    // 공격 함수
    void Fire()
    {
        // fire1~3 생성
        GameObject _fire1 = Instantiate(fire1, firePos.position, firePos.rotation);
        GameObject _fire2 = Instantiate(fire2, firePos.position, firePos.rotation);
        GameObject _fire3 = Instantiate(fire3, firePos.position, firePos.rotation);
        // fire1~3의 damage로 적 캐릭터가 줄 수 있는 damage를 저장한 damage 변수 값 적용
        _fire1.GetComponent<damageCtrl>().damage = this.damage;
        _fire2.GetComponent<damageCtrl>().damage = this.damage;
        _fire3.GetComponent<damageCtrl>().damage = this.damage;
        // 3초 후 fire1~3 제거
        Destroy(_fire1, 3.0f);
        Destroy(_fire2, 3.0f);
        Destroy(_fire3, 3.0f);
    }

    void getReward()
    {
        // 적 캐릭터 소멸 후 보상 오브젝트 나타나도록 함
        Instantiate(reward, transform.position, transform.rotation);
    }

    // 자기 자신의 hpBar 생성하는 함수
    void setHpBar()
    {
        // 씬의 uiCanvas 가져옴
        uiCanvas = GameObject.Find("UICanvas").GetComponent<Canvas>();
        // hpBarPrefab을 복사해 uiCanvas의 위치에 hpBar 생성
        hpBar = Instantiate(hpBarPrefab, uiCanvas.transform);
        // hpBarImage에 생성한 hpBar의 fill Image 저장
        hpBarImage = hpBar.GetComponentsInChildren<Image>()[1];
        // 생성된 hpBar 오브젝트의 EnemyHpBar 스크립트 가져와 저장
        EnemyHpBar bar = hpBar.GetComponent<EnemyHpBar>();
        // bar의 추적 타겟을 현재 적 오브젝트로 설정
        bar.targetTr = gameObject.transform;
        // bar의 오프셋을 hpBarOffset 값으로 설정
        bar.offset = hpBarOffset;
    }

    // 현재 체력을 hpBar의 hpBarImage가 채워진 비율에 반영
    void displayHpBar()
    {
        float ratio = hp / 50.0f;
        hpBarImage.fillAmount = ratio;
    }

    IEnumerator Action()
    {
        // 적 캐릭터가 사망하지 않은 경우
        while (!isDie)
        {
            yield return ws;
            switch (monsterState)
            {
                // IDLE 일 때는 정지
                case state.IDLE:
                    moveAgent.Stop();
                    break;
                // 추적 상태일 때는 moveAgent 스크립트를 통해 player의 Transform 추적
                case state.TRACE:
                    moveAgent.SetTraceTarget(playerTr.position);
                    break;
                // 공격 상태일 때
                case state.ATTACK:
                    // 정지
                    moveAgent.Stop();
                    // nextFire 시간을 넘어가게 되면 공격함수 Fire 실행 / nextFire에 현재 시간 + 0.5~0.9초를 더해 저장
                    if (Time.time >= nextFire)
                    {
                        Fire();
                        nextFire = Time.time + fireRate + Random.Range(0.5f, 0.9f);
                    }
                    break;
                    // 사망 상태일 때
                case state.DIE:
                    // 정지
                    moveAgent.Stop();
                    // reward 오브젝트 생성
                    getReward();
                    // 현재 적 오브젝트 삭제
                    Destroy(gameObject);
                    break;
            }
        }
    }
}
