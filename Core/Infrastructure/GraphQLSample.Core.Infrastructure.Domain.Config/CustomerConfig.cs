using GraphQLSample.Core.Infrastructure.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GraphQLSample.Core.Infrastructure.Domain.Config
{
    public class CustomerConfig : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(x => x.Id);
            
            builder
                .Property(x => x.Id)
                .HasColumnName("CustomerID");

            builder
                .Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(128);

            builder
                .Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(128);

            builder
                .HasMany(x => x.Addresses)
                .WithOne(x => x.Customer)
                .HasForeignKey(x => x.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
