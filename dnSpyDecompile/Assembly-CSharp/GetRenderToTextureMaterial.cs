using System;
using HutongGames.PlayMaker;
using UnityEngine;

// Token: 0x0200075F RID: 1887
[ActionCategory("Pegasus")]
[HutongGames.PlayMaker.Tooltip("Get the material instance from an object with RenderToTexture")]
public class GetRenderToTextureMaterial : FsmStateAction
{
	// Token: 0x060069EA RID: 27114 RVA: 0x00228A1F File Offset: 0x00226C1F
	[HutongGames.PlayMaker.Tooltip("Get the material instance from an object with RenderToTexture. This is used to get the material of the procedurally generated render plane.")]
	public override void Reset()
	{
		this.gameObject = null;
		this.material = null;
	}

	// Token: 0x060069EB RID: 27115 RVA: 0x00228A2F File Offset: 0x00226C2F
	public override void OnEnter()
	{
		this.DoGetMaterial();
		base.Finish();
	}

	// Token: 0x060069EC RID: 27116 RVA: 0x00228A40 File Offset: 0x00226C40
	private void DoGetMaterial()
	{
		GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
		if (ownerDefaultTarget == null)
		{
			return;
		}
		RenderToTexture component = ownerDefaultTarget.GetComponent<RenderToTexture>();
		if (component == null)
		{
			base.LogError("Missing RenderToTexture component!");
			return;
		}
		this.material.Value = component.GetRenderMaterial();
	}

	// Token: 0x040056C0 RID: 22208
	[RequiredField]
	[CheckForComponent(typeof(RenderToTexture))]
	public FsmOwnerDefault gameObject;

	// Token: 0x040056C1 RID: 22209
	[RequiredField]
	[UIHint(UIHint.Variable)]
	public FsmMaterial material;
}
