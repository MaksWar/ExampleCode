namespace Additions.Extensions
{
    public static class IntExtension
    {
        public static int ToMilliseconds(this int value)
            => value * 1000;
    }
}