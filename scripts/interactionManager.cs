using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class interactionManager : MonoBehaviour
{
    // Enemy related variables
    public GameObject enemyParent;
        public Animator enemyActive;

        // Variables for instantiating/destroying enemy prefabs
        /*public GameObject tempEnemy;
            public GameObject enemyPrefab;*/

    // Player character variables
    public Animator playerAnimator;

    // UI variables
    public Button[] uiButtons;
    
        /*public prefabManager prefabulousManager;*/                                       // Holds the prefab manager object from the scene

    // Assigns button functions when this group is instantiated by marker image
    private void Start()
    {
        // Links button variables from the camera, locally to the prefab
        /*prefabulousManager = GameObject.Find("AR Session Origin/AR Camera").GetComponent<prefabManager>();

        uiButtons[0] = prefabulousManager.interactionButtons[0];
            uiButtons[1] = prefabulousManager.interactionButtons[1];
            uiButtons[2] = prefabulousManager.interactionButtons[2];*/

        // Removes any click-able functions that may already be assigned
        for (int i = 0; i < uiButtons.Length; i++)
        {
            uiButtons[i].onClick.RemoveAllListeners();
        }

        // Adds function associations
        uiButtons[0].onClick.AddListener(playerAnimator.GetComponent<animationManager>().setAttack);
            uiButtons[1].onClick.AddListener(barrelRoll);
            uiButtons[2].onClick.AddListener(enemyParent.GetComponent<movementManager_v2>().enemyBehavior);

    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void barrelRoll()
    {
        // Only runs the function is the attack animation is currently not running
        if (!playerAnimator.GetCurrentAnimatorStateInfo(1).IsName("DodgeLeft") && !playerAnimator.GetCurrentAnimatorStateInfo(1).IsName("DodgeRight"))
        {
            playerAnimator.GetComponent<animationManager>().setDodge();
        }
        
    }

}
