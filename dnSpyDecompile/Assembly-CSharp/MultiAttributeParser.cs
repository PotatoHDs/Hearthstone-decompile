using System;
using System.Collections.Generic;

// Token: 0x020008EC RID: 2284
public class MultiAttributeParser
{
	// Token: 0x06007E9D RID: 32413 RVA: 0x00290BC8 File Offset: 0x0028EDC8
	public bool load(string[] args, out string errMsg)
	{
		errMsg = null;
		this.rawDict = new Dictionary<string, string>();
		if (args.Length == 0)
		{
			errMsg = "There are too few number of arguments.";
			return false;
		}
		for (int i = 0; i < args.Length; i++)
		{
			string[] array = args[i].Split(new char[]
			{
				'='
			});
			if (array.Length <= 1)
			{
				errMsg = "Failed to parse into raw dictionary: no value provided.";
				return false;
			}
			this.rawDict.Add(array[0], array[1]);
		}
		return true;
	}

	// Token: 0x06007E9E RID: 32414 RVA: 0x00290C34 File Offset: 0x0028EE34
	public bool getIntAttribute(string key, out int? value, out string errMsg)
	{
		errMsg = null;
		value = null;
		if (this.rawDict.ContainsKey(key))
		{
			int value2;
			if (!int.TryParse(this.rawDict[key], out value2))
			{
				errMsg = string.Format("Failed to parse {0} int attribute value: The value must be a valid number.", key);
				return false;
			}
			value = new int?(value2);
		}
		return true;
	}

	// Token: 0x06007E9F RID: 32415 RVA: 0x00290C8C File Offset: 0x0028EE8C
	public bool getBoolAttribute(string key, out bool? value, out string errMsg)
	{
		errMsg = null;
		value = null;
		if (this.rawDict.ContainsKey(key))
		{
			bool value2;
			if (!bool.TryParse(this.rawDict[key], out value2))
			{
				errMsg = string.Format("Failed to parse {0} boolean attribute value: The value must be a valid boolean(true/false).", key);
				return false;
			}
			value = new bool?(value2);
		}
		return true;
	}

	// Token: 0x06007EA0 RID: 32416 RVA: 0x00290CE2 File Offset: 0x0028EEE2
	public bool getStringAttribute(string key, out string value)
	{
		value = null;
		if (this.rawDict.ContainsKey(key))
		{
			value = this.rawDict[key];
		}
		return true;
	}

	// Token: 0x0400663E RID: 26174
	private const string TOO_FEW_ARGUMENTS_ERROR_MSG = "There are too few number of arguments.";

	// Token: 0x0400663F RID: 26175
	private const string NO_VALID_VALUE_GIVEN_ERROR_MSG = "Failed to parse into raw dictionary: no value provided.";

	// Token: 0x04006640 RID: 26176
	private Dictionary<string, string> rawDict;
}
