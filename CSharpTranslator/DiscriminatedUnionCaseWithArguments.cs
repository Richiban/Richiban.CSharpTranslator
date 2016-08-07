using System;
using System.Linq;

namespace CSharpTranslator
{
    public class ParameterisedDiscriminatedUnionCase : DiscriminatedUnionCase
    {
        public DiscriminatedUnionCaseArgumentCollection Parameters { get; }
        public DestructureMethodCollection DestructureMethods { get; }

        public ParameterisedDiscriminatedUnionCase(
            string caseName,
            string typeName,
            params Tuple<string, string>[] parameters) : base(caseName, typeName)
        {
            Parameters = new NamedDiscriminatedUnionCaseArgumentCollection(caseName, parameters);

            DestructureMethods = new DestructureMethodCollection(Parameters);
        }

        public ParameterisedDiscriminatedUnionCase(
            string caseName,
            string typeName,
            params string[] parameterTypes) : base(caseName, typeName)
        {
            Parameters = new AnonymousDiscriminatedUnionCaseParameterCollection(caseName, parameterTypes);
            DestructureMethods = new DestructureMethodCollection(Parameters);
        }

        public override string CreateAndDestructureMethod()
            => $@"{CreateMethod}
{String.Join("\r\n", DestructureMethods.DestructureMethods)}
	
";

        private string CreateMethod
            => $@"	public static {TypeName} {CaseName}({Definitions}) => new {TypeName}(Case.{CaseName}, {Names});";

        private string Names => String.Join(", ", Parameters.Parameters.Select(arg => arg.Name));

        private string Definitions => String.Join(", ", Parameters.Parameters.Select(arg => arg.Definition));

        private string OutVariableDefinitions => String.Join(", ", Parameters.Parameters.Select(arg => arg.OutParameterDeclaration));

        private string OtherOutVariableUsages => String.Join(", ", Parameters.Parameters.Select(arg => arg.OtherOutVariableUsage));
        private string OutVariableUsages => String.Join(", ", Parameters.Parameters.Select(arg => arg.OutVariableUsage));

        private string OtherOutVariableReferences
            => String.Join(", ", Parameters.Parameters.Select(arg => arg.OtherOutVariableReference));

        private string OutVariableDeclarations
            => String.Join("", Parameters.Parameters.Select(arg => arg.OutArgumentDeclaration));

        private string OtherOutVariableDeclarations()
            => String.Join("", Parameters.Parameters.Select(arg => arg.OtherOutVariableDeclaration));

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

        public string ToStrings => String.Join(", ", Parameters.Parameters.Select(arg => arg.ToStringRepresentation));
    }
}