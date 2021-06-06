using System;
using UnityEngine;

// Token: 0x02000A98 RID: 2712
public class StageTransistion : MonoBehaviour
{
	// Token: 0x060090C3 RID: 37059 RVA: 0x002EF2FC File Offset: 0x002ED4FC
	private void Start()
	{
		this.rays.SetActive(false);
		this.flash.SetActive(false);
		this.entireObj.SetActive(true);
		this.inplayObj.SetActive(false);
		this.m_hlBaseRenderer = this.hlBase.GetComponent<Renderer>();
		this.hlEdgeRenderer = this.hlEdge.GetComponent<Renderer>();
		this.m_hlBaseRenderer.GetMaterial().SetFloat("_Amount", 0f);
		this.hlEdgeRenderer.GetMaterial().SetFloat("_Amount", 0f);
	}

	// Token: 0x060090C4 RID: 37060 RVA: 0x002EF38F File Offset: 0x002ED58F
	private void OnGUI()
	{
		if (Event.current.isKey)
		{
			this.amountchange = true;
		}
	}

	// Token: 0x060090C5 RID: 37061 RVA: 0x002EF3A4 File Offset: 0x002ED5A4
	private void OnMouseEnter()
	{
		if (this.FxStartAnim)
		{
			return;
		}
		this.FxStartStop = false;
		this.FxStartAnim = true;
		this.powerchange = true;
		this.fxEmitterAScale = true;
	}

	// Token: 0x060090C6 RID: 37062 RVA: 0x002EF3CB File Offset: 0x002ED5CB
	private void OnMouseExit()
	{
		if (this.FxStartStop)
		{
			return;
		}
		this.FxStartAnim = false;
		this.FxStartStop = true;
	}

	// Token: 0x060090C7 RID: 37063 RVA: 0x002EF3E4 File Offset: 0x002ED5E4
	private void OnMouseDown()
	{
		int num = this.stage;
		if (num != 0)
		{
			if (num == 1)
			{
				this.RaysOn();
			}
		}
		else
		{
			this.ManaUse();
		}
		this.stage++;
	}

	// Token: 0x060090C8 RID: 37064 RVA: 0x002EF41D File Offset: 0x002ED61D
	private void RaysOn()
	{
		this.rays.SetActive(true);
		this.flash.SetActive(true);
		this.rayschange = true;
	}

	// Token: 0x060090C9 RID: 37065 RVA: 0x002EF43E File Offset: 0x002ED63E
	private void ManaUse()
	{
		this.colorchange = true;
	}

	// Token: 0x060090CA RID: 37066 RVA: 0x002EF448 File Offset: 0x002ED648
	private void Update()
	{
		Material material = this.hlEdgeRenderer.GetMaterial();
		Material material2 = this.m_hlBaseRenderer.GetMaterial();
		if (this.amountchange)
		{
			float num = Time.deltaTime / 0.5f;
			float num2 = num * 0.6954f;
			float num3 = num * 0.6954f;
			float num4 = material.GetFloat("_Amount") + num3;
			Debug.Log("amount edge " + num4);
			material2.SetFloat("_Amount", material2.GetFloat("_Amount") + num2);
			if (material2.GetFloat("_Amount") >= 0.6954f)
			{
				this.amountchange = false;
			}
			material.SetFloat("_Amount", material.GetFloat("_Amount") + num3);
		}
		if (this.colorchange)
		{
			float t = Time.deltaTime / 0.5f;
			Color color = material2.color;
			material2.color = Color.Lerp(color, this.endColor, t);
		}
		if (this.powerchange)
		{
			float num5 = Time.deltaTime / 0.5f;
			float num6 = num5 * 18f;
			float num7 = num5 * 0.6954f;
			material2.SetFloat("_power", material2.GetFloat("_power") + num6);
			if (material2.GetFloat("_power") >= 29f)
			{
				this.powerchange = false;
			}
			material2.SetFloat("_Amount", material2.GetFloat("_Amount") + num7);
			if (material2.GetFloat("_Amount") >= 1.12f)
			{
				this.amountchange = false;
			}
		}
		if (this.rayschange)
		{
			float y = Time.deltaTime / 0.5f * this.RayTime;
			this.rays.transform.localScale += new Vector3(0f, y, 0f);
			if (!this.raysdone && this.rays.transform.localScale.y >= 20f)
			{
				this.rays.SetActive(false);
				base.GetComponent<Renderer>().enabled = false;
				this.inplayObj.SetActive(true);
				this.inplayObj.GetComponent<Animation>().Play();
				this.fxEmitterA.SetActive(false);
				this.raysdone = true;
			}
		}
		if (this.raysdone)
		{
			Material material3 = this.flash.GetComponent<Renderer>().GetMaterial();
			float num8 = material3.GetFloat("_InvFade") - Time.deltaTime;
			material3.SetFloat("_InvFade", num8);
			Debug.Log("InvFade " + num8);
			if (num8 <= 0.01f)
			{
				this.entireObj.SetActive(false);
			}
		}
		if (this.fxEmitterAScale)
		{
			float num9 = Time.deltaTime / 0.5f * this.fxATime;
			this.fxEmitterA.transform.localScale += new Vector3(num9, num9, num9);
		}
	}

	// Token: 0x0400799B RID: 31131
	public GameObject hlBase;

	// Token: 0x0400799C RID: 31132
	public GameObject hlEdge;

	// Token: 0x0400799D RID: 31133
	public GameObject entireObj;

	// Token: 0x0400799E RID: 31134
	public GameObject inplayObj;

	// Token: 0x0400799F RID: 31135
	public GameObject rays;

	// Token: 0x040079A0 RID: 31136
	public GameObject flash;

	// Token: 0x040079A1 RID: 31137
	public GameObject fxEmitterA;

	// Token: 0x040079A2 RID: 31138
	public GameObject fxEmitterB;

	// Token: 0x040079A3 RID: 31139
	public float FxEmitterAKillTime = 1f;

	// Token: 0x040079A4 RID: 31140
	private Shader shaderBucket;

	// Token: 0x040079A5 RID: 31141
	private bool colorchange;

	// Token: 0x040079A6 RID: 31142
	private bool powerchange;

	// Token: 0x040079A7 RID: 31143
	private bool amountchange;

	// Token: 0x040079A8 RID: 31144
	private bool turnon;

	// Token: 0x040079A9 RID: 31145
	private bool rayschange;

	// Token: 0x040079AA RID: 31146
	private bool flashchange;

	// Token: 0x040079AB RID: 31147
	public Color endColor;

	// Token: 0x040079AC RID: 31148
	public Color flashendColor;

	// Token: 0x040079AD RID: 31149
	private int stage;

	// Token: 0x040079AE RID: 31150
	public float RayTime = 10f;

	// Token: 0x040079AF RID: 31151
	public float fxATime = 1f;

	// Token: 0x040079B0 RID: 31152
	public float FxEmitterAWaitTime = 1f;

	// Token: 0x040079B1 RID: 31153
	public float FxEmitterATimer = 2f;

	// Token: 0x040079B2 RID: 31154
	private bool FxStartAnim;

	// Token: 0x040079B3 RID: 31155
	private bool FxStartStop;

	// Token: 0x040079B4 RID: 31156
	private bool fxEmitterAScale;

	// Token: 0x040079B5 RID: 31157
	private bool raysdone;

	// Token: 0x040079B6 RID: 31158
	private Renderer m_hlBaseRenderer;

	// Token: 0x040079B7 RID: 31159
	private Renderer hlEdgeRenderer;
}
