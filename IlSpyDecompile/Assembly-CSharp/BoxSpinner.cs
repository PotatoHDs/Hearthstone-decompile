using UnityEngine;

public class BoxSpinner : MonoBehaviour
{
	private Box m_parent;

	private BoxSpinnerStateInfo m_info;

	private bool m_spinning;

	private float m_spinY;

	private Material m_spinnerMat;

	private void Awake()
	{
		m_spinnerMat = GetComponent<Renderer>().GetMaterial();
	}

	private void Update()
	{
		if (IsSpinning())
		{
			m_spinnerMat.SetFloat("_RotAngle", m_spinY);
			m_spinY += m_info.m_DegreesPerSec * Time.deltaTime * 0.01f;
		}
	}

	private void OnDestroy()
	{
		Object.Destroy(m_spinnerMat);
	}

	public Box GetParent()
	{
		return m_parent;
	}

	public void SetParent(Box parent)
	{
		m_parent = parent;
	}

	public BoxSpinnerStateInfo GetInfo()
	{
		return m_info;
	}

	public void SetInfo(BoxSpinnerStateInfo info)
	{
		m_info = info;
	}

	public void Spin()
	{
		m_spinning = true;
	}

	public bool IsSpinning()
	{
		return m_spinning;
	}

	public void Stop()
	{
		m_spinning = false;
	}

	public void Reset()
	{
		m_spinning = false;
		m_spinnerMat.SetFloat("_RotAngle", 0f);
	}
}
