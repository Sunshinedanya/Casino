//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Casino.DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class Session
    {
        public int id { get; set; }
        public Nullable<int> UserId { get; set; }
        public Nullable<int> GameId { get; set; }
    
        public virtual Game Game { get; set; }
        public virtual User User { get; set; }
    }
}
