using System;
using UnityEngine;

namespace Hearthstone.UI
{
	// Token: 0x0200102C RID: 4140
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	public class WidgetBehaviorDescriptionAttribute : PropertyAttribute
	{
		// Token: 0x0400967E RID: 38526
		public string Path;

		// Token: 0x0400967F RID: 38527
		public string UniqueWithinCategory = "";
	}
}
