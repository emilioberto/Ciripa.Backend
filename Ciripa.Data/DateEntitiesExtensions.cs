using Ciripa.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ciripa.Data
{
    public static class DateEntitiesExtensions
    {
        public static EntityTypeBuilder<T> ConfigureDateField<T>(this EntityTypeBuilder<T> source)
            where T : class, IDateEntity
        {
            source.Property(e => e.Date).IsDate();
            return source;
        }
    }
    
    
    public sealed class DateConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : class, IDateEntity
    {
        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.ConfigureDateField();
        }
    }
}