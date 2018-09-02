using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public int damagePerShot = 20; //player damage - can increase on level up or new gun
    public float timeBetweenBullets = 0.15f; //shooting speed
    public float range = 100f; //range of shot
    
    float timer; //timer for time between bullets
    Ray shootRay = new Ray(); //creates new ray to check for hit
    RaycastHit shootHit; //if hits
    int shootableMask; //can only shoot at shootable objects
    ParticleSystem gunParticles; //gets particles to activate on hit
    LineRenderer gunLine; //the shot
    AudioSource gunAudio; //gun shot sound
    Light gunLight; //gun shot light
    float effectsDisplayTime = 0.2f; //how long effects are shown for


    void Awake ()
    {
        shootableMask = LayerMask.GetMask ("Shootable"); //makes shootable mask anything on the shootable layer
        gunParticles = GetComponent<ParticleSystem> ();
        gunLine = GetComponent <LineRenderer> ();
        gunAudio = GetComponent<AudioSource> ();
        gunLight = GetComponent<Light> ();
    }


    void Update ()
    {
        timer += Time.deltaTime; //increases timer

		if(Input.GetButton ("Fire1") && timer >= timeBetweenBullets && Time.timeScale != 0)
        { //if firing (fire1 is left mouse) and timer is more than time to shoot
            Shoot ();
        }

        if(timer >= timeBetweenBullets * effectsDisplayTime)
        { //resets effects
            DisableEffects ();
        }
    }


    public void DisableEffects ()
    {
        gunLine.enabled = false;
        gunLight.enabled = false; //hides gun shot line and gun light
    }


    void Shoot ()
    {
        timer = 0f; //resets timer

        gunAudio.Play (); //plays gunshot

        gunLight.enabled = true; //shows gunlight

        gunParticles.Stop (); //stops particles if shown
        gunParticles.Play (); //plays particles

        gunLine.enabled = true; //shows gunshot
        gunLine.SetPosition (0, transform.position); //puts gunline back to where it starts

        shootRay.origin = transform.position;  //from where the object is
        shootRay.direction = transform.forward; 

        if(Physics.Raycast (shootRay, out shootHit, range, shootableMask)) //if hit something which is shootable and ni range
        {
            EnemyHealth enemyHealth = shootHit.collider.GetComponent <EnemyHealth> (); //take off enemy health
            if(enemyHealth != null) //if enemy has health
            {
                enemyHealth.TakeDamage (damagePerShot, shootHit.point); //run take damage
            }
            gunLine.SetPosition (1, shootHit.point);
        }
        else
        {
            gunLine.SetPosition (1, shootRay.origin + shootRay.direction * range); //shoots bullet from gun to where you click upto the range if nothing is between
        }
    }
}
    