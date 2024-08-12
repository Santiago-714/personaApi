using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using personaApi.Model; //Agregar para que detecte el modelo Person

namespace personaApi.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) //Creado con ctor + TAB
        : base(options) //Pasa las opciones a la clase base, en este caso DbContext
        {
            try{
            
            var dbCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;

            if(dbCreator != null)
            {
                if(!dbCreator.CanConnect())
                    dbCreator.Create();
                if(!dbCreator.HasTables())
                    dbCreator.CreateTables();
            }

            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        public DbSet<Person> Persons { get; set; }


    }
}