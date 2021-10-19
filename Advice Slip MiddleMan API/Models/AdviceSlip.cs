using System;

namespace Advice_Slip_MiddleMan_API
{

    public class AdviceSlip
    {
        public int id { get; set; }
        public string advice { get; set; }
    }
    //public class Slip
    //{
        //public AdviceSlip()
        //{            
        //    OnAdviceSlipCreated();
        //}


        //public int id { get; set; }
        //public string advice { get; set; }

        //public delegate Slip AdviceSlipEventHandler(object source, EventArgs e);
        //public event AdviceSlipEventHandler AdviceSlipCreated;
        //protected virtual void OnAdviceSlipCreated()
        //{
        //AdviceSlipCreated(this, EventArgs.Empty);
        //}
    //}


    

}
