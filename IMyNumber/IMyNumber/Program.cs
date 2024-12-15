﻿using System;
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


}

