using System;
using System.Collections.Generic;
using Blizzard.Commerce;
using UnityEngine;

// Token: 0x02000007 RID: 7
public class CheckoutInputManager : MonoBehaviour
{
	// Token: 0x1700000B RID: 11
	// (get) Token: 0x0600001F RID: 31 RVA: 0x00002668 File Offset: 0x00000868
	// (set) Token: 0x06000020 RID: 32 RVA: 0x00002670 File Offset: 0x00000870
	public bool IsActive { get; set; }

	// Token: 0x06000021 RID: 33 RVA: 0x0000267C File Offset: 0x0000087C
	private int GetModifiers(Event e)
	{
		int num = 0;
		if (e.isKey)
		{
			if ((e.modifiers & EventModifiers.Shift) != EventModifiers.None)
			{
				num |= 2;
			}
			if ((e.modifiers & EventModifiers.Control) != EventModifiers.None)
			{
				num |= 1;
			}
			if ((e.modifiers & EventModifiers.Alt) != EventModifiers.None)
			{
				num |= 4;
			}
		}
		return num;
	}

	// Token: 0x06000022 RID: 34 RVA: 0x000026BE File Offset: 0x000008BE
	public void Setup(HearthstoneCheckout checkout, IScreenSpace screenSpace)
	{
		this.m_checkout = checkout;
		this.screenSpace = screenSpace;
	}

	// Token: 0x06000023 RID: 35 RVA: 0x000026CE File Offset: 0x000008CE
	public void AddKeyboardEventListener(KeyCode keyCode, CheckoutInputManager.KeyboardEventListener listener)
	{
		this.m_KeyboardEventHandlers[keyCode] = listener;
	}

	// Token: 0x06000024 RID: 36 RVA: 0x000026DD File Offset: 0x000008DD
	public void RemoveKeyboardEventListener(KeyCode keyCode)
	{
		this.m_KeyboardEventHandlers.Remove(keyCode);
	}

	// Token: 0x06000025 RID: 37 RVA: 0x000026EC File Offset: 0x000008EC
	private CheckoutInputManager.Vec2D GetMousePosition(Rect window, Vector3 mousePosition, float inputScale)
	{
		if (window.Contains(mousePosition))
		{
			return new CheckoutInputManager.Vec2D((int)((mousePosition.x - window.x) / inputScale), (int)(((float)Screen.height - mousePosition.y - window.y) / inputScale));
		}
		return null;
	}

