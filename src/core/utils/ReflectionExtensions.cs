using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace d9.aoc.core;
public static class ReflectionExtensions
{
    /// <summary>
    /// Checks that the specified method has the desired signature.
    /// </summary>
    /// <param name="method">The method whose signature to compare.</param>
    /// <param name="returnType">The type the method should return.</param>
    /// <param name="argumentTypes">The type(s), if any, the method should have as arguments</param>
    /// <returns><see langword="true"/> if the method has the specified 
    /// <paramref name="argumentTypes"/> and <paramref name="returnType"/>, or
    /// <see langword="false"/> otherwise.</returns>
    public static bool ParametersMatch(this MethodInfo method, Type returnType, params Type[] argumentTypes)
    {
        if (method.ReturnType != returnType)
            return false;
        Type[] methodArgTypes = method.GetParameters().Select(x => x.ParameterType).ToArray();
        return methodArgTypes.Length == argumentTypes.Length
            && methodArgTypes.Zip(argumentTypes)
                             .All(x => x.First == x.Second);
    }
    public static IEnumerable<object> UsingFile(this MethodInfo method, string filePath)
        => (IEnumerable<object>)method.Invoke(null, [File.ReadAllLines(filePath)])!;
    /// <summary>
    /// https://stackoverflow.com/a/16530993
    /// </summary>
    /// <param name="mi"></param>
    /// <returns></returns>
    public static bool IsOverride(this MethodInfo mi)
        => mi.GetBaseDefinition().DeclaringType != mi.DeclaringType;
}
