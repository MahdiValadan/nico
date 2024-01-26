using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public string Player;
    public float moveSpeed = 5f; // Adjust the speed as needed
    public string spriteName;

    void Update()
    {
        float horizontalInput = 0f;
        float verticalInput = 0f;

        if (Player == "Left")
        {
            horizontalInput = Input.GetKey(KeyCode.A) ? -1f : (Input.GetKey(KeyCode.D) ? 1f : 0f);
            verticalInput = Input.GetKey(KeyCode.S) ? -1f : (Input.GetKey(KeyCode.W) ? 1f : 0f);
        }
        else if (Player == "Right")
        {
            horizontalInput = Input.GetKey(KeyCode.LeftArrow) ? -1f : (Input.GetKey(KeyCode.RightArrow) ? 1f : 0f);
            verticalInput = Input.GetKey(KeyCode.DownArrow) ? -1f : (Input.GetKey(KeyCode.UpArrow) ? 1f : 0f);
        }
        // Calculate the movement vector
        Vector3 movement = moveSpeed * Time.deltaTime * new Vector3(horizontalInput, 0f, verticalInput);
        // Move the object
        transform.Translate(movement);
    }

    void OnCollisionEnter(Collision collision)
    {
        SpriteRenderer spriteRenderer = collision.gameObject.GetComponentInChildren<SpriteRenderer>();
        spriteName = spriteRenderer.sprite.name;
    }

    void OnCollisionExit(Collision collision)
    {
        spriteName = null;
    }
}
