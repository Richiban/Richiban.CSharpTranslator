using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpTranslator
{
    public class DestructureMethod
    {
        public DestructureMethod(string caseName, IReadOnlyCollection<DestructureMethodParameter> methodParameters)
        {
            MethodParameters = methodParameters;
            CaseName = caseName;
        }

        public override string ToString()
            =>
                $@"
	public bool Is{CaseName}({ParameterDefinitions})
	{{
		if (Discriminator == Case.{CaseName})
		{{
            var result = true;

            {IfDestructuringSucceeds}

            return result;
		}}
		else
		{{
            {IfDestructuringFails}

			return false;
		}}
	}}";

        public string IfDestructuringSucceeds  =>
            String.Join("\r\n", MethodParameters.Select(p => p.IfDestructuringSucceeds));
        public string IfDestructuringFails  =>
            String.Join("\r\n", MethodParameters.Select(p => p.IfDestructuringFails));

        public string CaseName { get; }

        public string ParameterDefinitions => String.Join(", ", MethodParameters.Select(p => p.Declaration));

        public IReadOnlyCollection<DestructureMethodParameter> MethodParameters { get; }
    }
}