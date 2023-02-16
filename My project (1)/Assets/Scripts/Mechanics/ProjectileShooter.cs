using UnityEngine;

public class ProjectileShooter : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float shootingSpeed = 20.0f;
    public float spreadAngle = 30.0f;
    Vector2 shootingDirection;
    

    private void Update()
    {
    }

    public void Shoot(Vector2 target)
    {
        shootingDirection = (target - (Vector2)transform.position).normalized;

        Vector2 direction = shootingDirection;
        GameObject newProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        newProjectile.GetComponent<Rigidbody2D>().velocity = direction * shootingSpeed;
    }
}
