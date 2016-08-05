using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CSharpTranslator
{
    public class DiscriminatedUnionCaseWithArguments : DiscriminatedUnionCase
    {
        public IReadOnlyCollection<DiscriminatedUnionCaseArgument> Arguments { get; }
        public DestructureMethodCollection DestructureMethods { get; }

        public DiscriminatedUnionCaseWithArguments(
            string caseName,
            string typeName,
            Tuple<string, string> firstArgumentNameAndType,
            params Tuple<string, string>[] furtherArguments) : base(caseName, typeName)
        {
            var allArguments = new[] { firstArgumentNameAndType }.Concat(furtherArguments).ToList();

            Arguments =
                new ReadOnlyCollection<DiscriminatedUnionCaseArgument>(
                    allArguments.Zip(
                        Enumerable.Range(0, allArguments.Count),
                        (t, i) => new DiscriminatedUnionCaseArgument(caseName, i, t.Item1, t.Item2)).ToArray());

            DestructureMethods = new DestructureMethodCollection(caseName, Arguments);
        }

        public DiscriminatedUnionCaseWithArguments(
            string caseName,
            string typeName,
            string firstArgumentType,
            params string[] furtherArguments) : base(caseName, typeName)
        {
            var allArguments = new[] { firstArgumentType }.Concat(furtherArguments).ToList();

            Arguments =
                new ReadOnlyCollection<DiscriminatedUnionCaseArgument>(
                    allArguments.Zip(
                        Enumerable.Range(0, allArguments.Count),
                        (s, i) => new DiscriminatedUnionCaseArgument(caseName, i, s)).ToArray());

            DestructureMethods = new DestructureMethodCollection(caseName, Arguments);
        }

        public override string CreateAndDestructureMethod()
            => $@"{CreateMethod}
{String.Join("\r\n", DestructureMethods.DestructureMethods)}
	
";

        private string CreateMethod
            => $@"	public static {TypeName} {CaseName}({Definitions}) => new {TypeName}(Case.{CaseName}, {Names});";

        private string Names => String.Join(", ", Arguments.Select(arg => arg.Name));

        private string Definitions => String.Join(", ", Arguments.Select(arg => arg.Definition));

        private string OutVariableDefinitions => String.Join(", ", Arguments.Select(arg => arg.OutDefinition));

        private string OtherOutVariableUsages => String.Join(", ", Arguments.Select(arg => arg.OtherOutVariableUsage));

        private string OtherOutVariableReferences
            => String.Join(", ", Arguments.Select(arg => arg.OtherOutVariableReference));

        private string OtherOutVariableDeclarations()
            => String.Join("", Arguments.Select(arg => arg.OtherOutVariableDeclaration));

        public override string GetEqualityComparison(string otherVariableName)
            =>
                $@"		{OtherOutVariableDeclarations()}
		if ({otherVariableName}.Is{CaseName}({OtherOutVariableUsages}) && this.Is{CaseName}({OtherOutVariableReferences}))
			return true;
			
";
    }
}