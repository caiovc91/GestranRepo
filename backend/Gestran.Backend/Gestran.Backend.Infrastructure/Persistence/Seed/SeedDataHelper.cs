using Gestran.Backend.Application.Common.Helpers;
using Gestran.Backend.Domain.Entities;
using Gestran.Backend.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestran.Backend.Infrastructure.Persistence.Seed
{
    public static class SeedDataHelper
    {
        public static List<User> GenerateTestUsers()
        {
            var users = new List<User>();

            // 3 Executores
            for (int i = 1; i <= 3; i++)
            {
                users.Add(new User
                {
                    Id = Guid.NewGuid(),
                    Name = $"Executor{i}",
                    AccessHashCode = AuthHelper.GenerateHash($"exec@{i}123"),
                    IsAccessActive = true,
                    Role = UserRole.Executor
                });
            }

            // 2 Aprovadores
            for (int i = 1; i <= 2; i++)
            {
                users.Add(new User
                {
                    Id = Guid.NewGuid(),
                    Name = $"Supervisor{i}",
                    AccessHashCode = AuthHelper.GenerateHash($"sup@{i}123"),
                    IsAccessActive = true,
                    Role = UserRole.Supervisor
                });
            }

            return users;
        }

        public static List<CheckListItemType> GenerateTestCheckListItemTypes()
        {
            return new List<CheckListItemType>
            {
                new CheckListItemType
                {
                    Id = Guid.NewGuid(),
                    TypeName = "Freios",
                    Description = "Verificar condição dos freios",
                    IsEnabled = true
                },
                new CheckListItemType
                {
                    Id = Guid.NewGuid(),
                    TypeName = "Lanterna Traseira",
                    Description = "Verificar funcionamento da lanterna traseira",
                    IsEnabled = true
                },
                new CheckListItemType
                {
                    Id = Guid.NewGuid(),
                    TypeName = "Lanterna Dianteira",
                    Description = "Verificar funcionamento da lanterna dianteira",
                    IsEnabled = true
                },
                new CheckListItemType
                {
                    Id = Guid.NewGuid(),
                    TypeName = "Para-brisa",
                    Description = "Verificar estado do para-brisa",
                    IsEnabled = true
                },
                new CheckListItemType
                {
                    Id = Guid.NewGuid(),
                    TypeName = "Óleo",
                    Description = "Verificar nível e qualidade do óleo",
                    IsEnabled = true
                },
                new CheckListItemType
                {
                    Id = Guid.NewGuid(),
                    TypeName = "Filtro Motor",
                    Description = "Verificar condição do filtro de motor",
                    IsEnabled = true
                }
            };
        }
    }

}

