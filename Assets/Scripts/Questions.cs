using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class YourScriptName : MonoBehaviour
{
    public TextMeshProUGUI questionA;
    public TextMeshProUGUI questionB;
    public TextMeshProUGUI questionC;

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
    }
}
