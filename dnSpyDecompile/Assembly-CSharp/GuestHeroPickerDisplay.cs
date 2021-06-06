using System;
using System.Collections;
using Hearthstone.UI;
using UnityEngine;

// Token: 0x02000067 RID: 103
[CustomEditClass]
public class GuestHeroPickerDisplay : MonoBehaviour
{
	// Token: 0x060005CF RID: 1487 RVA: 0x00021FA8 File Offset: 0x000201A8
	private void Awake()
	{
		if (GuestHeroPickerDisplay.s_instance != null)
		{
			Debug.LogWarning("GuestHeroPickerDisplay is supposed to be a singleton, but a second instance of it is being created!");
		}
		GuestHeroPickerDisplay.s_instance = this;
		this.startPosition = base.transform.localPosition;
		base.transform.localPosition = this.startPosition + this.startOffset;
		this.m_trayControllerReference.RegisterReadyListener<VisualController>(new Action<VisualController>(this.OnTrayControllerReady));
		this.m_trayControllerReference_phone.RegisterReadyListener<VisualController>(new Action<VisualController>(this.OnTrayControllerReady));
		SoundManager.Get().Load(SoundUtils.SquarePanelSlideOnSFX);
		SoundManager.Get().Load(SoundUtils.SquarePanelSlideOffSFX);
	}

	// Token: 0x060005D0 RID: 1488 RVA: 0x00022050 File Offset: 0x00020250
	private void OnTrayControllerReady(VisualController trayController)
	{
		this.m_heroPickerTray = trayController.GetComponentInChildren<GuestHeroPickerTrayDisplay>();
		if (this.m_heroPickerTray == null)
		{
			Debug.LogError("GuestHeroPickerTrayDisplay component not found in GuestHeroPickerTray object.");
			return;
		}
		if (trayController == null)
		{
			Debug.LogError("trayController was null in OnTrayControllerReady!");
			return;
		}
		this.m_heroPickerTray.InitAssets();
	}

	// Token: 0x060005D1 RID: 1489 RVA: 0x000220A4 File Offset: 0x000202A4
	public void ShowTray()
	{
		Hashtable args = iTween.Hash(new object[]
		{
			"position",
			this.startPosition,
			"time",
			1f,
			"isLocal",
			true,
			"oncomplete",
			"OnTrayShown",
			"oncompletetarget",
			base.gameObject,
			"easeType",
			iTween.EaseType.easeOutBounce
		});
		SoundManager.Get().LoadAndPlay(SoundUtils.SquarePanelSlideOnSFX);
		iTween.MoveTo(base.gameObject, args);
	}

	// Token: 0x060005D2 RID: 1490 RVA: 0x0002214B File Offset: 0x0002034B
	public void OnTrayShown()
	{
		this.m_heroPickerTray.EnableBackButton(true);
		if (PvPDungeonRunScene.Get() != null)
		{
			PvPDungeonRunScene.Get().OnHeroPickerShown();
		}
	}

	// Token: 0x060005D3 RID: 1491 RVA: 0x00022170 File Offset: 0x00020370
	public void HideTray(float delay = 0f)
	{
		Hashtable args = iTween.Hash(new object[]
		{
			"position",
			this.startPosition + this.startOffset,
			"time",
			1f,
			"isLocal",
			true,
			"oncomplete",
			"OnTrayHidden",
			"oncompletetarget",
			base.gameObject,
			"easeType",
			iTween.EaseType.easeOutBounce,
			"delay",
			delay
		});
		SoundManager.Get().LoadAndPlay(SoundUtils.SquarePanelSlideOffSFX);
		iTween.MoveTo(base.gameObject, args);
	}

	// Token: 0x060005D4 RID: 1492 RVA: 0x00022238 File Offset: 0x00020438
	private void OnTrayHidden()
	{
		this.m_heroPickerTray.Unload();
		UnityEngine.Object.DestroyImmediate(base.gameObject);
		if (TavernBrawlDisplay.Get() != null)
		{
			TavernBrawlDisplay.Get().OnHeroPickerClosed();
		}
		if (PvPDungeonRunScene.Get() != null)
		{
			PvPDungeonRunScene.Get().OnHeroPickerHidden();
		}
	}

	// Token: 0x060005D5 RID: 1493 RVA: 0x00022289 File Offset: 0x00020489
	public static GuestHeroPickerDisplay Get()
	{
		return GuestHeroPickerDisplay.s_instance;
	}

	// Token: 0x0400041C RID: 1052
	public AsyncReference m_trayControllerReference;

	// Token: 0x0400041D RID: 1053
	public AsyncReference m_trayControllerReference_phone;

	// Token: 0x0400041E RID: 1054
	private static GuestHeroPickerDisplay s_instance;

	// Token: 0x0400041F RID: 1055
	private GuestHeroPickerTrayDisplay m_heroPickerTray;

	// Token: 0x04000420 RID: 1056
	private Vector3 startOffset = new Vector3(-120f, 0f, 0f);

	// Token: 0x04000421 RID: 1057
	private Vector3 startPosition;
}
