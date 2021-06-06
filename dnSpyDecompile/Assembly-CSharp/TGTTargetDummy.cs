using System;
using System.Collections;
using UnityEngine;

// Token: 0x020000B3 RID: 179
[CustomEditClass]
public class TGTTargetDummy : MonoBehaviour
{
	// Token: 0x06000B37 RID: 2871 RVA: 0x0004220F File Offset: 0x0004040F
	private void Awake()
	{
		TGTTargetDummy.s_instance = this;
	}

	// Token: 0x06000B38 RID: 2872 RVA: 0x00042218 File Offset: 0x00040418
	private void Start()
	{
		base.StartCoroutine(this.RegisterBoardEventLargeShake());
		this.m_BodyRotX.GetComponent<Rigidbody>().maxAngularVelocity = this.m_BodyHitIntensity;
		this.m_BodyRotY.GetComponent<Rigidbody>().maxAngularVelocity = Mathf.Max(this.m_SwordHitIntensity, this.m_ShieldHitIntensity);
	}

	// Token: 0x06000B39 RID: 2873 RVA: 0x00042269 File Offset: 0x00040469
	private void Update()
	{
		this.HandleHits();
	}

	// Token: 0x06000B3A RID: 2874 RVA: 0x00042271 File Offset: 0x00040471
	public static TGTTargetDummy Get()
	{
		return TGTTargetDummy.s_instance;
	}

	// Token: 0x06000B3B RID: 2875 RVA: 0x00042278 File Offset: 0x00040478
	public void ArrowHit()
	{
		this.m_BodyRotX.GetComponent<Rigidbody>().angularVelocity = new Vector3(UnityEngine.Random.Range(this.m_BodyHitIntensity * 0.25f, this.m_BodyHitIntensity * 0.5f), 0f, 0f);
	}

	// Token: 0x06000B3C RID: 2876 RVA: 0x000422B8 File Offset: 0x000404B8
	public void BodyHit()
	{
		this.PlaySqueakSound();
		if (!string.IsNullOrEmpty(this.m_HitBodySoundPrefab))
		{
			string hitBodySoundPrefab = this.m_HitBodySoundPrefab;
			if (!string.IsNullOrEmpty(hitBodySoundPrefab))
			{
				SoundManager.Get().LoadAndPlay(hitBodySoundPrefab, this.m_Body);
			}
		}
		this.m_BodyRotX.GetComponent<Rigidbody>().angularVelocity = new Vector3(UnityEngine.Random.Range(this.m_BodyHitIntensity * 0.75f, this.m_BodyHitIntensity), 0f, 0f);
		this.m_BodyRotY.GetComponent<Rigidbody>().angularVelocity = new Vector3(0f, UnityEngine.Random.Range(-5f, 5f), 0f);
	}

	// Token: 0x06000B3D RID: 2877 RVA: 0x00042364 File Offset: 0x00040564
	public void ShieldHit()
	{
		this.PlaySqueakSound();
		if (UnityEngine.Random.Range(0, 100) < 5)
		{
			this.Spin(false);
			return;
		}
		if (!string.IsNullOrEmpty(this.m_HitShieldSoundPrefab))
		{
			string hitShieldSoundPrefab = this.m_HitShieldSoundPrefab;
			if (!string.IsNullOrEmpty(hitShieldSoundPrefab))
			{
				SoundManager.Get().LoadAndPlay(hitShieldSoundPrefab, this.m_Body);
			}
		}
		this.m_BodyRotY.GetComponent<Rigidbody>().angularVelocity = new Vector3(0f, UnityEngine.Random.Range(this.m_ShieldHitIntensity * 0.7f, this.m_ShieldHitIntensity), 0f);
		this.m_BodyRotX.GetComponent<Rigidbody>().angularVelocity = new Vector3(UnityEngine.Random.Range(-5f, -10f), 0f, 0f);
	}

	// Token: 0x06000B3E RID: 2878 RVA: 0x00042420 File Offset: 0x00040620
	public void SwordHit()
	{
		this.PlaySqueakSound();
		if (UnityEngine.Random.Range(0, 100) < 5)
		{
			this.Spin(true);
			return;
		}
		if (!string.IsNullOrEmpty(this.m_HitSwordSoundPrefab))
		{
			string hitSwordSoundPrefab = this.m_HitSwordSoundPrefab;
			if (!string.IsNullOrEmpty(hitSwordSoundPrefab))
			{
				SoundManager.Get().LoadAndPlay(hitSwordSoundPrefab, this.m_Body);
			}
		}
		this.m_BodyRotY.GetComponent<Rigidbody>().angularVelocity = new Vector3(0f, UnityEngine.Random.Range(this.m_SwordHitIntensity * 0.7f, this.m_SwordHitIntensity), 0f);
		this.m_BodyRotX.GetComponent<Rigidbody>().angularVelocity = new Vector3(UnityEngine.Random.Range(-5f, -10f), 0f, 0f);
	}

	// Token: 0x06000B3F RID: 2879 RVA: 0x000424DC File Offset: 0x000406DC
	private IEnumerator RegisterBoardEventLargeShake()
	{
		while (BoardEvents.Get() == null)
		{
			yield return null;
		}
		yield return new WaitForSeconds(2f);
		BoardEvents.Get().RegisterLargeShakeEvent(new BoardEvents.LargeShakeEventDelegate(this.BodyHit));
		yield break;
	}

