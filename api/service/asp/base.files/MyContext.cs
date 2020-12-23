using Microsoft.EntityFrameworkCore;

namespace Models
{
    public partial class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options) { }

        /*{dbSets}*/

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*{entities}*/


            modelBuilder
                /*{seedClass}*/
                ;
        }


        // partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
