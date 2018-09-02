using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100; //health when enemy gets loaded in
    public int currentHealth; //current health to be updated 
    public float sinkSpeed = 2.5f; //sinks into floor
    public int scoreValue = 10; //how many points you get for killing this enemy
    public AudioClip deathClip; //audio to play on death


    Animator anim;
    AudioSource enemyAudio;
    ParticleSystem hitParticles; //particles to show on hit
    CapsuleCollider capsuleCollider; //takes in the collider for this enemy because this is the actual enemy that moves
    bool isDead;
    bool isSinking;


    void Awake ()
    {
        anim = GetComponent <Animator> ();
        enemyAudio = GetComponent <AudioSource> ();
        hitParticles = GetComponentInChildren <ParticleSystem> (); //finds component inside this child (finds first if not specified)
        capsuleCollider = GetComponent <CapsuleCollider> ();

        currentHealth = startingHealth; //set health to start health on load
    }


    void Update ()
    {
        if(isSinking)
        {
            transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime); //sinks enemy into floor over time
        }
    }


    public void TakeDamage (int amount, Vector3 hitPoint) //how much damage it takes and then where the hit was
    {
        if(isDead) 
            return;

        enemyAudio.Play ();

        currentHealth -= amount;
            
        hitParticles.transform.position = hitPoint;
        hitParticles.Play();

        if(currentHealth <= 0)
        {
            Death ();
        }
    }


    void Death ()
    {
        isDead = true;

        capsuleCollider.isTrigger = true;

        anim.SetTrigger ("Dead");

        enemyAudio.clip = deathClip;
        enemyAudio.Play ();
    }


    public void StartSinking () //public to call on animation event - called on the death animation
    {
        GetComponent <UnityEngine.AI.NavMeshAgent> ().enabled = false; //not part of the mesh anymore - doesn't hide game object only the mesh agent
        GetComponent <Rigidbody> ().isKinematic = true; //change to kinematic type to ignore this object when rendering
        isSinking = true;
        ScoreManager.score += scoreValue;
        Destroy (gameObject, 2f); //destroy game object after 2 seconds
    }
}
