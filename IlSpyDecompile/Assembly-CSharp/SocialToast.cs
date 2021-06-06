using UnityEngine;

public class SocialToast : MonoBehaviour
{
	private const float FUDGE_FACTOR = 0.95f;

	public UberText m_text;

	public void SetText(string text)
	{
		m_text.Text = text;
		ThreeSliceElement component = GetComponent<ThreeSliceElement>();
		if (component != null)
		{
			component.SetMiddleWidth(m_text.GetTextWorldSpaceBounds().size.x * 0.95f);
		}
	}
}
