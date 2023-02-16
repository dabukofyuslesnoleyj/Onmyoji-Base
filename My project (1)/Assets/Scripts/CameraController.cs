using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player; // The player's transform component
    public float backgroundLayerSpeedMultiplier = 0.5f; // The speed multiplier for the background layer
    public Vector2 margin; // The distance in pixels from the edges of the screen that the player can move before the camera moves
    public float smoothTime = 0.3f; // The smooth time for the camera's movement

    private Vector3 previousPlayerPosition; // The player's position in the previous frame
    private Vector3 cameraVelocity = Vector3.zero; // The velocity of the camera's movement

    void LateUpdate()
    {
        // Get the player's position in the screen
        Vector3 playerScreenPosition = Camera.main.WorldToScreenPoint(player.position);

        // Check if the player has moved beyond the margins
        if (playerScreenPosition.x > Screen.width - margin.x || playerScreenPosition.x < margin.x ||
            playerScreenPosition.y > Screen.height - margin.y || playerScreenPosition.y < margin.y)
        {
            // Get the amount of movement of the player
            Vector3 movement = player.position - previousPlayerPosition;

            // Update the previous player position
            previousPlayerPosition = player.position;

            // Move the camera by the player's movement, with a slower speed for the background layer
            transform.position += movement * (1.0f - backgroundLayerSpeedMultiplier);
        }

        // Smoothly move the camera towards the target position
        transform.position = Vector3.SmoothDamp(transform.position, player.position, ref cameraVelocity, smoothTime);

        // Set the camera's Z position to the default Z position
        transform.position = new Vector3(transform.position.x, transform.position.y, -10f);
    }
}
