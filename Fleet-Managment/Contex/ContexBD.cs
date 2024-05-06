using Fleet_Managment.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fleet_Managment.Contex
{
    public class ContexBD : DbContext
    {
        private readonly ConnectionBD _connection;

        public ContexBD(ConnectionBD connection)
        {
            _connection = connection;
        }

        public DbSet<TaxiEntity> Taxis { get; set; }
        public DbSet<TrajectoryEntity> Trajectories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaxiEntity>()
                .ToTable("taxis") // Nombre de la tabla en la base de datos
                .Property(t => t.Id)
                .HasColumnName("id"); // Nombre de la columna "Id" en la base de datos

            modelBuilder.Entity<TaxiEntity>()
                .Property(t => t.Plate)
                .HasColumnName("plate"); // Nombre de la columna "Plate" en la base de datos

            // Si necesitas hacer lo mismo para la entidad TrajectoryEntity, aquí es donde lo harías

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connection = _connection.OpenConnection();
            optionsBuilder.UseNpgsql(connection);
            _connection.CloseConnection(connection);
        }
    }
}



//using Microsoft.Extensions.Configuration;
//using Microsoft.EntityFrameworkCore;
//using Fleet_Managment.Entities;

//namespace Fleet_Managment.Contex
//{
//    public class ContexBD : DbContext
//    {
//        private readonly IConfiguration _configuration;

//        public ContexBD(DbContextOptions<ContexBD> options, IConfiguration configuration) : base(options)
//        {
//            _configuration = configuration;
//        }

//        // Definir las DbSet para cada entidad
//        public DbSet<TaxiEntity> Taxis { get; set; }
//        public DbSet<TrajectoryEntity> Trajectories { get; set; }

//        // Sobreescribir el método OnConfiguring para configurar la conexión a la base de datos
//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            var connectionString = _configuration.GetConnectionString("DefaultConnection");
//            optionsBuilder.UseNpgsql(connectionString);
//        }

//        // Sobreescribir el método OnModelCreating si necesitas configuraciones adicionales para el modelo
//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            // Aquí puedes agregar configuraciones adicionales para el modelo, como mapeos de entidades a tablas, restricciones, etc.
//        }
//    }
//}



//using Microsoft.EntityFrameworkCore;
//using Fleet_Managment.Entities;

//namespace Fleet_Managment.Contex
//{
//    public class ContexBD : DbContext
//    {
//        private readonly ConnectionBD _connection;

//        public ContexBD(ConnectionBD connection)
//        {
//            _connection = connection;
//        }

//        public DbSet<TaxiEntity> Taxis { get; set; }
//        public DbSet<TrajectoryEntity> Trajectories { get; set; }

//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            modelBuilder.Entity<TaxiEntity>().ToTable("Taxis"); // Mapea la entidad TaxiEntity a la tabla Taxis
//            modelBuilder.Entity<TrajectoryEntity>().ToTable("Trajectories"); // Mapea la entidad TrajectoryEntity a la tabla Trajectories
//        }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            var connection = _connection.OpenConnection();
//            optionsBuilder.UseNpgsql(connection);
//            _connection.CloseConnection(connection);
//        }
//    }
//}



//using Microsoft.EntityFrameworkCore;
//using Fleet_Managment.Entities;

//namespace Fleet_Managment.Contex
//{
//    public class ContexBD : DbContext
//    {
//        private readonly ConnectionBD _connection;

//        public ContexBD(ConnectionBD connection)
//        {
//            _connection = connection;
//        }

//        public DbSet<TaxiEntity> Taxis { get; set; }
//        public DbSet<TrajectoryEntity> Trajectories { get; set; }

//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            modelBuilder.Entity<TaxiEntity>().ToTable("Taxis"); // Mapea la entidad TaxiEntity a la tabla Taxis
//            modelBuilder.Entity<TrajectoryEntity>().ToTable("Trajectories"); // Mapea la entidad TrajectoryEntity a la tabla Trajectories
//        }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            var connection = _connection.OpenConnection();
//            optionsBuilder.UseNpgsql(connection);
//            _connection.CloseConnection(connection);
//        }
//    }
//}



//using Microsoft.EntityFrameworkCore;
//using static Fleet_Managment.Entities.TaxiEntity;
//using static Fleet_Managment.Entities.TrajectoryEntity;
//using Npgsql;
//using Fleet_Managment.Entities;

//namespace Fleet_Managment.Contex
//{
//    public class ContexBD : DbContext
//    {
//        private readonly ConnectionBD _connection;

//        public ContexBD(ConnectionBD connection)
//        {
//            _connection = connection;
//        }


//        public DbSet<TaxiEntity> Taxis { get; set; }



//        public DbSet<TrajectoryEntity> Trajectories { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            var connection = _connection.OpenConnection();
//            optionsBuilder.UseNpgsql(connection);
//            _connection.CloseConnection(connection);
//        }
//    }
//}
