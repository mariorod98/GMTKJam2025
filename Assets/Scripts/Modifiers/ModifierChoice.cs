using System;

[Serializable]
public class ModifierChoice
{
    public int m_level = 0;
    public PowerUp m_powerUp = null;
    public ChallengeUp m_challengeUp = null;

    public ModifierChoice()
    {

    }

    public ModifierChoice(int level, PowerUp powerUp, ChallengeUp challengeUp)
    {
        m_level = level;
        m_powerUp = powerUp;
        m_challengeUp = challengeUp;
    }

    public void ApplyChoice()
    {
        m_powerUp.ApplyModifier();
        m_challengeUp.ApplyModifier();
    }
}
