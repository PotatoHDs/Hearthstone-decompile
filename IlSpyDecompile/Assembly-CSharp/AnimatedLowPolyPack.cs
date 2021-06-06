using System.Collections;
using UnityEngine;

[CustomEditClass]
public class AnimatedLowPolyPack : MonoBehaviour
{
	public enum State
	{
		UNKNOWN,
		FLOWN_IN,
		FLYING_IN,
		FLYING_OUT,
		HIDDEN
	}

	public Vector3 PUNCH_POSITION_AMOUNT = new Vector3(0f, 5f, 0f);

	public float PUNCH_POSITION_TIME = 0.25f;

	public ParticleSystem m_DustParticle;

	public FirstPurchaseBox m_FirstPurchaseBox;

	public GameObject m_AmountBanner;

	public UberText m_AmountBannerText;

	public GameObject m_amountFlash;

	public Animator m_amountFlashAnimController;

	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_FlyOutSound = "purchase_pack_lift_whoosh_1.prefab:5e1611f00212a1f43beb26b37be32eee";

	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_FlyInSound = "purchase_pack_drop_impact_1.prefab:45f550c45ed7b5645a2d7f493df17489";

	public bool m_isLeavingSoonBanner;

	public GameObject m_shadow;

	private Vector3 m_flyInLocalAngles = Vector3.zero;

	private Vector3 m_flyOutLocalAngles = Vector3.zero;

	private Vector3 m_targetOffScreenLocalPos = Vector3.zero;

	private Vector3 m_targetLocalPos = Vector3.zero;

	private State m_state;

	private int m_lastVisibleBannerCount;

	private bool m_amountBannerFlashing;

	private bool m_changeActivation = true;

	public int Column { get; private set; }

	public bool IsShowingShadow
	{
		get
		{
			if (m_shadow != null)
			{
				return m_shadow.activeSelf;
			}
			return false;
		}
		set
		{
			if (m_shadow != null && value != m_shadow.activeSelf)
			{
				m_shadow.SetActive(value);
			}
		}
	}

	public void Init(int column, Vector3 targetLocalPos, Vector3 offScreenOffset, bool ignoreFullscreenEffects = true, bool changeActivation = true)
	{
		m_targetLocalPos = targetLocalPos;
		m_targetOffScreenLocalPos = targetLocalPos + offScreenOffset;
		m_changeActivation = changeActivation;
		Column = column;
		if (ignoreFullscreenEffects)
		{
			SceneUtils.SetLayer(base.gameObject, GameLayer.IgnoreFullScreenEffects);
		}
		PositionOffScreen();
	}

	public void FlyInImmediate()
	{
		iTween.Stop(base.gameObject);
		base.transform.localEulerAngles = m_flyInLocalAngles;
		base.transform.localPosition = m_targetLocalPos;
		m_state = State.FLOWN_IN;
		if (m_changeActivation)
		{
			base.gameObject.SetActive(value: true);
		}
		if (m_FirstPurchaseBox != null)
		{
			m_FirstPurchaseBox.RevealContents();
		}
	}

	public bool FlyIn(float animTime, float delay)
	{
		if (m_state == State.FLOWN_IN)
		{
			return false;
		}
		if (m_state == State.FLYING_IN)
		{
			return false;
		}
		m_state = State.FLYING_IN;
		if (m_changeActivation)
		{
			base.gameObject.SetActive(value: true);
		}
		base.transform.localEulerAngles = m_flyInLocalAngles;
		if (m_FirstPurchaseBox != null)
		{
			m_FirstPurchaseBox.Reset();
		}
		Hashtable args = iTween.Hash("position", m_targetLocalPos, "isLocal", true, "time", animTime, "delay", delay, "easetype", iTween.EaseType.easeInCubic, "oncomplete", "OnFlownIn", "oncompletetarget", base.gameObject);
		iTween.Stop(base.gameObject);
		iTween.MoveTo(base.gameObject, args);
		return true;
	}

	public void FlyOutImmediate()
	{
		iTween.Stop(base.gameObject);
		base.transform.localEulerAngles = m_flyOutLocalAngles;
		base.transform.localPosition = m_targetOffScreenLocalPos;
		OnHidden();
	}

