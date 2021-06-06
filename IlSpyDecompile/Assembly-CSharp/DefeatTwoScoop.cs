using System.Collections;
using UnityEngine;

public class DefeatTwoScoop : EndGameTwoScoop
{
	public GameObject m_rightTrumpet;

	public GameObject m_rightBanner;

	public GameObject m_rightBannerShred;

	public GameObject m_rightCloud;

	public GameObject m_leftTrumpet;

	public GameObject m_leftBanner;

	public GameObject m_leftBannerFront;

	public GameObject m_leftCloud;

	public GameObject m_crown;

	public GameObject m_defeatBanner;

	protected override void ShowImpl()
	{
		m_heroActor.SetFullDefFromEntity(GameState.Get().GetFriendlySidePlayer().GetHero());
		m_heroActor.UpdateAllComponents();
		m_heroActor.TurnOffCollider();
		GameEntity gameEntity = GameState.Get().GetGameEntity();
		string bannerLabel = gameEntity.GetDefeatScreenBannerText();
		if (GameMgr.Get().LastGameData.GameResult == TAG_PLAYSTATE.TIED)
		{
			bannerLabel = gameEntity.GetTieScreenBannerText();
		}
		SetBannerLabel(bannerLabel);
		GetComponent<PlayMakerFSM>().SendEvent("Action");
		iTween.FadeTo(base.gameObject, 1f, 0.25f);
		base.gameObject.transform.localScale = new Vector3(EndGameTwoScoop.START_SCALE_VAL, EndGameTwoScoop.START_SCALE_VAL, EndGameTwoScoop.START_SCALE_VAL);
		Hashtable args = iTween.Hash("scale", new Vector3(EndGameTwoScoop.END_SCALE_VAL, EndGameTwoScoop.END_SCALE_VAL, EndGameTwoScoop.END_SCALE_VAL), "time", 0.5f, "oncomplete", "PunchEndGameTwoScoop", "oncompletetarget", base.gameObject, "easetype", iTween.EaseType.easeOutBounce);
		iTween.ScaleTo(base.gameObject, args);
		Hashtable args2 = iTween.Hash("position", base.gameObject.transform.position + new Vector3(0.005f, 0.005f, 0.005f), "time", 1.5f, "oncomplete", "TokyoDriftTo", "oncompletetarget", base.gameObject);
		iTween.MoveTo(base.gameObject, args2);
		AnimateCrownTo();
		AnimateLeftTrumpetTo();
		AnimateRightTrumpetTo();
		StartCoroutine(AnimateAll());
	}

	protected override void ResetPositions()
	{
		base.gameObject.transform.localPosition = EndGameTwoScoop.START_POSITION;
		base.gameObject.transform.eulerAngles = new Vector3(0f, 180f, 0f);
		m_rightTrumpet.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
		m_rightTrumpet.transform.localEulerAngles = new Vector3(0f, -180f, 0f);
		m_leftTrumpet.transform.localEulerAngles = new Vector3(0f, -180f, 0f);
		m_rightBanner.transform.localScale = new Vector3(1f, 1f, -0.0375f);
		m_rightBannerShred.transform.localScale = new Vector3(1f, 1f, 0.05f);
		m_rightCloud.transform.localPosition = new Vector3(-0.036f, -0.3f, 0.46f);
		m_leftCloud.transform.localPosition = new Vector3(-0.047f, -0.3f, 0.41f);
		m_crown.transform.localEulerAngles = new Vector3(-0.026f, 17f, 0.2f);
		m_defeatBanner.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
	}

