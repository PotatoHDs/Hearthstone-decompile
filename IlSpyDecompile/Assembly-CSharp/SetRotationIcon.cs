using UnityEngine;

public class SetRotationIcon : MonoBehaviour
{
	public GameObject m_YearIconQuad;

	public const string SHOW_EVENT = "SHOW";

	public const string HIDE_EVENT = "HIDE";

	private static Vector2 SET_ROTATION_FIRST_TEXTURE_OFFSET = new Vector2(0f, 0.5f);

	private static Vector2 SET_ROTATION_SECOND_TEXTURE_OFFSET = new Vector2(0.5f, 0.5f);

	private void Awake()
	{
		m_YearIconQuad.GetComponent<Renderer>().GetMaterial().mainTextureOffset = GetYearIconTextureOffset();
	}

	private Vector2 GetYearIconTextureOffset()
	{
		Vector2 sET_ROTATION_FIRST_TEXTURE_OFFSET = SET_ROTATION_FIRST_TEXTURE_OFFSET;
		Vector2 sET_ROTATION_SECOND_TEXTURE_OFFSET = SET_ROTATION_SECOND_TEXTURE_OFFSET;
		if (!SpecialEventManager.Get().IsEventActive(SpecialEventType.SPECIAL_EVENT_POST_SET_ROTATION_2021, activeIfDoesNotExist: true))
		{
			return sET_ROTATION_SECOND_TEXTURE_OFFSET;
		}
		return sET_ROTATION_FIRST_TEXTURE_OFFSET;
	}
}
