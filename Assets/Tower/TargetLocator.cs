using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    [SerializeField]
    Transform Weapon;
    [SerializeField]
    float Range = 15f;
    [SerializeField]
    ParticleSystem bulletParticles;

    Transform Target;

    void Update()
    {
        FindClosesttarget();
        AimWeapon();
    }

    void FindClosesttarget()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        List<Enemy> enemiesInRange = new List<Enemy>();

        foreach (Enemy enemy in enemies)
        {
            float targetDistance = Vector3.Distance(transform.position, enemy.transform.position);

            if (targetDistance < Range)
            {
                enemiesInRange.Add(enemy);
            }
        }

        if (enemiesInRange.Count != 0)
        {
            Target = enemiesInRange.Last().transform;
        }
        else
        {
            Target = null;
        }
        
    }
    private void AimWeapon()
    {
        Weapon.LookAt(Target);

        if (Target != null)
        {
            Attack(true);
        }
        else
        {
            Attack(false);
        }
    }

    private void Attack(bool isActive)
    {
        var emissionModule = bulletParticles.emission;
        emissionModule.enabled = isActive;
    }
}
