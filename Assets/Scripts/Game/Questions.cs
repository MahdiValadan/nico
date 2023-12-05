using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class YourScriptName : MonoBehaviour
{
    public TextMeshProUGUI questionA;
    public TextMeshProUGUI questionB;
    public TextMeshProUGUI questionC;
    public SpriteRenderer optionA1;
    public SpriteRenderer optionA2;
    public SpriteRenderer optionA3;
    public SpriteRenderer optionA4;

    public Sprite spriteA1;
    public Sprite spriteA2;
    public Sprite spriteA3;
    public Sprite spriteA4;



    //Reading JSON
    static string path = Application.dataPath + "/JSON/Questions.json";
    static string json = System.IO.File.ReadAllText(path);
    Questions Questions = JsonUtility.FromJson<Questions>(json);
    //
    void Start()
    {
        questionA.text = Questions.questionA.context;
        questionB.text = Questions.questionB.context;
        questionC.text = Questions.questionC.context;

        optionA1.sprite = spriteA1;
        optionA2.sprite = spriteA2;
        optionA3.sprite = spriteA3;
        optionA4.sprite = spriteA4;

    }
}
