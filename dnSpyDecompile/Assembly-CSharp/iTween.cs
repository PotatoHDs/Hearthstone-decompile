using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000A41 RID: 2625
public class iTween
{
	// Token: 0x170007F3 RID: 2035
	// (get) Token: 0x06008D0E RID: 36110 RVA: 0x002D373F File Offset: 0x002D193F
	private Transform transform
	{
		get
		{
			return this.gameObject.transform;
		}
	}

	// Token: 0x170007F4 RID: 2036
	// (get) Token: 0x06008D0F RID: 36111 RVA: 0x002D374C File Offset: 0x002D194C
	private Renderer renderer
	{
		get
		{
			return this.gameObject.GetComponent<Renderer>();
		}
	}

	// Token: 0x170007F5 RID: 2037
	// (get) Token: 0x06008D10 RID: 36112 RVA: 0x002D3759 File Offset: 0x002D1959
	private Light light
	{
		get
		{
			return this.gameObject.GetComponent<Light>();
		}
	}

	// Token: 0x170007F6 RID: 2038
	// (get) Token: 0x06008D11 RID: 36113 RVA: 0x002D3766 File Offset: 0x002D1966
	private AudioSource audio
	{
		get
		{
			return this.gameObject.GetComponent<AudioSource>();
		}
	}

	// Token: 0x170007F7 RID: 2039
	// (get) Token: 0x06008D12 RID: 36114 RVA: 0x002D3773 File Offset: 0x002D1973
	private Rigidbody rigidbody
	{
		get
		{
			return this.gameObject.GetComponent<Rigidbody>();
		}
	}

	// Token: 0x170007F8 RID: 2040
	// (get) Token: 0x06008D13 RID: 36115 RVA: 0x002D3780 File Offset: 0x002D1980
	private UberText uberText
	{
		get
		{
			return this.gameObject.GetComponent<UberText>();
		}
	}

	// Token: 0x170007F9 RID: 2041
	// (get) Token: 0x06008D14 RID: 36116 RVA: 0x002D378D File Offset: 0x002D198D
	private bool activeInHierarchy
	{
		get
		{
			return this.enabled && !this.destroyed && this.gameObject && this.gameObject.activeInHierarchy;
		}
	}

	// Token: 0x06008D15 RID: 36117 RVA: 0x002D37B9 File Offset: 0x002D19B9
	private Component GetComponent(Type t)
	{
		return this.gameObject.GetComponent(t);
	}

	// Token: 0x06008D16 RID: 36118 RVA: 0x002D37C7 File Offset: 0x002D19C7
	public static void Init(GameObject target)
	{
		iTween.MoveBy(target, Vector3.zero, 0f);
	}

