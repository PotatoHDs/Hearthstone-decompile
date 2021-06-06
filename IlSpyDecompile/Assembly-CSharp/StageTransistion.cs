using UnityEngine;

public class StageTransistion : MonoBehaviour
{
	public GameObject hlBase;

	public GameObject hlEdge;

	public GameObject entireObj;

	public GameObject inplayObj;

	public GameObject rays;

	public GameObject flash;

	public GameObject fxEmitterA;

	public GameObject fxEmitterB;

	public float FxEmitterAKillTime = 1f;

	private Shader shaderBucket;

	private bool colorchange;

	private bool powerchange;

	private bool amountchange;

	private bool turnon;

	private bool rayschange;

	private bool flashchange;

	public Color endColor;

	public Color flashendColor;

	private int stage;

	public float RayTime = 10f;

	public float fxATime = 1f;

	public float FxEmitterAWaitTime = 1f;

	public float FxEmitterATimer = 2f;

	private bool FxStartAnim;

	private bool FxStartStop;

	private bool fxEmitterAScale;

	private bool raysdone;

	private Renderer m_hlBaseRenderer;

	private Renderer hlEdgeRenderer;

	private void Start()
	{
		rays.SetActive(value: false);
		flash.SetActive(value: false);
		entireObj.SetActive(value: true);
		inplayObj.SetActive(value: false);
		m_hlBaseRenderer = hlBase.GetComponent<Renderer>();
		hlEdgeRenderer = hlEdge.GetComponent<Renderer>();
		m_hlBaseRenderer.GetMaterial().SetFloat("_Amount", 0f);
		hlEdgeRenderer.GetMaterial().SetFloat("_Amount", 0f);
	}

	private void OnGUI()
	{
		if (Event.current.isKey)
		{
			amountchange = true;
		}
	}

	private void OnMouseEnter()
	{
		if (!FxStartAnim)
		{
			FxStartStop = false;
			FxStartAnim = true;
			powerchange = true;
			fxEmitterAScale = true;
		}
	}

	private void OnMouseExit()
	{
		if (!FxStartStop)
		{
			FxStartAnim = false;
			FxStartStop = true;
		}
	}

	private void OnMouseDown()
	{
		switch (stage)
		{
		case 0:
			ManaUse();
			break;
		case 1:
			RaysOn();
			break;
		}
		stage++;
	}

	private void RaysOn()
	{
		rays.SetActive(value: true);
		flash.SetActive(value: true);
		rayschange = true;
	}

	private void ManaUse()
	{
		colorchange = true;
	}

	private void Update()
	{
		Material material = hlEdgeRenderer.GetMaterial();
		Material material2 = m_hlBaseRenderer.GetMaterial();
		if (amountchange)
		{
			float num = Time.deltaTime / 0.5f;
			float num2 = num * 0.6954f;
			float num3 = num * 0.6954f;
			float num4 = material.GetFloat("_Amount") + num3;
			Debug.Log("amount edge " + num4);
			material2.SetFloat("_Amount", material2.GetFloat("_Amount") + num2);
			if (material2.GetFloat("_Amount") >= 0.6954f)
			{
				amountchange = false;
			}
			material.SetFloat("_Amount", material.GetFloat("_Amount") + num3);
		}
		if (colorchange)
		{
			float t = Time.deltaTime / 0.5f;
			Color color = material2.color;
			material2.color = Color.Lerp(color, endColor, t);
		}
		if (powerchange)
		{
			float num5 = Time.deltaTime / 0.5f;
			float num6 = num5 * 18f;
			float num7 = num5 * 0.6954f;
			material2.SetFloat("_power", material2.GetFloat("_power") + num6);
			if (material2.GetFloat("_power") >= 29f)
			{
				powerchange = false;
			}
			material2.SetFloat("_Amount", material2.GetFloat("_Amount") + num7);
			if (material2.GetFloat("_Amount") >= 1.12f)
			{
				amountchange = false;
			}
		}
		if (rayschange)
		{
			float y = Time.deltaTime / 0.5f * RayTime;
			rays.transform.localScale += new Vector3(0f, y, 0f);
			if (!raysdone && rays.transform.localScale.y >= 20f)
			{
				rays.SetActive(value: false);
				GetComponent<Renderer>().enabled = false;
				inplayObj.SetActive(value: true);
				inplayObj.GetComponent<Animation>().Play();
				fxEmitterA.SetActive(value: false);
				raysdone = true;
			}
		}
		if (raysdone)
		{
			Material material3 = flash.GetComponent<Renderer>().GetMaterial();
			float num8 = material3.GetFloat("_InvFade") - Time.deltaTime;
			material3.SetFloat("_InvFade", num8);
			Debug.Log("InvFade " + num8);
			if (num8 <= 0.01f)
			{
				entireObj.SetActive(value: false);
			}
		}
		if (fxEmitterAScale)
		{
			float num9 = Time.deltaTime / 0.5f * fxATime;
			fxEmitterA.transform.localScale += new Vector3(num9, num9, num9);
		}
	}
}
