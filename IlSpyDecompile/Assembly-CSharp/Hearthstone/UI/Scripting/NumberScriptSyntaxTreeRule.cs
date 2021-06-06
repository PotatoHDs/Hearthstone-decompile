using System;
using System.Collections.Generic;
using System.Globalization;

namespace Hearthstone.UI.Scripting
{
	public class NumberScriptSyntaxTreeRule : ScriptSyntaxTreeRule<NumberScriptSyntaxTreeRule>
	{
		private static Dictionary<Type, Dictionary<int, bool>> s_isIntDefinedInEnumCache = new Dictionary<Type, Dictionary<int, bool>>();

		private static Dictionary<Type, Dictionary<int, object>> s_intToEnumObjectCache = new Dictionary<Type, Dictionary<int, object>>();

		private static Dictionary<string, double?> s_stringsToDoubles = new Dictionary<string, double?>();

		protected override HashSet<ScriptToken.TokenType> TokensInternal => new HashSet<ScriptToken.TokenType> { ScriptToken.TokenType.Numerical };

		protected override IEnumerable<ScriptSyntaxTreeRule> ExpectedRulesInternal => new ScriptSyntaxTreeRule[5]
		{
			ScriptSyntaxTreeRule<ConditionalScriptSyntaxTreeRule>.Get(),
			ScriptSyntaxTreeRule<RelationalScriptSyntaxTreeRule>.Get(),
			ScriptSyntaxTreeRule<ArithmeticScriptSyntaxTreeRule>.Get(),
			ScriptSyntaxTreeRule<TupleScriptSyntaxTreeRule>.Get(),
			ScriptSyntaxTreeRule<EmptyScriptSyntaxTreeRule>.Get()
		};

		public override void Evaluate(ScriptSyntaxTreeNode node, ScriptContext.EvaluationContext context, out object outValue)
		{
			outValue = null;
			if (!context.CheckFeatureIsSupported(ScriptFeatureFlags.Constants))
			{
				return;
			}
			string value = node.Token.Value;
			if (!TryParseAsDouble(value, out var value2))
			{
				return;
			}
			if (node.Parent.Left != null && node.Parent.Left.ValueType != null && node.Parent.Left.ValueType.IsEnum)
			{
				Type valueType = node.Parent.Left.ValueType;
				double num = Math.Ceiling(value2) - value2;
				int intValue = (int)value2;
				object obj = IntToEnumObject(valueType, intValue);
				if (num > 0.0 || obj == null)
				{
					context.EmitError(ScriptContext.ErrorCodes.EvaluationError, "Number {0} cannot be converted to enum {1}.", value2, valueType);
					return;
				}
				node.Value = obj;
				node.ValueType = node.Parent.Left.ValueType;
				outValue = node.Value;
				ScriptContext.EncodingPolicy encodingPolicy = context.EncodingPolicy;
				if (encodingPolicy != ScriptContext.EncodingPolicy.Numerical)
				{
					_ = 2;
				}
			}
			else
			{
				node.Value = value2;
				node.ValueType = typeof(double);
				outValue = node.Value;
			}
		}

		private static bool IsIntDefinedInEnum(Type enumType, int intValue)
		{
			if (!s_isIntDefinedInEnumCache.TryGetValue(enumType, out var value))
			{
				value = new Dictionary<int, bool>();
				s_isIntDefinedInEnumCache[enumType] = value;
			}
			if (!value.TryGetValue(intValue, out var value2))
			{
				value2 = (value[intValue] = Enum.IsDefined(enumType, intValue));
			}
			return value2;
		}

		private static object IntToEnumObject(Type enumType, int intValue)
		{
			if (!IsIntDefinedInEnum(enumType, intValue))
			{
				return null;
			}
			if (!s_intToEnumObjectCache.TryGetValue(enumType, out var value))
			{
				value = new Dictionary<int, object>();
				s_intToEnumObjectCache[enumType] = value;
			}
			if (!value.TryGetValue(intValue, out var value2))
			{
				value2 = (value[intValue] = Enum.ToObject(enumType, intValue));
			}
			return value2;
		}

		private bool TryParseAsDouble(string str, out double value)
		{
			if (!s_stringsToDoubles.TryGetValue(str, out var value2))
			{
				if (double.TryParse(str, NumberStyles.Any, CultureInfo.InvariantCulture, out value))
				{
					value2 = value;
					s_stringsToDoubles[str] = value2;
				}
				else
				{
					s_stringsToDoubles[str] = null;
				}
			}
			value = value2 ?? 0.0;
			return value2.HasValue;
		}
	}
}
