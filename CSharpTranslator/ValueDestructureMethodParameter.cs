namespace CSharpTranslator
{
    public class ValueDestructureMethodParameter : DestructureMethodParameter
    {
        public override string Declaration => $@"{Type} {Name}";
        public override string IfDestructuringFails => $@"";
        public override string IfDestructuringSucceeds => 
            $"\t\t\tresult = result && ({Type})Items[{Position}] == {Name};";

        public ValueDestructureMethodParameter(int position, string name, string type) : base(position, name, type)
        {
        }
    }
}