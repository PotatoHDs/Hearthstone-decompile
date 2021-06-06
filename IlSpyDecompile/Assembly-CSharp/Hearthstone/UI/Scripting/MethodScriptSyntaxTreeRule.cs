using System;
using System.Collections;
using System.Collections.Generic;

namespace Hearthstone.UI.Scripting
{
	public class MethodScriptSyntaxTreeRule : ScriptSyntaxTreeRule<MethodScriptSyntaxTreeRule>
	{
		public abstract class Evaluator
		{
			private string m_methodSymbolCache;

			private Type[] m_expectedArgsCache;

			public string MethodSymbol => m_methodSymbolCache ?? (m_methodSymbolCache = MethodSymbolInternal);

			public Type[] ExpectedArgs => m_expectedArgsCache ?? (m_expectedArgsCache = ExpectedArgsInternal);

			protected abstract string MethodSymbolInternal { get; }

			protected virtual Type[] ExpectedArgsInternal => new Type[0];

			public abstract void Evaluate(List<object> args, ScriptSyntaxTreeNode node, ScriptContext.EvaluationContext context, out object outValue);
		}

		public abstract class Evaluator<T> : Evaluator where T : Evaluator, new()
		{
			private static T s_instance;

			public static T Get()
			{
				return s_instance ?? (s_instance = new T());
			}
		}

		private static readonly Map<string, Evaluator> s_methodEvaluators = new Map<string, Evaluator> { 
		{
			Evaluator<StringFormatScriptSyntaxTreeRule>.Get().MethodSymbol,
			Evaluator<StringFormatScriptSyntaxTreeRule>.Get()
		} };

		protected override HashSet<ScriptToken.TokenType> TokensInternal => new HashSet<ScriptToken.TokenType> { ScriptToken.TokenType.Method };

		protected override IEnumerable<ScriptSyntaxTreeRule> ExpectedRulesInternal => new ScriptSyntaxTreeRule[1] { ScriptSyntaxTreeRule<ExpressionGroupScriptSyntaxTreeRule>.Get() };

		public static bool TryGetMethodEvaluator(string methodSymbol, out Evaluator evaluator)
		{
			evaluator = null;
			if (s_methodEvaluators.TryGetValue(methodSymbol, out var value))
			{
				evaluator = value;
				return true;
			}
			return false;
		}

		public override ParseResult Parse(ScriptToken[] tokens, ref int tokenIndex, out string parseErrorMessage, out ScriptSyntaxTreeNode node)
		{
			node = null;
			ScriptToken scriptToken = tokens[tokenIndex];
			if (TryGetMethodEvaluator(scriptToken.Value, out var _))
			{
				node = new ScriptSyntaxTreeNode(ScriptSyntaxTreeRule<MethodScriptSyntaxTreeRule>.Get());
				parseErrorMessage = null;
				return ParseResult.Success;
			}
			parseErrorMessage = "No such method \"" + scriptToken.Value + "\" exists";
			return ParseResult.Failed;
		}

		public override void Evaluate(ScriptSyntaxTreeNode node, ScriptContext.EvaluationContext context, out object outValue)
		{
			outValue = null;
			if (!context.CheckFeatureIsSupported(ScriptFeatureFlags.Methods))
			{
				return;
			}
			string value = node.Token.Value;
			if (!TryGetMethodEvaluator(value, out var evaluator))
			{
				context.EmitError(ScriptContext.ErrorCodes.EvaluationError, "No such method \"" + value + "\" exists");
				context.Results.SetFailedNodeIfNoneExists(node, node);
				return;
			}
			if (node.Right == null || node.Right.Rule != ScriptSyntaxTreeRule<ExpressionGroupScriptSyntaxTreeRule>.Get())
			{
				context.EmitError(ScriptContext.ErrorCodes.EvaluationError, "Method signature expected '()' brackets");
				context.Results.SetFailedNodeIfNoneExists(node, node);
				return;
			}
			if (!node.Right.Evaluate(context, out var value2))
			{
				context.EmitError(ScriptContext.ErrorCodes.EvaluationError, "Unexpected error evaluating method arguments!");
				context.Results.SetFailedNodeIfNoneExists(node, node.Right);
				return;
			}
			ArrayList arrayList = value2 as ArrayList;
			if (arrayList == null)
			{
				context.EmitError(ScriptContext.ErrorCodes.EvaluationError, "Unexpected error evaluating method arguments!");
				context.Results.SetFailedNodeIfNoneExists(node, node.Right);
				return;
			}
			Type[] expectedArgs = evaluator.ExpectedArgs;
			for (int i = 0; i < expectedArgs.Length; i++)
			{
				if (i >= arrayList.Count)
				{
					context.EmitError(ScriptContext.ErrorCodes.EvaluationError, $"{value} expects at least {expectedArgs.Length} arguments");
					context.Results.SetFailedNodeIfNoneExists(node, node.Right);
					return;
				}
				Type type = expectedArgs[0];
				DynamicValue dynamicValue = ((arrayList[i] is DynamicValue) ? ((DynamicValue)arrayList[i]) : default(DynamicValue));
				if (!dynamicValue.HasValidValue)
				{
					context.EmitError(ScriptContext.ErrorCodes.EvaluationError, "Missing type metadata in arguments list!");
					context.Results.SetFailedNodeIfNoneExists(node, node.Right);
					return;
				}
				if (!dynamicValue.ValueType.Equals(type) && !dynamicValue.ValueType.IsSubclassOf(type))
				{
					context.EmitError(ScriptContext.ErrorCodes.EvaluationError, $"{value} argument {i + 1} must be type {type}");
					context.Results.SetFailedNodeIfNoneExists(node, node.Right);
					return;
				}
			}
			List<object> list = new List<object>(arrayList.Count);
			foreach (DynamicValue item in arrayList)
			{
				list.Add(item.Value);
			}
			evaluator.Evaluate(list, node, context, out outValue);
		}
	}
}
