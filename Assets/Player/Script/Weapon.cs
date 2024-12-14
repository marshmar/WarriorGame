using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    
    //플레이어의 무기 공격력
    private float atk;

    private Animator animator;

    private AudioSource audio;
    public AudioClip attackSound;

    // Start is called before the first frame update
    void Start()
    {
        //무기 공격력 설정
        atk = 20;

        animator = GetComponentInParent<Animator>();
        audio = gameObject.AddComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Monster")
        {
            if (animator.GetBool("Attacking") == true)
            {
                //공격받은 몬스터에게 데미지 주기.
                if(other.gameObject.TryGetComponent<Monster>(out Monster monsterComponent) == true)
                {
                    monsterComponent.Damage(atk);
                    if(audio.isPlaying == false)
                    {
                        audio.clip = attackSound;
                        audio.Play();
                    }
                    
                }
            }
        }
    }

    // getter, setter
    public float getAtk() { return atk; }
    public void plusAtk(float atk)
    {
        this.atk += atk;
    }
}
