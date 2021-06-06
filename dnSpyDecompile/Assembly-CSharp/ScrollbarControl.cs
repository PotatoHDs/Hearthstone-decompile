using System;
using UnityEngine;

// Token: 0x02000611 RID: 1553
public class ScrollbarControl : MonoBehaviour
{
	// Token: 0x060056DD RID: 22237 RVA: 0x001C74E7 File Offset: 0x001C56E7
	private void Awake()
	{
		this.m_PressElement.AddEventListener(UIEventType.PRESS, new UIEvent.Handler(this.OnPressElementPress));
		this.m_PressElement.AddEventListener(UIEventType.RELEASEALL, new UIEvent.Handler(this.OnPressElementReleaseAll));
		this.m_DragCollider.enabled = false;
	}

	// Token: 0x060056DE RID: 22238 RVA: 0x001C7527 File Offset: 0x001C5727
	private void Update()
	{
		this.UpdateDrag();
	}

	// Token: 0x060056DF RID: 22239 RVA: 0x001C752F File Offset: 0x001C572F
	private void OnDestroy()
	{
		if (UniversalInputManager.Get() != null)
		{
			UniversalInputManager.Get().UnregisterMouseOnOrOffScreenListener(new UniversalInputManager.MouseOnOrOffScreenCallback(this.OnMouseOnOrOffScreen));
		}
	}

	// Token: 0x060056E0 RID: 22240 RVA: 0x001C754F File Offset: 0x001C574F
	public float GetValue()
	{
		return this.m_thumbUnitPos;
	}

	// Token: 0x060056E1 RID: 22241 RVA: 0x001C7557 File Offset: 0x001C5757
	public void SetValue(float val)
	{
		this.m_thumbUnitPos = Mathf.Clamp01(val);
		this.m_prevThumbUnitPos = this.m_thumbUnitPos;
		this.UpdateThumb();
	}

	// Token: 0x060056E2 RID: 22242 RVA: 0x001C7577 File Offset: 0x001C5777
	public ScrollbarControl.UpdateHandler GetUpdateHandler()
	{
		return this.m_updateHandler;
	}

	// Token: 0x060056E3 RID: 22243 RVA: 0x001C757F File Offset: 0x001C577F
	public void SetUpdateHandler(ScrollbarControl.UpdateHandler handler)
	{
		this.m_updateHandler = handler;
	}

	// Token: 0x060056E4 RID: 22244 RVA: 0x001C7588 File Offset: 0x001C5788
	public ScrollbarControl.FinishHandler GetFinishHandler()
	{
		return this.m_finishHandler;
	}

	// Token: 0x060056E5 RID: 22245 RVA: 0x001C7590 File Offset: 0x001C5790
	public void SetFinishHandler(ScrollbarControl.FinishHandler handler)
	{
		this.m_finishHandler = handler;
	}

	// Token: 0x060056E6 RID: 22246 RVA: 0x001C7599 File Offset: 0x001C5799
	private void OnPressElementPress(UIEvent e)
	{
		this.HandlePress();
	}

	// Token: 0x060056E7 RID: 22247 RVA: 0x001C75A1 File Offset: 0x001C57A1
	private void OnPressElementReleaseAll(UIEvent e)
	{
		this.HandleRelease();
		this.FireFinishEvent();
	}

	// Token: 0x060056E8 RID: 22248 RVA: 0x001C75AF File Offset: 0x001C57AF
	private void OnMouseOnOrOffScreen(bool onScreen)
	{
		if (onScreen)
		{
			return;
		}
		this.HandleOutOfBounds();
	}

	// Token: 0x060056E9 RID: 22249 RVA: 0x001C75BC File Offset: 0x001C57BC
	private void UpdateDrag()
	{
		if (!this.m_dragging)
		{
			return;
		}
		RaycastHit raycastHit;
		if (UniversalInputManager.Get().GetInputHitInfo(1 << this.m_DragCollider.gameObject.layer, out raycastHit) && raycastHit.collider == this.m_DragCollider)
		{
			float x = this.m_LeftBone.position.x;
			float num = this.m_RightBone.position.x - x;
			this.m_thumbUnitPos = Mathf.Clamp01((raycastHit.point.x - x) / num);
			this.UpdateThumb();
			this.HandleThumbUpdate();
			return;
		}
		this.m_thumbUnitPos = this.m_prevThumbUnitPos;
		this.HandleOutOfBounds();
	}

