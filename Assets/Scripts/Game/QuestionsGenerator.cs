using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class QuestionsGenerator : MonoBehaviour
{
    // Questions
    public TextMeshProUGUI questionA;
    public TextMeshProUGUI questionB;
    public TextMeshProUGUI questionC;

    //Answers
    public List<string> answersA;
    public List<string> answersB; 
    public List<string> answersC; 

    // Options
    public SpriteRenderer[] options;

    //Sprites List
    public Sprite[] sprites;

    // Reading JSON
    static string path = Application.dataPath + "/JSON/Questions.json";
    static string json = System.IO.File.ReadAllText(path);
    Questions Questions = JsonUtility.FromJson<Questions>(json);
    //

    void Start()
    {
        //Load generated Questions context
        questionA.text = Questions.questionA.context;
        questionB.text = Questions.questionB.context;
        questionC.text = Questions.questionC.context;

        //Set answers
        answersA = Questions.questionA.answers;
        answersB = Questions.questionB.answers;
        answersC = Questions.questionC.answers;

        // Load all of the generated sprites from folder
        sprites = Resources.LoadAll<Sprite>("Generated Sprites");
        // Assign Images to the FLOOR options
        for (int i = 0; i < 12; i++)
        {
            options[i].sprite = sprites[i];
        }

    }
}
