using System.Collections;
using UnityEngine;

public class VictoryTwoScoop : EndGameTwoScoop
{
	public GameObject m_godRays;

	public GameObject m_godRays2;

	public GameObject m_rightTrumpet;

	public GameObject m_rightBanner;

	public GameObject m_rightCloud;

	public GameObject m_rightLaurel;

	public GameObject m_leftTrumpet;

	public GameObject m_leftBanner;

	public GameObject m_leftCloud;

	public GameObject m_leftLaurel;

	public GameObject m_crown;

	public AudioSource m_fireworksAudio;

	private const float GOD_RAY_ANGLE = 20f;

	private const float GOD_RAY_DURATION = 20f;

	private const float LAUREL_ROTATION = 2f;

	protected EntityDef m_overrideHeroEntityDef;

	protected DefLoader.DisposableCardDef m_overrideHeroCardDef;

	public void StopFireworksAudio()
	{
		if (m_fireworksAudio != null)
		{
			SoundManager.Get().Stop(m_fireworksAudio);
		}
	}

	public void SetOverrideHero(EntityDef overrideHero)
	{
		if (overrideHero != null)
		{
			if (!overrideHero.IsHero())
			{
				Log.Gameplay.PrintError("VictoryTwoScoop.SetOverrideHero() - passed EntityDef {0} is not a hero!", overrideHero);
				return;
			}
			DefLoader.DisposableCardDef cardDef = DefLoader.Get().GetCardDef(overrideHero.GetCardId());
			if (cardDef == null)
			{
				Log.Gameplay.PrintError("VictoryTwoScoop.SetOverrideHero() - passed EntityDef {0} does not have a CardDef!", overrideHero);
			}
			else
			{
				m_overrideHeroEntityDef = overrideHero;
				m_overrideHeroCardDef?.Dispose();
				m_overrideHeroCardDef = cardDef;
			}
		}
		else
		{
			m_overrideHeroEntityDef = null;
			m_overrideHeroCardDef?.Dispose();
			m_overrideHeroCardDef = null;
		}
	}

	public override void OnDestroy()
	{
		m_overrideHeroCardDef?.Dispose();
		m_overrideHeroCardDef = null;
		base.OnDestroy();
	}

	protected override void ShowImpl()
	{
		SetupHeroActor();
		SetupBannerText();
		PlayShowAnimations();
	}

	protected override void ResetPositions()
	{
		base.gameObject.transform.localPosition = EndGameTwoScoop.START_POSITION;
		base.gameObject.transform.eulerAngles = new Vector3(0f, 180f, 0f);
		if (m_rightTrumpet != null)
		{
			m_rightTrumpet.transform.localPosition = new Vector3(0.23f, -0.6f, 0.16f);
			m_rightTrumpet.transform.localScale = new Vector3(1f, 1f, 1f);
		}
		if (m_leftTrumpet != null)
		{
			m_leftTrumpet.transform.localPosition = new Vector3(-0.23f, -0.6f, 0.16f);
			m_leftTrumpet.transform.localScale = new Vector3(-1f, 1f, 1f);
		}
		if (m_rightBanner != null)
		{
			m_rightBanner.transform.localScale = new Vector3(1f, 1f, 0.08f);
		}
		if (m_leftBanner != null)
		{
			m_leftBanner.transform.localScale = new Vector3(1f, 1f, 0.08f);
		}
		if (m_rightCloud != null)
		{
			m_rightCloud.transform.localPosition = new Vector3(-0.2f, -0.8f, 0.26f);
		}
		if (m_leftCloud != null)
		{
			m_leftCloud.transform.localPosition = new Vector3(0.16f, -0.8f, 0.2f);
		}
		if (m_godRays != null)
		{
			m_godRays.transform.localEulerAngles = new Vector3(0f, 29f, 0f);
		}
		if (m_godRays2 != null)
		{
			m_godRays2.transform.localEulerAngles = new Vector3(0f, -29f, 0f);
		}
		if (m_crown != null)
		{
			m_crown.transform.localPosition = new Vector3(-0.041f, -0.06f, -0.834f);
		}
		if (m_rightLaurel != null)
		{
			m_rightLaurel.transform.localEulerAngles = new Vector3(0f, -90f, 0f);
			m_rightLaurel.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
		}
		if (m_leftLaurel != null)
		{
			m_leftLaurel.transform.localEulerAngles = new Vector3(0f, 90f, 0f);
			m_leftLaurel.transform.localScale = new Vector3(-0.7f, 0.7f, 0.7f);
		}
	}

