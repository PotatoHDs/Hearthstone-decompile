using System;
using System.Collections;
using UnityEngine;

// Token: 0x020002D6 RID: 726
public class VictoryTwoScoop : EndGameTwoScoop
{
	// Token: 0x06002624 RID: 9764 RVA: 0x000BF869 File Offset: 0x000BDA69
	public void StopFireworksAudio()
	{
		if (this.m_fireworksAudio != null)
		{
			SoundManager.Get().Stop(this.m_fireworksAudio);
		}
	}

	// Token: 0x06002625 RID: 9765 RVA: 0x000BF88C File Offset: 0x000BDA8C
	public void SetOverrideHero(EntityDef overrideHero)
	{
		if (overrideHero == null)
		{
			this.m_overrideHeroEntityDef = null;
			DefLoader.DisposableCardDef overrideHeroCardDef = this.m_overrideHeroCardDef;
			if (overrideHeroCardDef != null)
			{
				overrideHeroCardDef.Dispose();
			}
			this.m_overrideHeroCardDef = null;
			return;
		}
		if (!overrideHero.IsHero())
		{
			Log.Gameplay.PrintError("VictoryTwoScoop.SetOverrideHero() - passed EntityDef {0} is not a hero!", new object[]
			{
				overrideHero
			});
			return;
		}
		DefLoader.DisposableCardDef cardDef = DefLoader.Get().GetCardDef(overrideHero.GetCardId(), null);
		if (cardDef == null)
		{
			Log.Gameplay.PrintError("VictoryTwoScoop.SetOverrideHero() - passed EntityDef {0} does not have a CardDef!", new object[]
			{
				overrideHero
			});
			return;
		}
		this.m_overrideHeroEntityDef = overrideHero;
		DefLoader.DisposableCardDef overrideHeroCardDef2 = this.m_overrideHeroCardDef;
		if (overrideHeroCardDef2 != null)
		{
			overrideHeroCardDef2.Dispose();
		}
		this.m_overrideHeroCardDef = cardDef;
	}

	// Token: 0x06002626 RID: 9766 RVA: 0x000BF92C File Offset: 0x000BDB2C
	public override void OnDestroy()
	{
		DefLoader.DisposableCardDef overrideHeroCardDef = this.m_overrideHeroCardDef;
		if (overrideHeroCardDef != null)
		{
			overrideHeroCardDef.Dispose();
		}
		this.m_overrideHeroCardDef = null;
		base.OnDestroy();
	}

	// Token: 0x06002627 RID: 9767 RVA: 0x000BF94C File Offset: 0x000BDB4C
	protected override void ShowImpl()
	{
		this.SetupHeroActor();
		this.SetupBannerText();
		this.PlayShowAnimations();
	}

	// Token: 0x06002628 RID: 9768 RVA: 0x000BF960 File Offset: 0x000BDB60
	protected override void ResetPositions()
	{
		base.gameObject.transform.localPosition = EndGameTwoScoop.START_POSITION;
		base.gameObject.transform.eulerAngles = new Vector3(0f, 180f, 0f);
		if (this.m_rightTrumpet != null)
		{
			this.m_rightTrumpet.transform.localPosition = new Vector3(0.23f, -0.6f, 0.16f);
			this.m_rightTrumpet.transform.localScale = new Vector3(1f, 1f, 1f);
		}
		if (this.m_leftTrumpet != null)
		{
			this.m_leftTrumpet.transform.localPosition = new Vector3(-0.23f, -0.6f, 0.16f);
			this.m_leftTrumpet.transform.localScale = new Vector3(-1f, 1f, 1f);
		}
		if (this.m_rightBanner != null)
		{
			this.m_rightBanner.transform.localScale = new Vector3(1f, 1f, 0.08f);
		}
		if (this.m_leftBanner != null)
		{
			this.m_leftBanner.transform.localScale = new Vector3(1f, 1f, 0.08f);
		}
		if (this.m_rightCloud != null)
		{
			this.m_rightCloud.transform.localPosition = new Vector3(-0.2f, -0.8f, 0.26f);
		}
		if (this.m_leftCloud != null)
		{
			this.m_leftCloud.transform.localPosition = new Vector3(0.16f, -0.8f, 0.2f);
		}
		if (this.m_godRays != null)
		{
			this.m_godRays.transform.localEulerAngles = new Vector3(0f, 29f, 0f);
		}
		if (this.m_godRays2 != null)
		{
			this.m_godRays2.transform.localEulerAngles = new Vector3(0f, -29f, 0f);
		}
		if (this.m_crown != null)
		{
			this.m_crown.transform.localPosition = new Vector3(-0.041f, -0.06f, -0.834f);
		}
		if (this.m_rightLaurel != null)
		{
			this.m_rightLaurel.transform.localEulerAngles = new Vector3(0f, -90f, 0f);
			this.m_rightLaurel.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
		}
		if (this.m_leftLaurel != null)
		{
			this.m_leftLaurel.transform.localEulerAngles = new Vector3(0f, 90f, 0f);
			this.m_leftLaurel.transform.localScale = new Vector3(-0.7f, 0.7f, 0.7f);
		}
	}

