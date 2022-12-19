using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RewardCtrl : MonoBehaviour
{
    public enum reward
    {
        portion,
        flowercrystal,
        bonecrystal,
        dragoncrystal
    }
    GameObject player;
    Transform playerTr;
    Transform rewardTr;
    GameManager gameMng;
    AudioSource itemSound;
    AudioSource buffSound;

    public reward _reward;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("PLAYER");
        playerTr = player.transform;
        rewardTr = gameObject.transform;
        gameMng = GameObject.Find("GameManager").GetComponent<GameManager>();
        itemSound = GameObject.Find("item effect").GetComponent<AudioSource>();
        buffSound = GameObject.Find("buff effect").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if ((playerTr.position - rewardTr.position).magnitude <= 3.0f)
            {

                switch (_reward)
                {
                    case reward.portion:
                        buffSound.Play();
                        if (player.GetComponent<PlayerCtrl>().hp <= 50)
                        {
                            player.GetComponent<PlayerCtrl>().hp += 50;
                        }
                        else
                        {
                            player.GetComponent<PlayerCtrl>().hp = 100;
                        }
                        player.GetComponent<PlayerCtrl>().displayHPbar();
                        player.GetComponent<PlayerCtrl>().hpGetEffect();
                        Destroy(gameObject);
                        break;
                    case reward.flowercrystal:
                        itemSound.Play();
                        if (gameMng.stage == Stage.QUEST1)
                        {
                            gameMng.questItem++;
                            gameMng.itemGet();
                            if (gameMng.questItem == gameMng.requirednum)
                            {
                                gameMng.questSuccess();
                                gameMng.stage = Stage.QUEST1_s;
                            }
                        }
                        Destroy(gameObject);
                        break;
                    case reward.dragoncrystal:
                        itemSound.Play();
                        if (gameMng.stage == Stage.QUEST3)
                        {
                            gameMng.questItem++;
                            gameMng.itemGet();
                            if (gameMng.questItem == gameMng.requirednum)
                            {
                                gameMng.questSuccess();
                                gameMng.stage = Stage.QUEST3_s;
                            }
                        }
                        Destroy(gameObject);
                        break;
                }
            }
            if (Vector3.Distance(playerTr.position, rewardTr.position) <= 15.0f)
            {
                switch (_reward)
                {
                    case reward.portion:
                        buffSound.Play();
                        if (player.GetComponent<PlayerCtrl>().hp <= 50)
                        {
                            player.GetComponent<PlayerCtrl>().hp += 50;
                        }
                        else
                        {
                            player.GetComponent<PlayerCtrl>().hp = 100;
                        }
                        player.GetComponent<PlayerCtrl>().displayHPbar();
                        player.GetComponent<PlayerCtrl>().hpGetEffect();
                        Destroy(gameObject);
                        break;
                    case reward.bonecrystal:
                        itemSound.Play();
                        if (gameMng.stage == Stage.QUEST2)
                        {
                            gameMng.questItem++;
                            gameMng.itemGet();
                            if (gameMng.questItem == gameMng.requirednum)
                            {
                                gameMng.questSuccess();
                                gameMng.stage = Stage.QUEST2_s;
                            }
                        }
                        Destroy(gameObject);
                        break;
                }
            }
        }
    }
}
