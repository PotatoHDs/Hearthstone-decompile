using UnityEngine;

public class LossMarks : MonoBehaviour
{
	public void Init(int numMarks)
	{
		for (int i = 0; i < base.transform.childCount; i++)
		{
			base.transform.GetChild(i).gameObject.SetActive(numMarks > 0);
			numMarks--;
		}
	}

	public void SetNumMarked(int numMarked)
	{
		for (int i = 0; i < base.transform.childCount; i++)
		{
			base.transform.GetChild(i).GetChild(0).gameObject.SetActive(numMarked > 0);
			numMarked--;
		}
	}
}
