using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f; //how often the enemy attacks
    public int attackDamage = 10; //how much damage the enemy gives


    Animator anim;
    GameObject player; //gets the player
    PlayerHealth playerHealth; //gets player heatlh script for player damage
    EnemyHealth enemyHealth; //gets enemy health for enemy damage
    bool playerInRange; //checks to see if the player is in range
    float timer; //keeps everything in sync


    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag ("Player"); //gets the player
        playerHealth = player.GetComponent <PlayerHealth> (); //gets the player script
        enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent <Animator> ();
    }


    void OnTriggerEnter (Collider other) //on the trigger set earlier
    {
        Debug.Log("trigger collision");

        Debug.Log(other.gameObject);

        if(other.gameObject == player) //if it collides with player
        {
            playerInRange = true; //sets player in range to true
        }
    }


    void OnTriggerExit (Collider other) //when player is not in range anymore
    {
        if(other.gameObject == player)
        {
            playerInRange = false; //changes the player in range to false
        }
    }


    void Update ()
    {
        timer += Time.deltaTime; //updates the timer

        if(timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0) //if timer is greater than time between attacks then attack && player is in range && enemy isn't dead
        {
            Debug.Log("Attacking");
            Attack (); //calls attack function
        }

        if(playerHealth.currentHealth <= 0) //player dies
        {
            anim.SetTrigger ("PlayerDead"); //triggers the player dead things
        }
    }


    void Attack ()
    {
        Debug.Log("Attacking");

        timer = 0f; //resets timer

        if(playerHealth.currentHealth > 0) //check to see if playaer is alive
        {
            playerHealth.TakeDamage (attackDamage); //uses player script to take damage based on enemies damage var
        }
    }
}
