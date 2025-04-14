using UnityEngine;

public class Weapon : MonoBehaviour {

    public Transform firePoint; // The point from where the projectile will be fired:
    public GameObject bulletPrefab; // The projectile prefab to be instantiated


    // Update is called once per frame
    void Update()
    {
        // Check if the F key is pressed
        if (Input.GetKeyDown(KeyCode.F))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation); // Instantiate the bullet prefab at the fire point position and rotation

    }
        
}
