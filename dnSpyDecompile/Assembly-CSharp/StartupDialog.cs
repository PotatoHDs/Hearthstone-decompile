using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000B2D RID: 2861
public class StartupDialog : MonoBehaviour
{
	// Token: 0x060097AA RID: 38826 RVA: 0x00310954 File Offset: 0x0030EB54
	public static void ShowStartupDialog(string header, string body, string buttonText, Action buttonDelegate)
	{
		if (StartupDialog.EnsureInstance())
		{
			StartupDialog.s_instance.SetupSingleButtonDialog(header, body, buttonText, buttonDelegate);
		}
	}

	// Token: 0x060097AB RID: 38827 RVA: 0x0031096B File Offset: 0x0030EB6B
	public static void ShowStartupDialog(string header, string body, string buttonText1, Action buttonDelegate1, string buttonText2, Action buttonDelegate2)
	{
		if (StartupDialog.EnsureInstance())
		{
			StartupDialog.s_instance.SetupDoubleButtonDialog(header, body, buttonText1, buttonDelegate1, buttonText2, buttonDelegate2);
		}
	}

	// Token: 0x060097AC RID: 38828 RVA: 0x00310986 File Offset: 0x0030EB86
	public static void Destroy()
	{
		if (StartupDialog.s_instance != null)
		{
			UnityEngine.Object.Destroy(StartupDialog.s_instance.gameObject);
			StartupDialog.s_instance = null;
		}
	}

	// Token: 0x060097AD RID: 38829 RVA: 0x003109AC File Offset: 0x0030EBAC
	private static bool EnsureInstance()
	{
		if (StartupDialog.s_instance == null)
		{
			GameObject gameObject = Resources.Load<GameObject>("StartupDialog");
			if (gameObject == null)
			{
				Debug.LogErrorFormat("Couldn't load prefab at ({0}).", new object[]
				{
					"StartupDialog"
				});
				return false;
			}
			GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(gameObject);
			StartupDialog.s_instance = gameObject2.GetComponent<StartupDialog>();
			if (StartupDialog.s_instance == null)
			{
				UnityEngine.Object.Destroy(gameObject2);
				Debug.LogErrorFormat("Couldn't find StartupDialog component on prefab at ({0}).", new object[]
				{
					"StartupDialog"
				});
				return false;
			}
			UnityEngine.Object.DontDestroyOnLoad(gameObject2);
		}
		return true;
	}

	// Token: 0x060097AE RID: 38830 RVA: 0x00310A3C File Offset: 0x0030EC3C
	private void SetupSingleButtonDialog(string header, string body, string buttonText, Action buttonDelegate)
	{
		this.m_singleButtonRoot.SetActive(true);
		this.m_doubleButtonRoot.SetActive(false);
		this.m_headerText.text = header;
		this.m_bodyText.text = body;
		this.m_singleButton.SetupButton(buttonText, buttonDelegate, new Action(StartupDialog.Destroy));
	}

	// Token: 0x060097AF RID: 38831 RVA: 0x00310A94 File Offset: 0x0030EC94
	private void SetupDoubleButtonDialog(string header, string body, string buttonText1, Action buttonDelegate1, string buttonText2, Action buttonDelegate2)
	{
		this.m_singleButtonRoot.SetActive(false);
		this.m_doubleButtonRoot.SetActive(true);
		this.m_headerText.text = header;
		this.m_bodyText.text = body;
		this.m_doubleButton1.SetupButton(buttonText1, buttonDelegate1, new Action(StartupDialog.Destroy));
		this.m_doubleButton2.SetupButton(buttonText2, buttonDelegate2, new Action(StartupDialog.Destroy));
	}

	// Token: 0x04007F00 RID: 32512
	private const string kStartupDialogResourcePath = "StartupDialog";

	// Token: 0x04007F01 RID: 32513
	[SerializeField]
	private GameObject m_singleButtonRoot;

	// Token: 0x04007F02 RID: 32514
	[SerializeField]
	private GameObject m_doubleButtonRoot;

	// Token: 0x04007F03 RID: 32515
	[SerializeField]
	private Text m_headerText;

	// Token: 0x04007F04 RID: 32516
	[SerializeField]
	private Text m_bodyText;

	// Token: 0x04007F05 RID: 32517
	[SerializeField]
	private UGUIButton m_singleButton;

	// Token: 0x04007F06 RID: 32518
	[SerializeField]
	private UGUIButton m_doubleButton1;

	// Token: 0x04007F07 RID: 32519
	[SerializeField]
	private UGUIButton m_doubleButton2;

	// Token: 0x04007F08 RID: 32520
	private static StartupDialog s_instance;
}
