using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Hearthstone.UI.Scripting
{
	// Token: 0x02001048 RID: 4168
	public abstract class ScriptSyntaxTreeRule
	{
		// Token: 0x170009CD RID: 2509
		// (get) Token: 0x0600B4D6 RID: 46294
		protected abstract IEnumerable<ScriptSyntaxTreeRule> ExpectedRulesInternal { get; }

		// Token: 0x170009CE RID: 2510
		// (get) Token: 0x0600B4D7 RID: 46295 RVA: 0x00090064 File Offset: 0x0008E264
		protected virtual IEnumerable<ScriptSyntaxTreeRule> NestedRulesInternal
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170009CF RID: 2511
		// (get) Token: 0x0600B4D8 RID: 46296
		protected abstract HashSet<ScriptToken.TokenType> TokensInternal { get; }

		// Token: 0x170009D0 RID: 2512
		// (get) Token: 0x0600B4D9 RID: 46297 RVA: 0x00379F00 File Offset: 0x00378100
		public HashSet<ScriptToken.TokenType> Tokens
		{
			get
			{
				HashSet<ScriptToken.TokenType> result;
				if ((result = this.m_cachedTokens) == null)
				{
					result = (this.m_cachedTokens = this.TokensInternal);
				}
				return result;
			}
		}

		// Token: 0x170009D1 RID: 2513
		// (get) Token: 0x0600B4DA RID: 46298 RVA: 0x00379F28 File Offset: 0x00378128
		public IEnumerable<ScriptSyntaxTreeRule> ExpectedRules
		{
			get
			{
				IEnumerable<ScriptSyntaxTreeRule> result;
				if ((result = this.m_cachedExpectedRules) == null)
				{
					result = (this.m_cachedExpectedRules = this.ExpectedRulesInternal);
				}
				return result;
			}
		}

		// Token: 0x170009D2 RID: 2514
		// (get) Token: 0x0600B4DB RID: 46299 RVA: 0x00379F50 File Offset: 0x00378150
		public IEnumerable<ScriptSyntaxTreeRule> NestedRules
		{
			get
			{
				IEnumerable<ScriptSyntaxTreeRule> result;
				if ((result = this.m_cachedNestedRules) == null)
				{
					result = (this.m_cachedNestedRules = this.NestedRulesInternal);
				}
				return result;
			}
		}

		// Token: 0x0600B4DC RID: 46300 RVA: 0x00379F76 File Offset: 0x00378176
		public virtual ScriptSyntaxTreeRule.ParseResult Parse(ScriptToken[] tokens, ref int tokenIndex, out string parseErrorMessage, out ScriptSyntaxTreeNode node)
		{
			parseErrorMessage = null;
			node = new ScriptSyntaxTreeNode(this);
			return ScriptSyntaxTreeRule.ParseResult.Success;
		}

		// Token: 0x0600B4DD RID: 46301 RVA: 0x0001FA65 File Offset: 0x0001DC65
		public virtual bool ParseStepInto(ScriptToken[] tokens, ref int startToken, ref int endToken)
		{
			return false;
		}

		// Token: 0x0600B4DE RID: 46302
		public abstract void Evaluate(ScriptSyntaxTreeNode node, ScriptContext.EvaluationContext evaluationContext, out object value);

		// Token: 0x040096F5 RID: 38645
		private IEnumerable<ScriptSyntaxTreeRule> m_cachedExpectedRules;

		// Token: 0x040096F6 RID: 38646
		private IEnumerable<ScriptSyntaxTreeRule> m_cachedNestedRules;

		// Token: 0x040096F7 RID: 38647
		private HashSet<ScriptToken.TokenType> m_cachedTokens;

		// Token: 0x0200285B RID: 10331
		public enum ParseResult
		{
			// Token: 0x0400F942 RID: 63810
			Failed,
			// Token: 0x0400F943 RID: 63811
			Success
		}

		// Token: 0x0200285C RID: 10332
		protected static class Utilities
		{
			// Token: 0x06013BAA RID: 80810 RVA: 0x0053B58D File Offset: 0x0053978D
			public static bool IsDynamicType(Type type)
			{
				return type == ScriptSyntaxTreeRule.Utilities.s_objectType;
			}

			// Token: 0x06013BAB RID: 80811 RVA: 0x0053B59C File Offset: 0x0053979C
			public static bool UsesNumericalEncoding(ScriptSyntaxTreeNode node)
			{
				return node.Token.Value[0] == '$';
			}

			// Token: 0x06013BAC RID: 80812 RVA: 0x0053B5C4 File Offset: 0x005397C4
			public static bool TryParseNumericalIdentifier(ScriptSyntaxTreeNode node, out int id)
			{
				int? num;
				if (!ScriptSyntaxTreeRule.Utilities.s_stringToNumericalIdentifier.TryGetValue(node.Token.Value, out num))
				{
					num = (int.TryParse(node.Token.Value.Substring(1, node.Token.Value.Length - 1), out id) ? new int?(id) : null);
					ScriptSyntaxTreeRule.Utilities.s_stringToNumericalIdentifier[node.Token.Value] = num;
				}
				id = (num ?? 0);
				return num != null;
			}

			// Token: 0x06013BAD RID: 80813 RVA: 0x0053B668 File Offset: 0x00539868
			public static bool GetPropertyByDisplayName(IDataModelProperties dataModel, string displayName, out DataModelProperty outProperty)
			{
				foreach (DataModelProperty dataModelProperty in dataModel.Properties)
				{
					if (dataModelProperty.PropertyDisplayName == displayName)
					{
						outProperty = dataModelProperty;
						return true;
					}
				}
				outProperty = default(DataModelProperty);
				return false;
			}

			// Token: 0x06013BAE RID: 80814 RVA: 0x0053B6B4 File Offset: 0x005398B4
			public static void CollectSuggestionsInGlobalNamespace(ScriptContext.EvaluationContext evaluationContext, List<ScriptContext.SuggestionInfo> candidates)
			{
				if (ScriptSyntaxTreeRule.Utilities.s_globalDataModels == null)
				{
					Type dataModelInterface = typeof(IDataModel);
					ScriptSyntaxTreeRule.Utilities.s_globalDataModels = (from a in dataModelInterface.Assembly.GetTypes()
					where a != dataModelInterface && dataModelInterface.IsAssignableFrom(a)
					select Activator.CreateInstance(a) as IDataModel).ToArray<IDataModel>().ToArray<IDataModel>();
				}
				foreach (IDataModel dataModel in ScriptSyntaxTreeRule.Utilities.s_globalDataModels)
				{
					candidates.Add(new ScriptContext.SuggestionInfo
					{
						Identifier = dataModel.DataModelDisplayName,
						CandidateType = ScriptContext.SuggestionInfo.Types.Model,
						ValueType = dataModel.GetType(),
						Weight = 1
					});
				}
				if ((evaluationContext.SupportedFeatures & ScriptFeatureFlags.Keywords) != (ScriptFeatureFlags)0)
				{
					foreach (KeyValuePair<string, object> keyValuePair in ScriptKeywords.Keywords)
					{
						candidates.Add(new ScriptContext.SuggestionInfo
						{
							Identifier = keyValuePair.Key,
							CandidateType = ScriptContext.SuggestionInfo.Types.Keyword,
							ValueType = keyValuePair.Value.GetType()
						});
					}
				}
			}

			// Token: 0x06013BAF RID: 80815 RVA: 0x0053B7F4 File Offset: 0x005399F4
			[Conditional("UNITY_EDITOR")]
			public static void EmitGlobalNamespaceSuggestions(ScriptContext.EvaluationContext evaluationContext)
			{
				if (!evaluationContext.SuggestionsEnabled)
				{
					return;
				}
				List<ScriptContext.SuggestionInfo> list = new List<ScriptContext.SuggestionInfo>();
				ScriptSyntaxTreeRule.Utilities.CollectSuggestionsInGlobalNamespace(evaluationContext, list);
				foreach (ScriptContext.SuggestionInfo suggestionInfo in list)
				{
				}
			}

			// Token: 0x0400F944 RID: 63812
			private static Type s_objectType = typeof(object);

			// Token: 0x0400F945 RID: 63813
			private static readonly Dictionary<string, int?> s_stringToNumericalIdentifier = new Dictionary<string, int?>();

			// Token: 0x0400F946 RID: 63814
			private static IDataModel[] s_globalDataModels;
		}
	}
}
