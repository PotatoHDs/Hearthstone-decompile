using System.Collections.Generic;
using Blizzard.Commerce;
using UnityEngine;

public class CheckoutInputManager : MonoBehaviour
{
	private class Vec2D
	{
		public int x;

		public int y;

		public Vec2D(int x, int y)
		{
			this.x = x;
			this.y = y;
		}
	}

	public delegate void KeyboardEventListener(bool isKeyDown);

	private Vec2D m_lastMousePosition;

	private HearthstoneCheckout m_checkout;

	private IScreenSpace screenSpace;

	private List<char> blockedCharacters = new List<char> { '\t', '\n' };

	private Dictionary<KeyCode, KeyboardEventListener> m_KeyboardEventHandlers = new Dictionary<KeyCode, KeyboardEventListener>();

	public bool IsActive { get; set; }

	private int GetModifiers(Event e)
	{
		int num = 0;
		if (e.isKey)
		{
			if ((e.modifiers & EventModifiers.Shift) != 0)
			{
				num |= 2;
			}
			if ((e.modifiers & EventModifiers.Control) != 0)
			{
				num |= 1;
			}
			if ((e.modifiers & EventModifiers.Alt) != 0)
			{
				num |= 4;
			}
		}
		return num;
	}

	public void Setup(HearthstoneCheckout checkout, IScreenSpace screenSpace)
	{
		m_checkout = checkout;
		this.screenSpace = screenSpace;
	}

	public void AddKeyboardEventListener(KeyCode keyCode, KeyboardEventListener listener)
	{
		m_KeyboardEventHandlers[keyCode] = listener;
	}

	public void RemoveKeyboardEventListener(KeyCode keyCode)
	{
		m_KeyboardEventHandlers.Remove(keyCode);
	}

	private Vec2D GetMousePosition(Rect window, Vector3 mousePosition, float inputScale)
	{
		if (window.Contains(mousePosition))
		{
			return new Vec2D((int)((mousePosition.x - window.x) / inputScale), (int)(((float)Screen.height - mousePosition.y - window.y) / inputScale));
		}
		return null;
	}

