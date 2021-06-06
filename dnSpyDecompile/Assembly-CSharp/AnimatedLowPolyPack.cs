using System;
using System.Collections;
using UnityEngine;

// Token: 0x020006E9 RID: 1769
[CustomEditClass]
public class AnimatedLowPolyPack : MonoBehaviour
{
	// Token: 0x170005DA RID: 1498
	// (get) Token: 0x06006283 RID: 25219 RVA: 0x002025B7 File Offset: 0x002007B7
	// (set) Token: 0x06006284 RID: 25220 RVA: 0x002025BF File Offset: 0x002007BF
	public int Column { get; private set; }

	// Token: 0x170005DB RID: 1499
	// (get) Token: 0x06006285 RID: 25221 RVA: 0x002025C8 File Offset: 0x002007C8
	// (set) Token: 0x06006286 RID: 25222 RVA: 0x002025E5 File Offset: 0x002007E5
	public bool IsShowingShadow
	{
		get
		{
			return this.m_shadow != null && this.m_shadow.activeSelf;
		}
		set
		{
			if (this.m_shadow != null && value != this.m_shadow.activeSelf)
			{
				this.m_shadow.SetActive(value);
			}
		}
	}

	// Token: 0x06006287 RID: 25223 RVA: 0x0020260F File Offset: 0x0020080F
	public void Init(int column, Vector3 targetLocalPos, Vector3 offScreenOffset, bool ignoreFullscreenEffects = true, bool changeActivation = true)
	{
		this.m_targetLocalPos = targetLocalPos;
		this.m_targetOffScreenLocalPos = targetLocalPos + offScreenOffset;
		this.m_changeActivation = changeActivation;
		this.Column = column;
		if (ignoreFullscreenEffects)
		{
			SceneUtils.SetLayer(base.gameObject, GameLayer.IgnoreFullScreenEffects);
		}
		this.PositionOffScreen();
	}

	// Token: 0x06006288 RID: 25224 RVA: 0x0020264C File Offset: 0x0020084C
	public void FlyInImmediate()
	{
		iTween.Stop(base.gameObject);
		base.transform.localEulerAngles = this.m_flyInLocalAngles;
		base.transform.localPosition = this.m_targetLocalPos;
		this.m_state = AnimatedLowPolyPack.State.FLOWN_IN;
		if (this.m_changeActivation)
		{
			base.gameObject.SetActive(true);
		}
		if (this.m_FirstPurchaseBox != null)
		{
			this.m_FirstPurchaseBox.RevealContents();
		}
	}

	// Token: 0x06006289 RID: 25225 RVA: 0x002026BC File Offset: 0x002008BC
	public bool FlyIn(float animTime, float delay)
	{
		if (this.m_state == AnimatedLowPolyPack.State.FLOWN_IN)
		{
			return false;
		}
		if (this.m_state == AnimatedLowPolyPack.State.FLYING_IN)
		{
			return false;
		}
		this.m_state = AnimatedLowPolyPack.State.FLYING_IN;
		if (this.m_changeActivation)
		{
			base.gameObject.SetActive(true);
		}
		base.transform.localEulerAngles = this.m_flyInLocalAngles;
		if (this.m_FirstPurchaseBox != null)
		{
			this.m_FirstPurchaseBox.Reset();
		}
		Hashtable args = iTween.Hash(new object[]
		{
			"position",
			this.m_targetLocalPos,
			"isLocal",
			true,
			"time",
			animTime,
			"delay",
			delay,
			"easetype",
			iTween.EaseType.easeInCubic,
			"oncomplete",
			"OnFlownIn",
			"oncompletetarget",
			base.gameObject
		});
		iTween.Stop(base.gameObject);
		iTween.MoveTo(base.gameObject, args);
		return true;
	}

	// Token: 0x0600628A RID: 25226 RVA: 0x002027C9 File Offset: 0x002009C9
	public void FlyOutImmediate()
	{
		iTween.Stop(base.gameObject);
		base.transform.localEulerAngles = this.m_flyOutLocalAngles;
		base.transform.localPosition = this.m_targetOffScreenLocalPos;
		this.OnHidden();
	}

