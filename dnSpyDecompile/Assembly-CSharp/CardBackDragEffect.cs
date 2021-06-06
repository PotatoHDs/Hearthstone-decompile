using System;
using UnityEngine;

// Token: 0x020000D9 RID: 217
public class CardBackDragEffect : MonoBehaviour
{
	// Token: 0x06000CA3 RID: 3235 RVA: 0x00003BE8 File Offset: 0x00001DE8
	private void Awake()
	{
	}

	// Token: 0x06000CA4 RID: 3236 RVA: 0x000498A8 File Offset: 0x00047AA8
	private void Start()
	{
		SceneMgr sceneMgr;
		if (!HearthstoneServices.TryGet<SceneMgr>(out sceneMgr) || sceneMgr.GetMode() != SceneMgr.Mode.GAMEPLAY)
		{
			base.enabled = false;
			return;
		}
		this.m_LastPosition = base.transform.position;
		if (this.m_CardBackManager == null)
		{
			this.m_CardBackManager = CardBackManager.Get();
			if (this.m_CardBackManager == null)
			{
				Debug.LogError("Failed to get CardBackManager!");
				base.enabled = false;
			}
		}
		if (this.m_CardBackManager != null)
		{
			this.m_CardBackManager.RegisterUpdateCardbacksListener(new CardBackManager.UpdateCardbacksCallback(this.SetEffect));
		}
		this.SetEffect();
	}

	// Token: 0x06000CA5 RID: 3237 RVA: 0x00003BE8 File Offset: 0x00001DE8
	private void FixedUpdate()
	{
	}

	// Token: 0x06000CA6 RID: 3238 RVA: 0x00049934 File Offset: 0x00047B34
	private void Update()
	{
		if (this.m_EffectsRoot != null)
		{
			if (!base.GetComponent<Renderer>().enabled)
			{
				this.ShowParticles(false);
				if (this.m_EffectsRoot.activeSelf)
				{
					this.m_EffectsRoot.SetActive(false);
					return;
				}
			}
			else
			{
				this.m_Speed = (base.transform.position - this.m_LastPosition).magnitude / Time.deltaTime * 3.6f;
				this.UpdateDragEffect();
				this.m_LastPosition = base.transform.position;
			}
		}
	}

	// Token: 0x06000CA7 RID: 3239 RVA: 0x000499C4 File Offset: 0x00047BC4
	private void OnDisable()
	{
		if (this.m_EffectsRoot != null)
		{
			this.ShowParticles(false);
		}
	}

	// Token: 0x06000CA8 RID: 3240 RVA: 0x000499DB File Offset: 0x00047BDB
	private void OnDestroy()
	{
		if (CardBackManager.Get() != null)
		{
			CardBackManager.Get().UnregisterUpdateCardbacksListener(new CardBackManager.UpdateCardbacksCallback(this.SetEffect));
		}
	}

	// Token: 0x06000CA9 RID: 3241 RVA: 0x00003BE8 File Offset: 0x00001DE8
	private void OnEnable()
	{
	}

	// Token: 0x06000CAA RID: 3242 RVA: 0x000499FC File Offset: 0x00047BFC
	public void SetEffect()
	{
		if (this.m_CardBackManager == null)
		{
			this.m_CardBackManager = CardBackManager.Get();
			if (this.m_CardBackManager == null)
			{
				Debug.LogError("Failed to get CardBackManager!");
				base.enabled = false;
				return;
			}
		}
		CardBackManager.CardBackSlot slot = this.m_CardBackManager.IsActorFriendly(this.m_Actor) ? CardBackManager.CardBackSlot.FRIENDLY : CardBackManager.CardBackSlot.OPPONENT;
		this.m_CardBackManager.UpdateDragEffect(base.gameObject, slot);
		CardBack cardBackForActor = this.m_CardBackManager.GetCardBackForActor(this.m_Actor);
		if (cardBackForActor != null)
		{
			this.m_Min = cardBackForActor.m_EffectMinVelocity;
			this.m_Max = cardBackForActor.m_EffectMaxVelocity;
		}
	}

	// Token: 0x06000CAB RID: 3243 RVA: 0x00049A94 File Offset: 0x00047C94
	private void UpdateDragEffect()
	{
		if (this.m_Speed > this.m_Min && this.m_Speed < this.m_Max)
		{
			if (this.m_Active)
			{
				return;
			}
			this.m_Active = true;
			this.ShowParticles(true);
			return;
		}
		else
		{
			if (!this.m_Active)
			{
				return;
			}
			this.m_Active = false;
			this.ShowParticles(false);
			return;
		}
	}

	// Token: 0x06000CAC RID: 3244 RVA: 0x00049AEC File Offset: 0x00047CEC
	private void ShowParticles(bool show)
	{
		if (show)
		{
			foreach (ParticleSystem particleSystem in base.GetComponentsInChildren<ParticleSystem>())
			{
				if (!(particleSystem == null))
				{
					particleSystem.Play();
				}
			}
			return;
		}
		foreach (ParticleSystem particleSystem2 in base.GetComponentsInChildren<ParticleSystem>())
		{
			if (particleSystem2 == null)
			{
				return;
			}
			particleSystem2.Stop();
		}
	}

	// Token: 0x040008D2 RID: 2258
	private const float MIN_VELOCITY = 2f;

	// Token: 0x040008D3 RID: 2259
	private const float MAX_VELOCITY = 30f;

	// Token: 0x040008D4 RID: 2260
	public Actor m_Actor;

	// Token: 0x040008D5 RID: 2261
	public GameObject m_EffectsRoot;

	// Token: 0x040008D6 RID: 2262
	private CardBackManager m_CardBackManager;

	// Token: 0x040008D7 RID: 2263
	private Vector3 m_LastPosition;

	// Token: 0x040008D8 RID: 2264
	private float m_Speed;

	// Token: 0x040008D9 RID: 2265
	private bool m_Active;

	// Token: 0x040008DA RID: 2266
	private float m_Min = 2f;

	// Token: 0x040008DB RID: 2267
	private float m_Max = 30f;
}
