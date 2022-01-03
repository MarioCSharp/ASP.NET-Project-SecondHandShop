namespace Shop.Services.Order
{
    public interface IOrderService
    {
        bool ConfirmOrder(string userId);
    }
}
