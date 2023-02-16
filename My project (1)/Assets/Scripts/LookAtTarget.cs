using UnityEngine;

public class LookAtTarget : MonoBehaviour
{

    public Transform target;

    public bool LookOnStart = true;

    public bool LookOnUpdate = false;

    private void Start() 
    {
        if(LookOnStart)
        {
            lookAtTarget();
        }
    }

    private void Update()
    {
        if(LookOnUpdate)
        {
            lookAtTarget();
        }
    }

    private void lookAtTarget()
    {

        Vector2 targetPostion = target.position;
        Vector2 objectPosition = Camera.main.WorldToScreenPoint(transform.position);
        Vector2 direction = targetPostion - objectPosition;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}