	// Token: 0x0600628B RID: 25227 RVA: 0x00202800 File Offset: 0x00200A00
	public bool FlyOut(float animTime, float delay)
	{
		if (this.m_state == AnimatedLowPolyPack.State.HIDDEN)
		{
			return false;
		}
		if (this.m_state == AnimatedLowPolyPack.State.FLYING_OUT)
		{
			return false;
		}
		this.m_state = AnimatedLowPolyPack.State.FLYING_OUT;
		base.transform.localEulerAngles = this.m_flyOutLocalAngles;
		Hashtable args = iTween.Hash(new object[]
		{
			"position",
			this.m_targetOffScreenLocalPos,
			"isLocal",
			true,
			"time",
			animTime,
			"delay",
			delay,
			"easetype",
			iTween.EaseType.linear,
			"oncomplete",
			"OnHidden",
			"oncompletetarget",
			base.gameObject
		});
		iTween.Stop(base.gameObject);
		iTween.MoveTo(base.gameObject, args);
		if (!string.IsNullOrEmpty(this.m_FlyOutSound))
		{
			SoundManager.Get().LoadAndPlay(this.m_FlyOutSound);
		}
		return true;
	}

	// Token: 0x0600628C RID: 25228 RVA: 0x00202903 File Offset: 0x00200B03
	public void SetFlyingLocalRotations(Vector3 flyInLocalAngles, Vector3 flyOutLocalAngles)
	{
		this.m_flyInLocalAngles = flyInLocalAngles;
		this.m_flyOutLocalAngles = flyOutLocalAngles;
	}

	// Token: 0x0600628D RID: 25229 RVA: 0x00202913 File Offset: 0x00200B13
	public AnimatedLowPolyPack.State GetState()
	{
		return this.m_state;
	}

	// Token: 0x0600628E RID: 25230 RVA: 0x0020291B File Offset: 0x00200B1B
	public void Hide()
	{
		this.OnHidden();
	}

	// Token: 0x0600628F RID: 25231 RVA: 0x00202923 File Offset: 0x00200B23
	public FirstPurchaseBox GetFirstPurchaseBox()
	{
		return this.m_FirstPurchaseBox;
	}

	// Token: 0x06006290 RID: 25232 RVA: 0x0020292B File Offset: 0x00200B2B
	public void UpdateBannerCount(int count)
	{
		base.StartCoroutine(this.UpdateBannerCountCoroutine(count));
	}

	// Token: 0x06006291 RID: 25233 RVA: 0x0020293C File Offset: 0x00200B3C
	public void UpdateBannerCountImmediately(int count)
	{
		if (this.m_AmountBanner != null && this.m_AmountBannerText != null)
		{
			this.m_AmountBanner.SetActive(true);
			this.m_lastVisibleBannerCount = count;
			this.m_AmountBannerText.Text = this.m_lastVisibleBannerCount.ToString();
		}
	}

	// Token: 0x06006292 RID: 25234 RVA: 0x0020298E File Offset: 0x00200B8E
	private IEnumerator UpdateBannerCountCoroutine(int count)
	{
		if (this.m_AmountBanner != null && this.m_AmountBannerText != null)
		{
			this.m_AmountBanner.SetActive(true);
			if (this.m_lastVisibleBannerCount == count)
			{
				yield break;
			}
			if (this.m_amountBannerFlashing)
			{
				this.m_AmountBannerText.Text = this.m_lastVisibleBannerCount.ToString();
			}
			this.m_lastVisibleBannerCount = count;
			if (this.m_amountFlash != null && this.m_amountFlashAnimController != null)
			{
				this.m_amountFlash.SetActive(false);
				this.m_amountFlash.SetActive(true);
				this.m_amountFlashAnimController.enabled = true;
				this.m_amountFlashAnimController.StopPlayback();
				yield return new WaitForEndOfFrame();
				if (this.m_amountFlashAnimController == null)
				{
					yield break;
				}
				this.m_amountFlashAnimController.Play("Flash");
				this.m_amountBannerFlashing = true;
				while (this.m_amountFlashAnimController != null && this.m_amountFlashAnimController.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.5f)
				{
					yield return null;
				}
			}
			this.m_AmountBannerText.Text = count.ToString();
			this.m_amountBannerFlashing = false;
		}
		yield break;
	}

