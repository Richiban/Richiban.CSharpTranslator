namespace CSharpTranslator
{
    public class DiscriminatedUnionCaseParameter
    {
        public int Position { get; }
        public string CaseName { get; }
        public string Name { get; }
        public bool IsAnonymous { get; }
        public string Type { get; }

        public DiscriminatedUnionCaseParameter(string caseName, int position, string name, string type)
        {
            Position = position;
            Name = name;
            Type = type;
            CaseName = caseName;
            IsAnonymous = false;
        }

        public DiscriminatedUnionCaseParameter(string caseName, int position, string type)
        {
            Position = position;
            Name = $"_{position}";
            Type = type;
            CaseName = caseName;
            IsAnonymous = true;
        }

        public string Definition => $"{Type} {Name}";
        public string OutArgumentDeclaration => $"{Type} {CaseName}{Name};";
        public string OutParameterDeclaration => $"out {Type} {Name}";

        public string OtherOutVariableDeclaration => $"{Type} other{CaseName}{Name};";

        public string ThisOutVariableDeclaration =>
            $"{Type} this{CaseName}{Name}; ";

        public string OtherOutVariableUsage =>
            $"out other{CaseName}{Name}";

        public string OtherOutVariableReference =>
            $"other{CaseName}{Name}";

        public string ThisOutVariableUsage =>
            $"{Type} other{CaseName}{Name}; {Type} this{CaseName}{Name}; ";

        public string Comparison => $"({Type})Items[{Position}] == {Name}";

        public string OutVariableAssignment => $"{Name} = ({Type})Items[{Position}];";
        public string OutVariableDefaultAssignment => $"{Name} = default({Type});";
        public string OutVariableUsage => $"out {CaseName}{Name}";

        public string ToStringRepresentation => 
            (IsAnonymous) ? $"{{Items[{Position}]}}" : $"{Name}: {{Items[{Position}]}}";
    }
}