using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class animationManager : MonoBehaviour
{
    // General variables
    public Animator objectAnimator;
        public ParticleSystem[] characterParticles;

    // Player-specific variables
    public GameObject thrusterButton;
        public int attackState = 0;
        public GameObject audioObject;

    public GameObject dodgeButton;
        
    public Color[] activeColor;

    // Start is called before the first frame update
    void Start()
    {
        // Prevents the enemy particles from playing
        if (this.gameObject.tag == "enemy") { characterParticles[0].Stop(); }
            
            else if (this.gameObject.tag == "Player")
            {
                // Turns off after-burners until triggered by player
                for (int i = 0; i < 3; i++)
                {
                    characterParticles[i].Stop();
                }            
            }
    }

    // Update is called once per frame
    void Update()
    {
        // Reset the attack animation if enemy is attacking, but particle system finishes
        if (this.gameObject.tag == "enemy" && objectAnimator.GetCurrentAnimatorStateInfo(0).IsName("Attack") == true && characterParticles[0].isEmitting == false)
        {
            enemyAttackOff();

            // Resets the button color in such an ugly coding fashion
            endMove();
        }
    }

    // Functions for setting animator bools
    public void setLeft()
    {
        objectAnimator.SetBool("isLeft", true);
            objectAnimator.SetBool("isRight", false);
    }

        public void setRight()
        {
            objectAnimator.SetBool("isLeft", false);
                objectAnimator.SetBool("isRight", true);
        }

    public void setAttack()
    {
        if (attackState == 0)
        {
            objectAnimator.SetTrigger("Attack");

            // Plays thruster sound
            audioObject.GetComponent<playerSoundManager>().thrusterStart();
        }

        else if (attackState == 1)
        {
            objectAnimator.SetTrigger("StopAttack");            
        }
                       
    }

    // Function for thruster animation triggers
        public void thrustersToggle()
        {

            if (attackState == 0)
            {
                // Turns on after-burners
                for (int i = 0; i < 3; i++)
                {
                    characterParticles[i].Play();
                }

                thrusterButton.GetComponent<Button>().image.color = activeColor[1];

                attackState = 1;
            }
                
            else
            {
                // Turns off after-burners
                for (int i = 0; i < 3; i++)
                {
                    characterParticles[i].Stop();
                }

                thrusterButton.GetComponent<Button>().image.color = activeColor[0];

                attackState = 0;

                // Ends thruster sound loop
                audioObject.GetComponent<playerSoundManager>().thrusterFinish();
            }
                
        }


    public void setDodge()
    {
        objectAnimator.SetTrigger("BarrelRoll");
        /*
        // Checks the direction before playing appropriate dodge animation
        if (objectAnimator.GetBool("isLeft") == true)
        {
            objectAnimator.Play("DodgeLeft");
        }

            else { objectAnimator.Play("DodgeRight"); }
        
        objectAnimator.SetBool("Dodge",true);
        */

        // Set button color
        dodgeButton.GetComponent<Button>().image.color = activeColor[1];
    }

        public void unsetDodge()
        {
            objectAnimator.SetBool("Dodge", false);
            
            // Reset button color
            dodgeButton.GetComponent<Button>().image.color = activeColor[0];
        }

    // Functions for enemy movement
    public void moveLeft()
    {
        objectAnimator.SetTrigger("movingLeft");
    }

        public void moveRight()
        {
            objectAnimator.SetTrigger("movingRight");
        }

        public void moveForward()
        {
            objectAnimator.SetTrigger("movingForward");
        }

        public void moveBack()
        {
            objectAnimator.SetTrigger("movingBack");
        }

    public void enemyAttack()
    {
        objectAnimator.SetTrigger("Attack");
            characterParticles[0].Play();            // release particles
    }

        public void enemyAttackOff()
        {
        objectAnimator.SetTrigger("stopAttack");
        }

    public void endMove()
    {
        objectAnimator.SetTrigger("endMove");
    }
}
