using UnityEngine;

public class PlayOnce : MonoBehaviour
{
	public string notes;

	public string notes2;

	public GameObject tester;

	public string testerAnim;

	public GameObject tester2;

	public string tester2Anim;

	public GameObject tester3;

	public string tester3Anim;

	private void Start()
	{
		if (tester != null)
		{
			tester.SetActive(value: false);
		}
		if (tester2 != null)
		{
			tester2.SetActive(value: false);
		}
		if (tester3 != null)
		{
			tester3.SetActive(value: false);
		}
	}

	private void OnGUI()
	{
		if (Event.current.isKey)
		{
			if (tester != null)
			{
				tester.SetActive(value: true);
				tester.GetComponent<Animation>().Stop(testerAnim);
				tester.GetComponent<Animation>().Play(testerAnim);
			}
			else
			{
				Debug.Log("NO 'tester' object.");
			}
			if (tester2 != null)
			{
				tester2.SetActive(value: true);
				tester2.GetComponent<Animation>().Stop(tester2Anim);
				tester2.GetComponent<Animation>().Play(tester2Anim);
			}
			else
			{
				Debug.Log("NO 'tester2' object.");
			}
			if (tester3 != null)
			{
				tester3.SetActive(value: true);
				tester3.GetComponent<Animation>().Stop(tester3Anim);
				tester3.GetComponent<Animation>().Play(tester3Anim);
			}
			else
			{
				Debug.Log("NO 'tester3' object.");
			}
		}
	}

	private void Update()
	{
	}
}
