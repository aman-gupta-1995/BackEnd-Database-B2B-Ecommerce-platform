namespace AybCommerce.Domain.Enumerations
{
    public enum EntityStatus : short
    {
        None = 0,
        Active = 1,
        Passive = 2,
        Draft = -1,
        Deleted = -999
    }

    public enum CartStatus : short
    {
        None = 0,
        Active = 1,
        Receipt = 2,
        Deleted = -999
    }

    public enum PaymentStatus : short
    {
        None = 0,
        Approved = 1,
        Prending = 2,
        Declined = -1,
    }

    public enum OrderStatus : short
    {
        None = 0, 
        InProgress = 1,
        Approved = 2,
        Completed = 3,
        Declined = 4,
        Refund = -1
    }

    public enum PaymentLogType : short
    {
        None = 0,
        Request = 1,
        Response = 2,
        Commerce = 3
    }
}
