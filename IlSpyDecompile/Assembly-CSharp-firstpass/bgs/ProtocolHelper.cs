using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using bnet.protocol;
using bnet.protocol.v2;

namespace bgs
{
	public static class ProtocolHelper
	{
		public static bnet.protocol.Attribute CreateAttribute(string name, long val)
		{
			bnet.protocol.Attribute obj = new bnet.protocol.Attribute();
			bnet.protocol.Variant variant = new bnet.protocol.Variant();
			variant.SetIntValue(val);
			obj.SetName(name);
			obj.SetValue(variant);
			return obj;
		}

		public static bnet.protocol.Attribute CreateAttribute(string name, bool val)
		{
			bnet.protocol.Attribute obj = new bnet.protocol.Attribute();
			bnet.protocol.Variant variant = new bnet.protocol.Variant();
			variant.SetBoolValue(val);
			obj.SetName(name);
			obj.SetValue(variant);
			return obj;
		}

		public static bnet.protocol.Attribute CreateAttribute(string name, string val)
		{
			bnet.protocol.Attribute obj = new bnet.protocol.Attribute();
			bnet.protocol.Variant variant = new bnet.protocol.Variant();
			variant.SetStringValue(val);
			obj.SetName(name);
			obj.SetValue(variant);
			return obj;
		}

		public static bnet.protocol.Attribute CreateAttribute(string name, byte[] val)
		{
			bnet.protocol.Attribute obj = new bnet.protocol.Attribute();
			bnet.protocol.Variant variant = new bnet.protocol.Variant();
			variant.SetBlobValue(val);
			obj.SetName(name);
			obj.SetValue(variant);
			return obj;
		}

		public static bnet.protocol.Attribute CreateAttribute(string name, ulong val)
		{
			bnet.protocol.Attribute obj = new bnet.protocol.Attribute();
			bnet.protocol.Variant variant = new bnet.protocol.Variant();
			variant.SetUintValue(val);
			obj.SetName(name);
			obj.SetValue(variant);
			return obj;
		}

		public static bnet.protocol.v2.Attribute CreateAttributeV2(string name, long val)
		{
			bnet.protocol.v2.Attribute obj = new bnet.protocol.v2.Attribute();
			bnet.protocol.v2.Variant variant = new bnet.protocol.v2.Variant();
			variant.SetIntValue(val);
			obj.SetName(name);
			obj.SetValue(variant);
			return obj;
		}

		public static bnet.protocol.v2.Attribute CreateAttributeV2(string name, bool val)
		{
			bnet.protocol.v2.Attribute obj = new bnet.protocol.v2.Attribute();
			bnet.protocol.v2.Variant variant = new bnet.protocol.v2.Variant();
			variant.SetBoolValue(val);
			obj.SetName(name);
			obj.SetValue(variant);
			return obj;
		}

		public static bnet.protocol.v2.Attribute CreateAttributeV2(string name, string val)
		{
			bnet.protocol.v2.Attribute obj = new bnet.protocol.v2.Attribute();
			bnet.protocol.v2.Variant variant = new bnet.protocol.v2.Variant();
			variant.SetStringValue(val);
			obj.SetName(name);
			obj.SetValue(variant);
			return obj;
		}

		public static bnet.protocol.v2.Attribute CreateAttributeV2(string name, byte[] val)
		{
			bnet.protocol.v2.Attribute obj = new bnet.protocol.v2.Attribute();
			bnet.protocol.v2.Variant variant = new bnet.protocol.v2.Variant();
			variant.SetBlobValue(val);
			obj.SetName(name);
			obj.SetValue(variant);
			return obj;
		}

		public static bnet.protocol.v2.Attribute CreateAttributeV2(string name, ulong val)
		{
			bnet.protocol.v2.Attribute obj = new bnet.protocol.v2.Attribute();
			bnet.protocol.v2.Variant variant = new bnet.protocol.v2.Variant();
			variant.SetUintValue(val);
			obj.SetName(name);
			obj.SetValue(variant);
			return obj;
		}