	// Token: 0x06000026 RID: 38 RVA: 0x00002728 File Offset: 0x00000928
	public void OnGUI()
	{
		if (!this.IsActive || this.m_checkout == null || !this.m_checkout.CheckoutIsReady)
		{
			return;
		}
		Event current = Event.current;
		if (current == null)
		{
			return;
		}
		int num = 0;
		KeyCode keyCode = KeyCode.None;
		char c = '\0';
		blz_commerce_scene_input_type_t blz_commerce_scene_input_type_t = blz_commerce_scene_input_type_t.BLZ_COMMERCE_SCENE_PRESS;
		while (Event.PopEvent(current))
		{
			new Rect((float)(Screen.width / 2 - this.m_checkout.CheckoutUi.BrowserWidth / 2), (float)(Screen.height / 2 - this.m_checkout.CheckoutUi.BrowserHeight / 2), (float)this.m_checkout.CheckoutUi.BrowserWidth, (float)this.m_checkout.CheckoutUi.BrowserHeight);
			Vector3 mousePosition = InputCollection.GetMousePosition();
			CheckoutInputManager.Vec2D mousePosition2 = this.GetMousePosition(this.screenSpace.GetScreenRect(), mousePosition, this.screenSpace.GetScreenSpaceScale());
			if (current.isKey)
			{
				num = this.GetModifiers(current);
				blz_commerce_scene_input_type_t = ((current.type == EventType.KeyDown) ? blz_commerce_scene_input_type_t.BLZ_COMMERCE_SCENE_PRESS : blz_commerce_scene_input_type_t.BLZ_COMMERCE_SCENE_RELEASE);
				if ((current.modifiers & EventModifiers.FunctionKey) != EventModifiers.None)
				{
					int keyCode2 = CheckoutInputManager.KeycodeToVK(current.keyCode);
					blz_commerce_key_input_t obj = new blz_commerce_key_input_t
					{
						keyCode = keyCode2,
						type = blz_commerce_scene_input_type_t,
						modifiers = (uint)num,
						character = (int)current.character
					};
					battlenet_commerce.blz_commerce_browser_send_event(this.m_checkout.Sdk, blz_commerce_browser_event_type_t.KEYBOARD_EVENT, blz_commerce_key_input_t.getCPtr(obj).Handle);
					continue;
				}
				if (current.keyCode > KeyCode.None)
				{
					keyCode = current.keyCode;
				}
				if (current.character != '\0')
				{
					c = CheckoutInputManager.SwapCharacter(current.character);
				}
			}
			if (mousePosition2 != null)
			{
				if (current.isScrollWheel)
				{
					if (current.type == EventType.ScrollWheel)
					{
						Vector2 delta = current.delta;
						blz_commerce_mouse_wheel_t obj2 = new blz_commerce_mouse_wheel_t
						{
							delta = (int)((0f - delta.y) * 40f),
							coords = this.ConvertVec2d(mousePosition2),
							mod = (uint)num
						};
						battlenet_commerce.blz_commerce_browser_send_event(this.m_checkout.Sdk, blz_commerce_browser_event_type_t.MOUSE_WHEEL, blz_commerce_mouse_wheel_t.getCPtr(obj2).Handle);
					}
				}
				else
				{
					int button = current.button;
					blz_commerce_scene_mouse_button_t button2;
					if (button != 0)
					{
						if (button != 1)
						{
							button2 = blz_commerce_scene_mouse_button_t.MIDDLE;
						}
						else
						{
							button2 = blz_commerce_scene_mouse_button_t.RIGHT;
						}
					}
					else
					{
						button2 = blz_commerce_scene_mouse_button_t.LEFT;
					}
					blz_commerce_scene_input_type_t type = (current.type == EventType.MouseDown) ? blz_commerce_scene_input_type_t.BLZ_COMMERCE_SCENE_PRESS : blz_commerce_scene_input_type_t.BLZ_COMMERCE_SCENE_RELEASE;
					blz_commerce_mouse_input_t obj3 = new blz_commerce_mouse_input_t
					{
						type = type,
						button = button2,
						coords = this.ConvertVec2d(mousePosition2)
					};
					if (current.type == EventType.MouseDown || current.type == EventType.MouseUp)
					{
						battlenet_commerce.blz_commerce_browser_send_event(this.m_checkout.Sdk, blz_commerce_browser_event_type_t.MOUSE_INPUT, blz_commerce_mouse_input_t.getCPtr(obj3).Handle);
					}
					else if (current.type == EventType.MouseEnterWindow || current.type == EventType.MouseMove || this.m_lastMousePosition == null || this.m_lastMousePosition.x != mousePosition2.x || this.m_lastMousePosition.y != mousePosition2.y)
					{
						blz_commerce_mouse_move_t obj4 = new blz_commerce_mouse_move_t
						{
							coords = this.ConvertVec2d(mousePosition2),
							mod = (uint)num
						};
						battlenet_commerce.blz_commerce_browser_send_event(this.m_checkout.Sdk, blz_commerce_browser_event_type_t.MOUSE_MOVE, blz_commerce_mouse_move_t.getCPtr(obj4).Handle);
					}
				}
				this.m_lastMousePosition = mousePosition2;
			}
		}
		CheckoutInputManager.KeyboardEventListener keyboardEventListener;
		if (!this.m_KeyboardEventHandlers.TryGetValue(current.keyCode, out keyboardEventListener))
		{
			if (keyCode > KeyCode.None || c != '\0')
			{
				if (c == '\0' || c == '\t')
				{
					int num2 = CheckoutInputManager.KeycodeToVK(keyCode);
					if (num2 == (int)keyCode)
					{
						if (c != '\0')
						{
							num2 = (int)c;
						}
						else
						{
							num2 = (int)char.ToUpper(keyCode.ToString()[0]);
						}
					}
					blz_commerce_key_input_t obj5 = new blz_commerce_key_input_t
					{
						keyCode = num2,
						type = blz_commerce_scene_input_type_t,
						modifiers = (uint)num,
						character = (int)c
					};
					battlenet_commerce.blz_commerce_browser_send_event(this.m_checkout.Sdk, blz_commerce_browser_event_type_t.KEYBOARD_EVENT, blz_commerce_key_input_t.getCPtr(obj5).Handle);
				}
				if (!this.blockedCharacters.Contains(c))
				{
					blz_commerce_character_input_t obj6 = new blz_commerce_character_input_t
					{
						character = (int)c,
						modifiers = (uint)num
					};
					battlenet_commerce.blz_commerce_browser_send_event(this.m_checkout.Sdk, blz_commerce_browser_event_type_t.CHARACTER, blz_commerce_character_input_t.getCPtr(obj6).Handle);
					return;
				}
			}
		}
		else
		{
			keyboardEventListener(blz_commerce_scene_input_type_t == blz_commerce_scene_input_type_t.BLZ_COMMERCE_SCENE_PRESS);
		}
	}