	public override void StopAnimating()
	{
		StopCoroutine("AnimateAll");
		iTween.Stop(base.gameObject, includechildren: true);
		StartCoroutine(ResetPositionsForGoldEvent());
	}

	protected void SetupHeroActor()
	{
		if (m_overrideHeroEntityDef != null && m_overrideHeroCardDef != null)
		{
			m_heroActor.SetEntityDef(m_overrideHeroEntityDef);
			m_heroActor.SetCardDef(m_overrideHeroCardDef);
		}
		else
		{
			m_heroActor.SetFullDefFromEntity(GameState.Get().GetFriendlySidePlayer().GetHero());
		}
		m_heroActor.UpdateAllComponents();
		m_heroActor.TurnOffCollider();
	}

	protected void SetupBannerText()
	{
		string victoryScreenBannerText = GameState.Get().GetGameEntity().GetVictoryScreenBannerText();
		SetBannerLabel(victoryScreenBannerText);
	}

	protected void PlayShowAnimations()
	{
		GetComponent<PlayMakerFSM>().SendEvent("Action");
		iTween.FadeTo(base.gameObject, 1f, 0.25f);
		base.gameObject.transform.localScale = new Vector3(EndGameTwoScoop.START_SCALE_VAL, EndGameTwoScoop.START_SCALE_VAL, EndGameTwoScoop.START_SCALE_VAL);
		Hashtable args = iTween.Hash("scale", new Vector3(EndGameTwoScoop.END_SCALE_VAL, EndGameTwoScoop.END_SCALE_VAL, EndGameTwoScoop.END_SCALE_VAL), "time", 0.5f, "oncomplete", "PunchEndGameTwoScoop", "oncompletetarget", base.gameObject);
		iTween.ScaleTo(base.gameObject, args);
		Hashtable args2 = iTween.Hash("position", base.gameObject.transform.position + new Vector3(0.005f, 0.005f, 0.005f), "time", 1.5f, "oncomplete", "TokyoDriftTo", "oncompletetarget", base.gameObject);
		iTween.MoveTo(base.gameObject, args2);
		AnimateGodraysTo();
		AnimateCrownTo();
		StartCoroutine(AnimateAll());
	}

	private IEnumerator AnimateAll()
	{
		yield return new WaitForSeconds(0.25f);
		float num = 0.4f;
		Hashtable args = iTween.Hash("position", new Vector3(-0.52f, -0.6f, -0.23f), "time", num, "isLocal", true, "easetype", iTween.EaseType.easeOutElastic);
		iTween.MoveTo(m_rightTrumpet, args);
		Hashtable args2 = iTween.Hash("position", new Vector3(0.44f, -0.6f, -0.23f), "time", num, "isLocal", true, "easetype", iTween.EaseType.easeOutElastic);
		iTween.MoveTo(m_leftTrumpet, args2);
		Hashtable args3 = iTween.Hash("scale", new Vector3(1.1f, 1.1f, 1.1f), "time", 0.25f, "delay", 0.3f, "isLocal", true, "easetype", iTween.EaseType.easeOutBounce);
		iTween.ScaleTo(m_rightTrumpet, args3);
		Hashtable args4 = iTween.Hash("scale", new Vector3(-1.1f, 1.1f, 1.1f), "time", 0.25f, "delay", 0.3f, "isLocal", true, "easetype", iTween.EaseType.easeOutBounce);
		iTween.ScaleTo(m_leftTrumpet, args4);
		Hashtable args5 = iTween.Hash("z", 1, "delay", 0.24f, "time", 1f, "isLocal", true, "easetype", iTween.EaseType.easeOutElastic);
		iTween.ScaleTo(m_rightBanner, args5);
		Hashtable args6 = iTween.Hash("z", 1, "delay", 0.24f, "time", 1f, "isLocal", true, "easetype", iTween.EaseType.easeOutElastic);
		iTween.ScaleTo(m_leftBanner, args6);
		Hashtable args7 = iTween.Hash("x", -1.227438, "time", 5, "isLocal", true, "easetype", iTween.EaseType.easeOutCubic, "oncomplete", "CloudTo", "oncompletetarget", base.gameObject);
		iTween.MoveTo(m_rightCloud, args7);
		Hashtable args8 = iTween.Hash("x", 1.053244, "time", 5, "isLocal", true, "easetype", iTween.EaseType.easeOutCubic);
		iTween.MoveTo(m_leftCloud, args8);
		Hashtable args9 = iTween.Hash("rotation", new Vector3(0f, 2f, 0f), "time", 0.5f, "isLocal", true, "easetype", iTween.EaseType.easeOutElastic, "oncomplete", "LaurelWaveTo", "oncompletetarget", base.gameObject);
		iTween.RotateTo(m_rightLaurel, args9);
		Hashtable args10 = iTween.Hash("scale", new Vector3(1f, 1f, 1f), "time", 0.25f, "isLocal", true, "easetype", iTween.EaseType.easeOutBounce);
		iTween.ScaleTo(m_rightLaurel, args10);
		Hashtable args11 = iTween.Hash("rotation", new Vector3(0f, -2f, 0f), "time", 0.5f, "isLocal", true, "easetype", iTween.EaseType.easeOutElastic);
		iTween.RotateTo(m_leftLaurel, args11);
		Hashtable args12 = iTween.Hash("scale", new Vector3(-1f, 1f, 1f), "time", 0.25f, "isLocal", true, "easetype", iTween.EaseType.easeOutBounce);
		iTween.ScaleTo(m_leftLaurel, args12);
	}

