using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using UnityEngine;

// Token: 0x02000933 RID: 2355
public class TextField : PegUIElement
{
	// Token: 0x17000763 RID: 1891
	// (get) Token: 0x060081EB RID: 33259 RVA: 0x002A462D File Offset: 0x002A282D
	// (set) Token: 0x060081EC RID: 33260 RVA: 0x002A4634 File Offset: 0x002A2834
	public static Rect KeyboardArea { get; private set; }

	// Token: 0x17000764 RID: 1892
	// (get) Token: 0x060081ED RID: 33261 RVA: 0x002A463C File Offset: 0x002A283C
	public bool Active
	{
		get
		{
			return this == TextField.instance;
		}
	}

	// Token: 0x17000765 RID: 1893
	// (get) Token: 0x060081EE RID: 33262 RVA: 0x002A4649 File Offset: 0x002A2849
	// (set) Token: 0x060081EF RID: 33263 RVA: 0x002A4650 File Offset: 0x002A2850
	public string Text
	{
		get
		{
			return TextField.GetTextFieldText();
		}
		set
		{
			TextField.SetTextFieldText(value);
		}
	}

	// Token: 0x1400008A RID: 138
	// (add) Token: 0x060081F0 RID: 33264 RVA: 0x002A4658 File Offset: 0x002A2858
	// (remove) Token: 0x060081F1 RID: 33265 RVA: 0x002A4690 File Offset: 0x002A2890
	public event Action<Event> Preprocess;

	// Token: 0x1400008B RID: 139
	// (add) Token: 0x060081F2 RID: 33266 RVA: 0x002A46C8 File Offset: 0x002A28C8
	// (remove) Token: 0x060081F3 RID: 33267 RVA: 0x002A4700 File Offset: 0x002A2900
	public event Action<string> Changed;

	// Token: 0x1400008C RID: 140
	// (add) Token: 0x060081F4 RID: 33268 RVA: 0x002A4738 File Offset: 0x002A2938
	// (remove) Token: 0x060081F5 RID: 33269 RVA: 0x002A4770 File Offset: 0x002A2970
	public event Action<string> Submitted;

	// Token: 0x1400008D RID: 141
	// (add) Token: 0x060081F6 RID: 33270 RVA: 0x002A47A8 File Offset: 0x002A29A8
	// (remove) Token: 0x060081F7 RID: 33271 RVA: 0x002A47E0 File Offset: 0x002A29E0
	public event Action Canceled;

	// Token: 0x060081F8 RID: 33272 RVA: 0x002A4818 File Offset: 0x002A2A18
	protected override void Awake()
	{
		base.Awake();
		base.gameObject.name = string.Format("TextField_{0:000}", TextField.nextId++);
		if (base.gameObject.GetComponent<BoxCollider>() == null)
		{
			base.gameObject.AddComponent<BoxCollider>();
		}
		this.UpdateCollider();
		FatalErrorMgr.Get().AddErrorListener(new FatalErrorMgr.ErrorCallback(this.OnFatalError));
	}

	// Token: 0x060081F9 RID: 33273 RVA: 0x002A488E File Offset: 0x002A2A8E
	protected override void OnDestroy()
	{
		if (this.Active)
		{
			this.Deactivate();
		}
		FatalErrorMgr.Get().RemoveErrorListener(new FatalErrorMgr.ErrorCallback(this.OnFatalError));
		base.OnDestroy();
	}

	// Token: 0x060081FA RID: 33274 RVA: 0x002A48BC File Offset: 0x002A2ABC
	private void Update()
	{
		if (this.lastTopLeft != this.inputTopLeft.position || this.lastBottomRight != this.inputBottomRight.position)
		{
			this.UpdateCollider();
			this.lastTopLeft = this.inputTopLeft.position;
			this.lastBottomRight = this.inputBottomRight.position;
		}
		if (!this.Active)
		{
			return;
		}
		Rect rect = this.ComputeBounds();
		if (rect != this.lastBounds)
		{
			this.lastBounds = rect;
			TextField.SetTextFieldBounds(rect);
		}
	}

	// Token: 0x060081FB RID: 33275 RVA: 0x002A4951 File Offset: 0x002A2B51
	public void SetInputFont(Font font)
	{
		this.m_InputFont = font;
	}

