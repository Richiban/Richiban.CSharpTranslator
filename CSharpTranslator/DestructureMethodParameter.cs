namespace CSharpTranslator
{
    public abstract class DestructureMethodParameter
    {
        protected DestructureMethodParameter(int position, string name, string type)
        {
            Position = position;
            Name = name;
            Type = type;
        }

        public int Position { get; }
        public string Name { get; }
        public string Type { get; }
        public abstract string Declaration { get; }
        public abstract string IfDestructuringFails { get; }
        public abstract string IfDestructuringSucceeds { get; }
    }
}