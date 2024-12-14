using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float jumpPower;
    [SerializeField] private float gravity;
    [SerializeField] private bool canMove;
    [SerializeField] private bool canRun;
    [SerializeField] private bool canJump;
    [SerializeField] private bool isMoving;
    private float healTime;

    private CharacterController characterController;
    private Animator animator;
    private Vector3 moveVelocity;
    private Vector3 turnVelocity;

    public GameObject objWeapon;
    private BoxCollider weaponCollider;

    private AudioSource audio;
    private AudioSource audio3;

    public AudioClip moveSound;
    public AudioClip runSound;
    public AudioClip jumpSound;
    public AudioClip attackSound;

    // Start is called before the first frame update
    void Start()
    {
        //���� �ʱ�ȭ
        moveSpeed = 3.0f;
        rotationSpeed = 135.0f;
        jumpPower = 7.0f;
        gravity = -20.0f;
        canMove = true;
        canRun = true;
        canJump = true;

        healTime = 5.0f;
        characterController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        //���� �ݶ��̴� ����
        weaponCollider = objWeapon.GetComponent<BoxCollider>();
        weaponCollider.enabled = false;
        //�����
        audio = gameObject.AddComponent<AudioSource>();
        audio.loop = false;
        audio3 = gameObject.AddComponent<AudioSource>();
        audio3.loop = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving == false)
            audio.Stop();

        // ������
        if (canMove)
            Move();
        // ����
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
            audio3.clip = attackSound;
            audio3.Play();
        }
           

        Sound();
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.6f)
        { 
            //������ ���� �����ϸ� ������ �ݶ��̴��� ������ �ʵ���.
            weaponCollider.enabled = false;
            //���� �ִϸ��̼��� �����ð� �����߸� ������ �����ϵ���
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.78f)
            {
                canMove = true;
                animator.SetBool("Attacking", false);
               

            }
        }
        
    }

    // �÷��̾� ������
    void Move()
    {
        float hInput = Input.GetAxis("Horizontal");
        float vInput = Input.GetAxis("Vertical");

        // �÷��̾ ���� ���������� ������ ����
        if (characterController.isGrounded)
        {
            animator.SetBool("Walking", false);
            animator.SetBool("Running", false);
            animator.SetBool("Jumping", false);

            if (vInput != 0)
            {
                isMoving = true;
                //�޸���
                if (Input.GetKey(KeyCode.LeftShift) && canRun)
                {
                    animator.SetBool("Running", true);
                    animator.SetBool("Walking", false);
                    moveSpeed = 5.0f;
                    
                }
                //�ȱ�
                else
                {
                    animator.SetBool("Walking", true);
                    animator.SetBool("Running", false);
                    moveSpeed = 2.0f;
                   
                }
            }
            else
                isMoving = false;

            moveVelocity = transform.forward * moveSpeed * vInput;
            turnVelocity = transform.up * rotationSpeed * hInput;

            //����
            if (Input.GetButtonDown("Jump") && canJump)
            {
                isMoving = true;
                animator.SetBool("Jumping", true);
                animator.SetBool("Walking", false);
                animator.SetBool("Running", false);
                moveVelocity.y = jumpPower;
            }
        }

        // �߷� �߰�
        moveVelocity.y += gravity * Time.deltaTime;
        characterController.Move(moveVelocity * Time.deltaTime);
        transform.Rotate(turnVelocity * Time.deltaTime);
        
    }

    public void Sound()
    {
        if (animator.GetBool("Walking") == true)
        {
            if (audio.isPlaying == false)
            {
                audio.clip = moveSound;
                audio.Play();
            }
            else if (audio.isPlaying == true && audio.clip != moveSound)
            {
                audio.Stop();
                audio.clip = moveSound;
                audio.Play();
            }


        }
        else if (animator.GetBool("Running") == true)
        {
            if (audio.isPlaying == false)
            {
                audio.clip = runSound;
                audio.Play();
            }
            else if (audio.isPlaying == true && audio.clip != runSound)
            {
                audio.Stop();
                audio.clip = runSound;
                audio.Play();
            }

        }
        else if (animator.GetBool("Jumping") == true)
        {
            if (audio.isPlaying == false)
            {
                audio.clip = jumpSound;
                audio.Play();
            }
            else if (audio.isPlaying == true && audio.clip != jumpSound)
            {
                audio.Stop();
                audio.clip = jumpSound;
                audio.Play();
            }
        }
        else
            audio.clip = null;
    }
    //����
    void Attack()
    {
        animator.SetBool("Attacking", true);
        if(characterController.isGrounded == true)
            canMove = false;
        // ������ ������ ������ �ݶ��̴��� ���⵵��.
        weaponCollider.enabled = true;
        
    }


    private void OnTriggerStay(Collider other)
    {
        //���Ϳ� �浹��
        if (other.tag == "Monster")
        {
            //������ �Ա�
            if (gameObject.TryGetComponent<Player>(out Player playerComponent) == true)
            {
                if (playerComponent.getDamaged() == false)
                {
                    playerComponent.Damage(20);
                }
                   
            }
        }
        else if (other.tag == "Slime")
        {
            if (gameObject.TryGetComponent<Player>(out Player playerComponent) == true)
            {
                playerComponent.Damage(5f);
                canRun = false;
                canJump = false;
            }
            
        }
        else if (other.tag == "HealingZone")
        {
            if (gameObject.TryGetComponent<Player>(out Player playerComponent) == true)
            {
                healTime -= Time.deltaTime;
                if(healTime <= 0)
                {
                    playerComponent.plusHealth(10);
                    healTime = 5.0f;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Slime")
        {
            canRun = true;
            canJump = true;
        }
        if(other.tag == "HealingZone")
        {
            healTime = 5.0f;
        }
    }
    //getter, setter
    public float GetMoveSpeed(){ return moveSpeed; }
    public void SetMoveSpeed(float moveSpeed) 
    {
        this.moveSpeed = moveSpeed;
    }

    public float GetRotationSpeed() { return rotationSpeed; }
    public void SetRotationSpeed(float rotationSpeed)
    {
        this.rotationSpeed = rotationSpeed;
    }

    public float GetJumpPower() { return jumpPower; }
    public void SetJumpPower(float jumpPower) 
    { 
        this.jumpPower = jumpPower; 
    }

}
