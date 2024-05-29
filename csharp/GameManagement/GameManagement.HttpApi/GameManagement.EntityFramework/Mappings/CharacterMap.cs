using GameMenagement.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameManagement.EntityFramework.Mappings
{
    public class CharacterMap : IEntityTypeConfiguration<Character>
    {
        public void Configure(EntityTypeBuilder<Character> builder)
        {
            builder.Property(character => character.Nickname).HasMaxLength(20);
            builder.Property(character => character.classes).HasMaxLength(20);

            builder.HasIndex(character => character.Nickname).IsUnique();
            builder.HasOne(c => c.Player)
                .WithMany(p=>p.Characters)
                .HasForeignKey(c=>c.PlayerId);
        }
    }
}