	// Token: 0x06002629 RID: 9769 RVA: 0x000BFC5C File Offset: 0x000BDE5C
	public override void StopAnimating()
	{
		base.StopCoroutine("AnimateAll");
		iTween.Stop(base.gameObject, true);
		base.StartCoroutine(this.ResetPositionsForGoldEvent());
	}

	// Token: 0x0600262A RID: 9770 RVA: 0x000BFC84 File Offset: 0x000BDE84
	protected void SetupHeroActor()
	{
		if (this.m_overrideHeroEntityDef != null && this.m_overrideHeroCardDef != null)
		{
			this.m_heroActor.SetEntityDef(this.m_overrideHeroEntityDef);
			this.m_heroActor.SetCardDef(this.m_overrideHeroCardDef);
		}
		else
		{
			this.m_heroActor.SetFullDefFromEntity(GameState.Get().GetFriendlySidePlayer().GetHero());
		}
		this.m_heroActor.UpdateAllComponents();
		this.m_heroActor.TurnOffCollider();
	}

	// Token: 0x0600262B RID: 9771 RVA: 0x000BFCF8 File Offset: 0x000BDEF8
	protected void SetupBannerText()
	{
		string victoryScreenBannerText = GameState.Get().GetGameEntity().GetVictoryScreenBannerText();
		base.SetBannerLabel(victoryScreenBannerText);
	}

	// Token: 0x0600262C RID: 9772 RVA: 0x000BFD1C File Offset: 0x000BDF1C
	protected void PlayShowAnimations()
	{
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
			base.gameObject
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
		this.AnimateGodraysTo();
		this.AnimateCrownTo();
		base.StartCoroutine(this.AnimateAll());
	}

