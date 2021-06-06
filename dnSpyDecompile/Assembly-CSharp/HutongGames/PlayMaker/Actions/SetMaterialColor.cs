using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000DE7 RID: 3559
	[ActionCategory(ActionCategory.Material)]
	[Tooltip("Sets a named color value in a game object's material.")]
	public class SetMaterialColor : ComponentAction<Renderer>
	{
		// Token: 0x0600A658 RID: 42584 RVA: 0x00348DD0 File Offset: 0x00346FD0
		public override void Reset()
		{
			this.gameObject = null;
			this.materialIndex = 0;
			this.material = null;
			this.namedColor = "_Color";
			this.color = Color.black;
			this.everyFrame = false;
		}

		// Token: 0x0600A659 RID: 42585 RVA: 0x00348E1E File Offset: 0x0034701E
		public override void OnEnter()
		{
			this.DoSetMaterialColor();
			if (!this.everyFrame)
			{
				base.Finish();
			}
		}

		// Token: 0x0600A65A RID: 42586 RVA: 0x00348E34 File Offset: 0x00347034
		public override void OnUpdate()
		{
			this.DoSetMaterialColor();
		}

		// Token: 0x0600A65B RID: 42587 RVA: 0x00348E3C File Offset: 0x0034703C
		private void DoSetMaterialColor()
		{
			if (this.color.IsNone)
			{
				return;
			}
			string text = this.namedColor.Value;
			if (text == "")
			{
				text = "_Color";
			}
			if (this.material.Value != null)
			{
				this.material.Value.SetColor(text, this.color.Value);
				return;
			}
			GameObject ownerDefaultTarget = base.Fsm.GetOwnerDefaultTarget(this.gameObject);
			if (!base.UpdateCache(ownerDefaultTarget))
			{
				return;
			}
			Material material = base.renderer.GetMaterial();
			if (material == null)
			{
				base.LogError("Missing Material!");
				return;
			}
			if (this.materialIndex.Value == 0)
			{
				material.SetColor(text, this.color.Value);
				return;
			}
			if (base.renderer.GetMaterials().Count > this.materialIndex.Value)
			{
				base.renderer.GetMaterial(this.materialIndex.Value).SetColor(text, this.color.Value);
			}
		}

		// Token: 0x04008CD9 RID: 36057
		[Tooltip("The GameObject that the material is applied to.")]
		[CheckForComponent(typeof(Renderer))]
		public FsmOwnerDefault gameObject;

		// Token: 0x04008CDA RID: 36058
		[Tooltip("GameObjects can have multiple materials. Specify an index to target a specific material.")]
		public FsmInt materialIndex;

		// Token: 0x04008CDB RID: 36059
		[Tooltip("Alternatively specify a Material instead of a GameObject and Index.")]
		public FsmMaterial material;

		// Token: 0x04008CDC RID: 36060
		[UIHint(UIHint.NamedColor)]
		[Tooltip("A named color parameter in the shader.")]
		public FsmString namedColor;

		// Token: 0x04008CDD RID: 36061
		[RequiredField]
		[Tooltip("Set the parameter value.")]
		public FsmColor color;

		// Token: 0x04008CDE RID: 36062
		[Tooltip("Repeat every frame. Useful if the value is animated.")]
		public bool everyFrame;
	}
}
