using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using UnityEngine;

public class TextField : PegUIElement
{
	private struct PluginRect
	{
		public float x;

		public float y;

		public float width;

		public float height;

		public PluginRect(float x, float y, float width, float height)
		{
			this.x = x;
			this.y = y;
			this.width = width;
			this.height = height;
		}

		public static implicit operator PluginRect(Rect rect)
		{
			return new PluginRect(rect.x, rect.y, rect.width, rect.height);
		}

		public static implicit operator Rect(PluginRect rect)
		{
			return new Rect(rect.x, rect.y, rect.width, rect.height);
		}

		public override string ToString()
		{
			return $"[x: {x}, y: {y}, width: {width}, height: {height}]";
		}
	}

	public enum KeyboardType
	{
		Default,
		ASCIICapable,
		NumbersAndPunctuation,
		URL,
		NumberPad,
		PhonePad,
		NamePhonePad,
		EmailAddress,
		DecimalPad,
		Twitter
	}

	public enum KeyboardReturnKeyType
	{
		Default,
		Go,
		Google,
		Join,
		Next,
		Route,
		Search,
		Send,
		Yahoo,
		Done,
		EmergencyCall
	}

	public Transform inputTopLeft;

	public Transform inputBottomRight;

	public Color textColor;

	public int maxCharacters;

	public bool autocorrect;

	public KeyboardType keyboardType;

	public KeyboardReturnKeyType returnKeyType;

	public bool hideInput = true;

	public bool useNativeKeyboard;

	public bool inputKeepFocusOnComplete;

	private static uint nextId;

	private static TextField instance;

	private Rect lastBounds;

	private Vector3 lastTopLeft;

	private Vector3 lastBottomRight;

	private Font m_InputFont;

	private UniversalInputManager.TextInputParams inputParams;

	public static Rect KeyboardArea { get; private set; }

	public bool Active => this == instance;

	public string Text
	{
		get
		{
			return GetTextFieldText();
		}
		set
		{
			SetTextFieldText(value);
		}
	}

	public event Action<Event> Preprocess;

	public event Action<string> Changed;

	public event Action<string> Submitted;

	public event Action Canceled;

	protected override void Awake()
	{
		base.Awake();
		base.gameObject.name = $"TextField_{nextId++:000}";
		if (base.gameObject.GetComponent<BoxCollider>() == null)
		{
			base.gameObject.AddComponent<BoxCollider>();
		}
		UpdateCollider();
		FatalErrorMgr.Get().AddErrorListener(OnFatalError);
	}

	protected override void OnDestroy()
	{
		if (Active)
		{
			Deactivate();
		}
		FatalErrorMgr.Get().RemoveErrorListener(OnFatalError);
		base.OnDestroy();
	}

	private void Update()
	{
		if (lastTopLeft != inputTopLeft.position || lastBottomRight != inputBottomRight.position)
		{
			UpdateCollider();
			lastTopLeft = inputTopLeft.position;
			lastBottomRight = inputBottomRight.position;
		}
		if (Active)
		{
			Rect rect = ComputeBounds();
			if (rect != lastBounds)
			{
				lastBounds = rect;
				SetTextFieldBounds(rect);
			}
		}
	}

	public void SetInputFont(Font font)
	{
		m_InputFont = font;
	}

	public void Activate()
	{
		if (instance != null && instance != this)
		{
			instance.Deactivate();
		}
		instance = this;
		lastBounds = ComputeBounds();
		KeyboardArea = ActivateTextField(base.gameObject.name, lastBounds, autocorrect ? 1 : 0, (uint)keyboardType, (uint)returnKeyType);
		SetTextFieldColor(textColor.r, textColor.g, textColor.b, textColor.a);
		SetTextFieldMaxCharacters(512);
	}

	public void Deactivate()
	{
		if (this == instance)
		{
			KeyboardArea = default(Rect);
			DeactivateTextField();
			instance = null;
		}
	}

	public static void ToggleDebug()
	{
	}

	protected override void OnRelease()
	{
		if (!Active)
		{
			Activate();
		}
	}

	private bool OnPreprocess(Event e)
	{
		if (this.Preprocess != null)
		{
			this.Preprocess(e);
		}
		return false;
	}

	private void OnChanged(string text)
	{
		if (this.Changed != null)
		{
			this.Changed(text);
		}
	}

	private void OnSubmitted(string text)
	{
		if (this.Submitted != null)
		{
			this.Submitted(text);
		}
	}

	private void OnCanceled()
	{
		if (this.Canceled != null)
		{
			this.Canceled();
		}
		Deactivate();
	}

	private void OnKeyboardAreaChanged(Rect area)
	{
		KeyboardArea = area;
	}

	private void OnFatalError(FatalErrorMessage message, object userData)
	{
		Deactivate();
	}

	private void UpdateCollider()
	{
		BoxCollider component = GetComponent<BoxCollider>();
		Vector3 vector = base.transform.InverseTransformPoint(inputTopLeft.transform.position);
		Vector3 vector2 = base.transform.InverseTransformPoint(inputBottomRight.transform.position);
		component.center = (vector + vector2) / 2f;
		component.size = VectorUtils.Abs(vector2 - vector);
	}

	private Rect ComputeBounds()
	{
		Camera camera = CameraUtils.FindFirstByLayer(base.gameObject.layer);
		Vector2 lhs = camera.WorldToScreenPoint(inputTopLeft.transform.position);
		Vector2 rhs = camera.WorldToScreenPoint(inputBottomRight.transform.position);
		lhs.y = (float)Screen.height - lhs.y;
		rhs.y = (float)Screen.height - rhs.y;
		Vector2 vector = Vector2.Min(lhs, rhs);
		Vector2 vector2 = Vector2.Max(lhs, rhs);
		return Rect.MinMaxRect(Mathf.Round(vector.x), Mathf.Round(vector.y), Mathf.Round(vector2.x), Mathf.Round(vector2.y));
	}

