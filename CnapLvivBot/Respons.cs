//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CnapLvivBot
{
    using System;
    using System.Collections.Generic;
    
    public partial class Respons
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public Nullable<int> IntentId { get; set; }
    
        public virtual Intent Intent { get; set; }
    }
}
