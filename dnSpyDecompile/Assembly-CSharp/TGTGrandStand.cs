using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000B2 RID: 178
[CustomEditClass]
public class TGTGrandStand : MonoBehaviour
{
	// Token: 0x06000B12 RID: 2834 RVA: 0x00041C5A File Offset: 0x0003FE5A
	private void Awake()
	{
		TGTGrandStand.s_instance = this;
	}

	// Token: 0x06000B13 RID: 2835 RVA: 0x00041C62 File Offset: 0x0003FE62
	private void Start()
	{
		base.StartCoroutine(this.RegisterBoardEvents());
	}

	// Token: 0x06000B14 RID: 2836 RVA: 0x00041C71 File Offset: 0x0003FE71
	private void Update()
	{
		this.HandleClicks();
	}

	// Token: 0x06000B15 RID: 2837 RVA: 0x00041C79 File Offset: 0x0003FE79
	private void OnDestroy()
	{
		TGTGrandStand.s_instance = null;
	}

	// Token: 0x06000B16 RID: 2838 RVA: 0x00041C81 File Offset: 0x0003FE81
	public static TGTGrandStand Get()
	{
		return TGTGrandStand.s_instance;
	}

	// Token: 0x06000B17 RID: 2839 RVA: 0x00041C88 File Offset: 0x0003FE88
	private void HandleClicks()
	{
		if (UniversalInputManager.Get().GetMouseButtonDown(0) && this.IsOver(this.m_HumanRoot))
		{
			this.HumanClick();
		}
		if (UniversalInputManager.Get().GetMouseButtonDown(0) && this.IsOver(this.m_OrcRoot))
		{
			this.OrcClick();
		}
		if (UniversalInputManager.Get().GetMouseButtonDown(0) && this.IsOver(this.m_KnightRoot))
		{
			this.KnightClick();
		}
	}

