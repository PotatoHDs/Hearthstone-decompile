using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Hearthstone.UI.Scripting
{
	public class AccessorScriptSyntaxTreeRule : ScriptSyntaxTreeRule<AccessorScriptSyntaxTreeRule>
	{
		private static Dictionary<Type, Dictionary<Type, bool>> s_isAssignableFromCache = new Dictionary<Type, Dictionary<Type, bool>>();

		protected override HashSet<ScriptToken.TokenType> TokensInternal => new HashSet<ScriptToken.TokenType> { ScriptToken.TokenType.Period };

		protected override IEnumerable<ScriptSyntaxTreeRule> ExpectedRulesInternal => new ScriptSyntaxTreeRule[1] { ScriptSyntaxTreeRule<IdentifierScriptSyntaxTreeRule>.Get() };

		public override void Evaluate(ScriptSyntaxTreeNode node, ScriptContext.EvaluationContext context, out object value)
		{
			value = null;
			if (!context.CheckFeatureIsSupported(ScriptFeatureFlags.Identifiers))
			{
				return;
			}
			if (node.Left == null || !node.Left.Evaluate(context, out var value2))
			{
				context.Results.SetFailedNodeIfNoneExists(node, node.Left);
				return;
			}
			if (Utilities.IsDynamicType(node.Left.ValueType))
			{
				_ = context.EncodingPolicy;
				node.ValueType = node.Left.ValueType;
				return;
			}
			if (!IsTypeAssignableFrom(typeof(IDataModelProperties), node.Left.ValueType))
			{
				context.EmitError(ScriptContext.ErrorCodes.EvaluationError, "'{0}' cannot be accessed because it's not a data model.", node.Left.Token.Value);
				context.Results.SetFailedNodeIfNoneExists(node, node.Left);
				return;
			}
			IDataModelProperties dataModelProperties = value2 as IDataModelProperties;
			if (dataModelProperties == null)
			{
				context.EmitError(ScriptContext.ErrorCodes.EvaluationError, "Model '{0}' cannot be accessed because it's null.", node.Left.Token.Value);
				context.Results.SetFailedNodeIfNoneExists(node, node.Left);
				return;
			}
			if (node.Right == null || node.Right.Rule != ScriptSyntaxTreeRule<IdentifierScriptSyntaxTreeRule>.Get())
			{
				context.EmitError(ScriptContext.ErrorCodes.EvaluationError, "Missing property.");
				context.Results.SetFailedNodeIfNoneExists(node, node.Right);
				return;
			}
			bool flag;
			DataModelProperty info;
			if (Utilities.UsesNumericalEncoding(node.Right))
			{
				if (!Utilities.TryParseNumericalIdentifier(node.Right, out var id))
				{
					context.EmitError(ScriptContext.ErrorCodes.EvaluationError, "Property identifier '{0}' is not valid because not numerical.", node.Right.Token.Value);
					context.Results.SetFailedNodeIfNoneExists(node, node.Right);
					return;
				}
				flag = dataModelProperties.GetPropertyInfo(id, out info);
			}
			else
			{
				flag = Utilities.GetPropertyByDisplayName(dataModelProperties, node.Right.Token.Value, out info);
			}
			if (!flag)
			{
				context.EmitError(ScriptContext.ErrorCodes.EvaluationError, "Property '{0}' does not exist on model '{1}'.", node.Right.Token.Value, node.Left.Token.Value);
				context.Results.SetFailedNodeIfNoneExists(node, node.Right);
				return;
			}
			ScriptContext.EncodingPolicy encodingPolicy = context.EncodingPolicy;
			if (encodingPolicy != ScriptContext.EncodingPolicy.Numerical)
			{
				_ = 2;
			}
			if (info.QueryMethod != null)
			{
				node.ValueType = typeof(DataModelProperty.QueryDelegate);
				node.Target = dataModelProperties;
				value = info.QueryMethod;
				return;
			}
			dataModelProperties.GetPropertyValue(info.PropertyId, out value);
			if (context.EditMode && value == null && context.DataModelDefaultConstructor != null && IsTypeAssignableFrom(typeof(IDataModel), info.Type))
			{
				value = context.DataModelDefaultConstructor(info.Type);
			}
			if (!context.EditMode && value != null && Utilities.IsDynamicType(info.Type))
			{
				node.ValueType = value.GetType();
			}
			else
			{
				node.ValueType = info.Type;
			}
		}

		private static bool IsTypeAssignableFrom(Type toType, Type fromType)
		{
			if (!s_isAssignableFromCache.TryGetValue(toType, out var value))
			{
				value = new Dictionary<Type, bool>();
				s_isAssignableFromCache[toType] = value;
			}
			if (!value.TryGetValue(fromType, out var value2))
			{
				value2 = (value[fromType] = toType.IsAssignableFrom(fromType));
			}
			return value2;
		}

		[Conditional("UNITY_EDITOR")]
		private void EmitSuggestions(ScriptSyntaxTreeNode node, ScriptContext.EvaluationContext context)
		{
			if (!context.SuggestionsEnabled)
			{
				return;
			}
			IDataModelProperties dataModelProperties = ((node.Left != null) ? (node.Left.Value as IDataModelProperties) : null);
			if (dataModelProperties != null)
			{
				DataModelProperty[] properties = dataModelProperties.Properties;
				for (int i = 0; i < properties.Length; i++)
				{
					_ = ref properties[i];
				}
			}
		}
	}
}
