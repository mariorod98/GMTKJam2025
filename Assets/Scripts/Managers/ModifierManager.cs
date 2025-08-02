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

    public int m_maxPowerLevel = 3;
    // PowerUps
    public float m_pickSphereSizeModifier = 1.0f;
    public List<float> m_pickSphereSizePossibleValues;

    // Challenges
    public float m_loopsMultiplierModifier = 1.0f;
    public List<float> m_loopsMultiplierPossibleValues;

    public int m_numberOfColorsModifier = 2;

    public ModifierChoice GenerateModifierChoice()
    {
        int level = Random.Range(0, m_maxPowerLevel);
        BaseModifier powerUp = GeneratePowerUp(level);
        BaseModifier challengeUp = GenerateChallengeUp(level);
        ModifierChoice choice = new ModifierChoice(level, powerUp, challengeUp);
        return choice;
    }

    private BaseModifier GeneratePowerUp(int level)
    {
        PowerUpType powerUpType = (PowerUpType)Random.Range(0, (int)PowerUpType.Size);

        switch(powerUpType)
        {
            case PowerUpType.PickSphereSize:
                return new PickSphereSizePowerUp(m_pickSphereSizePossibleValues[level]);
        }

        Assert.IsFalse(false, "Didn't pick a power up");
        BaseModifier powerUp = new BaseModifier();
        return powerUp;
    }

    private BaseModifier GenerateChallengeUp(int level) 
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
        BaseModifier powerUp = new BaseModifier();
        return powerUp;
    }
}
