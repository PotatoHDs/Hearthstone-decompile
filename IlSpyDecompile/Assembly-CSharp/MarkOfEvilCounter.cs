using UnityEngine;

public class MarkOfEvilCounter : MonoBehaviour
{
	public SpriteRenderer[] m_MarkOfEvilIcons;

	public Sprite m_FullMarkOfEvilSprite;

	public Sprite m_EmptyMarkOfEvilSprite;

	private void Awake()
	{
		OnMarksChanged(0);
	}

	public void OnMarksChanged(int numMarks)
	{
		if (numMarks <= 0)
		{
			base.gameObject.SetActive(value: false);
			return;
		}
		if (numMarks > m_MarkOfEvilIcons.Length)
		{
			Log.Gameplay.PrintWarning("{0}.OnMarksChanged() : num marks is greater than the number of icons!");
		}
		for (int i = 0; i < m_MarkOfEvilIcons.Length; i++)
		{
			m_MarkOfEvilIcons[i].sprite = ((i < numMarks) ? m_FullMarkOfEvilSprite : m_EmptyMarkOfEvilSprite);
		}
		base.gameObject.SetActive(value: true);
	}
}