	// Token: 0x060081FC RID: 33276 RVA: 0x002A495C File Offset: 0x002A2B5C
	public void Activate()
	{
		if (TextField.instance != null && TextField.instance != this)
		{
			TextField.instance.Deactivate();
		}
		TextField.instance = this;
		this.lastBounds = this.ComputeBounds();
		TextField.KeyboardArea = TextField.ActivateTextField(base.gameObject.name, this.lastBounds, this.autocorrect ? 1 : 0, (uint)this.keyboardType, (uint)this.returnKeyType);
		TextField.SetTextFieldColor(this.textColor.r, this.textColor.g, this.textColor.b, this.textColor.a);
		TextField.SetTextFieldMaxCharacters(512);
	}

	// Token: 0x060081FD RID: 33277 RVA: 0x002A4A18 File Offset: 0x002A2C18
	public void Deactivate()
	{
		if (this == TextField.instance)
		{
			TextField.KeyboardArea = default(Rect);
			TextField.DeactivateTextField();
			TextField.instance = null;
		}
	}

	// Token: 0x060081FE RID: 33278 RVA: 0x00003BE8 File Offset: 0x00001DE8
	public static void ToggleDebug()
	{
	}

	// Token: 0x060081FF RID: 33279 RVA: 0x002A4A4B File Offset: 0x002A2C4B
	protected override void OnRelease()
	{
		if (this.Active)
		{
			return;
		}
		this.Activate();
	}

	// Token: 0x06008200 RID: 33280 RVA: 0x002A4A5C File Offset: 0x002A2C5C
	private bool OnPreprocess(Event e)
	{
		if (this.Preprocess != null)
		{
			this.Preprocess(e);
		}
		return false;
	}

	// Token: 0x06008201 RID: 33281 RVA: 0x002A4A73 File Offset: 0x002A2C73
	private void OnChanged(string text)
	{
		if (this.Changed != null)
		{
			this.Changed(text);
		}
	}

	// Token: 0x06008202 RID: 33282 RVA: 0x002A4A89 File Offset: 0x002A2C89
	private void OnSubmitted(string text)
	{
		if (this.Submitted != null)
		{
			this.Submitted(text);
		}
	}

	// Token: 0x06008203 RID: 33283 RVA: 0x002A4A9F File Offset: 0x002A2C9F
	private void OnCanceled()
	{
		if (this.Canceled != null)
		{
			this.Canceled();
		}
		this.Deactivate();
	}

	// Token: 0x06008204 RID: 33284 RVA: 0x002A4ABA File Offset: 0x002A2CBA
	private void OnKeyboardAreaChanged(Rect area)
	{
		TextField.KeyboardArea = area;
	}

	// Token: 0x06008205 RID: 33285 RVA: 0x002A4AC2 File Offset: 0x002A2CC2
	private void OnFatalError(FatalErrorMessage message, object userData)
	{
		this.Deactivate();
	}

	// Token: 0x06008206 RID: 33286 RVA: 0x002A4ACC File Offset: 0x002A2CCC
	private void UpdateCollider()
	{
		BoxCollider component = base.GetComponent<BoxCollider>();
		Vector3 vector = base.transform.InverseTransformPoint(this.inputTopLeft.transform.position);
		Vector3 vector2 = base.transform.InverseTransformPoint(this.inputBottomRight.transform.position);
		component.center = (vector + vector2) / 2f;
		component.size = VectorUtils.Abs(vector2 - vector);
	}

	// Token: 0x06008207 RID: 33287 RVA: 0x002A4B40 File Offset: 0x002A2D40
	private Rect ComputeBounds()
	{
		Camera camera = CameraUtils.FindFirstByLayer(base.gameObject.layer);
		Vector2 vector = camera.WorldToScreenPoint(this.inputTopLeft.transform.position);
		Vector2 vector2 = camera.WorldToScreenPoint(this.inputBottomRight.transform.position);
		vector.y = (float)Screen.height - vector.y;
		vector2.y = (float)Screen.height - vector2.y;
		Vector2 vector3 = Vector2.Min(vector, vector2);
		Vector2 vector4 = Vector2.Max(vector, vector2);
		return Rect.MinMaxRect(Mathf.Round(vector3.x), Mathf.Round(vector3.y), Mathf.Round(vector4.x), Mathf.Round(vector4.y));
	}

	// Token: 0x06008208 RID: 33288 RVA: 0x002A4C00 File Offset: 0x002A2E00
	private static TextField.PluginRect Plugin_ActivateTextField(string name, [MarshalAs(UnmanagedType.Struct)] TextField.PluginRect bounds, int autocorrect, uint keyboardType, uint returnKeyType)
	{
		return default(TextField.PluginRect);
	}

