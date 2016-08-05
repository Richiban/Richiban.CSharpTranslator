namespace CSharpTranslator
{
    public class DiscriminatedUnionCaseArgument
    {
        public int Position { get; }
        public string CaseName { get; }
        public string Name { get; }
        public string Type { get; }

        public DiscriminatedUnionCaseArgument(string caseName, int position, string name, string type)
        {
            Position = position;
            Name = name;
            Type = type;
            CaseName = caseName;
        }

        public DiscriminatedUnionCaseArgument(string caseName, int position, string type) : this(caseName, position, $"_{position}", type)
        {
        }

        public string OtherOutVariableName => $"other{CaseName}{Name}";
        public string ThisOutVariableName => $"this{Name}";

        public string Definition => $"{Type} {Name}";
        public string OutDefinition => $"out {Type} {Name}";

        public string OtherOutVariableDeclaration =>
            $"{Type} {OtherOutVariableName}; ";

        public string ThisOutVariableDeclaration =>
            $"{Type} {ThisOutVariableName}; ";

        public string OtherOutVariableUsage =>
            $"out {OtherOutVariableName}";

        public string OtherOutVariableReference =>
            $"{OtherOutVariableName}";

        public string ThisOutVariableUsage =>
            $"{Type} {OtherOutVariableName}; {Type} {ThisOutVariableName}; ";

        public string Comparison => $"({Type})Items[{Position}] == {Name}";

        public string OutVariableAssignment => $"{Name} = ({Type})Items[{Position}];";
        public string OutVariableDefaultAssignment => $"{Name} = default({Type});";
        public string OutUsage => $"out {Name}";
    }
}