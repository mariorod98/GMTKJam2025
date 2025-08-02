using System;

public enum ModifierType { PowerUp, ChallengeUp}
public enum PowerUpType { PickSphereSize, Size}
public enum ChallengeUpType { IncreaseLoopsPerWave, Size}

[Serializable]
public class BaseModifier
{
    public ModifierType m_type;
    public float m_floatModifier = 0.0f;
    public int m_intModifier = 0;

    public virtual String GetName() { return "BaseModifier"; }
    public virtual String GetFormattedText() { return ""; }
    public virtual void ApplyModifier() { }
}