	// Token: 0x0600262D RID: 9773 RVA: 0x000BFE84 File Offset: 0x000BE084
	private IEnumerator AnimateAll()
	{
		yield return new WaitForSeconds(0.25f);
		float num = 0.4f;
		Hashtable args = iTween.Hash(new object[]
		{
			"position",
			new Vector3(-0.52f, -0.6f, -0.23f),
			"time",
			num,
			"isLocal",
			true,
			"easetype",
			iTween.EaseType.easeOutElastic
		});
		iTween.MoveTo(this.m_rightTrumpet, args);
		Hashtable args2 = iTween.Hash(new object[]
		{
			"position",
			new Vector3(0.44f, -0.6f, -0.23f),
			"time",
			num,
			"isLocal",
			true,
			"easetype",
			iTween.EaseType.easeOutElastic
		});
		iTween.MoveTo(this.m_leftTrumpet, args2);
		Hashtable args3 = iTween.Hash(new object[]
		{
			"scale",
			new Vector3(1.1f, 1.1f, 1.1f),
			"time",
			0.25f,
			"delay",
			0.3f,
			"isLocal",
			true,
			"easetype",
			iTween.EaseType.easeOutBounce
		});
		iTween.ScaleTo(this.m_rightTrumpet, args3);
		Hashtable args4 = iTween.Hash(new object[]
		{
			"scale",
			new Vector3(-1.1f, 1.1f, 1.1f),
			"time",
			0.25f,
			"delay",
			0.3f,
			"isLocal",
			true,
			"easetype",
			iTween.EaseType.easeOutBounce
		});
		iTween.ScaleTo(this.m_leftTrumpet, args4);
		Hashtable args5 = iTween.Hash(new object[]
		{
			"z",
			1,
			"delay",
			0.24f,
			"time",
			1f,
			"isLocal",
			true,
			"easetype",
			iTween.EaseType.easeOutElastic
		});
		iTween.ScaleTo(this.m_rightBanner, args5);
		Hashtable args6 = iTween.Hash(new object[]
		{
			"z",
			1,
			"delay",
			0.24f,
			"time",
			1f,
			"isLocal",
			true,
			"easetype",
			iTween.EaseType.easeOutElastic
		});
		iTween.ScaleTo(this.m_leftBanner, args6);
		Hashtable args7 = iTween.Hash(new object[]
		{
			"x",
			-1.227438,
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
		iTween.MoveTo(this.m_rightCloud, args7);
		Hashtable args8 = iTween.Hash(new object[]
		{
			"x",
			1.053244,
			"time",
			5,
			"isLocal",
			true,
			"easetype",
			iTween.EaseType.easeOutCubic
		});
		iTween.MoveTo(this.m_leftCloud, args8);
		Hashtable args9 = iTween.Hash(new object[]
		{
			"rotation",
			new Vector3(0f, 2f, 0f),
			"time",
			0.5f,
			"isLocal",
			true,
			"easetype",
			iTween.EaseType.easeOutElastic,
			"oncomplete",
			"LaurelWaveTo",
			"oncompletetarget",
			base.gameObject
		});
		iTween.RotateTo(this.m_rightLaurel, args9);
		Hashtable args10 = iTween.Hash(new object[]
		{
			"scale",
			new Vector3(1f, 1f, 1f),
			"time",
			0.25f,
			"isLocal",
			true,
			"easetype",
			iTween.EaseType.easeOutBounce
		});
		iTween.ScaleTo(this.m_rightLaurel, args10);
		Hashtable args11 = iTween.Hash(new object[]
		{
			"rotation",
			new Vector3(0f, -2f, 0f),
			"time",
			0.5f,
			"isLocal",
			true,
			"easetype",
			iTween.EaseType.easeOutElastic
		});
		iTween.RotateTo(this.m_leftLaurel, args11);
		Hashtable args12 = iTween.Hash(new object[]
		{
			"scale",
			new Vector3(-1f, 1f, 1f),
			"time",
			0.25f,
			"isLocal",
			true,
			"easetype",
			iTween.EaseType.easeOutBounce
		});
		iTween.ScaleTo(this.m_leftLaurel, args12);
		yield break;
	}

	// Token: 0x0600262E RID: 9774 RVA: 0x000BFE94 File Offset: 0x000BE094
	protected void TokyoDriftTo()
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

	// Token: 0x0600262F RID: 9775 RVA: 0x000BFF44 File Offset: 0x000BE144
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

	// Token: 0x06002630 RID: 9776 RVA: 0x000BFFD8 File Offset: 0x000BE1D8
	private void CloudTo()
	{
		Hashtable args = iTween.Hash(new object[]
		{
			"x",
			-0.92f,
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
			0.82f,
			"time",
			10,
			"isLocal",
			true,
			"easetype",
			iTween.EaseType.linear
		});
		iTween.MoveTo(this.m_leftCloud, args2);
	}

	// Token: 0x06002631 RID: 9777 RVA: 0x000C00D0 File Offset: 0x000BE2D0
	private void CloudFro()
	{
		Hashtable args = iTween.Hash(new object[]
		{
			"x",
			-1.227438f,
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
			1.053244f,
			"time",
			10,
			"isLocal",
			true,
			"easetype",
			iTween.EaseType.linear
		});
		iTween.MoveTo(this.m_leftCloud, args2);
	}

	// Token: 0x06002632 RID: 9778 RVA: 0x000C01C8 File Offset: 0x000BE3C8
	private void LaurelWaveTo()
	{
		Hashtable args = iTween.Hash(new object[]
		{
			"rotation",
			new Vector3(0f, 0f, 0f),
			"time",
			10,
			"isLocal",
			true,
			"easetype",
			iTween.EaseType.linear,
			"oncomplete",
			"LaurelWaveFro",
			"oncompletetarget",
			base.gameObject
		});
		iTween.RotateTo(this.m_rightLaurel, args);
		Hashtable args2 = iTween.Hash(new object[]
		{
			"rotation",
			new Vector3(0f, 0f, 0f),
			"time",
			10,
			"isLocal",
			true,
			"easetype",
			iTween.EaseType.linear
		});
		iTween.RotateTo(this.m_leftLaurel, args2);
	}

	// Token: 0x06002633 RID: 9779 RVA: 0x000C02DC File Offset: 0x000BE4DC
	private void LaurelWaveFro()
	{
		Hashtable args = iTween.Hash(new object[]
		{
			"rotation",
			new Vector3(0f, 2f, 0f),
			"time",
			10,
			"isLocal",
			true,
			"easetype",
			iTween.EaseType.linear,
			"oncomplete",
			"LaurelWaveTo",
			"oncompletetarget",
			base.gameObject
		});
		iTween.RotateTo(this.m_rightLaurel, args);
		Hashtable args2 = iTween.Hash(new object[]
		{
			"rotation",
			new Vector3(0f, -2f, 0f),
			"time",
			10,
			"isLocal",
			true,
			"easetype",
			iTween.EaseType.linear
		});
		iTween.RotateTo(this.m_leftLaurel, args2);
	}

	// Token: 0x06002634 RID: 9780 RVA: 0x000C03F0 File Offset: 0x000BE5F0
	protected void AnimateCrownTo()
	{
		Hashtable args = iTween.Hash(new object[]
		{
			"z",
			-0.78f,
			"time",
			5,
			"isLocal",
			true,
			"easetype",
			iTween.EaseType.easeInOutBack,
			"oncomplete",
			"AnimateCrownFro",
			"oncompletetarget",
			base.gameObject
		});
		iTween.MoveTo(this.m_crown, args);
	}

	// Token: 0x06002635 RID: 9781 RVA: 0x000C0484 File Offset: 0x000BE684
	private void AnimateCrownFro()
	{
		Hashtable args = iTween.Hash(new object[]
		{
			"z",
			-0.834f,
			"time",
			5,
			"isLocal",
			true,
			"easetype",
			iTween.EaseType.easeInOutBack,
			"oncomplete",
			"AnimateCrownTo",
			"oncompletetarget",
			base.gameObject
		});
		iTween.MoveTo(this.m_crown, args);
	}

	// Token: 0x06002636 RID: 9782 RVA: 0x000C0518 File Offset: 0x000BE718
	protected void AnimateGodraysTo()
	{
		Hashtable args = iTween.Hash(new object[]
		{
			"rotation",
			new Vector3(0f, -20f, 0f),
			"time",
			20f,
			"isLocal",
			true,
			"easetype",
			iTween.EaseType.linear,
			"oncomplete",
			"AnimateGodraysFro",
			"oncompletetarget",
			base.gameObject
		});
		iTween.RotateTo(this.m_godRays, args);
		Hashtable args2 = iTween.Hash(new object[]
		{
			"rotation",
			new Vector3(0f, 20f, 0f),
			"time",
			20f,
			"isLocal",
			true,
			"easetype",
			iTween.EaseType.linear
		});
		iTween.RotateTo(this.m_godRays2, args2);
	}

	// Token: 0x06002637 RID: 9783 RVA: 0x000C0634 File Offset: 0x000BE834
	private void AnimateGodraysFro()
	{
		Hashtable args = iTween.Hash(new object[]
		{
			"rotation",
			new Vector3(0f, 20f, 0f),
			"time",
			20f,
			"isLocal",
			true,
			"easetype",
			iTween.EaseType.linear,
			"oncomplete",
			"AnimateGodraysTo",
			"oncompletetarget",
			base.gameObject
		});
		iTween.RotateTo(this.m_godRays, args);
		Hashtable args2 = iTween.Hash(new object[]
		{
			"rotation",
			new Vector3(0f, -20f, 0f),
			"time",
			20f,
			"isLocal",
			true,
			"easetype",
			iTween.EaseType.linear
		});
		iTween.RotateTo(this.m_godRays2, args2);
	}

	// Token: 0x06002638 RID: 9784 RVA: 0x000C074E File Offset: 0x000BE94E
	private IEnumerator ResetPositionsForGoldEvent()
	{
		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();
		float num = 0.25f;
		Hashtable args = iTween.Hash(new object[]
		{
			"position",
			new Vector3(-1.211758f, -0.8f, -0.2575677f),
			"time",
			num,
			"isLocal",
			true,
			"easetype",
			iTween.EaseType.linear
		});
		iTween.MoveTo(this.m_rightCloud, args);
		Hashtable args2 = iTween.Hash(new object[]
		{
			"position",
			new Vector3(1.068925f, -0.8f, -0.197469f),
			"time",
			num,
			"isLocal",
			true,
			"easetype",
			iTween.EaseType.linear
		});
		iTween.MoveTo(this.m_leftCloud, args2);
		this.m_rightLaurel.transform.localRotation = Quaternion.Euler(Vector3.zero);
		Hashtable args3 = iTween.Hash(new object[]
		{
			"position",
			new Vector3(0.1723f, -0.206f, 0.753f),
			"time",
			num,
			"isLocal",
			true,
			"easetype",
			iTween.EaseType.linear
		});
		iTween.MoveTo(this.m_rightLaurel, args3);
		this.m_leftLaurel.transform.localRotation = Quaternion.Euler(Vector3.zero);
		Hashtable args4 = iTween.Hash(new object[]
		{
			"position",
			new Vector3(-0.2201783f, -0.318f, 0.753f),
			"time",
			num,
			"isLocal",
			true,
			"easetype",
			iTween.EaseType.linear
		});
		iTween.MoveTo(this.m_leftLaurel, args4);
		Hashtable args5 = iTween.Hash(new object[]
		{
			"z",
			-0.9677765f,
			"time",
			num,
			"isLocal",
			true,
			"easetype",
			iTween.EaseType.easeInOutBack
		});
		iTween.MoveTo(this.m_crown, args5);
		Hashtable args6 = iTween.Hash(new object[]
		{
			"rotation",
			new Vector3(0f, 20f, 0f),
			"time",
			num,
			"isLocal",
			true,
			"easetype",
			iTween.EaseType.linear
		});
		iTween.RotateTo(this.m_godRays, args6);
		Hashtable args7 = iTween.Hash(new object[]
		{
			"rotation",
			new Vector3(0f, -20f, 0f),
			"time",
			num,
			"isLocal",
			true,
			"easetype",
			iTween.EaseType.linear
		});
		iTween.RotateTo(this.m_godRays2, args7);
		yield break;
	}

	// Token: 0x04001580 RID: 5504
	public GameObject m_godRays;

	// Token: 0x04001581 RID: 5505
	public GameObject m_godRays2;

	// Token: 0x04001582 RID: 5506
	public GameObject m_rightTrumpet;

	// Token: 0x04001583 RID: 5507
	public GameObject m_rightBanner;

	// Token: 0x04001584 RID: 5508
	public GameObject m_rightCloud;

	// Token: 0x04001585 RID: 5509
	public GameObject m_rightLaurel;

	// Token: 0x04001586 RID: 5510
	public GameObject m_leftTrumpet;

	// Token: 0x04001587 RID: 5511
	public GameObject m_leftBanner;

	// Token: 0x04001588 RID: 5512
	public GameObject m_leftCloud;

	// Token: 0x04001589 RID: 5513
	public GameObject m_leftLaurel;

	// Token: 0x0400158A RID: 5514
	public GameObject m_crown;

	// Token: 0x0400158B RID: 5515
	public AudioSource m_fireworksAudio;

	// Token: 0x0400158C RID: 5516
	private const float GOD_RAY_ANGLE = 20f;

	// Token: 0x0400158D RID: 5517
	private const float GOD_RAY_DURATION = 20f;

	// Token: 0x0400158E RID: 5518
	private const float LAUREL_ROTATION = 2f;

	// Token: 0x0400158F RID: 5519
	protected EntityDef m_overrideHeroEntityDef;

	// Token: 0x04001590 RID: 5520
	protected DefLoader.DisposableCardDef m_overrideHeroCardDef;
}
