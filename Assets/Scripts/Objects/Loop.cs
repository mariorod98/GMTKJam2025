using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loop : MonoBehaviour
{
    public LoopColor m_loopColor;
    public AudioSource m_audioSource;

    public bool m_isInBowl = false;
    public int m_score = 1;
    private float m_timeInBowl = 0.0f;
    private float m_timeToDespawn = 5.0f;

    public void OnEnterBowl(int score)
    {
        m_isInBowl = true;
        m_timeInBowl = 0.0f;
        m_score = score;
    }

    public void ResetState()
    {
        m_isInBowl = false;
        m_timeInBowl = 0.0f;
        m_score = 0;
    }

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

    private void Update()
    {
        if(GameManager.Instance.IsPlaying() && m_isInBowl)
        {
            m_timeInBowl += Time.deltaTime;
            if(m_timeInBowl > m_timeToDespawn)
            {
                ResetState();
                GameManager.Instance.LoopConsumed(gameObject);
            }
        }
    }
}
