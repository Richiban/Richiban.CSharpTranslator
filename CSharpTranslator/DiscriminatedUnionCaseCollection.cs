using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CSharpTranslator
{
    public class DiscriminatedUnionCaseCollection
    {
        public IReadOnlyCollection<DiscriminatedUnionCase> Cases { get; }

        public DiscriminatedUnionCaseCollection(params DiscriminatedUnionCase[] cases)
        {
            Cases = new ReadOnlyCollection<DiscriminatedUnionCase>(cases);
        }

        public string AsEnum() =>
            $@"	private enum Case
	{{
		{String.Join(", ", Cases.Select(@case => @case.CaseName))}
	}}";

        public string CreateAndDestructureMethods =>
            String.Join("", Cases.Select(@case => @case.CreateAndDestructureMethod()));

        public string GetEqualityComparisons(string otherVariableName) =>
            String.Join("", Cases.Select(@case => @case.GetEqualityComparison(otherVariableName)));

        public string GetToStrings() =>
            String.Join("", Cases.Select(@case => @case.GetToString()));
    }
}