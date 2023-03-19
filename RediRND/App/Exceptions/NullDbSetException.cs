namespace RediRND.App.Exceptions
{
    public class NullDbSetException : Exception
    {
        public NullDbSetException() : base("DbSet in DbContext is null") { }

        public NullDbSetException(string message) : base("DbSet in DbContext is null: " + message) { }

        public NullDbSetException(string message, Exception innerException) : base("DbSet in DbContext is null: " + message, innerException) { }
    }
}
