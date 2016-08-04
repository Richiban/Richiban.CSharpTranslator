using System;

namespace CSharpTranslator
{
    public class EqualsMethod
    {
        public string TypeName { get; }
        public DiscriminatedUnionCaseCollection Cases { get; }

        private readonly string _otherVariableName;

        public EqualsMethod(string typeName, DiscriminatedUnionCaseCollection cases)
        {
            TypeName = typeName;
            _otherVariableName = "other" + typeName;
            Cases = cases;
        }

        public override string ToString() =>
            $@"	public override bool Equals(object other)
	{{
		var {_otherVariableName} = other as {TypeName};

		if ({_otherVariableName} == null)
			return false;
			
{String.Join("", Cases.GetEqualityComparisons(_otherVariableName))}

		return false;
	}}
";
    }
}