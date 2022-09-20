namespace Application.Utils;

public abstract class Fn
{
    public static T Identity<T>(T x) => x;

    public static void Throw<T>(T ex) where T : Exception => throw ex;
}