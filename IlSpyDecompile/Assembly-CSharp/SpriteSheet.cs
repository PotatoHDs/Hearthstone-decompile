using UnityEngine;

public class SpriteSheet : MonoBehaviour
{
	public int _uvTieX = 1;

	public int _uvTieY = 1;

	public float m_fps = 30f;

	public bool m_Old_Mode;

	private int m_LastIdx = -1;

	private Vector2 m_Size = Vector2.one;

	private float m_NextFrame;

	private int m_X;

	private int m_Y;

	private Renderer m_renderer;

	private void Start()
	{
		m_NextFrame = Time.timeSinceLevelLoad + 1f / m_fps;
		m_renderer = GetComponent<Renderer>();
		if (m_renderer == null)
		{
			Debug.LogError("SpriteSheet needs a Renderer on: " + base.gameObject.name);
			base.enabled = false;
		}
		m_Size = new Vector2(1f / (float)_uvTieX, 1f / (float)_uvTieY);
	}

	private void Update()
	{
		if (m_Old_Mode)
		{
			int num = (int)(Time.time * m_fps % (float)(_uvTieX * _uvTieY));
			if (num != m_LastIdx)
			{
				Material material = m_renderer.GetMaterial();
				material.mainTextureOffset = new Vector2((float)(num % _uvTieX) * m_Size.x, 1f - m_Size.y - (float)(num / _uvTieY) * m_Size.y);
				material.mainTextureScale = m_Size;
				m_LastIdx = num;
			}
		}
		else if (!(Time.timeSinceLevelLoad < m_NextFrame))
		{
			m_X++;
			if (m_X > _uvTieX - 1)
			{
				m_Y++;
				m_X = 0;
			}
			if (m_Y > _uvTieY - 1)
			{
				m_Y = 0;
			}
			Material material2 = m_renderer.GetMaterial();
			material2.mainTextureOffset = new Vector2((float)m_X * m_Size.x, 1f - (float)m_Y * m_Size.y);
			material2.mainTextureScale = m_Size;
			m_NextFrame = Time.timeSinceLevelLoad + 1f / m_fps;
		}
	}
}
