using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuManager : MonoBehaviour
{
    public Animator menuAnimator;

    public int menuState;
    
    // Start is called before the first frame update
    void Start()
    {
        menuState = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void menuInteraction()
    {
        // Activate the panel on first button press
        if (menuState == 0) { menuAnimator.Play("Activate"); }
            // Deactivate the panel after first button press
            else { menuAnimator.Play("Deactivate"); }

        // Cycle through the menuState variable
        if (menuState == 0) { menuState++; }
            else { menuState = 0; }
    }
}
