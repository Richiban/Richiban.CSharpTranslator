namespace CSharpTranslator
{
    public class OutDestructureMethodParameter : DestructureMethodParameter
    {
        public override string Declaration => $"out {Type} {Name}";
        public override string IfDestructuringFails => $"\t\t\t{Name} = default({Type});";
        public override string IfDestructuringSucceeds => $"\t\t\t{Name} = ({Type})Items[{Position}];";

        public OutDestructureMethodParameter(int position, string name, string type) : base(position, name, type)
        {
        }
    }
}