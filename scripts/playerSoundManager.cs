using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerSoundManager : MonoBehaviour
{

    public AudioSource playerAudio;
    public AudioClip[] playerAudioClips;

    public void thrusterStart()
    {
        // Plays initial clip
        playerAudio.PlayOneShot(playerAudioClips[0], .75f);

        // Queues the looping audio clip
        playerAudio.clip = playerAudioClips[1];                             // Selects the loopable clip
        playerAudio.loop = true;                                            // Sets looping
        playerAudio.volume = .6f;
        playerAudio.PlayScheduled(AudioSettings.dspTime + 1.7);             // Plays the clip 1.8 seconds afterwards

    }

    public void thrusterFinish()
    {
        playerAudio.volume = .25f;

        // Plays the final clip of the thruster sound loop
        playerAudio.PlayOneShot(playerAudioClips[2], .75f);

        /*
        // Queues the background rocket sound
        playerAudio.clip = playerAudioClips[3];                             // Selects the loopable clip
            playerAudio.loop = true;                                            // Sets looping
            playerAudio.PlayScheduled(AudioSettings.dspTime + 1.8);             // Plays the clip 1.8 seconds afterwards
        */
    }

}
