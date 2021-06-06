using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

// Token: 0x020002DA RID: 730
public class FatalErrorDialog : MonoBehaviour
{
	// Token: 0x170004C9 RID: 1225
	// (get) Token: 0x06002648 RID: 9800 RVA: 0x000C0B28 File Offset: 0x000BED28
	private float DialogTop
	{
		get
		{
			return ((float)Screen.height - 347f) / 2f;
		}
	}

	// Token: 0x170004CA RID: 1226
	// (get) Token: 0x06002649 RID: 9801 RVA: 0x000C0B3C File Offset: 0x000BED3C
	private float DialogLeft
	{
		get
		{
			return ((float)Screen.width - 600f) / 2f;
		}
	}

	// Token: 0x170004CB RID: 1227
	// (get) Token: 0x0600264A RID: 9802 RVA: 0x000C0B50 File Offset: 0x000BED50
	private Rect DialogRect
	{
		get
		{
			return new Rect(this.DialogLeft, this.DialogTop, 600f, 347f);
		}
	}

	// Token: 0x170004CC RID: 1228
	// (get) Token: 0x0600264B RID: 9803 RVA: 0x000C0B6D File Offset: 0x000BED6D
	private float ButtonTop
	{
		get
		{
			return this.DialogTop + 347f - 20f - 31f;
		}
	}

	// Token: 0x170004CD RID: 1229
	// (get) Token: 0x0600264C RID: 9804 RVA: 0x000C0B87 File Offset: 0x000BED87
	private float ButtonLeft
	{
		get
		{
			return ((float)Screen.width - 100f) / 2f;
		}
	}

	// Token: 0x170004CE RID: 1230
	// (get) Token: 0x0600264D RID: 9805 RVA: 0x000C0B9B File Offset: 0x000BED9B
	private Rect ButtonRect
	{
		get
		{
			return new Rect(this.ButtonLeft, this.ButtonTop, 100f, 31f);
		}
	}

	// Token: 0x0600264E RID: 9806 RVA: 0x000C0BB8 File Offset: 0x000BEDB8
	private void Awake()
	{
		this.BuildText();
		FatalErrorMgr.Get().AddErrorListener(new FatalErrorMgr.ErrorCallback(this.OnFatalError));
	}

	// Token: 0x0600264F RID: 9807 RVA: 0x000C0BD8 File Offset: 0x000BEDD8
	private void OnGUI()
	{
		this.InitGUIStyles();
		GUI.Box(this.DialogRect, string.Empty, this.m_dialogStyle);
		GUI.Box(this.DialogRect, this.m_text, this.m_dialogStyle);
		if (GUI.Button(this.ButtonRect, GameStrings.Get("GLOBAL_EXIT")))
		{
			FatalErrorMgr.Get().NotifyExitPressed();
		}
	}

	// Token: 0x06002650 RID: 9808 RVA: 0x000C0C3C File Offset: 0x000BEE3C
	private void InitGUIStyles()
	{
		if (this.m_dialogStyle != null)
		{
			return;
		}
		this.m_dialogStyle = new GUIStyle("box")
		{
			clipping = TextClipping.Overflow,
			stretchHeight = true,
			stretchWidth = true,
			wordWrap = true,
			fontSize = 16
		};
	}

	// Token: 0x06002651 RID: 9809 RVA: 0x000C0C8B File Offset: 0x000BEE8B
	private void OnFatalError(FatalErrorMessage message, object userData)
	{
		FatalErrorMgr.Get().RemoveErrorListener(new FatalErrorMgr.ErrorCallback(this.OnFatalError));
		this.BuildText();
	}

	// Token: 0x06002652 RID: 9810 RVA: 0x000C0CAC File Offset: 0x000BEEAC
	private void BuildText()
	{
		List<FatalErrorMessage> messages = FatalErrorMgr.Get().GetMessages();
		if (messages.Count == 0)
		{
			this.m_text = string.Empty;
			return;
		}
		List<string> list = new List<string>();
		for (int i = 0; i < messages.Count; i++)
		{
			string text = messages[i].m_text;
			if (!list.Contains(text))
			{
				list.Add(text);
			}
		}
		StringBuilder stringBuilder = new StringBuilder();
		for (int j = 0; j < list.Count; j++)
		{
			string value = list[j];
			stringBuilder.Append(value);
			stringBuilder.Append("\n");
		}
		stringBuilder.Remove(stringBuilder.Length - 1, 1);
		this.m_text = stringBuilder.ToString();
	}

	// Token: 0x040015A0 RID: 5536
	private const float DialogWidth = 600f;

	// Token: 0x040015A1 RID: 5537
	private const float DialogHeight = 347f;

	// Token: 0x040015A2 RID: 5538
	private const float DialogPadding = 20f;

	// Token: 0x040015A3 RID: 5539
	private const float ButtonWidth = 100f;

	// Token: 0x040015A4 RID: 5540
	private const float ButtonHeight = 31f;

	// Token: 0x040015A5 RID: 5541
	private GUIStyle m_dialogStyle;

	// Token: 0x040015A6 RID: 5542
	private string m_text;
}
