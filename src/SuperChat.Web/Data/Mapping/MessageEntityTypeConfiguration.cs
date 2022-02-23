using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SuperChat.Web.Entities;

namespace SuperChat.Web.Data.Mapping
{
    public class MessageEntityTypeConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder
                .HasKey(c => c.Id);

            builder
                .Property(c => c.Id)
                .IsRequired();

            builder
                .Property(b => b.Text)
                .IsRequired();

            builder
                .Property(b => b.Date)
                .IsRequired();

            builder
                .Property(b => b.UserName)
                .IsRequired();

            builder
                .HasOne(b => b.Group)
                .WithMany()
                .HasForeignKey(x => x.GroupId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
