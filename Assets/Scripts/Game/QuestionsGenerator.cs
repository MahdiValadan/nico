using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuestionsGenerator : MonoBehaviour
{
    // Questions
    public TextMeshProUGUI questionA;
    public TextMeshProUGUI questionB;
    public TextMeshProUGUI questionC;

    // OPTIONS
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
        questionA.text = Questions.questionA.context;
        questionB.text = Questions.questionB.context;
        questionC.text = Questions.questionC.context;

        // Load all of the generated sprites from folder
        sprites = Resources.LoadAll<Sprite>("Generated Sprites");
        // Assign Images to the FLOOR options
        for (int i = 0; i < 12; i++)
        {
            options[i].sprite = sprites[i];
        }

    }
}
