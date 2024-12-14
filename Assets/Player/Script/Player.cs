using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private float health;
    private float invincibleTime;
    private bool damaged;
    private bool invincible;
    Material material;

    private AudioSource audio;
    public AudioClip hitSound;
    private bool audioPlayed;

    public Slider healthSlider;
    // Start is called before the first frame update
    void Start()
    {
        health = 200;
        damaged = false;
        //피격 후 무적시간
        invincibleTime = 1.5f;
        invincible = false;

        material = GetComponentInChildren<Renderer>().material;
        material.color = Color.white;

        audio = gameObject.AddComponent<AudioSource>();
        audio.loop = false;
        audioPlayed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (damaged)
        {
            //피격 시 빨간 이펙트
            material.color = Color.red;
            if(audioPlayed == false)
            {
                audio.clip = hitSound;
                audio.Play();
                audioPlayed = true;
            }
            
            //무적시간 설정
            invincibleTime -= Time.deltaTime;
            invincible = true;
            if(invincibleTime <= 0)
            {
                // 무적시간 종료시 원래대로 돌리기
                material.color = Color.white;
                invincibleTime = 1.5f;
                damaged = false;
                invincible = false;
                audioPlayed = false;
            }
        }
    }

    //데미지 함수
    public void Damage(float damageAmount)
    {
        if(invincible == false)
        {
            damaged = true;
            health -= damageAmount;
            healthSlider.value -= damageAmount * 0.005f;

            //게임오버
            if(health <= 0)
            {
                SceneManager.LoadScene("Fail");
            }
        }
        
    }

    // getter, setter
    public bool getDamaged() { return damaged;  }
    public void setDamaged(bool damaged)
    {
        this.damaged = damaged;
    }

    public float getHealth() { return health; }
    public void plusHealth(float amount)
    {
        if (health < 200.0f)
        {
            this.health += amount;
            if (health > 200.0f)
                health = 200.0f;
        }
        
    }
}
