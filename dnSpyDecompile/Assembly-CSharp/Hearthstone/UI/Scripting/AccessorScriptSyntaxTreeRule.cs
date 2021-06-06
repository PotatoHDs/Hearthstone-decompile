using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Hearthstone.UI.Scripting
{
	// Token: 0x0200103A RID: 4154
	public class AccessorScriptSyntaxTreeRule : ScriptSyntaxTreeRule<AccessorScriptSyntaxTreeRule>
	{
		// Token: 0x170009B4 RID: 2484
		// (get) Token: 0x0600B48D RID: 46221 RVA: 0x00377FD2 File Offset: 0x003761D2
		protected override HashSet<ScriptToken.TokenType> TokensInternal
		{
			get
			{
				return new HashSet<ScriptToken.TokenType>
				{
					ScriptToken.TokenType.Period
				};
			}
		}

		// Token: 0x170009B5 RID: 2485
		// (get) Token: 0x0600B48E RID: 46222 RVA: 0x00377FE1 File Offset: 0x003761E1
		protected override IEnumerable<ScriptSyntaxTreeRule> ExpectedRulesInternal
		{
			get
			{
				return new ScriptSyntaxTreeRule[]
				{
					ScriptSyntaxTreeRule<IdentifierScriptSyntaxTreeRule>.Get()
				};
			}
		}

		// Token: 0x0600B48F RID: 46223 RVA: 0x00377FF4 File Offset: 0x003761F4
		public override void Evaluate(ScriptSyntaxTreeNode node, ScriptContext.EvaluationContext context, out object value)
		{
			value = null;
			if (!context.CheckFeatureIsSupported(ScriptFeatureFlags.Identifiers))
			{
				return;
			}
			object obj;
			if (node.Left == null || !node.Left.Evaluate(context, out obj))
			{
				context.Results.SetFailedNodeIfNoneExists(node, node.Left);
				return;
			}
			if (ScriptSyntaxTreeRule.Utilities.IsDynamicType(node.Left.ValueType))
			{
				ScriptContext.EncodingPolicy encodingPolicy = context.EncodingPolicy;
				node.ValueType = node.Left.ValueType;
				return;
			}
			if (!AccessorScriptSyntaxTreeRule.IsTypeAssignableFrom(typeof(IDataModelProperties), node.Left.ValueType))
			{
				context.EmitError(ScriptContext.ErrorCodes.EvaluationError, "'{0}' cannot be accessed because it's not a data model.", new object[]
				{
					node.Left.Token.Value
				});
				context.Results.SetFailedNodeIfNoneExists(node, node.Left);
				return;
			}
			IDataModelProperties dataModelProperties = obj as IDataModelProperties;
			if (dataModelProperties == null)
			{
				context.EmitError(ScriptContext.ErrorCodes.EvaluationError, "Model '{0}' cannot be accessed because it's null.", new object[]
				{
					node.Left.Token.Value
				});
				context.Results.SetFailedNodeIfNoneExists(node, node.Left);
				return;
			}
			if (node.Right == null || node.Right.Rule != ScriptSyntaxTreeRule<IdentifierScriptSyntaxTreeRule>.Get())
			{
				context.EmitError(ScriptContext.ErrorCodes.EvaluationError, "Missing property.", Array.Empty<object>());
				context.Results.SetFailedNodeIfNoneExists(node, node.Right);
				return;
			}
			DataModelProperty dataModelProperty;
			bool flag;
			if (ScriptSyntaxTreeRule.Utilities.UsesNumericalEncoding(node.Right))
			{
				int id;
				if (!ScriptSyntaxTreeRule.Utilities.TryParseNumericalIdentifier(node.Right, out id))
				{
					context.EmitError(ScriptContext.ErrorCodes.EvaluationError, "Property identifier '{0}' is not valid because not numerical.", new object[]
					{
						node.Right.Token.Value
					});
					context.Results.SetFailedNodeIfNoneExists(node, node.Right);
					return;
				}
				flag = dataModelProperties.GetPropertyInfo(id, out dataModelProperty);
			}
			else
			{
				flag = ScriptSyntaxTreeRule.Utilities.GetPropertyByDisplayName(dataModelProperties, node.Right.Token.Value, out dataModelProperty);
			}
			if (!flag)
			{
				context.EmitError(ScriptContext.ErrorCodes.EvaluationError, "Property '{0}' does not exist on model '{1}'.", new object[]
				{
					node.Right.Token.Value,
					node.Left.Token.Value
				});
				context.Results.SetFailedNodeIfNoneExists(node, node.Right);
				return;
			}
			ScriptContext.EncodingPolicy encodingPolicy2 = context.EncodingPolicy;
			if (encodingPolicy2 != ScriptContext.EncodingPolicy.Numerical)
			{
			}
			if (dataModelProperty.QueryMethod != null)
			{
				node.ValueType = typeof(DataModelProperty.QueryDelegate);
				node.Target = dataModelProperties;
				value = dataModelProperty.QueryMethod;
				return;
			}
			dataModelProperties.GetPropertyValue(dataModelProperty.PropertyId, out value);
			if (context.EditMode && value == null && context.DataModelDefaultConstructor != null && AccessorScriptSyntaxTreeRule.IsTypeAssignableFrom(typeof(IDataModel), dataModelProperty.Type))
			{
				value = context.DataModelDefaultConstructor(dataModelProperty.Type);
			}
			if (!context.EditMode && value != null && ScriptSyntaxTreeRule.Utilities.IsDynamicType(dataModelProperty.Type))
			{
				node.ValueType = value.GetType();
				return;
			}
			node.ValueType = dataModelProperty.Type;
		}

		// Token: 0x0600B490 RID: 46224 RVA: 0x003782D8 File Offset: 0x003764D8
		private static bool IsTypeAssignableFrom(Type toType, Type fromType)
		{
			Dictionary<Type, bool> dictionary;
			if (!AccessorScriptSyntaxTreeRule.s_isAssignableFromCache.TryGetValue(toType, out dictionary))
			{
				dictionary = new Dictionary<Type, bool>();
				AccessorScriptSyntaxTreeRule.s_isAssignableFromCache[toType] = dictionary;
			}
			bool flag;
			if (!dictionary.TryGetValue(fromType, out flag))
			{
				flag = toType.IsAssignableFrom(fromType);
				dictionary[fromType] = flag;
			}
			return flag;
		}

		// Token: 0x0600B491 RID: 46225 RVA: 0x00378324 File Offset: 0x00376524
		[Conditional("UNITY_EDITOR")]
		private void EmitSuggestions(ScriptSyntaxTreeNode node, ScriptContext.EvaluationContext context)
		{
			if (!context.SuggestionsEnabled)
			{
				return;
			}
			IDataModelProperties dataModelProperties = (node.Left != null) ? (node.Left.Value as IDataModelProperties) : null;
			if (dataModelProperties != null)
			{
				DataModelProperty[] properties = dataModelProperties.Properties;
				for (int i = 0; i < properties.Length; i++)
				{
				}
			}
		}

		// Token: 0x040096EB RID: 38635
		private static Dictionary<Type, Dictionary<Type, bool>> s_isAssignableFromCache = new Dictionary<Type, Dictionary<Type, bool>>();
	}
}
