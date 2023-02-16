using UnityEngine;

public class DespawnOnScreenExit : MonoBehaviour
{
    private void Update()
    {
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        if (screenPoint.x < 0 || screenPoint.x > Screen.width || screenPoint.y < 0 || screenPoint.y > Screen.height)
        {
            Destroy(gameObject);
        }
    }
}
