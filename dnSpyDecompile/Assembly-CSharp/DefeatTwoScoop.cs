using System;
using System.Collections;
using UnityEngine;

// Token: 0x020002C6 RID: 710
public class DefeatTwoScoop : EndGameTwoScoop
{
	// Token: 0x06002538 RID: 9528 RVA: 0x000BB244 File Offset: 0x000B9444
	protected override void ShowImpl()
	{
		this.m_heroActor.SetFullDefFromEntity(GameState.Get().GetFriendlySidePlayer().GetHero());
		this.m_heroActor.UpdateAllComponents();
		this.m_heroActor.TurnOffCollider();
		GameEntity gameEntity = GameState.Get().GetGameEntity();
		string bannerLabel = gameEntity.GetDefeatScreenBannerText();
		if (GameMgr.Get().LastGameData.GameResult == TAG_PLAYSTATE.TIED)
		{
			bannerLabel = gameEntity.GetTieScreenBannerText();
		}
		base.SetBannerLabel(bannerLabel);
		base.GetComponent<PlayMakerFSM>().SendEvent("Action");
		iTween.FadeTo(base.gameObject, 1f, 0.25f);
		base.gameObject.transform.localScale = new Vector3(EndGameTwoScoop.START_SCALE_VAL, EndGameTwoScoop.START_SCALE_VAL, EndGameTwoScoop.START_SCALE_VAL);
		Hashtable args = iTween.Hash(new object[]
		{
			"scale",
			new Vector3(EndGameTwoScoop.END_SCALE_VAL, EndGameTwoScoop.END_SCALE_VAL, EndGameTwoScoop.END_SCALE_VAL),
			"time",
			0.5f,
			"oncomplete",
			"PunchEndGameTwoScoop",
			"oncompletetarget",
			base.gameObject,
			"easetype",
			iTween.EaseType.easeOutBounce
		});
		iTween.ScaleTo(base.gameObject, args);
		Hashtable args2 = iTween.Hash(new object[]
		{
			"position",
			base.gameObject.transform.position + new Vector3(0.005f, 0.005f, 0.005f),
			"time",
			1.5f,
			"oncomplete",
			"TokyoDriftTo",
			"oncompletetarget",
			base.gameObject
		});
		iTween.MoveTo(base.gameObject, args2);
		this.AnimateCrownTo();
		this.AnimateLeftTrumpetTo();
		this.AnimateRightTrumpetTo();
		base.StartCoroutine(this.AnimateAll());
	}

	// Token: 0x06002539 RID: 9529 RVA: 0x000BB428 File Offset: 0x000B9628
	protected override void ResetPositions()
	{
		base.gameObject.transform.localPosition = EndGameTwoScoop.START_POSITION;
		base.gameObject.transform.eulerAngles = new Vector3(0f, 180f, 0f);
		this.m_rightTrumpet.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
		this.m_rightTrumpet.transform.localEulerAngles = new Vector3(0f, -180f, 0f);
		this.m_leftTrumpet.transform.localEulerAngles = new Vector3(0f, -180f, 0f);
		this.m_rightBanner.transform.localScale = new Vector3(1f, 1f, -0.0375f);
		this.m_rightBannerShred.transform.localScale = new Vector3(1f, 1f, 0.05f);
		this.m_rightCloud.transform.localPosition = new Vector3(-0.036f, -0.3f, 0.46f);
		this.m_leftCloud.transform.localPosition = new Vector3(-0.047f, -0.3f, 0.41f);
		this.m_crown.transform.localEulerAngles = new Vector3(-0.026f, 17f, 0.2f);
		this.m_defeatBanner.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
	}