	private IEnumerator AnimateAll()
	{
		yield return new WaitForSeconds(0.25f);
		Hashtable args = iTween.Hash("scale", new Vector3(1f, 1f, 1.1f), "time", 0.25f, "isLocal", true, "easetype", iTween.EaseType.easeOutBounce);
		iTween.ScaleTo(m_rightTrumpet, args);
		Hashtable args2 = iTween.Hash("z", 1, "delay", 0.5f, "time", 1f, "isLocal", true, "easetype", iTween.EaseType.easeOutElastic);
		iTween.ScaleTo(m_rightBanner, args2);
		Hashtable args3 = iTween.Hash("z", 1, "delay", 0.5f, "time", 1f, "isLocal", true, "easetype", iTween.EaseType.easeOutElastic);
		iTween.ScaleTo(m_rightBannerShred, args3);
		Hashtable args4 = iTween.Hash("x", -0.81f, "time", 5, "isLocal", true, "easetype", iTween.EaseType.easeOutCubic, "oncomplete", "CloudTo", "oncompletetarget", base.gameObject);
		iTween.MoveTo(m_rightCloud, args4);
		Hashtable args5 = iTween.Hash("x", 0.824f, "time", 5, "isLocal", true, "easetype", iTween.EaseType.easeOutCubic);
		iTween.MoveTo(m_leftCloud, args5);
		Hashtable args6 = iTween.Hash("rotation", new Vector3(0f, 183f, 0f), "time", 0.5f, "delay", 0.75f, "isLocal", true, "easetype", iTween.EaseType.easeOutBounce);
		iTween.RotateTo(m_defeatBanner, args6);
	}

	private void AnimateLeftTrumpetTo()
	{
		Hashtable args = iTween.Hash("rotation", new Vector3(0f, -184f, 0f), "time", 5f, "isLocal", true, "easetype", iTween.EaseType.easeInOutCirc, "oncomplete", "AnimateLeftTrumpetFro", "oncompletetarget", base.gameObject);
		iTween.RotateTo(m_leftTrumpet, args);
	}

	private void AnimateLeftTrumpetFro()
	{
		Hashtable args = iTween.Hash("rotation", new Vector3(0f, -180f, 0f), "time", 5f, "isLocal", true, "easetype", iTween.EaseType.easeInOutCirc, "oncomplete", "AnimateLeftTrumpetTo", "oncompletetarget", base.gameObject);
		iTween.RotateTo(m_leftTrumpet, args);
	}

	private void AnimateRightTrumpetTo()
	{
		Hashtable args = iTween.Hash("rotation", new Vector3(0f, -172f, 0f), "time", 8f, "isLocal", true, "easetype", iTween.EaseType.easeInOutCirc, "oncomplete", "AnimateRightTrumpetFro", "oncompletetarget", base.gameObject);
		iTween.RotateTo(m_rightTrumpet, args);
	}

	private void AnimateRightTrumpetFro()
	{
		Hashtable args = iTween.Hash("rotation", new Vector3(0f, -180f, 0f), "time", 8f, "isLocal", true, "easetype", iTween.EaseType.easeInOutCirc, "oncomplete", "AnimateRightTrumpetTo", "oncompletetarget", base.gameObject);
		iTween.RotateTo(m_rightTrumpet, args);
	}

	private void TokyoDriftTo()
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
		Hashtable args = iTween.Hash("x", -0.38f, "time", 10, "isLocal", true, "easetype", iTween.EaseType.linear, "oncomplete", "CloudFro", "oncompletetarget", base.gameObject);
		iTween.MoveTo(m_rightCloud, args);
		Hashtable args2 = iTween.Hash("x", 0.443f, "time", 10, "isLocal", true, "easetype", iTween.EaseType.linear);
		iTween.MoveTo(m_leftCloud, args2);
	}

	private void CloudFro()
	{
		Hashtable args = iTween.Hash("x", -0.81f, "time", 10, "isLocal", true, "easetype", iTween.EaseType.linear, "oncomplete", "CloudTo", "oncompletetarget", base.gameObject);
		iTween.MoveTo(m_rightCloud, args);
		Hashtable args2 = iTween.Hash("x", 0.824f, "time", 10, "isLocal", true, "easetype", iTween.EaseType.linear);
		iTween.MoveTo(m_leftCloud, args2);
	}

	private void AnimateCrownTo()
	{
		Hashtable args = iTween.Hash("rotation", new Vector3(0f, 1.8f, 0f), "time", 0.75f, "isLocal", true, "easetype", iTween.EaseType.linear, "oncomplete", "AnimateCrownFro", "oncompletetarget", base.gameObject);
		iTween.RotateTo(m_crown, args);
	}

	private void AnimateCrownFro()
	{
		Hashtable args = iTween.Hash("rotation", new Vector3(0f, 17f, 0f), "time", 0.75f, "isLocal", true, "easetype", iTween.EaseType.linear, "oncomplete", "AnimateCrownTo", "oncompletetarget", base.gameObject);
		iTween.RotateTo(m_crown, args);
	}
}
