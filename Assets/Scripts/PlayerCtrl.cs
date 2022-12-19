using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class PlayerCtrl : MonoBehaviour
{
    public float moveSpeed = 10;
    public float rotSpeed = 1000;
    public float hp = 100;
    public float sp = 100;
    Animator animator;
    public GameObject arrow;
    public GameObject specialarrow;
    public GameObject shotPos;
    public Image hpBar;
    public Image spBar;
    public GameObject hpEffect;
    public GameObject spEffect;
    public AudioSource arrowSound;
    public AudioSource specialArrowSound;
    public AudioSource hurtSound;

    private void Start()
    {
        animator = GetComponent<Animator>();
        displayHPbar();
        displaySPbar();
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float r = Input.GetAxis("Mouse X");

        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);
        transform.Translate(moveDir.normalized * moveSpeed * Time.deltaTime, Space.Self);
        transform.Rotate(Vector3.up * rotSpeed * Time.deltaTime * r);

        if (v >= 0.1f)
        {
            animator.SetInteger("Move", 1);
        }
        else if (v <= -0.1f)
        {
            animator.SetInteger("Move", 2);
        }
        else if (h >= 0.1f)
        {
            animator.SetInteger("Move", 4);
        }
        else if (h <= -0.1f)
        {
            animator.SetInteger("Move", 3);
        }
        else
        {
            animator.SetInteger("Move", 0);
        }

        if(Input.GetKeyDown(KeyCode.LeftControl)){
            attack();
        }

        if(Input.GetKeyDown(KeyCode.Q) && sp >= 20)
        {
            sp -= 20;
            specialattack();
            displaySPbar();
        }

        if (hp <= 0)
        {
            animator.SetTrigger("Death");
        }
    }

    void attack()
    {
        GameObject tmp = Instantiate(arrow, shotPos.transform.position , shotPos.transform.rotation);
        arrowSound.Play();
        animator.SetBool("Attack", true);
        Invoke("stopAttackAni", 0.64f);
        Destroy(tmp, 5f);
    }

    void specialattack()
    {
        specialArrowSound.Play();
        GameObject tmp = Instantiate(specialarrow, shotPos.transform.position, shotPos.transform.rotation);
        animator.SetBool("Attack", true);
        Invoke("stopAttackAni", 0.64f);
        Destroy(tmp, 5f);
    }

    void stopAttackAni()
    {
        animator.SetBool("Attack", false);
    }
    void unattackedAni()
    {
        animator.SetBool("Attacked", false);
    }

    public void displayHPbar()
    {
        float ratio = hp / 100.0f;
        hpBar.fillAmount = ratio;
        
    }

    public void displaySPbar()
    {
        float ratio = sp / 100.0f;
        spBar.fillAmount = ratio;

    }

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "ATTACK")
        {
            hurtSound.Play();
            animator.SetBool("Attacked", true);
            Invoke("unattackedAni", 0.76f);
            hp -= coll.gameObject.GetComponent<damageCtrl>().damage;
            displayHPbar();
            Destroy(coll.gameObject, 0.1f);
        }
        else if (coll.tag == "PORTAL1")
        {
            SceneManager.LoadScene("FirstStage");
            transform.position =  new Vector3(280, 0, 200);
        }
        else if (coll.tag == "PORTAL2" || coll.tag == "PORTAL4")
        {
            SceneManager.LoadScene("StartStage");
            transform.position = new Vector3(0, 0, 0);
        }
        else if (coll.tag == "PORTAL3")
        {
            SceneManager.LoadScene("SecondStage");
            transform.position = new Vector3(22, 0, 78);
        }
    }

    public void hpGetEffect()
    {
        GameObject tmp = Instantiate(hpEffect, transform.position + new Vector3(0, 4, 0), transform.rotation);
        Destroy(tmp, 0.8f);
    }

    public void spGetEffect()
    {
        GameObject tmp = Instantiate(spEffect, transform.position + new Vector3(0, 4, 0), transform.rotation);
        Destroy(tmp, 0.8f);
    }
}
