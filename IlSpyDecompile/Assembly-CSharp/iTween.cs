using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iTween
{
	private delegate float EasingFunction(float start, float end, float value);

	private delegate void ApplyTween();

	public enum EaseType
	{
		easeInQuad,
		easeOutQuad,
		easeInOutQuad,
		easeInCubic,
		easeOutCubic,
		easeInOutCubic,
		easeInQuart,
		easeOutQuart,
		easeInOutQuart,
		easeInQuint,
		easeOutQuint,
		easeInOutQuint,
		easeInSine,
		easeOutSine,
		easeInOutSine,
		easeInExpo,
		easeOutExpo,
		easeInOutExpo,
		easeInCirc,
		easeOutCirc,
		easeInOutCirc,
		linear,
		spring,
		easeInBounce,
		easeOutBounce,
		easeInOutBounce,
		easeInBack,
		easeOutBack,
		easeInOutBack,
		easeInElastic,
		easeOutElastic,
		easeInOutElastic,
		punch,
		easeInSineOutExpo,
		easeOutElasticLight
	}

	private enum CallbackType
	{
		OnStart,
		OnUpdate,
		OnComplete,
		OnConflict
	}

	public enum LoopType
	{
		none,
		loop,
		pingPong
	}

	public enum NamedValueColor
	{
		_Color,
		_SpecColor,
		_Emission,
		_ReflectColor
	}

	public static class Defaults
	{
		public static float time = 1f;

		public static float delay = 0f;

		public static NamedValueColor namedColorValue = NamedValueColor._Color;

		public static LoopType loopType = LoopType.none;

		public static EaseType easeType = EaseType.easeOutExpo;

		public static float lookSpeed = 3f;

		public static bool isLocal = false;

		public static Space space = Space.Self;

		public static bool orientToPath = false;

		public static Color color = Color.white;

		public static float updateTimePercentage = 0.05f;

		public static float updateTime = 1f * updateTimePercentage;

		public static float lookAhead = 0.05f;

		public static bool useRealTime = false;

		public static Vector3 up = Vector3.up;
	}

	private class CRSpline
	{
		public Vector3[] pts;

		public CRSpline(params Vector3[] pts)
		{
			this.pts = new Vector3[pts.Length];
			Array.Copy(pts, this.pts, pts.Length);
		}

		public Vector3 Interp(float t)
		{
			int num = pts.Length - 3;
			int num2 = Mathf.Min(Mathf.FloorToInt(t * (float)num), num - 1);
			float num3 = t * (float)num - (float)num2;
			Vector3 vector = pts[num2];
			Vector3 vector2 = pts[num2 + 1];
			Vector3 vector3 = pts[num2 + 2];
			Vector3 vector4 = pts[num2 + 3];
			return 0.5f * ((-vector + 3f * vector2 - 3f * vector3 + vector4) * (num3 * num3 * num3) + (2f * vector - 5f * vector2 + 4f * vector3 - vector4) * (num3 * num3) + (-vector + vector3) * num3 + 2f * vector2);
		}
	}

	private static int nextId = 1;

	public int id;

	public string type;

	public string method;

	public EaseType easeType;

	public float time;

	public float delay;

	public LoopType loopType;

	public bool isRunning;

	public bool isPaused;

	public string _name;

	private bool waitForDelay;

	private float runningTime;

	private float percentage;

	private float delayStarted;

	private bool kinematic;

	private bool isLocal;

	private bool loop;

	private bool reverse;

	private bool physics;

	private Hashtable tweenArguments;

	private Space space;

	private EasingFunction ease;

	private ApplyTween apply;

	private AudioSource audioSource;

	private Vector3[] vector3s;

	private Vector2[] vector2s;

	private Color[,] colors;

	private float[] floats;

	private Rect[] rects;

	private CRSpline path;

	private Vector3 preUpdate;

	private Vector3 postUpdate;

	private NamedValueColor namedcolorvalue;

	private string namedColorValueString;

	private float lastRealTime;

	private bool useRealTime;

	public GameObject gameObject;

	public bool enabled = true;

	public bool activeLastTick;

	public bool destroyed;

	private static string[] CALLBACK_NAMES = new string[4] { "onstart", "onupdate", "oncomplete", "onconflict" };

	private static string[] CALLBACK_TARGET_NAMES = new string[4] { "onstarttarget", "onupdatetarget", "oncompletetarget", "onconflicttarget" };

	private static string[] CALLBACK_PARAMS_NAMES = new string[4] { "onstartparams", "onupdateparams", "oncompleteparams", "onconflictparams" };

	private static List<Material> m_MaterialTempList = new List<Material>();

	private static readonly string KEY_ID = "id";

	private Transform transform => gameObject.transform;

	private Renderer renderer => gameObject.GetComponent<Renderer>();

	private Light light => gameObject.GetComponent<Light>();

	private AudioSource audio => gameObject.GetComponent<AudioSource>();

	private Rigidbody rigidbody => gameObject.GetComponent<Rigidbody>();

	private UberText uberText => gameObject.GetComponent<UberText>();

	private bool activeInHierarchy
	{
		get
		{
			if (enabled && !destroyed && (bool)gameObject)
			{
				return gameObject.activeInHierarchy;
			}
			return false;
		}
	}

	private Component GetComponent(Type t)
	{
		return gameObject.GetComponent(t);
	}

	public static void Init(GameObject target)
	{
		MoveBy(target, Vector3.zero, 0f);
	}

	public static void ValueTo(GameObject target, Hashtable args)
	{
		args = CleanArgs(args);
		if (!args.Contains("onupdate") || !args.Contains("from") || !args.Contains("to"))
		{
			Debug.LogError("iTween Error: ValueTo() requires an 'onupdate' callback function and a 'from' and 'to' property.  The supplied 'onupdate' callback must accept a single argument that is the same type as the supplied 'from' and 'to' properties!");
			return;
		}
		args["type"] = "value";
		if (args["from"].GetType() == typeof(Vector2))
		{
			args["method"] = "vector2";
		}
		else if (args["from"].GetType() == typeof(Vector3))
		{
			args["method"] = "vector3";
		}
		else if (args["from"].GetType() == typeof(Rect))
		{
			args["method"] = "rect";
		}
		else if (args["from"].GetType() == typeof(float))
		{
			args["method"] = "float";
		}
		else
		{
			if (!(args["from"].GetType() == typeof(Color)))
			{
				Debug.LogError("iTween Error: ValueTo() only works with interpolating Vector3s, Vector2s, floats, ints, Rects and Colors!");
				return;
			}
			args["method"] = "color";
		}
		if (!args.Contains("easetype"))
		{
			args.Add("easetype", EaseType.linear);
		}
		Launch(target, args);
	}

	public static void FadeFrom(GameObject target, float alpha, float time)
	{
		FadeFrom(target, Hash("alpha", alpha, "time", time));
	}

	public static void FadeFrom(GameObject target, Hashtable args)
	{
		ColorFrom(target, args);
	}

	public static void FadeTo(GameObject target, float alpha, float time)
	{
		FadeTo(target, Hash("alpha", alpha, "time", time));
	}

	public static void FadeTo(GameObject target, Hashtable args)
	{
		ColorTo(target, args);
	}

	public static void ColorFrom(GameObject target, Color color, float time)
	{
		ColorFrom(target, Hash("color", color, "time", time));
	}

	public static void ColorFrom(GameObject target, Hashtable args)
	{
		Color color = default(Color);
		Color color2 = default(Color);
		args = CleanArgs(args);
		if (!args.Contains("includechildren") || (bool)args["includechildren"])
		{
			foreach (Transform item in target.transform)
			{
				Hashtable hashtable = (Hashtable)args.Clone();
				hashtable["ischild"] = true;
				ColorFrom(item.gameObject, hashtable);
			}
		}
		if (!args.Contains("easetype"))
		{
			args.Add("easetype", EaseType.linear);
		}
		color2 = (color = GetTargetColor(target));
		if (args.Contains("color"))
		{
			color = (Color)args["color"];
		}
		else
		{
			if (args.Contains("r"))
			{
				color.r = (float)args["r"];
			}
			if (args.Contains("g"))
			{
				color.g = (float)args["g"];
			}
			if (args.Contains("b"))
			{
				color.b = (float)args["b"];
			}
			if (args.Contains("a"))
			{
				color.a = (float)args["a"];
			}
		}
		if (args.Contains("amount"))
		{
			color.a = (float)args["amount"];
			args.Remove("amount");
		}
		else if (args.Contains("alpha"))
		{
			color.a = (float)args["alpha"];
			args.Remove("alpha");
		}
		Renderer component = target.GetComponent<Renderer>();
		UberText component2 = target.GetComponent<UberText>();
		if ((bool)component2)
		{
			component2.TextColor = color;
		}
		else if ((bool)component)
		{
			component.GetMaterial().color = color;
		}
		else if ((bool)target.GetComponent<Light>())
		{
			target.GetComponent<Light>().color = color;
		}
		args["color"] = color2;
		args["type"] = "color";
		args["method"] = "to";
		Launch(target, args);
	}

	public static void ColorTo(GameObject target, Color color, float time)
	{
		ColorTo(target, Hash("color", color, "time", time));
	}

	public static void ColorTo(GameObject target, Hashtable args)
	{
		args = CleanArgs(args);
		if (!args.Contains("includechildren") || (bool)args["includechildren"])
		{
			foreach (Transform item in target.transform)
			{
				Hashtable hashtable = (Hashtable)args.Clone();
				hashtable["ischild"] = true;
				ColorTo(item.gameObject, hashtable);
			}
		}
		if (!args.Contains("easetype"))
		{
			args.Add("easetype", EaseType.linear);
		}
		args["type"] = "color";
		args["method"] = "to";
		Launch(target, args);
	}

	public static void AudioFrom(GameObject target, float volume, float pitch, float time)
	{
		AudioFrom(target, Hash("volume", volume, "pitch", pitch, "time", time));
	}

	public static void AudioFrom(GameObject target, Hashtable args)
	{
		args = CleanArgs(args);
		AudioSource audioSource;
		if (args.Contains("audiosource"))
		{
			audioSource = (AudioSource)args["audiosource"];
		}
		else
		{
			if (!target.GetComponent(typeof(AudioSource)))
			{
				Debug.LogError("iTween Error: AudioFrom requires an AudioSource.");
				return;
			}
			audioSource = target.GetComponent<AudioSource>();
		}
		Vector2 vector = default(Vector2);
		Vector2 vector2 = default(Vector2);
		vector.x = (vector2.x = audioSource.volume);
		vector.y = (vector2.y = audioSource.pitch);
		if (args.Contains("volume"))
		{
			vector2.x = (float)args["volume"];
		}
		if (args.Contains("pitch"))
		{
			vector2.y = (float)args["pitch"];
		}
		audioSource.volume = vector2.x;
		audioSource.pitch = vector2.y;
		args["volume"] = vector.x;
		args["pitch"] = vector.y;
		if (!args.Contains("easetype"))
		{
			args.Add("easetype", EaseType.linear);
		}
		args["type"] = "audio";
		args["method"] = "to";
		Launch(target, args);
	}

	public static void AudioTo(GameObject target, float volume, float pitch, float time)
	{
		AudioTo(target, Hash("volume", volume, "pitch", pitch, "time", time));
	}

	public static void AudioTo(GameObject target, Hashtable args)
	{
		args = CleanArgs(args);
		if (!args.Contains("easetype"))
		{
			args.Add("easetype", EaseType.linear);
		}
		args["type"] = "audio";
		args["method"] = "to";
		Launch(target, args);
	}

	public static void Stab(GameObject target, AudioClip audioclip, float delay)
	{
		Stab(target, Hash("audioclip", audioclip, "delay", delay));
	}

	public static void Stab(GameObject target, Hashtable args)
	{
		args = CleanArgs(args);
		args["type"] = "stab";
		Launch(target, args);
	}

	public static void LookFrom(GameObject target, Vector3 looktarget, float time)
	{
		LookFrom(target, Hash("looktarget", looktarget, "time", time));
	}

	public static void LookFrom(GameObject target, Hashtable args)
	{
		args = CleanArgs(args);
		Vector3 eulerAngles = target.transform.eulerAngles;
		if (args["looktarget"].GetType() == typeof(Transform))
		{
			target.transform.LookAt((Transform)args["looktarget"], ((Vector3?)args["up"]) ?? Defaults.up);
		}
		else if (args["looktarget"].GetType() == typeof(Vector3))
		{
			target.transform.LookAt((Vector3)args["looktarget"], ((Vector3?)args["up"]) ?? Defaults.up);
		}
		if (args.Contains("axis"))
		{
			Vector3 eulerAngles2 = target.transform.eulerAngles;
			switch ((string)args["axis"])
			{
			case "x":
				eulerAngles2.y = eulerAngles.y;
				eulerAngles2.z = eulerAngles.z;
				break;
			case "y":
				eulerAngles2.x = eulerAngles.x;
				eulerAngles2.z = eulerAngles.z;
				break;
			case "z":
				eulerAngles2.x = eulerAngles.x;
				eulerAngles2.y = eulerAngles.y;
				break;
			}
			target.transform.eulerAngles = eulerAngles2;
		}
		args["rotation"] = eulerAngles;
		args["type"] = "rotate";
		args["method"] = "to";
		Launch(target, args);
	}

	public static void LookTo(GameObject target, Vector3 looktarget, float time)
	{
		LookTo(target, Hash("looktarget", looktarget, "time", time));
	}

	public static void LookTo(GameObject target, Hashtable args)
	{
		args = CleanArgs(args);
		if (args.Contains("looktarget") && args["looktarget"].GetType() == typeof(Transform))
		{
			Transform transform = (Transform)args["looktarget"];
			args["position"] = new Vector3(transform.position.x, transform.position.y, transform.position.z);
			args["rotation"] = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
		}
		args["type"] = "look";
		args["method"] = "to";
		Launch(target, args);
	}

	public static void MoveTo(GameObject target, Vector3 position, float time)
	{
		MoveTo(target, Hash("position", position, "time", time));
	}

	public static void MoveTo(GameObject target, Hashtable args)
	{
		args = CleanArgs(args);
		if (args.Contains("position") && args["position"].GetType() == typeof(Transform))
		{
			Transform transform = (Transform)args["position"];
			args["position"] = new Vector3(transform.position.x, transform.position.y, transform.position.z);
			args["rotation"] = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
			args["scale"] = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
		}
		args["type"] = "move";
		args["method"] = "to";
		Launch(target, args);
	}

	public static void MoveFrom(GameObject target, Vector3 position, float time)
	{
		MoveFrom(target, Hash("position", position, "time", time));
	}

	public static void MoveFrom(GameObject target, Hashtable args)
	{
		args = CleanArgs(args);
		bool flag = ((!args.Contains("islocal")) ? Defaults.isLocal : ((bool)args["islocal"]));
		if (args.Contains("path"))
		{
			Vector3[] array2;
			if (args["path"].GetType() == typeof(Vector3[]))
			{
				Vector3[] array = (Vector3[])args["path"];
				array2 = new Vector3[array.Length];
				Array.Copy(array, array2, array.Length);
			}
			else
			{
				Transform[] array3 = (Transform[])args["path"];
				array2 = new Vector3[array3.Length];
				for (int i = 0; i < array3.Length; i++)
				{
					array2[i] = array3[i].position;
				}
			}
			if (array2[array2.Length - 1] != target.transform.position)
			{
				Vector3[] array4 = new Vector3[array2.Length + 1];
				Array.Copy(array2, array4, array2.Length);
				if (flag)
				{
					array4[array4.Length - 1] = target.transform.localPosition;
					target.transform.localPosition = array4[0];
				}
				else
				{
					array4[array4.Length - 1] = target.transform.position;
					target.transform.position = array4[0];
				}
				args["path"] = array4;
			}
			else
			{
				if (flag)
				{
					target.transform.localPosition = array2[0];
				}
				else
				{
					target.transform.position = array2[0];
				}
				args["path"] = array2;
			}
		}
		else
		{
			Vector3 vector;
			Vector3 vector2 = ((!flag) ? (vector = target.transform.position) : (vector = target.transform.localPosition));
			if (args.Contains("position"))
			{
				if (args["position"].GetType() == typeof(Transform))
				{
					vector = ((Transform)args["position"]).position;
				}
				else if (args["position"].GetType() == typeof(Vector3))
				{
					vector = (Vector3)args["position"];
				}
			}
			else
			{
				if (args.Contains("x"))
				{
					vector.x = (float)args["x"];
				}
				if (args.Contains("y"))
				{
					vector.y = (float)args["y"];
				}
				if (args.Contains("z"))
				{
					vector.z = (float)args["z"];
				}
			}
			if (flag)
			{
				target.transform.localPosition = vector;
			}
			else
			{
				target.transform.position = vector;
			}
			args["position"] = vector2;
		}
		args["type"] = "move";
		args["method"] = "to";
		Launch(target, args);
	}

	public static void MoveAdd(GameObject target, Vector3 amount, float time)
	{
		MoveAdd(target, Hash("amount", amount, "time", time));
	}

	public static void MoveAdd(GameObject target, Hashtable args)
	{
		args = CleanArgs(args);
		args["type"] = "move";
		args["method"] = "add";
		Launch(target, args);
	}

	public static void MoveBy(GameObject target, Vector3 amount, float time)
	{
		MoveBy(target, Hash("amount", amount, "time", time));
	}

	public static void MoveBy(GameObject target, Hashtable args)
	{
		args = CleanArgs(args);
		args["type"] = "move";
		args["method"] = "by";
		Launch(target, args);
	}

	public static void ScaleTo(GameObject target, Vector3 scale, float time)
	{
		ScaleTo(target, Hash("scale", scale, "time", time));
	}

	public static void ScaleTo(GameObject target, Hashtable args)
	{
		args = CleanArgs(args);
		if (args.Contains("scale") && args["scale"].GetType() == typeof(Transform))
		{
			Transform transform = (Transform)args["scale"];
			args["position"] = new Vector3(transform.position.x, transform.position.y, transform.position.z);
			args["rotation"] = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
			args["scale"] = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
		}
		args["type"] = "scale";
		args["method"] = "to";
		Launch(target, args);
	}

	public static void ScaleFrom(GameObject target, Vector3 scale, float time)
	{
		ScaleFrom(target, Hash("scale", scale, "time", time));
	}

	public static void ScaleFrom(GameObject target, Hashtable args)
	{
		args = CleanArgs(args);
		Vector3 localScale;
		Vector3 vector = (localScale = target.transform.localScale);
		if (args.Contains("scale"))
		{
			if (args["scale"].GetType() == typeof(Transform))
			{
				localScale = ((Transform)args["scale"]).localScale;
			}
			else if (args["scale"].GetType() == typeof(Vector3))
			{
				localScale = (Vector3)args["scale"];
			}
		}
		else
		{
			if (args.Contains("x"))
			{
				localScale.x = (float)args["x"];
			}
			if (args.Contains("y"))
			{
				localScale.y = (float)args["y"];
			}
			if (args.Contains("z"))
			{
				localScale.z = (float)args["z"];
			}
		}
		target.transform.localScale = localScale;
		args["scale"] = vector;
		args["type"] = "scale";
		args["method"] = "to";
		Launch(target, args);
	}

	public static void ScaleAdd(GameObject target, Vector3 amount, float time)
	{
		ScaleAdd(target, Hash("amount", amount, "time", time));
	}

	public static void ScaleAdd(GameObject target, Hashtable args)
	{
		args = CleanArgs(args);
		args["type"] = "scale";
		args["method"] = "add";
		Launch(target, args);
	}

	public static void ScaleBy(GameObject target, Vector3 amount, float time)
	{
		ScaleBy(target, Hash("amount", amount, "time", time));
	}

	public static void ScaleBy(GameObject target, Hashtable args)
	{
		args = CleanArgs(args);
		args["type"] = "scale";
		args["method"] = "by";
		Launch(target, args);
	}

	public static void RotateTo(GameObject target, Vector3 rotation, float time)
	{
		RotateTo(target, Hash("rotation", rotation, "time", time));
	}

	public static void RotateTo(GameObject target, Hashtable args)
	{
		args = CleanArgs(args);
		if (args.Contains("rotation") && args["rotation"].GetType() == typeof(Transform))
		{
			Transform transform = (Transform)args["rotation"];
			args["position"] = new Vector3(transform.position.x, transform.position.y, transform.position.z);
			args["rotation"] = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
			args["scale"] = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
		}
		args["type"] = "rotate";
		args["method"] = "to";
		Launch(target, args);
	}

	public static void RotateFrom(GameObject target, Vector3 rotation, float time)
	{
		RotateFrom(target, Hash("rotation", rotation, "time", time));
	}

	public static void RotateFrom(GameObject target, Hashtable args)
	{
		args = CleanArgs(args);
		bool flag = ((!args.Contains("islocal")) ? Defaults.isLocal : ((bool)args["islocal"]));
		Vector3 vector;
		Vector3 vector2 = ((!flag) ? (vector = target.transform.eulerAngles) : (vector = target.transform.localEulerAngles));
		if (args.Contains("rotation"))
		{
			if (args["rotation"].GetType() == typeof(Transform))
			{
				vector = ((Transform)args["rotation"]).eulerAngles;
			}
			else if (args["rotation"].GetType() == typeof(Vector3))
			{
				vector = (Vector3)args["rotation"];
			}
		}
		else
		{
			if (args.Contains("x"))
			{
				vector.x = (float)args["x"];
			}
			if (args.Contains("y"))
			{
				vector.y = (float)args["y"];
			}
			if (args.Contains("z"))
			{
				vector.z = (float)args["z"];
			}
		}
		if (flag)
		{
			target.transform.localEulerAngles = vector;
		}
		else
		{
			target.transform.eulerAngles = vector;
		}
		args["rotation"] = vector2;
		args["type"] = "rotate";
		args["method"] = "to";
		Launch(target, args);
	}

	public static void RotateAdd(GameObject target, Vector3 amount, float time)
	{
		RotateAdd(target, Hash("amount", amount, "time", time));
	}

	public static void RotateAdd(GameObject target, Hashtable args)
	{
		args = CleanArgs(args);
		args["type"] = "rotate";
		args["method"] = "add";
		Launch(target, args);
	}

	public static void RotateBy(GameObject target, Vector3 amount, float time)
	{
		RotateBy(target, Hash("amount", amount, "time", time));
	}

	public static void RotateBy(GameObject target, Hashtable args)
	{
		args = CleanArgs(args);
		args["type"] = "rotate";
		args["method"] = "by";
		Launch(target, args);
	}

	public static void ShakePosition(GameObject target, Vector3 amount, float time)
	{
		ShakePosition(target, Hash("amount", amount, "time", time));
	}

	public static void ShakePosition(GameObject target, Hashtable args)
	{
		args = CleanArgs(args);
		args["type"] = "shake";
		args["method"] = "position";
		Launch(target, args);
	}

	public static void ShakeScale(GameObject target, Vector3 amount, float time)
	{
		ShakeScale(target, Hash("amount", amount, "time", time));
	}

	public static void ShakeScale(GameObject target, Hashtable args)
	{
		args = CleanArgs(args);
		args["type"] = "shake";
		args["method"] = "scale";
		Launch(target, args);
	}

	public static void ShakeRotation(GameObject target, Vector3 amount, float time)
	{
		ShakeRotation(target, Hash("amount", amount, "time", time));
	}

	public static void ShakeRotation(GameObject target, Hashtable args)
	{
		args = CleanArgs(args);
		args["type"] = "shake";
		args["method"] = "rotation";
		Launch(target, args);
	}

	public static void PunchPosition(GameObject target, Vector3 amount, float time)
	{
		PunchPosition(target, Hash("amount", amount, "time", time));
	}

	public static void PunchPosition(GameObject target, Hashtable args)
	{
		args = CleanArgs(args);
		args["type"] = "punch";
		args["method"] = "position";
		args["easetype"] = EaseType.punch;
		Launch(target, args);
	}

	public static void PunchRotation(GameObject target, Vector3 amount, float time)
	{
		PunchRotation(target, Hash("amount", amount, "time", time));
	}

	public static void PunchRotation(GameObject target, Hashtable args)
	{
		args = CleanArgs(args);
		args["type"] = "punch";
		args["method"] = "rotation";
		args["easetype"] = EaseType.punch;
		Launch(target, args);
	}

	public static void PunchScale(GameObject target, Vector3 amount, float time)
	{
		PunchScale(target, Hash("amount", amount, "time", time));
	}

	public static void PunchScale(GameObject target, Hashtable args)
	{
		args = CleanArgs(args);
		args["type"] = "punch";
		args["method"] = "scale";
		args["easetype"] = EaseType.punch;
		Launch(target, args);
	}

	public static void Timer(GameObject target, Hashtable args)
	{
		args = CleanArgs(args);
		args["type"] = "timer";
		Launch(target, args);
	}

	private void GenerateTargets()
	{
		switch (type)
		{
		case "value":
			switch (method)
			{
			case "float":
				GenerateFloatTargets();
				apply = ApplyFloatTargets;
				break;
			case "vector2":
				GenerateVector2Targets();
				apply = ApplyVector2Targets;
				break;
			case "vector3":
				GenerateVector3Targets();
				apply = ApplyVector3Targets;
				break;
			case "color":
				GenerateColorTargets();
				apply = ApplyColorTargets;
				break;
			case "rect":
				GenerateRectTargets();
				apply = ApplyRectTargets;
				break;
			}
			break;
		case "color":
		{
			string text = method;
			if (text == "to")
			{
				GenerateColorToTargets();
				apply = ApplyColorToTargets;
			}
			break;
		}
		case "audio":
		{
			string text = method;
			if (text == "to")
			{
				GenerateAudioToTargets();
				apply = ApplyAudioToTargets;
			}
			break;
		}
		case "move":
			switch (method)
			{
			case "to":
				if (tweenArguments.Contains("path"))
				{
					GenerateMoveToPathTargets();
					apply = ApplyMoveToPathTargets;
				}
				else
				{
					GenerateMoveToTargets();
					apply = ApplyMoveToTargets;
				}
				break;
			case "by":
			case "add":
				GenerateMoveByTargets();
				apply = ApplyMoveByTargets;
				break;
			}
			break;
		case "scale":
			switch (method)
			{
			case "to":
				GenerateScaleToTargets();
				apply = ApplyScaleToTargets;
				break;
			case "by":
				GenerateScaleByTargets();
				apply = ApplyScaleToTargets;
				break;
			case "add":
				GenerateScaleAddTargets();
				apply = ApplyScaleToTargets;
				break;
			}
			break;
		case "rotate":
			switch (method)
			{
			case "to":
				GenerateRotateToTargets();
				apply = ApplyRotateToTargets;
				break;
			case "add":
				GenerateRotateAddTargets();
				apply = ApplyRotateAddTargets;
				break;
			case "by":
				GenerateRotateByTargets();
				apply = ApplyRotateAddTargets;
				break;
			}
			break;
		case "shake":
			switch (method)
			{
			case "position":
				GenerateShakePositionTargets();
				apply = ApplyShakePositionTargets;
				break;
			case "scale":
				GenerateShakeScaleTargets();
				apply = ApplyShakeScaleTargets;
				break;
			case "rotation":
				GenerateShakeRotationTargets();
				apply = ApplyShakeRotationTargets;
				break;
			}
			break;
		case "punch":
			switch (method)
			{
			case "position":
				GeneratePunchPositionTargets();
				apply = ApplyPunchPositionTargets;
				break;
			case "rotation":
				GeneratePunchRotationTargets();
				apply = ApplyPunchRotationTargets;
				break;
			case "scale":
				GeneratePunchScaleTargets();
				apply = ApplyPunchScaleTargets;
				break;
			}
			break;
		case "look":
		{
			string text = method;
			if (text == "to")
			{
				GenerateLookToTargets();
				apply = ApplyLookToTargets;
			}
			break;
		}
		case "stab":
			GenerateStabTargets();
			apply = ApplyStabTargets;
			break;
		}
	}

	private void GenerateRectTargets()
	{
		rects = new Rect[3];
		rects[0] = (Rect)tweenArguments["from"];
		rects[1] = (Rect)tweenArguments["to"];
	}

	private void GenerateColorTargets()
	{
		colors = new Color[1, 3];
		colors[0, 0] = (Color)tweenArguments["from"];
		colors[0, 1] = (Color)tweenArguments["to"];
	}

	private void GenerateVector3Targets()
	{
		vector3s = new Vector3[3];
		vector3s[0] = (Vector3)tweenArguments["from"];
		vector3s[1] = (Vector3)tweenArguments["to"];
		if (tweenArguments.Contains("speed"))
		{
			float num = Math.Abs(Vector3.Distance(vector3s[0], vector3s[1]));
			time = num / (float)tweenArguments["speed"];
		}
	}

	private void GenerateVector2Targets()
	{
		vector2s = new Vector2[3];
		vector2s[0] = (Vector2)tweenArguments["from"];
		vector2s[1] = (Vector2)tweenArguments["to"];
		if (tweenArguments.Contains("speed"))
		{
			Vector3 a = new Vector3(vector2s[0].x, vector2s[0].y, 0f);
			Vector3 b = new Vector3(vector2s[1].x, vector2s[1].y, 0f);
			float num = Math.Abs(Vector3.Distance(a, b));
			time = num / (float)tweenArguments["speed"];
		}
	}

	private void GenerateFloatTargets()
	{
		floats = new float[3];
		floats[0] = (float)tweenArguments["from"];
		floats[1] = (float)tweenArguments["to"];
		if (tweenArguments.Contains("speed"))
		{
			float num = Math.Abs(floats[0] - floats[1]);
			time = num / (float)tweenArguments["speed"];
		}
	}

	private void GenerateColorToTargets()
	{
		if ((bool)GetComponent(typeof(UberText)) && !uberText.RenderToTexture)
		{
			colors = new Color[1, 3];
			colors[0, 0] = (colors[0, 1] = uberText.TextColor);
		}
		else if ((bool)renderer)
		{
			int num = 0;
			m_MaterialTempList.Clear();
			renderer.GetSharedMaterials(m_MaterialTempList);
			colors = new Color[m_MaterialTempList.Count, 3];
			for (int i = 0; i < m_MaterialTempList.Count; i++)
			{
				if ((bool)m_MaterialTempList[i] && m_MaterialTempList[i].HasProperty(namedColorValueString))
				{
					colors[i, 0] = m_MaterialTempList[i].GetColor(namedColorValueString);
					colors[i, 1] = m_MaterialTempList[i].GetColor(namedColorValueString);
					num++;
				}
			}
		}
		else if ((bool)light)
		{
			colors = new Color[1, 3];
			colors[0, 0] = (colors[0, 1] = light.color);
		}
		else
		{
			colors = new Color[1, 3];
		}
		if (tweenArguments.Contains("color"))
		{
			for (int j = 0; j < colors.GetLength(0); j++)
			{
				colors[j, 1] = (Color)tweenArguments["color"];
			}
		}
		else
		{
			if (tweenArguments.Contains("r"))
			{
				for (int k = 0; k < colors.GetLength(0); k++)
				{
					colors[k, 1].r = (float)tweenArguments["r"];
				}
			}
			if (tweenArguments.Contains("g"))
			{
				for (int l = 0; l < colors.GetLength(0); l++)
				{
					colors[l, 1].g = (float)tweenArguments["g"];
				}
			}
			if (tweenArguments.Contains("b"))
			{
				for (int m = 0; m < colors.GetLength(0); m++)
				{
					colors[m, 1].b = (float)tweenArguments["b"];
				}
			}
			if (tweenArguments.Contains("a"))
			{
				for (int n = 0; n < colors.GetLength(0); n++)
				{
					colors[n, 1].a = (float)tweenArguments["a"];
				}
			}
		}
		if (tweenArguments.Contains("amount"))
		{
			for (int num2 = 0; num2 < colors.GetLength(0); num2++)
			{
				colors[num2, 1].a = (float)tweenArguments["amount"];
			}
		}
		else if (tweenArguments.Contains("alpha"))
		{
			for (int num3 = 0; num3 < colors.GetLength(0); num3++)
			{
				colors[num3, 1].a = (float)tweenArguments["alpha"];
			}
		}
	}

	private void GenerateAudioToTargets()
	{
		vector2s = new Vector2[3];
		if (tweenArguments.Contains("audiosource"))
		{
			audioSource = (AudioSource)tweenArguments["audiosource"];
		}
		else if ((bool)GetComponent(typeof(AudioSource)))
		{
			audioSource = audio;
		}
		else
		{
			Debug.LogError("iTween Error: AudioTo requires an AudioSource.");
			Dispose();
		}
		vector2s[0] = (vector2s[1] = new Vector2(audioSource.volume, audioSource.pitch));
		if (tweenArguments.Contains("volume"))
		{
			vector2s[1].x = (float)tweenArguments["volume"];
		}
		if (tweenArguments.Contains("pitch"))
		{
			vector2s[1].y = (float)tweenArguments["pitch"];
		}
	}

	private void GenerateStabTargets()
	{
		if (tweenArguments.Contains("audiosource"))
		{
			audioSource = (AudioSource)tweenArguments["audiosource"];
		}
		else if ((bool)GetComponent(typeof(AudioSource)))
		{
			audioSource = audio;
		}
		else
		{
			gameObject.AddComponent(typeof(AudioSource));
			audioSource = audio;
			audioSource.playOnAwake = false;
		}
		audioSource.clip = (AudioClip)tweenArguments["audioclip"];
		if (tweenArguments.Contains("pitch"))
		{
			audioSource.pitch = (float)tweenArguments["pitch"];
		}
		if (tweenArguments.Contains("volume"))
		{
			audioSource.volume = (float)tweenArguments["volume"];
		}
		time = audioSource.clip.length / audioSource.pitch;
	}

	private void GenerateLookToTargets()
	{
		vector3s = new Vector3[3];
		vector3s[0] = transform.eulerAngles;
		if (tweenArguments.Contains("looktarget"))
		{
			if (tweenArguments["looktarget"].GetType() == typeof(Transform))
			{
				transform.LookAt((Transform)tweenArguments["looktarget"], ((Vector3?)tweenArguments["up"]) ?? Defaults.up);
			}
			else if (tweenArguments["looktarget"].GetType() == typeof(Vector3))
			{
				transform.LookAt((Vector3)tweenArguments["looktarget"], ((Vector3?)tweenArguments["up"]) ?? Defaults.up);
			}
		}
		else
		{
			Debug.LogError("iTween Error: LookTo needs a 'looktarget' property!");
			Dispose();
		}
		vector3s[1] = transform.eulerAngles;
		transform.eulerAngles = vector3s[0];
		if (tweenArguments.Contains("axis"))
		{
			switch ((string)tweenArguments["axis"])
			{
			case "x":
				vector3s[1].y = vector3s[0].y;
				vector3s[1].z = vector3s[0].z;
				break;
			case "y":
				vector3s[1].x = vector3s[0].x;
				vector3s[1].z = vector3s[0].z;
				break;
			case "z":
				vector3s[1].x = vector3s[0].x;
				vector3s[1].y = vector3s[0].y;
				break;
			}
		}
		vector3s[1] = new Vector3(clerp(vector3s[0].x, vector3s[1].x, 1f), clerp(vector3s[0].y, vector3s[1].y, 1f), clerp(vector3s[0].z, vector3s[1].z, 1f));
		if (tweenArguments.Contains("speed"))
		{
			float num = Math.Abs(Vector3.Distance(vector3s[0], vector3s[1]));
			time = num / (float)tweenArguments["speed"];
		}
	}

	private void GenerateMoveToPathTargets()
	{
		Vector3[] array2;
		if (tweenArguments["path"].GetType() == typeof(Vector3[]))
		{
			Vector3[] array = (Vector3[])tweenArguments["path"];
			if (array.Length == 1)
			{
				Debug.LogError("iTween Error: Attempting a path movement with MoveTo requires an array of more than 1 entry!");
				Dispose();
			}
			array2 = new Vector3[array.Length];
			Array.Copy(array, array2, array.Length);
		}
		else
		{
			Transform[] array3 = (Transform[])tweenArguments["path"];
			if (array3.Length == 1)
			{
				Debug.LogError("iTween Error: Attempting a path movement with MoveTo requires an array of more than 1 entry!");
				Dispose();
			}
			array2 = new Vector3[array3.Length];
			for (int i = 0; i < array3.Length; i++)
			{
				array2[i] = array3[i].position;
			}
		}
		bool flag;
		int num;
		if (transform.position != array2[0])
		{
			if (!tweenArguments.Contains("movetopath") || (bool)tweenArguments["movetopath"])
			{
				flag = true;
				num = 3;
			}
			else
			{
				flag = false;
				num = 2;
			}
		}
		else
		{
			flag = false;
			num = 2;
		}
		vector3s = new Vector3[array2.Length + num];
		if (flag)
		{
			vector3s[1] = transform.position;
			num = 2;
		}
		else
		{
			num = 1;
		}
		Array.Copy(array2, 0, vector3s, num, array2.Length);
		vector3s[0] = vector3s[1] + (vector3s[1] - vector3s[2]);
		vector3s[vector3s.Length - 1] = vector3s[vector3s.Length - 2] + (vector3s[vector3s.Length - 2] - vector3s[vector3s.Length - 3]);
		if (vector3s[1] == vector3s[vector3s.Length - 2])
		{
			Vector3[] array4 = new Vector3[vector3s.Length];
			Array.Copy(vector3s, array4, vector3s.Length);
			array4[0] = array4[array4.Length - 3];
			array4[array4.Length - 1] = array4[2];
			vector3s = new Vector3[array4.Length];
			Array.Copy(array4, vector3s, array4.Length);
		}
		path = new CRSpline(vector3s);
		if (tweenArguments.Contains("speed"))
		{
			float num2 = PathLength(vector3s);
			time = num2 / (float)tweenArguments["speed"];
		}
	}

	private void GenerateMoveToTargets()
	{
		vector3s = new Vector3[3];
		if (isLocal)
		{
			vector3s[0] = (vector3s[1] = this.transform.localPosition);
		}
		else
		{
			vector3s[0] = (vector3s[1] = this.transform.position);
		}
		if (tweenArguments.Contains("position"))
		{
			if (tweenArguments["position"].GetType() == typeof(Transform))
			{
				Transform transform = (Transform)tweenArguments["position"];
				vector3s[1] = transform.position;
			}
			else if (tweenArguments["position"].GetType() == typeof(Vector3))
			{
				vector3s[1] = (Vector3)tweenArguments["position"];
			}
		}
		else
		{
			if (tweenArguments.Contains("x"))
			{
				vector3s[1].x = (float)tweenArguments["x"];
			}
			if (tweenArguments.Contains("y"))
			{
				vector3s[1].y = (float)tweenArguments["y"];
			}
			if (tweenArguments.Contains("z"))
			{
				vector3s[1].z = (float)tweenArguments["z"];
			}
		}
		if (tweenArguments.Contains("orienttopath") && (bool)tweenArguments["orienttopath"])
		{
			tweenArguments["looktarget"] = vector3s[1];
		}
		if (tweenArguments.Contains("speed"))
		{
			float num = Math.Abs(Vector3.Distance(vector3s[0], vector3s[1]));
			time = num / (float)tweenArguments["speed"];
		}
	}

	private void GenerateMoveByTargets()
	{
		vector3s = new Vector3[6];
		vector3s[4] = transform.eulerAngles;
		vector3s[0] = (vector3s[1] = (vector3s[3] = transform.position));
		if (tweenArguments.Contains("amount"))
		{
			vector3s[1] = vector3s[0] + (Vector3)tweenArguments["amount"];
		}
		else
		{
			if (tweenArguments.Contains("x"))
			{
				vector3s[1].x = vector3s[0].x + (float)tweenArguments["x"];
			}
			if (tweenArguments.Contains("y"))
			{
				vector3s[1].y = vector3s[0].y + (float)tweenArguments["y"];
			}
			if (tweenArguments.Contains("z"))
			{
				vector3s[1].z = vector3s[0].z + (float)tweenArguments["z"];
			}
		}
		transform.Translate(vector3s[1], space);
		vector3s[5] = transform.position;
		transform.position = vector3s[0];
		if (tweenArguments.Contains("orienttopath") && (bool)tweenArguments["orienttopath"])
		{
			tweenArguments["looktarget"] = vector3s[1];
		}
		if (tweenArguments.Contains("speed"))
		{
			float num = Math.Abs(Vector3.Distance(vector3s[0], vector3s[1]));
			time = num / (float)tweenArguments["speed"];
		}
	}

	private void GenerateScaleToTargets()
	{
		vector3s = new Vector3[3];
		vector3s[0] = (vector3s[1] = this.transform.localScale);
		if (tweenArguments.Contains("scale"))
		{
			if (tweenArguments["scale"].GetType() == typeof(Transform))
			{
				Transform transform = (Transform)tweenArguments["scale"];
				vector3s[1] = transform.localScale;
			}
			else if (tweenArguments["scale"].GetType() == typeof(Vector3))
			{
				vector3s[1] = (Vector3)tweenArguments["scale"];
			}
		}
		else
		{
			if (tweenArguments.Contains("x"))
			{
				vector3s[1].x = (float)tweenArguments["x"];
			}
			if (tweenArguments.Contains("y"))
			{
				vector3s[1].y = (float)tweenArguments["y"];
			}
			if (tweenArguments.Contains("z"))
			{
				vector3s[1].z = (float)tweenArguments["z"];
			}
		}
		if (tweenArguments.Contains("speed"))
		{
			float num = Math.Abs(Vector3.Distance(vector3s[0], vector3s[1]));
			time = num / (float)tweenArguments["speed"];
		}
	}

	private void GenerateScaleByTargets()
	{
		vector3s = new Vector3[3];
		vector3s[0] = (vector3s[1] = transform.localScale);
		if (tweenArguments.Contains("amount"))
		{
			vector3s[1] = Vector3.Scale(vector3s[1], (Vector3)tweenArguments["amount"]);
		}
		else
		{
			if (tweenArguments.Contains("x"))
			{
				vector3s[1].x *= (float)tweenArguments["x"];
			}
			if (tweenArguments.Contains("y"))
			{
				vector3s[1].y *= (float)tweenArguments["y"];
			}
			if (tweenArguments.Contains("z"))
			{
				vector3s[1].z *= (float)tweenArguments["z"];
			}
		}
		if (tweenArguments.Contains("speed"))
		{
			float num = Math.Abs(Vector3.Distance(vector3s[0], vector3s[1]));
			time = num / (float)tweenArguments["speed"];
		}
	}

	private void GenerateScaleAddTargets()
	{
		vector3s = new Vector3[3];
		vector3s[0] = (vector3s[1] = transform.localScale);
		if (tweenArguments.Contains("amount"))
		{
			vector3s[1] += (Vector3)tweenArguments["amount"];
		}
		else
		{
			if (tweenArguments.Contains("x"))
			{
				vector3s[1].x += (float)tweenArguments["x"];
			}
			if (tweenArguments.Contains("y"))
			{
				vector3s[1].y += (float)tweenArguments["y"];
			}
			if (tweenArguments.Contains("z"))
			{
				vector3s[1].z += (float)tweenArguments["z"];
			}
		}
		if (tweenArguments.Contains("speed"))
		{
			float num = Math.Abs(Vector3.Distance(vector3s[0], vector3s[1]));
			time = num / (float)tweenArguments["speed"];
		}
	}

	private void GenerateRotateToTargets()
	{
		vector3s = new Vector3[3];
		if (isLocal)
		{
			vector3s[0] = (vector3s[1] = this.transform.localEulerAngles);
		}
		else
		{
			vector3s[0] = (vector3s[1] = this.transform.eulerAngles);
		}
		if (tweenArguments.Contains("rotation"))
		{
			if (tweenArguments["rotation"].GetType() == typeof(Transform))
			{
				Transform transform = (Transform)tweenArguments["rotation"];
				vector3s[1] = transform.eulerAngles;
			}
			else if (tweenArguments["rotation"].GetType() == typeof(Vector3))
			{
				vector3s[1] = (Vector3)tweenArguments["rotation"];
			}
		}
		else
		{
			if (tweenArguments.Contains("x"))
			{
				vector3s[1].x = (float)tweenArguments["x"];
			}
			if (tweenArguments.Contains("y"))
			{
				vector3s[1].y = (float)tweenArguments["y"];
			}
			if (tweenArguments.Contains("z"))
			{
				vector3s[1].z = (float)tweenArguments["z"];
			}
		}
		vector3s[1] = new Vector3(clerp(vector3s[0].x, vector3s[1].x, 1f), clerp(vector3s[0].y, vector3s[1].y, 1f), clerp(vector3s[0].z, vector3s[1].z, 1f));
		if (tweenArguments.Contains("speed"))
		{
			float num = Math.Abs(Vector3.Distance(vector3s[0], vector3s[1]));
			time = num / (float)tweenArguments["speed"];
		}
	}

	private void GenerateRotateAddTargets()
	{
		vector3s = new Vector3[5];
		vector3s[0] = (vector3s[1] = (vector3s[3] = transform.eulerAngles));
		if (tweenArguments.Contains("amount"))
		{
			vector3s[1] += (Vector3)tweenArguments["amount"];
		}
		else
		{
			if (tweenArguments.Contains("x"))
			{
				vector3s[1].x += (float)tweenArguments["x"];
			}
			if (tweenArguments.Contains("y"))
			{
				vector3s[1].y += (float)tweenArguments["y"];
			}
			if (tweenArguments.Contains("z"))
			{
				vector3s[1].z += (float)tweenArguments["z"];
			}
		}
		if (tweenArguments.Contains("speed"))
		{
			float num = Math.Abs(Vector3.Distance(vector3s[0], vector3s[1]));
			time = num / (float)tweenArguments["speed"];
		}
	}

	private void GenerateRotateByTargets()
	{
		vector3s = new Vector3[4];
		vector3s[0] = (vector3s[1] = (vector3s[3] = transform.eulerAngles));
		if (tweenArguments.Contains("amount"))
		{
			vector3s[1] += Vector3.Scale((Vector3)tweenArguments["amount"], new Vector3(360f, 360f, 360f));
		}
		else
		{
			if (tweenArguments.Contains("x"))
			{
				vector3s[1].x += 360f * (float)tweenArguments["x"];
			}
			if (tweenArguments.Contains("y"))
			{
				vector3s[1].y += 360f * (float)tweenArguments["y"];
			}
			if (tweenArguments.Contains("z"))
			{
				vector3s[1].z += 360f * (float)tweenArguments["z"];
			}
		}
		if (tweenArguments.Contains("speed"))
		{
			float num = Math.Abs(Vector3.Distance(vector3s[0], vector3s[1]));
			time = num / (float)tweenArguments["speed"];
		}
	}

	private void GenerateShakePositionTargets()
	{
		vector3s = new Vector3[4];
		vector3s[3] = transform.eulerAngles;
		if (isLocal)
		{
			vector3s[0] = transform.localPosition;
		}
		else
		{
			vector3s[0] = transform.position;
		}
		if (tweenArguments.Contains("amount"))
		{
			vector3s[1] = (Vector3)tweenArguments["amount"];
			return;
		}
		if (tweenArguments.Contains("x"))
		{
			vector3s[1].x = (float)tweenArguments["x"];
		}
		if (tweenArguments.Contains("y"))
		{
			vector3s[1].y = (float)tweenArguments["y"];
		}
		if (tweenArguments.Contains("z"))
		{
			vector3s[1].z = (float)tweenArguments["z"];
		}
	}

	private void GenerateShakeScaleTargets()
	{
		vector3s = new Vector3[3];
		vector3s[0] = transform.localScale;
		if (tweenArguments.Contains("amount"))
		{
			vector3s[1] = (Vector3)tweenArguments["amount"];
			return;
		}
		if (tweenArguments.Contains("x"))
		{
			vector3s[1].x = (float)tweenArguments["x"];
		}
		if (tweenArguments.Contains("y"))
		{
			vector3s[1].y = (float)tweenArguments["y"];
		}
		if (tweenArguments.Contains("z"))
		{
			vector3s[1].z = (float)tweenArguments["z"];
		}
	}

	private void GenerateShakeRotationTargets()
	{
		vector3s = new Vector3[3];
		vector3s[0] = transform.eulerAngles;
		if (tweenArguments.Contains("amount"))
		{
			vector3s[1] = (Vector3)tweenArguments["amount"];
			return;
		}
		if (tweenArguments.Contains("x"))
		{
			vector3s[1].x = (float)tweenArguments["x"];
		}
		if (tweenArguments.Contains("y"))
		{
			vector3s[1].y = (float)tweenArguments["y"];
		}
		if (tweenArguments.Contains("z"))
		{
			vector3s[1].z = (float)tweenArguments["z"];
		}
	}

	private void GeneratePunchPositionTargets()
	{
		vector3s = new Vector3[5];
		vector3s[4] = transform.eulerAngles;
		vector3s[0] = transform.position;
		vector3s[1] = (vector3s[3] = Vector3.zero);
		if (tweenArguments.Contains("amount"))
		{
			vector3s[1] = (Vector3)tweenArguments["amount"];
			return;
		}
		if (tweenArguments.Contains("x"))
		{
			vector3s[1].x = (float)tweenArguments["x"];
		}
		if (tweenArguments.Contains("y"))
		{
			vector3s[1].y = (float)tweenArguments["y"];
		}
		if (tweenArguments.Contains("z"))
		{
			vector3s[1].z = (float)tweenArguments["z"];
		}
	}

	private void GeneratePunchRotationTargets()
	{
		vector3s = new Vector3[4];
		vector3s[0] = transform.eulerAngles;
		vector3s[1] = (vector3s[3] = Vector3.zero);
		if (tweenArguments.Contains("amount"))
		{
			vector3s[1] = (Vector3)tweenArguments["amount"];
			return;
		}
		if (tweenArguments.Contains("x"))
		{
			vector3s[1].x = (float)tweenArguments["x"];
		}
		if (tweenArguments.Contains("y"))
		{
			vector3s[1].y = (float)tweenArguments["y"];
		}
		if (tweenArguments.Contains("z"))
		{
			vector3s[1].z = (float)tweenArguments["z"];
		}
	}

	private void GeneratePunchScaleTargets()
	{
		vector3s = new Vector3[3];
		vector3s[0] = transform.localScale;
		vector3s[1] = Vector3.zero;
		if (tweenArguments.Contains("amount"))
		{
			vector3s[1] = (Vector3)tweenArguments["amount"];
			return;
		}
		if (tweenArguments.Contains("x"))
		{
			vector3s[1].x = (float)tweenArguments["x"];
		}
		if (tweenArguments.Contains("y"))
		{
			vector3s[1].y = (float)tweenArguments["y"];
		}
		if (tweenArguments.Contains("z"))
		{
			vector3s[1].z = (float)tweenArguments["z"];
		}
	}

	private void ApplyRectTargets()
	{
		rects[2].x = ease(rects[0].x, rects[1].x, percentage);
		rects[2].y = ease(rects[0].y, rects[1].y, percentage);
		rects[2].width = ease(rects[0].width, rects[1].width, percentage);
		rects[2].height = ease(rects[0].height, rects[1].height, percentage);
		tweenArguments["onupdateparams"] = rects[2];
		if (percentage == 1f)
		{
			tweenArguments["onupdateparams"] = rects[1];
		}
	}

	private void ApplyColorTargets()
	{
		colors[0, 2].r = ease(colors[0, 0].r, colors[0, 1].r, percentage);
		colors[0, 2].g = ease(colors[0, 0].g, colors[0, 1].g, percentage);
		colors[0, 2].b = ease(colors[0, 0].b, colors[0, 1].b, percentage);
		colors[0, 2].a = ease(colors[0, 0].a, colors[0, 1].a, percentage);
		tweenArguments["onupdateparams"] = colors[0, 2];
		if (percentage == 1f)
		{
			tweenArguments["onupdateparams"] = colors[0, 1];
		}
	}

	private void ApplyVector3Targets()
	{
		vector3s[2].x = ease(vector3s[0].x, vector3s[1].x, percentage);
		vector3s[2].y = ease(vector3s[0].y, vector3s[1].y, percentage);
		vector3s[2].z = ease(vector3s[0].z, vector3s[1].z, percentage);
		tweenArguments["onupdateparams"] = vector3s[2];
		if (percentage == 1f)
		{
			tweenArguments["onupdateparams"] = vector3s[1];
		}
	}

	private void ApplyVector2Targets()
	{
		vector2s[2].x = ease(vector2s[0].x, vector2s[1].x, percentage);
		vector2s[2].y = ease(vector2s[0].y, vector2s[1].y, percentage);
		tweenArguments["onupdateparams"] = vector2s[2];
		if (percentage == 1f)
		{
			tweenArguments["onupdateparams"] = vector2s[1];
		}
	}

	private void ApplyFloatTargets()
	{
		floats[2] = ease(floats[0], floats[1], percentage);
		tweenArguments["onupdateparams"] = floats[2];
		if (percentage == 1f)
		{
			tweenArguments["onupdateparams"] = floats[1];
		}
	}

	private void ApplyColorToTargets()
	{
		for (int i = 0; i < colors.GetLength(0); i++)
		{
			colors[i, 2].r = ease(colors[i, 0].r, colors[i, 1].r, percentage);
			colors[i, 2].g = ease(colors[i, 0].g, colors[i, 1].g, percentage);
			colors[i, 2].b = ease(colors[i, 0].b, colors[i, 1].b, percentage);
			colors[i, 2].a = ease(colors[i, 0].a, colors[i, 1].a, percentage);
		}
		if ((bool)GetComponent(typeof(UberText)) && !uberText.RenderToTexture)
		{
			uberText.TextAlpha = colors[0, 2].a;
			uberText.OutlineAlpha = colors[0, 2].a;
			uberText.ShadowAlpha = colors[0, 2].a;
		}
		else if ((bool)renderer)
		{
			m_MaterialTempList.Clear();
			renderer.GetMaterials(m_MaterialTempList);
			for (int j = 0; j < colors.GetLength(0); j++)
			{
				if ((bool)m_MaterialTempList[j])
				{
					m_MaterialTempList[j].SetColor(namedColorValueString, colors[j, 2]);
				}
			}
		}
		else if ((bool)light)
		{
			light.color = colors[0, 2];
		}
		if (percentage != 1f)
		{
			return;
		}
		if ((bool)renderer)
		{
			m_MaterialTempList.Clear();
			renderer.GetMaterials(m_MaterialTempList);
			for (int k = 0; k < colors.GetLength(0); k++)
			{
				if ((bool)m_MaterialTempList[k])
				{
					m_MaterialTempList[k].SetColor(namedColorValueString, colors[k, 1]);
				}
			}
		}
		else if ((bool)light)
		{
			light.color = colors[0, 1];
		}
	}

	private void ApplyAudioToTargets()
	{
		vector2s[2].x = ease(vector2s[0].x, vector2s[1].x, percentage);
		vector2s[2].y = ease(vector2s[0].y, vector2s[1].y, percentage);
		audioSource.volume = vector2s[2].x;
		audioSource.pitch = vector2s[2].y;
		if (percentage == 1f)
		{
			audioSource.volume = vector2s[1].x;
			audioSource.pitch = vector2s[1].y;
		}
	}

	private void ApplyStabTargets()
	{
	}

	private void ApplyMoveToPathTargets()
	{
		preUpdate = transform.position;
		float value = ease(0f, 1f, percentage);
		if (isLocal)
		{
			transform.localPosition = path.Interp(Mathf.Clamp(value, 0f, 1f));
		}
		else
		{
			transform.position = path.Interp(Mathf.Clamp(value, 0f, 1f));
		}
		if (tweenArguments.Contains("orienttopath") && (bool)tweenArguments["orienttopath"])
		{
			float num = ((!tweenArguments.Contains("lookahead")) ? Defaults.lookAhead : ((float)tweenArguments["lookahead"]));
			float value2 = ease(0f, 1f, Mathf.Min(1f, percentage + num));
			tweenArguments["looktarget"] = path.Interp(Mathf.Clamp(value2, 0f, 1f));
		}
		postUpdate = transform.position;
		if (physics)
		{
			transform.position = preUpdate;
			rigidbody.MovePosition(postUpdate);
		}
	}

	private void ApplyMoveToTargets()
	{
		preUpdate = transform.position;
		vector3s[2].x = ease(vector3s[0].x, vector3s[1].x, percentage);
		vector3s[2].y = ease(vector3s[0].y, vector3s[1].y, percentage);
		vector3s[2].z = ease(vector3s[0].z, vector3s[1].z, percentage);
		if (isLocal)
		{
			transform.localPosition = vector3s[2];
		}
		else
		{
			transform.position = vector3s[2];
		}
		if (percentage == 1f)
		{
			if (isLocal)
			{
				transform.localPosition = vector3s[1];
			}
			else
			{
				transform.position = vector3s[1];
			}
		}
		postUpdate = transform.position;
		if (physics)
		{
			transform.position = preUpdate;
			rigidbody.MovePosition(postUpdate);
		}
	}

	private void ApplyMoveByTargets()
	{
		preUpdate = transform.position;
		Vector3 eulerAngles = default(Vector3);
		if (tweenArguments.Contains("looktarget"))
		{
			eulerAngles = transform.eulerAngles;
			transform.eulerAngles = vector3s[4];
		}
		vector3s[2].x = ease(vector3s[0].x, vector3s[1].x, percentage);
		vector3s[2].y = ease(vector3s[0].y, vector3s[1].y, percentage);
		vector3s[2].z = ease(vector3s[0].z, vector3s[1].z, percentage);
		transform.Translate(vector3s[2] - vector3s[3], space);
		vector3s[3] = vector3s[2];
		if (tweenArguments.Contains("looktarget"))
		{
			transform.eulerAngles = eulerAngles;
		}
		postUpdate = transform.position;
		if (physics)
		{
			transform.position = preUpdate;
			rigidbody.MovePosition(postUpdate);
		}
	}

	private void ApplyScaleToTargets()
	{
		vector3s[2].x = ease(vector3s[0].x, vector3s[1].x, percentage);
		vector3s[2].y = ease(vector3s[0].y, vector3s[1].y, percentage);
		vector3s[2].z = ease(vector3s[0].z, vector3s[1].z, percentage);
		transform.localScale = vector3s[2];
		if (percentage == 1f)
		{
			transform.localScale = vector3s[1];
		}
	}

	private void ApplyLookToTargets()
	{
		vector3s[2].x = ease(vector3s[0].x, vector3s[1].x, percentage);
		vector3s[2].y = ease(vector3s[0].y, vector3s[1].y, percentage);
		vector3s[2].z = ease(vector3s[0].z, vector3s[1].z, percentage);
		if (isLocal)
		{
			transform.localRotation = Quaternion.Euler(vector3s[2]);
		}
		else
		{
			transform.rotation = Quaternion.Euler(vector3s[2]);
		}
	}

	private void ApplyRotateToTargets()
	{
		preUpdate = transform.eulerAngles;
		vector3s[2].x = ease(vector3s[0].x, vector3s[1].x, percentage);
		vector3s[2].y = ease(vector3s[0].y, vector3s[1].y, percentage);
		vector3s[2].z = ease(vector3s[0].z, vector3s[1].z, percentage);
		if (isLocal)
		{
			transform.localRotation = Quaternion.Euler(vector3s[2]);
		}
		else
		{
			transform.rotation = Quaternion.Euler(vector3s[2]);
		}
		if (percentage == 1f)
		{
			if (isLocal)
			{
				transform.localRotation = Quaternion.Euler(vector3s[1]);
			}
			else
			{
				transform.rotation = Quaternion.Euler(vector3s[1]);
			}
		}
		postUpdate = transform.eulerAngles;
		if (physics)
		{
			transform.eulerAngles = preUpdate;
			rigidbody.MoveRotation(Quaternion.Euler(postUpdate));
		}
	}

	private void ApplyRotateAddTargets()
	{
		preUpdate = transform.eulerAngles;
		vector3s[2].x = ease(vector3s[0].x, vector3s[1].x, percentage);
		vector3s[2].y = ease(vector3s[0].y, vector3s[1].y, percentage);
		vector3s[2].z = ease(vector3s[0].z, vector3s[1].z, percentage);
		transform.Rotate(vector3s[2] - vector3s[3], space);
		vector3s[3] = vector3s[2];
		postUpdate = transform.eulerAngles;
		if (physics)
		{
			transform.eulerAngles = preUpdate;
			rigidbody.MoveRotation(Quaternion.Euler(postUpdate));
		}
	}

	private void ApplyShakePositionTargets()
	{
		if (isLocal)
		{
			preUpdate = transform.localPosition;
		}
		else
		{
			preUpdate = transform.position;
		}
		Vector3 eulerAngles = default(Vector3);
		if (tweenArguments.Contains("looktarget"))
		{
			eulerAngles = transform.eulerAngles;
			transform.eulerAngles = vector3s[3];
		}
		float num = 1f - percentage;
		vector3s[2].x = UnityEngine.Random.Range((0f - vector3s[1].x) * num, vector3s[1].x * num);
		vector3s[2].y = UnityEngine.Random.Range((0f - vector3s[1].y) * num, vector3s[1].y * num);
		vector3s[2].z = UnityEngine.Random.Range((0f - vector3s[1].z) * num, vector3s[1].z * num);
		if (isLocal)
		{
			transform.localPosition = vector3s[0] + vector3s[2];
		}
		else
		{
			transform.position = vector3s[0] + vector3s[2];
		}
		if (tweenArguments.Contains("looktarget"))
		{
			transform.eulerAngles = eulerAngles;
		}
		postUpdate = transform.position;
		if (physics)
		{
			transform.position = preUpdate;
			rigidbody.MovePosition(postUpdate);
		}
	}

	private void ApplyShakeScaleTargets()
	{
		if (percentage == 0f)
		{
			transform.localScale = vector3s[1];
		}
		transform.localScale = vector3s[0];
		float num = 1f - percentage;
		vector3s[2].x = UnityEngine.Random.Range((0f - vector3s[1].x) * num, vector3s[1].x * num);
		vector3s[2].y = UnityEngine.Random.Range((0f - vector3s[1].y) * num, vector3s[1].y * num);
		vector3s[2].z = UnityEngine.Random.Range((0f - vector3s[1].z) * num, vector3s[1].z * num);
		transform.localScale += vector3s[2];
	}

	private void ApplyShakeRotationTargets()
	{
		preUpdate = transform.eulerAngles;
		if (percentage == 0f)
		{
			transform.Rotate(vector3s[1], space);
		}
		transform.eulerAngles = vector3s[0];
		float num = 1f - percentage;
		vector3s[2].x = UnityEngine.Random.Range((0f - vector3s[1].x) * num, vector3s[1].x * num);
		vector3s[2].y = UnityEngine.Random.Range((0f - vector3s[1].y) * num, vector3s[1].y * num);
		vector3s[2].z = UnityEngine.Random.Range((0f - vector3s[1].z) * num, vector3s[1].z * num);
		transform.Rotate(vector3s[2], space);
		postUpdate = transform.eulerAngles;
		if (physics)
		{
			transform.eulerAngles = preUpdate;
			rigidbody.MoveRotation(Quaternion.Euler(postUpdate));
		}
	}

	private void ApplyPunchPositionTargets()
	{
		preUpdate = transform.position;
		Vector3 eulerAngles = default(Vector3);
		if (tweenArguments.Contains("looktarget"))
		{
			eulerAngles = transform.eulerAngles;
			transform.eulerAngles = vector3s[4];
		}
		if (vector3s[1].x > 0f)
		{
			vector3s[2].x = punch(vector3s[1].x, percentage);
		}
		else if (vector3s[1].x < 0f)
		{
			vector3s[2].x = 0f - punch(Mathf.Abs(vector3s[1].x), percentage);
		}
		if (vector3s[1].y > 0f)
		{
			vector3s[2].y = punch(vector3s[1].y, percentage);
		}
		else if (vector3s[1].y < 0f)
		{
			vector3s[2].y = 0f - punch(Mathf.Abs(vector3s[1].y), percentage);
		}
		if (vector3s[1].z > 0f)
		{
			vector3s[2].z = punch(vector3s[1].z, percentage);
		}
		else if (vector3s[1].z < 0f)
		{
			vector3s[2].z = 0f - punch(Mathf.Abs(vector3s[1].z), percentage);
		}
		transform.Translate(vector3s[2] - vector3s[3], space);
		vector3s[3] = vector3s[2];
		if (tweenArguments.Contains("looktarget"))
		{
			transform.eulerAngles = eulerAngles;
		}
		postUpdate = transform.position;
		if (physics)
		{
			transform.position = preUpdate;
			rigidbody.MovePosition(postUpdate);
		}
	}

	private void ApplyPunchRotationTargets()
	{
		preUpdate = transform.eulerAngles;
		if (vector3s[1].x > 0f)
		{
			vector3s[2].x = punch(vector3s[1].x, percentage);
		}
		else if (vector3s[1].x < 0f)
		{
			vector3s[2].x = 0f - punch(Mathf.Abs(vector3s[1].x), percentage);
		}
		if (vector3s[1].y > 0f)
		{
			vector3s[2].y = punch(vector3s[1].y, percentage);
		}
		else if (vector3s[1].y < 0f)
		{
			vector3s[2].y = 0f - punch(Mathf.Abs(vector3s[1].y), percentage);
		}
		if (vector3s[1].z > 0f)
		{
			vector3s[2].z = punch(vector3s[1].z, percentage);
		}
		else if (vector3s[1].z < 0f)
		{
			vector3s[2].z = 0f - punch(Mathf.Abs(vector3s[1].z), percentage);
		}
		transform.Rotate(vector3s[2] - vector3s[3], space);
		vector3s[3] = vector3s[2];
		postUpdate = transform.eulerAngles;
		if (physics)
		{
			transform.eulerAngles = preUpdate;
			rigidbody.MoveRotation(Quaternion.Euler(postUpdate));
		}
	}

	private void ApplyPunchScaleTargets()
	{
		if (vector3s[1].x > 0f)
		{
			vector3s[2].x = punch(vector3s[1].x, percentage);
		}
		else if (vector3s[1].x < 0f)
		{
			vector3s[2].x = 0f - punch(Mathf.Abs(vector3s[1].x), percentage);
		}
		if (vector3s[1].y > 0f)
		{
			vector3s[2].y = punch(vector3s[1].y, percentage);
		}
		else if (vector3s[1].y < 0f)
		{
			vector3s[2].y = 0f - punch(Mathf.Abs(vector3s[1].y), percentage);
		}
		if (vector3s[1].z > 0f)
		{
			vector3s[2].z = punch(vector3s[1].z, percentage);
		}
		else if (vector3s[1].z < 0f)
		{
			vector3s[2].z = 0f - punch(Mathf.Abs(vector3s[1].z), percentage);
		}
		transform.localScale = vector3s[0] + vector3s[2];
	}

	private void ResetDelay()
	{
		delayStarted = Time.time;
		waitForDelay = true;
	}

	private void TweenStart()
	{
		if (tweenArguments != null)
		{
			CallBack(CallbackType.OnStart);
			if (!loop)
			{
				ConflictCheck();
				GenerateTargets();
			}
			loop = true;
			if (type == "stab")
			{
				audioSource.PlayOneShot(audioSource.clip);
			}
			if (type == "move" || type == "scale" || type == "rotate" || type == "punch" || type == "shake" || type == "curve" || type == "look")
			{
				EnableKinematic();
			}
			isRunning = true;
		}
	}

	private void TweenRestart()
	{
		ResetDelay();
		loop = true;
	}

	private void TweenUpdate()
	{
		if (type != "timer")
		{
			if (apply == null)
			{
				return;
			}
			apply();
		}
		CallBack(CallbackType.OnUpdate);
		UpdatePercentage();
	}

	private void TweenComplete()
	{
		isRunning = false;
		if (percentage > 0.5f)
		{
			percentage = 1f;
		}
		else
		{
			percentage = 0f;
		}
		if (type != "timer")
		{
			apply();
		}
		if (type == "value" || type == "timer")
		{
			CallBack(CallbackType.OnUpdate);
		}
		if (loopType == LoopType.none)
		{
			Dispose();
		}
		else
		{
			TweenLoop();
		}
		CallBack(CallbackType.OnComplete);
	}

	private void TweenLoop()
	{
		DisableKinematic();
		switch (loopType)
		{
		case LoopType.loop:
			percentage = 0f;
			runningTime = 0f;
			if (apply != null)
			{
				apply();
			}
			TweenRestart();
			break;
		case LoopType.pingPong:
			reverse = !reverse;
			runningTime = 0f;
			TweenRestart();
			break;
		}
	}

	public static Rect RectUpdate(Rect currentValue, Rect targetValue, float speed)
	{
		return new Rect(FloatUpdate(currentValue.x, targetValue.x, speed), FloatUpdate(currentValue.y, targetValue.y, speed), FloatUpdate(currentValue.width, targetValue.width, speed), FloatUpdate(currentValue.height, targetValue.height, speed));
	}

	public static Vector3 Vector3Update(Vector3 currentValue, Vector3 targetValue, float speed)
	{
		Vector3 vector = targetValue - currentValue;
		currentValue += vector * speed * Time.deltaTime;
		return currentValue;
	}

	public static Vector2 Vector2Update(Vector2 currentValue, Vector2 targetValue, float speed)
	{
		Vector2 vector = targetValue - currentValue;
		currentValue += vector * speed * Time.deltaTime;
		return currentValue;
	}

	public static float FloatUpdate(float currentValue, float targetValue, float speed)
	{
		float num = targetValue - currentValue;
		currentValue += num * speed * Time.deltaTime;
		return currentValue;
	}

	public static void FadeUpdate(GameObject target, Hashtable args)
	{
		args["a"] = args["alpha"];
		ColorUpdate(target, args);
	}

	public static void FadeUpdate(GameObject target, float alpha, float time)
	{
		FadeUpdate(target, Hash("alpha", alpha, "time", time));
	}

	public static void ColorUpdate(GameObject target, Hashtable args)
	{
		args = CleanArgs(args);
		Color[] array = new Color[4];
		if (!args.Contains("includechildren") || (bool)args["includechildren"])
		{
			foreach (Transform item in target.transform)
			{
				ColorUpdate(item.gameObject, args);
			}
		}
		float num;
		if (args.Contains("time"))
		{
			num = (float)args["time"];
			num *= Defaults.updateTimePercentage;
		}
		else
		{
			num = Defaults.updateTime;
		}
		array[0] = (array[1] = GetTargetColor(target));
		if (args.Contains("color"))
		{
			array[1] = (Color)args["color"];
		}
		else
		{
			if (args.Contains("r"))
			{
				array[1].r = (float)args["r"];
			}
			if (args.Contains("g"))
			{
				array[1].g = (float)args["g"];
			}
			if (args.Contains("b"))
			{
				array[1].b = (float)args["b"];
			}
			if (args.Contains("a"))
			{
				array[1].a = (float)args["a"];
			}
		}
		array[3].r = Mathf.SmoothDamp(array[0].r, array[1].r, ref array[2].r, num);
		array[3].g = Mathf.SmoothDamp(array[0].g, array[1].g, ref array[2].g, num);
		array[3].b = Mathf.SmoothDamp(array[0].b, array[1].b, ref array[2].b, num);
		array[3].a = Mathf.SmoothDamp(array[0].a, array[1].a, ref array[2].a, num);
		Renderer component = target.GetComponent<Renderer>();
		if ((bool)component)
		{
			component.GetMaterial().color = array[3];
		}
		else if ((bool)target.GetComponent<Light>())
		{
			target.GetComponent<Light>().color = array[3];
		}
		else if ((bool)target.GetComponent<UberText>())
		{
			UberText component2 = target.GetComponent<UberText>();
			component2.TextAlpha = array[3].a;
			component2.OutlineAlpha = array[3].a;
			component2.ShadowAlpha = array[3].a;
		}
	}

	public static void ColorUpdate(GameObject target, Color color, float time)
	{
		ColorUpdate(target, Hash("color", color, "time", time));
	}

	public static void AudioUpdate(GameObject target, Hashtable args)
	{
		args = CleanArgs(args);
		Vector2[] array = new Vector2[4];
		float num;
		if (args.Contains("time"))
		{
			num = (float)args["time"];
			num *= Defaults.updateTimePercentage;
		}
		else
		{
			num = Defaults.updateTime;
		}
		AudioSource audioSource;
		if (args.Contains("audiosource"))
		{
			audioSource = (AudioSource)args["audiosource"];
		}
		else
		{
			if (!target.GetComponent(typeof(AudioSource)))
			{
				Debug.LogError("iTween Error: AudioUpdate requires an AudioSource.");
				return;
			}
			audioSource = target.GetComponent<AudioSource>();
		}
		array[0] = (array[1] = new Vector2(audioSource.volume, audioSource.pitch));
		if (args.Contains("volume"))
		{
			array[1].x = (float)args["volume"];
		}
		if (args.Contains("pitch"))
		{
			array[1].y = (float)args["pitch"];
		}
		array[3].x = Mathf.SmoothDampAngle(array[0].x, array[1].x, ref array[2].x, num);
		array[3].y = Mathf.SmoothDampAngle(array[0].y, array[1].y, ref array[2].y, num);
		audioSource.volume = array[3].x;
		audioSource.pitch = array[3].y;
	}

	public static void AudioUpdate(GameObject target, float volume, float pitch, float time)
	{
		AudioUpdate(target, Hash("volume", volume, "pitch", pitch, "time", time));
	}

	public static void RotateUpdate(GameObject target, Hashtable args)
	{
		args = CleanArgs(args);
		Vector3[] array = new Vector3[4];
		Vector3 eulerAngles = target.transform.eulerAngles;
		float num;
		if (args.Contains("time"))
		{
			num = (float)args["time"];
			num *= Defaults.updateTimePercentage;
		}
		else
		{
			num = Defaults.updateTime;
		}
		bool flag = ((!args.Contains("islocal")) ? Defaults.isLocal : ((bool)args["islocal"]));
		if (flag)
		{
			array[0] = target.transform.localEulerAngles;
		}
		else
		{
			array[0] = target.transform.eulerAngles;
		}
		if (args.Contains("rotation"))
		{
			if (args["rotation"].GetType() == typeof(Transform))
			{
				Transform transform = (Transform)args["rotation"];
				array[1] = transform.eulerAngles;
			}
			else if (args["rotation"].GetType() == typeof(Vector3))
			{
				array[1] = (Vector3)args["rotation"];
			}
		}
		array[3].x = Mathf.SmoothDampAngle(array[0].x, array[1].x, ref array[2].x, num);
		array[3].y = Mathf.SmoothDampAngle(array[0].y, array[1].y, ref array[2].y, num);
		array[3].z = Mathf.SmoothDampAngle(array[0].z, array[1].z, ref array[2].z, num);
		if (flag)
		{
			target.transform.localEulerAngles = array[3];
		}
		else
		{
			target.transform.eulerAngles = array[3];
		}
		if (target.GetComponent<Rigidbody>() != null)
		{
			Vector3 eulerAngles2 = target.transform.eulerAngles;
			target.transform.eulerAngles = eulerAngles;
			target.GetComponent<Rigidbody>().MoveRotation(Quaternion.Euler(eulerAngles2));
		}
	}

	public static void RotateUpdate(GameObject target, Vector3 rotation, float time)
	{
		RotateUpdate(target, Hash("rotation", rotation, "time", time));
	}

	public static void ScaleUpdate(GameObject target, Hashtable args)
	{
		args = CleanArgs(args);
		Vector3[] array = new Vector3[4];
		float num;
		if (args.Contains("time"))
		{
			num = (float)args["time"];
			num *= Defaults.updateTimePercentage;
		}
		else
		{
			num = Defaults.updateTime;
		}
		array[0] = (array[1] = target.transform.localScale);
		if (args.Contains("scale"))
		{
			if (args["scale"].GetType() == typeof(Transform))
			{
				Transform transform = (Transform)args["scale"];
				array[1] = transform.localScale;
			}
			else if (args["scale"].GetType() == typeof(Vector3))
			{
				array[1] = (Vector3)args["scale"];
			}
		}
		else
		{
			if (args.Contains("x"))
			{
				array[1].x = (float)args["x"];
			}
			if (args.Contains("y"))
			{
				array[1].y = (float)args["y"];
			}
			if (args.Contains("z"))
			{
				array[1].z = (float)args["z"];
			}
		}
		array[3].x = Mathf.SmoothDamp(array[0].x, array[1].x, ref array[2].x, num);
		array[3].y = Mathf.SmoothDamp(array[0].y, array[1].y, ref array[2].y, num);
		array[3].z = Mathf.SmoothDamp(array[0].z, array[1].z, ref array[2].z, num);
		target.transform.localScale = array[3];
	}

	public static void ScaleUpdate(GameObject target, Vector3 scale, float time)
	{
		ScaleUpdate(target, Hash("scale", scale, "time", time));
	}

	public static void MoveUpdate(GameObject target, Hashtable args)
	{
		args = CleanArgs(args);
		Vector3[] array = new Vector3[4];
		Vector3 position = target.transform.position;
		float num;
		if (args.Contains("time"))
		{
			num = (float)args["time"];
			num *= Defaults.updateTimePercentage;
		}
		else
		{
			num = Defaults.updateTime;
		}
		bool flag = ((!args.Contains("islocal")) ? Defaults.isLocal : ((bool)args["islocal"]));
		if (flag)
		{
			array[0] = (array[1] = target.transform.localPosition);
		}
		else
		{
			array[0] = (array[1] = target.transform.position);
		}
		if (args.Contains("position"))
		{
			if (args["position"].GetType() == typeof(Transform))
			{
				Transform transform = (Transform)args["position"];
				array[1] = transform.position;
			}
			else if (args["position"].GetType() == typeof(Vector3))
			{
				array[1] = (Vector3)args["position"];
			}
		}
		else
		{
			if (args.Contains("x"))
			{
				array[1].x = (float)args["x"];
			}
			if (args.Contains("y"))
			{
				array[1].y = (float)args["y"];
			}
			if (args.Contains("z"))
			{
				array[1].z = (float)args["z"];
			}
		}
		array[3].x = Mathf.SmoothDamp(array[0].x, array[1].x, ref array[2].x, num);
		array[3].y = Mathf.SmoothDamp(array[0].y, array[1].y, ref array[2].y, num);
		array[3].z = Mathf.SmoothDamp(array[0].z, array[1].z, ref array[2].z, num);
		if (args.Contains("orienttopath") && (bool)args["orienttopath"])
		{
			args["looktarget"] = array[3];
		}
		if (args.Contains("looktarget"))
		{
			LookUpdate(target, args);
		}
		if (flag)
		{
			target.transform.localPosition = array[3];
		}
		else
		{
			target.transform.position = array[3];
		}
		if (target.GetComponent<Rigidbody>() != null)
		{
			Vector3 position2 = target.transform.position;
			target.transform.position = position;
			target.GetComponent<Rigidbody>().MovePosition(position2);
		}
	}

	public static void MoveUpdate(GameObject target, Vector3 position, float time)
	{
		MoveUpdate(target, Hash("position", position, "time", time));
	}

	public static void LookUpdate(GameObject target, Hashtable args)
	{
		args = CleanArgs(args);
		Vector3[] array = new Vector3[5];
		float num;
		if (args.Contains("looktime"))
		{
			num = (float)args["looktime"];
			num *= Defaults.updateTimePercentage;
		}
		else if (args.Contains("time"))
		{
			num = (float)args["time"] * 0.15f;
			num *= Defaults.updateTimePercentage;
		}
		else
		{
			num = Defaults.updateTime;
		}
		array[0] = target.transform.eulerAngles;
		if (args.Contains("looktarget"))
		{
			if (args["looktarget"].GetType() == typeof(Transform))
			{
				target.transform.LookAt((Transform)args["looktarget"], ((Vector3?)args["up"]) ?? Defaults.up);
			}
			else if (args["looktarget"].GetType() == typeof(Vector3))
			{
				target.transform.LookAt((Vector3)args["looktarget"], ((Vector3?)args["up"]) ?? Defaults.up);
			}
			array[1] = target.transform.eulerAngles;
			target.transform.eulerAngles = array[0];
			array[3].x = Mathf.SmoothDampAngle(array[0].x, array[1].x, ref array[2].x, num);
			array[3].y = Mathf.SmoothDampAngle(array[0].y, array[1].y, ref array[2].y, num);
			array[3].z = Mathf.SmoothDampAngle(array[0].z, array[1].z, ref array[2].z, num);
			target.transform.eulerAngles = array[3];
			if (args.Contains("axis"))
			{
				array[4] = target.transform.eulerAngles;
				switch ((string)args["axis"])
				{
				case "x":
					array[4].y = array[0].y;
					array[4].z = array[0].z;
					break;
				case "y":
					array[4].x = array[0].x;
					array[4].z = array[0].z;
					break;
				case "z":
					array[4].x = array[0].x;
					array[4].y = array[0].y;
					break;
				}
				target.transform.eulerAngles = array[4];
			}
		}
		else
		{
			Debug.LogError("iTween Error: LookUpdate needs a 'looktarget' property!");
		}
	}

	public static void LookUpdate(GameObject target, Vector3 looktarget, float time)
	{
		LookUpdate(target, Hash("looktarget", looktarget, "time", time));
	}

	public static float PathLength(Transform[] path)
	{
		Vector3[] array = new Vector3[path.Length];
		float num = 0f;
		for (int i = 0; i < path.Length; i++)
		{
			array[i] = path[i].position;
		}
		Vector3[] pts = PathControlPointGenerator(array);
		Vector3 a = Interp(pts, 0f);
		int num2 = path.Length * 20;
		for (int j = 1; j <= num2; j++)
		{
			float t = (float)j / (float)num2;
			Vector3 vector = Interp(pts, t);
			num += Vector3.Distance(a, vector);
			a = vector;
		}
		return num;
	}

	public static float PathLength(Vector3[] path)
	{
		float num = 0f;
		Vector3[] pts = PathControlPointGenerator(path);
		Vector3 a = Interp(pts, 0f);
		int num2 = path.Length * 20;
		for (int i = 1; i <= num2; i++)
		{
			float t = (float)i / (float)num2;
			Vector3 vector = Interp(pts, t);
			num += Vector3.Distance(a, vector);
			a = vector;
		}
		return num;
	}

	public static void PutOnPath(GameObject target, Vector3[] path, float percent)
	{
		target.transform.position = Interp(PathControlPointGenerator(path), percent);
	}

	public static void PutOnPath(Transform target, Vector3[] path, float percent)
	{
		target.position = Interp(PathControlPointGenerator(path), percent);
	}

	public static void PutOnPath(GameObject target, Transform[] path, float percent)
	{
		Vector3[] array = new Vector3[path.Length];
		for (int i = 0; i < path.Length; i++)
		{
			array[i] = path[i].position;
		}
		target.transform.position = Interp(PathControlPointGenerator(array), percent);
	}

	public static void PutOnPath(Transform target, Transform[] path, float percent)
	{
		Vector3[] array = new Vector3[path.Length];
		for (int i = 0; i < path.Length; i++)
		{
			array[i] = path[i].position;
		}
		target.position = Interp(PathControlPointGenerator(array), percent);
	}

	public static Vector3 PointOnPath(Transform[] path, float percent)
	{
		Vector3[] array = new Vector3[path.Length];
		for (int i = 0; i < path.Length; i++)
		{
			array[i] = path[i].position;
		}
		return Interp(PathControlPointGenerator(array), percent);
	}

	public static void DrawLine(Vector3[] line)
	{
		if (line.Length != 0)
		{
			DrawLineHelper(line, Defaults.color, "gizmos");
		}
	}

	public static void DrawLine(Vector3[] line, Color color)
	{
		if (line.Length != 0)
		{
			DrawLineHelper(line, color, "gizmos");
		}
	}

	public static void DrawLine(Transform[] line)
	{
		if (line.Length != 0)
		{
			Vector3[] array = new Vector3[line.Length];
			for (int i = 0; i < line.Length; i++)
			{
				array[i] = line[i].position;
			}
			DrawLineHelper(array, Defaults.color, "gizmos");
		}
	}

	public static void DrawLine(Transform[] line, Color color)
	{
		if (line.Length != 0)
		{
			Vector3[] array = new Vector3[line.Length];
			for (int i = 0; i < line.Length; i++)
			{
				array[i] = line[i].position;
			}
			DrawLineHelper(array, color, "gizmos");
		}
	}

	public static void DrawLineGizmos(Vector3[] line)
	{
		if (line.Length != 0)
		{
			DrawLineHelper(line, Defaults.color, "gizmos");
		}
	}

	public static void DrawLineGizmos(Vector3[] line, Color color)
	{
		if (line.Length != 0)
		{
			DrawLineHelper(line, color, "gizmos");
		}
	}

	public static void DrawLineGizmos(Transform[] line)
	{
		if (line.Length != 0)
		{
			Vector3[] array = new Vector3[line.Length];
			for (int i = 0; i < line.Length; i++)
			{
				array[i] = line[i].position;
			}
			DrawLineHelper(array, Defaults.color, "gizmos");
		}
	}

	public static void DrawLineGizmos(Transform[] line, Color color)
	{
		if (line.Length != 0)
		{
			Vector3[] array = new Vector3[line.Length];
			for (int i = 0; i < line.Length; i++)
			{
				array[i] = line[i].position;
			}
			DrawLineHelper(array, color, "gizmos");
		}
	}

	public static void DrawLineHandles(Vector3[] line)
	{
		if (line.Length != 0)
		{
			DrawLineHelper(line, Defaults.color, "handles");
		}
	}

	public static void DrawLineHandles(Vector3[] line, Color color)
	{
		if (line.Length != 0)
		{
			DrawLineHelper(line, color, "handles");
		}
	}

	public static void DrawLineHandles(Transform[] line)
	{
		if (line.Length != 0)
		{
			Vector3[] array = new Vector3[line.Length];
			for (int i = 0; i < line.Length; i++)
			{
				array[i] = line[i].position;
			}
			DrawLineHelper(array, Defaults.color, "handles");
		}
	}

	public static void DrawLineHandles(Transform[] line, Color color)
	{
		if (line.Length != 0)
		{
			Vector3[] array = new Vector3[line.Length];
			for (int i = 0; i < line.Length; i++)
			{
				array[i] = line[i].position;
			}
			DrawLineHelper(array, color, "handles");
		}
	}

	public static Vector3 PointOnPath(Vector3[] path, float percent)
	{
		return Interp(PathControlPointGenerator(path), percent);
	}

	public static void DrawPath(Vector3[] path)
	{
		if (path.Length != 0)
		{
			DrawPathHelper(path, Defaults.color, "gizmos");
		}
	}

	public static void DrawPath(Vector3[] path, Color color)
	{
		if (path.Length != 0)
		{
			DrawPathHelper(path, color, "gizmos");
		}
	}

	public static void DrawPath(Transform[] path)
	{
		if (path.Length != 0)
		{
			Vector3[] array = new Vector3[path.Length];
			for (int i = 0; i < path.Length; i++)
			{
				array[i] = path[i].position;
			}
			DrawPathHelper(array, Defaults.color, "gizmos");
		}
	}

	public static void DrawPath(Transform[] path, Color color)
	{
		if (path.Length != 0)
		{
			Vector3[] array = new Vector3[path.Length];
			for (int i = 0; i < path.Length; i++)
			{
				array[i] = path[i].position;
			}
			DrawPathHelper(array, color, "gizmos");
		}
	}

	public static void DrawPathGizmos(Vector3[] path)
	{
		if (path.Length != 0)
		{
			DrawPathHelper(path, Defaults.color, "gizmos");
		}
	}

	public static void DrawPathGizmos(Vector3[] path, Color color)
	{
		if (path.Length != 0)
		{
			DrawPathHelper(path, color, "gizmos");
		}
	}

	public static void DrawPathGizmos(Transform[] path)
	{
		if (path.Length != 0)
		{
			Vector3[] array = new Vector3[path.Length];
			for (int i = 0; i < path.Length; i++)
			{
				array[i] = path[i].position;
			}
			DrawPathHelper(array, Defaults.color, "gizmos");
		}
	}

	public static void DrawPathGizmos(Transform[] path, Color color)
	{
		if (path.Length != 0)
		{
			Vector3[] array = new Vector3[path.Length];
			for (int i = 0; i < path.Length; i++)
			{
				array[i] = path[i].position;
			}
			DrawPathHelper(array, color, "gizmos");
		}
	}

	public static void DrawPathHandles(Vector3[] path)
	{
		if (path.Length != 0)
		{
			DrawPathHelper(path, Defaults.color, "handles");
		}
	}

	public static void DrawPathHandles(Vector3[] path, Color color)
	{
		if (path.Length != 0)
		{
			DrawPathHelper(path, color, "handles");
		}
	}

	public static void DrawPathHandles(Transform[] path)
	{
		if (path.Length != 0)
		{
			Vector3[] array = new Vector3[path.Length];
			for (int i = 0; i < path.Length; i++)
			{
				array[i] = path[i].position;
			}
			DrawPathHelper(array, Defaults.color, "handles");
		}
	}

	public static void DrawPathHandles(Transform[] path, Color color)
	{
		if (path.Length != 0)
		{
			Vector3[] array = new Vector3[path.Length];
			for (int i = 0; i < path.Length; i++)
			{
				array[i] = path[i].position;
			}
			DrawPathHelper(array, color, "handles");
		}
	}

	public static void EnableTween(iTween tween)
	{
		tween.enabled = true;
	}

	public static void Resume(GameObject target)
	{
		iTweenManager.ForEachByGameObject(EnableTween, target);
	}

	public static void Resume(GameObject target, bool includechildren)
	{
		iTweenManager.ForEach(EnableTween, target, null, null, includechildren);
	}

	public static void Resume(GameObject target, string type)
	{
		iTweenManager.ForEach(EnableTween, target, null, type);
	}

	public static void Resume(GameObject target, string type, bool includechildren)
	{
		iTweenManager.ForEach(EnableTween, target, null, type, includechildren);
	}

	public static void Resume()
	{
		iTweenManager.ForEach(EnableTween);
	}

	public static void Resume(string type)
	{
		iTweenManager.ForEachByType(EnableTween, type);
	}

	public static void PauseTween(iTween tween)
	{
		tween.isPaused = true;
		tween.enabled = false;
	}

	public static void Pause(GameObject target)
	{
		iTweenManager.ForEachByGameObject(PauseTween, target);
	}

	public static void Pause(GameObject target, bool includechildren)
	{
		iTweenManager.ForEach(PauseTween, target, null, null, includechildren);
	}

	public static void Pause(GameObject target, string type)
	{
		iTweenManager.ForEach(PauseTween, target, null, type);
	}

	public static void Pause(GameObject target, string type, bool includechildren)
	{
		iTweenManager.ForEach(PauseTween, target, null, type, includechildren);
	}

	public static void Pause()
	{
		iTweenManager.ForEach(PauseTween);
	}

	public static void Pause(string type)
	{
		iTweenManager.ForEachByType(PauseTween, type);
	}

	public static int Count()
	{
		return iTweenManager.GetTweenCount();
	}

	public static int Count(string type)
	{
		int num = 0;
		iTweenIterator iterator = iTweenManager.GetIterator();
		iTween next;
		while ((next = iterator.GetNext()) != null)
		{
			if ((next.type + next.method).Substring(0, type.Length).ToLower().Equals(type.ToLower()))
			{
				num++;
			}
		}
		return num;
	}

	public static int Count(GameObject target)
	{
		int num = 0;
		iTweenIterator iterator = iTweenManager.GetIterator();
		iTween next;
		while ((next = iterator.GetNext()) != null)
		{
			if (next.gameObject == target)
			{
				num++;
			}
		}
		return num;
	}

	public static int Count(GameObject target, string type)
	{
		int num = 0;
		iTween[] tweensForObject = iTweenManager.GetTweensForObject(target);
		foreach (iTween iTween2 in tweensForObject)
		{
			if ((iTween2.type + iTween2.method).Substring(0, type.Length).ToLower() == type.ToLower())
			{
				num++;
			}
		}
		return num;
	}

	public static int CountByName(GameObject target, string name)
	{
		int num = 0;
		iTween[] tweensForObject = iTweenManager.GetTweensForObject(target);
		for (int i = 0; i < tweensForObject.Length; i++)
		{
			if (tweensForObject[i]._name == name)
			{
				num++;
			}
		}
		return num;
	}

	public static int CountOtherTypes(GameObject target, string type)
	{
		int num = 0;
		iTween[] tweensForObject = iTweenManager.GetTweensForObject(target);
		foreach (iTween iTween2 in tweensForObject)
		{
			if ((iTween2.type + iTween2.method).Substring(0, type.Length).ToLower() != type.ToLower())
			{
				num++;
			}
		}
		return num;
	}

	public static int CountOtherNames(GameObject target, string name)
	{
		int num = 0;
		iTween[] tweensForObject = iTweenManager.GetTweensForObject(target);
		for (int i = 0; i < tweensForObject.Length; i++)
		{
			if (tweensForObject[i]._name != name)
			{
				num++;
			}
		}
		return num;
	}

	public static bool HasTween(GameObject target)
	{
		return Count(target) > 0;
	}

	public static bool HasType(GameObject target, string type)
	{
		return Count(target, type) > 0;
	}

	public static bool HasName(GameObject target, string name)
	{
		return CountByName(target, name) > 0;
	}

	public static bool HasOtherType(GameObject target, string type)
	{
		iTween[] tweensForObject = iTweenManager.GetTweensForObject(target);
		foreach (iTween iTween2 in tweensForObject)
		{
			if ((iTween2.type + iTween2.method).Substring(0, type.Length).ToLower() != type.ToLower())
			{
				return true;
			}
		}
		return false;
	}

	public static bool HasOtherName(GameObject target, string name)
	{
		iTween[] tweensForObject = iTweenManager.GetTweensForObject(target);
		for (int i = 0; i < tweensForObject.Length; i++)
		{
			if (tweensForObject[i]._name != name)
			{
				return true;
			}
		}
		return false;
	}

	public static bool HasNameNotInList(GameObject target, params string[] names)
	{
		iTween[] tweensForObject = iTweenManager.GetTweensForObject(target);
		foreach (iTween iTween2 in tweensForObject)
		{
			bool flag = false;
			for (int j = 0; j < names.Length; j++)
			{
				if (iTween2._name == names[j])
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				return true;
			}
		}
		return false;
	}

	public static void StopTween(iTween tween)
	{
		tween.Dispose();
	}

	public static void Stop()
	{
		iTweenManager.ForEach(StopTween);
	}

	public static void Stop(string type)
	{
		iTweenManager.ForEachByType(StopTween, type);
	}

	public static void StopByName(string name)
	{
		iTweenManager.ForEachByName(StopTween, name);
	}

	public static void Stop(GameObject target)
	{
		iTweenManager.ForEachByGameObject(StopTween, target);
	}

	public static void Stop(GameObject target, bool includechildren)
	{
		iTweenManager.ForEach(StopTween, target, null, null, includechildren);
	}

	public static void Stop(GameObject target, string type)
	{
		iTweenManager.ForEach(StopTween, target, null, type);
	}

	public static void StopByName(GameObject target, string name)
	{
		iTweenManager.ForEach(StopTween, target, name);
	}

	public static void Stop(GameObject target, string type, bool includechildren)
	{
		iTweenManager.ForEach(StopTween, target, null, type, includechildren);
	}

	public static void StopByName(GameObject target, string name, bool includechildren)
	{
		iTweenManager.ForEach(StopTween, target, name, null, includechildren);
	}

	public static void StopOthers(GameObject target, string type, bool includechildren = false)
	{
		iTweenManager.ForEachInverted(StopTween, target, null, type, includechildren);
	}

	public static void StopOthersByName(GameObject target, string name, bool includechildren = false)
	{
		iTweenManager.ForEachInverted(StopTween, target, name, null, includechildren);
	}

	public static Hashtable Hash(params object[] args)
	{
		Hashtable hashtable = new Hashtable(args.Length / 2);
		if (args.Length % 2 != 0)
		{
			Debug.LogError("Tween Error: Hash requires an even number of arguments!");
			return null;
		}
		for (int i = 0; i < args.Length - 1; i += 2)
		{
			hashtable.Add(args[i], args[i + 1]);
		}
		return hashtable;
	}

	public iTween(GameObject obj, Hashtable args)
	{
		gameObject = obj;
		tweenArguments = args;
	}

	public void Awake()
	{
		RetrieveArgs();
		lastRealTime = Time.realtimeSinceStartup;
		ResetDelay();
	}

	public void Update()
	{
		if (!activeInHierarchy)
		{
			return;
		}
		if (waitForDelay)
		{
			if (delay > 0f && delay > Time.time - delayStarted)
			{
				return;
			}
			TweenStart();
			waitForDelay = false;
		}
		if (!isRunning || physics)
		{
			return;
		}
		if (!reverse)
		{
			if (percentage < 1f)
			{
				TweenUpdate();
			}
			else
			{
				TweenComplete();
			}
		}
		else if (percentage > 0f)
		{
			TweenUpdate();
		}
		else
		{
			TweenComplete();
		}
	}

	public void FixedUpdate()
	{
		if (!activeInHierarchy || !isRunning || !physics)
		{
			return;
		}
		if (!reverse)
		{
			if (percentage < 1f)
			{
				TweenUpdate();
			}
			else
			{
				TweenComplete();
			}
		}
		else if (percentage > 0f)
		{
			TweenUpdate();
		}
		else
		{
			TweenComplete();
		}
	}

	public void LateUpdate()
	{
		if (activeInHierarchy && !waitForDelay && tweenArguments != null && tweenArguments.Contains("looktarget") && isRunning && (type == "move" || type == "shake" || type == "punch"))
		{
			LookUpdate(gameObject, tweenArguments);
		}
	}

	public void OnEnable()
	{
		if (isRunning)
		{
			EnableKinematic();
		}
		if (isPaused)
		{
			isPaused = false;
			if (delay > 0f)
			{
				ResumeDelay();
			}
		}
	}

	public void OnDisable()
	{
		DisableKinematic();
	}

	public void Upkeep()
	{
		if (destroyed)
		{
			return;
		}
		if (gameObject == null)
		{
			iTweenManager.Remove(this);
		}
		else if (!gameObject.activeInHierarchy || !enabled)
		{
			if (activeLastTick)
			{
				OnDisable();
			}
			activeLastTick = false;
		}
		else
		{
			if (!activeLastTick)
			{
				OnEnable();
			}
			activeLastTick = true;
		}
	}

	private static void DrawLineHelper(Vector3[] line, Color color, string method)
	{
		Gizmos.color = color;
		for (int i = 0; i < line.Length - 1; i++)
		{
			if (method == "gizmos")
			{
				Gizmos.DrawLine(line[i], line[i + 1]);
			}
			else if (method == "handles")
			{
				Debug.LogError("iTween Error: Drawing a line with Handles is temporarily disabled because of compatability issues with Unity 2.6!");
			}
		}
	}

	private static void DrawPathHelper(Vector3[] path, Color color, string method)
	{
		Vector3[] pts = PathControlPointGenerator(path);
		Vector3 to = Interp(pts, 0f);
		Gizmos.color = color;
		int num = path.Length * 20;
		for (int i = 1; i <= num; i++)
		{
			float t = (float)i / (float)num;
			Vector3 vector = Interp(pts, t);
			if (method == "gizmos")
			{
				Gizmos.DrawLine(vector, to);
			}
			else if (method == "handles")
			{
				Debug.LogError("iTween Error: Drawing a path with Handles is temporarily disabled because of compatability issues with Unity 2.6!");
			}
			to = vector;
		}
	}

	private static Vector3[] PathControlPointGenerator(Vector3[] path)
	{
		int num = 2;
		Vector3[] array = new Vector3[path.Length + num];
		Array.Copy(path, 0, array, 1, path.Length);
		array[0] = array[1] + (array[1] - array[2]);
		array[array.Length - 1] = array[array.Length - 2] + (array[array.Length - 2] - array[array.Length - 3]);
		if (array[1] == array[array.Length - 2])
		{
			Vector3[] array2 = new Vector3[array.Length];
			Array.Copy(array, array2, array.Length);
			array2[0] = array2[array2.Length - 3];
			array2[array2.Length - 1] = array2[2];
			array = new Vector3[array2.Length];
			Array.Copy(array2, array, array2.Length);
		}
		return array;
	}

	private static Vector3 Interp(Vector3[] pts, float t)
	{
		int num = pts.Length - 3;
		int num2 = Mathf.Min(Mathf.FloorToInt(t * (float)num), num - 1);
		float num3 = t * (float)num - (float)num2;
		Vector3 vector = pts[num2];
		Vector3 vector2 = pts[num2 + 1];
		Vector3 vector3 = pts[num2 + 2];
		Vector3 vector4 = pts[num2 + 3];
		return 0.5f * ((-vector + 3f * vector2 - 3f * vector3 + vector4) * (num3 * num3 * num3) + (2f * vector - 5f * vector2 + 4f * vector3 - vector4) * (num3 * num3) + (-vector + vector3) * num3 + 2f * vector2);
	}

	private static void Launch(GameObject target, Hashtable args)
	{
		if (!args.Contains("id"))
		{
			args["id"] = GenerateID();
		}
		if (!args.Contains("target"))
		{
			args["target"] = target;
		}
		if (args.Contains("oncomplete") && !args.Contains("onconflict"))
		{
			args["onconflict"] = args["oncomplete"];
			if (args.Contains("oncompletetarget") && !args.Contains("onconflicttarget"))
			{
				args["onconflicttarget"] = args["oncompletetarget"];
			}
			if (args.Contains("oncompleteparams") && !args.Contains("onconflictparams"))
			{
				args["onconflictparams"] = args["oncompleteparams"];
			}
		}
		iTweenManager.Add(new iTween(target, args));
	}

	private static Hashtable CleanArgs(Hashtable args)
	{
		Hashtable hashtable = new Hashtable(args.Count);
		foreach (DictionaryEntry arg in args)
		{
			string text = arg.Key.ToString().ToLower();
			object obj = arg.Value;
			if (text != KEY_ID)
			{
				Type type = obj.GetType();
				if (type == typeof(int) || type == typeof(double))
				{
					obj = Convert.ToSingle(arg.Value);
				}
			}
			hashtable.Add(text, obj);
		}
		return hashtable;
	}

	private static int GenerateID()
	{
		int result = nextId;
		nextId = ((nextId == int.MaxValue) ? 1 : (nextId + 1));
		return result;
	}

	public Vector3 GetTargetPosition()
	{
		if (vector3s.Length >= 2)
		{
			return vector3s[1];
		}
		return new Vector3(0f, 0f, 0f);
	}

	private static Color GetTargetColor(GameObject target)
	{
		UberText component = target.GetComponent<UberText>();
		if ((bool)component)
		{
			return component.TextColor;
		}
		if ((bool)target.GetComponent<Renderer>())
		{
			Material sharedMaterial = target.GetComponent<Renderer>().GetSharedMaterial();
			if (sharedMaterial != null)
			{
				return sharedMaterial.color;
			}
		}
		else if ((bool)target.GetComponent<Light>())
		{
			return target.GetComponent<Light>().color;
		}
		return default(Color);
	}

	private void RetrieveArgs()
	{
		if (tweenArguments == null)
		{
			return;
		}
		id = (int)tweenArguments["id"];
		type = (string)tweenArguments["type"];
		_name = (string)tweenArguments["name"];
		method = (string)tweenArguments["method"];
		if (tweenArguments.Contains("time"))
		{
			time = (float)tweenArguments["time"];
		}
		else
		{
			time = Defaults.time;
		}
		if (rigidbody != null)
		{
			physics = true;
		}
		if (tweenArguments.Contains("delay"))
		{
			delay = (float)tweenArguments["delay"];
		}
		else
		{
			delay = Defaults.delay;
		}
		if (tweenArguments.Contains("namedcolorvalue"))
		{
			if (tweenArguments["namedcolorvalue"].GetType() == typeof(NamedValueColor))
			{
				namedColorValueString = ((NamedValueColor)tweenArguments["namedcolorvalue"]).ToString();
			}
			else if (tweenArguments["namedcolorvalue"].GetType() == typeof(string))
			{
				namedColorValueString = (string)tweenArguments["namedcolorvalue"];
			}
			else
			{
				try
				{
					namedColorValueString = ((NamedValueColor)Enum.Parse(typeof(NamedValueColor), (string)tweenArguments["namedcolorvalue"], ignoreCase: true)).ToString();
				}
				catch
				{
					Debug.LogWarning("iTween: Unsupported namedcolorvalue supplied! Default will be used.");
					namedcolorvalue = NamedValueColor._Color;
					namedColorValueString = namedcolorvalue.ToString();
				}
			}
		}
		else
		{
			namedcolorvalue = Defaults.namedColorValue;
			namedColorValueString = "_Color";
		}
		if (tweenArguments.Contains("looptype"))
		{
			if (tweenArguments["looptype"].GetType() == typeof(LoopType))
			{
				loopType = (LoopType)tweenArguments["looptype"];
			}
			else
			{
				try
				{
					loopType = (LoopType)Enum.Parse(typeof(LoopType), (string)tweenArguments["looptype"], ignoreCase: true);
				}
				catch
				{
					Debug.LogWarning("iTween: Unsupported loopType supplied! Default will be used.");
					loopType = LoopType.none;
				}
			}
		}
		else
		{
			loopType = LoopType.none;
		}
		if (tweenArguments.Contains("easetype"))
		{
			if (tweenArguments["easetype"].GetType() == typeof(EaseType))
			{
				easeType = (EaseType)tweenArguments["easetype"];
			}
			else
			{
				try
				{
					easeType = (EaseType)Enum.Parse(typeof(EaseType), (string)tweenArguments["easetype"], ignoreCase: true);
				}
				catch
				{
					Debug.LogWarning("iTween: Unsupported easeType supplied! Default will be used.");
					easeType = Defaults.easeType;
				}
			}
		}
		else
		{
			easeType = Defaults.easeType;
		}
		if (tweenArguments.Contains("space"))
		{
			if (tweenArguments["space"].GetType() == typeof(Space))
			{
				space = (Space)tweenArguments["space"];
			}
			else
			{
				try
				{
					space = (Space)Enum.Parse(typeof(Space), (string)tweenArguments["space"], ignoreCase: true);
				}
				catch
				{
					Debug.LogWarning("iTween: Unsupported space supplied! Default will be used.");
					space = Defaults.space;
				}
			}
		}
		else
		{
			space = Defaults.space;
		}
		if (tweenArguments.Contains("islocal"))
		{
			isLocal = (bool)tweenArguments["islocal"];
		}
		else
		{
			isLocal = Defaults.isLocal;
		}
		if (tweenArguments.Contains("ignoretimescale"))
		{
			useRealTime = (bool)tweenArguments["ignoretimescale"];
		}
		else
		{
			useRealTime = Defaults.useRealTime;
		}
		GetEasingFunction();
	}

	private void GetEasingFunction()
	{
		switch (easeType)
		{
		case EaseType.easeInQuad:
			ease = easeInQuad;
			break;
		case EaseType.easeOutQuad:
			ease = easeOutQuad;
			break;
		case EaseType.easeInOutQuad:
			ease = easeInOutQuad;
			break;
		case EaseType.easeInCubic:
			ease = easeInCubic;
			break;
		case EaseType.easeOutCubic:
			ease = easeOutCubic;
			break;
		case EaseType.easeInOutCubic:
			ease = easeInOutCubic;
			break;
		case EaseType.easeInQuart:
			ease = easeInQuart;
			break;
		case EaseType.easeOutQuart:
			ease = easeOutQuart;
			break;
		case EaseType.easeInOutQuart:
			ease = easeInOutQuart;
			break;
		case EaseType.easeInQuint:
			ease = easeInQuint;
			break;
		case EaseType.easeOutQuint:
			ease = easeOutQuint;
			break;
		case EaseType.easeInOutQuint:
			ease = easeInOutQuint;
			break;
		case EaseType.easeInSine:
			ease = easeInSine;
			break;
		case EaseType.easeOutSine:
			ease = easeOutSine;
			break;
		case EaseType.easeInOutSine:
			ease = easeInOutSine;
			break;
		case EaseType.easeInExpo:
			ease = easeInExpo;
			break;
		case EaseType.easeOutExpo:
			ease = easeOutExpo;
			break;
		case EaseType.easeInOutExpo:
			ease = easeInOutExpo;
			break;
		case EaseType.easeInCirc:
			ease = easeInCirc;
			break;
		case EaseType.easeOutCirc:
			ease = easeOutCirc;
			break;
		case EaseType.easeInOutCirc:
			ease = easeInOutCirc;
			break;
		case EaseType.linear:
			ease = linear;
			break;
		case EaseType.spring:
			ease = spring;
			break;
		case EaseType.easeInBounce:
			ease = easeInBounce;
			break;
		case EaseType.easeOutBounce:
			ease = easeOutBounce;
			break;
		case EaseType.easeInOutBounce:
			ease = easeInOutBounce;
			break;
		case EaseType.easeInBack:
			ease = easeInBack;
			break;
		case EaseType.easeOutBack:
			ease = easeOutBack;
			break;
		case EaseType.easeInOutBack:
			ease = easeInOutBack;
			break;
		case EaseType.easeInElastic:
			ease = easeInElastic;
			break;
		case EaseType.easeOutElastic:
			ease = easeOutElastic;
			break;
		case EaseType.easeInOutElastic:
			ease = easeInOutElastic;
			break;
		case EaseType.easeInSineOutExpo:
			ease = easeInSineOutExpo;
			break;
		case EaseType.easeOutElasticLight:
			ease = easeOutElasticLight;
			break;
		case EaseType.punch:
			break;
		}
	}

	private void UpdatePercentage()
	{
		if (useRealTime)
		{
			runningTime += Time.realtimeSinceStartup - lastRealTime;
		}
		else
		{
			runningTime += Time.deltaTime;
		}
		if (reverse)
		{
			percentage = 1f - runningTime / time;
		}
		else
		{
			percentage = runningTime / time;
		}
		lastRealTime = Time.realtimeSinceStartup;
	}

	private void CallBack(CallbackType callbackType)
	{
		string text = CALLBACK_NAMES[(int)callbackType];
		string key = CALLBACK_PARAMS_NAMES[(int)callbackType];
		string key2 = CALLBACK_TARGET_NAMES[(int)callbackType];
		if (!tweenArguments.Contains(text) || tweenArguments.Contains("ischild"))
		{
			return;
		}
		GameObject gameObject = ((!tweenArguments.Contains(key2)) ? this.gameObject : ((GameObject)tweenArguments[key2]));
		if (gameObject == null)
		{
			Debug.LogError($"iTween Error: target is null! callbackType={text} tween={this}");
			return;
		}
		object obj = tweenArguments[text];
		if (obj is Action<object>)
		{
			((Action<object>)obj)(tweenArguments[key]);
			return;
		}
		if (obj is string)
		{
			gameObject.SendMessage((string)obj, tweenArguments[key], SendMessageOptions.DontRequireReceiver);
			return;
		}
		Debug.LogError("iTween Error: Callback method references must be passed as a delegate or string!");
		iTweenManager.Remove(this);
	}

	private void Dispose()
	{
		iTweenManager.Remove(this);
	}

	private void ConflictCheck()
	{
		iTweenIterator iterator = iTweenManager.GetIterator();
		iTween next;
		while ((next = iterator.GetNext()) != null)
		{
			if (next.destroyed || next.gameObject != gameObject || next == this)
			{
				continue;
			}
			if (next.type == "value" || next.type == "timer")
			{
				break;
			}
			if (!next.isRunning || !(next.type == type))
			{
				continue;
			}
			if (next.method != method || next.tweenArguments == null)
			{
				break;
			}
			if (next.tweenArguments.Count != tweenArguments.Count)
			{
				next.Dispose();
				next.CallBack(CallbackType.OnConflict);
				break;
			}
			foreach (DictionaryEntry tweenArgument in tweenArguments)
			{
				if (!next.tweenArguments.Contains(tweenArgument.Key))
				{
					next.Dispose();
					next.CallBack(CallbackType.OnConflict);
					return;
				}
				if (!next.tweenArguments[tweenArgument.Key].Equals(tweenArguments[tweenArgument.Key]) && (string)tweenArgument.Key != "id")
				{
					next.Dispose();
					next.CallBack(CallbackType.OnConflict);
					return;
				}
			}
			Dispose();
			CallBack(CallbackType.OnConflict);
		}
	}

	private void EnableKinematic()
	{
	}

	private void DisableKinematic()
	{
	}

	private void ResumeDelay()
	{
		waitForDelay = true;
	}

	private float linear(float start, float end, float value)
	{
		return Mathf.Lerp(start, end, value);
	}

	private float clerp(float start, float end, float value)
	{
		float num = 0f;
		float num2 = 360f;
		float num3 = Mathf.Abs((num2 - num) / 2f);
		float num4 = 0f;
		float num5 = 0f;
		if (end - start < 0f - num3)
		{
			num5 = (num2 - start + end) * value;
			return start + num5;
		}
		if (end - start > num3)
		{
			num5 = (0f - (num2 - end + start)) * value;
			return start + num5;
		}
		return start + (end - start) * value;
	}

	private float spring(float start, float end, float value)
	{
		value = Mathf.Clamp01(value);
		value = (Mathf.Sin(value * (float)Math.PI * (0.2f + 2.5f * value * value * value)) * Mathf.Pow(1f - value, 2.2f) + value) * (1f + 1.2f * (1f - value));
		return start + (end - start) * value;
	}

	private float easeInQuad(float start, float end, float value)
	{
		end -= start;
		return end * value * value + start;
	}

	private float easeOutQuad(float start, float end, float value)
	{
		end -= start;
		return (0f - end) * value * (value - 2f) + start;
	}

	private float easeInOutQuad(float start, float end, float value)
	{
		value /= 0.5f;
		end -= start;
		if (value < 1f)
		{
			return end / 2f * value * value + start;
		}
		value -= 1f;
		return (0f - end) / 2f * (value * (value - 2f) - 1f) + start;
	}

	private float easeInCubic(float start, float end, float value)
	{
		end -= start;
		return end * value * value * value + start;
	}

	private float easeOutCubic(float start, float end, float value)
	{
		value -= 1f;
		end -= start;
		return end * (value * value * value + 1f) + start;
	}

	private float easeInOutCubic(float start, float end, float value)
	{
		value /= 0.5f;
		end -= start;
		if (value < 1f)
		{
			return end / 2f * value * value * value + start;
		}
		value -= 2f;
		return end / 2f * (value * value * value + 2f) + start;
	}

	private float easeInQuart(float start, float end, float value)
	{
		end -= start;
		return end * value * value * value * value + start;
	}

	private float easeOutQuart(float start, float end, float value)
	{
		value -= 1f;
		end -= start;
		return (0f - end) * (value * value * value * value - 1f) + start;
	}

	private float easeInOutQuart(float start, float end, float value)
	{
		value /= 0.5f;
		end -= start;
		if (value < 1f)
		{
			return end / 2f * value * value * value * value + start;
		}
		value -= 2f;
		return (0f - end) / 2f * (value * value * value * value - 2f) + start;
	}

	private float easeInQuint(float start, float end, float value)
	{
		end -= start;
		return end * value * value * value * value * value + start;
	}

	private float easeOutQuint(float start, float end, float value)
	{
		value -= 1f;
		end -= start;
		return end * (value * value * value * value * value + 1f) + start;
	}

	private float easeInOutQuint(float start, float end, float value)
	{
		value /= 0.5f;
		end -= start;
		if (value < 1f)
		{
			return end / 2f * value * value * value * value * value + start;
		}
		value -= 2f;
		return end / 2f * (value * value * value * value * value + 2f) + start;
	}

	private float easeInSine(float start, float end, float value)
	{
		end -= start;
		return (0f - end) * Mathf.Cos(value / 1f * ((float)Math.PI / 2f)) + end + start;
	}

	private float easeOutSine(float start, float end, float value)
	{
		end -= start;
		return end * Mathf.Sin(value / 1f * ((float)Math.PI / 2f)) + start;
	}

	private float easeInOutSine(float start, float end, float value)
	{
		end -= start;
		return (0f - end) / 2f * (Mathf.Cos((float)Math.PI * value / 1f) - 1f) + start;
	}

	private float easeInExpo(float start, float end, float value)
	{
		end -= start;
		return end * Mathf.Pow(2f, 10f * (value / 1f - 1f)) + start;
	}

	private float easeOutExpo(float start, float end, float value)
	{
		end -= start;
		return end * (0f - Mathf.Pow(2f, -10f * value / 1f) + 1f) + start;
	}

	private float easeInOutExpo(float start, float end, float value)
	{
		value /= 0.5f;
		end -= start;
		if (value < 1f)
		{
			return end / 2f * Mathf.Pow(2f, 10f * (value - 1f)) + start;
		}
		value -= 1f;
		return end / 2f * (0f - Mathf.Pow(2f, -10f * value) + 2f) + start;
	}

	private float easeInCirc(float start, float end, float value)
	{
		end -= start;
		return (0f - end) * (Mathf.Sqrt(1f - value * value) - 1f) + start;
	}

	private float easeOutCirc(float start, float end, float value)
	{
		value -= 1f;
		end -= start;
		return end * Mathf.Sqrt(1f - value * value) + start;
	}

	private float easeInOutCirc(float start, float end, float value)
	{
		value /= 0.5f;
		end -= start;
		if (value < 1f)
		{
			return (0f - end) / 2f * (Mathf.Sqrt(1f - value * value) - 1f) + start;
		}
		value -= 2f;
		return end / 2f * (Mathf.Sqrt(1f - value * value) + 1f) + start;
	}

	private float easeInBounce(float start, float end, float value)
	{
		end -= start;
		float num = 1f;
		return end - easeOutBounce(0f, end, num - value) + start;
	}

	private float easeOutBounce(float start, float end, float value)
	{
		value /= 1f;
		end -= start;
		if (value < 0.363636374f)
		{
			return end * (7.5625f * value * value) + start;
		}
		if (value < 0.727272749f)
		{
			value -= 0.545454562f;
			return end * (7.5625f * value * value + 0.75f) + start;
		}
		if ((double)value < 0.90909090909090906)
		{
			value -= 0.8181818f;
			return end * (7.5625f * value * value + 0.9375f) + start;
		}
		value -= 21f / 22f;
		return end * (7.5625f * value * value + 63f / 64f) + start;
	}

	private float easeInOutBounce(float start, float end, float value)
	{
		end -= start;
		float num = 1f;
		if (value < num / 2f)
		{
			return easeInBounce(0f, end, value * 2f) * 0.5f + start;
		}
		return easeOutBounce(0f, end, value * 2f - num) * 0.5f + end * 0.5f + start;
	}

	private float easeInBack(float start, float end, float value)
	{
		end -= start;
		value /= 1f;
		float num = 1.70158f;
		return end * value * value * ((num + 1f) * value - num) + start;
	}

	private float easeOutBack(float start, float end, float value)
	{
		float num = 1.70158f;
		end -= start;
		value = value / 1f - 1f;
		return end * (value * value * ((num + 1f) * value + num) + 1f) + start;
	}

	private float easeInOutBack(float start, float end, float value)
	{
		float num = 1.70158f;
		end -= start;
		value /= 0.5f;
		if (value < 1f)
		{
			num *= 1.525f;
			return end / 2f * (value * value * ((num + 1f) * value - num)) + start;
		}
		value -= 2f;
		num *= 1.525f;
		return end / 2f * (value * value * ((num + 1f) * value + num) + 2f) + start;
	}

	private float punch(float amplitude, float value)
	{
		float num = 9f;
		if (value == 0f)
		{
			return 0f;
		}
		if (value == 1f)
		{
			return 0f;
		}
		float num2 = 0.3f;
		num = num2 / ((float)Math.PI * 2f) * Mathf.Asin(0f);
		return amplitude * Mathf.Pow(2f, -10f * value) * Mathf.Sin((value * 1f - num) * ((float)Math.PI * 2f) / num2);
	}

	private float easeInElastic(float start, float end, float value)
	{
		end -= start;
		float num = 1f;
		float num2 = num * 0.3f;
		float num3 = 0f;
		float num4 = 0f;
		if (value == 0f)
		{
			return start;
		}
		if ((value /= num) == 1f)
		{
			return start + end;
		}
		if (num4 == 0f || num4 < Mathf.Abs(end))
		{
			num4 = end;
			num3 = num2 / 4f;
		}
		else
		{
			num3 = num2 / ((float)Math.PI * 2f) * Mathf.Asin(end / num4);
		}
		return 0f - num4 * Mathf.Pow(2f, 10f * (value -= 1f)) * Mathf.Sin((value * num - num3) * ((float)Math.PI * 2f) / num2) + start;
	}

	private float easeOutElastic(float start, float end, float value)
	{
		end -= start;
		float num = 1f;
		float num2 = num * 0.3f;
		float num3 = 0f;
		float num4 = 0f;
		if (value == 0f)
		{
			return start;
		}
		if ((value /= num) == 1f)
		{
			return start + end;
		}
		if (num4 == 0f || num4 < Mathf.Abs(end))
		{
			num4 = end;
			num3 = num2 / 4f;
		}
		else
		{
			num3 = num2 / ((float)Math.PI * 2f) * Mathf.Asin(end / num4);
		}
		return num4 * Mathf.Pow(2f, -10f * value) * Mathf.Sin((value * num - num3) * ((float)Math.PI * 2f) / num2) + end + start;
	}

	private float easeOutElasticLight(float start, float end, float value)
	{
		if (value == 0f)
		{
			return start;
		}
		if (value == 1f)
		{
			return end;
		}
		end -= start;
		float num = 0.6f;
		float num2 = num / 4f;
		return end * Mathf.Pow(2f, -10f * value) * Mathf.Sin((value - num2) * ((float)Math.PI * 2f) / num) + end + start;
	}

	private float easeInOutElastic(float start, float end, float value)
	{
		end -= start;
		float num = 1f;
		float num2 = num * 0.3f;
		float num3 = 0f;
		float num4 = 0f;
		if (value == 0f)
		{
			return start;
		}
		if ((value /= num / 2f) == 2f)
		{
			return start + end;
		}
		if (num4 == 0f || num4 < Mathf.Abs(end))
		{
			num4 = end;
			num3 = num2 / 4f;
		}
		else
		{
			num3 = num2 / ((float)Math.PI * 2f) * Mathf.Asin(end / num4);
		}
		if (value < 1f)
		{
			return -0.5f * (num4 * Mathf.Pow(2f, 10f * (value -= 1f)) * Mathf.Sin((value * num - num3) * ((float)Math.PI * 2f) / num2)) + start;
		}
		return num4 * Mathf.Pow(2f, -10f * (value -= 1f)) * Mathf.Sin((value * num - num3) * ((float)Math.PI * 2f) / num2) * 0.5f + end + start;
	}

	private float easeInSineOutExpo(float start, float end, float value)
	{
		if (value > start / 2f)
		{
			return easeOutExpo(start, end, value);
		}
		return easeInSine(start, end, value);
	}

	private float easeInExpoFirstHalf(float start, float end, float value)
	{
		end -= start;
		return end * Mathf.Pow(2f, 10f * (value / 1f - 1f)) + start;
	}

	private float easeInExpoSecondHalf(float start, float end, float value)
	{
		end -= start;
		return end * Mathf.Pow(2f, 10f * (value / 1f - 1f)) + start;
	}

	public override string ToString()
	{
		string name = gameObject.name;
		if (tweenArguments == null)
		{
			return $"[iTween - gameObject={name}";
		}
		object obj = tweenArguments["type"];
		object obj2 = tweenArguments["method"];
		object obj3 = tweenArguments["name"];
		object obj4 = tweenArguments["id"];
		return $"[iTween - gameObject={name} type={obj} method={obj2} name={obj3} id={obj4}]";
	}
}