	// Token: 0x0600253A RID: 9530 RVA: 0x000BB5B2 File Offset: 0x000B97B2
	private IEnumerator AnimateAll()
	{
		yield return new WaitForSeconds(0.25f);
		Hashtable args = iTween.Hash(new object[]
		{
			"scale",
			new Vector3(1f, 1f, 1.1f),
			"time",
			0.25f,
			"isLocal",
			true,
			"easetype",
			iTween.EaseType.easeOutBounce
		});
		iTween.ScaleTo(this.m_rightTrumpet, args);
		Hashtable args2 = iTween.Hash(new object[]
		{
			"z",
			1,
			"delay",
			0.5f,
			"time",
			1f,
			"isLocal",
			true,
			"easetype",
			iTween.EaseType.easeOutElastic
		});
		iTween.ScaleTo(this.m_rightBanner, args2);
		Hashtable args3 = iTween.Hash(new object[]
		{
			"z",
			1,
			"delay",
			0.5f,
			"time",
			1f,
			"isLocal",
			true,
			"easetype",
			iTween.EaseType.easeOutElastic
		});
		iTween.ScaleTo(this.m_rightBannerShred, args3);
		Hashtable args4 = iTween.Hash(new object[]
		{
			"x",
			-0.81f,
			"time",
			5,
			"isLocal",
			true,
			"easetype",
			iTween.EaseType.easeOutCubic,
			"oncomplete",
			"CloudTo",
			"oncompletetarget",
			base.gameObject
		});
		iTween.MoveTo(this.m_rightCloud, args4);
		Hashtable args5 = iTween.Hash(new object[]
		{
			"x",
			0.824f,
			"time",
			5,
			"isLocal",
			true,
			"easetype",
			iTween.EaseType.easeOutCubic
		});
		iTween.MoveTo(this.m_leftCloud, args5);
		Hashtable args6 = iTween.Hash(new object[]
		{
			"rotation",
			new Vector3(0f, 183f, 0f),
			"time",
			0.5f,
			"delay",
			0.75f,
			"isLocal",
			true,
			"easetype",
			iTween.EaseType.easeOutBounce
		});
		iTween.RotateTo(this.m_defeatBanner, args6);
		yield break;
	}

	// Token: 0x0600253B RID: 9531 RVA: 0x000BB5C4 File Offset: 0x000B97C4
	private void AnimateLeftTrumpetTo()
	{
		Hashtable args = iTween.Hash(new object[]
		{
			"rotation",
			new Vector3(0f, -184f, 0f),
			"time",
			5f,
			"isLocal",
			true,
			"easetype",
			iTween.EaseType.easeInOutCirc,
			"oncomplete",
			"AnimateLeftTrumpetFro",
			"oncompletetarget",
			base.gameObject
		});
		iTween.RotateTo(this.m_leftTrumpet, args);
	}

	// Token: 0x0600253C RID: 9532 RVA: 0x000BB66C File Offset: 0x000B986C
	private void AnimateLeftTrumpetFro()
	{
		Hashtable args = iTween.Hash(new object[]
		{
			"rotation",
			new Vector3(0f, -180f, 0f),
			"time",
			5f,
			"isLocal",
			true,
			"easetype",
			iTween.EaseType.easeInOutCirc,
			"oncomplete",
			"AnimateLeftTrumpetTo",
			"oncompletetarget",
			base.gameObject
		});
		iTween.RotateTo(this.m_leftTrumpet, args);
	}

	// Token: 0x0600253D RID: 9533 RVA: 0x000BB714 File Offset: 0x000B9914
	private void AnimateRightTrumpetTo()
	{
		Hashtable args = iTween.Hash(new object[]
		{
			"rotation",
			new Vector3(0f, -172f, 0f),
			"time",
			8f,
			"isLocal",
			true,
			"easetype",
			iTween.EaseType.easeInOutCirc,
			"oncomplete",
			"AnimateRightTrumpetFro",
			"oncompletetarget",
			base.gameObject
		});
		iTween.RotateTo(this.m_rightTrumpet, args);
	}

	// Token: 0x0600253E RID: 9534 RVA: 0x000BB7BC File Offset: 0x000B99BC
	private void AnimateRightTrumpetFro()
	{
		Hashtable args = iTween.Hash(new object[]
		{
			"rotation",
			new Vector3(0f, -180f, 0f),
			"time",
			8f,
			"isLocal",
			true,
			"easetype",
			iTween.EaseType.easeInOutCirc,
			"oncomplete",
			"AnimateRightTrumpetTo",
			"oncompletetarget",
			base.gameObject
		});
		iTween.RotateTo(this.m_rightTrumpet, args);
	}

	// Token: 0x0600253F RID: 9535 RVA: 0x000BB864 File Offset: 0x000B9A64
	private void TokyoDriftTo()
	{
		Hashtable args = iTween.Hash(new object[]
		{
			"position",
			EndGameTwoScoop.START_POSITION + new Vector3(0.2f, 0.2f, 0.2f),
			"time",
			10,
			"isLocal",
			true,
			"easetype",
			iTween.EaseType.linear,
			"oncomplete",
			"TokyoDriftFro",
			"oncompletetarget",
			base.gameObject
		});
		iTween.MoveTo(base.gameObject, args);
	}

