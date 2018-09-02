using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public PlayerHealth playerHealth; //takes this so it doesn't keep spawning after player dies
    public GameObject enemy; //which enemy to spawn
    public float spawnTime = 3f; //how often to spawn
    public Transform[] spawnPoints; //spawn points


    void Start ()
    {
        InvokeRepeating ("Spawn", spawnTime, spawnTime); //no need for timer because it repeats (functionname, wait on first, wait on repeat)
    }


    void Spawn ()
    {
        if(playerHealth.currentHealth <= 0f)
        { //if dead just return
            return;
        }

        int spawnPointIndex = Random.Range (0, spawnPoints.Length); //random spawn points from the spawn point array

        Instantiate (enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation); // instantiates the enemies at random spawn points
    }
}
