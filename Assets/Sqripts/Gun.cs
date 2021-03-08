using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Camera fpsCamera;
    [SerializeField] private float damage;
    [SerializeField] private float range;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward,
            out var hit, range))
        {
            var enemy = hit.transform.GetComponent<Enemy>();

            if (enemy != null)
                enemy.TakeDamage(damage);
        }
    }
}
