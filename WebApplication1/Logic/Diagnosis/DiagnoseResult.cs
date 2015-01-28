using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Diagnosis
{
    public class DiagnoseResult
    {
        private int _id;
        private string _deviceid;
        private DateTime? _diagnosedate;
        private int? _gasid;
        private string _threeratiocode;
        private string _threeratioresult;
        private string _devidcode;
        private string _devidresult;
        private string _cubecode;
        protected string _result;
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DeviceID
        {
            set { _deviceid = value; }
            get { return _deviceid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? DiagnoseDate
        {
            set { _diagnosedate = value; }
            get { return _diagnosedate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? GasID
        {
            set { _gasid = value; }
            get { return _gasid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ThreeRatioCode
        {
            set { _threeratiocode = value; }
            get { return _threeratiocode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ThreeRatioResult
        {
            set { _threeratioresult = value; }
            get { return _threeratioresult; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DevidCode
        {
            set { _devidcode = value; }
            get { return _devidcode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DevidResult
        {
            set { _devidresult = value; }
            get { return _devidresult; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CubeCode
        {
            set { _cubecode = value; }
            get { return _cubecode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Result
        {
            set { _result = value; }
            get { return _result; }
        }
    }
}