	// Token: 0x06008209 RID: 33289 RVA: 0x00003BE8 File Offset: 0x00001DE8
	private static void Plugin_DeactivateTextField()
	{
	}

	// Token: 0x0600820A RID: 33290 RVA: 0x00003BE8 File Offset: 0x00001DE8
	private static void Plugin_SetTextFieldBounds([MarshalAs(UnmanagedType.Struct)] TextField.PluginRect bounds)
	{
	}

	// Token: 0x0600820B RID: 33291 RVA: 0x000D5239 File Offset: 0x000D3439
	private static string Plugin_GetTextFieldText()
	{
		return "";
	}

	// Token: 0x0600820C RID: 33292 RVA: 0x00003BE8 File Offset: 0x00001DE8
	private static void Plugin_SetTextFieldText(string text)
	{
	}

	// Token: 0x0600820D RID: 33293 RVA: 0x00003BE8 File Offset: 0x00001DE8
	private static void Plugin_SetTextFieldColor(float r, float g, float b, float a)
	{
	}

	// Token: 0x0600820E RID: 33294 RVA: 0x00003BE8 File Offset: 0x00001DE8
	private static void Plugin_SetTextFieldMaxCharacters(int maxCharacters)
	{
	}

	// Token: 0x0600820F RID: 33295 RVA: 0x002A4C16 File Offset: 0x002A2E16
	private void Unity_TextInputChanged(string text)
	{
		if (this.Active)
		{
			this.OnChanged(text);
		}
	}

	// Token: 0x06008210 RID: 33296 RVA: 0x002A4C27 File Offset: 0x002A2E27
	private void Unity_TextInputSubmitted(string text)
	{
		if (this.Active)
		{
			this.OnSubmitted(text);
		}
	}

	// Token: 0x06008211 RID: 33297 RVA: 0x002A4C38 File Offset: 0x002A2E38
	private void Unity_TextInputCanceled(string unused)
	{
		if (this.Active)
		{
			this.OnCanceled();
		}
	}

	// Token: 0x06008212 RID: 33298 RVA: 0x002A4C48 File Offset: 0x002A2E48
	private void Unity_KeyboardAreaChanged(string rectString)
	{
		if (this.Active)
		{
			Match match = Regex.Match(rectString, string.Format("x\\: (?<x>{0})\\, y\\: (?<y>{0})\\, width\\: (?<width>{0})\\, height\\: (?<height>{0})", "[-+]?[0-9]*\\.?[0-9]+"));
			CultureInfo invariantCulture = CultureInfo.InvariantCulture;
			Rect area = new Rect(float.Parse(match.Groups["x"].Value, invariantCulture), float.Parse(match.Groups["y"].Value, invariantCulture), float.Parse(match.Groups["width"].Value, invariantCulture), float.Parse(match.Groups["height"].Value, invariantCulture));
			this.OnKeyboardAreaChanged(area);
		}
	}

	// Token: 0x06008213 RID: 33299 RVA: 0x0001FA65 File Offset: 0x0001DC65
	private static bool UseNativeKeyboard()
	{
		return false;
	}

	// Token: 0x06008214 RID: 33300 RVA: 0x002A4CF8 File Offset: 0x002A2EF8
	private static TextField.PluginRect ActivateTextField(string name, TextField.PluginRect bounds, int autocorrect, uint keyboardType, uint returnKeyType)
	{
		if (TextField.UseNativeKeyboard())
		{
			return TextField.Plugin_ActivateTextField(name, bounds, autocorrect, keyboardType, returnKeyType);
		}
		if (UniversalInputManager.Get() == null)
		{
			return default(TextField.PluginRect);
		}
		TextField.instance.inputParams = new UniversalInputManager.TextInputParams
		{
			m_owner = TextField.instance.gameObject,
			m_preprocessCallback = new UniversalInputManager.TextInputPreprocessCallback(TextField.instance.OnPreprocess),
			m_completedCallback = new UniversalInputManager.TextInputCompletedCallback(TextField.instance.OnSubmitted),
			m_updatedCallback = new UniversalInputManager.TextInputUpdatedCallback(TextField.instance.OnChanged),
			m_canceledCallback = new UniversalInputManager.TextInputCanceledCallback(TextField.instance.InputCanceled),
			m_font = TextField.instance.m_InputFont,
			m_maxCharacters = TextField.instance.maxCharacters,
			m_inputKeepFocusOnComplete = TextField.instance.inputKeepFocusOnComplete,
			m_touchScreenKeyboardHideInput = TextField.instance.hideInput,
			m_useNativeKeyboard = TextField.UseNativeKeyboard()
		};
		UniversalInputManager.Get().UseTextInput(TextField.instance.inputParams, false);
		TextField.SetTextFieldBounds(bounds);
		if (TextField.instance.Active)
		{
			return new Rect(0f, (float)Screen.height, (float)Screen.width, (float)Screen.height * 0.5f);
		}
		return default(TextField.PluginRect);
	}

