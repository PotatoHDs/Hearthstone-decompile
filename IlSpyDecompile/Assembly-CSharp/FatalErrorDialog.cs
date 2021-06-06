using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class FatalErrorDialog : MonoBehaviour
{
	private const float DialogWidth = 600f;

	private const float DialogHeight = 347f;

	private const float DialogPadding = 20f;

	private const float ButtonWidth = 100f;

	private const float ButtonHeight = 31f;

	private GUIStyle m_dialogStyle;

	private string m_text;

	private float DialogTop => ((float)Screen.height - 347f) / 2f;

	private float DialogLeft => ((float)Screen.width - 600f) / 2f;

	private Rect DialogRect => new Rect(DialogLeft, DialogTop, 600f, 347f);

	private float ButtonTop => DialogTop + 347f - 20f - 31f;

	private float ButtonLeft => ((float)Screen.width - 100f) / 2f;

	private Rect ButtonRect => new Rect(ButtonLeft, ButtonTop, 100f, 31f);

	private void Awake()
	{
		BuildText();
		FatalErrorMgr.Get().AddErrorListener(OnFatalError);
	}

	private void OnGUI()
	{
		InitGUIStyles();
		GUI.Box(DialogRect, string.Empty, m_dialogStyle);
		GUI.Box(DialogRect, m_text, m_dialogStyle);
		if (GUI.Button(ButtonRect, GameStrings.Get("GLOBAL_EXIT")))
		{
			FatalErrorMgr.Get().NotifyExitPressed();
		}
	}

	private void InitGUIStyles()
	{
		if (m_dialogStyle == null)
		{
			m_dialogStyle = new GUIStyle("box")
			{
				clipping = TextClipping.Overflow,
				stretchHeight = true,
				stretchWidth = true,
				wordWrap = true,
				fontSize = 16
			};
		}
	}

	private void OnFatalError(FatalErrorMessage message, object userData)
	{
		FatalErrorMgr.Get().RemoveErrorListener(OnFatalError);
		BuildText();
	}

	private void BuildText()
	{
		List<FatalErrorMessage> messages = FatalErrorMgr.Get().GetMessages();
		if (messages.Count == 0)
		{
			m_text = string.Empty;
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
		m_text = stringBuilder.ToString();
	}
}
