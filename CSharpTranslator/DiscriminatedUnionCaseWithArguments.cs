using System;
using System.Linq;

namespace CSharpTranslator
{
    public class DiscriminatedUnionCaseWithArguments : DiscriminatedUnionCase
    {
        public DiscriminatedUnionCaseArgumentCollection Arguments { get; }
        public DestructureMethodCollection DestructureMethods { get; }

        public DiscriminatedUnionCaseWithArguments(
            string caseName,
            string typeName,
            Tuple<string, string> firstArgumentNameAndType,
            params Tuple<string, string>[] furtherArguments) : base(caseName, typeName)
        {
            Arguments = new NamedDiscriminatedUnionCaseArgumentCollection(caseName, firstArgumentNameAndType, furtherArguments);

            DestructureMethods = new DestructureMethodCollection(Arguments);
        }

        public DiscriminatedUnionCaseWithArguments(
            string caseName,
            string typeName,
            string firstArgumentType,
            params string[] furtherArguments) : base(caseName, typeName)
        {
            Arguments = new AnonymousDiscriminatedUnionCaseArgumentCollection(caseName, firstArgumentType, furtherArguments);
            DestructureMethods = new DestructureMethodCollection(Arguments);
        }

        public override string CreateAndDestructureMethod()
            => $@"{CreateMethod}
{String.Join("\r\n", DestructureMethods.DestructureMethods)}
	
";

        private string CreateMethod
            => $@"	public static {TypeName} {CaseName}({Definitions}) => new {TypeName}(Case.{CaseName}, {Names});";

        private string Names => String.Join(", ", Arguments.Arguments.Select(arg => arg.Name));

        private string Definitions => String.Join(", ", Arguments.Arguments.Select(arg => arg.Definition));

        private string OutVariableDefinitions => String.Join(", ", Arguments.Arguments.Select(arg => arg.OutParameterDeclaration));

        private string OtherOutVariableUsages => String.Join(", ", Arguments.Arguments.Select(arg => arg.OtherOutVariableUsage));
        private string OutVariableUsages => String.Join(", ", Arguments.Arguments.Select(arg => arg.OutVariableUsage));

        private string OtherOutVariableReferences
            => String.Join(", ", Arguments.Arguments.Select(arg => arg.OtherOutVariableReference));

        private string OutVariableDeclarations
            => String.Join("", Arguments.Arguments.Select(arg => arg.OutArgumentDeclaration));

        private string OtherOutVariableDeclarations()
            => String.Join("", Arguments.Arguments.Select(arg => arg.OtherOutVariableDeclaration));

        public override string GetEqualityComparison(string otherVariableName)
            =>
                $@"		{OtherOutVariableDeclarations()}
		if ({otherVariableName}.Is{CaseName}({OtherOutVariableUsages}) && this.Is{CaseName}({OtherOutVariableReferences}))
			return true;
			
";

        public override string GetToString() =>

                $@"		{OutVariableDeclarations}
        if (this.Is{CaseName}({OutVariableUsages}))
			return $""{CaseName}({ToStrings})"";
			
";

        public string ToStrings => String.Join(", ", Arguments.Arguments.Select(arg => arg.ToStringRepresentation));
    }
}