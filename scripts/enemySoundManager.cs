using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySoundManager : MonoBehaviour
{

    public AudioSource enemyAudio;
    public AudioClip[] enemyAudioClips;

    public void enemyAttack()
    {
        // Plays initial clip
        enemyAudio.PlayOneShot(enemyAudioClips[0], 2f);
    }

}
