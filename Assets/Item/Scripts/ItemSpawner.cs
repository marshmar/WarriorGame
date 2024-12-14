using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject itemPrefabs;
    private GameObject questObj;
    [SerializeField]private float itemSpawnTime;
    [SerializeField]private bool isSpawned;

    // Start is called before the first frame update
    void Start()
    {
        isSpawned = false;
        questObj = GameObject.Find("NPC");
        itemSpawnTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        NPCInteractable npcInteractableScr = questObj.GetComponent<NPCInteractable>();
        if (npcInteractableScr.getQuest())
        {
            if (isSpawned == false)
            {
                if (itemSpawnTime <= 0)
                {
                    Spawn();
                    itemSpawnTime = 30.0f;
                }
                else
                {
                    if (itemSpawnTime > 0)
                        itemSpawnTime -= Time.deltaTime;
                }
                    
            }
        }
    }

    public void Spawn()
    {
        Instantiate(itemPrefabs, transform.position, transform.rotation);       
        isSpawned = true;
    }

    public bool IsSpawned() { return isSpawned;  }
    public void setSpawned(bool isSpawned)
    {
        this.isSpawned = isSpawned;
    } 
}