	// Token: 0x06008215 RID: 33301 RVA: 0x002A4E43 File Offset: 0x002A3043
	private static void DeactivateTextField()
	{
		if (TextField.UseNativeKeyboard())
		{
			TextField.Plugin_DeactivateTextField();
			return;
		}
		if (UniversalInputManager.Get() != null)
		{
			UniversalInputManager.Get().CancelTextInput(TextField.instance.gameObject, false);
		}
	}

	// Token: 0x06008216 RID: 33302 RVA: 0x002A4E70 File Offset: 0x002A3070
	private static void SetTextFieldBounds(TextField.PluginRect bounds)
	{
		if (TextField.UseNativeKeyboard())
		{
			TextField.Plugin_SetTextFieldBounds(bounds);
			return;
		}
		bounds.x /= (float)Screen.width;
		bounds.y /= (float)Screen.height;
		bounds.width /= (float)Screen.width;
		bounds.height /= (float)Screen.height;
		if (TextField.instance.inputParams.m_rect != bounds)
		{
			TextField.instance.inputParams.m_rect = bounds;
			TextField.instance.UpdateTextInput();
		}
	}

	// Token: 0x06008217 RID: 33303 RVA: 0x002A4F0A File Offset: 0x002A310A
	private static string GetTextFieldText()
	{
		if (TextField.UseNativeKeyboard())
		{
			return TextField.Plugin_GetTextFieldText();
		}
		return UniversalInputManager.Get().GetInputText();
	}

	// Token: 0x06008218 RID: 33304 RVA: 0x002A4F24 File Offset: 0x002A3124
	private static void SetTextFieldText(string text)
	{
		if (TextField.UseNativeKeyboard())
		{
			TextField.Plugin_SetTextFieldText(text);
			return;
		}
		UniversalInputManager.Get().SetInputText(text, false);
		if (TextField.instance != null && TextField.instance.inputParams != null)
		{
			TextField.instance.inputParams.m_text = text;
		}
	}

	// Token: 0x06008219 RID: 33305 RVA: 0x002A4F74 File Offset: 0x002A3174
	private static void SetTextFieldColor(float r, float g, float b, float a)
	{
		if (TextField.UseNativeKeyboard())
		{
			TextField.Plugin_SetTextFieldColor(r, g, b, a);
			return;
		}
	}

	// Token: 0x0600821A RID: 33306 RVA: 0x002A4F87 File Offset: 0x002A3187
	private static void SetTextFieldMaxCharacters(int maxCharacters)
	{
		if (TextField.UseNativeKeyboard())
		{
			TextField.Plugin_SetTextFieldMaxCharacters(maxCharacters);
			return;
		}
		if (maxCharacters != TextField.instance.maxCharacters)
		{
			TextField.instance.maxCharacters = maxCharacters;
			TextField.instance.UpdateTextInput();
		}
	}

	// Token: 0x0600821B RID: 33307 RVA: 0x002A4FB9 File Offset: 0x002A31B9
	private void UpdateTextInput()
	{
		UniversalInputManager.Get().UseTextInput(TextField.instance.inputParams, true);
		UniversalInputManager.Get().FocusTextInput(TextField.instance.gameObject);
	}

	// Token: 0x0600821C RID: 33308 RVA: 0x002A4FE4 File Offset: 0x002A31E4
	private void InputCanceled(bool userRequested, GameObject requester)
	{
		this.OnCanceled();
	}

	// Token: 0x04006CFB RID: 27899
	public Transform inputTopLeft;

	// Token: 0x04006CFC RID: 27900
	public Transform inputBottomRight;

	// Token: 0x04006CFD RID: 27901
	public Color textColor;

