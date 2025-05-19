namespace CATECEV.FE.Models.ViewModels
{
    public class PartnerListItem
    {
        public string Name { get; set; }
        public string RegNo { get; set; }

        public decimal BalanceAmount { get; set; }
        public DateTime LastCalculationBalanceDate { get; set; }
        public string EncryptedId { get; set; }
    }
}
