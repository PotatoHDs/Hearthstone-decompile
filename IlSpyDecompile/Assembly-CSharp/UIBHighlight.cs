using UnityEngine;

[CustomEditClass]
[RequireComponent(typeof(PegUIElement))]
public class UIBHighlight : MonoBehaviour
{
	[CustomEditField(Sections = "Highlight Objects")]
	public GameObject m_MouseOverHighlight;

	[CustomEditField(Sections = "Highlight Objects")]
	public GameObject m_MouseDownHighlight;

	[CustomEditField(Sections = "Highlight Objects")]
	public GameObject m_MouseUpHighlight;

	[CustomEditField(Sections = "Highlight Sounds", T = EditType.SOUND_PREFAB)]
	public string m_MouseOverSound = "Small_Mouseover.prefab:692610296028713458ea58bc34adb4c9";

	[CustomEditField(Sections = "Highlight Sounds", T = EditType.SOUND_PREFAB)]
	public string m_MouseOutSound;

	[CustomEditField(Sections = "Highlight Sounds", T = EditType.SOUND_PREFAB)]
	public string m_MouseDownSound = "Small_Click.prefab:2a1c5335bf08dc84eb6e04fc58160681";

	[CustomEditField(Sections = "Highlight Sounds", T = EditType.SOUND_PREFAB)]
	public string m_MouseUpSound;

	[CustomEditField(Sections = "Behavior Settings")]
	public bool m_SelectOnRelease;

	[CustomEditField(Sections = "Behavior Settings")]
	public bool m_HideMouseOverOnPress;

	[SerializeField]
	private bool m_AlwaysOver;

	[SerializeField]
	private bool m_EnableResponse = true;

	[CustomEditField(Sections = "Allow Selection", Label = "Enable")]
	public bool m_AllowSelection;

	[CustomEditField(Parent = "m_AllowSelection")]
	public GameObject m_SelectedHighlight;

	[CustomEditField(Parent = "m_AllowSelection")]
	public GameObject m_MouseOverSelectedHighlight;

	private PegUIElement m_PegUIElement;

	[CustomEditField(Sections = "Behavior Settings")]
	public bool AlwaysOver
	{
		get
		{
			return m_AlwaysOver;
		}
		set
		{
			m_AlwaysOver = value;
			ResetState();
		}
	}

	[CustomEditField(Sections = "Behavior Settings")]
	public bool EnableResponse
	{
		get
		{
			return m_EnableResponse;
		}
		set
		{
			m_EnableResponse = value;
			ResetState();
		}
	}

	private void Awake()
	{
		PegUIElement component = base.gameObject.GetComponent<PegUIElement>();
		if (component != null)
		{
			component.AddEventListener(UIEventType.ROLLOVER, delegate
			{
				OnRollOver();
			});
			component.AddEventListener(UIEventType.PRESS, delegate
			{
				OnPress(playSound: true);
			});
			component.AddEventListener(UIEventType.RELEASE, delegate
			{
				OnRelease();
			});
			component.AddEventListener(UIEventType.ROLLOUT, delegate
			{
				OnRollOut();
			});
			ResetState();
		}
	}

	public void HighlightOnce()
	{
		OnRollOver(force: true);
	}

	public void Select()
	{
		if (m_SelectOnRelease)
		{
			OnRelease(playSound: true);
		}
		else
		{
			OnPress(playSound: true);
		}
	}

	public void SelectNoSound()
	{
		if (m_SelectOnRelease)
		{
			OnRelease(playSound: false);
		}
		else
		{
			OnPress(playSound: false);
		}
	}

	public void Reset()
	{
		ResetState();
		ShowHighlightObject(m_SelectedHighlight, show: false);
		ShowHighlightObject(m_MouseOverSelectedHighlight, show: false);
		ShowHighlightObject(m_MouseOverHighlight, show: false);
	}

	private void ResetState()
	{
		if (m_AlwaysOver)
		{
			OnRollOver(force: true);
		}
		else
		{
			OnRollOut(force: true);
		}
	}

