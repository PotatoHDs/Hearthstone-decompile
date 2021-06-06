using UnityEngine;

public class ChangeMaterialFloat : MonoBehaviour
{
	public Renderer m_Rend1;

	public float m_Intensity1;

	private Material m_mat1;

	public Renderer m_Rend2;

	public float m_Intensity2;

	private Material m_mat2;

	public Renderer m_Rend3;

	public float m_Intensity3;

	private Material m_mat3;

	public Renderer m_Rend4;

	public float m_Intensity4;

	private Material m_mat4;

	public Renderer m_Rend5;

	public float m_Intensity5;

	private Material m_mat5;

	public Renderer m_Rend6;

	public float m_Intensity6;

	private Material m_mat6;

	private int m_intensityProperty;

	private void Start()
	{
		m_intensityProperty = Shader.PropertyToID("_Intensity");
		if (m_Rend1 != null)
		{
			m_mat1 = m_Rend1.GetMaterial();
		}
		if (m_Rend2 != null)
		{
			m_mat2 = m_Rend2.GetMaterial();
		}
		if (m_Rend3 != null)
		{
			m_mat3 = m_Rend3.GetMaterial();
		}
		if (m_Rend4 != null)
		{
			m_mat4 = m_Rend4.GetMaterial();
		}
		if (m_Rend5 != null)
		{
			m_mat5 = m_Rend5.GetMaterial();
		}
		if (m_Rend6 != null)
		{
			m_mat6 = m_Rend6.GetMaterial();
		}
	}

	private void Update()
	{
		if (m_Rend1 != null)
		{
			if (m_Intensity1 <= 0f)
			{
				m_Rend1.enabled = false;
			}
			else
			{
				m_Rend1.enabled = true;
			}
			m_mat1.SetFloat(m_intensityProperty, m_Intensity1);
		}
		if (m_Rend2 != null)
		{
			if (m_Intensity2 <= 0f)
			{
				m_Rend2.enabled = false;
			}
			else
			{
				m_Rend2.enabled = true;
			}
			m_mat2.SetFloat(m_intensityProperty, m_Intensity2);
		}
		if (m_Rend3 != null)
		{
			if (m_Intensity3 <= 0f)
			{
				m_Rend3.enabled = false;
			}
			else
			{
				m_Rend3.enabled = true;
			}
			m_mat3.SetFloat(m_intensityProperty, m_Intensity3);
		}
		if (m_Rend4 != null)
		{
			if (m_Intensity4 <= 0f)
			{
				m_Rend4.enabled = false;
			}
			else
			{
				m_Rend4.enabled = true;
			}
			m_mat4.SetFloat(m_intensityProperty, m_Intensity4);
		}
		if (m_Rend5 != null)
		{
			if (m_Intensity5 <= 0f)
			{
				m_Rend5.enabled = false;
			}
			else
			{
				m_Rend5.enabled = true;
			}
			m_mat5.SetFloat(m_intensityProperty, m_Intensity5);
		}
		if (m_Rend6 != null)
		{
			if (m_Intensity6 <= 0f)
			{
				m_Rend6.enabled = false;
			}
			else
			{
				m_Rend6.enabled = true;
			}
			m_mat6.SetFloat(m_intensityProperty, m_Intensity6);
		}
	}

	private void OnDestroy()
	{
		if (m_mat1 != null)
		{
			Object.Destroy(m_mat1);
			m_mat1 = null;
		}
		if (m_mat2 != null)
		{
			Object.Destroy(m_mat2);
			m_mat2 = null;
		}
		if (m_mat3 != null)
		{
			Object.Destroy(m_mat3);
			m_mat3 = null;
		}
		if (m_mat4 != null)
		{
			Object.Destroy(m_mat4);
			m_mat4 = null;
		}
		if (m_mat5 != null)
		{
			Object.Destroy(m_mat5);
			m_mat5 = null;
		}
		if (m_mat6 != null)
		{
			Object.Destroy(m_mat6);
			m_mat1 = null;
		}
	}
}
