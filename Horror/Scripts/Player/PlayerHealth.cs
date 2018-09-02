using UnityEngine;
using UnityEngine.UI; //needed for ui components
using System.Collections;
using UnityEngine.SceneManagement;


public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100; //could this be linked to the slider? used for when respawning
    public int currentHealth; //changes
    public Slider healthSlider; //the slider
    public Image damageImage; //shows image on hurt
    public AudioClip deathClip; //plays sound on death
    public float flashSpeed = 5f; //damage image flashes on screen, increase when health gets lower
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f); //simply flashes red, can make it darker red for less health


    Animator anim; //for the animation we'll play
    AudioSource playerAudio; //for the sound we will play
    PlayerMovement playerMovement; //reference to a script
    PlayerShooting playerShooting;
    bool isDead; //check for death - does stuff on death
    bool damaged; //check for damage - plays sound and shows image on damage
    

    void Awake ()
    {
        anim = GetComponent <Animator> (); //gets the animator
        playerAudio = GetComponent <AudioSource> (); //gets the sound
        playerMovement = GetComponent <PlayerMovement> (); //get the script
        playerShooting = GetComponentInChildren <PlayerShooting> ();
        currentHealth = startingHealth; //when you start it refreshes health
    }


    void Update ()
    {
        if(damaged) //if player takes damage
        {
            damageImage.color = flashColour; //show the red image over the ui screen (alpha currently 0) could just set to red rather than having variable - maybe have the r in rgb = playerhealth / 1 to make it darker as health is lower
        }
        else
        {
            damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        } // takes away the damage image gradually
        damaged = false;
    } //reset the damaged var so the above condition isn't true


    public void TakeDamage (int amount) //public function so that it can be used in other files (enemy attack)
    { //function for taking damage taking in the amount of damage which is different depending on enemy
        damaged = true; //sets the damaged var so we see the red colour

        Debug.Log("Taking Damage");

        currentHealth -= amount; //take away player health

        healthSlider.value = currentHealth; //sets the slider to the current health

        playerAudio.Play (); //play the audio sound for damage

        Debug.Log("Getting attacked");

        if(currentHealth <= 0 && !isDead) //checks player health if its 0 then die
        {
            Death ();
        }
    }


    void Death () //function for death
    {
        isDead = true; //sets the is dead var 

        playerShooting.DisableEffects ();

        anim.SetTrigger ("Die"); //plays the death animation for player

        playerAudio.clip = deathClip; //gets the death sound for the player
        playerAudio.Play (); //plays the death sound for player

        playerMovement.enabled = false; //stops player from moving
        playerShooting.enabled = false;
    }


    public void RestartLevel () //restart level 
    {
        SceneManager.LoadScene (0); //reloads the scene
    }
}
