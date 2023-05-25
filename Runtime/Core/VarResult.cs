namespace Moein.Core
{
    public class BoolResult
    {
        public bool value;

        private static readonly BoolResult result = new BoolResult();

        public static BoolResult Set(bool value)
        {
            result.value = value;
            return result;
        }
    }

    public class IntResult
    {
        public int value;

        private static readonly IntResult result = new IntResult();

        public static IntResult Set(int value)
        {
            result.value = value;
            return result;
        }
    }

    public class FloatResult
    {
        public float value;

        private static readonly FloatResult result = new FloatResult();

        public static FloatResult Set(float value)
        {
            result.value = value;
            return result;
        }
    }

    public class LongResult
    {
        public long value;
        private static readonly LongResult result = new LongResult();

        public static LongResult Set(long value)
        {
            result.value = value;
            return result;
        }
    }
}