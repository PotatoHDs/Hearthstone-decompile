using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using bnet.protocol;
using bnet.protocol.v2;

namespace bgs
{
	// Token: 0x0200022D RID: 557
	public static class ProtocolHelper
	{
		// Token: 0x06002342 RID: 9026 RVA: 0x0007B800 File Offset: 0x00079A00
		public static bnet.protocol.Attribute CreateAttribute(string name, long val)
		{
			bnet.protocol.Attribute attribute = new bnet.protocol.Attribute();
			bnet.protocol.Variant variant = new bnet.protocol.Variant();
			variant.SetIntValue(val);
			attribute.SetName(name);
			attribute.SetValue(variant);
			return attribute;
		}

		// Token: 0x06002343 RID: 9027 RVA: 0x0007B830 File Offset: 0x00079A30
		public static bnet.protocol.Attribute CreateAttribute(string name, bool val)
		{
			bnet.protocol.Attribute attribute = new bnet.protocol.Attribute();
			bnet.protocol.Variant variant = new bnet.protocol.Variant();
			variant.SetBoolValue(val);
			attribute.SetName(name);
			attribute.SetValue(variant);
			return attribute;
		}

		// Token: 0x06002344 RID: 9028 RVA: 0x0007B860 File Offset: 0x00079A60
		public static bnet.protocol.Attribute CreateAttribute(string name, string val)
		{
			bnet.protocol.Attribute attribute = new bnet.protocol.Attribute();
			bnet.protocol.Variant variant = new bnet.protocol.Variant();
			variant.SetStringValue(val);
			attribute.SetName(name);
			attribute.SetValue(variant);
			return attribute;
		}

		// Token: 0x06002345 RID: 9029 RVA: 0x0007B890 File Offset: 0x00079A90
		public static bnet.protocol.Attribute CreateAttribute(string name, byte[] val)
		{
			bnet.protocol.Attribute attribute = new bnet.protocol.Attribute();
			bnet.protocol.Variant variant = new bnet.protocol.Variant();
			variant.SetBlobValue(val);
			attribute.SetName(name);
			attribute.SetValue(variant);
			return attribute;
		}

		// Token: 0x06002346 RID: 9030 RVA: 0x0007B8C0 File Offset: 0x00079AC0
		public static bnet.protocol.Attribute CreateAttribute(string name, ulong val)
		{
			bnet.protocol.Attribute attribute = new bnet.protocol.Attribute();
			bnet.protocol.Variant variant = new bnet.protocol.Variant();
			variant.SetUintValue(val);
			attribute.SetName(name);
			attribute.SetValue(variant);
			return attribute;
		}

		// Token: 0x06002347 RID: 9031 RVA: 0x0007B8F0 File Offset: 0x00079AF0
		public static bnet.protocol.v2.Attribute CreateAttributeV2(string name, long val)
		{
			bnet.protocol.v2.Attribute attribute = new bnet.protocol.v2.Attribute();
			bnet.protocol.v2.Variant variant = new bnet.protocol.v2.Variant();
			variant.SetIntValue(val);
			attribute.SetName(name);
			attribute.SetValue(variant);
			return attribute;
		}

		// Token: 0x06002348 RID: 9032 RVA: 0x0007B920 File Offset: 0x00079B20
		public static bnet.protocol.v2.Attribute CreateAttributeV2(string name, bool val)
		{
			bnet.protocol.v2.Attribute attribute = new bnet.protocol.v2.Attribute();
			bnet.protocol.v2.Variant variant = new bnet.protocol.v2.Variant();
			variant.SetBoolValue(val);
			attribute.SetName(name);
			attribute.SetValue(variant);
			return attribute;
		}

		// Token: 0x06002349 RID: 9033 RVA: 0x0007B950 File Offset: 0x00079B50
		public static bnet.protocol.v2.Attribute CreateAttributeV2(string name, string val)
		{
			bnet.protocol.v2.Attribute attribute = new bnet.protocol.v2.Attribute();
			bnet.protocol.v2.Variant variant = new bnet.protocol.v2.Variant();
			variant.SetStringValue(val);
			attribute.SetName(name);
			attribute.SetValue(variant);
			return attribute;
		}

		// Token: 0x0600234A RID: 9034 RVA: 0x0007B980 File Offset: 0x00079B80
		public static bnet.protocol.v2.Attribute CreateAttributeV2(string name, byte[] val)
		{
			bnet.protocol.v2.Attribute attribute = new bnet.protocol.v2.Attribute();
			bnet.protocol.v2.Variant variant = new bnet.protocol.v2.Variant();
			variant.SetBlobValue(val);
			attribute.SetName(name);
			attribute.SetValue(variant);
			return attribute;
		}

		// Token: 0x0600234B RID: 9035 RVA: 0x0007B9B0 File Offset: 0x00079BB0
		public static bnet.protocol.v2.Attribute CreateAttributeV2(string name, ulong val)
		{
			bnet.protocol.v2.Attribute attribute = new bnet.protocol.v2.Attribute();
			bnet.protocol.v2.Variant variant = new bnet.protocol.v2.Variant();
			variant.SetUintValue(val);
			attribute.SetName(name);
			attribute.SetValue(variant);
			return attribute;
		}

