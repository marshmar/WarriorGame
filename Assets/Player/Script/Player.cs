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
        //�ǰ� �� �����ð�
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
            //�ǰ� �� ���� ����Ʈ
            material.color = Color.red;
            if(audioPlayed == false)
            {
                audio.clip = hitSound;
                audio.Play();
                audioPlayed = true;
            }
            
            //�����ð� ����
            invincibleTime -= Time.deltaTime;
            invincible = true;
            if(invincibleTime <= 0)
            {
                // �����ð� ����� ������� ������
                material.color = Color.white;
                invincibleTime = 1.5f;
                damaged = false;
                invincible = false;
                audioPlayed = false;
            }
        }
    }

    //������ �Լ�
    public void Damage(float damageAmount)
    {
        if(invincible == false)
        {
            damaged = true;
            health -= damageAmount;
            healthSlider.value -= damageAmount * 0.005f;

            //���ӿ���
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
