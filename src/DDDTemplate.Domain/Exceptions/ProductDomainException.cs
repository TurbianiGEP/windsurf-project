namespace DDDTemplate.Domain.Exceptions
{
    public class ProductDomainException : DomainException
    {
        public ProductDomainException(string message) : base(message)
        {
        }

        public ProductDomainException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
