namespace SOATest.Contracts
{
    public interface IReserveProductRequest 
    {
        string ProductId { get; set; }

        int ProductQuantity { get; set; }
    }
}