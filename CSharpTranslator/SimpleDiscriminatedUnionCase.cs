namespace CSharpTranslator
{
    public class SimpleDiscriminatedUnionCase : DiscriminatedUnionCase
    {
        public SimpleDiscriminatedUnionCase(string caseName, string typeName)
            : base(caseName, typeName)
        {
        }

        public override string CreateAndDestructureMethod() =>
            $@"	public static readonly {TypeName} {CaseName} = new {TypeName}(Case.{CaseName});
	public bool Is{CaseName}() => Discriminator == Case.{CaseName};
	
";

        public override string GetEqualityComparison(string otherVariableName) =>
            $@"		if ({otherVariableName}.Is{CaseName}() && this.Is{CaseName}())
			return true;
			
";

        public override string GetToString() =>
            $@"		if (this.Is{CaseName}())
			return ""{CaseName}"";
			
";
    }
}