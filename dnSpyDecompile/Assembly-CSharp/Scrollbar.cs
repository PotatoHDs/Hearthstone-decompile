using System;
using UnityEngine;

// Token: 0x02000AE0 RID: 2784
public class Scrollbar : MonoBehaviour
{
	// Token: 0x17000875 RID: 2165
	// (get) Token: 0x06009458 RID: 37976 RVA: 0x00301AB6 File Offset: 0x002FFCB6
	public float ScrollValue
	{
		get
		{
			return this.m_scrollValue;
		}
	}

	// Token: 0x06009459 RID: 37977 RVA: 0x00301ABE File Offset: 0x002FFCBE
	protected virtual void Awake()
	{
		this.m_scrollWindowHeight = this.m_scrollWindow.size.z;
		this.m_scrollWindow.enabled = false;
	}

	// Token: 0x0600945A RID: 37978 RVA: 0x00301AE2 File Offset: 0x002FFCE2
	public bool IsActive()
	{
		return this.m_isActive;
	}

	// Token: 0x0600945B RID: 37979 RVA: 0x00301AEC File Offset: 0x002FFCEC
	private void Update()
	{
		if (!this.m_isActive)
		{
			return;
		}
		if (this.InputIsOver())
		{
			if (Input.GetAxis("Mouse ScrollWheel") < 0f)
			{
				this.ScrollDown();
			}
			if (Input.GetAxis("Mouse ScrollWheel") > 0f)
			{
				this.ScrollUp();
			}
		}
		if (this.m_thumb.IsDragging())
		{
			this.Drag();
		}
	}

	// Token: 0x0600945C RID: 37980 RVA: 0x00301B4C File Offset: 0x002FFD4C
	public void Drag()
	{
		Vector3 min = this.m_track.GetComponent<BoxCollider>().bounds.min;
		Camera camera = CameraUtils.FindFirstByLayer(this.m_track.layer);
		Plane plane = new Plane(-camera.transform.forward, min);
		Ray ray = camera.ScreenPointToRay(UniversalInputManager.Get().GetMousePosition());
		float distance;
		if (plane.Raycast(ray, out distance))
		{
			Vector3 vector = base.transform.InverseTransformPoint(ray.GetPoint(distance));
			TransformUtil.SetLocalPosZ(this.m_thumb.gameObject, Mathf.Clamp(vector.z, this.m_sliderStartLocalPos.z, this.m_sliderEndLocalPos.z));
			this.m_scrollValue = Mathf.Clamp01((vector.z - this.m_sliderStartLocalPos.z) / (this.m_sliderEndLocalPos.z - this.m_sliderStartLocalPos.z));
			this.UpdateScrollAreaPosition(false);
		}
	}

	// Token: 0x0600945D RID: 37981 RVA: 0x00301C42 File Offset: 0x002FFE42
	public virtual void Show()
	{
		this.m_isActive = true;
		this.ShowThumb(true);
		base.gameObject.SetActive(true);
	}

	// Token: 0x0600945E RID: 37982 RVA: 0x00301C5E File Offset: 0x002FFE5E
	public virtual void Hide()
	{
		this.m_isActive = false;
		this.ShowThumb(false);
		base.gameObject.SetActive(false);
	}

	// Token: 0x0600945F RID: 37983 RVA: 0x00301C7C File Offset: 0x002FFE7C
	public void Init()
	{
		this.m_scrollValue = 0f;
		this.m_stepSize = 1f;
		this.m_thumb.transform.localPosition = this.m_sliderStartLocalPos;
		this.m_scrollAreaStartPos = this.m_scrollArea.transform.position;
		this.UpdateScrollAreaBounds();
	}

	// Token: 0x06009460 RID: 37984 RVA: 0x00301CD4 File Offset: 0x002FFED4
	public void UpdateScrollAreaBounds()
	{
		this.GetBoundsOfChildren(this.m_scrollArea);
		float num = this.m_childrenBounds.size.z - this.m_scrollWindowHeight;
		this.m_scrollAreaEndPos = this.m_scrollAreaStartPos;
		if (num <= 0f)
		{
			this.m_scrollValue = 0f;
			this.Hide();
		}
		else
		{
			int num2 = (int)Mathf.Ceil(num / 5f);
			this.m_stepSize = 1f / (float)num2;
			this.m_scrollAreaEndPos.z = this.m_scrollAreaEndPos.z + num;
			this.Show();
		}
		this.UpdateThumbPosition();
		this.UpdateScrollAreaPosition(false);
	}

	// Token: 0x06009461 RID: 37985 RVA: 0x000B3885 File Offset: 0x000B1A85
	public virtual bool InputIsOver()
	{
		return UniversalInputManager.Get().InputIsOver(base.gameObject);
	}

	// Token: 0x06009462 RID: 37986 RVA: 0x00301D6B File Offset: 0x002FFF6B
	protected virtual void GetBoundsOfChildren(GameObject go)
	{
		this.m_childrenBounds = TransformUtil.GetBoundsOfChildren(go);
	}

