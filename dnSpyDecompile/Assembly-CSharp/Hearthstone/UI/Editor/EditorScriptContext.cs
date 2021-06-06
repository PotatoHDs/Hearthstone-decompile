using System;
using Hearthstone.UI.Scripting;

namespace Hearthstone.UI.Editor
{
	// Token: 0x02001060 RID: 4192
	public class EditorScriptContext : ScriptContext
	{
		// Token: 0x0600B54D RID: 46413 RVA: 0x0037BE64 File Offset: 0x0037A064
		public EditorScriptContext()
		{
			base.EnableEditMode(() => EditorDataContextProvider.GlobalDataModels, (Type type) => Activator.CreateInstance(type) as IDataModel);
		}
	}
}
