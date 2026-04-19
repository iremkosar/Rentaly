using Microsoft.EntityFrameworkCore;
using Rentaly.EntityLayer.Entities;


namespace Rentaly.DataAccessLayer.Concrete
{
    public class RentalyContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-UGIR0F4\\SQLEXPRESS;Database=RentalyDb;Trusted_Connection=True;");
        }

        public DbSet<Branch> Branches { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<CarModel> CarModels { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<Adventure> Adventures { get; set; }
        public DbSet<Testimonial> Testimonials { get; set; }
        public DbSet<Statistic> Statistics { get; set; }
        public DbSet<Faq> Faqs { get; set; }
        public DbSet<HowItWork> HowItWorks {  get; set; }
       public DbSet<Reservation> Reservations { get; set; }

    }
}

