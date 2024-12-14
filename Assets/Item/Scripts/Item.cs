using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private GameObject itemSpawnerObj;
    private GameObject weaponObj;

    private AudioSource audio;
    public AudioClip itemSound;
    // Start is called before the first frame update
    void Start()
    {
        itemSpawnerObj = GameObject.Find("ItemSpawner");
        weaponObj = GameObject.Find("Weapon");
        audio = gameObject.AddComponent<AudioSource>();
        audio.clip = itemSound;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            audio.Play();
            Destroy(gameObject);
            Weapon weaponScr = weaponObj.GetComponent<Weapon>();
            weaponScr.plusAtk(20.0f);

            ItemSpawner itemSpawnerScr = itemSpawnerObj.GetComponent<ItemSpawner>();
            itemSpawnerScr.setSpawned(false);
        }
    }
}
