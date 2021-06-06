using HutongGames.PlayMaker;
using UnityEngine;

[ActionCategory("Pegasus")]
[HutongGames.PlayMaker.Tooltip("Get the material instance from an object with RenderToTexture")]
public class GetRenderToTextureMaterial : FsmStateAction
{
	[RequiredField]
	[CheckForComponent(typeof(RenderToTexture))]
	public FsmOwnerDefault gameObject;

	[RequiredField]
	[UIHint(UIHint.Variable)]
	public FsmMaterial material;

	[HutongGames.PlayMaker.Tooltip("Get the material instance from an object with RenderToTexture. This is used to get the material of the procedurally generated render plane.")]
	public override void Reset()
	{
		gameObject = null;
		material = null;
	}

	public override void OnEnter()
	{
		DoGetMaterial();
		Finish();
	}

	private void DoGetMaterial()
	{
		GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(gameObject);
		if (!(ownerDefaultTarget == null))
		{
			RenderToTexture component = ownerDefaultTarget.GetComponent<RenderToTexture>();
			if (component == null)
			{
				LogError("Missing RenderToTexture component!");
			}
			else
			{
				material.Value = component.GetRenderMaterial();
			}
		}
	}
}
