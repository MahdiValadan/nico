using UnityEngine;
using System.Collections.Generic;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;
using System.Collections;
using TMPro;

public class CheckAnswer : MonoBehaviour
{
    public int level;
    public GameObject FrontDisplay;
    public GameObject NextLevel;
    public GameObject LeftPlayer;
    public GameObject RightPlayer;
    public List<string> answer;
    public string leftPlayerChoice;
    public string rightPlayerChoice;
    private float timer;
    private readonly float interval = 5f;
    private bool hasChecked = false;

    private void Start()
    {
        timer = 0f;
    }
    void Update()
    {
        GameObject feedbackGameObject = FrontDisplay.transform.GetChild(5).gameObject;
        TextMeshProUGUI feedback = feedbackGameObject.GetComponent<TextMeshProUGUI>();

        if (!hasChecked)
        {
            QuestionsGenerator questionsGenerator = FrontDisplay.GetComponent<QuestionsGenerator>();
            answer = level switch
            {
                1 => questionsGenerator.answersA,
                2 => questionsGenerator.answersB,
                3 => questionsGenerator.answersC,
                _ => questionsGenerator.answersA,
            };
            leftPlayerChoice = LeftPlayer.GetComponent<PlayerMovement>().spriteName;
            rightPlayerChoice = RightPlayer.GetComponent<PlayerMovement>().spriteName;

            if (!string.IsNullOrEmpty(leftPlayerChoice) && !string.IsNullOrEmpty(rightPlayerChoice))
            {
                timer += Time.deltaTime;
                if (timer >= interval)
                {
                    
                    if (answer.Contains(leftPlayerChoice) && answer.Contains(rightPlayerChoice))
                    {
                        feedback.text = "Yey!";
                        // MAGIC ROOM OBJECTS
                        //MagicRoomManager.instance.MagicRoomLightManager.SendColor(Color.green);
                        MagicRoomManager.instance.MagicRoomLightManager.SendColor("#00FFFF", 255);
                        MagicRoomManager.instance.MagicRoomAppliancesManager.SendChangeCommand("Macchina delle Bolle", true);
                    }
                    else
                    {
                        feedback.text = "Nope!";
                        // MAGIC ROOM OBJECTS
                        MagicRoomManager.instance.MagicRoomLightManager.SendColor(Color.magenta);
                    }
                    StartCoroutine(ActivateNextLevel(feedback));
                }
            }
        }
    }
    IEnumerator ActivateNextLevel(TextMeshProUGUI feedback)
    {
        // Wait for 5 seconds
        hasChecked = true;
        yield return new WaitForSeconds(5f);
        feedback.text = "";
        NextLevel.SetActive(true);
        gameObject.SetActive(false);
        // MAGIC ROOM OBJECTS
        MagicRoomManager.instance.MagicRoomLightManager.SendColor(Color.black);
        MagicRoomManager.instance.MagicRoomAppliancesManager.SendChangeCommand("Macchina delle Bolle", false);
    }
}

