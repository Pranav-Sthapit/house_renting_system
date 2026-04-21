namespace HouseRentalBackend.Exceptions
{
    public class DuplicateException : Exception
    {
        public DuplicateException(string title)
            : base($"{title} already exists, please choose another {title.ToLower()}")
        {}
    }
}