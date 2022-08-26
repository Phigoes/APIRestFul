namespace API.Entities
{
    public static class AssertionConcern
    {
        public static void AssertArgumentLength(string stringValue, int maximum, string message)
        {
            int length = stringValue.Trim().Length;
            if (length > maximum) throw new DomainException(message);
        }

        public static void AssertArgumentLength(string stringValue, int minimun, int maximum, string message)
        {
            int length = stringValue.Trim().Length;
            if (length < minimun || length > maximum) throw new DomainException(message);
        }

        public static void AssertArgumentNotEmpty(string stringValue, string message)
        {
            if (stringValue == null || stringValue.Trim().Length == 0) throw new DomainException(message);
        }

        public static void AssertArgumentNotNull(object object1, string message)
        {
            if (object1 == null) throw new DomainException(message);
        }
    }
}
