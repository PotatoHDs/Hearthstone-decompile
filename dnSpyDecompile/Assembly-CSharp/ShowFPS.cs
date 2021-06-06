using System;
using Hearthstone;
using UnityEngine;

// Token: 0x020009F3 RID: 2547
[ExecuteAlways]
public class ShowFPS : MonoBehaviour
{
	// Token: 0x060089EA RID: 35306 RVA: 0x002C379E File Offset: 0x002C199E
	private void Awake()
	{
		ShowFPS.s_instance = this;
		if (HearthstoneApplication.IsPublic())
		{
			UnityEngine.Object.DestroyImmediate(base.gameObject);
		}
	}

	// Token: 0x060089EB RID: 35307 RVA: 0x002C37B8 File Offset: 0x002C19B8
	private void OnDestroy()
	{
		ShowFPS.s_instance = null;
	}

	// Token: 0x060089EC RID: 35308 RVA: 0x002C37C0 File Offset: 0x002C19C0
	public static ShowFPS Get()
	{
		return ShowFPS.s_instance;
	}

	// Token: 0x060089ED RID: 35309 RVA: 0x002C37C7 File Offset: 0x002C19C7
	[ContextMenu("Start Frame Count")]
	public void StartFrameCount()
	{
		this.m_FrameCountLastTime = Time.realtimeSinceStartup;
		this.m_FrameCountTime = 0f;
		this.m_FrameCount = 0;
		this.m_FrameCountActive = true;
	}

	// Token: 0x060089EE RID: 35310 RVA: 0x002C37ED File Offset: 0x002C19ED
	[ContextMenu("Stop Frame Count")]
	public void StopFrameCount()
	{
		this.m_FrameCountActive = false;
	}

	// Token: 0x060089EF RID: 35311 RVA: 0x002C37F6 File Offset: 0x002C19F6
	[ContextMenu("Clear Frame Count")]
	public void ClearFrameCount()
	{
		this.m_FrameCountLastTime = 0f;
		this.m_FrameCountTime = 0f;
		this.m_FrameCount = 0;
		this.m_FrameCountActive = false;
	}

	// Token: 0x060089F0 RID: 35312 RVA: 0x002C381C File Offset: 0x002C1A1C
	private void Start()
	{
		this.m_LastInterval = (double)Time.realtimeSinceStartup;
		this.frames = 0;
		this.UpdateEnabled();
		Options.Get().RegisterChangedListener(Option.HUD, new Options.ChangedCallback(this.OnHudOptionChanged));
	}

	// Token: 0x060089F1 RID: 35313 RVA: 0x002C384F File Offset: 0x002C1A4F
	private void OnDisable()
	{
		Time.captureFramerate = 0;
	}

	// Token: 0x060089F2 RID: 35314 RVA: 0x002C3858 File Offset: 0x002C1A58
	private void Update()
	{
		this.frames++;
		float realtimeSinceStartup = Time.realtimeSinceStartup;
		if ((double)realtimeSinceStartup > this.m_LastInterval + (double)this.m_UpdateInterval)
		{
			float num = (float)this.frames / (float)((double)realtimeSinceStartup - this.m_LastInterval);
			if (this.m_verbose)
			{
				this.m_fpsText = string.Format("{0:f2} - {1} frames over {2}sec", num, this.frames, this.m_UpdateInterval);
			}
			else
			{
				this.m_fpsText = string.Format("{0:f2}", num);
			}
			this.frames = 0;
			this.m_LastInterval = (double)realtimeSinceStartup;
		}
		if ((this.m_FrameCountActive || this.m_FrameCount > 0) && this.m_FrameCountActive)
		{
			this.m_FrameCountTime += (realtimeSinceStartup - this.m_FrameCountLastTime) / 60f * Time.timeScale;
			if (this.m_FrameCountLastTime == 0f)
			{
				this.m_FrameCountLastTime = realtimeSinceStartup;
			}
			this.m_FrameCount = Mathf.CeilToInt(this.m_FrameCountTime * 60f);
		}
	}

	// Token: 0x060089F3 RID: 35315 RVA: 0x002C3960 File Offset: 0x002C1B60
	private void OnGUI()
	{
		int num = 0;
		Camera[] allCameras = Camera.allCameras;
		for (int i = 0; i < allCameras.Length; i++)
		{
			FullScreenEffects component = allCameras[i].GetComponent<FullScreenEffects>();
			if (!(component == null) && component.IsActive)
			{
				num++;
			}
		}
		string text = this.m_fpsText;
		if (this.m_FrameCountActive || this.m_FrameCount > 0)
		{
			text = string.Format("{0} - Frame Count: {1}", text, this.m_FrameCount);
		}
		if (num > 0)
		{
			text = string.Format("{0} - FSE (x{1})", text, num);
		}
		ScreenEffectsMgr screenEffectsMgr;
		if (HearthstoneServices.TryGet<ScreenEffectsMgr>(out screenEffectsMgr))
		{
			int activeScreenEffectsCount = screenEffectsMgr.GetActiveScreenEffectsCount();
			if (activeScreenEffectsCount > 0 && screenEffectsMgr.IsActive)
			{
				text = string.Format("{0} - ScreenEffects Active: {1}", text, activeScreenEffectsCount);
			}
		}
		GUI.Box(new Rect((float)Screen.width * 0.75f, (float)(Screen.height - 20), (float)Screen.width * 0.25f, 20f), text);
	}

	// Token: 0x060089F4 RID: 35316 RVA: 0x002C3A50 File Offset: 0x002C1C50
	private void OnHudOptionChanged(Option option, object prevValue, bool existed, object userData)
	{
		this.UpdateEnabled();
	}

	// Token: 0x060089F5 RID: 35317 RVA: 0x002C3A58 File Offset: 0x002C1C58
	private void UpdateEnabled()
	{
		base.enabled = Options.Get().GetBool(Option.HUD);
	}

	// Token: 0x0400734A RID: 29514
	private float m_UpdateInterval = 0.5f;

	// Token: 0x0400734B RID: 29515
	private double m_LastInterval;

	// Token: 0x0400734C RID: 29516
	private int frames;

	// Token: 0x0400734D RID: 29517
	private bool m_FrameCountActive;

	// Token: 0x0400734E RID: 29518
	private float m_FrameCountTime;

	// Token: 0x0400734F RID: 29519
	private float m_FrameCountLastTime;

	// Token: 0x04007350 RID: 29520
	private int m_FrameCount;

	// Token: 0x04007351 RID: 29521
	private bool m_verbose;

	// Token: 0x04007352 RID: 29522
	private string m_fpsText;

	// Token: 0x04007353 RID: 29523
	private static ShowFPS s_instance;
}
