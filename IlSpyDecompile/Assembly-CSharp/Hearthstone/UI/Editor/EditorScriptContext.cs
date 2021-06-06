using System;
using Hearthstone.UI.Scripting;

namespace Hearthstone.UI.Editor
{
	public class EditorScriptContext : ScriptContext
	{
		public EditorScriptContext()
		{
			EnableEditMode(() => EditorDataContextProvider.GlobalDataModels, (Type type) => Activator.CreateInstance(type) as IDataModel);
		}
	}
}
