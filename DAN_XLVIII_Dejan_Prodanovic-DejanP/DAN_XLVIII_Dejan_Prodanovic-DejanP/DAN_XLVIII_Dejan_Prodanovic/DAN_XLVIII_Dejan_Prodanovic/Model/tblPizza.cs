//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAN_XLVIII_Dejan_Prodanovic.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblPizza
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblPizza()
        {
            this.tblPizzaOrders = new HashSet<tblPizzaOrder>();
        }
    
        public int ID { get; set; }
        public string PizzaType { get; set; }
        public Nullable<decimal> Price { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblPizzaOrder> tblPizzaOrders { get; set; }
    }
}