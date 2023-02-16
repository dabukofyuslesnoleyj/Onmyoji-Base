using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    public BoxCollider2D boxCollider;
    public float extraHeightTest = 1f;
    public LayerMask groundLayerMask;

    private void Start()
    {
    }

    private void Update() 
    {
        isGrounded();
        
    }
    
    public bool isGrounded()
    {
        bool output = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, extraHeightTest, groundLayerMask);
    
        Color rayColor = output ? Color.green : Color.red;

        
        
        Debug.DrawRay(boxCollider.bounds.center, new Vector2(0,-1 * extraHeightTest), rayColor);
        return output;
    }


}
