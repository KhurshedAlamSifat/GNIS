using System.ComponentModel.DataAnnotations;

namespace GNIS_MUL_Form.ViewModels
{
    public class MeetingMinutesViewModel
    {
        public string SelectedCustomerType { get; set; }
        public int SelectedCustomerId { get; set; }
        public DateTime MeetingDate { get; set; }
        public string MeetingTime { get; set; }
        public string MeetingPlace { get; set; }
        public string AttendsClient { get; set; }
        public string AttendsHost { get; set; }
        public string MeetingAgenda { get; set; }
        public string MeetingDiscussion { get; set; }
        public string MeetingDecision { get; set; }

        // List to hold meeting details
        public List<ProductServiceViewModel> ProductsServices { get; set; }
        public List<MeetingMinutesDetailViewModel> MeetingMinutesDetails { get; set; }
    }
}
