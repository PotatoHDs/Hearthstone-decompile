using UnityEngine;

public class BounceScale : MonoBehaviour
{
	public float m_Time;

	public void BounceyScale()
	{
		Vector3 localScale = base.transform.localScale;
		base.transform.localScale = Vector3.zero;
		iTween.ScaleTo(base.gameObject, iTween.Hash("scale", localScale, "time", m_Time, "easetype", iTween.EaseType.easeOutElastic));
	}
}