		public static bnet.protocol.v2.Attribute[] V1AttributeToV2Attribute(bnet.protocol.Attribute[] attributeV1)
		{
			bnet.protocol.v2.Attribute[] array = new bnet.protocol.v2.Attribute[attributeV1.Length];
			for (int i = 0; i < attributeV1.Length; i++)
			{
				array[i] = V1AttributeToV2Attribute(attributeV1[i]);
			}
			return array;
		}

		public static bnet.protocol.v2.Attribute V1AttributeToV2Attribute(bnet.protocol.Attribute attributeV1)
		{
			if (attributeV1 == null)
			{
				return null;
			}
			bnet.protocol.v2.Attribute obj = new bnet.protocol.v2.Attribute();
			bnet.protocol.v2.Variant variant = new bnet.protocol.v2.Variant();
			obj.SetName(attributeV1.Name);
			obj.SetValue(variant);
			if (attributeV1.Value.HasBlobValue || attributeV1.Value.HasMessageValue || attributeV1.Value.HasEntityIdValue)
			{
				if (attributeV1.Value.HasBlobValue)
				{
					variant.SetBlobValue(attributeV1.Value.BlobValue);
					return obj;
				}
				if (attributeV1.Value.HasMessageValue)
				{
					variant.SetBlobValue(attributeV1.Value.MessageValue);
					return obj;
				}
				if (attributeV1.Value.HasEntityIdValue)
				{
					byte[] array = new byte[attributeV1.Value.EntityIdValue.GetSerializedSize()];
					MemoryStream memoryStream = new MemoryStream(array);
					attributeV1.Value.EntityIdValue.Serialize(memoryStream);
					memoryStream.Close();
					variant.SetBlobValue(array);
					return obj;
				}
			}
			else if (attributeV1.Value.HasStringValue || attributeV1.Value.HasFourccValue)
			{
				if (attributeV1.Value.HasStringValue)
				{
					variant.SetStringValue(attributeV1.Value.StringValue);
					return obj;
				}
				if (attributeV1.Value.HasFourccValue)
				{
					variant.SetStringValue(attributeV1.Value.FourccValue);
					return obj;
				}
			}
			else
			{
				if (attributeV1.Value.HasIntValue)
				{
					variant.SetIntValue(attributeV1.Value.IntValue);
					return obj;
				}
				if (attributeV1.Value.HasFloatValue)
				{
					variant.SetFloatValue(attributeV1.Value.FloatValue);
					return obj;
				}
				if (attributeV1.Value.HasUintValue)
				{
					variant.SetUintValue(attributeV1.Value.UintValue);
					return obj;
				}
				if (attributeV1.Value.HasBoolValue)
				{
					variant.SetBoolValue(attributeV1.Value.BoolValue);
				}
			}
			return obj;
		}

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

		public static ulong GetUintAttribute(List<bnet.protocol.Attribute> list, string attributeName, ulong defaultValue, Func<bnet.protocol.Attribute, bool> condition = null)
		{
			if (list == null)
			{
				return defaultValue;
			}
			return ((condition != null) ? list.FirstOrDefault((bnet.protocol.Attribute a) => a.Name == attributeName && a.Value.HasUintValue && condition(a)) : list.FirstOrDefault((bnet.protocol.Attribute a) => a.Name == attributeName && a.Value.HasUintValue))?.Value.UintValue ?? defaultValue;
		}

		public static EntityId CreateEntityId(ulong high, ulong low)
		{
			EntityId entityId = new EntityId();
			entityId.SetHigh(high);
			entityId.SetLow(low);
			return entityId;
		}

		public static bool IsNone(this bnet.protocol.Variant val)
		{
			if (!val.HasBoolValue && !val.HasIntValue && !val.HasFloatValue && !val.HasStringValue && !val.HasBlobValue && !val.HasMessageValue && !val.HasFourccValue && !val.HasUintValue)
			{
				return !val.HasEntityIdValue;
			}
			return false;
		}

		public static bool IsNone(this bnet.protocol.v2.Variant val)
		{
			if (!val.HasBoolValue && !val.HasIntValue && !val.HasFloatValue && !val.HasStringValue && !val.HasBlobValue)
			{
				return !val.HasUintValue;
			}
			return false;
		}
	}
}
