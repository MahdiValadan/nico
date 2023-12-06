using Unity.VisualScripting;
using UnityEngine;
public class LeftPlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Adjust the speed as needed
    public string spriteName;
    void Update()
    {
        float horizontalInput = Input.GetKey(KeyCode.A) ? -1f : (Input.GetKey(KeyCode.D) ? 1f : 0f);
        float verticalInput = Input.GetKey(KeyCode.S) ? -1f : (Input.GetKey(KeyCode.W) ? 1f : 0f);
        // Calculate the movement vector
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * moveSpeed * Time.deltaTime;
        // Move the object
        transform.Translate(movement);
    }
    void OnCollisionEnter(Collision collision)
    {
        SpriteRenderer spriteRenderer = collision.gameObject.GetComponentInChildren<SpriteRenderer>();
        spriteName = spriteRenderer.sprite.name;
        Debug.Log("Left Player stands on: " + spriteName);
    }
}
