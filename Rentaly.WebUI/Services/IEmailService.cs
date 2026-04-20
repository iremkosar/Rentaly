namespace Rentaly.WebUI.Services
{
    public interface IEmailService
    {
        Task SendReservationConfirmationAsync(string toEmail, string toName, int reservationId, string discountCode, DateTime pickUpDateTime, DateTime returnDateTime);
    }
}
