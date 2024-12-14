using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatusText : MonoBehaviour
{
    private Text playerStatusText;
    GameObject player;
    GameObject weapon;
    // Start is called before the first frame update
    void Start()
    {
        playerStatusText = GetComponent<Text>();
        player = GameObject.Find("Player");
        weapon = GameObject.Find("Weapon");
    }

    // Update is called once per frame
    void Update()
    {
        Player playerScr = player.GetComponent<Player>();
        Weapon weaponScr = weapon.GetComponent<Weapon>();
        playerStatusText.text = $"ATK: {weaponScr.getAtk()}\nHP: {playerScr.getHealth()}";
    }
}
