using System;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000CA1 RID: 3233
	[Tooltip("GUILayout base action - don't use!")]
	public abstract class GUILayoutAction : FsmStateAction
	{
		// Token: 0x170008C7 RID: 2247
		// (get) Token: 0x0600A04C RID: 41036 RVA: 0x00330A18 File Offset: 0x0032EC18
		public GUILayoutOption[] LayoutOptions
		{
			get
			{
				if (this.options == null)
				{
					this.options = new GUILayoutOption[this.layoutOptions.Length];
					for (int i = 0; i < this.layoutOptions.Length; i++)
					{
						this.options[i] = this.layoutOptions[i].GetGUILayoutOption();
					}
				}
				return this.options;
			}
		}

		// Token: 0x0600A04D RID: 41037 RVA: 0x00330A6E File Offset: 0x0032EC6E
		public override void Reset()
		{
			this.layoutOptions = new LayoutOption[0];
		}

		// Token: 0x040085CE RID: 34254
		public LayoutOption[] layoutOptions;

		// Token: 0x040085CF RID: 34255
		private GUILayoutOption[] options;
	}
}
