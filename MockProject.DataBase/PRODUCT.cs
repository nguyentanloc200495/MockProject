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
    
    public partial class PRODUCT
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PRODUCT()
        {
            this.BILL_DETAIL = new HashSet<BILL_DETAIL>();
            this.RETURNBILL_DETAIL = new HashSet<RETURNBILL_DETAIL>();
            this.WAREHOUSE_DETAIL = new HashSet<WAREHOUSE_DETAIL>();
            this.WAREHOUSETRANSACTION_DETAIL = new HashSet<WAREHOUSETRANSACTION_DETAIL>();
        }
    
        public int ID { get; set; }
        public Nullable<int> ProductType_ID { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string Unit { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<decimal> Amount { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BILL_DETAIL> BILL_DETAIL { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RETURNBILL_DETAIL> RETURNBILL_DETAIL { get; set; }
        public virtual PRODUCT_TYPE PRODUCT_TYPE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WAREHOUSE_DETAIL> WAREHOUSE_DETAIL { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WAREHOUSETRANSACTION_DETAIL> WAREHOUSETRANSACTION_DETAIL { get; set; }
    }
}
