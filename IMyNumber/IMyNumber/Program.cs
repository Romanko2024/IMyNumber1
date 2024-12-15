using System;
using System.Numerics;

interface IMyNumber<T> where T : IMyNumber<T>
{
    T Add(T b);
    T Subtract(T b);
    T Multiply(T b);
    T Divide(T b);
}
public class MyFrac : IMyNumber<MyFrac>, IComparable<MyFrac>
{
    //numerator/denominator
    private BigInteger nom; 
    private BigInteger denom;

    // констркуктор що ініціалізує дріб за nom denom
    public MyFrac(BigInteger nom, BigInteger denom)
    {
        //
        if (denom == 0) throw new DivideByZeroException("Знаменник не може бути нулем.");
        //присвоєння значень 
        this.nom = nom; 
        this.denom = denom; 

        Simplify(); 
    }

    //ділить чисельник знаменник на їх НСД
    private void Simplify()
    {
        var gcd = BigInteger.GreatestCommonDivisor(nom, denom);
        nom /= gcd;
        denom /= gcd;
        //-a/-b=a/b
        if (denom < 0) { nom = -nom; denom = -denom; }
    }
    //констр. парсить дріб зі стрінга у numerator/denominator

    public MyFrac(string fraction)
    {
        var parts = fraction.Split('/');
        if (parts.Length != 2 || !BigInteger.TryParse(parts[0], out nom) || !BigInteger.TryParse(parts[1], out denom))
        {
            throw new ArgumentException("Некоректний формат дробу.");
        }
        if (denom == 0) throw new DivideByZeroException("Знаменник не може бути нулем.");
        Simplify();
    }

    //додавання дво дробів 
    public MyFrac Add(MyFrac that)
    {
        return new MyFrac(nom * that.denom + denom * that.nom, denom * that.denom);
    }
    // віднімання одного дроба від іншого
    public MyFrac Subtract(MyFrac that)
    {
        return new MyFrac(nom * that.denom - denom * that.nom, denom * that.denom);
    }
    // множення дробів
    public MyFrac Multiply(MyFrac that)
    {
        return new MyFrac(nom * that.nom, denom * that.denom);
    }
        //ділення дробів
    public MyFrac Divide(MyFrac that)
    {
        if (that.nom == 0) throw new DivideByZeroException("Не можна ділити на нуль");
        return new MyFrac(nom * that.denom, denom * that.nom);
    }

    //порывняння двох дробів
    public int CompareTo(MyFrac that)
    {
        //чисельник дробу this після множення його чисельника на знаменник дробу that
        BigInteger left = nom * that.denom;
        //чисельник дробу that після множення його чисельника на знаменник дробу this
        BigInteger right = denom * that.nom;
        return left.CompareTo(right);
    }

    //вивід результату    
    public override string ToString()
    {
        return $"{nom}/{denom}";
    }
}

//---------------------------------------------------------------------------------------------------

public class MyComplex : IMyNumber<MyComplex>
{
    private double re; // дійсна частина
    private double im; // уявна

    //констр. що ініц. комплексне число з дійсною та уявною частиною
    public MyComplex(double re, double im)
    {
        this.re = re;
        this.im = im;
    }

    // констр. що парсить комплексне число зі стрінга в re+im
    public MyComplex(string complex)
    {
        var parts = complex.Split('+', '-');
        if (parts.Length != 2 || !double.TryParse(parts[0], out re) || !double.TryParse(parts[1].Replace("i", ""), out im))
        {
            throw new ArgumentException("Некоректний формат комплексного числа.");
        }
    }

    // додавання компл
    public MyComplex Add(MyComplex that)
    {
        return new MyComplex(re + that.re, im + that.im);
    }
    //віднімання компл,
    public MyComplex Subtract(MyComplex that)
    {
        return new MyComplex(re - that.re, im - that.im);
    }
    // vyj;/ rjvgk/
    public MyComplex Multiply(MyComplex that)
    {
        return new MyComplex(re * that.re - im * that.im, re * that.im + im * that.re);
    }

    // lsktyyz rjvgk/
    public MyComplex Divide(MyComplex that)
    {
        //C^2+d^2
        double denominator = that.re * that.re + that.im * that.im;
        if (denominator == 0) throw new DivideByZeroException("Не можна ділити на нуль");
        return new MyComplex((re * that.re + im * that.im) / denominator, (im * that.re - re * that.im) / denominator);
    }
    public override string ToString()
    {
        return $"{re}+{im}i";
    }
}

//------------------------------------------------------------------------------------------------------------------------
class Program
{
    static void TestAPlusBSquare<T>(T a, T b) where T : IMyNumber<T>
    {
        Console.WriteLine("=== Starting testing (a+b)^2=a^2+2ab+b^2 with a = " + a + ", b = " + b + " ===");
        T aPlusB = a.Add(b);
        Console.WriteLine("a = " + a);
        Console.WriteLine("b = " + b);
        Console.WriteLine("(a + b) = " + aPlusB);
        Console.WriteLine("(a+b)^2 = " + aPlusB.Multiply(aPlusB));
        Console.WriteLine(" = = = ");
        T curr = a.Multiply(a);
        Console.WriteLine("a^2 = " + curr);
        T wholeRightPart = curr;
        curr = a.Multiply(b); // ab
        curr = curr.Add(curr); // ab + ab = 2ab
        

Console.WriteLine("2*a*b = " + curr);
        wholeRightPart = wholeRightPart.Add(curr);
        curr = b.Multiply(b);
        Console.WriteLine("b^2 = " + curr);
        wholeRightPart = wholeRightPart.Add(curr);
        Console.WriteLine("a^2+2ab+b^2 = " + wholeRightPart);
        Console.WriteLine("=== Finishing testing (a+b)^2=a^2+2ab+b^2 with a = " + a + ", b = " + b + " ===");
    }
    static void Main(string[] args)
    {
        TestAPlusBSquare(new MyFrac(1, 3), new MyFrac(1, 6));
        TestAPlusBSquare(new MyComplex(1, 3), new MyComplex(1, 6));

        TestSquaresDifference(new MyFrac(1, 3), new MyFrac(1, 6));
        TestSquaresDifference(new MyComplex(1, 3), new MyComplex(1, 6));

        Console.ReadKey();
    }
    static void TestSquaresDifference<T>(T a, T b) where T : IMyNumber<T>
    {
        Console.WriteLine($"=== Starting tеsting (a-b) and (a^2-b^2)/(a+b) with a = {a}, b = {b} ===");
        T aMinusB = a.Subtract(b);
        T aSquared = a.Multiply(a);
        T bSquared = b.Multiply(b);
        T denominator = a.Add(b);

        try
        {
            T resultDivision = aSquared.Subtract(bSquared).Divide(denominator);
            Console.WriteLine($"a-b = {aMinusB}");
            Console.WriteLine($"(a^2-b^2)/(a+b) = {resultDivision}");
        }
        catch (DivideByZeroException e)
        {
            Console.WriteLine($"Error: {e.Message}");
        }

        Console.WriteLine($"=== Finishing tеsting (a-b) and (a^2-b^2)/(a+b) with a = {a}, b = {b} ===");
    }
}

