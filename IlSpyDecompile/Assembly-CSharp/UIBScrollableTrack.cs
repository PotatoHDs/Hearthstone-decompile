using UnityEngine;

[CustomEditClass]
[RequireComponent(typeof(BoxCollider), typeof(PegUIElement))]
public class UIBScrollableTrack : MonoBehaviour
{
	public UIBScrollable m_parentScrollbar;

	public GameObject m_scrollTrack;

	public Vector3 m_showRotation = Vector3.zero;

	public Vector3 m_hideRotation = new Vector3(0f, 0f, 180f);

	public float m_rotateAnimationTime = 0.5f;

	private bool m_lastEnabled;

	private void Awake()
	{
		if (m_parentScrollbar == null)
		{
			Debug.LogError("Parent scroll bar not set!");
		}
		else
		{
			m_parentScrollbar.AddEnableScrollListener(OnScrollEnabled);
		}
	}

	private void OnEnable()
	{
		if (m_scrollTrack != null)
		{
			m_lastEnabled = m_parentScrollbar.IsEnabled();
			m_scrollTrack.transform.localEulerAngles = (m_lastEnabled ? m_showRotation : m_hideRotation);
		}
	}

	private void OnScrollEnabled(bool enabled)
	{
		if (!(m_scrollTrack == null) && m_scrollTrack.activeInHierarchy && m_lastEnabled != enabled)
		{
			m_lastEnabled = enabled;
			Vector3 localEulerAngles;
			Vector3 vector;
			if (enabled)
			{
				localEulerAngles = m_hideRotation;
				vector = m_showRotation;
			}
			else
			{
				localEulerAngles = m_showRotation;
				vector = m_hideRotation;
			}
			m_scrollTrack.transform.localEulerAngles = localEulerAngles;
			iTween.StopByName(m_scrollTrack, "rotate");
			iTween.RotateTo(m_scrollTrack, iTween.Hash("rotation", vector, "islocal", true, "time", m_rotateAnimationTime, "name", "rotate"));
		}
	}
}