	// Token: 0x06000027 RID: 39 RVA: 0x00002B33 File Offset: 0x00000D33
	private blz_commerce_vec2d_t ConvertVec2d(CheckoutInputManager.Vec2D input)
	{
		return new blz_commerce_vec2d_t
		{
			x = input.x,
			y = input.y
		};
	}

	// Token: 0x06000028 RID: 40 RVA: 0x00002B54 File Offset: 0x00000D54
	private static char SwapCharacter(char character)
	{
		if (character == '\n')
		{
			return '\r';
		}
		if (character != '\u0019')
		{
			return character;
		}
		return '\t';
	}

	// Token: 0x06000029 RID: 41 RVA: 0x00002B78 File Offset: 0x00000D78
	private static int KeycodeToVK(KeyCode kCode)
	{
		if (kCode <= KeyCode.Tab)
		{
			if (kCode == KeyCode.Backspace)
			{
				return 8;
			}
			if (kCode == KeyCode.Tab)
			{
				return 9;
			}
		}
		else
		{
			if (kCode == KeyCode.Delete)
			{
				return 46;
			}
			switch (kCode)
			{
			case KeyCode.UpArrow:
				return 38;
			case KeyCode.DownArrow:
				return 40;
			case KeyCode.RightArrow:
				return 39;
			case KeyCode.LeftArrow:
				return 37;
			case KeyCode.Home:
				return 36;
			case KeyCode.End:
				return 35;
			}
		}
		return (int)kCode;
	}

	// Token: 0x0400000B RID: 11
	private CheckoutInputManager.Vec2D m_lastMousePosition;

	// Token: 0x0400000C RID: 12
	private HearthstoneCheckout m_checkout;

	// Token: 0x0400000D RID: 13
	private IScreenSpace screenSpace;

	// Token: 0x0400000E RID: 14
	private List<char> blockedCharacters = new List<char>
	{
		'\t',
		'\n'
	};

	// Token: 0x0400000F RID: 15
	private Dictionary<KeyCode, CheckoutInputManager.KeyboardEventListener> m_KeyboardEventHandlers = new Dictionary<KeyCode, CheckoutInputManager.KeyboardEventListener>();

	// Token: 0x02001264 RID: 4708
	private class Vec2D
	{
		// Token: 0x0600D3F4 RID: 54260 RVA: 0x003E369C File Offset: 0x003E189C
		public Vec2D(int x, int y)
		{
			this.x = x;
			this.y = y;
		}

		// Token: 0x0400A36B RID: 41835
		public int x;

		// Token: 0x0400A36C RID: 41836
		public int y;
	}

	// Token: 0x02001265 RID: 4709
	// (Invoke) Token: 0x0600D3F6 RID: 54262
	public delegate void KeyboardEventListener(bool isKeyDown);
}
