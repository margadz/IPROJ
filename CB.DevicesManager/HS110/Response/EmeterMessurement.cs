namespace CB.DevicesManager.HS110.Response
{
    public class EmeterMessurement
    {
        public decimal current { get; set; }
        public decimal voltage { get; set; }
        public decimal power { get; set; }
        public decimal total { get; set; }
        public int err_code { get; set; }
    }
}
