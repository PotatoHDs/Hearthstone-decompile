using UnityEngine;

public class MeshAnimation : MonoBehaviour
{
	public Mesh[] Meshes;

	public bool Loop;

	public float FrameDuration;

	private int m_Index;

	private bool m_Playing;

	private float m_FrameTime;

	private MeshFilter m_Mesh;

	private void Start()
	{
		m_Mesh = GetComponent<MeshFilter>();
	}

	private void Update()
	{
		if (!m_Playing)
		{
			return;
		}
		m_FrameTime += Time.deltaTime;
		if (m_FrameTime >= FrameDuration)
		{
			m_Index = (m_Index + 1) % Meshes.Length;
			m_FrameTime -= FrameDuration;
			if (!Loop && m_Index == 0)
			{
				m_Playing = false;
				base.enabled = false;
			}
			else
			{
				m_Mesh.mesh = Meshes[m_Index];
			}
		}
	}

	public void Play()
	{
		base.enabled = true;
		m_Playing = true;
	}

	public void Stop()
	{
		m_Playing = false;
		base.enabled = false;
	}

	public void Reset()
	{
		m_Mesh.mesh = Meshes[0];
		m_FrameTime = 0f;
		m_Index = 0;
	}
}
