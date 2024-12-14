using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillLog : MonoBehaviour
{
    private int killCount;
    private GameObject questObject;
    private Text killCountText;

    // Start is called before the first frame update
    void Start()
    {
        killCount = 0;
        questObject = GameObject.Find("NPC");
        killCountText = GameObject.Find("KillCount").GetComponent<Text>();
        killCountText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        NPCInteractable npcInteractableScr = questObject.GetComponent<NPCInteractable>();
        if (npcInteractableScr.getQuest())
        {
            killCountText.enabled = true;
            killCountText.text = ($"Kills: {killCount}/7");
        }
    }

    public int getKillCount() { return killCount; }
    public void plusKillCount()
    {
        killCount += 1;
    }
}
