using System;
using UnityEngine;

public class Bullet : MonoBehaviour {

    private GameObject target;
    public GameObject hitEffect;
    public float speed = 60f;

    public void SetTarget(GameObject target)
    {
        this.target = target;
    }

    // Update is called once per frame
    public void FixedUpdate () {
        
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = target.transform.position - transform.position;

        float frameDistance = speed* Time.fixedDeltaTime;

        if(direction.magnitude <= frameDistance)
        {
            HitTarget();
            return;
        }

        transform.Translate(direction.normalized * frameDistance, Space.World);
    }

    private void HitTarget()
    {
        GameObject effect = Instantiate(hitEffect, transform.position, transform.rotation);
        Destroy(target);
        Destroy(effect, 2f);
        Destroy(gameObject);
    }
}
