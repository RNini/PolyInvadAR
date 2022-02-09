using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movementManager : MonoBehaviour
{
    // Movement related helper variables storing valid positions and position indices
    public GameObject[] enemyPosOption0;                    // Array of empty objects, representing valid movement positions
        public int[] positionIntOption0;                        // Array of indices for keeping track of current position
        public string[] directionOption0;                       // Array of strings keeping track of movement direciton, for animation purposes
        
        public GameObject[] enemyPosOption1;
            public int[] positionIntOption1;
            public string[] directionOption1;

        public GameObject[] enemyPosOption2;
                public int[] positionIntOption2;
                public string[] directionOption2;

        public GameObject[] enemyPosOption3;
                public int[] positionIntOption3;
                public string[] directionOption3;

        public GameObject[] enemyPosOption4;
                public int[] positionIntOption4;
                public string[] directionOption4;

                public GameObject[] enemyPosOption5;
                public int[] positionIntOption5;
                public string[] directionOption5;

        public GameObject[] enemyPosOption6;
                public int[] positionIntOption6;
                public string[] directionOption6;

        public GameObject[] enemyPosOption7;
                public int[] positionIntOption7;
                public string[] directionOption7;

        public GameObject[] enemyPosOption8;
                public int[] positionIntOption8;
                public string[] directionOption8;

    // For keeping track of current position
    public int positionInt;

        // Variables to assist with lerping
        public Transform enemyParent;                  // Enemy parent object that will be moved

        public Vector3 origPos;                         // Original position
            public Vector3 targetPos;                       // Dstination position
            public Vector3 nuPos;                           // Temp variable for holding LERP values

        public float timeToMove;                            // Lerping time, adjustable in the inspector

        public float timeToWaitMin;                            // Time taken between actions, expressed as a range
            public float timeToWaitMax;

        public float elapsedTime;

    // Game object reference for accessing animation manager functions
    public GameObject animationManager;

        // Array of strings for keeping track of state: attacking, movement direction, etc.
        public int currentState;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(enemyBehavior());        // Starts enemy behavior loop
    }

        public IEnumerator enemyBehavior()
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
                    getPositionInt();
                }
        }

    // Functions for enemy attacking
        // Coroutine for attacking
        public IEnumerator enemyAttack()
        {
            animationManager.GetComponent<animationManager>().enemyAttack();

            yield return new WaitForSecondsRealtime(Random.Range(timeToWaitMin, timeToWaitMax));        // Wait a random amount of time to allow animation to loop

            // Reset the cycle
            animationManager.GetComponent<animationManager>().endMove();
                StartCoroutine(enemyBehavior());
        }


    // Functions for enemy movement
        // Function for pushing the appropriate arrays for choosing a new position
        public void getPositionInt()
        {
            currentState = 1;

            if (positionInt == 0)
            {
                positionSolver(positionIntOption0, enemyPosOption0, directionOption0);
            }

                else if (positionInt == 1)
                {
                    positionSolver(positionIntOption1, enemyPosOption1, directionOption1);
                }

                else if (positionInt == 2)
                {
                    positionSolver(positionIntOption2, enemyPosOption2, directionOption2);
                }

                else if (positionInt == 3)
                {
                    positionSolver(positionIntOption3, enemyPosOption3, directionOption3);
                }

                else if (positionInt == 4)
                {
                    positionSolver(positionIntOption4, enemyPosOption4, directionOption4);
                }

                else if (positionInt == 5)
                {
                    positionSolver(positionIntOption5, enemyPosOption5, directionOption5);
                }

                else if (positionInt == 6)
                {
                    positionSolver(positionIntOption6, enemyPosOption6, directionOption6);
                }

                else if (positionInt == 7)
                {
                    positionSolver(positionIntOption7, enemyPosOption7, directionOption7);
                }

                else if (positionInt == 8)
                {
                    positionSolver(positionIntOption8, enemyPosOption8, directionOption8);
                }

        }

            public void positionSolver(int[] positionOptions, GameObject[] positions, string[] directions)
            {
                GameObject tempTarget;
       
                // Grab's the first position in the appropriate array
                origPos = positions[0].transform.position;

                // Choose a new position randomly from the available options
                positionInt = Random.Range(1, positions.Length);                         // Choose a random index from available position choices
                    tempTarget = positions[positionInt];                                        // Assign the empty using the new position int
                        targetPos = tempTarget.transform.position;                              // Grab its position as the new position
                        string moveDir = directions[positionInt];                               // Grab the movement direction

            StartCoroutine(enemyMove(moveDir));
            }

        public IEnumerator enemyMove(string moveDirection)
        {
            elapsedTime = 0;

            if (moveDirection == "Left")
            {
                animationManager.GetComponent<animationManager>().moveLeft();
            }

                else if (moveDirection == "Right")
                {
                    animationManager.GetComponent<animationManager>().moveRight();
                }

                else if (moveDirection == "Forward")
                {
                    animationManager.GetComponent<animationManager>().moveForward();
                }

                else if (moveDirection == "Back")
                {
                    animationManager.GetComponent<animationManager>().moveBack();
                }

            while (elapsedTime < timeToMove)
            {
            elapsedTime += Time.deltaTime;            

            nuPos = Vector3.Lerp(origPos, targetPos, elapsedTime / timeToMove);

            enemyParent.position = nuPos;

            Debug.Log("moving to " + nuPos);
            }

            yield return null;

            // Sets parent to target position in case of partial position transformation
            enemyParent.position = targetPos;

            // Ends the movement animation cycle
            animationManager.GetComponent<animationManager>().endMove();
                currentState = 0;

            // Reset the cycle
            StartCoroutine(enemyBehavior());
        }
}
