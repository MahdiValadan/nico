using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class YourScriptName : MonoBehaviour
{
    public TextMeshProUGUI question;
    static string path = Application.dataPath + "/JSON/Questions.json";
    static string json = System.IO.File.ReadAllText(path);
    Questions Questions = JsonUtility.FromJson<Questions>(json);

    void Start()
    {
        question.text = Questions.questionA.context;
    }
}
