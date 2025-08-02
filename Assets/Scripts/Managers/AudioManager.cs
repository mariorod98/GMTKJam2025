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

    public List<AudioClip> m_collisionWithBigBowlClips;
    public List<AudioClip> m_collisionWithSmallBowlClips;
    public List<AudioClip> m_collisionWithLoopClips;
}
