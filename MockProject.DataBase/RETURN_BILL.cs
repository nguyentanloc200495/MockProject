//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MockProject.DataBase
{
    using System;
    using System.Collections.Generic;
    
    public partial class RETURN_BILL
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RETURN_BILL()
        {
            this.RETURNBILL_DETAIL = new HashSet<RETURNBILL_DETAIL>();
        }
    
        public int ID { get; set; }
        public Nullable<int> Bill_ID { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<int> CreateBy { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public Nullable<int> UpdateBy { get; set; }
    
        public virtual BILL BILL { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RETURNBILL_DETAIL> RETURNBILL_DETAIL { get; set; }
        public virtual USER USER { get; set; }
        public virtual USER USER1 { get; set; }
    }
}