	private static PluginRect Plugin_ActivateTextField(string name, [MarshalAs(UnmanagedType.Struct)] PluginRect bounds, int autocorrect, uint keyboardType, uint returnKeyType)
	{
		return default(PluginRect);
	}

	private static void Plugin_DeactivateTextField()
	{
	}

	private static void Plugin_SetTextFieldBounds([MarshalAs(UnmanagedType.Struct)] PluginRect bounds)
	{
	}

	private static string Plugin_GetTextFieldText()
	{
		return "";
	}

	private static void Plugin_SetTextFieldText(string text)
	{
	}

	private static void Plugin_SetTextFieldColor(float r, float g, float b, float a)
	{
	}

	private static void Plugin_SetTextFieldMaxCharacters(int maxCharacters)
	{
	}

	private void Unity_TextInputChanged(string text)
	{
		if (Active)
		{
			OnChanged(text);
		}
	}

	private void Unity_TextInputSubmitted(string text)
	{
		if (Active)
		{
			OnSubmitted(text);
		}
	}

	private void Unity_TextInputCanceled(string unused)
	{
		if (Active)
		{
			OnCanceled();
		}
	}

	private void Unity_KeyboardAreaChanged(string rectString)
	{
		if (Active)
		{
			Match match = Regex.Match(rectString, string.Format("x\\: (?<x>{0})\\, y\\: (?<y>{0})\\, width\\: (?<width>{0})\\, height\\: (?<height>{0})", "[-+]?[0-9]*\\.?[0-9]+"));
			CultureInfo invariantCulture = CultureInfo.InvariantCulture;
			Rect area = new Rect(float.Parse(match.Groups["x"].Value, invariantCulture), float.Parse(match.Groups["y"].Value, invariantCulture), float.Parse(match.Groups["width"].Value, invariantCulture), float.Parse(match.Groups["height"].Value, invariantCulture));
			OnKeyboardAreaChanged(area);
		}
	}

	private static bool UseNativeKeyboard()
	{
		return false;
	}

	private static PluginRect ActivateTextField(string name, PluginRect bounds, int autocorrect, uint keyboardType, uint returnKeyType)
	{
		if (UseNativeKeyboard())
		{
			return Plugin_ActivateTextField(name, bounds, autocorrect, keyboardType, returnKeyType);
		}
		if (UniversalInputManager.Get() == null)
		{
			return default(PluginRect);
		}
		instance.inputParams = new UniversalInputManager.TextInputParams
		{
			m_owner = instance.gameObject,
			m_preprocessCallback = instance.OnPreprocess,
			m_completedCallback = instance.OnSubmitted,
			m_updatedCallback = instance.OnChanged,
			m_canceledCallback = instance.InputCanceled,
			m_font = instance.m_InputFont,
			m_maxCharacters = instance.maxCharacters,
			m_inputKeepFocusOnComplete = instance.inputKeepFocusOnComplete,
			m_touchScreenKeyboardHideInput = instance.hideInput,
			m_useNativeKeyboard = UseNativeKeyboard()
		};
		UniversalInputManager.Get().UseTextInput(instance.inputParams);
		SetTextFieldBounds(bounds);
		if (instance.Active)
		{
			return new Rect(0f, Screen.height, Screen.width, (float)Screen.height * 0.5f);
		}
		return default(PluginRect);
	}

	private static void DeactivateTextField()
	{
		if (UseNativeKeyboard())
		{
			Plugin_DeactivateTextField();
		}
		else if (UniversalInputManager.Get() != null)
		{
			UniversalInputManager.Get().CancelTextInput(instance.gameObject);
		}
	}

	private static void SetTextFieldBounds(PluginRect bounds)
	{
		if (UseNativeKeyboard())
		{
			Plugin_SetTextFieldBounds(bounds);
			return;
		}
		bounds.x /= Screen.width;
		bounds.y /= Screen.height;
		bounds.width /= Screen.width;
		bounds.height /= Screen.height;
		if (instance.inputParams.m_rect != bounds)
		{
			instance.inputParams.m_rect = bounds;
			instance.UpdateTextInput();
		}
	}

	private static string GetTextFieldText()
	{
		if (UseNativeKeyboard())
		{
			return Plugin_GetTextFieldText();
		}
		return UniversalInputManager.Get().GetInputText();
	}

	private static void SetTextFieldText(string text)
	{
		if (UseNativeKeyboard())
		{
			Plugin_SetTextFieldText(text);
			return;
		}
		UniversalInputManager.Get().SetInputText(text);
		if (instance != null && instance.inputParams != null)
		{
			instance.inputParams.m_text = text;
		}
	}

	private static void SetTextFieldColor(float r, float g, float b, float a)
	{
		if (UseNativeKeyboard())
		{
			Plugin_SetTextFieldColor(r, g, b, a);
		}
	}

	private static void SetTextFieldMaxCharacters(int maxCharacters)
	{
		if (UseNativeKeyboard())
		{
			Plugin_SetTextFieldMaxCharacters(maxCharacters);
		}
		else if (maxCharacters != instance.maxCharacters)
		{
			instance.maxCharacters = maxCharacters;
			instance.UpdateTextInput();
		}
	}

	private void UpdateTextInput()
	{
		UniversalInputManager.Get().UseTextInput(instance.inputParams, force: true);
		UniversalInputManager.Get().FocusTextInput(instance.gameObject);
	}

	private void InputCanceled(bool userRequested, GameObject requester)
	{
		OnCanceled();
	}
}
