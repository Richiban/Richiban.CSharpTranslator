using System;
using System.Linq;

namespace CSharpTranslator
{
    public class ToStringMethod
    {
        public string TypeName { get; }
        public DiscriminatedUnionCaseCollection Cases { get; }

        public ToStringMethod(string typeName, DiscriminatedUnionCaseCollection cases)
        {
            TypeName = typeName;
            Cases = cases;
        }

        public override string ToString()
            =>
                $@"	public override string ToString()
	{{
{String.Join("", Cases.GetToStrings())}{"\t\t"}return """";
	}}
";
    }
}