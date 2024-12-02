using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace d9.aoc.core;
public static class MathUtils
{
    public static T GreatestCommonDivisor<T>(T a, T b)
        where T : INumber<T>
    {
        static (T large, T small) sort(T a, T b)
            => a > b ? (a, b) : (b, a);
        (T large, T small) = sort(a, b);
        while (small > T.Zero)
            (large, small) = sort(small, large % small);
        return large;
    }
    public static T LeastCommonMultiple<T>(T a, T b)
        where T : INumber<T>
        => T.Abs(a) * (T.Abs(b) / GreatestCommonDivisor(a, b));
    public static (T lo, T hi) QuadraticFormula<T>(T a, T b, T c)
        where T : INumber<T>, IFloatingPointIeee754<T>
    {
        // for most types, multiplying by 2 is faster than addition
        T two = T.CreateChecked(2), four = two * two;
        T discriminant = T.Sqrt((b * b) - four * a * c), denominator = two * a;
        T minusResult = (-b - discriminant) / denominator, plusResult = (-b + discriminant) / denominator;
        return minusResult < plusResult ? (minusResult, plusResult) : (plusResult, minusResult);
    }
    public static IEnumerable<T> Deltas<T>(this IEnumerable<T> enumerable)
        where T : INumber<T>
    {
        if (enumerable.Count() < 2)
            throw new ArgumentException("Can only calculate deltas of two or more numbers!");
        for (int i = 1; i < enumerable.Count(); i++)
            yield return enumerable.ElementAt(i) - enumerable.ElementAt(i - 1);
    }
    public static bool IsMonotonic<T>(this IEnumerable<T> numbers)
        where T : INumber<T>
    {
        IEnumerable<T> deltas = numbers.Deltas();
        bool? isIncreasing = null;
        foreach(T t in deltas)
        {
            int sign = 0;
            if(isIncreasing is null && (sign = T.Sign(t)) != 0)
            {
                isIncreasing = sign > 0;
            }
            else if(isIncreasing != sign > 0)
            {
                return false;
            }
        }
        return true;
    }
}
