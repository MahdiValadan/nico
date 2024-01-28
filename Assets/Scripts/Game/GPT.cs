using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using OpenAI;
using System.IO;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEditor;

public class GPT : MonoBehaviour
{
    public TextMeshProUGUI log;
    public Button startBtn;
    public Button generateBtn;
    public TMP_InputField input;
    public GameObject spinner;

    private OpenAIApi openAI = new OpenAIApi();

    // # STARTING GAME FUNCTION #
    public void LoadNextScene()
    {
        SceneManager.LoadScene("Game Scene");
    }
    // # SENDING REQUEST FUNCTION #
    public async void SendRequest()
    {
        // UPDATE UI
        generateBtn.gameObject.SetActive(false);
        input.gameObject.SetActive(false);
        log.gameObject.SetActive(true);
        log.text = "Generating Questions";
        spinner.SetActive(true);
        // READ THE PROMPT 
        string topic = input.text;
        if (string.IsNullOrEmpty(topic))
        {
            topic = "Animal";
        }
        string prompt = File.ReadAllText("Assets/Prompts/Prompt.txt");
        prompt = prompt.Replace("???", topic);
        // CREAT GPT-4 REQUEST
        var req = new CreateChatCompletionRequest
        {
            Model = "gpt-4-turbo-preview",
            Messages = new List<ChatMessage>()
        {
            new ChatMessage()
            {
                Role = "user",
                Content = prompt
            }
        }
        };
        // SEND REQUEST TO GPT
        var res = await openAI.CreateChatCompletion(req);
        var json = res.Choices[0].Message.Content;
        // WRITE JSON FILE
        using (StreamWriter writer = new StreamWriter("Assets/JSON/Questions.json"))
        {
            writer.Write(json);
        }
        // REFRESH ASSETS
        AssetDatabase.Refresh();
        // UPDATE UI
        log.text = "Questiones Generated";
        Debug.Log("GPT JSON Response written to file");
        //READ OPTIONS FROM JSON
        string jsonRead = System.IO.File.ReadAllText("Assets/JSON/Questions.json");
        Questions questions = JsonUtility.FromJson<Questions>(jsonRead);
        string[] optionsA = questions.questionA.options;
        string[] optionsB = questions.questionB.options;
        string[] optionsC = questions.questionC.options;
        // LIST OF ALL OPTIONS
        string[] allOptions = optionsA.Concat(optionsB).Concat(optionsC).ToArray();
        string[] allOptionsTrimed = optionsA.Concat(optionsB).Concat(optionsC).ToArray();
        for (int i = 0; i < allOptionsTrimed.Length; i++)
        {
            allOptionsTrimed[i] = allOptionsTrimed[i].Substring(3);
        }
        // UPDATE UI
        log.text = "Generating Images...";
        // CLEARE IMAGE DIRECTORY
        ClearGeneratedImageDirectory();
        AssetDatabase.Refresh();
        // Generate Images
        string imagePrompt = File.ReadAllText("Assets/Prompts/ImagePrompt.txt");
        for (int i = 0; i < allOptions.Length; i++)
        {
            // UPDATE UI
            log.text = "Generating Image " + i;
            // SEND DALL-E-3 REQUEST
            string concatedImagePrompt = allOptionsTrimed[i] + " " + imagePrompt;
            var resImage = await openAI.CreateImage(
                new CreateImageRequest
                {
                    Prompt = concatedImagePrompt,
                    Model = "dall-e-3",
                    Size = ImageSize.Size1024
                }
            );
            // DOWNLOAD GENERETED IMAGE
            string url = resImage.Data[0].Url;
            string savePath = "Assets/Resources/Generated Sprites/" + allOptions[i] + ".png";
            StartCoroutine(DownloadAndSaveImageCoroutine(url, savePath));
        }
        //FINAL SETUPS
        StartCoroutine(FinalSetup());
    }

    // IMAGE DOWNLOAD FUNCTION
    IEnumerator DownloadAndSaveImageCoroutine(string url, string savePath)
    {
        using (UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(url))
        {
            // Send the request and wait for a response
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                // Get the downloaded texture
                Texture2D downloadedTexture = DownloadHandlerTexture.GetContent(webRequest);

                // Convert the texture to a byte array
                byte[] bytes = downloadedTexture.EncodeToPNG(); // You can also use EncodeToPNG()

                // Save the byte array to a file
                File.WriteAllBytes(savePath, bytes);

                Debug.Log("Image downloaded and saved at: " + savePath);
            }
            else
            {
                Debug.LogError("Error downloading image: " + webRequest.error);
            }
        }
    }
    private void ClearGeneratedImageDirectory()
    {
        // Get all files in the folder
        string[] files = Directory.GetFiles("Assets/Resources/Generated Sprites");
        // Delete each file
        foreach (string file in files)
        {
            File.Delete(file);
        }
        Debug.Log("Generated Sprites Directory has been cleared");
    }
    private IEnumerator FinalSetup()
    {
        Debug.Log("Coroutine started");
        // Wait for 10 seconds
        yield return new WaitForSeconds(10.0f);
        Debug.Log("Coroutine resumed after 10 seconds");
        // UPDATE UI
        AssetDatabase.Refresh();
        spinner.SetActive(false);
        log.gameObject.SetActive(false);
        startBtn.gameObject.SetActive(true);
    }
}