	public void OnGUI()
	{
		if (!IsActive || m_checkout == null || !m_checkout.CheckoutIsReady)
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
			new Rect(Screen.width / 2 - m_checkout.CheckoutUi.BrowserWidth / 2, Screen.height / 2 - m_checkout.CheckoutUi.BrowserHeight / 2, m_checkout.CheckoutUi.BrowserWidth, m_checkout.CheckoutUi.BrowserHeight);
			Vector3 mousePosition = InputCollection.GetMousePosition();
			Vec2D mousePosition2 = GetMousePosition(screenSpace.GetScreenRect(), mousePosition, screenSpace.GetScreenSpaceScale());
			if (current.isKey)
			{
				num = GetModifiers(current);
				blz_commerce_scene_input_type_t = ((current.type != EventType.KeyDown) ? blz_commerce_scene_input_type_t.BLZ_COMMERCE_SCENE_RELEASE : blz_commerce_scene_input_type_t.BLZ_COMMERCE_SCENE_PRESS);
				if ((current.modifiers & EventModifiers.FunctionKey) != 0)
				{
					int keyCode2 = KeycodeToVK(current.keyCode);
					blz_commerce_key_input_t obj = new blz_commerce_key_input_t
					{
						keyCode = keyCode2,
						type = blz_commerce_scene_input_type_t,
						modifiers = (uint)num,
						character = current.character
					};
					battlenet_commerce.blz_commerce_browser_send_event(m_checkout.Sdk, blz_commerce_browser_event_type_t.KEYBOARD_EVENT, blz_commerce_key_input_t.getCPtr(obj).Handle);
					continue;
				}
				if (current.keyCode > KeyCode.None)
				{
					keyCode = current.keyCode;
				}
				if (current.character != 0)
				{
					c = SwapCharacter(current.character);
				}
			}
			if (mousePosition2 == null)
			{
				continue;
			}
			if (current.isScrollWheel)
			{
				if (current.type == EventType.ScrollWheel)
				{
					Vector2 delta = current.delta;
					blz_commerce_mouse_wheel_t obj2 = new blz_commerce_mouse_wheel_t
					{
						delta = (int)((0f - delta.y) * 40f),
						coords = ConvertVec2d(mousePosition2),
						mod = (uint)num
					};
					battlenet_commerce.blz_commerce_browser_send_event(m_checkout.Sdk, blz_commerce_browser_event_type_t.MOUSE_WHEEL, blz_commerce_mouse_wheel_t.getCPtr(obj2).Handle);
				}
			}
			else
			{
				blz_commerce_scene_mouse_button_t button = current.button switch
				{
					0 => blz_commerce_scene_mouse_button_t.LEFT, 
					1 => blz_commerce_scene_mouse_button_t.RIGHT, 
					_ => blz_commerce_scene_mouse_button_t.MIDDLE, 
				};
				blz_commerce_scene_input_type_t type = ((current.type != 0) ? blz_commerce_scene_input_type_t.BLZ_COMMERCE_SCENE_RELEASE : blz_commerce_scene_input_type_t.BLZ_COMMERCE_SCENE_PRESS);
				blz_commerce_mouse_input_t obj3 = new blz_commerce_mouse_input_t
				{
					type = type,
					button = button,
					coords = ConvertVec2d(mousePosition2)
				};
				if (current.type == EventType.MouseDown || current.type == EventType.MouseUp)
				{
					battlenet_commerce.blz_commerce_browser_send_event(m_checkout.Sdk, blz_commerce_browser_event_type_t.MOUSE_INPUT, blz_commerce_mouse_input_t.getCPtr(obj3).Handle);
				}
				else if (current.type == EventType.MouseEnterWindow || current.type == EventType.MouseMove || m_lastMousePosition == null || m_lastMousePosition.x != mousePosition2.x || m_lastMousePosition.y != mousePosition2.y)
				{
					blz_commerce_mouse_move_t obj4 = new blz_commerce_mouse_move_t
					{
						coords = ConvertVec2d(mousePosition2),
						mod = (uint)num
					};
					battlenet_commerce.blz_commerce_browser_send_event(m_checkout.Sdk, blz_commerce_browser_event_type_t.MOUSE_MOVE, blz_commerce_mouse_move_t.getCPtr(obj4).Handle);
				}
			}
			m_lastMousePosition = mousePosition2;
		}
		if (!m_KeyboardEventHandlers.TryGetValue(current.keyCode, out var value))
		{
			if (keyCode <= KeyCode.None && c == '\0')
			{
				return;
			}
			if (c == '\0' || c == '\t')
			{
				int num2 = KeycodeToVK(keyCode);
				if (num2 == (int)keyCode)
				{
					num2 = ((c == '\0') ? char.ToUpper(keyCode.ToString()[0]) : c);
				}
				blz_commerce_key_input_t obj5 = new blz_commerce_key_input_t
				{
					keyCode = num2,
					type = blz_commerce_scene_input_type_t,
					modifiers = (uint)num,
					character = c
				};
				battlenet_commerce.blz_commerce_browser_send_event(m_checkout.Sdk, blz_commerce_browser_event_type_t.KEYBOARD_EVENT, blz_commerce_key_input_t.getCPtr(obj5).Handle);
			}
			if (!blockedCharacters.Contains(c))
			{
				blz_commerce_character_input_t obj6 = new blz_commerce_character_input_t
				{
					character = c,
					modifiers = (uint)num
				};
				battlenet_commerce.blz_commerce_browser_send_event(m_checkout.Sdk, blz_commerce_browser_event_type_t.CHARACTER, blz_commerce_character_input_t.getCPtr(obj6).Handle);
			}
		}
		else
		{
			value((blz_commerce_scene_input_type_t == blz_commerce_scene_input_type_t.BLZ_COMMERCE_SCENE_PRESS) ? true : false);
		}
	}

	private blz_commerce_vec2d_t ConvertVec2d(Vec2D input)
	{
		return new blz_commerce_vec2d_t
		{
			x = input.x,
			y = input.y
		};
	}

	private static char SwapCharacter(char character)
	{
		return character switch
		{
			'\n' => '\r', 
			'\u0019' => '\t', 
			_ => character, 
		};
	}

	private static int KeycodeToVK(KeyCode kCode)
	{
		return kCode switch
		{
			KeyCode.Delete => 46, 
			KeyCode.Backspace => 8, 
			KeyCode.LeftArrow => 37, 
			KeyCode.RightArrow => 39, 
			KeyCode.UpArrow => 38, 
			KeyCode.DownArrow => 40, 
			KeyCode.Tab => 9, 
			KeyCode.Home => 36, 
			KeyCode.End => 35, 
			_ => (int)kCode, 
		};
	}
}
