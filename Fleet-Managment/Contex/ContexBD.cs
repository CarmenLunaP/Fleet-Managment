using Microsoft.EntityFrameworkCore;
using Fleet_Managment.Entities;
namespace Fleet_Managment.Contex
{
    public class ContexBD : DbContext
    {
        // BDcontext contructor:
        public ContexBD(DbContextOptions<ContexBD> options): 
            base(options) { }

        // property Data Base (CRUD)
        public DbSet<TaxiEntity> Taxis { get; set; }
        public DbSet<TrajectoryEntity> Trajectories { get; set; }

        //Method Data base model
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {   // Taxis
            modelBuilder.Entity<TaxiEntity>()
                .ToTable("taxis") // maping Table taxis
                .Property(t => t.Id) //configure taxi id
                .HasColumnName("id"); // maping id column
            modelBuilder.Entity<TaxiEntity>()
                .Property(t => t.Plate) 
                .HasColumnName("plate");

            //Trayectories
            modelBuilder.Entity<TrajectoryEntity>()
                .ToTable("trajectories")
                .HasKey(t => t.Id); //primary key
            modelBuilder.Entity<TrajectoryEntity>()
                .Property(t => t.TaxiId)
                .HasColumnName("taxi_id");
            modelBuilder.Entity<TrajectoryEntity>()
                .Property(t => t.Date)
                .HasColumnName("date");
            modelBuilder.Entity<TrajectoryEntity>()
                .Property(t => t.Latitude)
                .HasColumnName("latitude");
            modelBuilder.Entity<TrajectoryEntity>()
                .Property(t => t.Longitude)
                .HasColumnName("longitude");


            // Relationship
            modelBuilder.Entity<TrajectoryEntity>()
                .HasOne(t => t.Taxi) // relationship
                .WithMany(t => t.Trajectories) // may relationship
                .HasForeignKey(t => t.TaxiId); //clave foranea, referencia el taxi id entre ambas tablas
            
            // method 
            base.OnModelCreating(modelBuilder);
        }
    }
}

