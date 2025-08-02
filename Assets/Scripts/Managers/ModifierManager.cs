using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class ModifierManager : MonoBehaviour
{
    /******************************************************************************/

    /*** START SINGLETON BLOCK ***/
    private static ModifierManager m_instance = null;
    public static ModifierManager Instance
    {
        get { return m_instance; }
    }

    private void Awake()
    {
        m_instance = this;
    }
    /*** END SINGLETON BLOCK ***/

    /******************************************************************************/
    [Header("Modifiers")]
    // PowerUps
    public float m_pickRadiusModifier = 1.0f;
    // ChallengeUps
    public float m_loopsMultiplierModifier = 1.0f;
    public int m_numberOfColorsModifier = 2;

    [Header("PowerUp Options")]
    public int m_maxPowerLevel = 3;
    public List<float> m_pickRadiusPossibleValues;
    public List<float> m_timeModifierPossibleValues;
    public List<float> m_bowlSizePossibleValues;

    [Header("ChallengeUp Options")]
    public List<float> m_loopsMultiplierPossibleValues;

    public ModifierChoice GenerateModifierChoice()
    {
        int level = Random.Range(0, m_maxPowerLevel);
        PowerUp powerUp = GeneratePowerUp(level);
        ChallengeUp challengeUp = GenerateChallengeUp(level);
        ModifierChoice choice = new ModifierChoice(level, powerUp, challengeUp);
        return choice;
    }

    private PowerUp GeneratePowerUp(int level)
    {
        PowerUpType powerUpType = (PowerUpType)Random.Range(0, (int)PowerUpType.Size);

        switch(powerUpType)
        {
            case PowerUpType.IncreasePickRadius:
                return new IncreasePickRadiusPowerUp(m_pickRadiusPossibleValues[level]);
            case PowerUpType.IncreaseTime:
                return new IncreaseTimePowerUp(m_timeModifierPossibleValues[level]);
        }

        Assert.IsTrue(false, "Didn't pick a power up");
        PowerUp powerUp = new PowerUp();
        return powerUp;
    }

    private ChallengeUp GenerateChallengeUp(int level) 
    {
        ChallengeUpType powerUpType = (ChallengeUpType)Random.Range(0, (int)ChallengeUpType.Size);

        switch (powerUpType)
        {
            case ChallengeUpType.IncreaseLoopsPerWave:
                return new IncreaseLoopsPerWaveChallengeUp(m_loopsMultiplierPossibleValues[level]);
            case ChallengeUpType.IncreaseNumberOfColors:
                return new IncreaseNumberOfColorsChallengeUp(1);

        }

        Assert.IsFalse(false, "Didn't pick a challenge up");
        ChallengeUp powerUp = new ChallengeUp();
        return powerUp;
    }
}
