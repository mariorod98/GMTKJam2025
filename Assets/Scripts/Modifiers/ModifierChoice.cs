using System;

[Serializable]
public class ModifierChoice
{
    public BaseModifier m_powerUp = null;
    public BaseModifier m_challengeUp = null;

    public ModifierChoice()
    {

    }

    public ModifierChoice(BaseModifier powerUp, BaseModifier challengeUp)
    {
        m_powerUp = powerUp;
        m_challengeUp = challengeUp;
    }

    public void ApplyChoice()
    {
        m_powerUp.ApplyModifier();
        m_challengeUp.ApplyModifier();
    }
}