	// Token: 0x06008D17 RID: 36119 RVA: 0x002D37DC File Offset: 0x002D19DC
	public static void ValueTo(GameObject target, Hashtable args)
	{
		args = iTween.CleanArgs(args);
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
			args.Add("easetype", iTween.EaseType.linear);
		}
		iTween.Launch(target, args);
	}

	// Token: 0x06008D18 RID: 36120 RVA: 0x002D3969 File Offset: 0x002D1B69
	public static void FadeFrom(GameObject target, float alpha, float time)
	{
		iTween.FadeFrom(target, iTween.Hash(new object[]
		{
			"alpha",
			alpha,
			"time",
			time
		}));
	}

	// Token: 0x06008D19 RID: 36121 RVA: 0x002D399E File Offset: 0x002D1B9E
	public static void FadeFrom(GameObject target, Hashtable args)
	{
		iTween.ColorFrom(target, args);
	}

	// Token: 0x06008D1A RID: 36122 RVA: 0x002D39A7 File Offset: 0x002D1BA7
	public static void FadeTo(GameObject target, float alpha, float time)
	{
		iTween.FadeTo(target, iTween.Hash(new object[]
		{
			"alpha",
			alpha,
			"time",
			time
		}));
	}

	// Token: 0x06008D1B RID: 36123 RVA: 0x002D39DC File Offset: 0x002D1BDC
	public static void FadeTo(GameObject target, Hashtable args)
	{
		iTween.ColorTo(target, args);
	}

	// Token: 0x06008D1C RID: 36124 RVA: 0x002D39E5 File Offset: 0x002D1BE5
	public static void ColorFrom(GameObject target, Color color, float time)
	{
		iTween.ColorFrom(target, iTween.Hash(new object[]
		{
			"color",
			color,
			"time",
			time
		}));
	}

	// Token: 0x06008D1D RID: 36125 RVA: 0x002D3A1C File Offset: 0x002D1C1C
	public static void ColorFrom(GameObject target, Hashtable args)
	{
		Color color = default(Color);
		Color color2 = default(Color);
		args = iTween.CleanArgs(args);
		if (!args.Contains("includechildren") || (bool)args["includechildren"])
		{
			foreach (object obj in target.transform)
			{
				Component component = (Transform)obj;
				Hashtable hashtable = (Hashtable)args.Clone();
				hashtable["ischild"] = true;
				iTween.ColorFrom(component.gameObject, hashtable);
			}
		}
		if (!args.Contains("easetype"))
		{
			args.Add("easetype", iTween.EaseType.linear);
		}
		color = (color2 = iTween.GetTargetColor(target));
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
		Renderer component2 = target.GetComponent<Renderer>();
		UberText component3 = target.GetComponent<UberText>();
		if (component3)
		{
			component3.TextColor = color;
		}
		else if (component2)
		{
			component2.GetMaterial().color = color;
		}
		else if (target.GetComponent<Light>())
		{
			target.GetComponent<Light>().color = color;
		}
		args["color"] = color2;
		args["type"] = "color";
		args["method"] = "to";
		iTween.Launch(target, args);
	}

	// Token: 0x06008D1E RID: 36126 RVA: 0x002D3C98 File Offset: 0x002D1E98
	public static void ColorTo(GameObject target, Color color, float time)
	{
		iTween.ColorTo(target, iTween.Hash(new object[]
		{
			"color",
			color,
			"time",
			time
		}));
	}

	// Token: 0x06008D1F RID: 36127 RVA: 0x002D3CD0 File Offset: 0x002D1ED0
	public static void ColorTo(GameObject target, Hashtable args)
	{
		args = iTween.CleanArgs(args);
		if (!args.Contains("includechildren") || (bool)args["includechildren"])
		{
			foreach (object obj in target.transform)
			{
				Component component = (Transform)obj;
				Hashtable hashtable = (Hashtable)args.Clone();
				hashtable["ischild"] = true;
				iTween.ColorTo(component.gameObject, hashtable);
			}
		}
		if (!args.Contains("easetype"))
		{
			args.Add("easetype", iTween.EaseType.linear);
		}
		args["type"] = "color";
		args["method"] = "to";
		iTween.Launch(target, args);
	}

	// Token: 0x06008D20 RID: 36128 RVA: 0x002D3DB8 File Offset: 0x002D1FB8
	public static void AudioFrom(GameObject target, float volume, float pitch, float time)
	{
		iTween.AudioFrom(target, iTween.Hash(new object[]
		{
			"volume",
			volume,
			"pitch",
			pitch,
			"time",
			time
		}));
	}

	// Token: 0x06008D21 RID: 36129 RVA: 0x002D3E0C File Offset: 0x002D200C
	public static void AudioFrom(GameObject target, Hashtable args)
	{
		args = iTween.CleanArgs(args);
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
		Vector2 vector;
		Vector2 vector2;
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
			args.Add("easetype", iTween.EaseType.linear);
		}
		args["type"] = "audio";
		args["method"] = "to";
		iTween.Launch(target, args);
	}

	// Token: 0x06008D22 RID: 36130 RVA: 0x002D3F6C File Offset: 0x002D216C
	public static void AudioTo(GameObject target, float volume, float pitch, float time)
	{
		iTween.AudioTo(target, iTween.Hash(new object[]
		{
			"volume",
			volume,
			"pitch",
			pitch,
			"time",
			time
		}));
	}

	// Token: 0x06008D23 RID: 36131 RVA: 0x002D3FC0 File Offset: 0x002D21C0
	public static void AudioTo(GameObject target, Hashtable args)
	{
		args = iTween.CleanArgs(args);
		if (!args.Contains("easetype"))
		{
			args.Add("easetype", iTween.EaseType.linear);
		}
		args["type"] = "audio";
		args["method"] = "to";
		iTween.Launch(target, args);
	}

	// Token: 0x06008D24 RID: 36132 RVA: 0x002D401B File Offset: 0x002D221B
	public static void Stab(GameObject target, AudioClip audioclip, float delay)
	{
		iTween.Stab(target, iTween.Hash(new object[]
		{
			"audioclip",
			audioclip,
			"delay",
			delay
		}));
	}

	// Token: 0x06008D25 RID: 36133 RVA: 0x002D404B File Offset: 0x002D224B
	public static void Stab(GameObject target, Hashtable args)
	{
		args = iTween.CleanArgs(args);
		args["type"] = "stab";
		iTween.Launch(target, args);
	}

	// Token: 0x06008D26 RID: 36134 RVA: 0x002D406C File Offset: 0x002D226C
	public static void LookFrom(GameObject target, Vector3 looktarget, float time)
	{
		iTween.LookFrom(target, iTween.Hash(new object[]
		{
			"looktarget",
			looktarget,
			"time",
			time
		}));
	}

	// Token: 0x06008D27 RID: 36135 RVA: 0x002D40A4 File Offset: 0x002D22A4
	public static void LookFrom(GameObject target, Hashtable args)
	{
		args = iTween.CleanArgs(args);
		Vector3 eulerAngles = target.transform.eulerAngles;
		if (args["looktarget"].GetType() == typeof(Transform))
		{
			target.transform.LookAt((Transform)args["looktarget"], ((Vector3?)args["up"]) ?? iTween.Defaults.up);
		}
		else if (args["looktarget"].GetType() == typeof(Vector3))
		{
			target.transform.LookAt((Vector3)args["looktarget"], ((Vector3?)args["up"]) ?? iTween.Defaults.up);
		}
		if (args.Contains("axis"))
		{
			Vector3 eulerAngles2 = target.transform.eulerAngles;
			string a = (string)args["axis"];
			if (!(a == "x"))
			{
				if (!(a == "y"))
				{
					if (a == "z")
					{
						eulerAngles2.x = eulerAngles.x;
						eulerAngles2.y = eulerAngles.y;
					}
				}
				else
				{
					eulerAngles2.x = eulerAngles.x;
					eulerAngles2.z = eulerAngles.z;
				}
			}
			else
			{
				eulerAngles2.y = eulerAngles.y;
				eulerAngles2.z = eulerAngles.z;
			}
			target.transform.eulerAngles = eulerAngles2;
		}
		args["rotation"] = eulerAngles;
		args["type"] = "rotate";
		args["method"] = "to";
		iTween.Launch(target, args);
	}

	// Token: 0x06008D28 RID: 36136 RVA: 0x002D427B File Offset: 0x002D247B
	public static void LookTo(GameObject target, Vector3 looktarget, float time)
	{
		iTween.LookTo(target, iTween.Hash(new object[]
		{
			"looktarget",
			looktarget,
			"time",
			time
		}));
	}

	// Token: 0x06008D29 RID: 36137 RVA: 0x002D42B0 File Offset: 0x002D24B0
	public static void LookTo(GameObject target, Hashtable args)
	{
		args = iTween.CleanArgs(args);
		if (args.Contains("looktarget") && args["looktarget"].GetType() == typeof(Transform))
		{
			Transform transform = (Transform)args["looktarget"];
			args["position"] = new Vector3(transform.position.x, transform.position.y, transform.position.z);
			args["rotation"] = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
		}
		args["type"] = "look";
		args["method"] = "to";
		iTween.Launch(target, args);
	}

	// Token: 0x06008D2A RID: 36138 RVA: 0x002D439A File Offset: 0x002D259A
	public static void MoveTo(GameObject target, Vector3 position, float time)
	{
		iTween.MoveTo(target, iTween.Hash(new object[]
		{
			"position",
			position,
			"time",
			time
		}));
	}

	// Token: 0x06008D2B RID: 36139 RVA: 0x002D43D0 File Offset: 0x002D25D0
	public static void MoveTo(GameObject target, Hashtable args)
	{
		args = iTween.CleanArgs(args);
		if (args.Contains("position") && args["position"].GetType() == typeof(Transform))
		{
			Transform transform = (Transform)args["position"];
			args["position"] = new Vector3(transform.position.x, transform.position.y, transform.position.z);
			args["rotation"] = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
			args["scale"] = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
		}
		args["type"] = "move";
		args["method"] = "to";
		iTween.Launch(target, args);
	}

	// Token: 0x06008D2C RID: 36140 RVA: 0x002D44F3 File Offset: 0x002D26F3
	public static void MoveFrom(GameObject target, Vector3 position, float time)
	{
		iTween.MoveFrom(target, iTween.Hash(new object[]
		{
			"position",
			position,
			"time",
			time
		}));
	}

	// Token: 0x06008D2D RID: 36141 RVA: 0x002D4528 File Offset: 0x002D2728
	public static void MoveFrom(GameObject target, Hashtable args)
	{
		args = iTween.CleanArgs(args);
		bool flag;
		if (args.Contains("islocal"))
		{
			flag = (bool)args["islocal"];
		}
		else
		{
			flag = iTween.Defaults.isLocal;
		}
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
			Vector3 vector2;
			Vector3 vector;
			if (flag)
			{
				vector = (vector2 = target.transform.localPosition);
			}
			else
			{
				vector = (vector2 = target.transform.position);
			}
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
		iTween.Launch(target, args);
	}

	// Token: 0x06008D2E RID: 36142 RVA: 0x002D4838 File Offset: 0x002D2A38
	public static void MoveAdd(GameObject target, Vector3 amount, float time)
	{
		iTween.MoveAdd(target, iTween.Hash(new object[]
		{
			"amount",
			amount,
			"time",
			time
		}));
	}

	// Token: 0x06008D2F RID: 36143 RVA: 0x002D486D File Offset: 0x002D2A6D
	public static void MoveAdd(GameObject target, Hashtable args)
	{
		args = iTween.CleanArgs(args);
		args["type"] = "move";
		args["method"] = "add";
		iTween.Launch(target, args);
	}

	// Token: 0x06008D30 RID: 36144 RVA: 0x002D489E File Offset: 0x002D2A9E
	public static void MoveBy(GameObject target, Vector3 amount, float time)
	{
		iTween.MoveBy(target, iTween.Hash(new object[]
		{
			"amount",
			amount,
			"time",
			time
		}));
	}

	// Token: 0x06008D31 RID: 36145 RVA: 0x002D48D3 File Offset: 0x002D2AD3
	public static void MoveBy(GameObject target, Hashtable args)
	{
		args = iTween.CleanArgs(args);
		args["type"] = "move";
		args["method"] = "by";
		iTween.Launch(target, args);
	}

	// Token: 0x06008D32 RID: 36146 RVA: 0x002D4904 File Offset: 0x002D2B04
	public static void ScaleTo(GameObject target, Vector3 scale, float time)
	{
		iTween.ScaleTo(target, iTween.Hash(new object[]
		{
			"scale",
			scale,
			"time",
			time
		}));
	}

	// Token: 0x06008D33 RID: 36147 RVA: 0x002D493C File Offset: 0x002D2B3C
	public static void ScaleTo(GameObject target, Hashtable args)
	{
		args = iTween.CleanArgs(args);
		if (args.Contains("scale") && args["scale"].GetType() == typeof(Transform))
		{
			Transform transform = (Transform)args["scale"];
			args["position"] = new Vector3(transform.position.x, transform.position.y, transform.position.z);
			args["rotation"] = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
			args["scale"] = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
		}
		args["type"] = "scale";
		args["method"] = "to";
		iTween.Launch(target, args);
	}

	// Token: 0x06008D34 RID: 36148 RVA: 0x002D4A5F File Offset: 0x002D2C5F
	public static void ScaleFrom(GameObject target, Vector3 scale, float time)
	{
		iTween.ScaleFrom(target, iTween.Hash(new object[]
		{
			"scale",
			scale,
			"time",
			time
		}));
	}

	// Token: 0x06008D35 RID: 36149 RVA: 0x002D4A94 File Offset: 0x002D2C94
	public static void ScaleFrom(GameObject target, Hashtable args)
	{
		args = iTween.CleanArgs(args);
		Vector3 localScale2;
		Vector3 localScale = localScale2 = target.transform.localScale;
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
		args["scale"] = localScale2;
		args["type"] = "scale";
		args["method"] = "to";
		iTween.Launch(target, args);
	}

	// Token: 0x06008D36 RID: 36150 RVA: 0x002D4BE4 File Offset: 0x002D2DE4
	public static void ScaleAdd(GameObject target, Vector3 amount, float time)
	{
		iTween.ScaleAdd(target, iTween.Hash(new object[]
		{
			"amount",
			amount,
			"time",
			time
		}));
	}

	// Token: 0x06008D37 RID: 36151 RVA: 0x002D4C19 File Offset: 0x002D2E19
	public static void ScaleAdd(GameObject target, Hashtable args)
	{
		args = iTween.CleanArgs(args);
		args["type"] = "scale";
		args["method"] = "add";
		iTween.Launch(target, args);
	}

	// Token: 0x06008D38 RID: 36152 RVA: 0x002D4C4A File Offset: 0x002D2E4A
	public static void ScaleBy(GameObject target, Vector3 amount, float time)
	{
		iTween.ScaleBy(target, iTween.Hash(new object[]
		{
			"amount",
			amount,
			"time",
			time
		}));
	}

	// Token: 0x06008D39 RID: 36153 RVA: 0x002D4C7F File Offset: 0x002D2E7F
	public static void ScaleBy(GameObject target, Hashtable args)
	{
		args = iTween.CleanArgs(args);
		args["type"] = "scale";
		args["method"] = "by";
		iTween.Launch(target, args);
	}

	// Token: 0x06008D3A RID: 36154 RVA: 0x002D4CB0 File Offset: 0x002D2EB0
	public static void RotateTo(GameObject target, Vector3 rotation, float time)
	{
		iTween.RotateTo(target, iTween.Hash(new object[]
		{
			"rotation",
			rotation,
			"time",
			time
		}));
	}

	// Token: 0x06008D3B RID: 36155 RVA: 0x002D4CE8 File Offset: 0x002D2EE8
	public static void RotateTo(GameObject target, Hashtable args)
	{
		args = iTween.CleanArgs(args);
		if (args.Contains("rotation") && args["rotation"].GetType() == typeof(Transform))
		{
			Transform transform = (Transform)args["rotation"];
			args["position"] = new Vector3(transform.position.x, transform.position.y, transform.position.z);
			args["rotation"] = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
			args["scale"] = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
		}
		args["type"] = "rotate";
		args["method"] = "to";
		iTween.Launch(target, args);
	}

	// Token: 0x06008D3C RID: 36156 RVA: 0x002D4E0B File Offset: 0x002D300B
	public static void RotateFrom(GameObject target, Vector3 rotation, float time)
	{
		iTween.RotateFrom(target, iTween.Hash(new object[]
		{
			"rotation",
			rotation,
			"time",
			time
		}));
	}

	// Token: 0x06008D3D RID: 36157 RVA: 0x002D4E40 File Offset: 0x002D3040
	public static void RotateFrom(GameObject target, Hashtable args)
	{
		args = iTween.CleanArgs(args);
		bool flag;
		if (args.Contains("islocal"))
		{
			flag = (bool)args["islocal"];
		}
		else
		{
			flag = iTween.Defaults.isLocal;
		}
		Vector3 vector2;
		Vector3 vector;
		if (flag)
		{
			vector = (vector2 = target.transform.localEulerAngles);
		}
		else
		{
			vector = (vector2 = target.transform.eulerAngles);
		}
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
		iTween.Launch(target, args);
	}

	// Token: 0x06008D3E RID: 36158 RVA: 0x002D4FDA File Offset: 0x002D31DA
	public static void RotateAdd(GameObject target, Vector3 amount, float time)
	{
		iTween.RotateAdd(target, iTween.Hash(new object[]
		{
			"amount",
			amount,
			"time",
			time
		}));
	}

	// Token: 0x06008D3F RID: 36159 RVA: 0x002D500F File Offset: 0x002D320F
	public static void RotateAdd(GameObject target, Hashtable args)
	{
		args = iTween.CleanArgs(args);
		args["type"] = "rotate";
		args["method"] = "add";
		iTween.Launch(target, args);
	}

	// Token: 0x06008D40 RID: 36160 RVA: 0x002D5040 File Offset: 0x002D3240
	public static void RotateBy(GameObject target, Vector3 amount, float time)
	{
		iTween.RotateBy(target, iTween.Hash(new object[]
		{
			"amount",
			amount,
			"time",
			time
		}));
	}

	// Token: 0x06008D41 RID: 36161 RVA: 0x002D5075 File Offset: 0x002D3275
	public static void RotateBy(GameObject target, Hashtable args)
	{
		args = iTween.CleanArgs(args);
		args["type"] = "rotate";
		args["method"] = "by";
		iTween.Launch(target, args);
	}

	// Token: 0x06008D42 RID: 36162 RVA: 0x002D50A6 File Offset: 0x002D32A6
	public static void ShakePosition(GameObject target, Vector3 amount, float time)
	{
		iTween.ShakePosition(target, iTween.Hash(new object[]
		{
			"amount",
			amount,
			"time",
			time
		}));
	}

	// Token: 0x06008D43 RID: 36163 RVA: 0x002D50DB File Offset: 0x002D32DB
	public static void ShakePosition(GameObject target, Hashtable args)
	{
		args = iTween.CleanArgs(args);
		args["type"] = "shake";
		args["method"] = "position";
		iTween.Launch(target, args);
	}

	// Token: 0x06008D44 RID: 36164 RVA: 0x002D510C File Offset: 0x002D330C
	public static void ShakeScale(GameObject target, Vector3 amount, float time)
	{
		iTween.ShakeScale(target, iTween.Hash(new object[]
		{
			"amount",
			amount,
			"time",
			time
		}));
	}

	// Token: 0x06008D45 RID: 36165 RVA: 0x002D5141 File Offset: 0x002D3341
	public static void ShakeScale(GameObject target, Hashtable args)
	{
		args = iTween.CleanArgs(args);
		args["type"] = "shake";
		args["method"] = "scale";
		iTween.Launch(target, args);
	}

	// Token: 0x06008D46 RID: 36166 RVA: 0x002D5172 File Offset: 0x002D3372
	public static void ShakeRotation(GameObject target, Vector3 amount, float time)
	{
		iTween.ShakeRotation(target, iTween.Hash(new object[]
		{
			"amount",
			amount,
			"time",
			time
		}));
	}

	// Token: 0x06008D47 RID: 36167 RVA: 0x002D51A7 File Offset: 0x002D33A7
	public static void ShakeRotation(GameObject target, Hashtable args)
	{
		args = iTween.CleanArgs(args);
		args["type"] = "shake";
		args["method"] = "rotation";
		iTween.Launch(target, args);
	}

	// Token: 0x06008D48 RID: 36168 RVA: 0x002D51D8 File Offset: 0x002D33D8
	public static void PunchPosition(GameObject target, Vector3 amount, float time)
	{
		iTween.PunchPosition(target, iTween.Hash(new object[]
		{
			"amount",
			amount,
			"time",
			time
		}));
	}

	// Token: 0x06008D49 RID: 36169 RVA: 0x002D5210 File Offset: 0x002D3410
	public static void PunchPosition(GameObject target, Hashtable args)
	{
		args = iTween.CleanArgs(args);
		args["type"] = "punch";
		args["method"] = "position";
		args["easetype"] = iTween.EaseType.punch;
		iTween.Launch(target, args);
	}

	// Token: 0x06008D4A RID: 36170 RVA: 0x002D525E File Offset: 0x002D345E
	public static void PunchRotation(GameObject target, Vector3 amount, float time)
	{
		iTween.PunchRotation(target, iTween.Hash(new object[]
		{
			"amount",
			amount,
			"time",
			time
		}));
	}

	// Token: 0x06008D4B RID: 36171 RVA: 0x002D5294 File Offset: 0x002D3494
	public static void PunchRotation(GameObject target, Hashtable args)
	{
		args = iTween.CleanArgs(args);
		args["type"] = "punch";
		args["method"] = "rotation";
		args["easetype"] = iTween.EaseType.punch;
		iTween.Launch(target, args);
	}

	// Token: 0x06008D4C RID: 36172 RVA: 0x002D52E2 File Offset: 0x002D34E2
	public static void PunchScale(GameObject target, Vector3 amount, float time)
	{
		iTween.PunchScale(target, iTween.Hash(new object[]
		{
			"amount",
			amount,
			"time",
			time
		}));
	}

	// Token: 0x06008D4D RID: 36173 RVA: 0x002D5318 File Offset: 0x002D3518
	public static void PunchScale(GameObject target, Hashtable args)
	{
		args = iTween.CleanArgs(args);
		args["type"] = "punch";
		args["method"] = "scale";
		args["easetype"] = iTween.EaseType.punch;
		iTween.Launch(target, args);
	}

	// Token: 0x06008D4E RID: 36174 RVA: 0x002D5366 File Offset: 0x002D3566
	public static void Timer(GameObject target, Hashtable args)
	{
		args = iTween.CleanArgs(args);
		args["type"] = "timer";
		iTween.Launch(target, args);
	}

	// Token: 0x06008D4F RID: 36175 RVA: 0x002D5388 File Offset: 0x002D3588
	private void GenerateTargets()
	{
		string text = this.type;
		uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
		if (num <= 2361356451U)
		{
			if (num <= 1031692888U)
			{
				if (num != 407568404U)
				{
					if (num != 1031692888U)
					{
						return;
					}
					if (!(text == "color"))
					{
						return;
					}
					text = this.method;
					if (text == "to")
					{
						this.GenerateColorToTargets();
						this.apply = new iTween.ApplyTween(this.ApplyColorToTargets);
						return;
					}
				}
				else
				{
					if (!(text == "move"))
					{
						return;
					}
					text = this.method;
					if (!(text == "to"))
					{
						if (!(text == "by") && !(text == "add"))
						{
							return;
						}
						this.GenerateMoveByTargets();
						this.apply = new iTween.ApplyTween(this.ApplyMoveByTargets);
						return;
					}
					else
					{
						if (this.tweenArguments.Contains("path"))
						{
							this.GenerateMoveToPathTargets();
							this.apply = new iTween.ApplyTween(this.ApplyMoveToPathTargets);
							return;
						}
						this.GenerateMoveToTargets();
						this.apply = new iTween.ApplyTween(this.ApplyMoveToTargets);
						return;
					}
				}
			}
			else if (num != 1113510858U)
			{
				if (num != 2190941297U)
				{
					if (num != 2361356451U)
					{
						return;
					}
					if (!(text == "punch"))
					{
						return;
					}
					text = this.method;
					if (text == "position")
					{
						this.GeneratePunchPositionTargets();
						this.apply = new iTween.ApplyTween(this.ApplyPunchPositionTargets);
						return;
					}
					if (text == "rotation")
					{
						this.GeneratePunchRotationTargets();
						this.apply = new iTween.ApplyTween(this.ApplyPunchRotationTargets);
						return;
					}
					if (!(text == "scale"))
					{
						return;
					}
					this.GeneratePunchScaleTargets();
					this.apply = new iTween.ApplyTween(this.ApplyPunchScaleTargets);
					return;
				}
				else
				{
					if (!(text == "scale"))
					{
						return;
					}
					text = this.method;
					if (text == "to")
					{
						this.GenerateScaleToTargets();
						this.apply = new iTween.ApplyTween(this.ApplyScaleToTargets);
						return;
					}
					if (text == "by")
					{
						this.GenerateScaleByTargets();
						this.apply = new iTween.ApplyTween(this.ApplyScaleToTargets);
						return;
					}
					if (!(text == "add"))
					{
						return;
					}
					this.GenerateScaleAddTargets();
					this.apply = new iTween.ApplyTween(this.ApplyScaleToTargets);
					return;
				}
			}
			else
			{
				if (!(text == "value"))
				{
					return;
				}
				text = this.method;
				if (text == "float")
				{
					this.GenerateFloatTargets();
					this.apply = new iTween.ApplyTween(this.ApplyFloatTargets);
					return;
				}
				if (text == "vector2")
				{
					this.GenerateVector2Targets();
					this.apply = new iTween.ApplyTween(this.ApplyVector2Targets);
					return;
				}
				if (text == "vector3")
				{
					this.GenerateVector3Targets();
					this.apply = new iTween.ApplyTween(this.ApplyVector3Targets);
					return;
				}
				if (text == "color")
				{
					this.GenerateColorTargets();
					this.apply = new iTween.ApplyTween(this.ApplyColorTargets);
					return;
				}
				if (!(text == "rect"))
				{
					return;
				}
				this.GenerateRectTargets();
				this.apply = new iTween.ApplyTween(this.ApplyRectTargets);
				return;
			}
		}
		else if (num <= 3180049141U)
		{
			if (num != 2784296202U)
			{
				if (num != 3180049141U)
				{
					return;
				}
				if (!(text == "shake"))
				{
					return;
				}
				text = this.method;
				if (text == "position")
				{
					this.GenerateShakePositionTargets();
					this.apply = new iTween.ApplyTween(this.ApplyShakePositionTargets);
					return;
				}
				if (text == "scale")
				{
					this.GenerateShakeScaleTargets();
					this.apply = new iTween.ApplyTween(this.ApplyShakeScaleTargets);
					return;
				}
				if (!(text == "rotation"))
				{
					return;
				}
				this.GenerateShakeRotationTargets();
				this.apply = new iTween.ApplyTween(this.ApplyShakeRotationTargets);
				return;
			}
			else
			{
				if (!(text == "rotate"))
				{
					return;
				}
				text = this.method;
				if (text == "to")
				{
					this.GenerateRotateToTargets();
					this.apply = new iTween.ApplyTween(this.ApplyRotateToTargets);
					return;
				}
				if (text == "add")
				{
					this.GenerateRotateAddTargets();
					this.apply = new iTween.ApplyTween(this.ApplyRotateAddTargets);
					return;
				}
				if (!(text == "by"))
				{
					return;
				}
				this.GenerateRotateByTargets();
				this.apply = new iTween.ApplyTween(this.ApplyRotateAddTargets);
				return;
			}
		}
		else if (num != 3764468121U)
		{
			if (num != 3778758817U)
			{
				if (num != 3874444950U)
				{
					return;
				}
				if (!(text == "look"))
				{
					return;
				}
				text = this.method;
				if (text == "to")
				{
					this.GenerateLookToTargets();
					this.apply = new iTween.ApplyTween(this.ApplyLookToTargets);
					return;
				}
			}
			else
			{
				if (!(text == "stab"))
				{
					return;
				}
				this.GenerateStabTargets();
				this.apply = new iTween.ApplyTween(this.ApplyStabTargets);
			}
		}
		else
		{
			if (!(text == "audio"))
			{
				return;
			}
			text = this.method;
			if (text == "to")
			{
				this.GenerateAudioToTargets();
				this.apply = new iTween.ApplyTween(this.ApplyAudioToTargets);
				return;
			}
		}
	}

	// Token: 0x06008D50 RID: 36176 RVA: 0x002D58B0 File Offset: 0x002D3AB0
	private void GenerateRectTargets()
	{
		this.rects = new Rect[3];
		this.rects[0] = (Rect)this.tweenArguments["from"];
		this.rects[1] = (Rect)this.tweenArguments["to"];
	}

	// Token: 0x06008D51 RID: 36177 RVA: 0x002D590C File Offset: 0x002D3B0C
	private void GenerateColorTargets()
	{
		this.colors = new Color[1, 3];
		this.colors[0, 0] = (Color)this.tweenArguments["from"];
		this.colors[0, 1] = (Color)this.tweenArguments["to"];
	}

	// Token: 0x06008D52 RID: 36178 RVA: 0x002D596C File Offset: 0x002D3B6C
	private void GenerateVector3Targets()
	{
		this.vector3s = new Vector3[3];
		this.vector3s[0] = (Vector3)this.tweenArguments["from"];
		this.vector3s[1] = (Vector3)this.tweenArguments["to"];
		if (this.tweenArguments.Contains("speed"))
		{
			float num = Math.Abs(Vector3.Distance(this.vector3s[0], this.vector3s[1]));
			this.time = num / (float)this.tweenArguments["speed"];
		}
	}

	// Token: 0x06008D53 RID: 36179 RVA: 0x002D5A1C File Offset: 0x002D3C1C
	private void GenerateVector2Targets()
	{
		this.vector2s = new Vector2[3];
		this.vector2s[0] = (Vector2)this.tweenArguments["from"];
		this.vector2s[1] = (Vector2)this.tweenArguments["to"];
		if (this.tweenArguments.Contains("speed"))
		{
			Vector3 a = new Vector3(this.vector2s[0].x, this.vector2s[0].y, 0f);
			Vector3 b = new Vector3(this.vector2s[1].x, this.vector2s[1].y, 0f);
			float num = Math.Abs(Vector3.Distance(a, b));
			this.time = num / (float)this.tweenArguments["speed"];
		}
	}

	// Token: 0x06008D54 RID: 36180 RVA: 0x002D5B10 File Offset: 0x002D3D10
	private void GenerateFloatTargets()
	{
		this.floats = new float[3];
		this.floats[0] = (float)this.tweenArguments["from"];
		this.floats[1] = (float)this.tweenArguments["to"];
		if (this.tweenArguments.Contains("speed"))
		{
			float num = Math.Abs(this.floats[0] - this.floats[1]);
			this.time = num / (float)this.tweenArguments["speed"];
		}
	}

	// Token: 0x06008D55 RID: 36181 RVA: 0x002D5BAC File Offset: 0x002D3DAC
	private void GenerateColorToTargets()
	{
		if (this.GetComponent(typeof(UberText)) && !this.uberText.RenderToTexture)
		{
			this.colors = new Color[1, 3];
			this.colors[0, 0] = (this.colors[0, 1] = this.uberText.TextColor);
		}
		else if (this.renderer)
		{
			int num = 0;
			iTween.m_MaterialTempList.Clear();
			this.renderer.GetSharedMaterials(iTween.m_MaterialTempList);
			this.colors = new Color[iTween.m_MaterialTempList.Count, 3];
			for (int i = 0; i < iTween.m_MaterialTempList.Count; i++)
			{
				if (iTween.m_MaterialTempList[i] && iTween.m_MaterialTempList[i].HasProperty(this.namedColorValueString))
				{
					this.colors[i, 0] = iTween.m_MaterialTempList[i].GetColor(this.namedColorValueString);
					this.colors[i, 1] = iTween.m_MaterialTempList[i].GetColor(this.namedColorValueString);
					num++;
				}
			}
		}
		else if (this.light)
		{
			this.colors = new Color[1, 3];
			this.colors[0, 0] = (this.colors[0, 1] = this.light.color);
		}
		else
		{
			this.colors = new Color[1, 3];
		}
		if (this.tweenArguments.Contains("color"))
		{
			for (int j = 0; j < this.colors.GetLength(0); j++)
			{
				this.colors[j, 1] = (Color)this.tweenArguments["color"];
			}
		}
		else
		{
			if (this.tweenArguments.Contains("r"))
			{
				for (int k = 0; k < this.colors.GetLength(0); k++)
				{
					this.colors[k, 1].r = (float)this.tweenArguments["r"];
				}
			}
			if (this.tweenArguments.Contains("g"))
			{
				for (int l = 0; l < this.colors.GetLength(0); l++)
				{
					this.colors[l, 1].g = (float)this.tweenArguments["g"];
				}
			}
			if (this.tweenArguments.Contains("b"))
			{
				for (int m = 0; m < this.colors.GetLength(0); m++)
				{
					this.colors[m, 1].b = (float)this.tweenArguments["b"];
				}
			}
			if (this.tweenArguments.Contains("a"))
			{
				for (int n = 0; n < this.colors.GetLength(0); n++)
				{
					this.colors[n, 1].a = (float)this.tweenArguments["a"];
				}
			}
		}
		if (this.tweenArguments.Contains("amount"))
		{
			for (int num2 = 0; num2 < this.colors.GetLength(0); num2++)
			{
				this.colors[num2, 1].a = (float)this.tweenArguments["amount"];
			}
			return;
		}
		if (this.tweenArguments.Contains("alpha"))
		{
			for (int num3 = 0; num3 < this.colors.GetLength(0); num3++)
			{
				this.colors[num3, 1].a = (float)this.tweenArguments["alpha"];
			}
		}
	}

	// Token: 0x06008D56 RID: 36182 RVA: 0x002D5F88 File Offset: 0x002D4188
	private void GenerateAudioToTargets()
	{
		this.vector2s = new Vector2[3];
		if (this.tweenArguments.Contains("audiosource"))
		{
			this.audioSource = (AudioSource)this.tweenArguments["audiosource"];
		}
		else if (this.GetComponent(typeof(AudioSource)))
		{
			this.audioSource = this.audio;
		}
		else
		{
			Debug.LogError("iTween Error: AudioTo requires an AudioSource.");
			this.Dispose();
		}
		this.vector2s[0] = (this.vector2s[1] = new Vector2(this.audioSource.volume, this.audioSource.pitch));
		if (this.tweenArguments.Contains("volume"))
		{
			this.vector2s[1].x = (float)this.tweenArguments["volume"];
		}
		if (this.tweenArguments.Contains("pitch"))
		{
			this.vector2s[1].y = (float)this.tweenArguments["pitch"];
		}
	}

	// Token: 0x06008D57 RID: 36183 RVA: 0x002D60AC File Offset: 0x002D42AC
	private void GenerateStabTargets()
	{
		if (this.tweenArguments.Contains("audiosource"))
		{
			this.audioSource = (AudioSource)this.tweenArguments["audiosource"];
		}
		else if (this.GetComponent(typeof(AudioSource)))
		{
			this.audioSource = this.audio;
		}
		else
		{
			this.gameObject.AddComponent(typeof(AudioSource));
			this.audioSource = this.audio;
			this.audioSource.playOnAwake = false;
		}
		this.audioSource.clip = (AudioClip)this.tweenArguments["audioclip"];
		if (this.tweenArguments.Contains("pitch"))
		{
			this.audioSource.pitch = (float)this.tweenArguments["pitch"];
		}
		if (this.tweenArguments.Contains("volume"))
		{
			this.audioSource.volume = (float)this.tweenArguments["volume"];
		}
		this.time = this.audioSource.clip.length / this.audioSource.pitch;
	}

	// Token: 0x06008D58 RID: 36184 RVA: 0x002D61E4 File Offset: 0x002D43E4
	private void GenerateLookToTargets()
	{
		this.vector3s = new Vector3[3];
		this.vector3s[0] = this.transform.eulerAngles;
		if (this.tweenArguments.Contains("looktarget"))
		{
			if (this.tweenArguments["looktarget"].GetType() == typeof(Transform))
			{
				this.transform.LookAt((Transform)this.tweenArguments["looktarget"], ((Vector3?)this.tweenArguments["up"]) ?? iTween.Defaults.up);
			}
			else if (this.tweenArguments["looktarget"].GetType() == typeof(Vector3))
			{
				this.transform.LookAt((Vector3)this.tweenArguments["looktarget"], ((Vector3?)this.tweenArguments["up"]) ?? iTween.Defaults.up);
			}
		}
		else
		{
			Debug.LogError("iTween Error: LookTo needs a 'looktarget' property!");
			this.Dispose();
		}
		this.vector3s[1] = this.transform.eulerAngles;
		this.transform.eulerAngles = this.vector3s[0];
		if (this.tweenArguments.Contains("axis"))
		{
			string a = (string)this.tweenArguments["axis"];
			if (!(a == "x"))
			{
				if (!(a == "y"))
				{
					if (a == "z")
					{
						this.vector3s[1].x = this.vector3s[0].x;
						this.vector3s[1].y = this.vector3s[0].y;
					}
				}
				else
				{
					this.vector3s[1].x = this.vector3s[0].x;
					this.vector3s[1].z = this.vector3s[0].z;
				}
			}
			else
			{
				this.vector3s[1].y = this.vector3s[0].y;
				this.vector3s[1].z = this.vector3s[0].z;
			}
		}
		this.vector3s[1] = new Vector3(this.clerp(this.vector3s[0].x, this.vector3s[1].x, 1f), this.clerp(this.vector3s[0].y, this.vector3s[1].y, 1f), this.clerp(this.vector3s[0].z, this.vector3s[1].z, 1f));
		if (this.tweenArguments.Contains("speed"))
		{
			float num = Math.Abs(Vector3.Distance(this.vector3s[0], this.vector3s[1]));
			this.time = num / (float)this.tweenArguments["speed"];
		}
	}

	// Token: 0x06008D59 RID: 36185 RVA: 0x002D656C File Offset: 0x002D476C
	private void GenerateMoveToPathTargets()
	{
		Vector3[] array2;
		if (this.tweenArguments["path"].GetType() == typeof(Vector3[]))
		{
			Vector3[] array = (Vector3[])this.tweenArguments["path"];
			if (array.Length == 1)
			{
				Debug.LogError("iTween Error: Attempting a path movement with MoveTo requires an array of more than 1 entry!");
				this.Dispose();
			}
			array2 = new Vector3[array.Length];
			Array.Copy(array, array2, array.Length);
		}
		else
		{
			Transform[] array3 = (Transform[])this.tweenArguments["path"];
			if (array3.Length == 1)
			{
				Debug.LogError("iTween Error: Attempting a path movement with MoveTo requires an array of more than 1 entry!");
				this.Dispose();
			}
			array2 = new Vector3[array3.Length];
			for (int i = 0; i < array3.Length; i++)
			{
				array2[i] = array3[i].position;
			}
		}
		bool flag;
		int num;
		if (this.transform.position != array2[0])
		{
			if (!this.tweenArguments.Contains("movetopath") || (bool)this.tweenArguments["movetopath"])
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
		this.vector3s = new Vector3[array2.Length + num];
		if (flag)
		{
			this.vector3s[1] = this.transform.position;
			num = 2;
		}
		else
		{
			num = 1;
		}
		Array.Copy(array2, 0, this.vector3s, num, array2.Length);
		this.vector3s[0] = this.vector3s[1] + (this.vector3s[1] - this.vector3s[2]);
		this.vector3s[this.vector3s.Length - 1] = this.vector3s[this.vector3s.Length - 2] + (this.vector3s[this.vector3s.Length - 2] - this.vector3s[this.vector3s.Length - 3]);
		if (this.vector3s[1] == this.vector3s[this.vector3s.Length - 2])
		{
			Vector3[] array4 = new Vector3[this.vector3s.Length];
			Array.Copy(this.vector3s, array4, this.vector3s.Length);
			array4[0] = array4[array4.Length - 3];
			array4[array4.Length - 1] = array4[2];
			this.vector3s = new Vector3[array4.Length];
			Array.Copy(array4, this.vector3s, array4.Length);
		}
		this.path = new iTween.CRSpline(this.vector3s);
		if (this.tweenArguments.Contains("speed"))
		{
			float num2 = iTween.PathLength(this.vector3s);
			this.time = num2 / (float)this.tweenArguments["speed"];
		}
	}

	// Token: 0x06008D5A RID: 36186 RVA: 0x002D684C File Offset: 0x002D4A4C
	private void GenerateMoveToTargets()
	{
		this.vector3s = new Vector3[3];
		if (this.isLocal)
		{
			this.vector3s[0] = (this.vector3s[1] = this.transform.localPosition);
		}
		else
		{
			this.vector3s[0] = (this.vector3s[1] = this.transform.position);
		}
		if (this.tweenArguments.Contains("position"))
		{
			if (this.tweenArguments["position"].GetType() == typeof(Transform))
			{
				Transform transform = (Transform)this.tweenArguments["position"];
				this.vector3s[1] = transform.position;
			}
			else if (this.tweenArguments["position"].GetType() == typeof(Vector3))
			{
				this.vector3s[1] = (Vector3)this.tweenArguments["position"];
			}
		}
		else
		{
			if (this.tweenArguments.Contains("x"))
			{
				this.vector3s[1].x = (float)this.tweenArguments["x"];
			}
			if (this.tweenArguments.Contains("y"))
			{
				this.vector3s[1].y = (float)this.tweenArguments["y"];
			}
			if (this.tweenArguments.Contains("z"))
			{
				this.vector3s[1].z = (float)this.tweenArguments["z"];
			}
		}
		if (this.tweenArguments.Contains("orienttopath") && (bool)this.tweenArguments["orienttopath"])
		{
			this.tweenArguments["looktarget"] = this.vector3s[1];
		}
		if (this.tweenArguments.Contains("speed"))
		{
			float num = Math.Abs(Vector3.Distance(this.vector3s[0], this.vector3s[1]));
			this.time = num / (float)this.tweenArguments["speed"];
		}
	}

	// Token: 0x06008D5B RID: 36187 RVA: 0x002D6AB8 File Offset: 0x002D4CB8
	private void GenerateMoveByTargets()
	{
		this.vector3s = new Vector3[6];
		this.vector3s[4] = this.transform.eulerAngles;
		this.vector3s[0] = (this.vector3s[1] = (this.vector3s[3] = this.transform.position));
		if (this.tweenArguments.Contains("amount"))
		{
			this.vector3s[1] = this.vector3s[0] + (Vector3)this.tweenArguments["amount"];
		}
		else
		{
			if (this.tweenArguments.Contains("x"))
			{
				this.vector3s[1].x = this.vector3s[0].x + (float)this.tweenArguments["x"];
			}
			if (this.tweenArguments.Contains("y"))
			{
				this.vector3s[1].y = this.vector3s[0].y + (float)this.tweenArguments["y"];
			}
			if (this.tweenArguments.Contains("z"))
			{
				this.vector3s[1].z = this.vector3s[0].z + (float)this.tweenArguments["z"];
			}
		}
		this.transform.Translate(this.vector3s[1], this.space);
		this.vector3s[5] = this.transform.position;
		this.transform.position = this.vector3s[0];
		if (this.tweenArguments.Contains("orienttopath") && (bool)this.tweenArguments["orienttopath"])
		{
			this.tweenArguments["looktarget"] = this.vector3s[1];
		}
		if (this.tweenArguments.Contains("speed"))
		{
			float num = Math.Abs(Vector3.Distance(this.vector3s[0], this.vector3s[1]));
			this.time = num / (float)this.tweenArguments["speed"];
		}
	}

	// Token: 0x06008D5C RID: 36188 RVA: 0x002D6D2C File Offset: 0x002D4F2C
	private void GenerateScaleToTargets()
	{
		this.vector3s = new Vector3[3];
		this.vector3s[0] = (this.vector3s[1] = this.transform.localScale);
		if (this.tweenArguments.Contains("scale"))
		{
			if (this.tweenArguments["scale"].GetType() == typeof(Transform))
			{
				Transform transform = (Transform)this.tweenArguments["scale"];
				this.vector3s[1] = transform.localScale;
			}
			else if (this.tweenArguments["scale"].GetType() == typeof(Vector3))
			{
				this.vector3s[1] = (Vector3)this.tweenArguments["scale"];
			}
		}
		else
		{
			if (this.tweenArguments.Contains("x"))
			{
				this.vector3s[1].x = (float)this.tweenArguments["x"];
			}
			if (this.tweenArguments.Contains("y"))
			{
				this.vector3s[1].y = (float)this.tweenArguments["y"];
			}
			if (this.tweenArguments.Contains("z"))
			{
				this.vector3s[1].z = (float)this.tweenArguments["z"];
			}
		}
		if (this.tweenArguments.Contains("speed"))
		{
			float num = Math.Abs(Vector3.Distance(this.vector3s[0], this.vector3s[1]));
			this.time = num / (float)this.tweenArguments["speed"];
		}
	}

	// Token: 0x06008D5D RID: 36189 RVA: 0x002D6F1C File Offset: 0x002D511C
	private void GenerateScaleByTargets()
	{
		this.vector3s = new Vector3[3];
		this.vector3s[0] = (this.vector3s[1] = this.transform.localScale);
		if (this.tweenArguments.Contains("amount"))
		{
			this.vector3s[1] = Vector3.Scale(this.vector3s[1], (Vector3)this.tweenArguments["amount"]);
		}
		else
		{
			if (this.tweenArguments.Contains("x"))
			{
				Vector3[] array = this.vector3s;
				int num = 1;
				array[num].x = array[num].x * (float)this.tweenArguments["x"];
			}
			if (this.tweenArguments.Contains("y"))
			{
				Vector3[] array2 = this.vector3s;
				int num2 = 1;
				array2[num2].y = array2[num2].y * (float)this.tweenArguments["y"];
			}
			if (this.tweenArguments.Contains("z"))
			{
				Vector3[] array3 = this.vector3s;
				int num3 = 1;
				array3[num3].z = array3[num3].z * (float)this.tweenArguments["z"];
			}
		}
		if (this.tweenArguments.Contains("speed"))
		{
			float num4 = Math.Abs(Vector3.Distance(this.vector3s[0], this.vector3s[1]));
			this.time = num4 / (float)this.tweenArguments["speed"];
		}
	}

	// Token: 0x06008D5E RID: 36190 RVA: 0x002D70AC File Offset: 0x002D52AC
	private void GenerateScaleAddTargets()
	{
		this.vector3s = new Vector3[3];
		this.vector3s[0] = (this.vector3s[1] = this.transform.localScale);
		if (this.tweenArguments.Contains("amount"))
		{
			this.vector3s[1] += (Vector3)this.tweenArguments["amount"];
		}
		else
		{
			if (this.tweenArguments.Contains("x"))
			{
				Vector3[] array = this.vector3s;
				int num = 1;
				array[num].x = array[num].x + (float)this.tweenArguments["x"];
			}
			if (this.tweenArguments.Contains("y"))
			{
				Vector3[] array2 = this.vector3s;
				int num2 = 1;
				array2[num2].y = array2[num2].y + (float)this.tweenArguments["y"];
			}
			if (this.tweenArguments.Contains("z"))
			{
				Vector3[] array3 = this.vector3s;
				int num3 = 1;
				array3[num3].z = array3[num3].z + (float)this.tweenArguments["z"];
			}
		}
		if (this.tweenArguments.Contains("speed"))
		{
			float num4 = Math.Abs(Vector3.Distance(this.vector3s[0], this.vector3s[1]));
			this.time = num4 / (float)this.tweenArguments["speed"];
		}
	}

	// Token: 0x06008D5F RID: 36191 RVA: 0x002D723C File Offset: 0x002D543C
	private void GenerateRotateToTargets()
	{
		this.vector3s = new Vector3[3];
		if (this.isLocal)
		{
			this.vector3s[0] = (this.vector3s[1] = this.transform.localEulerAngles);
		}
		else
		{
			this.vector3s[0] = (this.vector3s[1] = this.transform.eulerAngles);
		}
		if (this.tweenArguments.Contains("rotation"))
		{
			if (this.tweenArguments["rotation"].GetType() == typeof(Transform))
			{
				Transform transform = (Transform)this.tweenArguments["rotation"];
				this.vector3s[1] = transform.eulerAngles;
			}
			else if (this.tweenArguments["rotation"].GetType() == typeof(Vector3))
			{
				this.vector3s[1] = (Vector3)this.tweenArguments["rotation"];
			}
		}
		else
		{
			if (this.tweenArguments.Contains("x"))
			{
				this.vector3s[1].x = (float)this.tweenArguments["x"];
			}
			if (this.tweenArguments.Contains("y"))
			{
				this.vector3s[1].y = (float)this.tweenArguments["y"];
			}
			if (this.tweenArguments.Contains("z"))
			{
				this.vector3s[1].z = (float)this.tweenArguments["z"];
			}
		}
		this.vector3s[1] = new Vector3(this.clerp(this.vector3s[0].x, this.vector3s[1].x, 1f), this.clerp(this.vector3s[0].y, this.vector3s[1].y, 1f), this.clerp(this.vector3s[0].z, this.vector3s[1].z, 1f));
		if (this.tweenArguments.Contains("speed"))
		{
			float num = Math.Abs(Vector3.Distance(this.vector3s[0], this.vector3s[1]));
			this.time = num / (float)this.tweenArguments["speed"];
		}
	}

	// Token: 0x06008D60 RID: 36192 RVA: 0x002D74F4 File Offset: 0x002D56F4
	private void GenerateRotateAddTargets()
	{
		this.vector3s = new Vector3[5];
		this.vector3s[0] = (this.vector3s[1] = (this.vector3s[3] = this.transform.eulerAngles));
		if (this.tweenArguments.Contains("amount"))
		{
			this.vector3s[1] += (Vector3)this.tweenArguments["amount"];
		}
		else
		{
			if (this.tweenArguments.Contains("x"))
			{
				Vector3[] array = this.vector3s;
				int num = 1;
				array[num].x = array[num].x + (float)this.tweenArguments["x"];
			}
			if (this.tweenArguments.Contains("y"))
			{
				Vector3[] array2 = this.vector3s;
				int num2 = 1;
				array2[num2].y = array2[num2].y + (float)this.tweenArguments["y"];
			}
			if (this.tweenArguments.Contains("z"))
			{
				Vector3[] array3 = this.vector3s;
				int num3 = 1;
				array3[num3].z = array3[num3].z + (float)this.tweenArguments["z"];
			}
		}
		if (this.tweenArguments.Contains("speed"))
		{
			float num4 = Math.Abs(Vector3.Distance(this.vector3s[0], this.vector3s[1]));
			this.time = num4 / (float)this.tweenArguments["speed"];
		}
	}

	// Token: 0x06008D61 RID: 36193 RVA: 0x002D7690 File Offset: 0x002D5890
	private void GenerateRotateByTargets()
	{
		this.vector3s = new Vector3[4];
		this.vector3s[0] = (this.vector3s[1] = (this.vector3s[3] = this.transform.eulerAngles));
		if (this.tweenArguments.Contains("amount"))
		{
			this.vector3s[1] += Vector3.Scale((Vector3)this.tweenArguments["amount"], new Vector3(360f, 360f, 360f));
		}
		else
		{
			if (this.tweenArguments.Contains("x"))
			{
				Vector3[] array = this.vector3s;
				int num = 1;
				array[num].x = array[num].x + 360f * (float)this.tweenArguments["x"];
			}
			if (this.tweenArguments.Contains("y"))
			{
				Vector3[] array2 = this.vector3s;
				int num2 = 1;
				array2[num2].y = array2[num2].y + 360f * (float)this.tweenArguments["y"];
			}
			if (this.tweenArguments.Contains("z"))
			{
				Vector3[] array3 = this.vector3s;
				int num3 = 1;
				array3[num3].z = array3[num3].z + 360f * (float)this.tweenArguments["z"];
			}
		}
		if (this.tweenArguments.Contains("speed"))
		{
			float num4 = Math.Abs(Vector3.Distance(this.vector3s[0], this.vector3s[1]));
			this.time = num4 / (float)this.tweenArguments["speed"];
		}
	}

	// Token: 0x06008D62 RID: 36194 RVA: 0x002D7858 File Offset: 0x002D5A58
	private void GenerateShakePositionTargets()
	{
		this.vector3s = new Vector3[4];
		this.vector3s[3] = this.transform.eulerAngles;
		if (this.isLocal)
		{
			this.vector3s[0] = this.transform.localPosition;
		}
		else
		{
			this.vector3s[0] = this.transform.position;
		}
		if (this.tweenArguments.Contains("amount"))
		{
			this.vector3s[1] = (Vector3)this.tweenArguments["amount"];
			return;
		}
		if (this.tweenArguments.Contains("x"))
		{
			this.vector3s[1].x = (float)this.tweenArguments["x"];
		}
		if (this.tweenArguments.Contains("y"))
		{
			this.vector3s[1].y = (float)this.tweenArguments["y"];
		}
		if (this.tweenArguments.Contains("z"))
		{
			this.vector3s[1].z = (float)this.tweenArguments["z"];
		}
	}

	// Token: 0x06008D63 RID: 36195 RVA: 0x002D799C File Offset: 0x002D5B9C
	private void GenerateShakeScaleTargets()
	{
		this.vector3s = new Vector3[3];
		this.vector3s[0] = this.transform.localScale;
		if (this.tweenArguments.Contains("amount"))
		{
			this.vector3s[1] = (Vector3)this.tweenArguments["amount"];
			return;
		}
		if (this.tweenArguments.Contains("x"))
		{
			this.vector3s[1].x = (float)this.tweenArguments["x"];
		}
		if (this.tweenArguments.Contains("y"))
		{
			this.vector3s[1].y = (float)this.tweenArguments["y"];
		}
		if (this.tweenArguments.Contains("z"))
		{
			this.vector3s[1].z = (float)this.tweenArguments["z"];
		}
	}

	// Token: 0x06008D64 RID: 36196 RVA: 0x002D7AA8 File Offset: 0x002D5CA8
	private void GenerateShakeRotationTargets()
	{
		this.vector3s = new Vector3[3];
		this.vector3s[0] = this.transform.eulerAngles;
		if (this.tweenArguments.Contains("amount"))
		{
			this.vector3s[1] = (Vector3)this.tweenArguments["amount"];
			return;
		}
		if (this.tweenArguments.Contains("x"))
		{
			this.vector3s[1].x = (float)this.tweenArguments["x"];
		}
		if (this.tweenArguments.Contains("y"))
		{
			this.vector3s[1].y = (float)this.tweenArguments["y"];
		}
		if (this.tweenArguments.Contains("z"))
		{
			this.vector3s[1].z = (float)this.tweenArguments["z"];
		}
	}

	// Token: 0x06008D65 RID: 36197 RVA: 0x002D7BB4 File Offset: 0x002D5DB4
	private void GeneratePunchPositionTargets()
	{
		this.vector3s = new Vector3[5];
		this.vector3s[4] = this.transform.eulerAngles;
		this.vector3s[0] = this.transform.position;
		this.vector3s[1] = (this.vector3s[3] = Vector3.zero);
		if (this.tweenArguments.Contains("amount"))
		{
			this.vector3s[1] = (Vector3)this.tweenArguments["amount"];
			return;
		}
		if (this.tweenArguments.Contains("x"))
		{
			this.vector3s[1].x = (float)this.tweenArguments["x"];
		}
		if (this.tweenArguments.Contains("y"))
		{
			this.vector3s[1].y = (float)this.tweenArguments["y"];
		}
		if (this.tweenArguments.Contains("z"))
		{
			this.vector3s[1].z = (float)this.tweenArguments["z"];
		}
	}

	// Token: 0x06008D66 RID: 36198 RVA: 0x002D7CF8 File Offset: 0x002D5EF8
	private void GeneratePunchRotationTargets()
	{
		this.vector3s = new Vector3[4];
		this.vector3s[0] = this.transform.eulerAngles;
		this.vector3s[1] = (this.vector3s[3] = Vector3.zero);
		if (this.tweenArguments.Contains("amount"))
		{
			this.vector3s[1] = (Vector3)this.tweenArguments["amount"];
			return;
		}
		if (this.tweenArguments.Contains("x"))
		{
			this.vector3s[1].x = (float)this.tweenArguments["x"];
		}
		if (this.tweenArguments.Contains("y"))
		{
			this.vector3s[1].y = (float)this.tweenArguments["y"];
		}
		if (this.tweenArguments.Contains("z"))
		{
			this.vector3s[1].z = (float)this.tweenArguments["z"];
		}
	}

	// Token: 0x06008D67 RID: 36199 RVA: 0x002D7E24 File Offset: 0x002D6024
	private void GeneratePunchScaleTargets()
	{
		this.vector3s = new Vector3[3];
		this.vector3s[0] = this.transform.localScale;
		this.vector3s[1] = Vector3.zero;
		if (this.tweenArguments.Contains("amount"))
		{
			this.vector3s[1] = (Vector3)this.tweenArguments["amount"];
			return;
		}
		if (this.tweenArguments.Contains("x"))
		{
			this.vector3s[1].x = (float)this.tweenArguments["x"];
		}
		if (this.tweenArguments.Contains("y"))
		{
			this.vector3s[1].y = (float)this.tweenArguments["y"];
		}
		if (this.tweenArguments.Contains("z"))
		{
			this.vector3s[1].z = (float)this.tweenArguments["z"];
		}
	}

	// Token: 0x06008D68 RID: 36200 RVA: 0x002D7F44 File Offset: 0x002D6144
	private void ApplyRectTargets()
	{
		this.rects[2].x = this.ease(this.rects[0].x, this.rects[1].x, this.percentage);
		this.rects[2].y = this.ease(this.rects[0].y, this.rects[1].y, this.percentage);
		this.rects[2].width = this.ease(this.rects[0].width, this.rects[1].width, this.percentage);
		this.rects[2].height = this.ease(this.rects[0].height, this.rects[1].height, this.percentage);
		this.tweenArguments["onupdateparams"] = this.rects[2];
		if (this.percentage == 1f)
		{
			this.tweenArguments["onupdateparams"] = this.rects[1];
		}
	}

	// Token: 0x06008D69 RID: 36201 RVA: 0x002D80B0 File Offset: 0x002D62B0
	private void ApplyColorTargets()
	{
		this.colors[0, 2].r = this.ease(this.colors[0, 0].r, this.colors[0, 1].r, this.percentage);
		this.colors[0, 2].g = this.ease(this.colors[0, 0].g, this.colors[0, 1].g, this.percentage);
		this.colors[0, 2].b = this.ease(this.colors[0, 0].b, this.colors[0, 1].b, this.percentage);
		this.colors[0, 2].a = this.ease(this.colors[0, 0].a, this.colors[0, 1].a, this.percentage);
		this.tweenArguments["onupdateparams"] = this.colors[0, 2];
		if (this.percentage == 1f)
		{
			this.tweenArguments["onupdateparams"] = this.colors[0, 1];
		}
	}

	// Token: 0x06008D6A RID: 36202 RVA: 0x002D822C File Offset: 0x002D642C
	private void ApplyVector3Targets()
	{
		this.vector3s[2].x = this.ease(this.vector3s[0].x, this.vector3s[1].x, this.percentage);
		this.vector3s[2].y = this.ease(this.vector3s[0].y, this.vector3s[1].y, this.percentage);
		this.vector3s[2].z = this.ease(this.vector3s[0].z, this.vector3s[1].z, this.percentage);
		this.tweenArguments["onupdateparams"] = this.vector3s[2];
		if (this.percentage == 1f)
		{
			this.tweenArguments["onupdateparams"] = this.vector3s[1];
		}
	}

	// Token: 0x06008D6B RID: 36203 RVA: 0x002D8354 File Offset: 0x002D6554
	private void ApplyVector2Targets()
	{
		this.vector2s[2].x = this.ease(this.vector2s[0].x, this.vector2s[1].x, this.percentage);
		this.vector2s[2].y = this.ease(this.vector2s[0].y, this.vector2s[1].y, this.percentage);
		this.tweenArguments["onupdateparams"] = this.vector2s[2];
		if (this.percentage == 1f)
		{
			this.tweenArguments["onupdateparams"] = this.vector2s[1];
		}
	}

	// Token: 0x06008D6C RID: 36204 RVA: 0x002D8438 File Offset: 0x002D6638
	private void ApplyFloatTargets()
	{
		this.floats[2] = this.ease(this.floats[0], this.floats[1], this.percentage);
		this.tweenArguments["onupdateparams"] = this.floats[2];
		if (this.percentage == 1f)
		{
			this.tweenArguments["onupdateparams"] = this.floats[1];
		}
	}

	// Token: 0x06008D6D RID: 36205 RVA: 0x002D84B8 File Offset: 0x002D66B8
	private void ApplyColorToTargets()
	{
		for (int i = 0; i < this.colors.GetLength(0); i++)
		{
			this.colors[i, 2].r = this.ease(this.colors[i, 0].r, this.colors[i, 1].r, this.percentage);
			this.colors[i, 2].g = this.ease(this.colors[i, 0].g, this.colors[i, 1].g, this.percentage);
			this.colors[i, 2].b = this.ease(this.colors[i, 0].b, this.colors[i, 1].b, this.percentage);
			this.colors[i, 2].a = this.ease(this.colors[i, 0].a, this.colors[i, 1].a, this.percentage);
		}
		if (this.GetComponent(typeof(UberText)) && !this.uberText.RenderToTexture)
		{
			this.uberText.TextAlpha = this.colors[0, 2].a;
			this.uberText.OutlineAlpha = this.colors[0, 2].a;
			this.uberText.ShadowAlpha = this.colors[0, 2].a;
		}
		else if (this.renderer)
		{
			iTween.m_MaterialTempList.Clear();
			this.renderer.GetMaterials(iTween.m_MaterialTempList);
			for (int j = 0; j < this.colors.GetLength(0); j++)
			{
				if (iTween.m_MaterialTempList[j])
				{
					iTween.m_MaterialTempList[j].SetColor(this.namedColorValueString, this.colors[j, 2]);
				}
			}
		}
		else if (this.light)
		{
			this.light.color = this.colors[0, 2];
		}
		if (this.percentage == 1f)
		{
			if (this.renderer)
			{
				iTween.m_MaterialTempList.Clear();
				this.renderer.GetMaterials(iTween.m_MaterialTempList);
				for (int k = 0; k < this.colors.GetLength(0); k++)
				{
					if (iTween.m_MaterialTempList[k])
					{
						iTween.m_MaterialTempList[k].SetColor(this.namedColorValueString, this.colors[k, 1]);
					}
				}
				return;
			}
			if (this.light)
			{
				this.light.color = this.colors[0, 1];
			}
		}
	}

	// Token: 0x06008D6E RID: 36206 RVA: 0x002D87C4 File Offset: 0x002D69C4
	private void ApplyAudioToTargets()
	{
		this.vector2s[2].x = this.ease(this.vector2s[0].x, this.vector2s[1].x, this.percentage);
		this.vector2s[2].y = this.ease(this.vector2s[0].y, this.vector2s[1].y, this.percentage);
		this.audioSource.volume = this.vector2s[2].x;
		this.audioSource.pitch = this.vector2s[2].y;
		if (this.percentage == 1f)
		{
			this.audioSource.volume = this.vector2s[1].x;
			this.audioSource.pitch = this.vector2s[1].y;
		}
	}

	// Token: 0x06008D6F RID: 36207 RVA: 0x00003BE8 File Offset: 0x00001DE8
	private void ApplyStabTargets()
	{
	}

	// Token: 0x06008D70 RID: 36208 RVA: 0x002D88D8 File Offset: 0x002D6AD8
	private void ApplyMoveToPathTargets()
	{
		this.preUpdate = this.transform.position;
		float value = this.ease(0f, 1f, this.percentage);
		if (this.isLocal)
		{
			this.transform.localPosition = this.path.Interp(Mathf.Clamp(value, 0f, 1f));
		}
		else
		{
			this.transform.position = this.path.Interp(Mathf.Clamp(value, 0f, 1f));
		}
		if (this.tweenArguments.Contains("orienttopath") && (bool)this.tweenArguments["orienttopath"])
		{
			float num;
			if (this.tweenArguments.Contains("lookahead"))
			{
				num = (float)this.tweenArguments["lookahead"];
			}
			else
			{
				num = iTween.Defaults.lookAhead;
			}
			float value2 = this.ease(0f, 1f, Mathf.Min(1f, this.percentage + num));
			this.tweenArguments["looktarget"] = this.path.Interp(Mathf.Clamp(value2, 0f, 1f));
		}
		this.postUpdate = this.transform.position;
		if (this.physics)
		{
			this.transform.position = this.preUpdate;
			this.rigidbody.MovePosition(this.postUpdate);
		}
	}

	// Token: 0x06008D71 RID: 36209 RVA: 0x002D8A5C File Offset: 0x002D6C5C
	private void ApplyMoveToTargets()
	{
		this.preUpdate = this.transform.position;
		this.vector3s[2].x = this.ease(this.vector3s[0].x, this.vector3s[1].x, this.percentage);
		this.vector3s[2].y = this.ease(this.vector3s[0].y, this.vector3s[1].y, this.percentage);
		this.vector3s[2].z = this.ease(this.vector3s[0].z, this.vector3s[1].z, this.percentage);
		if (this.isLocal)
		{
			this.transform.localPosition = this.vector3s[2];
		}
		else
		{
			this.transform.position = this.vector3s[2];
		}
		if (this.percentage == 1f)
		{
			if (this.isLocal)
			{
				this.transform.localPosition = this.vector3s[1];
			}
			else
			{
				this.transform.position = this.vector3s[1];
			}
		}
		this.postUpdate = this.transform.position;
		if (this.physics)
		{
			this.transform.position = this.preUpdate;
			this.rigidbody.MovePosition(this.postUpdate);
		}
	}

	// Token: 0x06008D72 RID: 36210 RVA: 0x002D8C00 File Offset: 0x002D6E00
	private void ApplyMoveByTargets()
	{
		this.preUpdate = this.transform.position;
		Vector3 eulerAngles = default(Vector3);
		if (this.tweenArguments.Contains("looktarget"))
		{
			eulerAngles = this.transform.eulerAngles;
			this.transform.eulerAngles = this.vector3s[4];
		}
		this.vector3s[2].x = this.ease(this.vector3s[0].x, this.vector3s[1].x, this.percentage);
		this.vector3s[2].y = this.ease(this.vector3s[0].y, this.vector3s[1].y, this.percentage);
		this.vector3s[2].z = this.ease(this.vector3s[0].z, this.vector3s[1].z, this.percentage);
		this.transform.Translate(this.vector3s[2] - this.vector3s[3], this.space);
		this.vector3s[3] = this.vector3s[2];
		if (this.tweenArguments.Contains("looktarget"))
		{
			this.transform.eulerAngles = eulerAngles;
		}
		this.postUpdate = this.transform.position;
		if (this.physics)
		{
			this.transform.position = this.preUpdate;
			this.rigidbody.MovePosition(this.postUpdate);
		}
	}

	// Token: 0x06008D73 RID: 36211 RVA: 0x002D8DC8 File Offset: 0x002D6FC8
	private void ApplyScaleToTargets()
	{
		this.vector3s[2].x = this.ease(this.vector3s[0].x, this.vector3s[1].x, this.percentage);
		this.vector3s[2].y = this.ease(this.vector3s[0].y, this.vector3s[1].y, this.percentage);
		this.vector3s[2].z = this.ease(this.vector3s[0].z, this.vector3s[1].z, this.percentage);
		this.transform.localScale = this.vector3s[2];
		if (this.percentage == 1f)
		{
			this.transform.localScale = this.vector3s[1];
		}
	}

	// Token: 0x06008D74 RID: 36212 RVA: 0x002D8EDC File Offset: 0x002D70DC
	private void ApplyLookToTargets()
	{
		this.vector3s[2].x = this.ease(this.vector3s[0].x, this.vector3s[1].x, this.percentage);
		this.vector3s[2].y = this.ease(this.vector3s[0].y, this.vector3s[1].y, this.percentage);
		this.vector3s[2].z = this.ease(this.vector3s[0].z, this.vector3s[1].z, this.percentage);
		if (this.isLocal)
		{
			this.transform.localRotation = Quaternion.Euler(this.vector3s[2]);
			return;
		}
		this.transform.rotation = Quaternion.Euler(this.vector3s[2]);
	}

	// Token: 0x06008D75 RID: 36213 RVA: 0x002D8FF8 File Offset: 0x002D71F8
	private void ApplyRotateToTargets()
	{
		this.preUpdate = this.transform.eulerAngles;
		this.vector3s[2].x = this.ease(this.vector3s[0].x, this.vector3s[1].x, this.percentage);
		this.vector3s[2].y = this.ease(this.vector3s[0].y, this.vector3s[1].y, this.percentage);
		this.vector3s[2].z = this.ease(this.vector3s[0].z, this.vector3s[1].z, this.percentage);
		if (this.isLocal)
		{
			this.transform.localRotation = Quaternion.Euler(this.vector3s[2]);
		}
		else
		{
			this.transform.rotation = Quaternion.Euler(this.vector3s[2]);
		}
		if (this.percentage == 1f)
		{
			if (this.isLocal)
			{
				this.transform.localRotation = Quaternion.Euler(this.vector3s[1]);
			}
			else
			{
				this.transform.rotation = Quaternion.Euler(this.vector3s[1]);
			}
		}
		this.postUpdate = this.transform.eulerAngles;
		if (this.physics)
		{
			this.transform.eulerAngles = this.preUpdate;
			this.rigidbody.MoveRotation(Quaternion.Euler(this.postUpdate));
		}
	}

	// Token: 0x06008D76 RID: 36214 RVA: 0x002D91B4 File Offset: 0x002D73B4
	private void ApplyRotateAddTargets()
	{
		this.preUpdate = this.transform.eulerAngles;
		this.vector3s[2].x = this.ease(this.vector3s[0].x, this.vector3s[1].x, this.percentage);
		this.vector3s[2].y = this.ease(this.vector3s[0].y, this.vector3s[1].y, this.percentage);
		this.vector3s[2].z = this.ease(this.vector3s[0].z, this.vector3s[1].z, this.percentage);
		this.transform.Rotate(this.vector3s[2] - this.vector3s[3], this.space);
		this.vector3s[3] = this.vector3s[2];
		this.postUpdate = this.transform.eulerAngles;
		if (this.physics)
		{
			this.transform.eulerAngles = this.preUpdate;
			this.rigidbody.MoveRotation(Quaternion.Euler(this.postUpdate));
		}
	}

	// Token: 0x06008D77 RID: 36215 RVA: 0x002D9324 File Offset: 0x002D7524
	private void ApplyShakePositionTargets()
	{
		if (this.isLocal)
		{
			this.preUpdate = this.transform.localPosition;
		}
		else
		{
			this.preUpdate = this.transform.position;
		}
		Vector3 eulerAngles = default(Vector3);
		if (this.tweenArguments.Contains("looktarget"))
		{
			eulerAngles = this.transform.eulerAngles;
			this.transform.eulerAngles = this.vector3s[3];
		}
		float num = 1f - this.percentage;
		this.vector3s[2].x = UnityEngine.Random.Range(-this.vector3s[1].x * num, this.vector3s[1].x * num);
		this.vector3s[2].y = UnityEngine.Random.Range(-this.vector3s[1].y * num, this.vector3s[1].y * num);
		this.vector3s[2].z = UnityEngine.Random.Range(-this.vector3s[1].z * num, this.vector3s[1].z * num);
		if (this.isLocal)
		{
			this.transform.localPosition = this.vector3s[0] + this.vector3s[2];
		}
		else
		{
			this.transform.position = this.vector3s[0] + this.vector3s[2];
		}
		if (this.tweenArguments.Contains("looktarget"))
		{
			this.transform.eulerAngles = eulerAngles;
		}
		this.postUpdate = this.transform.position;
		if (this.physics)
		{
			this.transform.position = this.preUpdate;
			this.rigidbody.MovePosition(this.postUpdate);
		}
	}

	// Token: 0x06008D78 RID: 36216 RVA: 0x002D9514 File Offset: 0x002D7714
	private void ApplyShakeScaleTargets()
	{
		if (this.percentage == 0f)
		{
			this.transform.localScale = this.vector3s[1];
		}
		this.transform.localScale = this.vector3s[0];
		float num = 1f - this.percentage;
		this.vector3s[2].x = UnityEngine.Random.Range(-this.vector3s[1].x * num, this.vector3s[1].x * num);
		this.vector3s[2].y = UnityEngine.Random.Range(-this.vector3s[1].y * num, this.vector3s[1].y * num);
		this.vector3s[2].z = UnityEngine.Random.Range(-this.vector3s[1].z * num, this.vector3s[1].z * num);
		this.transform.localScale += this.vector3s[2];
	}

	// Token: 0x06008D79 RID: 36217 RVA: 0x002D9644 File Offset: 0x002D7844
	private void ApplyShakeRotationTargets()
	{
		this.preUpdate = this.transform.eulerAngles;
		if (this.percentage == 0f)
		{
			this.transform.Rotate(this.vector3s[1], this.space);
		}
		this.transform.eulerAngles = this.vector3s[0];
		float num = 1f - this.percentage;
		this.vector3s[2].x = UnityEngine.Random.Range(-this.vector3s[1].x * num, this.vector3s[1].x * num);
		this.vector3s[2].y = UnityEngine.Random.Range(-this.vector3s[1].y * num, this.vector3s[1].y * num);
		this.vector3s[2].z = UnityEngine.Random.Range(-this.vector3s[1].z * num, this.vector3s[1].z * num);
		this.transform.Rotate(this.vector3s[2], this.space);
		this.postUpdate = this.transform.eulerAngles;
		if (this.physics)
		{
			this.transform.eulerAngles = this.preUpdate;
			this.rigidbody.MoveRotation(Quaternion.Euler(this.postUpdate));
		}
	}

	// Token: 0x06008D7A RID: 36218 RVA: 0x002D97C4 File Offset: 0x002D79C4
	private void ApplyPunchPositionTargets()
	{
		this.preUpdate = this.transform.position;
		Vector3 eulerAngles = default(Vector3);
		if (this.tweenArguments.Contains("looktarget"))
		{
			eulerAngles = this.transform.eulerAngles;
			this.transform.eulerAngles = this.vector3s[4];
		}
		if (this.vector3s[1].x > 0f)
		{
			this.vector3s[2].x = this.punch(this.vector3s[1].x, this.percentage);
		}
		else if (this.vector3s[1].x < 0f)
		{
			this.vector3s[2].x = -this.punch(Mathf.Abs(this.vector3s[1].x), this.percentage);
		}
		if (this.vector3s[1].y > 0f)
		{
			this.vector3s[2].y = this.punch(this.vector3s[1].y, this.percentage);
		}
		else if (this.vector3s[1].y < 0f)
		{
			this.vector3s[2].y = -this.punch(Mathf.Abs(this.vector3s[1].y), this.percentage);
		}
		if (this.vector3s[1].z > 0f)
		{
			this.vector3s[2].z = this.punch(this.vector3s[1].z, this.percentage);
		}
		else if (this.vector3s[1].z < 0f)
		{
			this.vector3s[2].z = -this.punch(Mathf.Abs(this.vector3s[1].z), this.percentage);
		}
		this.transform.Translate(this.vector3s[2] - this.vector3s[3], this.space);
		this.vector3s[3] = this.vector3s[2];
		if (this.tweenArguments.Contains("looktarget"))
		{
			this.transform.eulerAngles = eulerAngles;
		}
		this.postUpdate = this.transform.position;
		if (this.physics)
		{
			this.transform.position = this.preUpdate;
			this.rigidbody.MovePosition(this.postUpdate);
		}
	}

	// Token: 0x06008D7B RID: 36219 RVA: 0x002D9A7C File Offset: 0x002D7C7C
	private void ApplyPunchRotationTargets()
	{
		this.preUpdate = this.transform.eulerAngles;
		if (this.vector3s[1].x > 0f)
		{
			this.vector3s[2].x = this.punch(this.vector3s[1].x, this.percentage);
		}
		else if (this.vector3s[1].x < 0f)
		{
			this.vector3s[2].x = -this.punch(Mathf.Abs(this.vector3s[1].x), this.percentage);
		}
		if (this.vector3s[1].y > 0f)
		{
			this.vector3s[2].y = this.punch(this.vector3s[1].y, this.percentage);
		}
		else if (this.vector3s[1].y < 0f)
		{
			this.vector3s[2].y = -this.punch(Mathf.Abs(this.vector3s[1].y), this.percentage);
		}
		if (this.vector3s[1].z > 0f)
		{
			this.vector3s[2].z = this.punch(this.vector3s[1].z, this.percentage);
		}
		else if (this.vector3s[1].z < 0f)
		{
			this.vector3s[2].z = -this.punch(Mathf.Abs(this.vector3s[1].z), this.percentage);
		}
		this.transform.Rotate(this.vector3s[2] - this.vector3s[3], this.space);
		this.vector3s[3] = this.vector3s[2];
		this.postUpdate = this.transform.eulerAngles;
		if (this.physics)
		{
			this.transform.eulerAngles = this.preUpdate;
			this.rigidbody.MoveRotation(Quaternion.Euler(this.postUpdate));
		}
	}

	// Token: 0x06008D7C RID: 36220 RVA: 0x002D9CDC File Offset: 0x002D7EDC
	private void ApplyPunchScaleTargets()
	{
		if (this.vector3s[1].x > 0f)
		{
			this.vector3s[2].x = this.punch(this.vector3s[1].x, this.percentage);
		}
		else if (this.vector3s[1].x < 0f)
		{
			this.vector3s[2].x = -this.punch(Mathf.Abs(this.vector3s[1].x), this.percentage);
		}
		if (this.vector3s[1].y > 0f)
		{
			this.vector3s[2].y = this.punch(this.vector3s[1].y, this.percentage);
		}
		else if (this.vector3s[1].y < 0f)
		{
			this.vector3s[2].y = -this.punch(Mathf.Abs(this.vector3s[1].y), this.percentage);
		}
		if (this.vector3s[1].z > 0f)
		{
			this.vector3s[2].z = this.punch(this.vector3s[1].z, this.percentage);
		}
		else if (this.vector3s[1].z < 0f)
		{
			this.vector3s[2].z = -this.punch(Mathf.Abs(this.vector3s[1].z), this.percentage);
		}
		this.transform.localScale = this.vector3s[0] + this.vector3s[2];
	}

	// Token: 0x06008D7D RID: 36221 RVA: 0x002D9ECD File Offset: 0x002D80CD
	private void ResetDelay()
	{
		this.delayStarted = Time.time;
		this.waitForDelay = true;
	}

	// Token: 0x06008D7E RID: 36222 RVA: 0x002D9EE4 File Offset: 0x002D80E4
	private void TweenStart()
	{
		if (this.tweenArguments == null)
		{
			return;
		}
		this.CallBack(iTween.CallbackType.OnStart);
		if (!this.loop)
		{
			this.ConflictCheck();
			this.GenerateTargets();
		}
		this.loop = true;
		if (this.type == "stab")
		{
			this.audioSource.PlayOneShot(this.audioSource.clip);
		}
		if (this.type == "move" || this.type == "scale" || this.type == "rotate" || this.type == "punch" || this.type == "shake" || this.type == "curve" || this.type == "look")
		{
			this.EnableKinematic();
		}
		this.isRunning = true;
	}

	// Token: 0x06008D7F RID: 36223 RVA: 0x002D9FCF File Offset: 0x002D81CF
	private void TweenRestart()
	{
		this.ResetDelay();
		this.loop = true;
	}

	// Token: 0x06008D80 RID: 36224 RVA: 0x002D9FDE File Offset: 0x002D81DE
	private void TweenUpdate()
	{
		if (this.type != "timer")
		{
			if (this.apply == null)
			{
				return;
			}
			this.apply();
		}
		this.CallBack(iTween.CallbackType.OnUpdate);
		this.UpdatePercentage();
	}

	// Token: 0x06008D81 RID: 36225 RVA: 0x002DA014 File Offset: 0x002D8214
	private void TweenComplete()
	{
		this.isRunning = false;
		if (this.percentage > 0.5f)
		{
			this.percentage = 1f;
		}
		else
		{
			this.percentage = 0f;
		}
		if (this.type != "timer")
		{
			this.apply();
		}
		if (this.type == "value" || this.type == "timer")
		{
			this.CallBack(iTween.CallbackType.OnUpdate);
		}
		if (this.loopType == iTween.LoopType.none)
		{
			this.Dispose();
		}
		else
		{
			this.TweenLoop();
		}
		this.CallBack(iTween.CallbackType.OnComplete);
	}

	// Token: 0x06008D82 RID: 36226 RVA: 0x002DA0B4 File Offset: 0x002D82B4
	private void TweenLoop()
	{
		this.DisableKinematic();
		iTween.LoopType loopType = this.loopType;
		if (loopType == iTween.LoopType.loop)
		{
			this.percentage = 0f;
			this.runningTime = 0f;
			if (this.apply != null)
			{
				this.apply();
			}
			this.TweenRestart();
			return;
		}
		if (loopType != iTween.LoopType.pingPong)
		{
			return;
		}
		this.reverse = !this.reverse;
		this.runningTime = 0f;
		this.TweenRestart();
	}

	// Token: 0x06008D83 RID: 36227 RVA: 0x002DA128 File Offset: 0x002D8328
	public static Rect RectUpdate(Rect currentValue, Rect targetValue, float speed)
	{
		return new Rect(iTween.FloatUpdate(currentValue.x, targetValue.x, speed), iTween.FloatUpdate(currentValue.y, targetValue.y, speed), iTween.FloatUpdate(currentValue.width, targetValue.width, speed), iTween.FloatUpdate(currentValue.height, targetValue.height, speed));
	}

	// Token: 0x06008D84 RID: 36228 RVA: 0x002DA18C File Offset: 0x002D838C
	public static Vector3 Vector3Update(Vector3 currentValue, Vector3 targetValue, float speed)
	{
		Vector3 a = targetValue - currentValue;
		currentValue += a * speed * Time.deltaTime;
		return currentValue;
	}

	// Token: 0x06008D85 RID: 36229 RVA: 0x002DA1BC File Offset: 0x002D83BC
	public static Vector2 Vector2Update(Vector2 currentValue, Vector2 targetValue, float speed)
	{
		Vector2 a = targetValue - currentValue;
		currentValue += a * speed * Time.deltaTime;
		return currentValue;
	}

	// Token: 0x06008D86 RID: 36230 RVA: 0x002DA1EC File Offset: 0x002D83EC
	public static float FloatUpdate(float currentValue, float targetValue, float speed)
	{
		float num = targetValue - currentValue;
		currentValue += num * speed * Time.deltaTime;
		return currentValue;
	}

	// Token: 0x06008D87 RID: 36231 RVA: 0x002DA20B File Offset: 0x002D840B
	public static void FadeUpdate(GameObject target, Hashtable args)
	{
		args["a"] = args["alpha"];
		iTween.ColorUpdate(target, args);
	}

	// Token: 0x06008D88 RID: 36232 RVA: 0x002DA22A File Offset: 0x002D842A
	public static void FadeUpdate(GameObject target, float alpha, float time)
	{
		iTween.FadeUpdate(target, iTween.Hash(new object[]
		{
			"alpha",
			alpha,
			"time",
			time
		}));
	}

	// Token: 0x06008D89 RID: 36233 RVA: 0x002DA260 File Offset: 0x002D8460
	public static void ColorUpdate(GameObject target, Hashtable args)
	{
		args = iTween.CleanArgs(args);
		Color[] array = new Color[4];
		if (!args.Contains("includechildren") || (bool)args["includechildren"])
		{
			foreach (object obj in target.transform)
			{
				iTween.ColorUpdate(((Transform)obj).gameObject, args);
			}
		}
		float num;
		if (args.Contains("time"))
		{
			num = (float)args["time"];
			num *= iTween.Defaults.updateTimePercentage;
		}
		else
		{
			num = iTween.Defaults.updateTime;
		}
		array[0] = (array[1] = iTween.GetTargetColor(target));
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
		if (component)
		{
			component.GetMaterial().color = array[3];
			return;
		}
		if (target.GetComponent<Light>())
		{
			target.GetComponent<Light>().color = array[3];
			return;
		}
		if (target.GetComponent<UberText>())
		{
			UberText component2 = target.GetComponent<UberText>();
			component2.TextAlpha = array[3].a;
			component2.OutlineAlpha = array[3].a;
			component2.ShadowAlpha = array[3].a;
		}
	}

	// Token: 0x06008D8A RID: 36234 RVA: 0x002DA564 File Offset: 0x002D8764
	public static void ColorUpdate(GameObject target, Color color, float time)
	{
		iTween.ColorUpdate(target, iTween.Hash(new object[]
		{
			"color",
			color,
			"time",
			time
		}));
	}

	// Token: 0x06008D8B RID: 36235 RVA: 0x002DA59C File Offset: 0x002D879C
	public static void AudioUpdate(GameObject target, Hashtable args)
	{
		args = iTween.CleanArgs(args);
		Vector2[] array = new Vector2[4];
		float num;
		if (args.Contains("time"))
		{
			num = (float)args["time"];
			num *= iTween.Defaults.updateTimePercentage;
		}
		else
		{
			num = iTween.Defaults.updateTime;
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

	// Token: 0x06008D8C RID: 36236 RVA: 0x002DA738 File Offset: 0x002D8938
	public static void AudioUpdate(GameObject target, float volume, float pitch, float time)
	{
		iTween.AudioUpdate(target, iTween.Hash(new object[]
		{
			"volume",
			volume,
			"pitch",
			pitch,
			"time",
			time
		}));
	}

	// Token: 0x06008D8D RID: 36237 RVA: 0x002DA78C File Offset: 0x002D898C
	public static void RotateUpdate(GameObject target, Hashtable args)
	{
		args = iTween.CleanArgs(args);
		Vector3[] array = new Vector3[4];
		Vector3 eulerAngles = target.transform.eulerAngles;
		float num;
		if (args.Contains("time"))
		{
			num = (float)args["time"];
			num *= iTween.Defaults.updateTimePercentage;
		}
		else
		{
			num = iTween.Defaults.updateTime;
		}
		bool flag;
		if (args.Contains("islocal"))
		{
			flag = (bool)args["islocal"];
		}
		else
		{
			flag = iTween.Defaults.isLocal;
		}
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

	// Token: 0x06008D8E RID: 36238 RVA: 0x002DA9BD File Offset: 0x002D8BBD
	public static void RotateUpdate(GameObject target, Vector3 rotation, float time)
	{
		iTween.RotateUpdate(target, iTween.Hash(new object[]
		{
			"rotation",
			rotation,
			"time",
			time
		}));
	}

	// Token: 0x06008D8F RID: 36239 RVA: 0x002DA9F4 File Offset: 0x002D8BF4
	public static void ScaleUpdate(GameObject target, Hashtable args)
	{
		args = iTween.CleanArgs(args);
		Vector3[] array = new Vector3[4];
		float num;
		if (args.Contains("time"))
		{
			num = (float)args["time"];
			num *= iTween.Defaults.updateTimePercentage;
		}
		else
		{
			num = iTween.Defaults.updateTime;
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

	// Token: 0x06008D90 RID: 36240 RVA: 0x002DAC1A File Offset: 0x002D8E1A
	public static void ScaleUpdate(GameObject target, Vector3 scale, float time)
	{
		iTween.ScaleUpdate(target, iTween.Hash(new object[]
		{
			"scale",
			scale,
			"time",
			time
		}));
	}

	// Token: 0x06008D91 RID: 36241 RVA: 0x002DAC50 File Offset: 0x002D8E50
	public static void MoveUpdate(GameObject target, Hashtable args)
	{
		args = iTween.CleanArgs(args);
		Vector3[] array = new Vector3[4];
		Vector3 position = target.transform.position;
		float num;
		if (args.Contains("time"))
		{
			num = (float)args["time"];
			num *= iTween.Defaults.updateTimePercentage;
		}
		else
		{
			num = iTween.Defaults.updateTime;
		}
		bool flag;
		if (args.Contains("islocal"))
		{
			flag = (bool)args["islocal"];
		}
		else
		{
			flag = iTween.Defaults.isLocal;
		}
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
			iTween.LookUpdate(target, args);
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

	// Token: 0x06008D92 RID: 36242 RVA: 0x002DAF64 File Offset: 0x002D9164
	public static void MoveUpdate(GameObject target, Vector3 position, float time)
	{
		iTween.MoveUpdate(target, iTween.Hash(new object[]
		{
			"position",
			position,
			"time",
			time
		}));
	}

	// Token: 0x06008D93 RID: 36243 RVA: 0x002DAF9C File Offset: 0x002D919C
	public static void LookUpdate(GameObject target, Hashtable args)
	{
		args = iTween.CleanArgs(args);
		Vector3[] array = new Vector3[5];
		float num;
		if (args.Contains("looktime"))
		{
			num = (float)args["looktime"];
			num *= iTween.Defaults.updateTimePercentage;
		}
		else if (args.Contains("time"))
		{
			num = (float)args["time"] * 0.15f;
			num *= iTween.Defaults.updateTimePercentage;
		}
		else
		{
			num = iTween.Defaults.updateTime;
		}
		array[0] = target.transform.eulerAngles;
		if (args.Contains("looktarget"))
		{
			if (args["looktarget"].GetType() == typeof(Transform))
			{
				target.transform.LookAt((Transform)args["looktarget"], ((Vector3?)args["up"]) ?? iTween.Defaults.up);
			}
			else if (args["looktarget"].GetType() == typeof(Vector3))
			{
				target.transform.LookAt((Vector3)args["looktarget"], ((Vector3?)args["up"]) ?? iTween.Defaults.up);
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
				string a = (string)args["axis"];
				if (!(a == "x"))
				{
					if (!(a == "y"))
					{
						if (a == "z")
						{
							array[4].x = array[0].x;
							array[4].y = array[0].y;
						}
					}
					else
					{
						array[4].x = array[0].x;
						array[4].z = array[0].z;
					}
				}
				else
				{
					array[4].y = array[0].y;
					array[4].z = array[0].z;
				}
				target.transform.eulerAngles = array[4];
			}
			return;
		}
		Debug.LogError("iTween Error: LookUpdate needs a 'looktarget' property!");
	}

	// Token: 0x06008D94 RID: 36244 RVA: 0x002DB2EA File Offset: 0x002D94EA
	public static void LookUpdate(GameObject target, Vector3 looktarget, float time)
	{
		iTween.LookUpdate(target, iTween.Hash(new object[]
		{
			"looktarget",
			looktarget,
			"time",
			time
		}));
	}

	// Token: 0x06008D95 RID: 36245 RVA: 0x002DB320 File Offset: 0x002D9520
	public static float PathLength(Transform[] path)
	{
		Vector3[] array = new Vector3[path.Length];
		float num = 0f;
		for (int i = 0; i < path.Length; i++)
		{
			array[i] = path[i].position;
		}
		Vector3[] pts = iTween.PathControlPointGenerator(array);
		Vector3 a = iTween.Interp(pts, 0f);
		int num2 = path.Length * 20;
		for (int j = 1; j <= num2; j++)
		{
			float t = (float)j / (float)num2;
			Vector3 vector = iTween.Interp(pts, t);
			num += Vector3.Distance(a, vector);
			a = vector;
		}
		return num;
	}

	// Token: 0x06008D96 RID: 36246 RVA: 0x002DB3B0 File Offset: 0x002D95B0
	public static float PathLength(Vector3[] path)
	{
		float num = 0f;
		Vector3[] pts = iTween.PathControlPointGenerator(path);
		Vector3 a = iTween.Interp(pts, 0f);
		int num2 = path.Length * 20;
		for (int i = 1; i <= num2; i++)
		{
			float t = (float)i / (float)num2;
			Vector3 vector = iTween.Interp(pts, t);
			num += Vector3.Distance(a, vector);
			a = vector;
		}
		return num;
	}

	// Token: 0x06008D97 RID: 36247 RVA: 0x002DB40E File Offset: 0x002D960E
	public static void PutOnPath(GameObject target, Vector3[] path, float percent)
	{
		target.transform.position = iTween.Interp(iTween.PathControlPointGenerator(path), percent);
	}

	// Token: 0x06008D98 RID: 36248 RVA: 0x002DB427 File Offset: 0x002D9627
	public static void PutOnPath(Transform target, Vector3[] path, float percent)
	{
		target.position = iTween.Interp(iTween.PathControlPointGenerator(path), percent);
	}

	// Token: 0x06008D99 RID: 36249 RVA: 0x002DB43C File Offset: 0x002D963C
	public static void PutOnPath(GameObject target, Transform[] path, float percent)
	{
		Vector3[] array = new Vector3[path.Length];
		for (int i = 0; i < path.Length; i++)
		{
			array[i] = path[i].position;
		}
		target.transform.position = iTween.Interp(iTween.PathControlPointGenerator(array), percent);
	}

	// Token: 0x06008D9A RID: 36250 RVA: 0x002DB488 File Offset: 0x002D9688
	public static void PutOnPath(Transform target, Transform[] path, float percent)
	{
		Vector3[] array = new Vector3[path.Length];
		for (int i = 0; i < path.Length; i++)
		{
			array[i] = path[i].position;
		}
		target.position = iTween.Interp(iTween.PathControlPointGenerator(array), percent);
	}

	// Token: 0x06008D9B RID: 36251 RVA: 0x002DB4D0 File Offset: 0x002D96D0
	public static Vector3 PointOnPath(Transform[] path, float percent)
	{
		Vector3[] array = new Vector3[path.Length];
		for (int i = 0; i < path.Length; i++)
		{
			array[i] = path[i].position;
		}
		return iTween.Interp(iTween.PathControlPointGenerator(array), percent);
	}

	// Token: 0x06008D9C RID: 36252 RVA: 0x002DB50F File Offset: 0x002D970F
	public static void DrawLine(Vector3[] line)
	{
		if (line.Length != 0)
		{
			iTween.DrawLineHelper(line, iTween.Defaults.color, "gizmos");
		}
	}

	// Token: 0x06008D9D RID: 36253 RVA: 0x002DB525 File Offset: 0x002D9725
	public static void DrawLine(Vector3[] line, Color color)
	{
		if (line.Length != 0)
		{
			iTween.DrawLineHelper(line, color, "gizmos");
		}
	}

	// Token: 0x06008D9E RID: 36254 RVA: 0x002DB538 File Offset: 0x002D9738
	public static void DrawLine(Transform[] line)
	{
		if (line.Length != 0)
		{
			Vector3[] array = new Vector3[line.Length];
			for (int i = 0; i < line.Length; i++)
			{
				array[i] = line[i].position;
			}
			iTween.DrawLineHelper(array, iTween.Defaults.color, "gizmos");
		}
	}

	// Token: 0x06008D9F RID: 36255 RVA: 0x002DB580 File Offset: 0x002D9780
	public static void DrawLine(Transform[] line, Color color)
	{
		if (line.Length != 0)
		{
			Vector3[] array = new Vector3[line.Length];
			for (int i = 0; i < line.Length; i++)
			{
				array[i] = line[i].position;
			}
			iTween.DrawLineHelper(array, color, "gizmos");
		}
	}

	// Token: 0x06008DA0 RID: 36256 RVA: 0x002DB50F File Offset: 0x002D970F
	public static void DrawLineGizmos(Vector3[] line)
	{
		if (line.Length != 0)
		{
			iTween.DrawLineHelper(line, iTween.Defaults.color, "gizmos");
		}
	}

	// Token: 0x06008DA1 RID: 36257 RVA: 0x002DB525 File Offset: 0x002D9725
	public static void DrawLineGizmos(Vector3[] line, Color color)
	{
		if (line.Length != 0)
		{
			iTween.DrawLineHelper(line, color, "gizmos");
		}
	}

	// Token: 0x06008DA2 RID: 36258 RVA: 0x002DB5C4 File Offset: 0x002D97C4
	public static void DrawLineGizmos(Transform[] line)
	{
		if (line.Length != 0)
		{
			Vector3[] array = new Vector3[line.Length];
			for (int i = 0; i < line.Length; i++)
			{
				array[i] = line[i].position;
			}
			iTween.DrawLineHelper(array, iTween.Defaults.color, "gizmos");
		}
	}

	// Token: 0x06008DA3 RID: 36259 RVA: 0x002DB60C File Offset: 0x002D980C
	public static void DrawLineGizmos(Transform[] line, Color color)
	{
		if (line.Length != 0)
		{
			Vector3[] array = new Vector3[line.Length];
			for (int i = 0; i < line.Length; i++)
			{
				array[i] = line[i].position;
			}
			iTween.DrawLineHelper(array, color, "gizmos");
		}
	}

	// Token: 0x06008DA4 RID: 36260 RVA: 0x002DB64F File Offset: 0x002D984F
	public static void DrawLineHandles(Vector3[] line)
	{
		if (line.Length != 0)
		{
			iTween.DrawLineHelper(line, iTween.Defaults.color, "handles");
		}
	}

	// Token: 0x06008DA5 RID: 36261 RVA: 0x002DB665 File Offset: 0x002D9865
	public static void DrawLineHandles(Vector3[] line, Color color)
	{
		if (line.Length != 0)
		{
			iTween.DrawLineHelper(line, color, "handles");
		}
	}

	// Token: 0x06008DA6 RID: 36262 RVA: 0x002DB678 File Offset: 0x002D9878
	public static void DrawLineHandles(Transform[] line)
	{
		if (line.Length != 0)
		{
			Vector3[] array = new Vector3[line.Length];
			for (int i = 0; i < line.Length; i++)
			{
				array[i] = line[i].position;
			}
			iTween.DrawLineHelper(array, iTween.Defaults.color, "handles");
		}
	}

	// Token: 0x06008DA7 RID: 36263 RVA: 0x002DB6C0 File Offset: 0x002D98C0
	public static void DrawLineHandles(Transform[] line, Color color)
	{
		if (line.Length != 0)
		{
			Vector3[] array = new Vector3[line.Length];
			for (int i = 0; i < line.Length; i++)
			{
				array[i] = line[i].position;
			}
			iTween.DrawLineHelper(array, color, "handles");
		}
	}

	// Token: 0x06008DA8 RID: 36264 RVA: 0x002DB703 File Offset: 0x002D9903
	public static Vector3 PointOnPath(Vector3[] path, float percent)
	{
		return iTween.Interp(iTween.PathControlPointGenerator(path), percent);
	}

	// Token: 0x06008DA9 RID: 36265 RVA: 0x002DB711 File Offset: 0x002D9911
	public static void DrawPath(Vector3[] path)
	{
		if (path.Length != 0)
		{
			iTween.DrawPathHelper(path, iTween.Defaults.color, "gizmos");
		}
	}

	// Token: 0x06008DAA RID: 36266 RVA: 0x002DB727 File Offset: 0x002D9927
	public static void DrawPath(Vector3[] path, Color color)
	{
		if (path.Length != 0)
		{
			iTween.DrawPathHelper(path, color, "gizmos");
		}
	}

	// Token: 0x06008DAB RID: 36267 RVA: 0x002DB73C File Offset: 0x002D993C
	public static void DrawPath(Transform[] path)
	{
		if (path.Length != 0)
		{
			Vector3[] array = new Vector3[path.Length];
			for (int i = 0; i < path.Length; i++)
			{
				array[i] = path[i].position;
			}
			iTween.DrawPathHelper(array, iTween.Defaults.color, "gizmos");
		}
	}

	// Token: 0x06008DAC RID: 36268 RVA: 0x002DB784 File Offset: 0x002D9984
	public static void DrawPath(Transform[] path, Color color)
	{
		if (path.Length != 0)
		{
			Vector3[] array = new Vector3[path.Length];
			for (int i = 0; i < path.Length; i++)
			{
				array[i] = path[i].position;
			}
			iTween.DrawPathHelper(array, color, "gizmos");
		}
	}

	// Token: 0x06008DAD RID: 36269 RVA: 0x002DB711 File Offset: 0x002D9911
	public static void DrawPathGizmos(Vector3[] path)
	{
		if (path.Length != 0)
		{
			iTween.DrawPathHelper(path, iTween.Defaults.color, "gizmos");
		}
	}

	// Token: 0x06008DAE RID: 36270 RVA: 0x002DB727 File Offset: 0x002D9927
	public static void DrawPathGizmos(Vector3[] path, Color color)
	{
		if (path.Length != 0)
		{
			iTween.DrawPathHelper(path, color, "gizmos");
		}
	}

	// Token: 0x06008DAF RID: 36271 RVA: 0x002DB7C8 File Offset: 0x002D99C8
	public static void DrawPathGizmos(Transform[] path)
	{
		if (path.Length != 0)
		{
			Vector3[] array = new Vector3[path.Length];
			for (int i = 0; i < path.Length; i++)
			{
				array[i] = path[i].position;
			}
			iTween.DrawPathHelper(array, iTween.Defaults.color, "gizmos");
		}
	}

	// Token: 0x06008DB0 RID: 36272 RVA: 0x002DB810 File Offset: 0x002D9A10
	public static void DrawPathGizmos(Transform[] path, Color color)
	{
		if (path.Length != 0)
		{
			Vector3[] array = new Vector3[path.Length];
			for (int i = 0; i < path.Length; i++)
			{
				array[i] = path[i].position;
			}
			iTween.DrawPathHelper(array, color, "gizmos");
		}
	}

	// Token: 0x06008DB1 RID: 36273 RVA: 0x002DB853 File Offset: 0x002D9A53
	public static void DrawPathHandles(Vector3[] path)
	{
		if (path.Length != 0)
		{
			iTween.DrawPathHelper(path, iTween.Defaults.color, "handles");
		}
	}

	// Token: 0x06008DB2 RID: 36274 RVA: 0x002DB869 File Offset: 0x002D9A69
	public static void DrawPathHandles(Vector3[] path, Color color)
	{
		if (path.Length != 0)
		{
			iTween.DrawPathHelper(path, color, "handles");
		}
	}

	// Token: 0x06008DB3 RID: 36275 RVA: 0x002DB87C File Offset: 0x002D9A7C
	public static void DrawPathHandles(Transform[] path)
	{
		if (path.Length != 0)
		{
			Vector3[] array = new Vector3[path.Length];
			for (int i = 0; i < path.Length; i++)
			{
				array[i] = path[i].position;
			}
			iTween.DrawPathHelper(array, iTween.Defaults.color, "handles");
		}
	}

	// Token: 0x06008DB4 RID: 36276 RVA: 0x002DB8C4 File Offset: 0x002D9AC4
	public static void DrawPathHandles(Transform[] path, Color color)
	{
		if (path.Length != 0)
		{
			Vector3[] array = new Vector3[path.Length];
			for (int i = 0; i < path.Length; i++)
			{
				array[i] = path[i].position;
			}
			iTween.DrawPathHelper(array, color, "handles");
		}
	}

	// Token: 0x06008DB5 RID: 36277 RVA: 0x002DB907 File Offset: 0x002D9B07
	public static void EnableTween(iTween tween)
	{
		tween.enabled = true;
	}

	// Token: 0x06008DB6 RID: 36278 RVA: 0x002DB910 File Offset: 0x002D9B10
	public static void Resume(GameObject target)
	{
		iTweenManager.ForEachByGameObject(new iTweenManager.TweenOperation(iTween.EnableTween), target);
	}

	// Token: 0x06008DB7 RID: 36279 RVA: 0x002DB924 File Offset: 0x002D9B24
	public static void Resume(GameObject target, bool includechildren)
	{
		iTweenManager.ForEach(new iTweenManager.TweenOperation(iTween.EnableTween), target, null, null, includechildren);
	}

	// Token: 0x06008DB8 RID: 36280 RVA: 0x002DB93B File Offset: 0x002D9B3B
	public static void Resume(GameObject target, string type)
	{
		iTweenManager.ForEach(new iTweenManager.TweenOperation(iTween.EnableTween), target, null, type, false);
	}

	// Token: 0x06008DB9 RID: 36281 RVA: 0x002DB952 File Offset: 0x002D9B52
	public static void Resume(GameObject target, string type, bool includechildren)
	{
		iTweenManager.ForEach(new iTweenManager.TweenOperation(iTween.EnableTween), target, null, type, includechildren);
	}

	// Token: 0x06008DBA RID: 36282 RVA: 0x002DB969 File Offset: 0x002D9B69
	public static void Resume()
	{
		iTweenManager.ForEach(new iTweenManager.TweenOperation(iTween.EnableTween), null, null, null, false);
	}

	// Token: 0x06008DBB RID: 36283 RVA: 0x002DB980 File Offset: 0x002D9B80
	public static void Resume(string type)
	{
		iTweenManager.ForEachByType(new iTweenManager.TweenOperation(iTween.EnableTween), type);
	}

	// Token: 0x06008DBC RID: 36284 RVA: 0x002DB994 File Offset: 0x002D9B94
	public static void PauseTween(iTween tween)
	{
		tween.isPaused = true;
		tween.enabled = false;
	}

	// Token: 0x06008DBD RID: 36285 RVA: 0x002DB9A4 File Offset: 0x002D9BA4
	public static void Pause(GameObject target)
	{
		iTweenManager.ForEachByGameObject(new iTweenManager.TweenOperation(iTween.PauseTween), target);
	}

	// Token: 0x06008DBE RID: 36286 RVA: 0x002DB9B8 File Offset: 0x002D9BB8
	public static void Pause(GameObject target, bool includechildren)
	{
		iTweenManager.ForEach(new iTweenManager.TweenOperation(iTween.PauseTween), target, null, null, includechildren);
	}

	// Token: 0x06008DBF RID: 36287 RVA: 0x002DB9CF File Offset: 0x002D9BCF
	public static void Pause(GameObject target, string type)
	{
		iTweenManager.ForEach(new iTweenManager.TweenOperation(iTween.PauseTween), target, null, type, false);
	}

	// Token: 0x06008DC0 RID: 36288 RVA: 0x002DB9E6 File Offset: 0x002D9BE6
	public static void Pause(GameObject target, string type, bool includechildren)
	{
		iTweenManager.ForEach(new iTweenManager.TweenOperation(iTween.PauseTween), target, null, type, includechildren);
	}

	// Token: 0x06008DC1 RID: 36289 RVA: 0x002DB9FD File Offset: 0x002D9BFD
	public static void Pause()
	{
		iTweenManager.ForEach(new iTweenManager.TweenOperation(iTween.PauseTween), null, null, null, false);
	}

	// Token: 0x06008DC2 RID: 36290 RVA: 0x002DBA14 File Offset: 0x002D9C14
	public static void Pause(string type)
	{
		iTweenManager.ForEachByType(new iTweenManager.TweenOperation(iTween.PauseTween), type);
	}

	// Token: 0x06008DC3 RID: 36291 RVA: 0x002DBA28 File Offset: 0x002D9C28
	public static int Count()
	{
		return iTweenManager.GetTweenCount();
	}

	// Token: 0x06008DC4 RID: 36292 RVA: 0x002DBA30 File Offset: 0x002D9C30
	public static int Count(string type)
	{
		int num = 0;
		iTween next;
		while ((next = iTweenManager.GetIterator().GetNext()) != null)
		{
			if ((next.type + next.method).Substring(0, type.Length).ToLower().Equals(type.ToLower()))
			{
				num++;
			}
		}
		return num;
	}

	// Token: 0x06008DC5 RID: 36293 RVA: 0x002DBA88 File Offset: 0x002D9C88
	public static int Count(GameObject target)
	{
		int num = 0;
		iTween next;
		while ((next = iTweenManager.GetIterator().GetNext()) != null)
		{
			if (next.gameObject == target)
			{
				num++;
			}
		}
		return num;
	}

	// Token: 0x06008DC6 RID: 36294 RVA: 0x002DBAC0 File Offset: 0x002D9CC0
	public static int Count(GameObject target, string type)
	{
		int num = 0;
		foreach (iTween iTween in iTweenManager.GetTweensForObject(target))
		{
			if ((iTween.type + iTween.method).Substring(0, type.Length).ToLower() == type.ToLower())
			{
				num++;
			}
		}
		return num;
	}

	// Token: 0x06008DC7 RID: 36295 RVA: 0x002DBB1C File Offset: 0x002D9D1C
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

	// Token: 0x06008DC8 RID: 36296 RVA: 0x002DBB58 File Offset: 0x002D9D58
	public static int CountOtherTypes(GameObject target, string type)
	{
		int num = 0;
		foreach (iTween iTween in iTweenManager.GetTweensForObject(target))
		{
			if ((iTween.type + iTween.method).Substring(0, type.Length).ToLower() != type.ToLower())
			{
				num++;
			}
		}
		return num;
	}

	// Token: 0x06008DC9 RID: 36297 RVA: 0x002DBBB4 File Offset: 0x002D9DB4
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

	// Token: 0x06008DCA RID: 36298 RVA: 0x002DBBED File Offset: 0x002D9DED
	public static bool HasTween(GameObject target)
	{
		return iTween.Count(target) > 0;
	}

	// Token: 0x06008DCB RID: 36299 RVA: 0x002DBBF8 File Offset: 0x002D9DF8
	public static bool HasType(GameObject target, string type)
	{
		return iTween.Count(target, type) > 0;
	}

	// Token: 0x06008DCC RID: 36300 RVA: 0x002DBC04 File Offset: 0x002D9E04
	public static bool HasName(GameObject target, string name)
	{
		return iTween.CountByName(target, name) > 0;
	}

	// Token: 0x06008DCD RID: 36301 RVA: 0x002DBC10 File Offset: 0x002D9E10
	public static bool HasOtherType(GameObject target, string type)
	{
		foreach (iTween iTween in iTweenManager.GetTweensForObject(target))
		{
			if ((iTween.type + iTween.method).Substring(0, type.Length).ToLower() != type.ToLower())
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06008DCE RID: 36302 RVA: 0x002DBC68 File Offset: 0x002D9E68
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

	// Token: 0x06008DCF RID: 36303 RVA: 0x002DBCA0 File Offset: 0x002D9EA0
	public static bool HasNameNotInList(GameObject target, params string[] names)
	{
		foreach (iTween iTween in iTweenManager.GetTweensForObject(target))
		{
			bool flag = false;
			for (int j = 0; j < names.Length; j++)
			{
				if (iTween._name == names[j])
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

	// Token: 0x06008DD0 RID: 36304 RVA: 0x002DBCF5 File Offset: 0x002D9EF5
	public static void StopTween(iTween tween)
	{
		tween.Dispose();
	}

	// Token: 0x06008DD1 RID: 36305 RVA: 0x002DBCFD File Offset: 0x002D9EFD
	public static void Stop()
	{
		iTweenManager.ForEach(new iTweenManager.TweenOperation(iTween.StopTween), null, null, null, false);
	}

	// Token: 0x06008DD2 RID: 36306 RVA: 0x002DBD14 File Offset: 0x002D9F14
	public static void Stop(string type)
	{
		iTweenManager.ForEachByType(new iTweenManager.TweenOperation(iTween.StopTween), type);
	}

	// Token: 0x06008DD3 RID: 36307 RVA: 0x002DBD28 File Offset: 0x002D9F28
	public static void StopByName(string name)
	{
		iTweenManager.ForEachByName(new iTweenManager.TweenOperation(iTween.StopTween), name);
	}

	// Token: 0x06008DD4 RID: 36308 RVA: 0x002DBD3C File Offset: 0x002D9F3C
	public static void Stop(GameObject target)
	{
		iTweenManager.ForEachByGameObject(new iTweenManager.TweenOperation(iTween.StopTween), target);
	}

	// Token: 0x06008DD5 RID: 36309 RVA: 0x002DBD50 File Offset: 0x002D9F50
	public static void Stop(GameObject target, bool includechildren)
	{
		iTweenManager.ForEach(new iTweenManager.TweenOperation(iTween.StopTween), target, null, null, includechildren);
	}

	// Token: 0x06008DD6 RID: 36310 RVA: 0x002DBD67 File Offset: 0x002D9F67
	public static void Stop(GameObject target, string type)
	{
		iTweenManager.ForEach(new iTweenManager.TweenOperation(iTween.StopTween), target, null, type, false);
	}

	// Token: 0x06008DD7 RID: 36311 RVA: 0x002DBD7E File Offset: 0x002D9F7E
	public static void StopByName(GameObject target, string name)
	{
		iTweenManager.ForEach(new iTweenManager.TweenOperation(iTween.StopTween), target, name, null, false);
	}

	// Token: 0x06008DD8 RID: 36312 RVA: 0x002DBD95 File Offset: 0x002D9F95
	public static void Stop(GameObject target, string type, bool includechildren)
	{
		iTweenManager.ForEach(new iTweenManager.TweenOperation(iTween.StopTween), target, null, type, includechildren);
	}

	// Token: 0x06008DD9 RID: 36313 RVA: 0x002DBDAC File Offset: 0x002D9FAC
	public static void StopByName(GameObject target, string name, bool includechildren)
	{
		iTweenManager.ForEach(new iTweenManager.TweenOperation(iTween.StopTween), target, name, null, includechildren);
	}

	// Token: 0x06008DDA RID: 36314 RVA: 0x002DBDC3 File Offset: 0x002D9FC3
	public static void StopOthers(GameObject target, string type, bool includechildren = false)
	{
		iTweenManager.ForEachInverted(new iTweenManager.TweenOperation(iTween.StopTween), target, null, type, includechildren);
	}

	// Token: 0x06008DDB RID: 36315 RVA: 0x002DBDDA File Offset: 0x002D9FDA
	public static void StopOthersByName(GameObject target, string name, bool includechildren = false)
	{
		iTweenManager.ForEachInverted(new iTweenManager.TweenOperation(iTween.StopTween), target, name, null, includechildren);
	}

	// Token: 0x06008DDC RID: 36316 RVA: 0x002DBDF4 File Offset: 0x002D9FF4
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

	// Token: 0x06008DDD RID: 36317 RVA: 0x002DBE3E File Offset: 0x002DA03E
	public iTween(GameObject obj, Hashtable args)
	{
		this.gameObject = obj;
		this.tweenArguments = args;
	}

	// Token: 0x06008DDE RID: 36318 RVA: 0x002DBE5B File Offset: 0x002DA05B
	public void Awake()
	{
		this.RetrieveArgs();
		this.lastRealTime = Time.realtimeSinceStartup;
		this.ResetDelay();
	}

	// Token: 0x06008DDF RID: 36319 RVA: 0x002DBE74 File Offset: 0x002DA074
	public void Update()
	{
		if (!this.activeInHierarchy)
		{
			return;
		}
		if (this.waitForDelay)
		{
			if (this.delay > 0f && this.delay > Time.time - this.delayStarted)
			{
				return;
			}
			this.TweenStart();
			this.waitForDelay = false;
		}
		if (this.isRunning && !this.physics)
		{
			if (!this.reverse)
			{
				if (this.percentage < 1f)
				{
					this.TweenUpdate();
					return;
				}
				this.TweenComplete();
				return;
			}
			else
			{
				if (this.percentage > 0f)
				{
					this.TweenUpdate();
					return;
				}
				this.TweenComplete();
			}
		}
	}

	// Token: 0x06008DE0 RID: 36320 RVA: 0x002DBF10 File Offset: 0x002DA110
	public void FixedUpdate()
	{
		if (!this.activeInHierarchy)
		{
			return;
		}
		if (this.isRunning && this.physics)
		{
			if (!this.reverse)
			{
				if (this.percentage < 1f)
				{
					this.TweenUpdate();
					return;
				}
				this.TweenComplete();
				return;
			}
			else
			{
				if (this.percentage > 0f)
				{
					this.TweenUpdate();
					return;
				}
				this.TweenComplete();
			}
		}
	}

	// Token: 0x06008DE1 RID: 36321 RVA: 0x002DBF74 File Offset: 0x002DA174
	public void LateUpdate()
	{
		if (!this.activeInHierarchy)
		{
			return;
		}
		if (this.waitForDelay)
		{
			return;
		}
		if (this.tweenArguments == null)
		{
			return;
		}
		if (this.tweenArguments.Contains("looktarget") && this.isRunning && (this.type == "move" || this.type == "shake" || this.type == "punch"))
		{
			iTween.LookUpdate(this.gameObject, this.tweenArguments);
		}
	}

	// Token: 0x06008DE2 RID: 36322 RVA: 0x002DBFFD File Offset: 0x002DA1FD
	public void OnEnable()
	{
		if (this.isRunning)
		{
			this.EnableKinematic();
		}
		if (this.isPaused)
		{
			this.isPaused = false;
			if (this.delay > 0f)
			{
				this.ResumeDelay();
			}
		}
	}

	// Token: 0x06008DE3 RID: 36323 RVA: 0x002DC02F File Offset: 0x002DA22F
	public void OnDisable()
	{
		this.DisableKinematic();
	}

	// Token: 0x06008DE4 RID: 36324 RVA: 0x002DC038 File Offset: 0x002DA238
	public void Upkeep()
	{
		if (this.destroyed)
		{
			return;
		}
		if (this.gameObject == null)
		{
			iTweenManager.Remove(this);
			return;
		}
		if (!this.gameObject.activeInHierarchy || !this.enabled)
		{
			if (this.activeLastTick)
			{
				this.OnDisable();
			}
			this.activeLastTick = false;
			return;
		}
		if (!this.activeLastTick)
		{
			this.OnEnable();
		}
		this.activeLastTick = true;
	}

	// Token: 0x06008DE5 RID: 36325 RVA: 0x002DC0A4 File Offset: 0x002DA2A4
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

	// Token: 0x06008DE6 RID: 36326 RVA: 0x002DC104 File Offset: 0x002DA304
	private static void DrawPathHelper(Vector3[] path, Color color, string method)
	{
		Vector3[] pts = iTween.PathControlPointGenerator(path);
		Vector3 to = iTween.Interp(pts, 0f);
		Gizmos.color = color;
		int num = path.Length * 20;
		for (int i = 1; i <= num; i++)
		{
			float t = (float)i / (float)num;
			Vector3 vector = iTween.Interp(pts, t);
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

	// Token: 0x06008DE7 RID: 36327 RVA: 0x002DC180 File Offset: 0x002DA380
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

	// Token: 0x06008DE8 RID: 36328 RVA: 0x002DC268 File Offset: 0x002DA468
	private static Vector3 Interp(Vector3[] pts, float t)
	{
		int num = pts.Length - 3;
		int num2 = Mathf.Min(Mathf.FloorToInt(t * (float)num), num - 1);
		float num3 = t * (float)num - (float)num2;
		Vector3 a = pts[num2];
		Vector3 a2 = pts[num2 + 1];
		Vector3 vector = pts[num2 + 2];
		Vector3 b = pts[num2 + 3];
		return 0.5f * ((-a + 3f * a2 - 3f * vector + b) * (num3 * num3 * num3) + (2f * a - 5f * a2 + 4f * vector - b) * (num3 * num3) + (-a + vector) * num3 + 2f * a2);
	}

	// Token: 0x06008DE9 RID: 36329 RVA: 0x002DC36C File Offset: 0x002DA56C
	private static void Launch(GameObject target, Hashtable args)
	{
		if (!args.Contains("id"))
		{
			args["id"] = iTween.GenerateID();
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

	// Token: 0x06008DEA RID: 36330 RVA: 0x002DC454 File Offset: 0x002DA654
	private static Hashtable CleanArgs(Hashtable args)
	{
		Hashtable hashtable = new Hashtable(args.Count);
		foreach (object obj in args)
		{
			DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
			string text = dictionaryEntry.Key.ToString().ToLower();
			object obj2 = dictionaryEntry.Value;
			if (text != iTween.KEY_ID)
			{
				Type left = obj2.GetType();
				if (left == typeof(int) || left == typeof(double))
				{
					obj2 = Convert.ToSingle(dictionaryEntry.Value);
				}
			}
			hashtable.Add(text, obj2);
		}
		return hashtable;
	}

	// Token: 0x06008DEB RID: 36331 RVA: 0x002DC528 File Offset: 0x002DA728
	private static int GenerateID()
	{
		int result = iTween.nextId;
		iTween.nextId = ((iTween.nextId == int.MaxValue) ? 1 : (iTween.nextId + 1));
		return result;
	}

	// Token: 0x06008DEC RID: 36332 RVA: 0x002DC54A File Offset: 0x002DA74A
	public Vector3 GetTargetPosition()
	{
		if (this.vector3s.Length >= 2)
		{
			return this.vector3s[1];
		}
		return new Vector3(0f, 0f, 0f);
	}

	// Token: 0x06008DED RID: 36333 RVA: 0x002DC578 File Offset: 0x002DA778
	private static Color GetTargetColor(GameObject target)
	{
		UberText component = target.GetComponent<UberText>();
		if (component)
		{
			return component.TextColor;
		}
		if (target.GetComponent<Renderer>())
		{
			Material sharedMaterial = target.GetComponent<Renderer>().GetSharedMaterial();
			if (sharedMaterial != null)
			{
				return sharedMaterial.color;
			}
		}
		else if (target.GetComponent<Light>())
		{
			return target.GetComponent<Light>().color;
		}
		return default(Color);
	}

	// Token: 0x06008DEE RID: 36334 RVA: 0x002DC5E8 File Offset: 0x002DA7E8
	private void RetrieveArgs()
	{
		if (this.tweenArguments == null)
		{
			return;
		}
		this.id = (int)this.tweenArguments["id"];
		this.type = (string)this.tweenArguments["type"];
		this._name = (string)this.tweenArguments["name"];
		this.method = (string)this.tweenArguments["method"];
		if (this.tweenArguments.Contains("time"))
		{
			this.time = (float)this.tweenArguments["time"];
		}
		else
		{
			this.time = iTween.Defaults.time;
		}
		if (this.rigidbody != null)
		{
			this.physics = true;
		}
		if (this.tweenArguments.Contains("delay"))
		{
			this.delay = (float)this.tweenArguments["delay"];
		}
		else
		{
			this.delay = iTween.Defaults.delay;
		}
		if (this.tweenArguments.Contains("namedcolorvalue"))
		{
			if (this.tweenArguments["namedcolorvalue"].GetType() == typeof(iTween.NamedValueColor))
			{
				this.namedColorValueString = ((iTween.NamedValueColor)this.tweenArguments["namedcolorvalue"]).ToString();
				goto IL_22F;
			}
			if (this.tweenArguments["namedcolorvalue"].GetType() == typeof(string))
			{
				this.namedColorValueString = (string)this.tweenArguments["namedcolorvalue"];
				goto IL_22F;
			}
			try
			{
				this.namedColorValueString = ((iTween.NamedValueColor)Enum.Parse(typeof(iTween.NamedValueColor), (string)this.tweenArguments["namedcolorvalue"], true)).ToString();
				goto IL_22F;
			}
			catch
			{
				Debug.LogWarning("iTween: Unsupported namedcolorvalue supplied! Default will be used.");
				this.namedcolorvalue = iTween.NamedValueColor._Color;
				this.namedColorValueString = this.namedcolorvalue.ToString();
				goto IL_22F;
			}
		}
		this.namedcolorvalue = iTween.Defaults.namedColorValue;
		this.namedColorValueString = "_Color";
		IL_22F:
		if (this.tweenArguments.Contains("looptype"))
		{
			if (this.tweenArguments["looptype"].GetType() == typeof(iTween.LoopType))
			{
				this.loopType = (iTween.LoopType)this.tweenArguments["looptype"];
				goto IL_2D5;
			}
			try
			{
				this.loopType = (iTween.LoopType)Enum.Parse(typeof(iTween.LoopType), (string)this.tweenArguments["looptype"], true);
				goto IL_2D5;
			}
			catch
			{
				Debug.LogWarning("iTween: Unsupported loopType supplied! Default will be used.");
				this.loopType = iTween.LoopType.none;
				goto IL_2D5;
			}
		}
		this.loopType = iTween.LoopType.none;
		IL_2D5:
		if (this.tweenArguments.Contains("easetype"))
		{
			if (this.tweenArguments["easetype"].GetType() == typeof(iTween.EaseType))
			{
				this.easeType = (iTween.EaseType)this.tweenArguments["easetype"];
				goto IL_383;
			}
			try
			{
				this.easeType = (iTween.EaseType)Enum.Parse(typeof(iTween.EaseType), (string)this.tweenArguments["easetype"], true);
				goto IL_383;
			}
			catch
			{
				Debug.LogWarning("iTween: Unsupported easeType supplied! Default will be used.");
				this.easeType = iTween.Defaults.easeType;
				goto IL_383;
			}
		}
		this.easeType = iTween.Defaults.easeType;
		IL_383:
		if (this.tweenArguments.Contains("space"))
		{
			if (this.tweenArguments["space"].GetType() == typeof(Space))
			{
				this.space = (Space)this.tweenArguments["space"];
				goto IL_431;
			}
			try
			{
				this.space = (Space)Enum.Parse(typeof(Space), (string)this.tweenArguments["space"], true);
				goto IL_431;
			}
			catch
			{
				Debug.LogWarning("iTween: Unsupported space supplied! Default will be used.");
				this.space = iTween.Defaults.space;
				goto IL_431;
			}
		}
		this.space = iTween.Defaults.space;
		IL_431:
		if (this.tweenArguments.Contains("islocal"))
		{
			this.isLocal = (bool)this.tweenArguments["islocal"];
		}
		else
		{
			this.isLocal = iTween.Defaults.isLocal;
		}
		if (this.tweenArguments.Contains("ignoretimescale"))
		{
			this.useRealTime = (bool)this.tweenArguments["ignoretimescale"];
		}
		else
		{
			this.useRealTime = iTween.Defaults.useRealTime;
		}
		this.GetEasingFunction();
	}

	// Token: 0x06008DEF RID: 36335 RVA: 0x002DCAD4 File Offset: 0x002DACD4
	private void GetEasingFunction()
	{
		switch (this.easeType)
		{
		case iTween.EaseType.easeInQuad:
			this.ease = new iTween.EasingFunction(this.easeInQuad);
			return;
		case iTween.EaseType.easeOutQuad:
			this.ease = new iTween.EasingFunction(this.easeOutQuad);
			return;
		case iTween.EaseType.easeInOutQuad:
			this.ease = new iTween.EasingFunction(this.easeInOutQuad);
			return;
		case iTween.EaseType.easeInCubic:
			this.ease = new iTween.EasingFunction(this.easeInCubic);
			return;
		case iTween.EaseType.easeOutCubic:
			this.ease = new iTween.EasingFunction(this.easeOutCubic);
			return;
		case iTween.EaseType.easeInOutCubic:
			this.ease = new iTween.EasingFunction(this.easeInOutCubic);
			return;
		case iTween.EaseType.easeInQuart:
			this.ease = new iTween.EasingFunction(this.easeInQuart);
			return;
		case iTween.EaseType.easeOutQuart:
			this.ease = new iTween.EasingFunction(this.easeOutQuart);
			return;
		case iTween.EaseType.easeInOutQuart:
			this.ease = new iTween.EasingFunction(this.easeInOutQuart);
			return;
		case iTween.EaseType.easeInQuint:
			this.ease = new iTween.EasingFunction(this.easeInQuint);
			return;
		case iTween.EaseType.easeOutQuint:
			this.ease = new iTween.EasingFunction(this.easeOutQuint);
			return;
		case iTween.EaseType.easeInOutQuint:
			this.ease = new iTween.EasingFunction(this.easeInOutQuint);
			return;
		case iTween.EaseType.easeInSine:
			this.ease = new iTween.EasingFunction(this.easeInSine);
			return;
		case iTween.EaseType.easeOutSine:
			this.ease = new iTween.EasingFunction(this.easeOutSine);
			return;
		case iTween.EaseType.easeInOutSine:
			this.ease = new iTween.EasingFunction(this.easeInOutSine);
			return;
		case iTween.EaseType.easeInExpo:
			this.ease = new iTween.EasingFunction(this.easeInExpo);
			return;
		case iTween.EaseType.easeOutExpo:
			this.ease = new iTween.EasingFunction(this.easeOutExpo);
			return;
		case iTween.EaseType.easeInOutExpo:
			this.ease = new iTween.EasingFunction(this.easeInOutExpo);
			return;
		case iTween.EaseType.easeInCirc:
			this.ease = new iTween.EasingFunction(this.easeInCirc);
			return;
		case iTween.EaseType.easeOutCirc:
			this.ease = new iTween.EasingFunction(this.easeOutCirc);
			return;
		case iTween.EaseType.easeInOutCirc:
			this.ease = new iTween.EasingFunction(this.easeInOutCirc);
			return;
		case iTween.EaseType.linear:
			this.ease = new iTween.EasingFunction(this.linear);
			return;
		case iTween.EaseType.spring:
			this.ease = new iTween.EasingFunction(this.spring);
			return;
		case iTween.EaseType.easeInBounce:
			this.ease = new iTween.EasingFunction(this.easeInBounce);
			return;
		case iTween.EaseType.easeOutBounce:
			this.ease = new iTween.EasingFunction(this.easeOutBounce);
			return;
		case iTween.EaseType.easeInOutBounce:
			this.ease = new iTween.EasingFunction(this.easeInOutBounce);
			return;
		case iTween.EaseType.easeInBack:
			this.ease = new iTween.EasingFunction(this.easeInBack);
			return;
		case iTween.EaseType.easeOutBack:
			this.ease = new iTween.EasingFunction(this.easeOutBack);
			return;
		case iTween.EaseType.easeInOutBack:
			this.ease = new iTween.EasingFunction(this.easeInOutBack);
			return;
		case iTween.EaseType.easeInElastic:
			this.ease = new iTween.EasingFunction(this.easeInElastic);
			return;
		case iTween.EaseType.easeOutElastic:
			this.ease = new iTween.EasingFunction(this.easeOutElastic);
			return;
		case iTween.EaseType.easeInOutElastic:
			this.ease = new iTween.EasingFunction(this.easeInOutElastic);
			return;
		case iTween.EaseType.punch:
			break;
		case iTween.EaseType.easeInSineOutExpo:
			this.ease = new iTween.EasingFunction(this.easeInSineOutExpo);
			return;
		case iTween.EaseType.easeOutElasticLight:
			this.ease = new iTween.EasingFunction(this.easeOutElasticLight);
			break;
		default:
			return;
		}
	}

	// Token: 0x06008DF0 RID: 36336 RVA: 0x002DCE00 File Offset: 0x002DB000
	private void UpdatePercentage()
	{
		if (this.useRealTime)
		{
			this.runningTime += Time.realtimeSinceStartup - this.lastRealTime;
		}
		else
		{
			this.runningTime += Time.deltaTime;
		}
		if (this.reverse)
		{
			this.percentage = 1f - this.runningTime / this.time;
		}
		else
		{
			this.percentage = this.runningTime / this.time;
		}
		this.lastRealTime = Time.realtimeSinceStartup;
	}

	// Token: 0x06008DF1 RID: 36337 RVA: 0x002DCE84 File Offset: 0x002DB084
	private void CallBack(iTween.CallbackType callbackType)
	{
		string text = iTween.CALLBACK_NAMES[(int)callbackType];
		string key = iTween.CALLBACK_PARAMS_NAMES[(int)callbackType];
		string key2 = iTween.CALLBACK_TARGET_NAMES[(int)callbackType];
		if (this.tweenArguments.Contains(text) && !this.tweenArguments.Contains("ischild"))
		{
			GameObject gameObject;
			if (this.tweenArguments.Contains(key2))
			{
				gameObject = (GameObject)this.tweenArguments[key2];
			}
			else
			{
				gameObject = this.gameObject;
			}
			if (gameObject == null)
			{
				Debug.LogError(string.Format("iTween Error: target is null! callbackType={0} tween={1}", text, this));
				return;
			}
			object obj = this.tweenArguments[text];
			if (obj is Action<object>)
			{
				((Action<object>)obj)(this.tweenArguments[key]);
				return;
			}
			if (obj is string)
			{
				gameObject.SendMessage((string)obj, this.tweenArguments[key], SendMessageOptions.DontRequireReceiver);
				return;
			}
			Debug.LogError("iTween Error: Callback method references must be passed as a delegate or string!");
			iTweenManager.Remove(this);
		}
	}

	// Token: 0x06008DF2 RID: 36338 RVA: 0x002DCF77 File Offset: 0x002DB177
	private void Dispose()
	{
		iTweenManager.Remove(this);
	}

	// Token: 0x06008DF3 RID: 36339 RVA: 0x002DCF80 File Offset: 0x002DB180
	private void ConflictCheck()
	{
		iTween next;
		while ((next = iTweenManager.GetIterator().GetNext()) != null)
		{
			if (!next.destroyed && !(next.gameObject != this.gameObject) && next != this)
			{
				if (next.type == "value")
				{
					return;
				}
				if (next.type == "timer")
				{
					return;
				}
				if (next.isRunning && next.type == this.type)
				{
					if (next.method != this.method)
					{
						return;
					}
					if (next.tweenArguments == null)
					{
						return;
					}
					if (next.tweenArguments.Count != this.tweenArguments.Count)
					{
						next.Dispose();
						next.CallBack(iTween.CallbackType.OnConflict);
						return;
					}
					foreach (object obj in this.tweenArguments)
					{
						DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
						if (!next.tweenArguments.Contains(dictionaryEntry.Key))
						{
							next.Dispose();
							next.CallBack(iTween.CallbackType.OnConflict);
							return;
						}
						if (!next.tweenArguments[dictionaryEntry.Key].Equals(this.tweenArguments[dictionaryEntry.Key]) && (string)dictionaryEntry.Key != "id")
						{
							next.Dispose();
							next.CallBack(iTween.CallbackType.OnConflict);
							return;
						}
					}
					this.Dispose();
					this.CallBack(iTween.CallbackType.OnConflict);
				}
			}
		}
	}

	// Token: 0x06008DF4 RID: 36340 RVA: 0x00003BE8 File Offset: 0x00001DE8
	private void EnableKinematic()
	{
	}

	// Token: 0x06008DF5 RID: 36341 RVA: 0x00003BE8 File Offset: 0x00001DE8
	private void DisableKinematic()
	{
	}

	// Token: 0x06008DF6 RID: 36342 RVA: 0x002DD12C File Offset: 0x002DB32C
	private void ResumeDelay()
	{
		this.waitForDelay = true;
	}

	// Token: 0x06008DF7 RID: 36343 RVA: 0x002DD135 File Offset: 0x002DB335
	private float linear(float start, float end, float value)
	{
		return Mathf.Lerp(start, end, value);
	}

	// Token: 0x06008DF8 RID: 36344 RVA: 0x002DD140 File Offset: 0x002DB340
	private float clerp(float start, float end, float value)
	{
		float num = 0f;
		float num2 = 360f;
		float num3 = Mathf.Abs((num2 - num) / 2f);
		float result;
		if (end - start < -num3)
		{
			float num4 = (num2 - start + end) * value;
			result = start + num4;
		}
		else if (end - start > num3)
		{
			float num4 = -(num2 - end + start) * value;
			result = start + num4;
		}
		else
		{
			result = start + (end - start) * value;
		}
		return result;
	}

	// Token: 0x06008DF9 RID: 36345 RVA: 0x002DD1AC File Offset: 0x002DB3AC
	private float spring(float start, float end, float value)
	{
		value = Mathf.Clamp01(value);
		value = (Mathf.Sin(value * 3.1415927f * (0.2f + 2.5f * value * value * value)) * Mathf.Pow(1f - value, 2.2f) + value) * (1f + 1.2f * (1f - value));
		return start + (end - start) * value;
	}

	// Token: 0x06008DFA RID: 36346 RVA: 0x002DD210 File Offset: 0x002DB410
	private float easeInQuad(float start, float end, float value)
	{
		end -= start;
		return end * value * value + start;
	}

	// Token: 0x06008DFB RID: 36347 RVA: 0x002DD21E File Offset: 0x002DB41E
	private float easeOutQuad(float start, float end, float value)
	{
		end -= start;
		return -end * value * (value - 2f) + start;
	}

	// Token: 0x06008DFC RID: 36348 RVA: 0x002DD234 File Offset: 0x002DB434
	private float easeInOutQuad(float start, float end, float value)
	{
		value /= 0.5f;
		end -= start;
		if (value < 1f)
		{
			return end / 2f * value * value + start;
		}
		value -= 1f;
		return -end / 2f * (value * (value - 2f) - 1f) + start;
	}

	// Token: 0x06008DFD RID: 36349 RVA: 0x002DD288 File Offset: 0x002DB488
	private float easeInCubic(float start, float end, float value)
	{
		end -= start;
		return end * value * value * value + start;
	}

	// Token: 0x06008DFE RID: 36350 RVA: 0x002DD298 File Offset: 0x002DB498
	private float easeOutCubic(float start, float end, float value)
	{
		value -= 1f;
		end -= start;
		return end * (value * value * value + 1f) + start;
	}

	// Token: 0x06008DFF RID: 36351 RVA: 0x002DD2B8 File Offset: 0x002DB4B8
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

	// Token: 0x06008E00 RID: 36352 RVA: 0x002DD309 File Offset: 0x002DB509
	private float easeInQuart(float start, float end, float value)
	{
		end -= start;
		return end * value * value * value * value + start;
	}

	// Token: 0x06008E01 RID: 36353 RVA: 0x002DD31B File Offset: 0x002DB51B
	private float easeOutQuart(float start, float end, float value)
	{
		value -= 1f;
		end -= start;
		return -end * (value * value * value * value - 1f) + start;
	}

	// Token: 0x06008E02 RID: 36354 RVA: 0x002DD340 File Offset: 0x002DB540
	private float easeInOutQuart(float start, float end, float value)
	{
		value /= 0.5f;
		end -= start;
		if (value < 1f)
		{
			return end / 2f * value * value * value * value + start;
		}
		value -= 2f;
		return -end / 2f * (value * value * value * value - 2f) + start;
	}

	// Token: 0x06008E03 RID: 36355 RVA: 0x002DD396 File Offset: 0x002DB596
	private float easeInQuint(float start, float end, float value)
	{
		end -= start;
		return end * value * value * value * value * value + start;
	}

	// Token: 0x06008E04 RID: 36356 RVA: 0x002DD3AA File Offset: 0x002DB5AA
	private float easeOutQuint(float start, float end, float value)
	{
		value -= 1f;
		end -= start;
		return end * (value * value * value * value * value + 1f) + start;
	}

	// Token: 0x06008E05 RID: 36357 RVA: 0x002DD3D0 File Offset: 0x002DB5D0
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

	// Token: 0x06008E06 RID: 36358 RVA: 0x002DD429 File Offset: 0x002DB629
	private float easeInSine(float start, float end, float value)
	{
		end -= start;
		return -end * Mathf.Cos(value / 1f * 1.5707964f) + end + start;
	}

	// Token: 0x06008E07 RID: 36359 RVA: 0x002DD449 File Offset: 0x002DB649
	private float easeOutSine(float start, float end, float value)
	{
		end -= start;
		return end * Mathf.Sin(value / 1f * 1.5707964f) + start;
	}

	// Token: 0x06008E08 RID: 36360 RVA: 0x002DD466 File Offset: 0x002DB666
	private float easeInOutSine(float start, float end, float value)
	{
		end -= start;
		return -end / 2f * (Mathf.Cos(3.1415927f * value / 1f) - 1f) + start;
	}

	// Token: 0x06008E09 RID: 36361 RVA: 0x002DD490 File Offset: 0x002DB690
	private float easeInExpo(float start, float end, float value)
	{
		end -= start;
		return end * Mathf.Pow(2f, 10f * (value / 1f - 1f)) + start;
	}

	// Token: 0x06008E0A RID: 36362 RVA: 0x002DD4B8 File Offset: 0x002DB6B8
	private float easeOutExpo(float start, float end, float value)
	{
		end -= start;
		return end * (-Mathf.Pow(2f, -10f * value / 1f) + 1f) + start;
	}

	// Token: 0x06008E0B RID: 36363 RVA: 0x002DD4E4 File Offset: 0x002DB6E4
	private float easeInOutExpo(float start, float end, float value)
	{
		value /= 0.5f;
		end -= start;
		if (value < 1f)
		{
			return end / 2f * Mathf.Pow(2f, 10f * (value - 1f)) + start;
		}
		value -= 1f;
		return end / 2f * (-Mathf.Pow(2f, -10f * value) + 2f) + start;
	}

	// Token: 0x06008E0C RID: 36364 RVA: 0x002DD554 File Offset: 0x002DB754
	private float easeInCirc(float start, float end, float value)
	{
		end -= start;
		return -end * (Mathf.Sqrt(1f - value * value) - 1f) + start;
	}

	// Token: 0x06008E0D RID: 36365 RVA: 0x002DD574 File Offset: 0x002DB774
	private float easeOutCirc(float start, float end, float value)
	{
		value -= 1f;
		end -= start;
		return end * Mathf.Sqrt(1f - value * value) + start;
	}

	// Token: 0x06008E0E RID: 36366 RVA: 0x002DD598 File Offset: 0x002DB798
	private float easeInOutCirc(float start, float end, float value)
	{
		value /= 0.5f;
		end -= start;
		if (value < 1f)
		{
			return -end / 2f * (Mathf.Sqrt(1f - value * value) - 1f) + start;
		}
		value -= 2f;
		return end / 2f * (Mathf.Sqrt(1f - value * value) + 1f) + start;
	}

	// Token: 0x06008E0F RID: 36367 RVA: 0x002DD604 File Offset: 0x002DB804
	private float easeInBounce(float start, float end, float value)
	{
		end -= start;
		float num = 1f;
		return end - this.easeOutBounce(0f, end, num - value) + start;
	}

	// Token: 0x06008E10 RID: 36368 RVA: 0x002DD630 File Offset: 0x002DB830
	private float easeOutBounce(float start, float end, float value)
	{
		value /= 1f;
		end -= start;
		if (value < 0.36363637f)
		{
			return end * (7.5625f * value * value) + start;
		}
		if (value < 0.72727275f)
		{
			value -= 0.54545456f;
			return end * (7.5625f * value * value + 0.75f) + start;
		}
		if ((double)value < 0.9090909090909091)
		{
			value -= 0.8181818f;
			return end * (7.5625f * value * value + 0.9375f) + start;
		}
		value -= 0.95454544f;
		return end * (7.5625f * value * value + 0.984375f) + start;
	}

	// Token: 0x06008E11 RID: 36369 RVA: 0x002DD6CC File Offset: 0x002DB8CC
	private float easeInOutBounce(float start, float end, float value)
	{
		end -= start;
		float num = 1f;
		if (value < num / 2f)
		{
			return this.easeInBounce(0f, end, value * 2f) * 0.5f + start;
		}
		return this.easeOutBounce(0f, end, value * 2f - num) * 0.5f + end * 0.5f + start;
	}

	// Token: 0x06008E12 RID: 36370 RVA: 0x002DD730 File Offset: 0x002DB930
	private float easeInBack(float start, float end, float value)
	{
		end -= start;
		value /= 1f;
		float num = 1.70158f;
		return end * value * value * ((num + 1f) * value - num) + start;
	}

	// Token: 0x06008E13 RID: 36371 RVA: 0x002DD764 File Offset: 0x002DB964
	private float easeOutBack(float start, float end, float value)
	{
		float num = 1.70158f;
		end -= start;
		value = value / 1f - 1f;
		return end * (value * value * ((num + 1f) * value + num) + 1f) + start;
	}

	// Token: 0x06008E14 RID: 36372 RVA: 0x002DD7A4 File Offset: 0x002DB9A4
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

	// Token: 0x06008E15 RID: 36373 RVA: 0x002DD820 File Offset: 0x002DBA20
	private float punch(float amplitude, float value)
	{
		if (value == 0f)
		{
			return 0f;
		}
		if (value == 1f)
		{
			return 0f;
		}
		float num = 0.3f;
		float num2 = num / 6.2831855f * Mathf.Asin(0f);
		return amplitude * Mathf.Pow(2f, -10f * value) * Mathf.Sin((value * 1f - num2) * 6.2831855f / num);
	}

	// Token: 0x06008E16 RID: 36374 RVA: 0x002DD894 File Offset: 0x002DBA94
	private float easeInElastic(float start, float end, float value)
	{
		end -= start;
		float num = 1f;
		float num2 = num * 0.3f;
		float num3 = 0f;
		if (value == 0f)
		{
			return start;
		}
		if ((value /= num) == 1f)
		{
			return start + end;
		}
		float num4;
		if (num3 == 0f || num3 < Mathf.Abs(end))
		{
			num3 = end;
			num4 = num2 / 4f;
		}
		else
		{
			num4 = num2 / 6.2831855f * Mathf.Asin(end / num3);
		}
		return -(num3 * Mathf.Pow(2f, 10f * (value -= 1f)) * Mathf.Sin((value * num - num4) * 6.2831855f / num2)) + start;
	}

	// Token: 0x06008E17 RID: 36375 RVA: 0x002DD93C File Offset: 0x002DBB3C
	private float easeOutElastic(float start, float end, float value)
	{
		end -= start;
		float num = 1f;
		float num2 = num * 0.3f;
		float num3 = 0f;
		if (value == 0f)
		{
			return start;
		}
		if ((value /= num) == 1f)
		{
			return start + end;
		}
		float num4;
		if (num3 == 0f || num3 < Mathf.Abs(end))
		{
			num3 = end;
			num4 = num2 / 4f;
		}
		else
		{
			num4 = num2 / 6.2831855f * Mathf.Asin(end / num3);
		}
		return num3 * Mathf.Pow(2f, -10f * value) * Mathf.Sin((value * num - num4) * 6.2831855f / num2) + end + start;
	}

	// Token: 0x06008E18 RID: 36376 RVA: 0x002DD9DC File Offset: 0x002DBBDC
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
		return end * Mathf.Pow(2f, -10f * value) * Mathf.Sin((value - num2) * 6.2831855f / num) + end + start;
	}

	// Token: 0x06008E19 RID: 36377 RVA: 0x002DDA38 File Offset: 0x002DBC38
	private float easeInOutElastic(float start, float end, float value)
	{
		end -= start;
		float num = 1f;
		float num2 = num * 0.3f;
		float num3 = 0f;
		if (value == 0f)
		{
			return start;
		}
		if ((value /= num / 2f) == 2f)
		{
			return start + end;
		}
		float num4;
		if (num3 == 0f || num3 < Mathf.Abs(end))
		{
			num3 = end;
			num4 = num2 / 4f;
		}
		else
		{
			num4 = num2 / 6.2831855f * Mathf.Asin(end / num3);
		}
		if (value < 1f)
		{
			return -0.5f * (num3 * Mathf.Pow(2f, 10f * (value -= 1f)) * Mathf.Sin((value * num - num4) * 6.2831855f / num2)) + start;
		}
		return num3 * Mathf.Pow(2f, -10f * (value -= 1f)) * Mathf.Sin((value * num - num4) * 6.2831855f / num2) * 0.5f + end + start;
	}

	// Token: 0x06008E1A RID: 36378 RVA: 0x002DDB2C File Offset: 0x002DBD2C
	private float easeInSineOutExpo(float start, float end, float value)
	{
		if (value > start / 2f)
		{
			return this.easeOutExpo(start, end, value);
		}
		return this.easeInSine(start, end, value);
	}

	// Token: 0x06008E1B RID: 36379 RVA: 0x002DD490 File Offset: 0x002DB690
	private float easeInExpoFirstHalf(float start, float end, float value)
	{
		end -= start;
		return end * Mathf.Pow(2f, 10f * (value / 1f - 1f)) + start;
	}

	// Token: 0x06008E1C RID: 36380 RVA: 0x002DD490 File Offset: 0x002DB690
	private float easeInExpoSecondHalf(float start, float end, float value)
	{
		end -= start;
		return end * Mathf.Pow(2f, 10f * (value / 1f - 1f)) + start;
	}

	// Token: 0x06008E1D RID: 36381 RVA: 0x002DDB58 File Offset: 0x002DBD58
	public override string ToString()
	{
		string name = this.gameObject.name;
		if (this.tweenArguments == null)
		{
			return string.Format("[iTween - gameObject={0}", name);
		}
		object obj = this.tweenArguments["type"];
		object obj2 = this.tweenArguments["method"];
		object obj3 = this.tweenArguments["name"];
		object obj4 = this.tweenArguments["id"];
		return string.Format("[iTween - gameObject={0} type={1} method={2} name={3} id={4}]", new object[]
		{
			name,
			obj,
			obj2,
			obj3,
			obj4
		});
	}

	// Token: 0x04007605 RID: 30213
	private static int nextId = 1;

	// Token: 0x04007606 RID: 30214
	public int id;

	// Token: 0x04007607 RID: 30215
	public string type;

	// Token: 0x04007608 RID: 30216
	public string method;

	// Token: 0x04007609 RID: 30217
	public iTween.EaseType easeType;

	// Token: 0x0400760A RID: 30218
	public float time;

	// Token: 0x0400760B RID: 30219
	public float delay;

	// Token: 0x0400760C RID: 30220
	public iTween.LoopType loopType;

	// Token: 0x0400760D RID: 30221
	public bool isRunning;

	// Token: 0x0400760E RID: 30222
	public bool isPaused;

	// Token: 0x0400760F RID: 30223
	public string _name;

	// Token: 0x04007610 RID: 30224
	private bool waitForDelay;

	// Token: 0x04007611 RID: 30225
	private float runningTime;

	// Token: 0x04007612 RID: 30226
	private float percentage;

	// Token: 0x04007613 RID: 30227
	private float delayStarted;

	// Token: 0x04007614 RID: 30228
	private bool kinematic;

	// Token: 0x04007615 RID: 30229
	private bool isLocal;

	// Token: 0x04007616 RID: 30230
	private bool loop;

	// Token: 0x04007617 RID: 30231
	private bool reverse;

	// Token: 0x04007618 RID: 30232
	private bool physics;

	// Token: 0x04007619 RID: 30233
	private Hashtable tweenArguments;

	// Token: 0x0400761A RID: 30234
	private Space space;

	// Token: 0x0400761B RID: 30235
	private iTween.EasingFunction ease;

	// Token: 0x0400761C RID: 30236
	private iTween.ApplyTween apply;

	// Token: 0x0400761D RID: 30237
	private AudioSource audioSource;

	// Token: 0x0400761E RID: 30238
	private Vector3[] vector3s;

	// Token: 0x0400761F RID: 30239
	private Vector2[] vector2s;

	// Token: 0x04007620 RID: 30240
	private Color[,] colors;

	// Token: 0x04007621 RID: 30241
	private float[] floats;

	// Token: 0x04007622 RID: 30242
	private Rect[] rects;

	// Token: 0x04007623 RID: 30243
	private iTween.CRSpline path;

	// Token: 0x04007624 RID: 30244
	private Vector3 preUpdate;

	// Token: 0x04007625 RID: 30245
	private Vector3 postUpdate;

	// Token: 0x04007626 RID: 30246
	private iTween.NamedValueColor namedcolorvalue;

	// Token: 0x04007627 RID: 30247
	private string namedColorValueString;

	// Token: 0x04007628 RID: 30248
	private float lastRealTime;

	// Token: 0x04007629 RID: 30249
	private bool useRealTime;

	// Token: 0x0400762A RID: 30250
	public GameObject gameObject;

	// Token: 0x0400762B RID: 30251
	public bool enabled = true;

	// Token: 0x0400762C RID: 30252
	public bool activeLastTick;

	// Token: 0x0400762D RID: 30253
	public bool destroyed;

	// Token: 0x0400762E RID: 30254
	private static string[] CALLBACK_NAMES = new string[]
	{
		"onstart",
		"onupdate",
		"oncomplete",
		"onconflict"
	};

	// Token: 0x0400762F RID: 30255
	private static string[] CALLBACK_TARGET_NAMES = new string[]
	{
		"onstarttarget",
		"onupdatetarget",
		"oncompletetarget",
		"onconflicttarget"
	};

	// Token: 0x04007630 RID: 30256
	private static string[] CALLBACK_PARAMS_NAMES = new string[]
	{
		"onstartparams",
		"onupdateparams",
		"oncompleteparams",
		"onconflictparams"
	};

	// Token: 0x04007631 RID: 30257
	private static List<Material> m_MaterialTempList = new List<Material>();

	// Token: 0x04007632 RID: 30258
	private static readonly string KEY_ID = "id";

	// Token: 0x020026AC RID: 9900
	// (Invoke) Token: 0x0601380E RID: 79886
	private delegate float EasingFunction(float start, float end, float value);

	// Token: 0x020026AD RID: 9901
	// (Invoke) Token: 0x06013812 RID: 79890
	private delegate void ApplyTween();

	// Token: 0x020026AE RID: 9902
	public enum EaseType
	{
		// Token: 0x0400F183 RID: 61827
		easeInQuad,
		// Token: 0x0400F184 RID: 61828
		easeOutQuad,
		// Token: 0x0400F185 RID: 61829
		easeInOutQuad,
		// Token: 0x0400F186 RID: 61830
		easeInCubic,
		// Token: 0x0400F187 RID: 61831
		easeOutCubic,
		// Token: 0x0400F188 RID: 61832
		easeInOutCubic,
		// Token: 0x0400F189 RID: 61833
		easeInQuart,
		// Token: 0x0400F18A RID: 61834
		easeOutQuart,
		// Token: 0x0400F18B RID: 61835
		easeInOutQuart,
		// Token: 0x0400F18C RID: 61836
		easeInQuint,
		// Token: 0x0400F18D RID: 61837
		easeOutQuint,
		// Token: 0x0400F18E RID: 61838
		easeInOutQuint,
		// Token: 0x0400F18F RID: 61839
		easeInSine,
		// Token: 0x0400F190 RID: 61840
		easeOutSine,
		// Token: 0x0400F191 RID: 61841
		easeInOutSine,
		// Token: 0x0400F192 RID: 61842
		easeInExpo,
		// Token: 0x0400F193 RID: 61843
		easeOutExpo,
		// Token: 0x0400F194 RID: 61844
		easeInOutExpo,
		// Token: 0x0400F195 RID: 61845
		easeInCirc,
		// Token: 0x0400F196 RID: 61846
		easeOutCirc,
		// Token: 0x0400F197 RID: 61847
		easeInOutCirc,
		// Token: 0x0400F198 RID: 61848
		linear,
		// Token: 0x0400F199 RID: 61849
		spring,
		// Token: 0x0400F19A RID: 61850
		easeInBounce,
		// Token: 0x0400F19B RID: 61851
		easeOutBounce,
		// Token: 0x0400F19C RID: 61852
		easeInOutBounce,
		// Token: 0x0400F19D RID: 61853
		easeInBack,
		// Token: 0x0400F19E RID: 61854
		easeOutBack,
		// Token: 0x0400F19F RID: 61855
		easeInOutBack,
		// Token: 0x0400F1A0 RID: 61856
		easeInElastic,
		// Token: 0x0400F1A1 RID: 61857
		easeOutElastic,
		// Token: 0x0400F1A2 RID: 61858
		easeInOutElastic,
		// Token: 0x0400F1A3 RID: 61859
		punch,
		// Token: 0x0400F1A4 RID: 61860
		easeInSineOutExpo,
		// Token: 0x0400F1A5 RID: 61861
		easeOutElasticLight
	}

	// Token: 0x020026AF RID: 9903
	private enum CallbackType
	{
		// Token: 0x0400F1A7 RID: 61863
		OnStart,
		// Token: 0x0400F1A8 RID: 61864
		OnUpdate,
		// Token: 0x0400F1A9 RID: 61865
		OnComplete,
		// Token: 0x0400F1AA RID: 61866
		OnConflict
	}

	// Token: 0x020026B0 RID: 9904
	public enum LoopType
	{
		// Token: 0x0400F1AC RID: 61868
		none,
		// Token: 0x0400F1AD RID: 61869
		loop,
		// Token: 0x0400F1AE RID: 61870
		pingPong
	}

	// Token: 0x020026B1 RID: 9905
	public enum NamedValueColor
	{
		// Token: 0x0400F1B0 RID: 61872
		_Color,
		// Token: 0x0400F1B1 RID: 61873
		_SpecColor,
		// Token: 0x0400F1B2 RID: 61874
		_Emission,
		// Token: 0x0400F1B3 RID: 61875
		_ReflectColor
	}

	// Token: 0x020026B2 RID: 9906
	public static class Defaults
	{
		// Token: 0x0400F1B4 RID: 61876
		public static float time = 1f;

		// Token: 0x0400F1B5 RID: 61877
		public static float delay = 0f;

		// Token: 0x0400F1B6 RID: 61878
		public static iTween.NamedValueColor namedColorValue = iTween.NamedValueColor._Color;

		// Token: 0x0400F1B7 RID: 61879
		public static iTween.LoopType loopType = iTween.LoopType.none;

		// Token: 0x0400F1B8 RID: 61880
		public static iTween.EaseType easeType = iTween.EaseType.easeOutExpo;

		// Token: 0x0400F1B9 RID: 61881
		public static float lookSpeed = 3f;

		// Token: 0x0400F1BA RID: 61882
		public static bool isLocal = false;

		// Token: 0x0400F1BB RID: 61883
		public static Space space = Space.Self;

		// Token: 0x0400F1BC RID: 61884
		public static bool orientToPath = false;

		// Token: 0x0400F1BD RID: 61885
		public static Color color = Color.white;

		// Token: 0x0400F1BE RID: 61886
		public static float updateTimePercentage = 0.05f;

		// Token: 0x0400F1BF RID: 61887
		public static float updateTime = 1f * iTween.Defaults.updateTimePercentage;

		// Token: 0x0400F1C0 RID: 61888
		public static float lookAhead = 0.05f;

		// Token: 0x0400F1C1 RID: 61889
		public static bool useRealTime = false;

		// Token: 0x0400F1C2 RID: 61890
		public static Vector3 up = Vector3.up;
	}

	// Token: 0x020026B3 RID: 9907
	private class CRSpline
	{
		// Token: 0x06013816 RID: 79894 RVA: 0x0053645A File Offset: 0x0053465A
		public CRSpline(params Vector3[] pts)
		{
			this.pts = new Vector3[pts.Length];
			Array.Copy(pts, this.pts, pts.Length);
		}

		// Token: 0x06013817 RID: 79895 RVA: 0x00536480 File Offset: 0x00534680
		public Vector3 Interp(float t)
		{
			int num = this.pts.Length - 3;
			int num2 = Mathf.Min(Mathf.FloorToInt(t * (float)num), num - 1);
			float num3 = t * (float)num - (float)num2;
			Vector3 a = this.pts[num2];
			Vector3 a2 = this.pts[num2 + 1];
			Vector3 vector = this.pts[num2 + 2];
			Vector3 b = this.pts[num2 + 3];
			return 0.5f * ((-a + 3f * a2 - 3f * vector + b) * (num3 * num3 * num3) + (2f * a - 5f * a2 + 4f * vector - b) * (num3 * num3) + (-a + vector) * num3 + 2f * a2);
		}

		// Token: 0x0400F1C3 RID: 61891
		public Vector3[] pts;
	}
}
