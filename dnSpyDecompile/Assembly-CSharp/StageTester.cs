using System;
using UnityEngine;

// Token: 0x02000A97 RID: 2711
public class StageTester : MonoBehaviour
{
	// Token: 0x060090BB RID: 37051 RVA: 0x00003BE8 File Offset: 0x00001DE8
	private void Start()
	{
	}

	// Token: 0x060090BC RID: 37052 RVA: 0x002EF1B4 File Offset: 0x002ED3B4
	private void OnMouseDown()
	{
		switch (this.stage)
		{
		case 0:
			this.Highlighted();
			break;
		case 1:
			this.Selected();
			break;
		case 2:
			this.ManaUsed();
			break;
		case 3:
			this.Released();
			break;
		}
		this.stage++;
	}

	// Token: 0x060090BD RID: 37053 RVA: 0x002EF20C File Offset: 0x002ED40C
	private void Highlighted()
	{
		this.highlightBase.GetComponent<Animation>().Play();
		this.highlightEdge.GetComponent<Animation>().Play();
	}

	// Token: 0x060090BE RID: 37054 RVA: 0x002EF230 File Offset: 0x002ED430
	private void Selected()
	{
		this.highlightBase.GetComponent<Animation>().CrossFade("AllyInHandActiveBaseSelected", 0.3f);
		this.fxEmitterA.GetComponent<Animation>().Play();
	}

	// Token: 0x060090BF RID: 37055 RVA: 0x002EF25D File Offset: 0x002ED45D
	private void ManaUsed()
	{
		this.highlightBase.GetComponent<Animation>().CrossFade("AllyInHandActiveBaseMana", 0.3f);
		this.fxEmitterA.GetComponent<Animation>().CrossFade("AllyInHandFXUnHighlight", 0.3f);
	}

	// Token: 0x060090C0 RID: 37056 RVA: 0x002EF294 File Offset: 0x002ED494
	private void Released()
	{
		this.rays.GetComponent<Animation>().Play("AllyInHandRaysUp");
		this.flash.GetComponent<Animation>().Play("AllyInHandGlowOn");
		this.entireObj.GetComponent<Animation>().Play("AllyInHandDeath");
		this.inplayObj.GetComponent<Animation>().Play("AllyInPlaySpawn");
	}

	// Token: 0x060090C1 RID: 37057 RVA: 0x00003BE8 File Offset: 0x00001DE8
	private void Update()
	{
	}

	// Token: 0x04007992 RID: 31122
	public GameObject highlightBase;

	// Token: 0x04007993 RID: 31123
	public GameObject highlightEdge;

	// Token: 0x04007994 RID: 31124
	public GameObject entireObj;

	// Token: 0x04007995 RID: 31125
	public GameObject inplayObj;

	// Token: 0x04007996 RID: 31126
	public GameObject rays;

	// Token: 0x04007997 RID: 31127
	public GameObject flash;

	// Token: 0x04007998 RID: 31128
	public GameObject fxEmitterA;

	// Token: 0x04007999 RID: 31129
	public GameObject fxEmitterB;

	// Token: 0x0400799A RID: 31130
	private int stage;
}