	// Token: 0x04006CFE RID: 27902
	public int maxCharacters;

	// Token: 0x04006CFF RID: 27903
	public bool autocorrect;

	// Token: 0x04006D00 RID: 27904
	public TextField.KeyboardType keyboardType;

	// Token: 0x04006D01 RID: 27905
	public TextField.KeyboardReturnKeyType returnKeyType;

	// Token: 0x04006D02 RID: 27906
	public bool hideInput = true;

	// Token: 0x04006D03 RID: 27907
	public bool useNativeKeyboard;

	// Token: 0x04006D04 RID: 27908
	public bool inputKeepFocusOnComplete;

	// Token: 0x04006D05 RID: 27909
	private static uint nextId;

	// Token: 0x04006D06 RID: 27910
	private static TextField instance;

	// Token: 0x04006D07 RID: 27911
	private Rect lastBounds;

	// Token: 0x04006D08 RID: 27912
	private Vector3 lastTopLeft;

	// Token: 0x04006D09 RID: 27913
	private Vector3 lastBottomRight;

	// Token: 0x04006D0A RID: 27914
	private Font m_InputFont;

	// Token: 0x04006D10 RID: 27920
	private UniversalInputManager.TextInputParams inputParams;

	// Token: 0x020025F6 RID: 9718
	private struct PluginRect
	{
		// Token: 0x06013535 RID: 79157 RVA: 0x00530ED6 File Offset: 0x0052F0D6
		public PluginRect(float x, float y, float width, float height)
		{
			this.x = x;
			this.y = y;
			this.width = width;
			this.height = height;
		}

		// Token: 0x06013536 RID: 79158 RVA: 0x00530EF5 File Offset: 0x0052F0F5
		public static implicit operator TextField.PluginRect(Rect rect)
		{
			return new TextField.PluginRect(rect.x, rect.y, rect.width, rect.height);
		}

		// Token: 0x06013537 RID: 79159 RVA: 0x00530F18 File Offset: 0x0052F118
		public static implicit operator Rect(TextField.PluginRect rect)
		{
			return new Rect(rect.x, rect.y, rect.width, rect.height);
		}

		// Token: 0x06013538 RID: 79160 RVA: 0x00530F38 File Offset: 0x0052F138
		public override string ToString()
		{
			return string.Format("[x: {0}, y: {1}, width: {2}, height: {3}]", new object[]
			{
				this.x,
				this.y,
				this.width,
				this.height
			});
		}

		// Token: 0x0400EF21 RID: 61217
		public float x;

		// Token: 0x0400EF22 RID: 61218
		public float y;

		// Token: 0x0400EF23 RID: 61219
		public float width;

		// Token: 0x0400EF24 RID: 61220
		public float height;
	}

	// Token: 0x020025F7 RID: 9719
	public enum KeyboardType
	{
		// Token: 0x0400EF26 RID: 61222
		Default,
		// Token: 0x0400EF27 RID: 61223
		ASCIICapable,
		// Token: 0x0400EF28 RID: 61224
		NumbersAndPunctuation,
		// Token: 0x0400EF29 RID: 61225
		URL,
		// Token: 0x0400EF2A RID: 61226
		NumberPad,
		// Token: 0x0400EF2B RID: 61227
		PhonePad,
		// Token: 0x0400EF2C RID: 61228
		NamePhonePad,
		// Token: 0x0400EF2D RID: 61229
		EmailAddress,
		// Token: 0x0400EF2E RID: 61230
		DecimalPad,
		// Token: 0x0400EF2F RID: 61231
		Twitter
	}

	// Token: 0x020025F8 RID: 9720
	public enum KeyboardReturnKeyType
	{
		// Token: 0x0400EF31 RID: 61233
		Default,
		// Token: 0x0400EF32 RID: 61234
		Go,
		// Token: 0x0400EF33 RID: 61235
		Google,
		// Token: 0x0400EF34 RID: 61236
		Join,
		// Token: 0x0400EF35 RID: 61237
		Next,
		// Token: 0x0400EF36 RID: 61238
		Route,
		// Token: 0x0400EF37 RID: 61239
		Search,
		// Token: 0x0400EF38 RID: 61240
		Send,
		// Token: 0x0400EF39 RID: 61241
		Yahoo,
		// Token: 0x0400EF3A RID: 61242
		Done,
		// Token: 0x0400EF3B RID: 61243
		EmergencyCall
	}
}
