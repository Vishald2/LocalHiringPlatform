using LocalHiringPlatform.Domain.Entities.Experience;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalHiringPlatform.Infrastructure.Data.Seed
{
    public static class IndustryTypeSeedData
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IndustryType>().HasData(

                new IndustryType
                {
                    IndustryTypeId = 1,
                    Code = "IT",
                    Name = "Information Technology",
                    DisplayOrder = 1,
                    IsActive = true
                },

                new IndustryType
                {
                    IndustryTypeId = 2,
                    Code = "MFG",
                    Name = "Manufacturing",
                    DisplayOrder = 2,
                    IsActive = true
                },

                new IndustryType
                {
                    IndustryTypeId = 3,
                    Code = "AUTO",
                    Name = "Automobile",
                    DisplayOrder = 3,
                    IsActive = true
                },

                new IndustryType
                {
                    IndustryTypeId = 4,
                    Code = "RETAIL",
                    Name = "Retail",
                    DisplayOrder = 4,
                    IsActive = true
                },

                new IndustryType
                {
                    IndustryTypeId = 5,
                    Code = "SALES",
                    Name = "Sales",
                    DisplayOrder = 5,
                    IsActive = true
                },

                new IndustryType
                {
                    IndustryTypeId = 6,
                    Code = "BPO",
                    Name = "BPO / Call Center",
                    DisplayOrder = 6,
                    IsActive = true
                },

                new IndustryType
                {
                    IndustryTypeId = 7,
                    Code = "BANK",
                    Name = "Banking & Financial Services",
                    DisplayOrder = 7,
                    IsActive = true
                },

                new IndustryType
                {
                    IndustryTypeId = 8,
                    Code = "HEALTH",
                    Name = "Healthcare",
                    DisplayOrder = 8,
                    IsActive = true
                },

                new IndustryType
                {
                    IndustryTypeId = 9,
                    Code = "EDU",
                    Name = "Education",
                    DisplayOrder = 9,
                    IsActive = true
                },

                new IndustryType
                {
                    IndustryTypeId = 10,
                    Code = "LOG",
                    Name = "Logistics & Supply Chain",
                    DisplayOrder = 10,
                    IsActive = true
                },

                new IndustryType
                {
                    IndustryTypeId = 11,
                    Code = "CONST",
                    Name = "Construction",
                    DisplayOrder = 11,
                    IsActive = true
                },

                new IndustryType
                {
                    IndustryTypeId = 12,
                    Code = "HOTEL",
                    Name = "Hospitality",
                    DisplayOrder = 12,
                    IsActive = true
                },

                new IndustryType
                {
                    IndustryTypeId = 13,
                    Code = "MEDIA",
                    Name = "Media & Entertainment",
                    DisplayOrder = 13,
                    IsActive = true
                },

                new IndustryType
                {
                    IndustryTypeId = 14,
                    Code = "TELECOM",
                    Name = "Telecommunications",
                    DisplayOrder = 14,
                    IsActive = true
                },

                new IndustryType
                {
                    IndustryTypeId = 15,
                    Code = "GOVT",
                    Name = "Government",
                    DisplayOrder = 15,
                    IsActive = true
                },

                new IndustryType
                {
                    IndustryTypeId = 16,
                    Code = "OTHER",
                    Name = "Other",
                    DisplayOrder = 99,
                    IsActive = true
                }
            );
        }
    }
}
