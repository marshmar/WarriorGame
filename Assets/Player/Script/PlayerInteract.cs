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
        //Physics.OverlapSphere : 가상의 원을 만들어 추출하려는 반경 이내에 들어와 있는 콜라이더들을 반환하는 함수
        Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
        foreach (Collider collider in colliderArray)
        {
            //TryGetComponenet :  콜라이더의 특정 컴포넌트를 추출하려고 시도, return 값은 true, false
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
