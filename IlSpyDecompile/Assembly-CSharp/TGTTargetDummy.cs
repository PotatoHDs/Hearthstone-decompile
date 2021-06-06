using System.Collections;
using UnityEngine;

[CustomEditClass]
public class TGTTargetDummy : MonoBehaviour
{
	private const int SPIN_PERCENT = 5;

	public GameObject m_Body;

	public GameObject m_Shield;

	public GameObject m_Sword;

	public GameObject m_BodyRotX;

	public GameObject m_BodyRotY;

	public GameObject m_BodyMesh;

	public float m_BodyHitIntensity = 25f;

	public float m_ShieldHitIntensity = 25f;

	public float m_SwordHitIntensity = -25f;

	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_HitBodySoundPrefab;

	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_HitShieldSoundPrefab;

	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_HitSwordSoundPrefab;

	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_HitSpinSoundPrefab;

	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_SqueakSoundPrefab;

	private static TGTTargetDummy s_instance;

	private float m_squeakSoundVelocity;

	private float m_lastSqueakSoundVol;

	private Quaternion m_lastFrameSqueakAngle;

	private AudioSource m_squeakSound;

	private void Awake()
	{
		s_instance = this;
	}

	private void Start()
	{
		StartCoroutine(RegisterBoardEventLargeShake());
		m_BodyRotX.GetComponent<Rigidbody>().maxAngularVelocity = m_BodyHitIntensity;
		m_BodyRotY.GetComponent<Rigidbody>().maxAngularVelocity = Mathf.Max(m_SwordHitIntensity, m_ShieldHitIntensity);
	}

	private void Update()
	{
		HandleHits();
	}

	public static TGTTargetDummy Get()
	{
		return s_instance;
	}

	public void ArrowHit()
	{
		m_BodyRotX.GetComponent<Rigidbody>().angularVelocity = new Vector3(Random.Range(m_BodyHitIntensity * 0.25f, m_BodyHitIntensity * 0.5f), 0f, 0f);
	}

	public void BodyHit()
	{
		PlaySqueakSound();
		if (!string.IsNullOrEmpty(m_HitBodySoundPrefab))
		{
			string hitBodySoundPrefab = m_HitBodySoundPrefab;
			if (!string.IsNullOrEmpty(hitBodySoundPrefab))
			{
				SoundManager.Get().LoadAndPlay(hitBodySoundPrefab, m_Body);
			}
		}
		m_BodyRotX.GetComponent<Rigidbody>().angularVelocity = new Vector3(Random.Range(m_BodyHitIntensity * 0.75f, m_BodyHitIntensity), 0f, 0f);
		m_BodyRotY.GetComponent<Rigidbody>().angularVelocity = new Vector3(0f, Random.Range(-5f, 5f), 0f);
	}

	public void ShieldHit()
	{
		PlaySqueakSound();
		if (Random.Range(0, 100) < 5)
		{
			Spin(reverse: false);
			return;
		}
		if (!string.IsNullOrEmpty(m_HitShieldSoundPrefab))
		{
			string hitShieldSoundPrefab = m_HitShieldSoundPrefab;
			if (!string.IsNullOrEmpty(hitShieldSoundPrefab))
			{
				SoundManager.Get().LoadAndPlay(hitShieldSoundPrefab, m_Body);
			}
		}
		m_BodyRotY.GetComponent<Rigidbody>().angularVelocity = new Vector3(0f, Random.Range(m_ShieldHitIntensity * 0.7f, m_ShieldHitIntensity), 0f);
		m_BodyRotX.GetComponent<Rigidbody>().angularVelocity = new Vector3(Random.Range(-5f, -10f), 0f, 0f);
	}

