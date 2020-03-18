using System;
using Ciripa.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ciripa.Data
{
    public static class DateExtensions
    {
        public static void IsDate(this PropertyBuilder<Date> source)
        {
            source.HasConversion(
                e => (DateTime) e,
                e => e
            );
        }
    }
}