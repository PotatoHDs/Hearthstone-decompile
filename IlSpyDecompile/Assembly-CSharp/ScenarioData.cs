using UnityEngine;

[CustomEditClass]
public class ScenarioData : ScriptableObject
{
	private bool _bottom;

	private float m_phoneoffset = -0.389f;

	[CustomEditField(T = EditType.TEXTURE)]
	public string m_Texture;

	[CustomEditField(Sections = "Phone", T = EditType.TEXTURE)]
	public string m_Texture_Phone;

	[CustomEditField(Hide = true)]
	public float m_Texture_Phone_offsetY;

	[CustomEditField(Sections = "Phone", Label = "Use Bottom Image")]
	public bool m_bottom
	{
		get
		{
			return _bottom;
		}
		set
		{
			_bottom = value;
			m_Texture_Phone_offsetY = (value ? m_phoneoffset : 0f);
		}
	}
}
