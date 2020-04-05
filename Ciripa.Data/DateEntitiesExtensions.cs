using Ciripa.Data.Entities;
using Ciripa.Data.Interfaces;
using Ciripa.Domain;
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
        
        public static EntityTypeBuilder ConfigureKidDateFields(this EntityTypeBuilder<Kid> source)
        {
            source.Property(e => e.From).IsDate();
            source.Property(e => e.To).IsDate();
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
    
    public sealed class KidDateConfiguration
    {
        public void Configure(EntityTypeBuilder<Kid> builder)
        {
            builder.ConfigureKidDateFields();
        }
    }
}