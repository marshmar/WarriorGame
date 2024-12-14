using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteract : MonoBehaviour
{
    private GameObject NPCInteract;
    private Text NPCInteractText;
    // Start is called before the first frame update
    void Start()
    {
        NPCInteract = GameObject.Find("NPCInteract");
        NPCInteractText = NPCInteract.GetComponent<Text>();
        NPCInteract.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        float interactRange = 2f;
        //Physics.OverlapSphere : ������ ���� ����� �����Ϸ��� �ݰ� �̳��� ���� �ִ� �ݶ��̴����� ��ȯ�ϴ� �Լ�
        Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
        foreach (Collider collider in colliderArray)
        {
            //TryGetComponenet :  �ݶ��̴��� Ư�� ������Ʈ�� �����Ϸ��� �õ�, return ���� true, false
            if (collider.TryGetComponent(out NPCInteractable npcInteractable))
            {
                NPCInteract.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    npcInteractable.Interact();
                }
                return;
            }
            NPCInteract.SetActive(false);
        }
    }
}
