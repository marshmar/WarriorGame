using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterMovement : MonoBehaviour
{
    [SerializeField] private GameObject target;
    public NavMeshAgent agent;
    private float findRange;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player");
        findRange = 15.0f;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] colliderArray = Physics.OverlapSphere(transform.position, findRange);
        foreach (Collider collider in colliderArray)
        {
            //TryGetComponenet :  콜라이더의 특정 컴포넌트를 추출하려고 시도, return 값은 true, false
            if (collider.TryGetComponent<Player>(out Player playerComponent) || collider.TryGetComponent<Weapon>(out Weapon weapon))
            {
                agent.isStopped = false;
                agent.destination = target.transform.position;
            }
            else
                agent.isStopped = true;
        }
        //플레이어 위치 추적하여 따라가기
        
    }

}
