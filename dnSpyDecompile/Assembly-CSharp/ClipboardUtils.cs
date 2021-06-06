using System;
using UnityEngine;

// Token: 0x020009A6 RID: 2470
public static class ClipboardUtils
{
	// Token: 0x060086DF RID: 34527 RVA: 0x002B8BE1 File Offset: 0x002B6DE1
	public static void CopyToClipboard(string copyText)
	{
		GUIUtility.systemCopyBuffer = copyText;
	}

	// Token: 0x1700078D RID: 1933
	// (get) Token: 0x060086E0 RID: 34528 RVA: 0x002B8BEC File Offset: 0x002B6DEC
	public static string PastedStringFromClipboard
	{
		get
		{
			TextEditor textEditor = new TextEditor
			{
				multiline = true
			};
			if (!textEditor.CanPaste())
			{
				return null;
			}
			textEditor.Paste();
			return textEditor.text;
		}
	}
}
