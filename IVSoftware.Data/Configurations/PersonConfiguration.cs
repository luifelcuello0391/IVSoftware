using IVSoftware.Data.Configurations.Core;
using IVSoftware.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace IVSoftware.Data.Configurations
{
    public class PersonConfiguration : BaseEntityMap<Person, Guid>
    {
        public PersonConfiguration(string TableName, string IdName) : base(TableName, IdName)
        {
        }

        protected override void InternalMap(EntityTypeBuilder<Person> builder)
        {
            builder
                .Property(x => x.IdentificationNumber)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(255);

            builder
                .HasAlternateKey(a => a.IdentificationNumber);

            builder
                .Property(x => x.FirstName)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(50);

            builder
                .Property(x => x.MiddleName)
                .IsRequired(false)
                .IsUnicode()
                .HasMaxLength(50);

            builder
                .Property(x => x.FirstLastName)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(50);

            builder
                .Property(x => x.SecondLastName)
                .IsRequired(false)
                .IsUnicode()
                .HasMaxLength(50);

            builder
                .Property(x => x.GenderId)
                .IsRequired(false);

            builder
                .HasOne(g => g.Gender)
                .WithMany(p => p.Persons)
                .HasForeignKey(g => g.GenderId)
                .OnDelete(DeleteBehavior.SetNull);

            builder
                .Property(x => x.IsColombian)
                .IsRequired();

            builder
                .Property(x => x.MilitaryCardType)
                .IsRequired(false)
                .IsUnicode()
                .HasMaxLength(2);

            builder
                .Property(x => x.MilitaryCardId)
                .IsRequired(false)
                .IsUnicode()
                .HasMaxLength(20);

            builder
                .Property(x => x.MilitaryDistrict)
                .IsRequired(false)
                .IsUnicode()
                .HasMaxLength(250);

            builder
                .Property(x => x.Birthdate)
                .IsRequired(false);

            builder
                .Property(x => x.Address)
                .IsRequired(false)
                .IsUnicode()
                .HasMaxLength(250);

            builder
                .Property(x => x.PhoneNumber)
                .IsRequired(false)
                .IsUnicode()
                .HasMaxLength(30);

            builder
                .Property(x => x.Email)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(250);

            builder
                .Property(x => x.Photo)
                .IsRequired(false)
                .IsUnicode()
                .HasMaxLength(1000);

            builder
                .Property(x => x.CompletionDateTime)
                .IsRequired(false);

            builder
                .Property(x => x.CorrespondenceMunicipalityId)
                .IsRequired(false);

            builder
                .HasOne(g => g.CorrespondenceMunicipality)
                .WithMany(p => p.PeopleCorrespondence)
                .HasForeignKey(g => g.CorrespondenceMunicipalityId);

            builder
                .Property(x => x.BirthMunicipalityId)
                .IsRequired(false);

            builder
                .HasOne(g => g.BirthMunicipality)
                .WithMany(p => p.PeopleBirth)
                .HasForeignKey(g => g.BirthMunicipalityId);

            builder
                .Property(x => x.CorrespondenceCountryId)
                .IsRequired(false);

            builder
                .HasOne(g => g.CorrespondenceCountry)
                .WithMany(p => p.PeopleCorrespondence)
                .HasForeignKey(g => g.CorrespondenceCountryId);

            builder
                .Property(x => x.BirthCountryId)
                .IsRequired(false);

            builder
                .HasOne(g => g.BirthCountry)
                .WithMany(p => p.PeopleBirth)
                .HasForeignKey(g => g.BirthCountryId);

            builder
                .Property(x => x.Skills)
                .IsRequired(false)
                .IsUnicode()
                .HasMaxLength(2500);

            builder
                .Property(x => x.ArlId)
                .IsRequired(false);

            builder
                .HasOne(g => g.Arl)
                .WithMany(p => p.People)
                .HasForeignKey(g => g.ArlId)
                .OnDelete(DeleteBehavior.SetNull);

            builder
                .Property(x => x.EpsId)
                .IsRequired(false);

            builder
                .HasOne(g => g.Eps)
                .WithMany(p => p.People)
                .HasForeignKey(g => g.EpsId)
                .OnDelete(DeleteBehavior.SetNull);

            builder
                .Property(x => x.IdentificationTypeId)
                .IsRequired();

            builder
                .HasOne(g => g.IdentificationType)
                .WithMany(p => p.People)
                .HasForeignKey(g => g.IdentificationTypeId);

            builder
                .Property(x => x.BloodTypeId)
                .IsRequired(false);

            builder
                .HasOne(g => g.BloodType)
                .WithMany(p => p.People)
                .HasForeignKey(g => g.BloodTypeId)
                .OnDelete(DeleteBehavior.SetNull);

            builder
                .Property(x => x.ContractTypeId)
                .IsRequired();

            builder
                .HasOne(g => g.ContractType)
                .WithMany(p => p.People)
                .HasForeignKey(g => g.ContractTypeId);

            builder
                .Property(x => x.UserId)
                .IsRequired(false);

            builder
                .HasOne(g => g.User)
                .WithOne(p => p.Person);

            builder
                .Property(x => x.RecordStatus)
                .IsRequired()
                .HasDefaultValue(true);
        }
    }
}