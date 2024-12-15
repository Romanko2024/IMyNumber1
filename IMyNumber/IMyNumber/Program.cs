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
}

