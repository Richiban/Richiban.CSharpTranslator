using System;

namespace CSharpTranslator.Tests.Unit.Output
{

public class Compass
{
	protected enum Case
	{
		North, South, Elsewhere
	}
	protected Case Discriminator { get; }
	protected object[] Items { get; }
	
	protected Compass(Case discriminator, params object[] items)
	{
		Discriminator = discriminator;
		Items = items;
	}


	private string ItemsToString() => Items.Length == 0 ? "" : $"({String.Join(",", Items)})";
    public override string ToString() => $"{Discriminator}{ItemsToString()}";

	public static readonly Compass North = new Compass(Case.North);
	public bool IsNorth() => Discriminator == Case.North;
	
	public static readonly Compass South = new Compass(Case.South);
	public bool IsSouth() => Discriminator == Case.South;
	
		public static Compass Elsewhere(bool IsEast, bool IsWest) => new Compass(Case.Elsewhere, IsEast, IsWest);

	public bool IsElsewhere(out bool IsEast, out bool IsWest)
	{
		if (Discriminator == Case.Elsewhere)
		{
            var result = true;

            IsEast = (bool)Items[0];
IsWest = (bool)Items[1];

            return result;
		}
		else
		{
            IsEast = default(bool);
IsWest = default(bool);

			return false;
		}
	}

	public bool IsElsewhere(bool IsEast, bool IsWest)
	{
		if (Discriminator == Case.Elsewhere)
		{
            var result = true;

            result = result && (bool)Items[0] == IsEast;
result = result && (bool)Items[1] == IsWest;

            return result;
		}
		else
		{
            


			return false;
		}
	}

	public bool IsElsewhere(out bool IsEast, out bool IsWest)
	{
		if (Discriminator == Case.Elsewhere)
		{
            var result = true;

            IsEast = (bool)Items[0];
IsWest = (bool)Items[1];

            return result;
		}
		else
		{
            IsEast = default(bool);
IsWest = default(bool);

			return false;
		}
	}

	public bool IsElsewhere(bool IsEast, bool IsWest)
	{
		if (Discriminator == Case.Elsewhere)
		{
            var result = true;

            result = result && (bool)Items[0] == IsEast;
result = result && (bool)Items[1] == IsWest;

            return result;
		}
		else
		{
            


			return false;
		}
	}
	


	public override bool Equals(object other)
	{
		var otherCompass = other as Compass;

		if (otherCompass == null)
			return false;
			
		if (otherCompass.IsNorth() && this.IsNorth())
			return true;
			
		if (otherCompass.IsSouth() && this.IsSouth())
			return true;
			
		bool otherIsEast; bool otherIsWest; 
		if (otherCompass.IsElsewhere(out otherIsEast, out otherIsWest) && this.IsElsewhere(otherIsEast, otherIsWest))
			return true;
			


		return false;
	}


	public override int GetHashCode() => Items.GetHashCode();
}
}
