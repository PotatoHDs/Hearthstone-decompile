using UnityEngine;

[ExecuteAlways]
public class LookAtCamera : MonoBehaviour
{
	private readonly Vector3 X_VECTOR = new Vector3(1f, 0f, 0f);

	private readonly Vector3 Y_VECTOR = new Vector3(0f, 1f, 0f);

	private readonly Vector3 Z_VECTOR = new Vector3(0f, 0f, 1f);

	public Vector3 m_LookAtPositionOffset = Vector3.zero;

	private Camera m_MainCamera;

	private GameObject m_LookAtTarget;

	private void Awake()
	{
		CreateLookAtTarget();
	}

	private void Start()
	{
		m_MainCamera = Camera.main;
	}

	private void Update()
	{
		if (m_MainCamera == null)
		{
			m_MainCamera = Camera.main;
			if (m_MainCamera == null)
			{
				return;
			}
		}
		if (m_LookAtTarget == null)
		{
			CreateLookAtTarget();
			if (m_LookAtTarget == null)
			{
				return;
			}
		}
		m_LookAtTarget.transform.position = m_MainCamera.transform.position + m_LookAtPositionOffset;
		base.transform.LookAt(m_LookAtTarget.transform, Z_VECTOR);
		base.transform.Rotate(X_VECTOR, 90f);
		base.transform.Rotate(Y_VECTOR, 180f);
	}

	private void OnDestroy()
	{
		if ((bool)m_LookAtTarget)
		{
			Object.Destroy(m_LookAtTarget);
		}
	}

	private void CreateLookAtTarget()
	{
		m_LookAtTarget = new GameObject();
		m_LookAtTarget.name = "LookAtCamera Target";
	}
}
