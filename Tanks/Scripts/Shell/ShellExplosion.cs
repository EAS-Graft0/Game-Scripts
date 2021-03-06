﻿using UnityEngine;

public class ShellExplosion : MonoBehaviour
{
    public LayerMask m_TankMask;
    public ParticleSystem m_ExplosionParticles;       
    public AudioSource m_ExplosionAudio;              
    public float m_MaxDamage = 100f;                  
    public float m_ExplosionForce = 1000f;            
    public float m_MaxLifeTime = 2f;                  
    public float m_ExplosionRadius = 5f;              


    private void Start()
    {
        Destroy(gameObject, m_MaxLifeTime);
    }


    private void OnTriggerEnter(Collider other)
    {
        // Find all the tanks in an area around the shell and damage them.
        Collider[] colliders = Physics.OverlapSphere(transform.position, m_ExplosionRadius, m_TankMask);
        //can also be any destrcutable scenery, should have health and states of destroyed
        for(int i = 0; i < colliders.Length; i++)
        {
            Rigidbody targetRigidBody = colliders[i].GetComponent<Rigidbody>();

            if (!targetRigidBody)
                continue;

            targetRigidBody.AddExplosionForce(m_ExplosionForce, transform.position, m_ExplosionRadius);

            TankHealth targetHealth = targetRigidBody.GetComponent<TankHealth>();

            if (!targetHealth)
                continue;

            float Damage = CalculateDamage(targetRigidBody.position);

            targetHealth.TakeDamage(Damage);
        }

        //explosion code 
        m_ExplosionParticles.transform.parent = null;

        m_ExplosionParticles.Play();
        m_ExplosionAudio.Play();

        Destroy(m_ExplosionParticles.gameObject, m_ExplosionParticles.main.duration);
        Destroy(gameObject);

    }


    private float CalculateDamage(Vector3 targetPosition)
    {
        // Calculate the amount of damage a target should take based on it's position.

        Vector3 ExplosionToTarget = targetPosition - transform.position;

        float ExplosionDistance = ExplosionToTarget.magnitude; 

        float RelativeDistance = (m_ExplosionRadius - ExplosionDistance) / m_ExplosionRadius;

        float Damage = RelativeDistance * m_MaxDamage;

        Damage = Mathf.Max(0f, Damage);

        return Damage;
    }
}