using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class movementManager_v2 : MonoBehaviour
{
    // Enemy parent object that will be moved
    public Animator enemyParent;                  

        public float timeToWaitMin;                            // Time taken between actions, expressed as a range
            public float timeToWaitMax;

        // Child game object representing the enemy unit
        public GameObject enemyChild;

    // Game object reference for accessing animation manager functions
    public GameObject animationManager;

        // Array of strings for keeping track of state: attacking, movement direction, etc.
        public int currentState;

        public string[] moveDirections;         // Array of strings to keep track of movement directions
            public string currentDirection;         // Temp variable for storing movement direction

        // UI variables for controlling button reactivity        
        public GameObject enemyButton;           
            public Color[] activeColor;


    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(enemyBehavior());        // Starts enemy behavior loop
        enemyParent.SetTrigger("deployEnemy");

    }

        /*public IEnumerator enemyBehavior()
        {
            currentState = Random.Range(0, 10);                                                          // Choose a random int, that we will use to determine next move
        
            yield return new WaitForSecondsRealtime(Random.Range(timeToWaitMin, timeToWaitMax));        // Wait a random amount of time before continuing

            if (currentState == 0)
            {
                // Do nothing, as this is the idle state

                // Restart the loop
                StartCoroutine(enemyBehavior());
        }

                else if (currentState == 1)
                {
                    // Start attacking!
                    StartCoroutine(enemyAttack());
                }

                else
                {
                    // Start moving!
                    enemyMove();
                }
        }*/

    public void enemyBehavior()
    {
        // Prevents the player from activating another state before the last state is finished
        if (currentState > 1)
        {
            // Do nothing, as the enemy is still moving
        }

            // The enemy can act
            else
            {
                if (currentState == 1)
                {
                    // Interupt the attack loop
                    currentState = 0;
                        animationManager.GetComponent<animationManager>().enemyAttackOff();

                    // Resets the button color
                    enemyButton.GetComponent<Button>().image.color = activeColor[0];
                }
            
                currentState = Random.Range(1, 10);             // Choose a random int, that we will use to determine next move

                if (currentState == 1) { enemyAttack(); }       // Start attacking!

                    else { enemyMove(); }                           // Start moving!
            }

    }

    // Functions for enemy attacking
    // Coroutine for attacking
    public void enemyAttack()
    {
        animationManager.GetComponent<animationManager>().enemyAttack();

        // Sets the button color
        enemyButton.GetComponent<Button>().image.color = activeColor[1];

        // Plays the appropriate sound
        enemyChild.GetComponent<enemySoundManager>().enemyAttack();

        //StartCoroutine(enemyBehavior());           
    }


    // Functions for enemy movement
            public void enemyMove()
            {
                // Choose a new position randomly from the available options
                currentDirection = moveDirections[Random.Range(0, moveDirections.Length)];                         // Choose a random direction from available choices

                    // Trigger appropriate animations
                    if (currentDirection == "Left")
                    {
                        // Checks for illegal positions
                            if (!enemyParent.GetCurrentAnimatorStateInfo(0).IsName("Pos6") &&
                                !enemyParent.GetCurrentAnimatorStateInfo(0).IsName("Pos7") &&
                                !enemyParent.GetCurrentAnimatorStateInfo(0).IsName("Pos8"))
                            {
                                    enemyParent.SetTrigger("MoveLeft");

                                    // Sets the button color
                                    enemyButton.GetComponent<Button>().image.color = activeColor[1];
                            }

                            // Restarts the move function if an illegal move is chosen
                            else { enemyMove(); }
                        
                    }

                        else if (currentDirection == "Right")
                        {
                            if (!enemyParent.GetCurrentAnimatorStateInfo(0).IsName("Pos2") &&
                                !enemyParent.GetCurrentAnimatorStateInfo(0).IsName("Pos3") &&
                                !enemyParent.GetCurrentAnimatorStateInfo(0).IsName("Pos4"))
                            {
                                    enemyParent.SetTrigger("MoveRight");

                                    // Sets the button color
                                    enemyButton.GetComponent<Button>().image.color = activeColor[1];
                            }

                            // Restarts the move function if an illegal move is chosen
                            else { enemyMove(); }
                        }

                        else if (currentDirection == "Forward")
                        {
                            if (!enemyParent.GetCurrentAnimatorStateInfo(0).IsName("Pos8") &&
                                !enemyParent.GetCurrentAnimatorStateInfo(0).IsName("Pos1") &&
                                !enemyParent.GetCurrentAnimatorStateInfo(0).IsName("Pos2"))
                            {
                                    enemyParent.SetTrigger("MoveForward");

                                    // Sets the button color
                                    enemyButton.GetComponent<Button>().image.color = activeColor[1];
                            }

                            // Restarts the move function if an illegal move is chosen
                            else { enemyMove(); }
                        }

                        else if (currentDirection == "Back")
                        {
                            if (!enemyParent.GetCurrentAnimatorStateInfo(0).IsName("Pos4") &&
                                !enemyParent.GetCurrentAnimatorStateInfo(0).IsName("Pos5") &&
                                !enemyParent.GetCurrentAnimatorStateInfo(0).IsName("Pos6"))
                            {
                                    enemyParent.SetTrigger("MoveBack");

                                    // Sets the button color
                                    enemyButton.GetComponent<Button>().image.color = activeColor[1];
                            }

                            // Restarts the move function if an illegal move is chosen
                            else { enemyMove(); }
                        }
                    
                    currentState = 0;

                    // Reset the cycle
                    //StartCoroutine(enemyBehavior());
            }

    // Functions for controlling game object movement animations, which will be used with animation triggers
    public void tiltLeft()
    {
        animationManager.GetComponent<animationManager>().moveLeft();
    }

        public void tiltRight()
        {
            animationManager.GetComponent<animationManager>().moveRight();
        }

        public void tiltBack()
        {
            animationManager.GetComponent<animationManager>().moveBack();
        }

        public void tiltForward()
        {
            animationManager.GetComponent<animationManager>().moveForward();
        }

    // This is triggered at the end of move animations to end tilt animations and rest the current state index
    public void endMove()
    {
        animationManager.GetComponent<animationManager>().endMove();
            currentState = 0;

        // Resets the button color
        enemyButton.GetComponent<Button>().image.color = activeColor[0];
    }
}
