using System;

[Serializable]
public class SpellTargetInfo
{
	public SpellTargetBehavior m_Behavior;

	public int m_RandomTargetCountMin = 8;

	public int m_RandomTargetCountMax = 10;

	public bool m_SuppressPlaySounds;
}
