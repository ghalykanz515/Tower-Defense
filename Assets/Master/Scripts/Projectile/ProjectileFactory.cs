using System.Collections;
using UnityEngine;

namespace Assets.Master.Scripts.Projectile
{
    public class ProjectileFactory : MonoBehaviour
    {
        public ProjectileData fireProjectileData; // Assign in Inspector
        public ProjectileData iceProjectileData;  // Assign in Inspector

        public void ShootProjectile(ProjectileData data, Vector3 position, Vector3 direction)
        {
            // Instantiate the projectile prefab
            GameObject projectileObj = Instantiate(data.prefab, position, Quaternion.identity);

            // Initialize the projectile with shared and unique data
            Projectile projectile = projectileObj.GetComponent<Projectile>();
            projectile.Initialize(data, direction);
        }
    }
}