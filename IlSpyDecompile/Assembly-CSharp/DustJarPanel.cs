using UnityEngine;

[CustomEditClass]
public class DustJarPanel : MonoBehaviour
{
	[CustomEditField(Sections = "Dust Panel")]
	public GameObject m_dustJar;

	[CustomEditField(Sections = "Dust Panel")]
	public UberText m_dustCount;

	[CustomEditField(Sections = "Dust Panel")]
	public AudioSource m_dustJarEntranceSound;

	public void Show(int dustAmount)
	{
		m_dustCount.Text = dustAmount.ToString();
		Vector3 localScale = m_dustJar.transform.localScale;
		m_dustJar.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
		iTween.ScaleTo(m_dustJar.gameObject, iTween.Hash("scale", localScale, "time", 0.5f, "easetype", iTween.EaseType.easeOutElastic));
		if (m_dustJarEntranceSound != null)
		{
			SoundManager.Get().Play(Object.Instantiate(m_dustJarEntranceSound, base.transform));
		}
	}
}