	public bool FlyOut(float animTime, float delay)
	{
		if (m_state == State.HIDDEN)
		{
			return false;
		}
		if (m_state == State.FLYING_OUT)
		{
			return false;
		}
		m_state = State.FLYING_OUT;
		base.transform.localEulerAngles = m_flyOutLocalAngles;
		Hashtable args = iTween.Hash("position", m_targetOffScreenLocalPos, "isLocal", true, "time", animTime, "delay", delay, "easetype", iTween.EaseType.linear, "oncomplete", "OnHidden", "oncompletetarget", base.gameObject);
		iTween.Stop(base.gameObject);
		iTween.MoveTo(base.gameObject, args);
		if (!string.IsNullOrEmpty(m_FlyOutSound))
		{
			SoundManager.Get().LoadAndPlay(m_FlyOutSound);
		}
		return true;
	}

	public void SetFlyingLocalRotations(Vector3 flyInLocalAngles, Vector3 flyOutLocalAngles)
	{
		m_flyInLocalAngles = flyInLocalAngles;
		m_flyOutLocalAngles = flyOutLocalAngles;
	}

	public State GetState()
	{
		return m_state;
	}

	public void Hide()
	{
		OnHidden();
	}

	public FirstPurchaseBox GetFirstPurchaseBox()
	{
		return m_FirstPurchaseBox;
	}

	public void UpdateBannerCount(int count)
	{
		StartCoroutine(UpdateBannerCountCoroutine(count));
	}

	public void UpdateBannerCountImmediately(int count)
	{
		if (m_AmountBanner != null && m_AmountBannerText != null)
		{
			m_AmountBanner.SetActive(value: true);
			m_lastVisibleBannerCount = count;
			m_AmountBannerText.Text = m_lastVisibleBannerCount.ToString();
		}
	}

	private IEnumerator UpdateBannerCountCoroutine(int count)
	{
		if (!(m_AmountBanner != null) || !(m_AmountBannerText != null))
		{
			yield break;
		}
		m_AmountBanner.SetActive(value: true);
		if (m_lastVisibleBannerCount == count)
		{
			yield break;
		}
		if (m_amountBannerFlashing)
		{
			m_AmountBannerText.Text = m_lastVisibleBannerCount.ToString();
		}
		m_lastVisibleBannerCount = count;
		if (m_amountFlash != null && m_amountFlashAnimController != null)
		{
			m_amountFlash.SetActive(value: false);
			m_amountFlash.SetActive(value: true);
			m_amountFlashAnimController.enabled = true;
			m_amountFlashAnimController.StopPlayback();
			yield return new WaitForEndOfFrame();
			if (m_amountFlashAnimController == null)
			{
				yield break;
			}
			m_amountFlashAnimController.Play("Flash");
			m_amountBannerFlashing = true;
			while (m_amountFlashAnimController != null && m_amountFlashAnimController.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.5f)
			{
				yield return null;
			}
		}
		m_AmountBannerText.Text = count.ToString();
		m_amountBannerFlashing = false;
	}

	public void HideBanner()
	{
		if (m_AmountBanner != null)
		{
			m_AmountBanner.SetActive(value: false);
		}
	}

	private void OnHidden()
	{
		m_state = State.HIDDEN;
		StopCoroutine("UpdateBannerCount");
		if (m_changeActivation)
		{
			base.gameObject.SetActive(value: false);
		}
	}

	private void OnFlownIn()
	{
		m_DustParticle.Play();
		m_state = State.FLOWN_IN;
		iTween.PunchPosition(base.gameObject, PUNCH_POSITION_AMOUNT, PUNCH_POSITION_TIME);
		if (!string.IsNullOrEmpty(m_FlyInSound))
		{
			SoundManager.Get().LoadAndPlay(m_FlyInSound);
		}
		if (m_FirstPurchaseBox != null)
		{
			m_FirstPurchaseBox.RevealContents();
		}
	}

	private void PositionOffScreen()
	{
		iTween.Stop(base.gameObject);
		base.transform.localPosition = m_targetOffScreenLocalPos;
		OnHidden();
	}
}
