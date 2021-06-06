using System;
using System.Collections;
using System.Collections.Generic;

namespace Hearthstone.UI.Scripting
{
	// Token: 0x02001044 RID: 4164
	public class MethodScriptSyntaxTreeRule : ScriptSyntaxTreeRule<MethodScriptSyntaxTreeRule>
	{
		// Token: 0x170009C5 RID: 2501
		// (get) Token: 0x0600B4BD RID: 46269 RVA: 0x003793F4 File Offset: 0x003775F4
		protected override HashSet<ScriptToken.TokenType> TokensInternal
		{
			get
			{
				return new HashSet<ScriptToken.TokenType>
				{
					ScriptToken.TokenType.Method
				};
			}
		}

		// Token: 0x170009C6 RID: 2502
		// (get) Token: 0x0600B4BE RID: 46270 RVA: 0x00379404 File Offset: 0x00377604
		protected override IEnumerable<ScriptSyntaxTreeRule> ExpectedRulesInternal
		{
			get
			{
				return new ScriptSyntaxTreeRule[]
				{
					ScriptSyntaxTreeRule<ExpressionGroupScriptSyntaxTreeRule>.Get()
				};
			}
		}

		// Token: 0x0600B4BF RID: 46271 RVA: 0x00379414 File Offset: 0x00377614
		public static bool TryGetMethodEvaluator(string methodSymbol, out MethodScriptSyntaxTreeRule.Evaluator evaluator)
		{
			evaluator = null;
			MethodScriptSyntaxTreeRule.Evaluator evaluator2;
			if (MethodScriptSyntaxTreeRule.s_methodEvaluators.TryGetValue(methodSymbol, out evaluator2))
			{
				evaluator = evaluator2;
				return true;
			}
			return false;
		}

		// Token: 0x0600B4C0 RID: 46272 RVA: 0x0037943C File Offset: 0x0037763C
		public override ScriptSyntaxTreeRule.ParseResult Parse(ScriptToken[] tokens, ref int tokenIndex, out string parseErrorMessage, out ScriptSyntaxTreeNode node)
		{
			node = null;
			ScriptToken scriptToken = tokens[tokenIndex];
			MethodScriptSyntaxTreeRule.Evaluator evaluator;
			if (MethodScriptSyntaxTreeRule.TryGetMethodEvaluator(scriptToken.Value, out evaluator))
			{
				node = new ScriptSyntaxTreeNode(ScriptSyntaxTreeRule<MethodScriptSyntaxTreeRule>.Get());
				parseErrorMessage = null;
				return ScriptSyntaxTreeRule.ParseResult.Success;
			}
			parseErrorMessage = "No such method \"" + scriptToken.Value + "\" exists";
			return ScriptSyntaxTreeRule.ParseResult.Failed;
		}

		// Token: 0x0600B4C1 RID: 46273 RVA: 0x00379494 File Offset: 0x00377694
		public override void Evaluate(ScriptSyntaxTreeNode node, ScriptContext.EvaluationContext context, out object outValue)
		{
			outValue = null;
			if (!context.CheckFeatureIsSupported(ScriptFeatureFlags.Methods))
			{
				return;
			}
			string value = node.Token.Value;
			MethodScriptSyntaxTreeRule.Evaluator evaluator;
			if (!MethodScriptSyntaxTreeRule.TryGetMethodEvaluator(value, out evaluator))
			{
				context.EmitError(ScriptContext.ErrorCodes.EvaluationError, "No such method \"" + value + "\" exists", Array.Empty<object>());
				context.Results.SetFailedNodeIfNoneExists(node, node);
				return;
			}
			if (node.Right == null || node.Right.Rule != ScriptSyntaxTreeRule<ExpressionGroupScriptSyntaxTreeRule>.Get())
			{
				context.EmitError(ScriptContext.ErrorCodes.EvaluationError, "Method signature expected '()' brackets", Array.Empty<object>());
				context.Results.SetFailedNodeIfNoneExists(node, node);
				return;
			}
			object obj;
			if (!node.Right.Evaluate(context, out obj))
			{
				context.EmitError(ScriptContext.ErrorCodes.EvaluationError, "Unexpected error evaluating method arguments!", Array.Empty<object>());
				context.Results.SetFailedNodeIfNoneExists(node, node.Right);
				return;
			}
			ArrayList arrayList = obj as ArrayList;
			if (arrayList == null)
			{
				context.EmitError(ScriptContext.ErrorCodes.EvaluationError, "Unexpected error evaluating method arguments!", Array.Empty<object>());
				context.Results.SetFailedNodeIfNoneExists(node, node.Right);
				return;
			}
			Type[] expectedArgs = evaluator.ExpectedArgs;
			for (int i = 0; i < expectedArgs.Length; i++)
			{
				if (i >= arrayList.Count)
				{
					context.EmitError(ScriptContext.ErrorCodes.EvaluationError, string.Format("{0} expects at least {1} arguments", value, expectedArgs.Length), Array.Empty<object>());
					context.Results.SetFailedNodeIfNoneExists(node, node.Right);
					return;
				}
				Type type = expectedArgs[0];
				DynamicValue dynamicValue = (arrayList[i] is DynamicValue) ? ((DynamicValue)arrayList[i]) : default(DynamicValue);
				if (!dynamicValue.HasValidValue)
				{
					context.EmitError(ScriptContext.ErrorCodes.EvaluationError, "Missing type metadata in arguments list!", Array.Empty<object>());
					context.Results.SetFailedNodeIfNoneExists(node, node.Right);
					return;
				}
				if (!dynamicValue.ValueType.Equals(type) && !dynamicValue.ValueType.IsSubclassOf(type))
				{
					context.EmitError(ScriptContext.ErrorCodes.EvaluationError, string.Format("{0} argument {1} must be type {2}", value, i + 1, type), Array.Empty<object>());
					context.Results.SetFailedNodeIfNoneExists(node, node.Right);
					return;
				}
			}
			List<object> list = new List<object>(arrayList.Count);
			foreach (object obj2 in arrayList)
			{
				DynamicValue dynamicValue2 = (DynamicValue)obj2;
				list.Add(dynamicValue2.Value);
			}
			evaluator.Evaluate(list, node, context, out outValue);
		}

