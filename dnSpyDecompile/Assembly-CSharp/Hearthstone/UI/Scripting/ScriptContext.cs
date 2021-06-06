using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Hearthstone.UI.Scripting
{
	// Token: 0x0200104D RID: 4173
	public class ScriptContext
	{
		// Token: 0x170009D9 RID: 2521
		// (get) Token: 0x0600B4EF RID: 46319 RVA: 0x0037A39C File Offset: 0x0037859C
		public ScriptContext.EvaluationResults Results
		{
			get
			{
				return this.m_evaluationContext.Results;
			}
		}

		// Token: 0x170009DA RID: 2522
		// (get) Token: 0x0600B4F1 RID: 46321 RVA: 0x0037A3B7 File Offset: 0x003785B7
		// (set) Token: 0x0600B4F0 RID: 46320 RVA: 0x0037A3A9 File Offset: 0x003785A9
		public ScriptFeatureFlags SupportedFeatures
		{
			get
			{
				return this.m_evaluationContext.SupportedFeatures;
			}
			set
			{
				this.m_evaluationContext.SupportedFeatures = value;
			}
		}

		// Token: 0x170009DB RID: 2523
		// (get) Token: 0x0600B4F3 RID: 46323 RVA: 0x0037A3D2 File Offset: 0x003785D2
		// (set) Token: 0x0600B4F2 RID: 46322 RVA: 0x0037A3C4 File Offset: 0x003785C4
		public bool SuggestionsEnabled
		{
			get
			{
				return this.m_evaluationContext.SuggestionsEnabled;
			}
			set
			{
				this.m_evaluationContext.SuggestionsEnabled = value;
			}
		}

		// Token: 0x0600B4F4 RID: 46324 RVA: 0x0037A3DF File Offset: 0x003785DF
		public ScriptContext.EvaluationResults Encode(string script, IDataModelProvider dataModelProvider, ScriptContext.EncodingPolicy encodingPolicy, out string encodedString)
		{
			this.m_evaluationContext.EncodingPolicy = encodingPolicy;
			ScriptContext.EvaluationResults result = this.Evaluate(script, dataModelProvider);
			encodedString = ((this.m_evaluationContext.EncodedScript != null) ? this.m_evaluationContext.EncodedScript.ToString() : script);
			return result;
		}

		// Token: 0x0600B4F5 RID: 46325 RVA: 0x0037A418 File Offset: 0x00378618
		public ScriptContext.EvaluationResults Evaluate(string script, IDataModelProvider dataModelProvider)
		{
			this.m_evaluationContext.Script = script;
			this.m_evaluationContext.Results = default(ScriptContext.EvaluationResults);
			this.m_evaluationContext.Results.ErrorCode = ScriptContext.ErrorCodes.Success;
			this.m_evaluationContext.DataModelProvider = dataModelProvider;
			if (this.m_evaluationContext.QueryObjects != null)
			{
				this.m_evaluationContext.QueryObjects.Clear();
			}
			try
			{
				this.m_evaluationContext.SyntaxTree = ScriptSyntaxTree.Get(this.m_evaluationContext.Script);
				this.m_evaluationContext.Results.ParseResults = this.m_evaluationContext.SyntaxTree.Results;
				if (this.m_evaluationContext.Results.ParseResults.ErrorCode != ScriptSyntaxTree.ParseResults.ErrorCodes.Success)
				{
					this.m_evaluationContext.EmitError(ScriptContext.ErrorCodes.ParseError, this.m_evaluationContext.EditMode ? ("Parsing error: " + this.m_evaluationContext.Results.ParseResults.ErrorMessage) : null, Array.Empty<object>());
					if (this.m_evaluationContext.EncodingPolicy == ScriptContext.EncodingPolicy.None && !this.SuggestionsEnabled)
					{
						return this.m_evaluationContext.Results;
					}
				}
			}
			catch (Exception arg)
			{
				this.m_evaluationContext.EmitError(ScriptContext.ErrorCodes.InternalError, this.m_evaluationContext.EditMode ? ("Failed to parse script: " + arg) : null, Array.Empty<object>());
				return this.m_evaluationContext.Results;
			}
			try
			{
				this.m_evaluationContext.EncodedScript = ((this.m_evaluationContext.EncodingPolicy == ScriptContext.EncodingPolicy.None) ? null : new StringBuilder());
				if (this.m_evaluationContext.SyntaxTree.Root != null)
				{
					this.m_evaluationContext.SyntaxTree.Root.Evaluate(this.m_evaluationContext, out this.m_evaluationContext.Results.Value);
				}
			}
			catch (Exception arg2)
			{
				this.m_evaluationContext.EmitError(ScriptContext.ErrorCodes.InternalError, this.m_evaluationContext.EditMode ? ("Failed to evaluate script: " + arg2) : null, Array.Empty<object>());
			}
			return this.m_evaluationContext.Results;
		}

		// Token: 0x0600B4F6 RID: 46326 RVA: 0x0037A630 File Offset: 0x00378830
		protected void EnableEditMode(Func<IEnumerable<IDataModel>> dataModelProvider, Func<Type, IDataModel> dataModelDefaultConstructor)
		{
			this.m_evaluationContext.EditMode = true;
			this.m_evaluationContext.EditModeDataModelProvider = dataModelProvider;
			this.m_evaluationContext.DataModelDefaultConstructor = dataModelDefaultConstructor;
		}

		// Token: 0x040096F9 RID: 38649
		private ScriptContext.EvaluationContext m_evaluationContext = new ScriptContext.EvaluationContext();

		// Token: 0x0200285D RID: 10333
		public enum ErrorCodes
		{
			// Token: 0x0400F948 RID: 63816
			Success,
			// Token: 0x0400F949 RID: 63817
			ParseError,
			// Token: 0x0400F94A RID: 63818
			EvaluationError,
			// Token: 0x0400F94B RID: 63819
			InternalError
		}

		// Token: 0x0200285E RID: 10334
		public class SuggestionInfo
		{
			// Token: 0x0400F94C RID: 63820
			public string Identifier;

			// Token: 0x0400F94D RID: 63821
			public ScriptContext.SuggestionInfo.Types CandidateType;

			// Token: 0x0400F94E RID: 63822
			public Type ValueType;

			// Token: 0x0400F94F RID: 63823
			public int Weight;

			// Token: 0x0400F950 RID: 63824
			public string Description;

			// Token: 0x0400F951 RID: 63825
			public bool IsHinted;

			// Token: 0x020029AD RID: 10669
			public enum Types
			{
				// Token: 0x0400FE11 RID: 65041
				Unknown,
				// Token: 0x0400FE12 RID: 65042
				Keyword,
				// Token: 0x0400FE13 RID: 65043
				Model,
				// Token: 0x0400FE14 RID: 65044
				Property
			}
		}

		// Token: 0x0200285F RID: 10335
		public class FailedNodeInfo
		{
			// Token: 0x17002D2D RID: 11565
			// (get) Token: 0x06013BB2 RID: 80818 RVA: 0x0053B86F File Offset: 0x00539A6F
			public ScriptSyntaxTreeNode EvaluatedNode
			{
				get
				{
					return this.m_evaluatedNode;
				}
			}

			// Token: 0x17002D2E RID: 11566
			// (get) Token: 0x06013BB3 RID: 80819 RVA: 0x0053B877 File Offset: 0x00539A77
			public ScriptSyntaxTreeNode InvalidNode
			{
				get
				{
					return this.m_invalidNode;
				}
			}

			// Token: 0x06013BB4 RID: 80820 RVA: 0x000052CE File Offset: 0x000034CE
			public FailedNodeInfo()
			{
			}

			// Token: 0x06013BB5 RID: 80821 RVA: 0x0053B87F File Offset: 0x00539A7F
			public FailedNodeInfo(ScriptSyntaxTreeNode evaluatedNode, ScriptSyntaxTreeNode invalidNode)
			{
				this.m_evaluatedNode = evaluatedNode;
				this.m_invalidNode = invalidNode;
			}

			// Token: 0x0400F952 RID: 63826
			private ScriptSyntaxTreeNode m_evaluatedNode;

			// Token: 0x0400F953 RID: 63827
			private ScriptSyntaxTreeNode m_invalidNode;
		}

		// Token: 0x02002860 RID: 10336
		public struct EvaluationResults
		{
			// Token: 0x17002D2F RID: 11567
			// (get) Token: 0x06013BB6 RID: 80822 RVA: 0x0053B895 File Offset: 0x00539A95
			public ScriptContext.FailedNodeInfo FailedNodeInfo
			{
				get
				{
					return this.m_failedNodeInfo;
				}
			}

			// Token: 0x06013BB7 RID: 80823 RVA: 0x0053B89D File Offset: 0x00539A9D
			public bool SetFailedNodeIfNoneExists(ScriptSyntaxTreeNode evaluatedNode, ScriptSyntaxTreeNode invalidNode)
			{
				if (this.m_failedNodeInfo == null)
				{
					this.m_failedNodeInfo = new ScriptContext.FailedNodeInfo(evaluatedNode, invalidNode);
					return true;
				}
				return false;
			}

			// Token: 0x0400F954 RID: 63828
			private ScriptContext.FailedNodeInfo m_failedNodeInfo;

			// Token: 0x0400F955 RID: 63829
			public object Value;

			// Token: 0x0400F956 RID: 63830
			public bool EventRaised;

			// Token: 0x0400F957 RID: 63831
			public ScriptContext.ErrorCodes ErrorCode;

			// Token: 0x0400F958 RID: 63832
			public string ErrorMessage;

			// Token: 0x0400F959 RID: 63833
			public ScriptSyntaxTreeNode LastNode;

			// Token: 0x0400F95A RID: 63834
			public ScriptSyntaxTree.ParseResults ParseResults;

			// Token: 0x0400F95B RID: 63835
			public List<ScriptContext.SuggestionInfo> Suggestions;
		}

		// Token: 0x02002861 RID: 10337
		public enum EncodingPolicy
		{
			// Token: 0x0400F95D RID: 63837
			None,
			// Token: 0x0400F95E RID: 63838
			Numerical,
			// Token: 0x0400F95F RID: 63839
			HumanReadable
		}

		// Token: 0x02002862 RID: 10338
		public class EvaluationContext
		{
			// Token: 0x06013BB8 RID: 80824 RVA: 0x0053B8B8 File Offset: 0x00539AB8
			public IDataModel GetDataModelById(int id)
			{
				if (this.EditMode)
				{
					return this.CreateDataModelById(id);
				}
				IDataModel result;
				if (this.DataModelProvider != null && this.DataModelProvider.GetDataModel(id, out result))
				{
					return result;
				}
				return null;
			}

			// Token: 0x06013BB9 RID: 80825 RVA: 0x0053B8F0 File Offset: 0x00539AF0
			public IDataModel GetDataModelByDisplayName(string displayName)
			{
				if (this.EditMode)
				{
					return this.CreateDataModelByName(displayName);
				}
				IDataModel result;
				if (ScriptContext.EvaluationContext.GetDataModelByDisplayName(this.DataModelProvider, displayName, out result))
				{
					return result;
				}
				if (ScriptContext.EvaluationContext.GetDataModelByDisplayName(GlobalDataContext.Get(), displayName, out result))
				{
					return result;
				}
				return null;
			}

			// Token: 0x06013BBA RID: 80826 RVA: 0x0053B934 File Offset: 0x00539B34
			private IDataModel CreateDataModel(Func<IDataModel, bool> predicate)
			{
				IDataModel dataModel;
				if (this.EditModeDataModelProvider != null && this.DataModelDefaultConstructor != null && (dataModel = this.EditModeDataModelProvider().FirstOrDefault(predicate)) != null)
				{
					return this.DataModelDefaultConstructor(dataModel.GetType());
				}
				return null;
			}

			// Token: 0x06013BBB RID: 80827 RVA: 0x0053B97C File Offset: 0x00539B7C
			private IDataModel CreateDataModelByName(string name)
			{
				return this.CreateDataModel((IDataModel a) => a.DataModelDisplayName == name);
			}

			// Token: 0x06013BBC RID: 80828 RVA: 0x0053B9A8 File Offset: 0x00539BA8
			private IDataModel CreateDataModelById(int id)
			{
				return this.CreateDataModel((IDataModel a) => a.DataModelId == id);
			}

			// Token: 0x06013BBD RID: 80829 RVA: 0x0053B9D4 File Offset: 0x00539BD4
			private static bool GetDataModelByDisplayName(IDataModelProvider dataModelProvider, string displayName, out IDataModel outDataModel)
			{
				if (dataModelProvider == null)
				{
					outDataModel = null;
					return false;
				}
				foreach (IDataModel dataModel in dataModelProvider.GetDataModels())
				{
					if (dataModel.DataModelDisplayName == displayName)
					{
						outDataModel = dataModel;
						return true;
					}
				}
				outDataModel = null;
				return false;
			}

			// Token: 0x06013BBE RID: 80830 RVA: 0x0053BA40 File Offset: 0x00539C40
			public void EmitError(ScriptContext.ErrorCodes errorCode, string errorMessage, params object[] args)
			{
				if (this.Results.ErrorMessage == null)
				{
					this.Results.ErrorCode = errorCode;
					this.Results.ErrorMessage = (this.EditMode ? string.Format(errorMessage, args) : null);
					this.Results.Value = null;
				}
			}

			// Token: 0x06013BBF RID: 80831 RVA: 0x0053BA8F File Offset: 0x00539C8F
			public bool CheckFeatureIsSupported(ScriptFeatureFlags feature)
			{
				if ((feature & this.SupportedFeatures) == (ScriptFeatureFlags)0)
				{
					this.EmitError(ScriptContext.ErrorCodes.EvaluationError, "Unsupported feature: {0}", new object[]
					{
						feature
					});
					return false;
				}
				return true;
			}

			// Token: 0x06013BC0 RID: 80832 RVA: 0x0053BABC File Offset: 0x00539CBC
			[Conditional("UNITY_EDITOR")]
			public void AppendToEncodedScript(ScriptSyntaxTreeNode node, string encodedNode, bool autoSpacing, int offset = 0)
			{
				if (this.EncodedScript == null || node == null)
				{
					return;
				}
				this.EncodedScript.Append(encodedNode);
				if (node.Tokens != null && node.Tokens.Length != 0)
				{
					ScriptToken scriptToken = node.Tokens[node.Tokens.Length - 1];
					for (int i = scriptToken.EndIndex; i < this.Script.Length; i++)
					{
						char c = this.Script[i];
						if (c == '\n')
						{
							this.EncodedScript.Append("\n");
							break;
						}
						if (i > scriptToken.EndIndex && c != ' ')
						{
							break;
						}
					}
					int num = scriptToken.Index + 1 + offset;
					if (num < this.SyntaxTree.Root.Tokens.Length)
					{
						ScriptToken.TokenType type = this.SyntaxTree.Root.Tokens[num].Type;
						if (type != ScriptToken.TokenType.Period && type - ScriptToken.TokenType.Colon > 6 && autoSpacing)
						{
							this.EncodedScript.Append(' ');
						}
					}
				}
			}

			// Token: 0x06013BC1 RID: 80833 RVA: 0x0053BBB9 File Offset: 0x00539DB9
			[Conditional("UNITY_EDITOR")]
			public void EmitSuggestion(ScriptContext.SuggestionInfo suggestion)
			{
				if (!this.SuggestionsEnabled)
				{
					return;
				}
				if (this.Results.Suggestions == null)
				{
					this.Results.Suggestions = new List<ScriptContext.SuggestionInfo>();
				}
				this.Results.Suggestions.Add(suggestion);
			}

			// Token: 0x0400F960 RID: 63840
			public IDataModelProvider DataModelProvider;

			// Token: 0x0400F961 RID: 63841
			public string Script;

			// Token: 0x0400F962 RID: 63842
			public ScriptContext.EvaluationResults Results;

			// Token: 0x0400F963 RID: 63843
			public ScriptSyntaxTree SyntaxTree;

			// Token: 0x0400F964 RID: 63844
			public ScriptContext.EncodingPolicy EncodingPolicy;

			// Token: 0x0400F965 RID: 63845
			public StringBuilder EncodedScript;

			// Token: 0x0400F966 RID: 63846
			public List<object> QueryObjects;

			// Token: 0x0400F967 RID: 63847
			public Func<Type, IDataModel> DataModelDefaultConstructor;

			// Token: 0x0400F968 RID: 63848
			public Func<IEnumerable<IDataModel>> EditModeDataModelProvider;

			// Token: 0x0400F969 RID: 63849
			public Map<ScriptSyntaxTreeNode, int> CachedNodeValues;

			// Token: 0x0400F96A RID: 63850
			public ScriptFeatureFlags SupportedFeatures = ScriptFeatureFlags.All;

			// Token: 0x0400F96B RID: 63851
			public bool SuggestionsEnabled;

			// Token: 0x0400F96C RID: 63852
			public bool EditMode;
		}
	}
}
