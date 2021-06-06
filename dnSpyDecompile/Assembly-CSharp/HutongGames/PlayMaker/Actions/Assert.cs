using System;
using PlayMaker.ConditionalExpression;

namespace HutongGames.PlayMaker.Actions
{
	// Token: 0x02000C03 RID: 3075
	[ActionCategory(ActionCategory.Debug)]
	[Tooltip("Checks if the conditional expression Is True or Is False. Breaks the execution of the game if the assertion fails.\nThis is a useful way to check your assumptions. If you expect a certain value use an Assert to make sure!\nOnly runs in Editor.")]
	public class Assert : FsmStateAction, IEvaluatorContext
	{
		// Token: 0x170008C5 RID: 2245
		// (get) Token: 0x06009DAB RID: 40363 RVA: 0x00329901 File Offset: 0x00327B01
		// (set) Token: 0x06009DAC RID: 40364 RVA: 0x00329909 File Offset: 0x00327B09
		public CompiledAst Ast { get; set; }

		// Token: 0x170008C6 RID: 2246
		// (get) Token: 0x06009DAD RID: 40365 RVA: 0x00329912 File Offset: 0x00327B12
		// (set) Token: 0x06009DAE RID: 40366 RVA: 0x0032991A File Offset: 0x00327B1A
		public string LastErrorMessage { get; set; }

		// Token: 0x06009DAF RID: 40367 RVA: 0x00329924 File Offset: 0x00327B24
		FsmVar IEvaluatorContext.GetVariable(string name)
		{
			NamedVariable variable = base.Fsm.Variables.GetVariable(name);
			if (variable != null)
			{
				return new FsmVar(variable);
			}
			throw new VariableNotFoundException(name);
		}

		// Token: 0x04008317 RID: 33559
		[UIHint(UIHint.TextArea)]
		[Tooltip("Enter an expression to evaluate.\nExample: (a < b) && c\n$(variable name with spaces)")]
		public FsmString expression;

		// Token: 0x04008318 RID: 33560
		[Tooltip("Expected result of the expression.")]
		public Assert.AssertType assert;

		// Token: 0x04008319 RID: 33561
		[Tooltip("Repeat every frame while the state is active.")]
		public bool everyFrame;

		// Token: 0x0400831A RID: 33562
		private string cachedExpression;

		// Token: 0x02002795 RID: 10133
		public enum AssertType
		{
			// Token: 0x0400F4B3 RID: 62643
			IsTrue,
			// Token: 0x0400F4B4 RID: 62644
			IsFalse
		}
	}
}
