using UnityEngine;
public class RightPlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Adjust the speed as needed
    public string spriteName;
    void Update()
    {
        float horizontalInput = Input.GetKey(KeyCode.LeftArrow) ? -1f : (Input.GetKey(KeyCode.RightArrow) ? 1f : 0f);
        float verticalInput = Input.GetKey(KeyCode.DownArrow) ? -1f : (Input.GetKey(KeyCode.UpArrow) ? 1f : 0f);
        // Calculate the movement vector
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * moveSpeed * Time.deltaTime;
        // Move the object
        transform.Translate(movement);
    }
    void OnCollisionEnter(Collision collision)
    {
        // MagicRoomManager.instance.MagicRoomLightManager.SendColor(Color.green);
        SpriteRenderer spriteRenderer = collision.gameObject.GetComponentInChildren<SpriteRenderer>();
        spriteName = spriteRenderer.sprite.name;
        Debug.Log("Right Player stands on: " + spriteName);
    }
}