	// Token: 0x06006293 RID: 25235 RVA: 0x002029A4 File Offset: 0x00200BA4
	public void HideBanner()
	{
		if (this.m_AmountBanner != null)
		{
			this.m_AmountBanner.SetActive(false);
		}
	}

	// Token: 0x06006294 RID: 25236 RVA: 0x002029C0 File Offset: 0x00200BC0
	private void OnHidden()
	{
		this.m_state = AnimatedLowPolyPack.State.HIDDEN;
		base.StopCoroutine("UpdateBannerCount");
		if (this.m_changeActivation)
		{
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x06006295 RID: 25237 RVA: 0x002029E8 File Offset: 0x00200BE8
	private void OnFlownIn()
	{
		this.m_DustParticle.Play();
		this.m_state = AnimatedLowPolyPack.State.FLOWN_IN;
		iTween.PunchPosition(base.gameObject, this.PUNCH_POSITION_AMOUNT, this.PUNCH_POSITION_TIME);
		if (!string.IsNullOrEmpty(this.m_FlyInSound))
		{
			SoundManager.Get().LoadAndPlay(this.m_FlyInSound);
		}
		if (this.m_FirstPurchaseBox != null)
		{
			this.m_FirstPurchaseBox.RevealContents();
		}
	}

	// Token: 0x06006296 RID: 25238 RVA: 0x00202A59 File Offset: 0x00200C59
	private void PositionOffScreen()
	{
		iTween.Stop(base.gameObject);
		base.transform.localPosition = this.m_targetOffScreenLocalPos;
		this.OnHidden();
	}

	// Token: 0x040051E0 RID: 20960
	public Vector3 PUNCH_POSITION_AMOUNT = new Vector3(0f, 5f, 0f);

	// Token: 0x040051E1 RID: 20961
	public float PUNCH_POSITION_TIME = 0.25f;

	// Token: 0x040051E2 RID: 20962
	public ParticleSystem m_DustParticle;

	// Token: 0x040051E3 RID: 20963
	public FirstPurchaseBox m_FirstPurchaseBox;

	// Token: 0x040051E4 RID: 20964
	public GameObject m_AmountBanner;

	// Token: 0x040051E5 RID: 20965
	public UberText m_AmountBannerText;

	// Token: 0x040051E6 RID: 20966
	public GameObject m_amountFlash;

	// Token: 0x040051E7 RID: 20967
	public Animator m_amountFlashAnimController;

	// Token: 0x040051E8 RID: 20968
	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_FlyOutSound = "purchase_pack_lift_whoosh_1.prefab:5e1611f00212a1f43beb26b37be32eee";

	// Token: 0x040051E9 RID: 20969
	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_FlyInSound = "purchase_pack_drop_impact_1.prefab:45f550c45ed7b5645a2d7f493df17489";

	// Token: 0x040051EA RID: 20970
	public bool m_isLeavingSoonBanner;

	// Token: 0x040051EB RID: 20971
	public GameObject m_shadow;

	// Token: 0x040051EC RID: 20972
	private Vector3 m_flyInLocalAngles = Vector3.zero;

	// Token: 0x040051ED RID: 20973
	private Vector3 m_flyOutLocalAngles = Vector3.zero;

	// Token: 0x040051EE RID: 20974
	private Vector3 m_targetOffScreenLocalPos = Vector3.zero;

	// Token: 0x040051EF RID: 20975
	private Vector3 m_targetLocalPos = Vector3.zero;

	// Token: 0x040051F0 RID: 20976
	private AnimatedLowPolyPack.State m_state;

	// Token: 0x040051F1 RID: 20977
	private int m_lastVisibleBannerCount;

	// Token: 0x040051F2 RID: 20978
	private bool m_amountBannerFlashing;

	// Token: 0x040051F3 RID: 20979
	private bool m_changeActivation = true;

	// Token: 0x0200224E RID: 8782
	public enum State
	{
		// Token: 0x0400E321 RID: 58145
		UNKNOWN,
		// Token: 0x0400E322 RID: 58146
		FLOWN_IN,
		// Token: 0x0400E323 RID: 58147
		FLYING_IN,
		// Token: 0x0400E324 RID: 58148
		FLYING_OUT,
		// Token: 0x0400E325 RID: 58149
		HIDDEN
	}
}
