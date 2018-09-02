using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    Transform player; //this is for following the player //could make this pulic and drag in what we want it to follow //could also set this to closest gameobject for enemies to attack each other
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    UnityEngine.AI.NavMeshAgent nav; //this is the navmesh we've created


    void Awake ()
    {
        //can also use getcomponent I think?
        player = GameObject.FindGameObjectWithTag ("Player").transform; //this finds the transform of the player (transform contains position)
        playerHealth = player.GetComponent <PlayerHealth> ();
        enemyHealth = GetComponent <EnemyHealth> ();
        nav = GetComponent <UnityEngine.AI.NavMeshAgent> (); //this gets the mesh agent
    }


    void Update () //doesn't need to keep upto date with physics or time so using regular update
    {
        if(enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
        {
            nav.SetDestination (player.position); //simply sets the ai navmesh which is the actual thing that moves to go to the player's position
        }
        else
        {
            nav.enabled = false;
         } //stop moving if the player or enemy is alive
    }
}
