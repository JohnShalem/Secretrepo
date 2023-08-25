namespace WhatsAppAPI.IServices
{
    public interface IWhatsAppIntegrator
    {
        void SaveCustomerData(string fieldName, string value, int? customerId);
    }
}
