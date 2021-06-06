using System;
using HutongGames.PlayMaker;
using UnityEngine;

// Token: 0x02000760 RID: 1888
[ActionCategory("Pegasus")]
[HutongGames.PlayMaker.Tooltip("Get the object being rendered to from RenderToTexture")]
public class GetRenderToTextureRenderObject : FsmStateAction
{
	// Token: 0x060069EE RID: 27118 RVA: 0x00228A96 File Offset: 0x00226C96
	[HutongGames.PlayMaker.Tooltip("Get the object being rendered to from RenderToTexture. This is used to get the procedurally generated render plane object.")]
	public override void Reset()
	{
		this.gameObject = null;
		this.renderObject = null;
	}

	// Token: 0x060069EF RID: 27119 RVA: 0x00228AA6 File Offset: 0x00226CA6
	public override void OnEnter()
	{
		this.DoGetObject();
		base.Finish();
	}

	// Token: 0x060069F0 RID: 27120 RVA: 0x00228AB4 File Offset: 0x00226CB4
	private void DoGetObject()
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
		this.renderObject.Value = component.GetRenderToObject();
	}

	// Token: 0x040056C2 RID: 22210
	[RequiredField]
	[CheckForComponent(typeof(RenderToTexture))]
	public FsmOwnerDefault gameObject;

	// Token: 0x040056C3 RID: 22211
	[RequiredField]
	[UIHint(UIHint.Variable)]
	public FsmGameObject renderObject;
}