		// Token: 0x040096F1 RID: 38641
		private static readonly Map<string, MethodScriptSyntaxTreeRule.Evaluator> s_methodEvaluators = new Map<string, MethodScriptSyntaxTreeRule.Evaluator>
		{
			{
				MethodScriptSyntaxTreeRule.Evaluator<StringFormatScriptSyntaxTreeRule>.Get().MethodSymbol,
				MethodScriptSyntaxTreeRule.Evaluator<StringFormatScriptSyntaxTreeRule>.Get()
			}
		};

		// Token: 0x02002859 RID: 10329
		public abstract class Evaluator
		{
			// Token: 0x17002D29 RID: 11561
			// (get) Token: 0x06013BA1 RID: 80801 RVA: 0x0053B51C File Offset: 0x0053971C
			public string MethodSymbol
			{
				get
				{
					string result;
					if ((result = this.m_methodSymbolCache) == null)
					{
						result = (this.m_methodSymbolCache = this.MethodSymbolInternal);
					}
					return result;
				}
			}

			// Token: 0x17002D2A RID: 11562
			// (get) Token: 0x06013BA2 RID: 80802 RVA: 0x0053B544 File Offset: 0x00539744
			public Type[] ExpectedArgs
			{
				get
				{
					Type[] result;
					if ((result = this.m_expectedArgsCache) == null)
					{
						result = (this.m_expectedArgsCache = this.ExpectedArgsInternal);
					}
					return result;
				}
			}

			// Token: 0x17002D2B RID: 11563
			// (get) Token: 0x06013BA3 RID: 80803
			protected abstract string MethodSymbolInternal { get; }

			// Token: 0x17002D2C RID: 11564
			// (get) Token: 0x06013BA4 RID: 80804 RVA: 0x00290D59 File Offset: 0x0028EF59
			protected virtual Type[] ExpectedArgsInternal
			{
				get
				{
					return new Type[0];
				}
			}

			// Token: 0x06013BA5 RID: 80805
			public abstract void Evaluate(List<object> args, ScriptSyntaxTreeNode node, ScriptContext.EvaluationContext context, out object outValue);

			// Token: 0x0400F93E RID: 63806
			private string m_methodSymbolCache;

			// Token: 0x0400F93F RID: 63807
			private Type[] m_expectedArgsCache;
		}

		// Token: 0x0200285A RID: 10330
		public abstract class Evaluator<T> : MethodScriptSyntaxTreeRule.Evaluator where T : MethodScriptSyntaxTreeRule.Evaluator, new()
		{
			// Token: 0x06013BA7 RID: 80807 RVA: 0x0053B56A File Offset: 0x0053976A
			public static T Get()
			{
				T result;
				if ((result = MethodScriptSyntaxTreeRule.Evaluator<T>.s_instance) == null)
				{
					result = (MethodScriptSyntaxTreeRule.Evaluator<T>.s_instance = Activator.CreateInstance<T>());
				}
				return result;
			}

			// Token: 0x0400F940 RID: 63808
			private static T s_instance;
		}
	}
}
