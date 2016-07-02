namespace WebApplication.Models
{
    public class PaymentModel
    {
        public string ik_co_id { get; set; }
        public string ik_pm_no { get; set; }
        public string ik_desc { get; set; }
        public string ik_pw_via { get; set; }
        public string ik_am { get; set; }
        public string ik_cur { get; set; }
        public string ik_act { get; set; }
        //mb ik_x_[name] is here... sorry don`t know
        public string ik_inv_id { get; set; }
        public string ik_co_prs_id { get; set; }
        public string ik_trn_id { get; set; }
        public string ik_inv_crt { get; set; }
        public string ik_inv_prc { get; set; }
        public string ik_inv_st { get; set; }
        public string ik_ps_price { get; set; }
        public string ik_co_rfn { get; set; }
        public string ik_sign { get; set; }
    }
}