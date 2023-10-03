namespace GlobalTradeSevkiyatTakip.Application.DTOs.EntegreDTOs
{
    public class IrsaliyeEntegreViewModelDto
    {
        public IrsaliyeEntegreDto? Irsaliye { get; set; }
        public List<IrsaliyeDetayEntegreDto>? IrsaliyeDetay { get; set; }
        public IrsaliyeEntegreViewModelDto()
        {
            Irsaliye = new IrsaliyeEntegreDto();
            IrsaliyeDetay = new List<IrsaliyeDetayEntegreDto>();
        }
    }
}
