using System;

namespace Hearthstone.UI.Scripting
{
	// Token: 0x02001049 RID: 4169
	public abstract class ScriptSyntaxTreeRule<T> : ScriptSyntaxTreeRule where T : ScriptSyntaxTreeRule, new()
	{
		// Token: 0x0600B4E0 RID: 46304 RVA: 0x00379F85 File Offset: 0x00378185
		public static ScriptSyntaxTreeRule Get()
		{
			ScriptSyntaxTreeRule result;
			if ((result = ScriptSyntaxTreeRule<T>.s_instance) == null)
			{
				result = (ScriptSyntaxTreeRule<T>.s_instance = Activator.CreateInstance<T>());
			}
			return result;
		}

		// Token: 0x040096F8 RID: 38648
		private static ScriptSyntaxTreeRule s_instance;
	}
}
