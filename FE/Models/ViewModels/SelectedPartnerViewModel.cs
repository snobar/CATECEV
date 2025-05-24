namespace CATECEV.FE.Models.ViewModels
{
    public class SelectedPartnerViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string RegNo { get; set; }
        public string Email { get; set; }        
        public string Mobile { get; set; }      
        public decimal BalanceAmount { get; set; }
        public DateTime? LastCalculationBalanceDate { get; set; }
    }

}
