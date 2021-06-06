using UnityEngine;

public class StageTester : MonoBehaviour
{
	public GameObject highlightBase;

	public GameObject highlightEdge;

	public GameObject entireObj;

	public GameObject inplayObj;

	public GameObject rays;

	public GameObject flash;

	public GameObject fxEmitterA;

	public GameObject fxEmitterB;

	private int stage;

	private void Start()
	{
	}

	private void OnMouseDown()
	{
		switch (stage)
		{
		case 0:
			Highlighted();
			break;
		case 1:
			Selected();
			break;
		case 2:
			ManaUsed();
			break;
		case 3:
			Released();
			break;
		}
		stage++;
	}

	private void Highlighted()
	{
		highlightBase.GetComponent<Animation>().Play();
		highlightEdge.GetComponent<Animation>().Play();
	}

	private void Selected()
	{
		highlightBase.GetComponent<Animation>().CrossFade("AllyInHandActiveBaseSelected", 0.3f);
		fxEmitterA.GetComponent<Animation>().Play();
	}

	private void ManaUsed()
	{
		highlightBase.GetComponent<Animation>().CrossFade("AllyInHandActiveBaseMana", 0.3f);
		fxEmitterA.GetComponent<Animation>().CrossFade("AllyInHandFXUnHighlight", 0.3f);
	}

	private void Released()
	{
		rays.GetComponent<Animation>().Play("AllyInHandRaysUp");
		flash.GetComponent<Animation>().Play("AllyInHandGlowOn");
		entireObj.GetComponent<Animation>().Play("AllyInHandDeath");
		inplayObj.GetComponent<Animation>().Play("AllyInPlaySpawn");
	}

	private void Update()
	{
	}
}
