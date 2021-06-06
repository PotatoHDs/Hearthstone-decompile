using UnityEngine;

public class RandomTransform : MonoBehaviour
{
	public bool m_applyOnStart;

	public Vector3 positionMin;

	public Vector3 positionMax;

	public Vector3 rotationMin;

	public Vector3 rotationMax;

	public Vector3 scaleMin = Vector3.one;

	public Vector3 scaleMax = Vector3.one;

	public void Start()
	{
		if (m_applyOnStart)
		{
			Apply();
		}
	}

	public void Apply()
	{
		Vector3 vector = new Vector3(Random.Range(positionMin.x, positionMax.x), Random.Range(positionMin.y, positionMax.y), Random.Range(positionMin.z, positionMax.z));
		Vector3 localPosition = base.transform.localPosition + vector;
		base.transform.localPosition = localPosition;
		Vector3 vector2 = new Vector3(Random.Range(rotationMin.x, rotationMax.x), Random.Range(rotationMin.y, rotationMax.y), Random.Range(rotationMin.z, rotationMax.z));
		Vector3 localEulerAngles = base.transform.localEulerAngles + vector2;
		base.transform.localEulerAngles = localEulerAngles;
		Vector3 vector3 = new Vector3(Random.Range(scaleMin.x, scaleMax.x), Random.Range(scaleMin.y, scaleMax.y), Random.Range(scaleMin.z, scaleMax.z));
		Vector3 localScale = vector3;
		vector3.Scale(base.transform.localScale);
		base.transform.localScale = localScale;
	}
}