	// Token: 0x06009463 RID: 37987 RVA: 0x00301D79 File Offset: 0x002FFF79
	public void OverrideScrollWindowHeight(float scrollWindowHeight)
	{
		this.m_scrollWindowHeight = scrollWindowHeight;
	}

	// Token: 0x06009464 RID: 37988 RVA: 0x00301D82 File Offset: 0x002FFF82
	protected void ShowThumb(bool show)
	{
		if (this.m_thumb != null)
		{
			this.m_thumb.gameObject.SetActive(show);
		}
	}

	// Token: 0x06009465 RID: 37989 RVA: 0x00301DA4 File Offset: 0x002FFFA4
	private void UpdateThumbPosition()
	{
		this.m_thumbPosition = Vector3.Lerp(this.m_sliderStartLocalPos, this.m_sliderEndLocalPos, Mathf.Clamp01(this.m_scrollValue));
		this.m_thumb.transform.localPosition = this.m_thumbPosition;
		this.m_thumb.transform.localScale = Vector3.one;
		if (this.m_scrollValue < 0f || this.m_scrollValue > 1f)
		{
			float num = 1f / ((this.m_scrollValue < 0f) ? (-this.m_scrollValue + 1f) : this.m_scrollValue);
			float z = this.m_thumb.transform.parent.InverseTransformPoint((this.m_scrollValue < 0f) ? this.m_thumb.GetComponent<Renderer>().bounds.max : this.m_thumb.GetComponent<Renderer>().bounds.min).z;
			float num2 = (this.m_thumbPosition.z - z) * (num - 1f);
			TransformUtil.SetLocalPosZ(this.m_thumb, this.m_thumbPosition.z + num2);
			TransformUtil.SetLocalScaleZ(this.m_thumb, num);
		}
	}

	// Token: 0x06009466 RID: 37990 RVA: 0x00301ED8 File Offset: 0x003000D8
	private void UpdateScrollAreaPosition(bool tween)
	{
		if (this.m_scrollArea == null)
		{
			return;
		}
		Vector3 vector = this.m_scrollAreaStartPos + this.m_scrollValue * (this.m_scrollAreaEndPos - this.m_scrollAreaStartPos);
		if (tween)
		{
			iTween.MoveTo(this.m_scrollArea, iTween.Hash(new object[]
			{
				"position",
				vector,
				"time",
				0.2f,
				"isLocal",
				false
			}));
			return;
		}
		this.m_scrollArea.transform.position = vector;
	}

	// Token: 0x06009467 RID: 37991 RVA: 0x00301F7E File Offset: 0x0030017E
	public void ScrollTo(float value, bool clamp = true, bool lerp = true)
	{
		this.m_scrollValue = (clamp ? Mathf.Clamp01(value) : value);
		this.UpdateThumbPosition();
		this.UpdateScrollAreaPosition(lerp);
	}

	// Token: 0x06009468 RID: 37992 RVA: 0x00301F9F File Offset: 0x0030019F
	private void ScrollUp()
	{
		this.Scroll(-this.m_stepSize, true);
	}

	// Token: 0x06009469 RID: 37993 RVA: 0x00301FAF File Offset: 0x003001AF
	public void Scroll(float amount, bool lerp = true)
	{
		this.m_scrollValue = Mathf.Clamp01(this.m_scrollValue + amount);
		this.UpdateThumbPosition();
		this.UpdateScrollAreaPosition(lerp);
	}

	// Token: 0x0600946A RID: 37994 RVA: 0x00301FD1 File Offset: 0x003001D1
	private void ScrollDown()
	{
		this.Scroll(this.m_stepSize, true);
	}

	// Token: 0x04007C53 RID: 31827
	public ScrollBarThumb m_thumb;

	// Token: 0x04007C54 RID: 31828
	public GameObject m_track;

	// Token: 0x04007C55 RID: 31829
	public Vector3 m_sliderStartLocalPos;

	// Token: 0x04007C56 RID: 31830
	public Vector3 m_sliderEndLocalPos;

	// Token: 0x04007C57 RID: 31831
	public GameObject m_scrollArea;

	// Token: 0x04007C58 RID: 31832
	public BoxCollider m_scrollWindow;

	// Token: 0x04007C59 RID: 31833
	protected bool m_isActive = true;

	// Token: 0x04007C5A RID: 31834
	protected bool m_isDragging;

	// Token: 0x04007C5B RID: 31835
	protected float m_scrollValue;

	// Token: 0x04007C5C RID: 31836
	protected Vector3 m_scrollAreaStartPos;

	// Token: 0x04007C5D RID: 31837
	protected Vector3 m_scrollAreaEndPos;

	// Token: 0x04007C5E RID: 31838
	protected float m_stepSize;

	// Token: 0x04007C5F RID: 31839
	protected Vector3 m_thumbPosition;

	// Token: 0x04007C60 RID: 31840
	protected Bounds m_childrenBounds;

	// Token: 0x04007C61 RID: 31841
	protected float m_scrollWindowHeight;
}