	// Token: 0x06002540 RID: 9536 RVA: 0x000BB914 File Offset: 0x000B9B14
	private void TokyoDriftFro()
	{
		Hashtable args = iTween.Hash(new object[]
		{
			"position",
			EndGameTwoScoop.START_POSITION,
			"time",
			10,
			"isLocal",
			true,
			"easetype",
			iTween.EaseType.linear,
			"oncomplete",
			"TokyoDriftTo",
			"oncompletetarget",
			base.gameObject
		});
		iTween.MoveTo(base.gameObject, args);
	}

	// Token: 0x06002541 RID: 9537 RVA: 0x000BB9A8 File Offset: 0x000B9BA8
	private void CloudTo()
	{
		Hashtable args = iTween.Hash(new object[]
		{
			"x",
			-0.38f,
			"time",
			10,
			"isLocal",
			true,
			"easetype",
			iTween.EaseType.linear,
			"oncomplete",
			"CloudFro",
			"oncompletetarget",
			base.gameObject
		});
		iTween.MoveTo(this.m_rightCloud, args);
		Hashtable args2 = iTween.Hash(new object[]
		{
			"x",
			0.443f,
			"time",
			10,
			"isLocal",
			true,
			"easetype",
			iTween.EaseType.linear
		});
		iTween.MoveTo(this.m_leftCloud, args2);
	}

	// Token: 0x06002542 RID: 9538 RVA: 0x000BBAA0 File Offset: 0x000B9CA0
	private void CloudFro()
	{
		Hashtable args = iTween.Hash(new object[]
		{
			"x",
			-0.81f,
			"time",
			10,
			"isLocal",
			true,
			"easetype",
			iTween.EaseType.linear,
			"oncomplete",
			"CloudTo",
			"oncompletetarget",
			base.gameObject
		});
		iTween.MoveTo(this.m_rightCloud, args);
		Hashtable args2 = iTween.Hash(new object[]
		{
			"x",
			0.824f,
			"time",
			10,
			"isLocal",
			true,
			"easetype",
			iTween.EaseType.linear
		});
		iTween.MoveTo(this.m_leftCloud, args2);
	}

	// Token: 0x06002543 RID: 9539 RVA: 0x000BBB98 File Offset: 0x000B9D98
	private void AnimateCrownTo()
	{
		Hashtable args = iTween.Hash(new object[]
		{
			"rotation",
			new Vector3(0f, 1.8f, 0f),
			"time",
			0.75f,
			"isLocal",
			true,
			"easetype",
			iTween.EaseType.linear,
			"oncomplete",
			"AnimateCrownFro",
			"oncompletetarget",
			base.gameObject
		});
		iTween.RotateTo(this.m_crown, args);
	}

	// Token: 0x06002544 RID: 9540 RVA: 0x000BBC40 File Offset: 0x000B9E40
	private void AnimateCrownFro()
	{
		Hashtable args = iTween.Hash(new object[]
		{
			"rotation",
			new Vector3(0f, 17f, 0f),
			"time",
			0.75f,
			"isLocal",
			true,
			"easetype",
			iTween.EaseType.linear,
			"oncomplete",
			"AnimateCrownTo",
			"oncompletetarget",
			base.gameObject
		});
		iTween.RotateTo(this.m_crown, args);
	}

	// Token: 0x040014D3 RID: 5331
	public GameObject m_rightTrumpet;

	// Token: 0x040014D4 RID: 5332
	public GameObject m_rightBanner;

	// Token: 0x040014D5 RID: 5333
	public GameObject m_rightBannerShred;

	// Token: 0x040014D6 RID: 5334
	public GameObject m_rightCloud;

	// Token: 0x040014D7 RID: 5335
	public GameObject m_leftTrumpet;

	// Token: 0x040014D8 RID: 5336
	public GameObject m_leftBanner;

	// Token: 0x040014D9 RID: 5337
	public GameObject m_leftBannerFront;

	// Token: 0x040014DA RID: 5338
	public GameObject m_leftCloud;

	// Token: 0x040014DB RID: 5339
	public GameObject m_crown;

	// Token: 0x040014DC RID: 5340
	public GameObject m_defeatBanner;
}
