using System;

namespace Hearthstone.UI.Scripting
{
	// Token: 0x02001050 RID: 4176
	public struct DynamicValue
	{
		// Token: 0x170009DD RID: 2525
		// (get) Token: 0x0600B4FB RID: 46331 RVA: 0x0037A6B2 File Offset: 0x003788B2
		public bool HasValidValue
		{
			get
			{
				return this.ValueType != null;
			}
		}

		// Token: 0x0600B4FC RID: 46332 RVA: 0x0037A6C0 File Offset: 0x003788C0
		public DynamicValue(object value, Type valueType)
		{
			this.Value = value;
			this.ValueType = ((valueType == null && value != null) ? value.GetType() : valueType);
		}

		// Token: 0x04009706 RID: 38662
		public readonly object Value;

		// Token: 0x04009707 RID: 38663
		public readonly Type ValueType;
	}
}