	// Token: 0x060056EA RID: 22250 RVA: 0x001C766C File Offset: 0x001C586C
	private void UpdateThumb()
	{
		this.m_Thumb.transform.localPosition = Vector3.Lerp(this.m_LeftBone.localPosition, this.m_RightBone.localPosition, this.m_thumbUnitPos);
	}

	// Token: 0x060056EB RID: 22251 RVA: 0x001C76A0 File Offset: 0x001C58A0
	private void HandlePress()
	{
		this.m_dragging = true;
		UniversalInputManager.Get().RegisterMouseOnOrOffScreenListener(new UniversalInputManager.MouseOnOrOffScreenCallback(this.OnMouseOnOrOffScreen));
		this.m_PressElement.AddEventListener(UIEventType.RELEASEALL, new UIEvent.Handler(this.OnPressElementReleaseAll));
		this.m_PressElement.GetComponent<Collider>().enabled = false;
		this.m_DragCollider.enabled = true;
	}

	// Token: 0x060056EC RID: 22252 RVA: 0x001C7704 File Offset: 0x001C5904
	private void HandleRelease()
	{
		this.m_DragCollider.enabled = false;
		this.m_PressElement.GetComponent<Collider>().enabled = true;
		this.m_PressElement.RemoveEventListener(UIEventType.RELEASEALL, new UIEvent.Handler(this.OnPressElementReleaseAll));
		UniversalInputManager.Get().UnregisterMouseOnOrOffScreenListener(new UniversalInputManager.MouseOnOrOffScreenCallback(this.OnMouseOnOrOffScreen));
		this.m_dragging = false;
	}

	// Token: 0x060056ED RID: 22253 RVA: 0x001C7768 File Offset: 0x001C5968
	private void HandleThumbUpdate()
	{
		float prevThumbUnitPos = this.m_prevThumbUnitPos;
		this.m_prevThumbUnitPos = this.m_thumbUnitPos;
		if (Mathf.Approximately(this.m_thumbUnitPos, prevThumbUnitPos))
		{
			return;
		}
		this.FireUpdateEvent();
	}

	// Token: 0x060056EE RID: 22254 RVA: 0x001C779D File Offset: 0x001C599D
	private void HandleOutOfBounds()
	{
		this.UpdateThumb();
		this.HandleThumbUpdate();
		this.HandleRelease();
		this.FireFinishEvent();
	}

	// Token: 0x060056EF RID: 22255 RVA: 0x001C77B7 File Offset: 0x001C59B7
	private void FireUpdateEvent()
	{
		if (this.m_updateHandler == null)
		{
			return;
		}
		this.m_updateHandler(this.GetValue());
	}

	// Token: 0x060056F0 RID: 22256 RVA: 0x001C77D3 File Offset: 0x001C59D3
	private void FireFinishEvent()
	{
		if (this.m_finishHandler == null)
		{
			return;
		}
		this.m_finishHandler();
	}

	// Token: 0x04004ACD RID: 19149
	public GameObject m_Thumb;

	// Token: 0x04004ACE RID: 19150
	public PegUIElement m_PressElement;

	// Token: 0x04004ACF RID: 19151
	public Collider m_DragCollider;

	// Token: 0x04004AD0 RID: 19152
	public Transform m_LeftBone;

	// Token: 0x04004AD1 RID: 19153
	public Transform m_RightBone;

	// Token: 0x04004AD2 RID: 19154
	private bool m_dragging;

	// Token: 0x04004AD3 RID: 19155
	private float m_thumbUnitPos;

	// Token: 0x04004AD4 RID: 19156
	private float m_prevThumbUnitPos;

	// Token: 0x04004AD5 RID: 19157
	private ScrollbarControl.UpdateHandler m_updateHandler;

	// Token: 0x04004AD6 RID: 19158
	private ScrollbarControl.FinishHandler m_finishHandler;

	// Token: 0x02002113 RID: 8467
	// (Invoke) Token: 0x06012228 RID: 74280
	public delegate void UpdateHandler(float val);

	// Token: 0x02002114 RID: 8468
	// (Invoke) Token: 0x0601222C RID: 74284
	public delegate void FinishHandler();
}
