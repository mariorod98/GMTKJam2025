using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    /******************************************************************************/

    /*** START SINGLETON BLOCK ***/
    private static AudioManager m_instance = null;
    public static AudioManager Instance
    {
        get { return m_instance; }
    }

    private void Awake()
    {
        m_instance = this;
    }
    /*** END SINGLETON BLOCK ***/

    /******************************************************************************/

    [Header("AudioClips")]
    public List<AudioClip> m_collisionWithBigBowlClips;
    public List<AudioClip> m_collisionWithSmallBowlClips;
    public List<AudioClip> m_collisionWithLoopClips;
    public List<AudioClip> m_startRoundClips;
    public List<AudioClip> m_endRoundClips;

    [Header("AudioSources")]
    public AudioSource m_startRoundAudioSource;
    public List<AudioSource> m_endRoundAudioSources;

    public void StartRound(int n_loops)
    {
        if(n_loops < 50) 
        {
            m_startRoundAudioSource.PlayOneShot(m_startRoundClips[0]);
        }
        else if(n_loops < 100)
        {
            m_startRoundAudioSource.PlayOneShot(m_startRoundClips[1]);
        }
        else if(n_loops < 500)
        {
            m_startRoundAudioSource.PlayOneShot(m_startRoundClips[2]);
        }
        else
        {
            m_startRoundAudioSource.PlayOneShot(m_startRoundClips[3]);
        }
    }

    public void EndRound(int n_round)
    {
        int n_sounds = 1; // < 5

        if (n_round < 10)
        {
            n_sounds = 3;
        }
        if (n_round < 15)
        {
            n_sounds = 5;
        }
        else
        {
            n_sounds = 8;
        }

        StartCoroutine(PlayEndRoundSound(n_sounds));
        for (int i = 0; i < n_sounds; i++)
        {
            float wait = Random.Range(0.25f, 0.45f);
            AudioSource source = m_endRoundAudioSources[i];
            AudioClip clip = m_endRoundClips[Random.Range(0, m_endRoundClips.Count)];


        }
    }
    private IEnumerator PlayEndRoundSound(int n_sounds)
    {
        for (int i = 0; i < n_sounds; i++)
        {
            float wait = Random.Range(0.0f, 1.0f);
            yield return new WaitForSeconds(wait);
            AudioSource source = m_endRoundAudioSources[i];
            AudioClip clip = m_endRoundClips[Random.Range(0, m_endRoundClips.Count)];

            source.PlayOneShot(clip);

        }

    }

}
