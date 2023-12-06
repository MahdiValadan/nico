using System.Collections;
using System.Collections.Generic;
[System.Serializable]
public class Question
{
    public string context;
    public string[] options;
    public List<string> answers;

}
public class Questions
{
    public Question questionA;
    public Question questionB;
    public Question questionC;

}
