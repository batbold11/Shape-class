using System;
using System.Collections.Generic;
using System.Text;

// Бүх дүрсийн эцэг класс (abstract - шууд объект үүсгэх боломжгүй)
abstract class Shape
{
    public string Name { get; }

    protected Shape(string name)
    {
        Name = name;
    }

    // Дүрс бүр өөрийнхөөрөө тооцоолно
    public abstract double Area();
    public abstract double Perimeter();

    public virtual void Display()
    {
        Console.WriteLine($"{Name}: талбай = {Area():F2}, периметр = {Perimeter():F2}");
    }
}

class Circle : Shape
{
    public double Radius { get; }

    public Circle(double radius) : base("Тойрог")
    {
        if (radius <= 0) throw new ArgumentException("Радиус 0-ээс их байх ёстой.");
        Radius = radius;
    }

    public override double Area() => Math.PI * Radius * Radius;
    public override double Perimeter() => 2 * Math.PI * Radius;
}

class Rectangle : Shape
{
    public double Width { get; }
    public double Height { get; }

    public Rectangle(double width, double height) : base("Тэгш өнцөгт")
    {
        if (width <= 0 || height <= 0)
            throw new ArgumentException("Урт, өргөн 0-ээс их байх ёстой.");
        Width = width;
        Height = height;
    }

    public override double Area() => Width * Height;
    public override double Perimeter() => 2 * (Width + Height);
}

class Triangle : Shape
{
    public double A { get; }
    public double B { get; }
    public double C { get; }

    public Triangle(double a, double b, double c) : base("Гурвалжин")
    {
        if (a <= 0 || b <= 0 || c <= 0)
            throw new ArgumentException("Талууд 0-ээс их байх ёстой.");
        if (a + b <= c || a + c <= b || b + c <= a)
            throw new ArgumentException("Ийм талтай гурвалжин байхгүй.");
        A = a;
        B = b;
        C = c;
    }

    public override double Perimeter() => A + B + C;

    // Херон-ийн томъёо
    public override double Area()
    {
        double p = Perimeter() / 2;
        return Math.Sqrt(p * (p - A) * (p - B) * (p - C));
    }
}

class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        var shapes = new List<Shape>
        {
            new Circle(5),
            new Rectangle(4, 6),
            new Triangle(3, 4, 5)
        };

        // Полиморфизм: бүгдийг Shape гэж үзээд адилхан дуудна
        double totalArea = 0;
        foreach (Shape s in shapes)
        {
            s.Display();
            totalArea += s.Area();
        }

        Console.WriteLine($"\nНийт талбай: {totalArea:F2}");

        // Алдаатай утга шалгах жишээ
        try
        {
            var bad = new Triangle(1, 2, 10);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine("Алдаа: " + ex.Message);
        }

        Console.ReadKey();
    }
}