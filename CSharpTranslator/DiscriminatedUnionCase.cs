namespace CSharpTranslator
{
    public abstract class DiscriminatedUnionCase
    {
        public string CaseName { get; }
        public string TypeName { get; }

        protected DiscriminatedUnionCase(string caseName, string typeName)
        {
            CaseName = caseName;
            TypeName = typeName;
        }
        public abstract string CreateAndDestructureMethod();

        public abstract string GetEqualityComparison(string otherVariableName);

        public abstract string GetToString();
    }
}