using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswersChecker : MonoBehaviour
{
    public GameObject AnswersHolder;
    public GameObject LeftPlayer;
    public GameObject RightPlayer;

    public List<string> answersA;
    public List<string> answersB;
    public List<string> answersC;

    public string leftPlayerChoice;
    public string rightPlayerChoice;

    void Update()
    {
        QuestionsGenerator questionsGenerator = AnswersHolder.GetComponent<QuestionsGenerator>();
        answersA = questionsGenerator.answersA;
        answersB = questionsGenerator.answersB;
        answersC = questionsGenerator.answersC;
    }
    public void CheckA()
    {
        LeftPlayerMovement leftPlayerMovement = LeftPlayer.GetComponent<LeftPlayerMovement>();
        RightPlayerMovement rightPlayerMovement = RightPlayer.GetComponent<RightPlayerMovement>();
        leftPlayerChoice = leftPlayerMovement.spriteName;
        rightPlayerChoice = rightPlayerMovement.spriteName;
        if (answersA.Contains(leftPlayerChoice) && answersA.Contains(rightPlayerChoice))
        {
            Debug.Log("A: Yey!");
        }
        else
        {
            Debug.Log("A: Nope!");
        }
    }
    public void CheckB()
    {
        LeftPlayerMovement leftPlayerMovement = LeftPlayer.GetComponent<LeftPlayerMovement>();
        RightPlayerMovement rightPlayerMovement = RightPlayer.GetComponent<RightPlayerMovement>();
        leftPlayerChoice = leftPlayerMovement.spriteName;
        rightPlayerChoice = rightPlayerMovement.spriteName;
        if (answersB.Contains(leftPlayerChoice) && answersB.Contains(rightPlayerChoice))
        {
            Debug.Log("B: Yey!");
        }
        else
        {
            Debug.Log("B: Nope!");
        }
    }
    public void CheckC()
    {
        LeftPlayerMovement leftPlayerMovement = LeftPlayer.GetComponent<LeftPlayerMovement>();
        RightPlayerMovement rightPlayerMovement = RightPlayer.GetComponent<RightPlayerMovement>();
        leftPlayerChoice = leftPlayerMovement.spriteName;
        rightPlayerChoice = rightPlayerMovement.spriteName;
        if (answersC.Contains(leftPlayerChoice) && answersC.Contains(rightPlayerChoice))
        {
            Debug.Log("C: Yey!");
        }
        else
        {
            Debug.Log("C: Nope!");
        }
    }
}
