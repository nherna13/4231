using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour
{
    [SerializeField] float delay = 3f;
    [SerializeField] float damageRadis = 20f;
    [SerializeField] float explosionForce = 1200f;

    float countdown;

    bool hasExploded = false;
    public bool hasBeenThrown = false;

    public enum ThrowableType
    {
        None,
        Grenade,
        Smoke_Grenade
    }

    public ThrowableType throwableType;

    private void Start() 
    {
       countdown = delay; 
    }


    private void Update() 
    {
        if (hasBeenThrown)
        {
            countdown -= Time.deltaTime;
            if (countdown <= 0f && !hasExploded)
            {
                Explode();
                hasExploded = true;
            }
        }
    }

    private void Explode()
    {
        GetThrowableEffect();

        Destroy(gameObject);
    }


    private void GetThrowableEffect()
    {
        switch (throwableType)
        {
           case ThrowableType.Grenade:
                GrenadeEffect();
                break; 
            case ThrowableType.Smoke_Grenade:
                SmokeGrenadeEffect();
                break;
            
        }
    }

    private void SmokeGrenadeEffect()
    {
        //visual effect
        GameObject SmokeEffect = GlobalReferences.Instance.smokeGrenadeEffect;
        Instantiate(SmokeEffect, transform.position, transform.rotation);

        // playsound
        SoundManager.Instance.throwablesChannel.PlayOneShot(SoundManager.Instance.grenadeSound);

        //physical effect
        Collider[] colliders = Physics.OverlapSphere(transform.position, damageRadis);
        foreach(Collider objectInRange in colliders)
        {
            Rigidbody rb = objectInRange.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // apply blindness to enemies
            }
        }
    }

    private void GrenadeEffect()
    {
        //visual effect
        GameObject explosionEffect = GlobalReferences.Instance.grenadeExplosionEffect;
        Instantiate(explosionEffect, transform.position, transform.rotation);

        // playsound
        SoundManager.Instance.throwablesChannel.PlayOneShot(SoundManager.Instance.grenadeSound);

        //physical effect
        Collider[] colliders = Physics.OverlapSphere(transform.position, damageRadis);
        foreach(Collider objectInRange in colliders)
        {
            Rigidbody rb = objectInRange.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, damageRadis);
            }

            if (objectInRange.gameObject.GetComponent<Enemy>())
            {
                objectInRange.gameObject.GetComponent<Enemy>().TakeDamage(100);
            }

        }
    }
}
