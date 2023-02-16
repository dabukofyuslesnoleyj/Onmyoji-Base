using UnityEngine;

public class LookAtMouse : MonoBehaviour
{

    public bool LookOnStart = true;

    public bool LookOnUpdate = false;
    
    private void Start() 
    {
        if(LookOnStart)
            lookAtMouse();
    }

    private void Update()
    {
        if(LookOnUpdate)
            lookAtMouse();
    }

    private void lookAtMouse()
    {
        Vector2 mousePosition = Input.mousePosition;
        Vector2 objectPosition = Camera.main.WorldToScreenPoint(transform.position);
        Vector2 direction = mousePosition - objectPosition;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}