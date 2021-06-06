using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000B11 RID: 2833
[CustomEditClass]
public class FlippingPanel : MonoBehaviour
{
	// Token: 0x060096A1 RID: 38561 RVA: 0x0030BF28 File Offset: 0x0030A128
	private void Awake()
	{
		for (int i = 0; i < this.m_panelContent.Count; i++)
		{
			if (i == this.m_currentContentOffset)
			{
				GameObject gameObject = this.m_panelContent[i];
				gameObject.transform.parent = this.m_faceBones[this.m_currentFaceBone];
				GameUtils.ResetTransform(gameObject);
				gameObject.gameObject.SetActive(true);
			}
			else
			{
				this.m_panelContent[i].SetActive(false);
			}
		}
	}

	// Token: 0x060096A2 RID: 38562 RVA: 0x0030BFA1 File Offset: 0x0030A1A1
	private void Start()
	{
		this.m_desiredOrientation = this.m_rotatingObject.transform.localRotation;
	}

	// Token: 0x1700088C RID: 2188
	// (get) Token: 0x060096A3 RID: 38563 RVA: 0x0030BFB9 File Offset: 0x0030A1B9
	public int CurrentContentOffset
	{
		get
		{
			return this.m_currentContentOffset;
		}
	}

	// Token: 0x060096A4 RID: 38564 RVA: 0x0030BFC4 File Offset: 0x0030A1C4
	public bool FlipPanel(bool forward)
	{
		if (iTween.CountByName(this.m_rotatingObject, "PANEL_ROTATION") > 0)
		{
			iTween.StopByName(this.m_rotatingObject, "PANEL_ROTATION");
			this.FinishFlip();
		}
		int num = this.m_currentContentOffset + (forward ? 1 : -1);
		if (num >= this.m_panelContent.Count)
		{
			if (!this.m_allowLoopingToStartOfContent)
			{
				return false;
			}
			num = 0;
		}
		else if (num < 0)
		{
			if (!this.m_allowLoopingToStartOfContent)
			{
				return false;
			}
			num = this.m_panelContent.Count - 1;
		}
		this.m_previousContent = this.m_panelContent[this.m_currentContentOffset];
		this.m_currentContentOffset = num;
		GameObject gameObject = this.m_panelContent[this.m_currentContentOffset];
		int num2 = this.m_currentFaceBone + (forward ? 1 : -1);
		if (num2 >= this.m_faceBones.Count)
		{
			num2 = 0;
		}
		else if (num2 < 0)
		{
			num2 = this.m_faceBones.Count - 1;
		}
		this.m_currentFaceBone = num2;
		if (gameObject != null)
		{
			gameObject.transform.parent = this.m_faceBones[this.m_currentFaceBone];
			GameUtils.ResetTransform(gameObject);
			gameObject.gameObject.SetActive(true);
		}
		if (!string.IsNullOrEmpty(this.m_contentFlipSound))
		{
			SoundManager.Get().LoadAndPlay(this.m_contentFlipSound);
		}
		Quaternion rhs = forward ? Quaternion.AngleAxis(this.m_rotationDegrees, Vector3.right) : Quaternion.AngleAxis(this.m_rotationDegrees, Vector3.left);
		Quaternion desiredOrientation = this.m_rotatingObject.transform.localRotation * rhs;
		this.m_desiredOrientation = desiredOrientation;
		if (this.m_contentFlipAnimationTime > 0f)
		{
			Hashtable args = iTween.Hash(new object[]
			{
				"amount",
				this.m_rotationDegrees * (forward ? Vector3.right : Vector3.left),
				"time",
				this.m_contentFlipAnimationTime,
				"easeType",
				this.m_contentFlipEaseType,
				"isLocal",
				true,
				"name",
				"PANEL_ROTATION",
				"oncomplete",
				new Action<object>(delegate(object o)
				{
					this.FinishFlip();
				})
			});
			iTween.RotateAdd(this.m_rotatingObject, args);
		}
		this.FirePanelContentChangedEvent(this.m_currentContentOffset);
		return true;
	}

	// Token: 0x060096A5 RID: 38565 RVA: 0x0030C215 File Offset: 0x0030A415
	public void AddPanelContentChangedListener(FlippingPanel.PanelContentChanged listener)
	{
		this.m_panelContentChangedListeners.Add(listener);
	}

	// Token: 0x060096A6 RID: 38566 RVA: 0x0030C223 File Offset: 0x0030A423
	public void RemovePanelContentChangedListener(FlippingPanel.PanelContentChanged listener)
	{
		this.m_panelContentChangedListeners.Remove(listener);
	}

	// Token: 0x060096A7 RID: 38567 RVA: 0x0030C234 File Offset: 0x0030A434
	private void FirePanelContentChangedEvent(int newContentOffset)
	{
		FlippingPanel.PanelContentChanged[] array = this.m_panelContentChangedListeners.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			array[i](newContentOffset);
		}
	}

	// Token: 0x060096A8 RID: 38568 RVA: 0x0030C264 File Offset: 0x0030A464
	private void FinishFlip()
	{
		this.m_rotatingObject.transform.localRotation = this.m_desiredOrientation;
		if (this.m_previousContent != null)
		{
			this.m_previousContent.gameObject.SetActive(false);
		}
	}

	// Token: 0x04007E2D RID: 32301
	[CustomEditField(Sections = "Panels")]
	public List<GameObject> m_panelContent = new List<GameObject>();

	// Token: 0x04007E2E RID: 32302
	[CustomEditField(Sections = "Panels")]
	public List<Transform> m_faceBones = new List<Transform>();

	// Token: 0x04007E2F RID: 32303
	[CustomEditField(Sections = "Rotation")]
	public GameObject m_rotatingObject;

	// Token: 0x04007E30 RID: 32304
	[CustomEditField(Sections = "Rotation")]
	public float m_contentFlipAnimationTime = 0.5f;

	// Token: 0x04007E31 RID: 32305
	[CustomEditField(Sections = "Rotation")]
	public iTween.EaseType m_contentFlipEaseType = iTween.EaseType.easeOutBounce;

	// Token: 0x04007E32 RID: 32306
	[CustomEditField(Sections = "Rotation")]
	public float m_rotationDegrees = 120f;

	// Token: 0x04007E33 RID: 32307
	[CustomEditField(Sections = "Rotation")]
	public bool m_allowLoopingToStartOfContent = true;

	// Token: 0x04007E34 RID: 32308
	[CustomEditField(Sections = "Sounds", T = EditType.SOUND_PREFAB)]
	public string m_contentFlipSound;

	// Token: 0x04007E35 RID: 32309
	private List<FlippingPanel.PanelContentChanged> m_panelContentChangedListeners = new List<FlippingPanel.PanelContentChanged>();

	// Token: 0x04007E36 RID: 32310
	private int m_currentContentOffset;

	// Token: 0x04007E37 RID: 32311
	private int m_currentFaceBone;

	// Token: 0x04007E38 RID: 32312
	private GameObject m_previousContent;

	// Token: 0x04007E39 RID: 32313
	private Quaternion m_desiredOrientation = Quaternion.identity;

	// Token: 0x02002760 RID: 10080
	// (Invoke) Token: 0x060139C2 RID: 80322
	public delegate void PanelContentChanged(int newContentOffset);
}