	protected void TokyoDriftTo()
	{
		Hashtable args = iTween.Hash("position", EndGameTwoScoop.START_POSITION + new Vector3(0.2f, 0.2f, 0.2f), "time", 10, "isLocal", true, "easetype", iTween.EaseType.linear, "oncomplete", "TokyoDriftFro", "oncompletetarget", base.gameObject);
		iTween.MoveTo(base.gameObject, args);
	}

	private void TokyoDriftFro()
	{
		Hashtable args = iTween.Hash("position", EndGameTwoScoop.START_POSITION, "time", 10, "isLocal", true, "easetype", iTween.EaseType.linear, "oncomplete", "TokyoDriftTo", "oncompletetarget", base.gameObject);
		iTween.MoveTo(base.gameObject, args);
	}

	private void CloudTo()
	{
		Hashtable args = iTween.Hash("x", -0.92f, "time", 10, "isLocal", true, "easetype", iTween.EaseType.linear, "oncomplete", "CloudFro", "oncompletetarget", base.gameObject);
		iTween.MoveTo(m_rightCloud, args);
		Hashtable args2 = iTween.Hash("x", 0.82f, "time", 10, "isLocal", true, "easetype", iTween.EaseType.linear);
		iTween.MoveTo(m_leftCloud, args2);
	}

	private void CloudFro()
	{
		Hashtable args = iTween.Hash("x", -1.227438f, "time", 10, "isLocal", true, "easetype", iTween.EaseType.linear, "oncomplete", "CloudTo", "oncompletetarget", base.gameObject);
		iTween.MoveTo(m_rightCloud, args);
		Hashtable args2 = iTween.Hash("x", 1.053244f, "time", 10, "isLocal", true, "easetype", iTween.EaseType.linear);
		iTween.MoveTo(m_leftCloud, args2);
	}

	private void LaurelWaveTo()
	{
		Hashtable args = iTween.Hash("rotation", new Vector3(0f, 0f, 0f), "time", 10, "isLocal", true, "easetype", iTween.EaseType.linear, "oncomplete", "LaurelWaveFro", "oncompletetarget", base.gameObject);
		iTween.RotateTo(m_rightLaurel, args);
		Hashtable args2 = iTween.Hash("rotation", new Vector3(0f, 0f, 0f), "time", 10, "isLocal", true, "easetype", iTween.EaseType.linear);
		iTween.RotateTo(m_leftLaurel, args2);
	}

