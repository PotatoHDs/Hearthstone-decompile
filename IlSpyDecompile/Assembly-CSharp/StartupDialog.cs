using System;
using UnityEngine;
using UnityEngine.UI;

public class StartupDialog : MonoBehaviour
{
	private const string kStartupDialogResourcePath = "StartupDialog";

	[SerializeField]
	private GameObject m_singleButtonRoot;

	[SerializeField]
	private GameObject m_doubleButtonRoot;

	[SerializeField]
	private Text m_headerText;

	[SerializeField]
	private Text m_bodyText;

	[SerializeField]
	private UGUIButton m_singleButton;

	[SerializeField]
	private UGUIButton m_doubleButton1;

	[SerializeField]
	private UGUIButton m_doubleButton2;

	private static StartupDialog s_instance;

	public static void ShowStartupDialog(string header, string body, string buttonText, Action buttonDelegate)
	{
		if (EnsureInstance())
		{
			s_instance.SetupSingleButtonDialog(header, body, buttonText, buttonDelegate);
		}
	}

	public static void ShowStartupDialog(string header, string body, string buttonText1, Action buttonDelegate1, string buttonText2, Action buttonDelegate2)
	{
		if (EnsureInstance())
		{
			s_instance.SetupDoubleButtonDialog(header, body, buttonText1, buttonDelegate1, buttonText2, buttonDelegate2);
		}
	}

	public static void Destroy()
	{
		if (s_instance != null)
		{
			UnityEngine.Object.Destroy(s_instance.gameObject);
			s_instance = null;
		}
	}

	private static bool EnsureInstance()
	{
		if (s_instance == null)
		{
			GameObject gameObject = Resources.Load<GameObject>("StartupDialog");
			if (gameObject == null)
			{
				Debug.LogErrorFormat("Couldn't load prefab at ({0}).", "StartupDialog");
				return false;
			}
			GameObject gameObject2 = UnityEngine.Object.Instantiate(gameObject);
			s_instance = gameObject2.GetComponent<StartupDialog>();
			if (s_instance == null)
			{
				UnityEngine.Object.Destroy(gameObject2);
				Debug.LogErrorFormat("Couldn't find StartupDialog component on prefab at ({0}).", "StartupDialog");
				return false;
			}
			UnityEngine.Object.DontDestroyOnLoad(gameObject2);
		}
		return true;
	}

	private void SetupSingleButtonDialog(string header, string body, string buttonText, Action buttonDelegate)
	{
		m_singleButtonRoot.SetActive(value: true);
		m_doubleButtonRoot.SetActive(value: false);
		m_headerText.text = header;
		m_bodyText.text = body;
		m_singleButton.SetupButton(buttonText, buttonDelegate, Destroy);
	}

	private void SetupDoubleButtonDialog(string header, string body, string buttonText1, Action buttonDelegate1, string buttonText2, Action buttonDelegate2)
	{
		m_singleButtonRoot.SetActive(value: false);
		m_doubleButtonRoot.SetActive(value: true);
		m_headerText.text = header;
		m_bodyText.text = body;
		m_doubleButton1.SetupButton(buttonText1, buttonDelegate1, Destroy);
		m_doubleButton2.SetupButton(buttonText2, buttonDelegate2, Destroy);
	}
}
