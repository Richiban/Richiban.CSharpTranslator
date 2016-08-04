namespace CSharpTranslator
{
    public class Constructor
    {
        public string Name { get; }

        public Constructor(string name)
        {
            Name = name;
        }

        public override string ToString() =>
            $@"	protected {Name}(Case discriminator, params object[] items)
	{{
		Discriminator = discriminator;
		Items = items;
	}}
";
    }
}