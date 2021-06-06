using UnityEngine;

public class MeshSwapPegButton : PegUIElement
{
	public GameObject upState;

	public GameObject overState;

	public GameObject downState;

	public GameObject disabledState;

	public Vector3 downOffset;

	private Vector3 originalPosition;

	private Vector3 originalScale;

	private int m_buttonID;

	public TextMesh buttonText;

	protected override void Awake()
	{
		originalPosition = upState.transform.localPosition;
		base.Awake();
		SetState(InteractionState.Up);
		Bounds boundsOfChildren = TransformUtil.GetBoundsOfChildren(base.gameObject);
		if (GetComponent<MeshRenderer>() != null)
		{
			GetComponent<MeshRenderer>().bounds.SetMinMax(boundsOfChildren.min, boundsOfChildren.max);
		}
	}

	protected override void OnOver(InteractionState oldState)
	{
		if (base.gameObject.activeSelf)
		{
			SetState(InteractionState.Over);
		}
	}

	protected override void OnOut(InteractionState oldState)
	{
		if (base.gameObject.activeSelf)
		{
			SetState(InteractionState.Up);
		}
	}

	protected override void OnPress()
	{
		if (base.gameObject.activeSelf)
		{
			SetState(InteractionState.Down);
		}
	}

	protected override void OnRelease()
	{
		if (base.gameObject.activeSelf)
		{
			SetState(InteractionState.Over);
		}
	}

	public void SetButtonText(string s)
	{
		buttonText.text = s;
	}

	public void SetButtonID(int id)
	{
		m_buttonID = id;
	}

	public int GetButtonID()
	{
		return m_buttonID;
	}

	public void SetState(InteractionState state)
	{
		if (overState != null)
		{
			overState.SetActive(value: false);
		}
		if (disabledState != null)
		{
			disabledState.SetActive(value: false);
		}
		if (upState != null)
		{
			upState.SetActive(value: false);
		}
		if (downState != null)
		{
			downState.SetActive(value: false);
		}
		SetEnabled(enabled: true);
		switch (state)
		{
		case InteractionState.Up:
			upState.SetActive(value: true);
			downState.transform.localPosition = originalPosition;
			break;
		case InteractionState.Over:
			overState.SetActive(value: true);
			break;
		case InteractionState.Down:
			downState.transform.localPosition = originalPosition + downOffset;
			downState.SetActive(value: true);
			break;
		case InteractionState.Disabled:
			disabledState.SetActive(value: true);
			SetEnabled(enabled: false);
			break;
		}
	}

	public void Show()
	{
		base.gameObject.SetActive(value: true);
		SetState(InteractionState.Up);
	}

	public void Hide()
	{
		base.gameObject.SetActive(value: false);
	}
}