		// Token: 0x0600234C RID: 9036 RVA: 0x0007B9E0 File Offset: 0x00079BE0
		public static bnet.protocol.v2.Attribute[] V1AttributeToV2Attribute(bnet.protocol.Attribute[] attributeV1)
		{
			bnet.protocol.v2.Attribute[] array = new bnet.protocol.v2.Attribute[attributeV1.Length];
			for (int i = 0; i < attributeV1.Length; i++)
			{
				array[i] = ProtocolHelper.V1AttributeToV2Attribute(attributeV1[i]);
			}
			return array;
		}

		// Token: 0x0600234D RID: 9037 RVA: 0x0007BA10 File Offset: 0x00079C10
		public static bnet.protocol.v2.Attribute V1AttributeToV2Attribute(bnet.protocol.Attribute attributeV1)
		{
			if (attributeV1 == null)
			{
				return null;
			}
			bnet.protocol.v2.Attribute attribute = new bnet.protocol.v2.Attribute();
			bnet.protocol.v2.Variant variant = new bnet.protocol.v2.Variant();
			attribute.SetName(attributeV1.Name);
			attribute.SetValue(variant);
			if (attributeV1.Value.HasBlobValue || attributeV1.Value.HasMessageValue || attributeV1.Value.HasEntityIdValue)
			{
				if (attributeV1.Value.HasBlobValue)
				{
					variant.SetBlobValue(attributeV1.Value.BlobValue);
					return attribute;
				}
				if (attributeV1.Value.HasMessageValue)
				{
					variant.SetBlobValue(attributeV1.Value.MessageValue);
					return attribute;
				}
				if (attributeV1.Value.HasEntityIdValue)
				{
					byte[] array = new byte[attributeV1.Value.EntityIdValue.GetSerializedSize()];
					MemoryStream memoryStream = new MemoryStream(array);
					attributeV1.Value.EntityIdValue.Serialize(memoryStream);
					memoryStream.Close();
					variant.SetBlobValue(array);
					return attribute;
				}
			}
			else if (attributeV1.Value.HasStringValue || attributeV1.Value.HasFourccValue)
			{
				if (attributeV1.Value.HasStringValue)
				{
					variant.SetStringValue(attributeV1.Value.StringValue);
					return attribute;
				}
				if (attributeV1.Value.HasFourccValue)
				{
					variant.SetStringValue(attributeV1.Value.FourccValue);
					return attribute;
				}
			}
			else
			{
				if (attributeV1.Value.HasIntValue)
				{
					variant.SetIntValue(attributeV1.Value.IntValue);
					return attribute;
				}
				if (attributeV1.Value.HasFloatValue)
				{
					variant.SetFloatValue(attributeV1.Value.FloatValue);
					return attribute;
				}
				if (attributeV1.Value.HasUintValue)
				{
					variant.SetUintValue(attributeV1.Value.UintValue);
					return attribute;
				}
				if (attributeV1.Value.HasBoolValue)
				{
					variant.SetBoolValue(attributeV1.Value.BoolValue);
				}
			}
			return attribute;
		}

		// Token: 0x0600234E RID: 9038 RVA: 0x0007BBCC File Offset: 0x00079DCC
		public static bnet.protocol.Attribute FindAttribute(List<bnet.protocol.Attribute> list, string attributeName, Func<bnet.protocol.Attribute, bool> condition = null)
		{
			if (list == null)
			{
				return null;
			}
			if (condition == null)
			{
				return list.FirstOrDefault((bnet.protocol.Attribute a) => a.Name == attributeName);
			}
			return list.FirstOrDefault((bnet.protocol.Attribute a) => a.Name == attributeName && condition(a));
		}

		// Token: 0x0600234F RID: 9039 RVA: 0x0007BC20 File Offset: 0x00079E20
		public static ulong GetUintAttribute(List<bnet.protocol.Attribute> list, string attributeName, ulong defaultValue, Func<bnet.protocol.Attribute, bool> condition = null)
		{
			if (list == null)
			{
				return defaultValue;
			}
			bnet.protocol.Attribute attribute;
			if (condition == null)
			{
				attribute = list.FirstOrDefault((bnet.protocol.Attribute a) => a.Name == attributeName && a.Value.HasUintValue);
			}
			else
			{
				attribute = list.FirstOrDefault((bnet.protocol.Attribute a) => a.Name == attributeName && a.Value.HasUintValue && condition(a));
			}
			if (attribute != null)
			{
				return attribute.Value.UintValue;
			}
			return defaultValue;
		}

		// Token: 0x06002350 RID: 9040 RVA: 0x0007BC86 File Offset: 0x00079E86
		public static EntityId CreateEntityId(ulong high, ulong low)
		{
			EntityId entityId = new EntityId();
			entityId.SetHigh(high);
			entityId.SetLow(low);
			return entityId;
		}

		// Token: 0x06002351 RID: 9041 RVA: 0x0007BC9C File Offset: 0x00079E9C
		public static bool IsNone(this bnet.protocol.Variant val)
		{
			return !val.HasBoolValue && !val.HasIntValue && !val.HasFloatValue && !val.HasStringValue && !val.HasBlobValue && !val.HasMessageValue && !val.HasFourccValue && !val.HasUintValue && !val.HasEntityIdValue;
		}

		// Token: 0x06002352 RID: 9042 RVA: 0x0007BCF4 File Offset: 0x00079EF4
		public static bool IsNone(this bnet.protocol.v2.Variant val)
		{
			return !val.HasBoolValue && !val.HasIntValue && !val.HasFloatValue && !val.HasStringValue && !val.HasBlobValue && !val.HasUintValue;
		}
	}
}