	// Token: 0x06000B18 RID: 2840 RVA: 0x00041CF8 File Offset: 0x0003FEF8
	private void HumanClick()
	{
		this.m_HumanRoot.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, -30f);
		if (!string.IsNullOrEmpty(this.m_ClickHumanSound))
		{
			string clickHumanSound = this.m_ClickHumanSound;
			if (!string.IsNullOrEmpty(clickHumanSound))
			{
				SoundManager.Get().LoadAndPlay(clickHumanSound, this.m_HumanRoot);
			}
		}
	}

	// Token: 0x06000B19 RID: 2841 RVA: 0x00041D5C File Offset: 0x0003FF5C
	private void OrcClick()
	{
		this.m_OrcRoot.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, -30f);
		if (!string.IsNullOrEmpty(this.m_ClickOrcSound))
		{
			string clickOrcSound = this.m_ClickOrcSound;
			if (!string.IsNullOrEmpty(clickOrcSound))
			{
				SoundManager.Get().LoadAndPlay(clickOrcSound, this.m_OrcRoot);
			}
		}
	}

	// Token: 0x06000B1A RID: 2842 RVA: 0x00041DC0 File Offset: 0x0003FFC0
	private void KnightClick()
	{
		this.m_KnightRoot.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, -30f);
		if (!string.IsNullOrEmpty(this.m_ClickKnightSound))
		{
			string clickKnightSound = this.m_ClickKnightSound;
			if (!string.IsNullOrEmpty(clickKnightSound))
			{
				SoundManager.Get().LoadAndPlay(clickKnightSound, this.m_KnightRoot);
			}
		}
	}

	// Token: 0x06000B1B RID: 2843 RVA: 0x00041E23 File Offset: 0x00040023
	private IEnumerator TestAnimations()
	{
		yield return new WaitForSeconds(4f);
		this.PlayCheerAnimation();
		yield return new WaitForSeconds(8f);
		this.PlayCheerAnimation();
		yield return new WaitForSeconds(9f);
		this.PlayCheerAnimation();
		yield return new WaitForSeconds(8f);
		this.PlayOhNoAnimation();
		yield return new WaitForSeconds(8f);
		this.PlayOhNoAnimation();
		yield break;
	}

	// Token: 0x06000B1C RID: 2844 RVA: 0x00041E34 File Offset: 0x00040034
	public void PlayCheerAnimation()
	{
		int num = UnityEngine.Random.Range(0, this.ANIMATION_CHEER.Length);
		string animName = this.ANIMATION_CHEER[num];
		this.PlayAnimation(this.m_HumanAnimator, animName, 4f);
		this.PlaySoundFromList(this.m_CheerHumanSounds, num);
		num = UnityEngine.Random.Range(0, this.ANIMATION_CHEER.Length);
		animName = this.ANIMATION_CHEER[num];
		this.PlayAnimation(this.m_OrcAnimator, animName, 4f);
		this.PlaySoundFromList(this.m_CheerOrcSounds, num);
		num = UnityEngine.Random.Range(0, this.ANIMATION_CHEER.Length);
		animName = this.ANIMATION_CHEER[num];
		this.PlayAnimation(this.m_KnightAnimator, animName, 4f);
		this.PlaySoundFromList(this.m_CheerKnightSounds, num);
	}

	// Token: 0x06000B1D RID: 2845 RVA: 0x00041EE8 File Offset: 0x000400E8
	public void PlayOhNoAnimation()
	{
		int num = UnityEngine.Random.Range(0, this.ANIMATION_OHNO.Length);
		string animName = this.ANIMATION_OHNO[num];
		this.PlayAnimation(this.m_HumanAnimator, animName, 3.5f);
		this.PlaySoundFromList(this.m_OhNoHumanSounds, num);
		num = UnityEngine.Random.Range(0, this.ANIMATION_OHNO.Length);
		animName = this.ANIMATION_OHNO[num];
		this.PlayAnimation(this.m_OrcAnimator, animName, 3.5f);
		this.PlaySoundFromList(this.m_OhNoOrcSounds, num);
		num = UnityEngine.Random.Range(0, this.ANIMATION_OHNO.Length);
		animName = this.ANIMATION_OHNO[num];
		this.PlayAnimation(this.m_KnightAnimator, animName, 3.5f);
		this.PlaySoundFromList(this.m_OhNoKnightSounds, num);
	}

	// Token: 0x06000B1E RID: 2846 RVA: 0x00041F9C File Offset: 0x0004019C
	public void PlayScoreCard(string humanScore, string orcScore, string knightScore)
	{
		this.m_HumanScoreUberText.Text = humanScore;
		this.m_OrcScoreUberText.Text = orcScore;
		this.m_KnightScoreUberText.Text = knightScore;
		this.m_HumanAnimator.SetTrigger("ScoreCard");
		this.m_OrcAnimator.SetTrigger("ScoreCard");
		this.m_KnightAnimator.SetTrigger("ScoreCard");
		this.PlaySound(this.m_ScoreCardSound);
	}

	// Token: 0x06000B1F RID: 2847 RVA: 0x00042009 File Offset: 0x00040209
	private void PlaySoundFromList(List<string> soundList, int index)
	{
		if (soundList == null || soundList.Count == 0)
		{
			return;
		}
		if (index > soundList.Count)
		{
			index = 0;
		}
		this.PlaySound(soundList[index]);
	}

	// Token: 0x06000B20 RID: 2848 RVA: 0x00042030 File Offset: 0x00040230
	private void PlaySound(string soundPath)
	{
		if (!string.IsNullOrEmpty(soundPath) && !string.IsNullOrEmpty(soundPath))
		{
			SoundManager.Get().LoadAndPlay(soundPath, this.m_OrcRoot);
		}
	}

	// Token: 0x06000B21 RID: 2849 RVA: 0x00042058 File Offset: 0x00040258
	private void PlayAnimation(Animator animator, string animName, float time)
	{
		this.m_isAnimating = true;
		this.m_HumanScoreCard.SetActive(false);
		this.m_OrcScoreCard.SetActive(false);
		this.m_KnightScoreCard.SetActive(false);
		base.StartCoroutine(this.PlayAnimationRandomStart(animator, animName, time));
	}

	// Token: 0x06000B22 RID: 2850 RVA: 0x00042095 File Offset: 0x00040295
	private IEnumerator PlayAnimationRandomStart(Animator animator, string animName, float time)
	{
		yield return new WaitForSeconds(UnityEngine.Random.Range(0.05f, 0.2f));
		animator.SetTrigger(animName);
		base.StartCoroutine(this.ReturnToIdleAnimation(animator, time));
		yield break;
	}

	// Token: 0x06000B23 RID: 2851 RVA: 0x000420B9 File Offset: 0x000402B9
	private IEnumerator ReturnToIdleAnimation(Animator animator, float time)
	{
		yield return new WaitForSeconds(time);
		this.m_isAnimating = false;
		animator.SetTrigger("Idle");
		yield break;
	}

	// Token: 0x06000B24 RID: 2852 RVA: 0x000420D6 File Offset: 0x000402D6
	private void Shake()
	{
		if (this.m_isAnimating)
		{
			return;
		}
		base.StartCoroutine(this.ShakeHuman());
		base.StartCoroutine(this.ShakeOrc());
		base.StartCoroutine(this.ShakeKnight());
	}

	// Token: 0x06000B25 RID: 2853 RVA: 0x00042108 File Offset: 0x00040308
	private IEnumerator ShakeHuman()
	{
		yield return new WaitForSeconds(UnityEngine.Random.Range(0.05f, 0.2f));
		this.m_HumanRoot.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, -25f);
		yield break;
	}

	// Token: 0x06000B26 RID: 2854 RVA: 0x00042117 File Offset: 0x00040317
	private IEnumerator ShakeOrc()
	{
		yield return new WaitForSeconds(UnityEngine.Random.Range(0.05f, 0.2f));
		this.m_OrcRoot.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, -25f);
		yield break;
	}

	// Token: 0x06000B27 RID: 2855 RVA: 0x00042126 File Offset: 0x00040326
	private IEnumerator ShakeKnight()
	{
		yield return new WaitForSeconds(UnityEngine.Random.Range(0.05f, 0.2f));
		this.m_KnightRoot.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, -25f);
		yield break;
	}

	// Token: 0x06000B28 RID: 2856 RVA: 0x000402EE File Offset: 0x0003E4EE
	private bool IsOver(GameObject go)
	{
		return go && InputUtil.IsPlayMakerMouseInputAllowed(go) && UniversalInputManager.Get().InputIsOver(go);
	}

	// Token: 0x06000B29 RID: 2857 RVA: 0x00042135 File Offset: 0x00040335
	private IEnumerator RegisterBoardEvents()
	{
		while (BoardEvents.Get() == null)
		{
			yield return null;
		}
		this.m_boardEvents = BoardEvents.Get();
		this.m_boardEvents.RegisterFriendlyHeroDamageEvent(new BoardEvents.EventDelegate(this.FriendlyHeroDamage), 7f);
		this.m_boardEvents.RegisterOpponentHeroDamageEvent(new BoardEvents.EventDelegate(this.OpponentHeroDamage), 10f);
		this.m_boardEvents.RegisterFriendlyLegendaryMinionSpawnEvent(new BoardEvents.EventDelegate(this.FriendlyLegendarySpawn), 6f);
		this.m_boardEvents.RegisterOppenentLegendaryMinionSpawnEvent(new BoardEvents.EventDelegate(this.OpponentLegendarySpawn), 9f);
		this.m_boardEvents.RegisterFriendlyLegendaryMinionDeathEvent(new BoardEvents.EventDelegate(this.FriendlyLegendaryDeath), 6f);
		this.m_boardEvents.RegisterOppenentLegendaryMinionDeathEvent(new BoardEvents.EventDelegate(this.OpponentLegendaryDeath), 9f);
		this.m_boardEvents.RegisterFriendlyMinionDamageEvent(new BoardEvents.EventDelegate(this.FriendlyMinionDamage), 15f);
		this.m_boardEvents.RegisterOpponentMinionDamageEvent(new BoardEvents.EventDelegate(this.OpponentMinionDamage), 15f);
		this.m_boardEvents.RegisterFriendlyMinionDeathEvent(new BoardEvents.EventDelegate(this.FriendlyMinionDeath), 15f);
		this.m_boardEvents.RegisterOppenentMinionDeathEvent(new BoardEvents.EventDelegate(this.OpponentMinionDeath), 15f);
		this.m_boardEvents.RegisterFriendlyMinionSpawnEvent(new BoardEvents.EventDelegate(this.FriendlyMinionSpawn), 10f);
		this.m_boardEvents.RegisterOppenentMinionSpawnEvent(new BoardEvents.EventDelegate(this.OpponentMinionSpawn), 10f);
		this.m_boardEvents.RegisterLargeShakeEvent(new BoardEvents.LargeShakeEventDelegate(this.Shake));
		yield break;
	}

	// Token: 0x06000B2A RID: 2858 RVA: 0x00042144 File Offset: 0x00040344
	private void FriendlyHeroDamage(float weight)
	{
		this.PlayOhNoAnimation();
	}

	// Token: 0x06000B2B RID: 2859 RVA: 0x0004214C File Offset: 0x0004034C
	private void OpponentHeroDamage(float weight)
	{
		if (weight <= 15f)
		{
			this.PlayCheerAnimation();
			return;
		}
		if (weight > 20f)
		{
			this.PlayScoreCard("10", "10", "10");
			return;
		}
		this.PlayScoreCard("10", UnityEngine.Random.Range(7, 9).ToString(), UnityEngine.Random.Range(8, 10).ToString());
	}

	// Token: 0x06000B2C RID: 2860 RVA: 0x000421B1 File Offset: 0x000403B1
	private void FriendlyLegendarySpawn(float weight)
	{
		this.PlayCheerAnimation();
	}

	// Token: 0x06000B2D RID: 2861 RVA: 0x00042144 File Offset: 0x00040344
	private void OpponentLegendarySpawn(float weight)
	{
		this.PlayOhNoAnimation();
	}

	// Token: 0x06000B2E RID: 2862 RVA: 0x00042144 File Offset: 0x00040344
	private void FriendlyLegendaryDeath(float weight)
	{
		this.PlayOhNoAnimation();
	}

	// Token: 0x06000B2F RID: 2863 RVA: 0x000421B1 File Offset: 0x000403B1
	private void OpponentLegendaryDeath(float weight)
	{
		this.PlayCheerAnimation();
	}

	// Token: 0x06000B30 RID: 2864 RVA: 0x00042144 File Offset: 0x00040344
	private void FriendlyMinionDamage(float weight)
	{
		this.PlayOhNoAnimation();
	}

	// Token: 0x06000B31 RID: 2865 RVA: 0x000421B1 File Offset: 0x000403B1
	private void OpponentMinionDamage(float weight)
	{
		this.PlayCheerAnimation();
	}

	// Token: 0x06000B32 RID: 2866 RVA: 0x00042144 File Offset: 0x00040344
	private void FriendlyMinionDeath(float weight)
	{
		this.PlayOhNoAnimation();
	}

	// Token: 0x06000B33 RID: 2867 RVA: 0x000421B1 File Offset: 0x000403B1
	private void OpponentMinionDeath(float weight)
	{
		this.PlayCheerAnimation();
	}

	// Token: 0x06000B34 RID: 2868 RVA: 0x000421B1 File Offset: 0x000403B1
	private void FriendlyMinionSpawn(float weight)
	{
		this.PlayCheerAnimation();
	}

	// Token: 0x06000B35 RID: 2869 RVA: 0x00042144 File Offset: 0x00040344
	private void OpponentMinionSpawn(float weight)
	{
		this.PlayOhNoAnimation();
	}

	// Token: 0x04000746 RID: 1862
	private const string ANIMATION_IDLE = "Idle";

	// Token: 0x04000747 RID: 1863
	private readonly string[] ANIMATION_CHEER = new string[]
	{
		"Cheer01",
		"Cheer02",
		"Cheer03"
	};

	// Token: 0x04000748 RID: 1864
	private readonly string[] ANIMATION_OHNO = new string[]
	{
		"OhNo01",
		"OhNo02"
	};

	// Token: 0x04000749 RID: 1865
	private const string ANIMATION_SCORE_CARD = "ScoreCard";

	// Token: 0x0400074A RID: 1866
	private const float MIN_RANDOM_TIME_FACTOR = 0.05f;

	// Token: 0x0400074B RID: 1867
	private const float MAX_RANDOM_TIME_FACTOR = 0.2f;

	// Token: 0x0400074C RID: 1868
	private const float CHEER_ANIMATION_PLAY_TIME = 4f;

	// Token: 0x0400074D RID: 1869
	private const float OHNO_ANIMATION_PLAY_TIME = 3.5f;

	// Token: 0x0400074E RID: 1870
	private const float FRIENDLY_HERO_DAMAGE_WEIGHT_TRGGER = 7f;

	// Token: 0x0400074F RID: 1871
	private const float OPPONENT_HERO_DAMAGE_WEIGHT_TRGGER = 10f;

	// Token: 0x04000750 RID: 1872
	private const float FRIENDLY_LEGENDARY_SPAWN_MIN_COST_TRGGER = 6f;

	// Token: 0x04000751 RID: 1873
	private const float OPPONENT_LEGENDARY_SPAWN_MIN_COST_TRGGER = 9f;

	// Token: 0x04000752 RID: 1874
	private const float FRIENDLY_LEGENDARY_DEATH_MIN_COST_TRGGER = 6f;

	// Token: 0x04000753 RID: 1875
	private const float OPPONENT_LEGENDARY_DEATH_MIN_COST_TRGGER = 9f;

	// Token: 0x04000754 RID: 1876
	private const float FRIENDLY_MINION_DAMAGE_WEIGHT = 15f;

	// Token: 0x04000755 RID: 1877
	private const float OPPONENT_MINION_DAMAGE_WEIGHT = 15f;

	// Token: 0x04000756 RID: 1878
	private const float FRIENDLY_MINION_DEATH_WEIGHT = 15f;

	// Token: 0x04000757 RID: 1879
	private const float OPPONENT_MINION_DEATH_WEIGHT = 15f;

	// Token: 0x04000758 RID: 1880
	private const float FRIENDLY_MINION_SPAWN_WEIGHT = 10f;

	// Token: 0x04000759 RID: 1881
	private const float OPPONENT_MINION_SPAWN_WEIGHT = 10f;

	// Token: 0x0400075A RID: 1882
	private const float OPPONENT_HERO_DAMAGE_SCORE_CARD_TRIGGER = 15f;

	// Token: 0x0400075B RID: 1883
	private const float OPPONENT_HERO_DAMAGE_SCORE_CARD_10S_TRIGGER = 20f;

	// Token: 0x0400075C RID: 1884
	public GameObject m_HumanRoot;

	// Token: 0x0400075D RID: 1885
	public GameObject m_OrcRoot;

	// Token: 0x0400075E RID: 1886
	public GameObject m_KnightRoot;

	// Token: 0x0400075F RID: 1887
	public Animator m_HumanAnimator;

	// Token: 0x04000760 RID: 1888
	public Animator m_OrcAnimator;

	// Token: 0x04000761 RID: 1889
	public Animator m_KnightAnimator;

	// Token: 0x04000762 RID: 1890
	public GameObject m_HumanScoreCard;

	// Token: 0x04000763 RID: 1891
	public GameObject m_OrcScoreCard;

	// Token: 0x04000764 RID: 1892
	public GameObject m_KnightScoreCard;

	// Token: 0x04000765 RID: 1893
	public UberText m_HumanScoreUberText;

	// Token: 0x04000766 RID: 1894
	public UberText m_OrcScoreUberText;

	// Token: 0x04000767 RID: 1895
	public UberText m_KnightScoreUberText;

	// Token: 0x04000768 RID: 1896
	[CustomEditField(T = EditType.SOUND_PREFAB, Sections = "Human Sounds")]
	public string m_ClickHumanSound;

	// Token: 0x04000769 RID: 1897
	[CustomEditField(T = EditType.SOUND_PREFAB, Sections = "Human Sounds")]
	public List<string> m_CheerHumanSounds;

	// Token: 0x0400076A RID: 1898
	[CustomEditField(T = EditType.SOUND_PREFAB, Sections = "Human Sounds")]
	public List<string> m_OhNoHumanSounds;

	// Token: 0x0400076B RID: 1899
	[CustomEditField(T = EditType.SOUND_PREFAB, Sections = "Orc Sounds")]
	public string m_ClickOrcSound;

	// Token: 0x0400076C RID: 1900
	[CustomEditField(T = EditType.SOUND_PREFAB, Sections = "Orc Sounds")]
	public List<string> m_CheerOrcSounds;

	// Token: 0x0400076D RID: 1901
	[CustomEditField(T = EditType.SOUND_PREFAB, Sections = "Orc Sounds")]
	public List<string> m_OhNoOrcSounds;

	// Token: 0x0400076E RID: 1902
	[CustomEditField(T = EditType.SOUND_PREFAB, Sections = "Knight Sounds")]
	public string m_ClickKnightSound;

	// Token: 0x0400076F RID: 1903
	[CustomEditField(T = EditType.SOUND_PREFAB, Sections = "Knight Sounds")]
	public List<string> m_CheerKnightSounds;

	// Token: 0x04000770 RID: 1904
	[CustomEditField(T = EditType.SOUND_PREFAB, Sections = "Knight Sounds")]
	public List<string> m_OhNoKnightSounds;

	// Token: 0x04000771 RID: 1905
	[CustomEditField(T = EditType.SOUND_PREFAB, Sections = "Sounds")]
	public string m_ScoreCardSound;

	// Token: 0x04000772 RID: 1906
	private BoardEvents m_boardEvents;

	// Token: 0x04000773 RID: 1907
	private bool m_isAnimating;

	// Token: 0x04000774 RID: 1908
	private static TGTGrandStand s_instance;
}
