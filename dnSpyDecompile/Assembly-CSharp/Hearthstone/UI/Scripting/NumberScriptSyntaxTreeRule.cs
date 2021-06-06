using System;
using System.Collections.Generic;
using System.Globalization;

namespace Hearthstone.UI.Scripting
{
	// Token: 0x02001045 RID: 4165
	public class NumberScriptSyntaxTreeRule : ScriptSyntaxTreeRule<NumberScriptSyntaxTreeRule>
	{
		// Token: 0x170009C7 RID: 2503
		// (get) Token: 0x0600B4C4 RID: 46276 RVA: 0x00379741 File Offset: 0x00377941
		protected override HashSet<ScriptToken.TokenType> TokensInternal
		{
			get
			{
				return new HashSet<ScriptToken.TokenType>
				{
					ScriptToken.TokenType.Numerical
				};
			}
		}

		// Token: 0x170009C8 RID: 2504
		// (get) Token: 0x0600B4C5 RID: 46277 RVA: 0x00379750 File Offset: 0x00377950
		protected override IEnumerable<ScriptSyntaxTreeRule> ExpectedRulesInternal
		{
			get
			{
				return new ScriptSyntaxTreeRule[]
				{
					ScriptSyntaxTreeRule<ConditionalScriptSyntaxTreeRule>.Get(),
					ScriptSyntaxTreeRule<RelationalScriptSyntaxTreeRule>.Get(),
					ScriptSyntaxTreeRule<ArithmeticScriptSyntaxTreeRule>.Get(),
					ScriptSyntaxTreeRule<TupleScriptSyntaxTreeRule>.Get(),
					ScriptSyntaxTreeRule<EmptyScriptSyntaxTreeRule>.Get()
				};
			}
		}

		// Token: 0x0600B4C6 RID: 46278 RVA: 0x00379780 File Offset: 0x00377980
		public override void Evaluate(ScriptSyntaxTreeNode node, ScriptContext.EvaluationContext context, out object outValue)
		{
			outValue = null;
			if (!context.CheckFeatureIsSupported(ScriptFeatureFlags.Constants))
			{
				return;
			}
			string value = node.Token.Value;
			double num;
			if (!this.TryParseAsDouble(value, out num))
			{
				return;
			}
			if (node.Parent.Left == null || !(node.Parent.Left.ValueType != null) || !node.Parent.Left.ValueType.IsEnum)
			{
				node.Value = num;
				node.ValueType = typeof(double);
				outValue = node.Value;
				return;
			}
			Type valueType = node.Parent.Left.ValueType;
			double num2 = Math.Ceiling(num) - num;
			int intValue = (int)num;
			object obj = NumberScriptSyntaxTreeRule.IntToEnumObject(valueType, intValue);
			if (num2 > 0.0 || obj == null)
			{
				context.EmitError(ScriptContext.ErrorCodes.EvaluationError, "Number {0} cannot be converted to enum {1}.", new object[]
				{
					num,
					valueType
				});
				return;
			}
			node.Value = obj;
			node.ValueType = node.Parent.Left.ValueType;
			outValue = node.Value;
			ScriptContext.EncodingPolicy encodingPolicy = context.EncodingPolicy;
			if (encodingPolicy != ScriptContext.EncodingPolicy.Numerical)
			{
			}
		}

		// Token: 0x0600B4C7 RID: 46279 RVA: 0x003798B0 File Offset: 0x00377AB0
		private static bool IsIntDefinedInEnum(Type enumType, int intValue)
		{
			Dictionary<int, bool> dictionary;
			if (!NumberScriptSyntaxTreeRule.s_isIntDefinedInEnumCache.TryGetValue(enumType, out dictionary))
			{
				dictionary = new Dictionary<int, bool>();
				NumberScriptSyntaxTreeRule.s_isIntDefinedInEnumCache[enumType] = dictionary;
			}
			bool flag;
			if (!dictionary.TryGetValue(intValue, out flag))
			{
				flag = Enum.IsDefined(enumType, intValue);
				dictionary[intValue] = flag;
			}
			return flag;
		}

		// Token: 0x0600B4C8 RID: 46280 RVA: 0x00379900 File Offset: 0x00377B00
		private static object IntToEnumObject(Type enumType, int intValue)
		{
			if (!NumberScriptSyntaxTreeRule.IsIntDefinedInEnum(enumType, intValue))
			{
				return null;
			}
			Dictionary<int, object> dictionary;
			if (!NumberScriptSyntaxTreeRule.s_intToEnumObjectCache.TryGetValue(enumType, out dictionary))
			{
				dictionary = new Dictionary<int, object>();
				NumberScriptSyntaxTreeRule.s_intToEnumObjectCache[enumType] = dictionary;
			}
			object obj;
			if (!dictionary.TryGetValue(intValue, out obj))
			{
				obj = Enum.ToObject(enumType, intValue);
				dictionary[intValue] = obj;
			}
			return obj;
		}

		// Token: 0x0600B4C9 RID: 46281 RVA: 0x00379958 File Offset: 0x00377B58
		private bool TryParseAsDouble(string str, out double value)
		{
			double? num;
			if (!NumberScriptSyntaxTreeRule.s_stringsToDoubles.TryGetValue(str, out num))
			{
				if (double.TryParse(str, NumberStyles.Any, CultureInfo.InvariantCulture, out value))
				{
					num = new double?(value);
					NumberScriptSyntaxTreeRule.s_stringsToDoubles[str] = num;
				}
				else
				{
					NumberScriptSyntaxTreeRule.s_stringsToDoubles[str] = null;
				}
			}
			value = (num ?? 0.0);
			return num != null;
		}

		// Token: 0x040096F2 RID: 38642
		private static Dictionary<Type, Dictionary<int, bool>> s_isIntDefinedInEnumCache = new Dictionary<Type, Dictionary<int, bool>>();

		// Token: 0x040096F3 RID: 38643
		private static Dictionary<Type, Dictionary<int, object>> s_intToEnumObjectCache = new Dictionary<Type, Dictionary<int, object>>();

		// Token: 0x040096F4 RID: 38644
		private static Dictionary<string, double?> s_stringsToDoubles = new Dictionary<string, double?>();
	}
}