	// Token: 0x06000B40 RID: 2880 RVA: 0x000424EC File Offset: 0x000406EC
	private void HandleHits()
	{
		if (UniversalInputManager.Get().GetMouseButtonDown(0) && this.IsOver(this.m_Body))
		{
			this.BodyHit();
		}
		if (UniversalInputManager.Get().GetMouseButtonDown(0) && this.IsOver(this.m_Shield))
		{
			this.ShieldHit();
		}
		if (UniversalInputManager.Get().GetMouseButtonDown(0) && this.IsOver(this.m_Sword))
		{
			this.SwordHit();
		}
	}

	// Token: 0x06000B41 RID: 2881 RVA: 0x0004255C File Offset: 0x0004075C
	private void Spin(bool reverse)
	{
		float num = 1080f;
		if (reverse)
		{
			num = -1080f;
		}
		if (!string.IsNullOrEmpty(this.m_HitSpinSoundPrefab))
		{
			string hitSpinSoundPrefab = this.m_HitSpinSoundPrefab;
			if (!string.IsNullOrEmpty(hitSpinSoundPrefab))
			{
				SoundManager.Get().LoadAndPlay(hitSpinSoundPrefab, this.m_Body);
			}
		}
		this.m_BodyMesh.transform.localEulerAngles = Vector3.zero;
		Vector3 vector = new Vector3(0f, this.m_BodyMesh.transform.localEulerAngles.y + num, 0f);
		Hashtable args = iTween.Hash(new object[]
		{
			"rotation",
			vector,
			"isLocal",
			true,
			"time",
			3f,
			"easetype",
			iTween.EaseType.easeOutElastic
		});
		iTween.RotateTo(this.m_BodyMesh, args);
	}

	// Token: 0x06000B42 RID: 2882 RVA: 0x00042648 File Offset: 0x00040848
	private void PlaySqueakSound()
	{
		base.StopCoroutine("SqueakSound");
		this.m_lastSqueakSoundVol = 0f;
		base.StartCoroutine("SqueakSound");
	}

	// Token: 0x06000B43 RID: 2883 RVA: 0x0004266C File Offset: 0x0004086C
	private IEnumerator SqueakSound()
	{
		if (string.IsNullOrEmpty(this.m_SqueakSoundPrefab))
		{
			yield break;
		}
		if (this.m_squeakSound != null && this.m_squeakSound.isPlaying)
		{
			SoundManager.Get().Stop(this.m_squeakSound);
		}
		if (this.m_squeakSound == null)
		{
			GameObject gameObject = SoundLoader.LoadSound(this.m_SqueakSoundPrefab);
			if (gameObject != null)
			{
				gameObject.transform.position = this.m_Body.transform.position;
				this.m_squeakSound = gameObject.GetComponent<AudioSource>();
			}
		}
		if (this.m_squeakSound == null)
		{
			yield break;
		}
		SoundManager.Get().PlayPreloaded(this.m_squeakSound, 0f);
		while (this.m_squeakSound != null && this.m_squeakSound.isPlaying)
		{
			float num = Mathf.Clamp01(Quaternion.Angle(this.m_Body.transform.rotation, this.m_lastFrameSqueakAngle) * 0.1f);
			this.m_lastFrameSqueakAngle = this.m_Body.transform.rotation;
			num = Mathf.SmoothDamp(num, this.m_lastSqueakSoundVol, ref this.m_squeakSoundVelocity, 0.5f);
			this.m_lastSqueakSoundVol = num;
			SoundManager.Get().SetVolume(this.m_squeakSound, Mathf.Clamp01(num));
			yield return null;
		}
		yield break;
	}

	// Token: 0x06000B44 RID: 2884 RVA: 0x000402EE File Offset: 0x0003E4EE
	private bool IsOver(GameObject go)
	{
		return go && InputUtil.IsPlayMakerMouseInputAllowed(go) && UniversalInputManager.Get().InputIsOver(go);
	}

	// Token: 0x04000775 RID: 1909
	private const int SPIN_PERCENT = 5;

	// Token: 0x04000776 RID: 1910
	public GameObject m_Body;

	// Token: 0x04000777 RID: 1911
	public GameObject m_Shield;

	// Token: 0x04000778 RID: 1912
	public GameObject m_Sword;

	// Token: 0x04000779 RID: 1913
	public GameObject m_BodyRotX;

	// Token: 0x0400077A RID: 1914
	public GameObject m_BodyRotY;

	// Token: 0x0400077B RID: 1915
	public GameObject m_BodyMesh;

	// Token: 0x0400077C RID: 1916
	public float m_BodyHitIntensity = 25f;

	// Token: 0x0400077D RID: 1917
	public float m_ShieldHitIntensity = 25f;

	// Token: 0x0400077E RID: 1918
	public float m_SwordHitIntensity = -25f;

	// Token: 0x0400077F RID: 1919
	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_HitBodySoundPrefab;

	// Token: 0x04000780 RID: 1920
	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_HitShieldSoundPrefab;

	// Token: 0x04000781 RID: 1921
	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_HitSwordSoundPrefab;

	// Token: 0x04000782 RID: 1922
	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_HitSpinSoundPrefab;

	// Token: 0x04000783 RID: 1923
	[CustomEditField(T = EditType.SOUND_PREFAB)]
	public string m_SqueakSoundPrefab;

	// Token: 0x04000784 RID: 1924
	private static TGTTargetDummy s_instance;

	// Token: 0x04000785 RID: 1925
	private float m_squeakSoundVelocity;

	// Token: 0x04000786 RID: 1926
	private float m_lastSqueakSoundVol;

	// Token: 0x04000787 RID: 1927
	private Quaternion m_lastFrameSqueakAngle;

	// Token: 0x04000788 RID: 1928
	private AudioSource m_squeakSound;
}
