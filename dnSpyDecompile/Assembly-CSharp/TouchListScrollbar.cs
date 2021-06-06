using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000B30 RID: 2864
public class TouchListScrollbar : PegUIElement
{
	// Token: 0x0600981C RID: 38940 RVA: 0x003147A8 File Offset: 0x003129A8
	protected override void Awake()
	{
		if (this.list.orientation == TouchList.Orientation.Horizontal)
		{
			Debug.LogError("Horizontal TouchListScrollbar not implemented");
			UnityEngine.Object.Destroy(this);
			return;
		}
		base.Awake();
		this.ShowThumb(this.isActive);
		this.list.ClipSizeChanged += this.UpdateLayout;
		this.list.ScrollingEnabledChanged += this.UpdateActive;
		this.list.Scrolled += this.UpdateThumb;
		this.thumb.AddEventListener(UIEventType.PRESS, new UIEvent.Handler(this.ThumbPressed));
		this.track.AddEventListener(UIEventType.PRESS, new UIEvent.Handler(this.TrackPressed));
		this.UpdateLayout();
	}

	// Token: 0x0600981D RID: 38941 RVA: 0x00314864 File Offset: 0x00312A64
	private void UpdateActive(bool canScroll)
	{
		if (this.isActive == canScroll)
		{
			return;
		}
		this.isActive = canScroll;
		this.thumb.GetComponent<Collider>().enabled = this.isActive;
		if (this.isActive)
		{
			this.UpdateThumb();
		}
		this.ShowThumb(this.isActive);
	}

	// Token: 0x0600981E RID: 38942 RVA: 0x003148B2 File Offset: 0x00312AB2
	private void UpdateLayout()
	{
		TransformUtil.SetPosX(this.thumb, this.thumbMin.position.x);
		this.UpdateThumb();
	}

	// Token: 0x0600981F RID: 38943 RVA: 0x003148D8 File Offset: 0x00312AD8
	private void ShowThumb(bool show)
	{
		Transform transform = this.thumb.transform.Find("Mesh");
		if (transform != null)
		{
			transform.gameObject.SetActive(show);
		}
		if (this.cover != null)
		{
			this.cover.SetActive(!show);
		}
	}

	// Token: 0x06009820 RID: 38944 RVA: 0x00314930 File Offset: 0x00312B30
	private void UpdateThumb()
	{
		if (!this.isActive)
		{
			return;
		}
		if (this.list.layoutPlane == TouchList.LayoutPlane.XZ)
		{
			TransformUtil.SetPosY(this.thumb, base.GetComponent<Collider>().bounds.min.y);
		}
		else
		{
			TransformUtil.SetPosZ(this.thumb, base.GetComponent<Collider>().bounds.min.z);
		}
		float scrollValue = this.list.ScrollValue;
		float value = this.thumbMin.position[(int)this.scrollPlane] + (this.thumbMax.position[(int)this.scrollPlane] - this.thumbMin.position[(int)this.scrollPlane]) * Mathf.Clamp01(scrollValue);
		Vector3 position = this.thumb.transform.position;
		position[(int)this.scrollPlane] = value;
		this.thumb.transform.position = position;
		this.thumb.transform.localScale = Vector3.one;
		if (scrollValue < 0f || scrollValue > 1f)
		{
			float num = 1f / ((scrollValue < 0f) ? (-scrollValue + 1f) : scrollValue);
			float num2 = ((scrollValue < 0f) ? this.thumb.GetComponent<Collider>().bounds.max : this.thumb.GetComponent<Collider>().bounds.min)[(int)this.scrollPlane];
			float num3 = (this.thumb.transform.position[(int)this.scrollPlane] - num2) * (num - 1f);
			position = this.thumb.transform.position;
			ref Vector3 ptr = ref position;
			int index = (int)this.scrollPlane;
			ptr[index] += num3;
			this.thumb.transform.position = position;
		}
	}

	// Token: 0x06009821 RID: 38945 RVA: 0x00314B2E File Offset: 0x00312D2E
	private void ThumbPressed(UIEvent e)
	{
		base.StartCoroutine(this.UpdateThumbDrag());
	}

	// Token: 0x06009822 RID: 38946 RVA: 0x00314B40 File Offset: 0x00312D40
	private void TrackPressed(UIEvent e)
	{
		Camera camera = CameraUtils.FindFirstByLayer(base.gameObject.layer);
		Plane dragPlane = new Plane(-camera.transform.forward, this.track.transform.position);
		float num = this.GetTouchPoint(dragPlane, camera)[(int)this.scrollPlane];
		num = Mathf.Clamp(num, this.thumbMax.position[(int)this.scrollPlane], this.thumbMin.position[(int)this.scrollPlane]);
		this.list.ScrollValue = (num - this.thumbMin.position[(int)this.scrollPlane]) / (this.thumbMax.position[(int)this.scrollPlane] - this.thumbMin.position[(int)this.scrollPlane]);
	}

	// Token: 0x06009823 RID: 38947 RVA: 0x00314C2F File Offset: 0x00312E2F
	private IEnumerator UpdateThumbDrag()
	{
		Camera camera = CameraUtils.FindFirstByLayer(base.gameObject.layer);
		Plane dragPlane = new Plane(-camera.transform.forward, this.thumb.transform.position);
		float dragOffset = (this.thumb.transform.position - this.GetTouchPoint(dragPlane, camera))[(int)this.scrollPlane];
		while (!UniversalInputManager.Get().GetMouseButtonUp(0))
		{
			float num = this.GetTouchPoint(dragPlane, camera)[(int)this.scrollPlane] + dragOffset;
			num = Mathf.Clamp(num, this.thumbMax.position[(int)this.scrollPlane], this.thumbMin.position[(int)this.scrollPlane]);
			this.list.ScrollValue = (num - this.thumbMin.position[(int)this.scrollPlane]) / (this.thumbMax.position[(int)this.scrollPlane] - this.thumbMin.position[(int)this.scrollPlane]);
			yield return null;
		}
		yield break;
	}

	// Token: 0x06009824 RID: 38948 RVA: 0x00314C40 File Offset: 0x00312E40
	private Vector3 GetTouchPoint(Plane dragPlane, Camera camera)
	{
		Ray ray = camera.ScreenPointToRay(UniversalInputManager.Get().GetMousePosition());
		float distance;
		dragPlane.Raycast(ray, out distance);
		return ray.GetPoint(distance);
	}

	// Token: 0x04007F3F RID: 32575
	public TouchList list;

	// Token: 0x04007F40 RID: 32576
	public PegUIElement thumb;

	// Token: 0x04007F41 RID: 32577
	public Transform thumbMin;

	// Token: 0x04007F42 RID: 32578
	public Transform thumbMax;

	// Token: 0x04007F43 RID: 32579
	public GameObject cover;

	// Token: 0x04007F44 RID: 32580
	public PegUIElement track;

	// Token: 0x04007F45 RID: 32581
	public TouchListScrollbar.ScrollDirection scrollPlane = TouchListScrollbar.ScrollDirection.Y;

	// Token: 0x04007F46 RID: 32582
	private bool isActive;

	// Token: 0x02002779 RID: 10105
	public enum ScrollDirection
	{
		// Token: 0x0400F41B RID: 62491
		X,
		// Token: 0x0400F41C RID: 62492
		Y,
		// Token: 0x0400F41D RID: 62493
		Z
	}
}