	private void LaurelWaveFro()
	{
		Hashtable args = iTween.Hash("rotation", new Vector3(0f, 2f, 0f), "time", 10, "isLocal", true, "easetype", iTween.EaseType.linear, "oncomplete", "LaurelWaveTo", "oncompletetarget", base.gameObject);
		iTween.RotateTo(m_rightLaurel, args);
		Hashtable args2 = iTween.Hash("rotation", new Vector3(0f, -2f, 0f), "time", 10, "isLocal", true, "easetype", iTween.EaseType.linear);
		iTween.RotateTo(m_leftLaurel, args2);
	}

	protected void AnimateCrownTo()
	{
		Hashtable args = iTween.Hash("z", -0.78f, "time", 5, "isLocal", true, "easetype", iTween.EaseType.easeInOutBack, "oncomplete", "AnimateCrownFro", "oncompletetarget", base.gameObject);
		iTween.MoveTo(m_crown, args);
	}

	private void AnimateCrownFro()
	{
		Hashtable args = iTween.Hash("z", -0.834f, "time", 5, "isLocal", true, "easetype", iTween.EaseType.easeInOutBack, "oncomplete", "AnimateCrownTo", "oncompletetarget", base.gameObject);
		iTween.MoveTo(m_crown, args);
	}

	protected void AnimateGodraysTo()
	{
		Hashtable args = iTween.Hash("rotation", new Vector3(0f, -20f, 0f), "time", 20f, "isLocal", true, "easetype", iTween.EaseType.linear, "oncomplete", "AnimateGodraysFro", "oncompletetarget", base.gameObject);
		iTween.RotateTo(m_godRays, args);
		Hashtable args2 = iTween.Hash("rotation", new Vector3(0f, 20f, 0f), "time", 20f, "isLocal", true, "easetype", iTween.EaseType.linear);
		iTween.RotateTo(m_godRays2, args2);
	}

	private void AnimateGodraysFro()
	{
		Hashtable args = iTween.Hash("rotation", new Vector3(0f, 20f, 0f), "time", 20f, "isLocal", true, "easetype", iTween.EaseType.linear, "oncomplete", "AnimateGodraysTo", "oncompletetarget", base.gameObject);
		iTween.RotateTo(m_godRays, args);
		Hashtable args2 = iTween.Hash("rotation", new Vector3(0f, -20f, 0f), "time", 20f, "isLocal", true, "easetype", iTween.EaseType.linear);
		iTween.RotateTo(m_godRays2, args2);
	}

	private IEnumerator ResetPositionsForGoldEvent()
	{
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		float num = 0.25f;
		Hashtable args = iTween.Hash("position", new Vector3(-1.211758f, -0.8f, -0.2575677f), "time", num, "isLocal", true, "easetype", iTween.EaseType.linear);
		iTween.MoveTo(m_rightCloud, args);
		Hashtable args2 = iTween.Hash("position", new Vector3(1.068925f, -0.8f, -0.197469f), "time", num, "isLocal", true, "easetype", iTween.EaseType.linear);
		iTween.MoveTo(m_leftCloud, args2);
		m_rightLaurel.transform.localRotation = Quaternion.Euler(Vector3.zero);
		Hashtable args3 = iTween.Hash("position", new Vector3(0.1723f, -0.206f, 0.753f), "time", num, "isLocal", true, "easetype", iTween.EaseType.linear);
		iTween.MoveTo(m_rightLaurel, args3);
		m_leftLaurel.transform.localRotation = Quaternion.Euler(Vector3.zero);
		Hashtable args4 = iTween.Hash("position", new Vector3(-0.2201783f, -0.318f, 0.753f), "time", num, "isLocal", true, "easetype", iTween.EaseType.linear);
		iTween.MoveTo(m_leftLaurel, args4);
		Hashtable args5 = iTween.Hash("z", -0.9677765f, "time", num, "isLocal", true, "easetype", iTween.EaseType.easeInOutBack);
		iTween.MoveTo(m_crown, args5);
		Hashtable args6 = iTween.Hash("rotation", new Vector3(0f, 20f, 0f), "time", num, "isLocal", true, "easetype", iTween.EaseType.linear);
		iTween.RotateTo(m_godRays, args6);
		Hashtable args7 = iTween.Hash("rotation", new Vector3(0f, -20f, 0f), "time", num, "isLocal", true, "easetype", iTween.EaseType.linear);
		iTween.RotateTo(m_godRays2, args7);
	}
}