	private void OnRollOver(bool force = false)
	{
		if (m_EnableResponse || force)
		{
			if (!m_AlwaysOver)
			{
				PlaySound(m_MouseOverSound);
			}
			if (m_AllowSelection && (m_SelectedHighlight == null || m_SelectedHighlight.activeSelf))
			{
				ShowHighlightObject(m_MouseOverSelectedHighlight, show: true);
				ShowHighlightObject(m_SelectedHighlight, show: false);
				ShowHighlightObject(m_MouseOverHighlight, show: false);
				ShowHighlightObject(m_MouseUpHighlight, show: false);
				ShowHighlightObject(m_MouseDownHighlight, show: false);
			}
			else
			{
				ShowHighlightObject(m_MouseDownHighlight, show: false);
				ShowHighlightObject(m_MouseOverHighlight, show: true);
				ShowHighlightObject(m_MouseUpHighlight, show: false);
			}
		}
	}

	private void OnRollOut(bool force = false)
	{
		if (m_EnableResponse || force)
		{
			PlaySound(m_MouseOutSound);
			if (m_AllowSelection && (m_MouseOverSelectedHighlight == null || m_MouseOverSelectedHighlight.activeSelf))
			{
				ShowHighlightObject(m_SelectedHighlight, show: true);
				ShowHighlightObject(m_MouseOverSelectedHighlight, show: false);
				ShowHighlightObject(m_MouseOverHighlight, show: false);
				ShowHighlightObject(m_MouseUpHighlight, show: false);
				ShowHighlightObject(m_MouseDownHighlight, show: false);
			}
			else
			{
				ShowHighlightObject(m_MouseDownHighlight, show: false);
				ShowHighlightObject(m_MouseOverHighlight, m_AlwaysOver);
				ShowHighlightObject(m_MouseUpHighlight, !m_AlwaysOver);
			}
		}
	}

	private void OnPress()
	{
		OnPress(playSound: true);
	}

	private void OnPress(bool playSound)
	{
		if (m_EnableResponse)
		{
			if (playSound)
			{
				PlaySound(m_MouseDownSound);
			}
			if (m_AllowSelection && !m_SelectOnRelease)
			{
				ShowHighlightObject(m_SelectedHighlight, show: true);
				ShowHighlightObject(m_MouseOverSelectedHighlight, show: false);
				ShowHighlightObject(m_MouseOverHighlight, show: false);
				ShowHighlightObject(m_MouseUpHighlight, show: false);
				ShowHighlightObject(m_MouseDownHighlight, show: false);
			}
			else
			{
				ShowHighlightObject(m_MouseDownHighlight, show: true);
				ShowHighlightObject(m_MouseOverHighlight, m_AlwaysOver || !m_HideMouseOverOnPress);
				ShowHighlightObject(m_MouseUpHighlight, !m_AlwaysOver);
			}
		}
	}

	private void OnRelease()
	{
		OnRelease(playSound: true);
	}

	private void OnRelease(bool playSound)
	{
		if (m_EnableResponse)
		{
			if (playSound)
			{
				PlaySound(m_MouseUpSound);
			}
			if (m_AllowSelection && m_SelectOnRelease)
			{
				ShowHighlightObject(m_SelectedHighlight, show: true);
				ShowHighlightObject(m_MouseOverSelectedHighlight, show: false);
				ShowHighlightObject(m_MouseOverHighlight, show: false);
				ShowHighlightObject(m_MouseUpHighlight, show: false);
				ShowHighlightObject(m_MouseDownHighlight, show: false);
			}
			else
			{
				ShowHighlightObject(m_MouseDownHighlight, show: false);
				ShowHighlightObject(m_MouseOverHighlight, show: true);
				ShowHighlightObject(m_MouseUpHighlight, show: false);
			}
		}
	}

	private void ShowHighlightObject(GameObject obj, bool show)
	{
		if (obj != null && obj.activeSelf != show)
		{
			obj.SetActive(show);
		}
	}

	private void PlaySound(string soundFilePath)
	{
		if (SoundManager.Get() != null && !string.IsNullOrEmpty(soundFilePath))
		{
			SoundManager.Get().LoadAndPlay(soundFilePath);
		}
	}
}
