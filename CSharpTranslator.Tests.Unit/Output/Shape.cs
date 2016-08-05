using System;

namespace CSharpTranslator.Tests.Unit.Output
{

public class Shape
{
	private enum Case
	{
		Point, Line, Square, Cube
	}

	private Case Discriminator { get; }
	private object[] Items { get; }
	
	private Shape(Case discriminator, params object[] items)
	{
		Discriminator = discriminator;
		Items = items;
	}

	public override string ToString()
	{
		if (this.IsPoint())
			return "Point";
			
		int LineLength;
        if (this.IsLine(out LineLength))
			return $"Line(Length: {Items[0]})";
			
		int SquareWidth;int SquareHeight;
        if (this.IsSquare(out SquareWidth, out SquareHeight))
			return $"Square(Width: {Items[0]}, Height: {Items[1]})";
			
		int CubeWidth;int CubeHeight;int CubeDepth;
        if (this.IsCube(out CubeWidth, out CubeHeight, out CubeDepth))
			return $"Cube(Width: {Items[0]}, Height: {Items[1]}, Depth: {Items[2]})";
			
		return "";
	}


	public static readonly Shape Point = new Shape(Case.Point);
	public bool IsPoint() => Discriminator == Case.Point;
	
	public static Shape Line(int Length) => new Shape(Case.Line, Length);

	public bool IsLine(int Length)
	{
		if (Discriminator == Case.Line)
		{
            var result = true;

			result = result && (int)Items[0] == Length;

            return result;
		}
		else
		{


			return false;
		}
	}

	public bool IsLine(out int Length)
	{
		if (Discriminator == Case.Line)
		{
            var result = true;

			Length = (int)Items[0];

            return result;
		}
		else
		{
			Length = default(int);

			return false;
		}
	}
	
	public static Shape Square(int Width, int Height) => new Shape(Case.Square, Width, Height);

	public bool IsSquare(int Width, int Height)
	{
		if (Discriminator == Case.Square)
		{
            var result = true;

			result = result && (int)Items[0] == Width;
			result = result && (int)Items[1] == Height;

            return result;
		}
		else
		{



			return false;
		}
	}

	public bool IsSquare(out int Width, int Height)
	{
		if (Discriminator == Case.Square)
		{
            var result = true;

			Width = (int)Items[0];
			result = result && (int)Items[1] == Height;

            return result;
		}
		else
		{
			Width = default(int);


			return false;
		}
	}

	public bool IsSquare(int Width, out int Height)
	{
		if (Discriminator == Case.Square)
		{
            var result = true;

			result = result && (int)Items[0] == Width;
			Height = (int)Items[1];

            return result;
		}
		else
		{

			Height = default(int);

			return false;
		}
	}

	public bool IsSquare(out int Width, out int Height)
	{
		if (Discriminator == Case.Square)
		{
            var result = true;

			Width = (int)Items[0];
			Height = (int)Items[1];

            return result;
		}
		else
		{
			Width = default(int);
			Height = default(int);

			return false;
		}
	}
	
	public static Shape Cube(int Width, int Height, int Depth) => new Shape(Case.Cube, Width, Height, Depth);

	public bool IsCube(int Width, int Height, int Depth)
	{
		if (Discriminator == Case.Cube)
		{
            var result = true;

			result = result && (int)Items[0] == Width;
			result = result && (int)Items[1] == Height;
			result = result && (int)Items[2] == Depth;

            return result;
		}
		else
		{




			return false;
		}
	}

	public bool IsCube(out int Width, int Height, int Depth)
	{
		if (Discriminator == Case.Cube)
		{
            var result = true;

			Width = (int)Items[0];
			result = result && (int)Items[1] == Height;
			result = result && (int)Items[2] == Depth;

            return result;
		}
		else
		{
			Width = default(int);



			return false;
		}
	}

	public bool IsCube(int Width, out int Height, int Depth)
	{
		if (Discriminator == Case.Cube)
		{
            var result = true;

			result = result && (int)Items[0] == Width;
			Height = (int)Items[1];
			result = result && (int)Items[2] == Depth;

            return result;
		}
		else
		{

			Height = default(int);


			return false;
		}
	}

	public bool IsCube(out int Width, out int Height, int Depth)
	{
		if (Discriminator == Case.Cube)
		{
            var result = true;

			Width = (int)Items[0];
			Height = (int)Items[1];
			result = result && (int)Items[2] == Depth;

            return result;
		}
		else
		{
			Width = default(int);
			Height = default(int);


			return false;
		}
	}

	public bool IsCube(int Width, int Height, out int Depth)
	{
		if (Discriminator == Case.Cube)
		{
            var result = true;

			result = result && (int)Items[0] == Width;
			result = result && (int)Items[1] == Height;
			Depth = (int)Items[2];

            return result;
		}
		else
		{


			Depth = default(int);

			return false;
		}
	}

	public bool IsCube(out int Width, int Height, out int Depth)
	{
		if (Discriminator == Case.Cube)
		{
            var result = true;

			Width = (int)Items[0];
			result = result && (int)Items[1] == Height;
			Depth = (int)Items[2];

            return result;
		}
		else
		{
			Width = default(int);

			Depth = default(int);

			return false;
		}
	}

	public bool IsCube(int Width, out int Height, out int Depth)
	{
		if (Discriminator == Case.Cube)
		{
            var result = true;

			result = result && (int)Items[0] == Width;
			Height = (int)Items[1];
			Depth = (int)Items[2];

            return result;
		}
		else
		{

			Height = default(int);
			Depth = default(int);

			return false;
		}
	}

	public bool IsCube(out int Width, out int Height, out int Depth)
	{
		if (Discriminator == Case.Cube)
		{
            var result = true;

			Width = (int)Items[0];
			Height = (int)Items[1];
			Depth = (int)Items[2];

            return result;
		}
		else
		{
			Width = default(int);
			Height = default(int);
			Depth = default(int);

			return false;
		}
	}
	


	public override bool Equals(object other)
	{
		var otherShape = other as Shape;

		if (otherShape == null)
			return false;
			
		if (otherShape.IsPoint() && this.IsPoint())
			return true;
			
		int otherLineLength;
		if (otherShape.IsLine(out otherLineLength) && this.IsLine(otherLineLength))
			return true;
			
		int otherSquareWidth;int otherSquareHeight;
		if (otherShape.IsSquare(out otherSquareWidth, out otherSquareHeight) && this.IsSquare(otherSquareWidth, otherSquareHeight))
			return true;
			
		int otherCubeWidth;int otherCubeHeight;int otherCubeDepth;
		if (otherShape.IsCube(out otherCubeWidth, out otherCubeHeight, out otherCubeDepth) && this.IsCube(otherCubeWidth, otherCubeHeight, otherCubeDepth))
			return true;
			


		return false;
	}


	public override int GetHashCode() => Items.GetHashCode();
}
}
