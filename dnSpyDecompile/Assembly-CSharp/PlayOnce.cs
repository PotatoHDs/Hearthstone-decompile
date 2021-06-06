using System;
using UnityEngine;

// Token: 0x02000A61 RID: 2657
public class PlayOnce : MonoBehaviour
{
	// Token: 0x06008ECA RID: 36554 RVA: 0x002E17F4 File Offset: 0x002DF9F4
	private void Start()
	{
		if (this.tester != null)
		{
			this.tester.SetActive(false);
		}
		if (this.tester2 != null)
		{
			this.tester2.SetActive(false);
		}
		if (this.tester3 != null)
		{
			this.tester3.SetActive(false);
		}
	}

	// Token: 0x06008ECB RID: 36555 RVA: 0x002E1850 File Offset: 0x002DFA50
	private void OnGUI()
	{
		if (Event.current.isKey)
		{
			if (this.tester != null)
			{
				this.tester.SetActive(true);
				this.tester.GetComponent<Animation>().Stop(this.testerAnim);
				this.tester.GetComponent<Animation>().Play(this.testerAnim);
			}
			else
			{
				Debug.Log("NO 'tester' object.");
			}
			if (this.tester2 != null)
			{
				this.tester2.SetActive(true);
				this.tester2.GetComponent<Animation>().Stop(this.tester2Anim);
				this.tester2.GetComponent<Animation>().Play(this.tester2Anim);
			}
			else
			{
				Debug.Log("NO 'tester2' object.");
			}
			if (this.tester3 != null)
			{
				this.tester3.SetActive(true);
				this.tester3.GetComponent<Animation>().Stop(this.tester3Anim);
				this.tester3.GetComponent<Animation>().Play(this.tester3Anim);
				return;
			}
			Debug.Log("NO 'tester3' object.");
		}
	}

	// Token: 0x06008ECC RID: 36556 RVA: 0x00003BE8 File Offset: 0x00001DE8
	private void Update()
	{
	}

	// Token: 0x0400771F RID: 30495
	public string notes;

	// Token: 0x04007720 RID: 30496
	public string notes2;

	// Token: 0x04007721 RID: 30497
	public GameObject tester;

	// Token: 0x04007722 RID: 30498
	public string testerAnim;

	// Token: 0x04007723 RID: 30499
	public GameObject tester2;

	// Token: 0x04007724 RID: 30500
	public string tester2Anim;

	// Token: 0x04007725 RID: 30501
	public GameObject tester3;

	// Token: 0x04007726 RID: 30502
	public string tester3Anim;
}
