using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    private float health;
    Material material;
    private float damageTime;

    public Slider healthSlider;
    // Start is called before the first frame update
    void Start()
    {
        setHealth(100);
        damageTime = 0;
        material = GetComponentInChildren<Renderer>().material;
        material.color = Color.gray;
        //material = GetComponentInChildren<Material>();
    }

    // Update is called once per frame
    void Update()
    {
        if (damageTime > 0)
        {
            material.color = Color.red;
            damageTime -= Time.deltaTime;
        }
        else
            material.color = Color.gray;
    }

    //µ¥¹ÌÁö ÇÔ¼ö
    public void Damage(float damageAmount)
    {
        this.health -= damageAmount;
        healthSlider.value -= damageAmount * 0.01f;
        damageTime = 0.5f;
        //³Ë¹é
        KnockBack(6.0f);
        
        if (health <= 0)
        {
            Destroy(gameObject);
            GameObject killObj = GameObject.Find("KillLog");
            KillLog killLogScript = killObj.GetComponent<KillLog>();
            killLogScript.plusKillCount();
        }
    }
    //³Ë¹é
    public void KnockBack(float knockbackPower)
    {
        GameObject player = GameObject.Find("Player");

        Vector3 reactVec = transform.position - player.transform.position;
        reactVec = reactVec.normalized;


        MonsterMovement monsterMovemnetScr = GetComponent<MonsterMovement>();
        NavMeshAgent agent = monsterMovemnetScr.agent;

        agent.velocity = reactVec * knockbackPower;
    }

    public float getHealth() { return health; }
    public void setHealth(float health)
    {
        this.health = health;
    }
}
