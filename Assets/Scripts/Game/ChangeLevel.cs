using UnityEngine;

public class ChangeLevel : MonoBehaviour
{
    public GameObject levelToActivate;
    public GameObject levelToDeactivate;
    private readonly float interval = 5f; // Set the interval in seconds
    private float timer;
    void Update()
    {
        timer += Time.deltaTime;
        // Check if the spacebar is pressed
        if (Input.GetKeyDown(KeyCode.Space) || timer >= interval)
        {
            // Toggle the activation status of the objects
            levelToActivate.SetActive(true);
            levelToDeactivate.SetActive(false);
        }
    }
}
