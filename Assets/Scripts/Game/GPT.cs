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

public class GPT : MonoBehaviour
{
    public TextMeshProUGUI log;
    public Button startBtn;
    public Button generateBtn;

    private OpenAIApi openAI = new OpenAIApi();

    public void LoadNextScene()
    {
        SceneManager.LoadScene("Game Scene");
    }

    public async void SendRequest()
    {

        generateBtn.gameObject.SetActive(false);
        log.gameObject.SetActive(true);

        string prompt = File.ReadAllText("Assets/Prompts/Prompt.txt");
        var req = new CreateChatCompletionRequest
        {
            Model = "gpt-4",
            Messages = new List<ChatMessage>()
        {
            new ChatMessage()
            {
                Role = "user",
                Content = prompt
            }
        }
        };

        log.text = "Generating Questions...";
        var res = await openAI.CreateChatCompletion(req);
        var json = res.Choices[0].Message.Content;
        log.text = "Questiones Generated";

        using (StreamWriter writer = new StreamWriter("Assets/JSON/Questions.json"))
        {
            writer.Write(json);
        }
        Debug.Log("GPT JSON Response written to file");

        // Reading JSON
        string jsonRead = System.IO.File.ReadAllText("Assets/JSON/Questions.json");
        Questions questions = JsonUtility.FromJson<Questions>(jsonRead);

        //Answers
        string[] optionsA = questions.questionA.options;
        string[] optionsB = questions.questionB.options;
        string[] optionsC = questions.questionC.options;

        string[] allOptions = optionsA.Concat(optionsB).Concat(optionsC).ToArray();
        string[] allOptionsTrimed = optionsA.Concat(optionsB).Concat(optionsC).ToArray();

        for (int i = 0; i < allOptionsTrimed.Length; i++)
        {
            allOptionsTrimed[i] = allOptionsTrimed[i].Substring(3);
        }

        log.text = "Generating Images...";

        ClearGeneratedImageDirectory();

        // Generate Images
        string imagePrompt = File.ReadAllText("Assets/Prompts/ImagePrompt.txt");
        for (int i = 0; i < allOptions.Length; i++)
        {
            string concatedImagePrompt = allOptionsTrimed[i] + " " + imagePrompt;
            var resImage = await openAI.CreateImage(
                new CreateImageRequest
                {
                    Prompt = concatedImagePrompt,
                    Model = "dall-e-3",
                    Size = ImageSize.Size1024
                }
            );
            string url = resImage.Data[0].Url;
            string savePath = "Assets/Resources/Generated Sprites/"+ allOptions[i] +".png";
            StartCoroutine(DownloadAndSaveImageCoroutine(url, savePath));
        }
        Invoke("DelayedAction", 5.0f);
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
    void DelayedAction()
    {
        Debug.Log("Wait 5 seconds");
        log.text = "";
        log.gameObject.SetActive(false);
        startBtn.gameObject.SetActive(true);
    }
}
