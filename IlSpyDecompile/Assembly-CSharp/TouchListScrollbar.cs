using System.Collections;
using UnityEngine;

public class TouchListScrollbar : PegUIElement
{
	public enum ScrollDirection
	{
		X,
		Y,
		Z
	}

	public TouchList list;

	public PegUIElement thumb;

	public Transform thumbMin;

	public Transform thumbMax;

	public GameObject cover;

	public PegUIElement track;

	public ScrollDirection scrollPlane = ScrollDirection.Y;

	private bool isActive;

	protected override void Awake()
	{
		if (list.orientation == TouchList.Orientation.Horizontal)
		{
			Debug.LogError("Horizontal TouchListScrollbar not implemented");
			Object.Destroy(this);
			return;
		}
		base.Awake();
		ShowThumb(isActive);
		list.ClipSizeChanged += UpdateLayout;
		list.ScrollingEnabledChanged += UpdateActive;
		list.Scrolled += UpdateThumb;
		thumb.AddEventListener(UIEventType.PRESS, ThumbPressed);
		track.AddEventListener(UIEventType.PRESS, TrackPressed);
		UpdateLayout();
	}

	private void UpdateActive(bool canScroll)
	{
		if (isActive != canScroll)
		{
			isActive = canScroll;
			thumb.GetComponent<Collider>().enabled = isActive;
			if (isActive)
			{
				UpdateThumb();
			}
			ShowThumb(isActive);
		}
	}

	private void UpdateLayout()
	{
		TransformUtil.SetPosX(thumb, thumbMin.position.x);
		UpdateThumb();
	}

	private void ShowThumb(bool show)
	{
		Transform transform = thumb.transform.Find("Mesh");
		if (transform != null)
		{
			transform.gameObject.SetActive(show);
		}
		if (cover != null)
		{
			cover.SetActive(!show);
		}
	}

	private void UpdateThumb()
	{
		if (isActive)
		{
			if (list.layoutPlane == TouchList.LayoutPlane.XZ)
			{
				TransformUtil.SetPosY(thumb, GetComponent<Collider>().bounds.min.y);
			}
			else
			{
				TransformUtil.SetPosZ(thumb, GetComponent<Collider>().bounds.min.z);
			}
			float scrollValue = list.ScrollValue;
			float value = thumbMin.position[(int)scrollPlane] + (thumbMax.position[(int)scrollPlane] - thumbMin.position[(int)scrollPlane]) * Mathf.Clamp01(scrollValue);
			Vector3 position = thumb.transform.position;
			position[(int)scrollPlane] = value;
			thumb.transform.position = position;
			thumb.transform.localScale = Vector3.one;
			if (scrollValue < 0f || scrollValue > 1f)
			{
				float num = 1f / ((scrollValue < 0f) ? (0f - scrollValue + 1f) : scrollValue);
				float num2 = ((scrollValue < 0f) ? thumb.GetComponent<Collider>().bounds.max : thumb.GetComponent<Collider>().bounds.min)[(int)scrollPlane];
				float num3 = (thumb.transform.position[(int)scrollPlane] - num2) * (num - 1f);
				position = thumb.transform.position;
				position[(int)scrollPlane] += num3;
				thumb.transform.position = position;
			}
		}
	}

	private void ThumbPressed(UIEvent e)
	{
		StartCoroutine(UpdateThumbDrag());
	}

	private void TrackPressed(UIEvent e)
	{
		Camera camera = CameraUtils.FindFirstByLayer(base.gameObject.layer);
		Plane dragPlane = new Plane(-camera.transform.forward, track.transform.position);
		float value = GetTouchPoint(dragPlane, camera)[(int)scrollPlane];
		value = Mathf.Clamp(value, thumbMax.position[(int)scrollPlane], thumbMin.position[(int)scrollPlane]);
		list.ScrollValue = (value - thumbMin.position[(int)scrollPlane]) / (thumbMax.position[(int)scrollPlane] - thumbMin.position[(int)scrollPlane]);
	}

	private IEnumerator UpdateThumbDrag()
	{
		Camera camera = CameraUtils.FindFirstByLayer(base.gameObject.layer);
		Plane dragPlane = new Plane(-camera.transform.forward, thumb.transform.position);
		float dragOffset = (thumb.transform.position - GetTouchPoint(dragPlane, camera))[(int)scrollPlane];
		while (!UniversalInputManager.Get().GetMouseButtonUp(0))
		{
			float value = GetTouchPoint(dragPlane, camera)[(int)scrollPlane] + dragOffset;
			value = Mathf.Clamp(value, thumbMax.position[(int)scrollPlane], thumbMin.position[(int)scrollPlane]);
			list.ScrollValue = (value - thumbMin.position[(int)scrollPlane]) / (thumbMax.position[(int)scrollPlane] - thumbMin.position[(int)scrollPlane]);
			yield return null;
		}
	}

	private Vector3 GetTouchPoint(Plane dragPlane, Camera camera)
	{
		Ray ray = camera.ScreenPointToRay(UniversalInputManager.Get().GetMousePosition());
		dragPlane.Raycast(ray, out var enter);
		return ray.GetPoint(enter);
	}
}
