using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;

namespace net6_odata;

public class SampleContext : DbContext
{
    public DbSet<SampleEntity> SampleEntities { get; set; }

    public SampleContext(DbContextOptions<SampleContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SampleEntity>()
            .ToTable("SampleEntities");

        modelBuilder
            .Entity<SampleEntity>()
            .Property(e => e.SerializedProperty)
            .HasConversion(
                v => JsonConvert.SerializeObject(v),
                v => JsonConvert.DeserializeObject<List<string>>(v),
                new ValueComparer<List<string>>(
                    (c1, c2) => c1.SequenceEqual(c2),
                    c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                    c => c.ToList()));
    }
}

public class SampleEntity
{
    [Key]
    public int Id { get; set; }
    public List<string> SerializedProperty { get; set; }
}