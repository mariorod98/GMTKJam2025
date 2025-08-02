using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public enum PowerUpType
{
    IncreasePickRadius,
    IncreaseTime,
    IncreaseBonusMultiplier,
    IncreaseBonusTime,
    Size,
    IncreaseBowlSize,
    IncreaseBowlMagnet,
}

public enum ChallengeUpType
{
    IncreaseLoopBounciness,
    IncreaseLoopSizeVariation,
    IncreaseLoopsPerWave,
    IncreaseNumberOfColors,
    Size,
}

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
    [Header("PowerUp Modifiers")]
    // PowerUps
    public float m_pickRadiusModifier = 1.0f;
    public int m_bonusScoreModifier= 2;
    public float m_countdownModifier = 0.0f;
    public float m_bonusCountdownModifier = 0.0f;

    // ChallengeUps
    [Header("ChallengeUp Modifiers")]
    public float m_loopsMultiplierModifier = 1.0f;
    public int m_numberOfColorsModifier = 2;
    public float m_loopBouncinessModifier = 0.0f;
    public float m_loopSizeVariationModifier = 0.0f;

    [Header("PowerUp Options")]
    public int m_maxPowerLevel = 3;
    public List<float> m_pickRadiusPossibleValues;
    public List<float> m_timeModifierPossibleValues;
    public List<float> m_bowlSizePossibleValues;
    public List<int> m_bonusMultiplierPossibleValues;
    public List<float> m_bonusTimePossibleValues;

    [Header("ChallengeUp Options")]
    public List<float> m_loopsMultiplierPossibleValues;
    public List<float> m_loopBouncinessPossibleValues;
    public List<float> m_loopSizeVariationPossibleValues;

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
        switch(SelectPowerUp())
        {
            case PowerUpType.IncreasePickRadius:
                return new IncreasePickRadiusPowerUp(m_pickRadiusPossibleValues[level]);
            case PowerUpType.IncreaseTime:
                return new IncreaseTimePowerUp(m_timeModifierPossibleValues[level]);
            case PowerUpType.IncreaseBonusMultiplier:
                return new IncreaseBonusPowerUp(m_bonusMultiplierPossibleValues[level]);
            case PowerUpType.IncreaseBonusTime:
                return new IncreaseBonusTimePowerUp(m_bonusTimePossibleValues[level]);
        }

        Assert.IsTrue(false, "Didn't pick a power up");
        PowerUp powerUp = new PowerUp();
        return powerUp;
    }

    private ChallengeUp GenerateChallengeUp(int level) 
    {
        switch (SelectChallengeUp())
        {
            case ChallengeUpType.IncreaseLoopsPerWave:
                return new IncreaseLoopsPerWaveChallengeUp(m_loopsMultiplierPossibleValues[level]);
            case ChallengeUpType.IncreaseNumberOfColors:
                return new IncreaseNumberOfColorsChallengeUp(1);
            case ChallengeUpType.IncreaseLoopBounciness:
                return new IncreaseLoopBouncinessChallengeUp(m_loopBouncinessPossibleValues[level]);
            case ChallengeUpType.IncreaseLoopSizeVariation:
                return new IncreaseLoopSizeVariationChallengeUp(m_loopSizeVariationPossibleValues[level]);
        }

        Assert.IsFalse(false, "Didn't pick a challenge up");
        ChallengeUp powerUp = new ChallengeUp();
        return powerUp;
    }

    private ChallengeUpType SelectChallengeUp()
    {
        List<ChallengeUpType> candidates = new List<ChallengeUpType>();
        candidates.Add(ChallengeUpType.IncreaseLoopsPerWave);

        if(m_numberOfColorsModifier < 6)
        {
            candidates.Add(ChallengeUpType.IncreaseNumberOfColors);
        }
        if(m_loopBouncinessModifier < 0.9f)
        {
            candidates.Add(ChallengeUpType.IncreaseLoopBounciness);
        }
        if (m_loopSizeVariationModifier < 5.0f)
        {
            candidates.Add(ChallengeUpType.IncreaseLoopSizeVariation);
        }

        return candidates[Random.Range(0, candidates.Count)];

    }

    private PowerUpType SelectPowerUp()
    {
        List<PowerUpType> candidates = new List<PowerUpType>();
        candidates.Add(PowerUpType.IncreaseTime);
        candidates.Add(PowerUpType.IncreaseBonusMultiplier);

        if (m_bonusCountdownModifier < m_countdownModifier)
        {
            candidates.Add(PowerUpType.IncreaseBonusTime);
        }
        if (m_pickRadiusModifier < 5.0f)
        {
            candidates.Add(PowerUpType.IncreasePickRadius);
        }

        return candidates[Random.Range(0, candidates.Count)];
    }
}
