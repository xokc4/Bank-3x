//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Bank_3x
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Entities1 : DbContext
    {
        public Entities1()
            : base("name=Entities1")
        {
            
            try
            {
                Database.Connection.ConnectionString += SqlConn.SqlConn.SqlIf();//строка подключения
            }
#pragma warning disable CS0168 // Переменная "ex" объявлена, но ни разу не использована.
            catch (Exception ex)
#pragma warning restore CS0168 // Переменная "ex" объявлена, но ни разу не использована.
            {
                
                SqlParametrs sqlParametrs = new SqlParametrs();
                sqlParametrs.MessageException();
                sqlParametrs.Show();
                
            }
           
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<People> People { get; set; }
        public virtual DbSet<TypePeople> TypePeople { get; set; }
    }
}
