using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TiemrCount : MonoBehaviour
{
    private Text timerText;
    private float time;
    private int currentTime;
    GameObject npcobject;
    // Start is called before the first frame update
    void Start()
    {
        time = 301.0f;
        npcobject = GameObject.Find("NPC");
        timerText = GetComponent<Text>();
        timerText.color = Color.red;
        timerText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        NPCInteractable npcInteractableScript = npcobject.GetComponent<NPCInteractable>();
        if (npcInteractableScript.getQuest() && npcInteractableScript.QuestClear() == false)
        {
            timerText.enabled = true;
            time -= Time.deltaTime;
            currentTime = (int)time;
            timerText.text = "Timer :" + currentTime;
        }
    }
}
