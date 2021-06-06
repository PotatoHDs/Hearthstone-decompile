using System;
using UnityEngine;

namespace Hearthstone.UI
{
	// Token: 0x0200102F RID: 4143
	public class WidgetBehaviorStateEnumAttribute : PropertyAttribute
	{
		// Token: 0x0600B3B7 RID: 46007 RVA: 0x00374700 File Offset: 0x00372900
		public WidgetBehaviorStateEnumAttribute(Type stateEnum, string propertyName = "")
		{
			this.StateEnum = stateEnum;
			this.PropertyName = propertyName;
		}

		// Token: 0x0400969D RID: 38557
		public Type StateEnum;

		// Token: 0x0400969E RID: 38558
		public string PropertyName;
	}
}
