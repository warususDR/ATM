namespace ATM
{
    public class eMonobank : eBank
    {
        static private string bankCode = "11";
        public eMonobank(eCommutator _commutator)
            : base(bankCode, _commutator)
        {}
    }
}