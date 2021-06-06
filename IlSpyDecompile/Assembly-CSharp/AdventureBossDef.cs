using UnityEngine;

[CustomEditClass]
public class AdventureBossDef : MonoBehaviour
{
	public enum IntroLinePlayTime
	{
		MissionSelect,
		MissionStart
	}

	[CustomEditField(Sections = "Intro Line")]
	public string m_IntroLine;

	[CustomEditField(Sections = "Intro Line")]
	public IntroLinePlayTime m_IntroLinePlayTime;

	[CustomEditField(T = EditType.GAME_OBJECT, Sections = "General")]
	public string m_quotePrefabOverride;

	[CustomEditField(Sections = "General")]
	public MusicPlaylistType m_MissionMusic;

	public MaterialReference m_CoinPortraitMaterial;

	public virtual string GetIntroLine()
	{
		return m_IntroLine;
	}
}
