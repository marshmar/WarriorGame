using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class NPCInteractable : MonoBehaviour
{
    private GameObject NPCDialog;
    private Text NPCText;
    private int textCount = 0;
    private bool questAccepted;
    private bool questCleared;

    private GameObject MonsterObj;
    private GameObject SlimeObj;
    private GameObject GuardianObj;
    private string[] NPCTexts = new string[9]
    {
        "Hello.",
        "Nice to Meet You.",
        "There are many monsters here.",
        "Defeat the monsters so they don't increase anymore.",
        "Can you do it?",
        "Let's start.",
        "Kill 7 monsters in 5 minutes and come to me.",
        "Well Done!",
        "You can back to the Title by pressing the Q key."
    };

    // Start is called before the first frame update
    void Start()
    {
        questAccepted = false;
        questCleared = false;
        NPCDialog = GameObject.Find("NPCDialog");
        NPCText = GameObject.Find("NPCText").GetComponent<Text>();
        NPCDialog.SetActive(false);

        MonsterObj = GameObject.Find("Monsters");
        MonsterObj.SetActive(false);
        SlimeObj = GameObject.Find("Slimes");
        SlimeObj.SetActive(false);
        GuardianObj = GameObject.Find("Guardians");
        GuardianObj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (questAccepted)
        {
            MonsterObj.SetActive(true);
            SlimeObj.SetActive(true);
            GuardianObj.SetActive(true);
        }

        if (questCleared)
        {
            // 게임 클리어한 후 Q버튼을 누르면 타이틀로 돌아가기
            if (Input.GetKeyDown(KeyCode.Q))
            {
                SceneManager.LoadScene("Clear");
            }
        }
    }

    public void Interact()
    {
        //Npc 대화창 띄우기
        NPCDialog.SetActive(true);
        //퀘스트 수락 전
        if (textCount < 7)
        {
            NPCText.text = NPCTexts[textCount];
            textCount++;
            
        }
        //퀘스트 수락 후
        if (textCount >= 7)
        {
            questAccepted = true;
            GameObject killObj = GameObject.Find("KillLog");
            KillLog killLogScript = killObj.GetComponent<KillLog>();

            //게임 클리어 조건
            if (killLogScript.getKillCount() >= 2)
            {
                if (questCleared == false)
                {
                    NPCText.text = NPCTexts[textCount];
                    textCount++;
                }
                if(textCount == 9)
                {
                    questCleared = true;
                }
            }
        }
       
    }

    public bool getQuest(){ return questAccepted; }
    public bool QuestClear() { return questCleared;  }
}
