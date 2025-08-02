using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loop : MonoBehaviour
{
    public LoopColor m_loopColor;
    public AudioSource m_audioSource;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("BigBowl") && collision.relativeVelocity.sqrMagnitude > 25.0f)
        {

            if(!m_audioSource.isPlaying)
            {
                AudioClip clip = AudioManager.Instance.m_collisionWithBigBowlClips[Random.Range(0, AudioManager.Instance.m_collisionWithBigBowlClips.Count)];
                m_audioSource.PlayOneShot(clip);
            }
        }
        else if (collision.gameObject.CompareTag("Bowl") && collision.relativeVelocity.sqrMagnitude > 20.0f)
        {

            if (!m_audioSource.isPlaying)
            {
                AudioClip clip = AudioManager.Instance.m_collisionWithSmallBowlClips[Random.Range(0, AudioManager.Instance.m_collisionWithSmallBowlClips.Count)];
                m_audioSource.PlayOneShot(clip);
            }
        }
        else if (collision.gameObject.CompareTag("Loop") && collision.relativeVelocity.sqrMagnitude > 20.0f)
        {
            AudioClip clip = AudioManager.Instance.m_collisionWithLoopClips[Random.Range(0, AudioManager.Instance.m_collisionWithLoopClips.Count)];
            m_audioSource.PlayOneShot(clip);
        }
    }
}
