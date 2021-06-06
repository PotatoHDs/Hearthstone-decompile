using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("PlayerPrefs")]
	[Tooltip("Sets the value of the preference identified by key.")]
	public class PlayerPrefsSetInt : FsmStateAction
	{
		[CompoundArray("Count", "Key", "Value")]
		[Tooltip("Case sensitive key.")]
		public FsmString[] keys;

		public FsmInt[] values;

		public override void Reset()
		{
			keys = new FsmString[1];
			values = new FsmInt[1];
		}

		public override void OnEnter()
		{
			for (int i = 0; i < keys.Length; i++)
			{
				if (!keys[i].IsNone || !keys[i].Value.Equals(""))
				{
					PlayerPrefs.SetInt(keys[i].Value, (!values[i].IsNone) ? values[i].Value : 0);
				}
			}
			Finish();
		}
	}
}
