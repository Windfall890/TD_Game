using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Turret : MonoBehaviour {
    
    [Header("Setup Fields")]
    public string enemyTag = "Enemy";
    public GameObject bulletPrefab;
    
    [Header("Attributes")]
    public float rotateSpeed = 10;
    public float range = 15f;
    public float fireRate = 1f;

    private Transform pivot;
    private Transform firepoint;
    private GameObject target;
    private float fireCountdown = 0f;
   

    // Use this for initialization
    void Start () {
        var children = GetComponentsInChildren<Transform>().ToList();
        pivot = children.First(t => t.name == "Pivot");
        firepoint = children.First(t => t.name == "FirePoint");

        InvokeRepeating("UpdateTarget", 0f, 0.5f);
	}

    void UpdateTarget()
    {
        if (TargetIsGood(target)) return;

        GameObject nearestEnemy = FindNearest(GameObject.FindGameObjectsWithTag(enemyTag));
        if (TargetIsGood(nearestEnemy))
        {
            target = nearestEnemy;
        } else
        {
            target = null;
        }
    }

    private bool TargetIsGood(GameObject ob)
    {
        return ob != null && DistanceTo(ob.transform) <= range;
    }

    private float DistanceTo(Transform t)
    {
       return Vector3.Distance(transform.position, t.position);
    }

    private GameObject FindNearest(GameObject[] enemies)
    {
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = DistanceTo(enemy.transform);
            if (distanceToEnemy < shortestDistance)
            {
                nearestEnemy = enemy;
                shortestDistance = distanceToEnemy;
            }
        }

        return nearestEnemy;
    }

    // Update is called once per frame
    void Update ()
    {
        if (target == null) return;

        pivot.rotation = CalculateNewRotation();
        
        if(fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;    
        }

        fireCountdown -= Time.deltaTime;
    }

    private void Shoot()
    {
        GameObject bulletObject = Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);
        Bullet bullet = bulletObject.GetComponent<Bullet>();
        bullet.SetTarget(target);
    }

    private Quaternion CalculateNewRotation()
    {
        Vector3 diection = target.transform.position - transform.position;
        Quaternion newLookRotation = Quaternion.LookRotation(diection);
        Vector3 rotation = Quaternion.Lerp(pivot.rotation, newLookRotation, Time.deltaTime * rotateSpeed).eulerAngles;

        return Quaternion.Euler(0f, rotation.y, 0f);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
        if(target != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, target.transform.position);
        }
    }
}