	public void SwordHit()
	{
		PlaySqueakSound();
		if (Random.Range(0, 100) < 5)
		{
			Spin(reverse: true);
			return;
		}
		if (!string.IsNullOrEmpty(m_HitSwordSoundPrefab))
		{
			string hitSwordSoundPrefab = m_HitSwordSoundPrefab;
			if (!string.IsNullOrEmpty(hitSwordSoundPrefab))
			{
				SoundManager.Get().LoadAndPlay(hitSwordSoundPrefab, m_Body);
			}
		}
		m_BodyRotY.GetComponent<Rigidbody>().angularVelocity = new Vector3(0f, Random.Range(m_SwordHitIntensity * 0.7f, m_SwordHitIntensity), 0f);
		m_BodyRotX.GetComponent<Rigidbody>().angularVelocity = new Vector3(Random.Range(-5f, -10f), 0f, 0f);
	}

	private IEnumerator RegisterBoardEventLargeShake()
	{
		while (BoardEvents.Get() == null)
		{
			yield return null;
		}
		yield return new WaitForSeconds(2f);
		BoardEvents.Get().RegisterLargeShakeEvent(BodyHit);
	}

	private void HandleHits()
	{
		if (UniversalInputManager.Get().GetMouseButtonDown(0) && IsOver(m_Body))
		{
			BodyHit();
		}
		if (UniversalInputManager.Get().GetMouseButtonDown(0) && IsOver(m_Shield))
		{
			ShieldHit();
		}
		if (UniversalInputManager.Get().GetMouseButtonDown(0) && IsOver(m_Sword))
		{
			SwordHit();
		}
	}

	private void Spin(bool reverse)
	{
		float num = 1080f;
		if (reverse)
		{
			num = -1080f;
		}
		if (!string.IsNullOrEmpty(m_HitSpinSoundPrefab))
		{
			string hitSpinSoundPrefab = m_HitSpinSoundPrefab;
			if (!string.IsNullOrEmpty(hitSpinSoundPrefab))
			{
				SoundManager.Get().LoadAndPlay(hitSpinSoundPrefab, m_Body);
			}
		}
		m_BodyMesh.transform.localEulerAngles = Vector3.zero;
		Vector3 vector = new Vector3(0f, m_BodyMesh.transform.localEulerAngles.y + num, 0f);
		Hashtable args = iTween.Hash("rotation", vector, "isLocal", true, "time", 3f, "easetype", iTween.EaseType.easeOutElastic);
		iTween.RotateTo(m_BodyMesh, args);
	}

	private void PlaySqueakSound()
	{
		StopCoroutine("SqueakSound");
		m_lastSqueakSoundVol = 0f;
		StartCoroutine("SqueakSound");
	}

	private IEnumerator SqueakSound()
	{
		if (string.IsNullOrEmpty(m_SqueakSoundPrefab))
		{
			yield break;
		}
		if (m_squeakSound != null && m_squeakSound.isPlaying)
		{
			SoundManager.Get().Stop(m_squeakSound);
		}
		if (m_squeakSound == null)
		{
			GameObject gameObject = SoundLoader.LoadSound(m_SqueakSoundPrefab);
			if (gameObject != null)
			{
				gameObject.transform.position = m_Body.transform.position;
				m_squeakSound = gameObject.GetComponent<AudioSource>();
			}
		}
		if (!(m_squeakSound == null))
		{
			SoundManager.Get().PlayPreloaded(m_squeakSound, 0f);
			while (m_squeakSound != null && m_squeakSound.isPlaying)
			{
				float current = Mathf.Clamp01(Quaternion.Angle(m_Body.transform.rotation, m_lastFrameSqueakAngle) * 0.1f);
				m_lastFrameSqueakAngle = m_Body.transform.rotation;
				current = (m_lastSqueakSoundVol = Mathf.SmoothDamp(current, m_lastSqueakSoundVol, ref m_squeakSoundVelocity, 0.5f));
				SoundManager.Get().SetVolume(m_squeakSound, Mathf.Clamp01(current));
				yield return null;
			}
		}
	}

	private bool IsOver(GameObject go)
	{
		if (!go)
		{
			return false;
		}
		if (!InputUtil.IsPlayMakerMouseInputAllowed(go))
		{
			return false;
		}
		if (!UniversalInputManager.Get().InputIsOver(go))
		{
			return false;
		}
		return true;
	}
}
