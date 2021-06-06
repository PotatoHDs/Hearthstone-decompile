using UnityEngine;

public class UberFloaty : MonoBehaviour
{
	public float speed = 1f;

	public float positionBlend = 1f;

	public float frequencyMin = 1f;

	public float frequencyMax = 3f;

	public bool localSpace = true;

	public Vector3 magnitude = new Vector3(0.001f, 0.001f, 0.001f);

	public float rotationBlend = 1f;

	public float frequencyMinRot = 1f;

	public float frequencyMaxRot = 3f;

	public Vector3 magnitudeRot = new Vector3(0f, 0f, 0f);

	private Vector3 m_interval;

	private Vector3 m_offset;

	private Vector3 m_rotationInterval;

	private Vector3 m_startingPosition;

	private Vector3 m_startingRotation;

	private void Start()
	{
		Init();
	}

	private void OnEnable()
	{
		InitTransforms();
	}

	private void Update()
	{
		float num = Time.time * speed;
		Vector3 vector = default(Vector3);
		vector.x = Mathf.Sin(num * m_interval.x + m_offset.x) * magnitude.x * m_interval.x;
		vector.y = Mathf.Sin(num * m_interval.y + m_offset.y) * magnitude.y * m_interval.y;
		vector.z = Mathf.Sin(num * m_interval.z + m_offset.z) * magnitude.z * m_interval.z;
		Vector3 vector2 = Vector3.Lerp(m_startingPosition, m_startingPosition + vector, positionBlend);
		if (localSpace)
		{
			base.transform.localPosition = vector2;
		}
		else
		{
			base.transform.position = vector2;
		}
		Vector3 vector3 = default(Vector3);
		vector3.x = Mathf.Sin(num * m_rotationInterval.x + m_offset.x) * magnitudeRot.x * m_rotationInterval.x;
		vector3.y = Mathf.Sin(num * m_rotationInterval.y + m_offset.y) * magnitudeRot.y * m_rotationInterval.y;
		vector3.z = Mathf.Sin(num * m_rotationInterval.z + m_offset.z) * magnitudeRot.z * m_rotationInterval.z;
		base.transform.eulerAngles = Vector3.Lerp(m_startingRotation, m_startingRotation + vector3, rotationBlend);
	}

	private void InitTransforms()
	{
		if (localSpace)
		{
			m_startingPosition = base.transform.localPosition;
		}
		else
		{
			m_startingPosition = base.transform.position;
		}
		m_startingRotation = base.transform.eulerAngles;
	}

	private void Init()
	{
		InitTransforms();
		m_interval.x = Random.Range(frequencyMin, frequencyMax);
		m_interval.y = Random.Range(frequencyMin, frequencyMax);
		m_interval.z = Random.Range(frequencyMin, frequencyMax);
		m_offset.x = 0.5f * Random.Range(0f - m_interval.x, m_interval.x);
		m_offset.y = 0.5f * Random.Range(0f - m_interval.y, m_interval.y);
		m_offset.z = 0.5f * Random.Range(0f - m_interval.z, m_interval.z);
		m_rotationInterval.x = Random.Range(frequencyMinRot, frequencyMaxRot);
		m_rotationInterval.y = Random.Range(frequencyMinRot, frequencyMaxRot);
		m_rotationInterval.z = Random.Range(frequencyMinRot, frequencyMaxRot);
	}
}
