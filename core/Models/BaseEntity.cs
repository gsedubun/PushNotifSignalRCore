using System;

namespace core.Models
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            this._Createdon = DateTime.Now;
            this._ModifiedOn= DateTime.Now;
        }
        public Int64 Id
        {
            get;
            set;
        }
        private DateTime _Createdon;
        public DateTime CreatedOn
        {
            get { return _Createdon; }
            set
            {
                if (value == null)
                    _Createdon = DateTime.Now;
                else
                    _Createdon = value;
            }
        }
        private DateTime _ModifiedOn;
        public DateTime ModifiedOn
        {
            get { return _ModifiedOn; }
            set
            {
                if (value == null)
                    _ModifiedOn = DateTime.Now;
                else
                    _ModifiedOn = value;
            }
        }
        public Int64 CreatedBy
        {
            get;
            set;
        }
        public Int64 ModifiedBy
        {
            get;
            set;
        }
        //public string IPAddress
        //{
        //    get;
        //    set;
        //}
    }

}
