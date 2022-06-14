using AppliSoccerDatabasing.DBModels;
using AppliSoccerObjects.Modeling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppliSoccerDatabasing
{
    class DBModelConverter
    {
        public static Team ConvertTeam(TeamDBModel teamDBModel)
        {
            Team team = new Team(teamDBModel.CountryName, teamDBModel.Name);
            team.LeagueName = teamDBModel.LeagueName;
            team.IsRegistered = teamDBModel.IsRegistred;
            return team;
        }

        public static TeamDBModel ConvertTeam(Team team)
        {
            return new TeamDBModel()
            {
                Id = team.Id,
                Name = team.Name,
                CountryName = team.CountryName,
                LeagueName = team.LeagueName,
                IsRegistred = team.IsRegistered
            };
        }
        public static List<Team> ConvertTeams(List<TeamDBModel> teamDBModels)
        {
            List<Team> teams = new List<Team>();
            foreach (var teamDBModel in teamDBModels)
            {
                teams.Add(ConvertTeam(teamDBModel));
            }
            return teams;
        }

        public static UserDBModel ConvertUser(User user)
        {
            return new UserDBModel()
            {
                UserName = user.Username,
                Password = user.Password,
                IsAdmin = user.IsAdmin,
                TeamMember = ConvertTeamMember(user.TeamMember)
            };
        }

        public static List<Order> ConvertOrders(List<OrderDBModel> orderDBModels)
        {
            List<Order> output = new List<Order>();
            foreach (var orderDbModel in orderDBModels)
            {
                output.Add(ConvertOrder(orderDbModel));
            }
            return output;
        }

        public static List<OrderDBModel> ConvertOrders(List<Order> orders)
        {
            List<OrderDBModel> output = new List<OrderDBModel>();
            foreach (var order in orders)
            {
                output.Add(ConvertOrder(order));
            }
            return output;
        }

        public static List<OrderReceivingDBModel> ConvertOrderReceivingList(List<OrderReceiving> orderReceivings)
        {
            List<OrderReceivingDBModel> output = new List<OrderReceivingDBModel>();
            foreach (var orderReceiving in orderReceivings)
            {
                output.Add(ConvertOrderReceiving(orderReceiving));
            }
            return output;
        }

        public static OrderReceivingDBModel ConvertOrderReceiving(OrderReceiving orderReceiving)
        {
            return new OrderReceivingDBModel()
            {
                ReceiverId = orderReceiving.ReceiverId,
                WasRead = orderReceiving.WasRead,
                Order = ConvertOrder(orderReceiving.Order)
            };
        }

        public static OrderReceiving ConvertOrderReceiving(OrderReceivingDBModel orderReceiving)
        {
            return new OrderReceiving()
            {
                ReceiverId = orderReceiving.ReceiverId,
                WasRead = orderReceiving.WasRead,
                Order = ConvertOrder(orderReceiving.Order)
            };
        }

        public static User ConvertUser(UserDBModel userDBModel)
        {
            return new User()
            {
                Username = userDBModel.UserName,
                Password = userDBModel.Password,
                IsAdmin = userDBModel.IsAdmin,
                TeamMember = ConvertTeamMember(userDBModel.TeamMember)
            };
        }


        public static List<TeamMember> ConvertTeamMembers(List<TeamMemberDBModel> teamMemberDBModelList)
        {
            List<TeamMember> teamMembersResult = new List<TeamMember>();
            foreach (var teamMemberDBModel in teamMemberDBModelList)
            {
                teamMembersResult.Add(ConvertTeamMember(teamMemberDBModel));
            }
            return teamMembersResult;
        }
        public static TeamMemberDBModel ConvertTeamMember(TeamMember teamMember)
        {
            return new TeamMemberDBModel
            {
                BirthDate = teamMember.BirthDate,
                FirstName = teamMember.FirstName,
                LastName = teamMember.LastName,
                Description = teamMember.Description,
                ID = teamMember.ID,
                MemberType = ExtractMemberType(teamMember),
                PhoneNumber = teamMember.PhoneNumber,
                TeamId = teamMember.TeamId,
                TeamName = teamMember.TeamName,
                AdditionalInfo = ConvertAdditionalInfo(teamMember.MemberType, teamMember.AdditionalInfo)
            };
        }


        public static AdditionalInfoDBModel ConvertAdditionalInfo(MemberType memberType, Object additionalInfo)
        {
            switch (memberType)
            {
                case MemberType.Admin:
                    {
                        return null;
                    }
                case MemberType.Staff:
                    {
                        var staffAditionalInfo = (StaffAdditionalInfo)additionalInfo;
                        return new StaffAdditionalInfoDBModel()
                        {
                            IsCoach = staffAditionalInfo.IsCoach,
                            ManagedRoles = staffAditionalInfo.ManagedRoles.Select(dbRoleEnum => ConvertRoleEnum(dbRoleEnum)).ToList()
                        };
                    }
                case MemberType.Player:
                    {
                        var playerAdditionalInfo = (PlayerAdditionalInfo)additionalInfo;
                        return new PlayerAdditionalInfoDBModel()
                        {
                            Number = playerAdditionalInfo.Number,
                            Role = ConvertRoleEnum(playerAdditionalInfo.Role)
                        };
                    }

                default: return null;
            }
        }

        private static DBEnums.Role ConvertRoleEnum(Role RoleEnum)
        {
            switch (RoleEnum)
            {
                case Role.Attacker:
                    {
                        return DBEnums.Role.Attacker;
                    }
                case Role.Defender:
                    {
                        return DBEnums.Role.Defender;
                    }
                case Role.GoalKeeper:
                    {
                        return DBEnums.Role.GoalKeeper;
                    }
                case Role.Midfielder:
                    {
                        return DBEnums.Role.Midfielder;
                    }
                default: return DBEnums.Role.Defender;
            }
        }

        public static TeamMember ConvertTeamMember(TeamMemberDBModel teamMemberDBModel)
        {
            return new TeamMember
            {
                BirthDate = teamMemberDBModel.BirthDate,
                AdditionalInfo = ConvertAdditionalInfo(teamMemberDBModel.MemberType, teamMemberDBModel.AdditionalInfo),
                Description = teamMemberDBModel.Description,
                FirstName = teamMemberDBModel.FirstName,
                ID = teamMemberDBModel.ID,
                LastName = teamMemberDBModel.LastName,
                MemberType = ExtractMemberType(teamMemberDBModel),
                PhoneNumber = teamMemberDBModel.PhoneNumber,
                TeamId = teamMemberDBModel.TeamId,
                TeamName = teamMemberDBModel.TeamName
            };
        }

        public static Object ConvertAdditionalInfo(DBEnums.MemberType memberType , Object additionalInfoDBModel)
        {
            switch (memberType)
            {
                case DBEnums.MemberType.Admin:
                    {
                        return null;
                    }
                case DBEnums.MemberType.Staff:
                    {
                        var staffAditionalInfoDBModel = (StaffAdditionalInfoDBModel)additionalInfoDBModel;
                        return new StaffAdditionalInfo()
                        {
                            IsCoach = staffAditionalInfoDBModel.IsCoach,
                            ManagedRoles = staffAditionalInfoDBModel.ManagedRoles.Select(dbRoleEnum => ConvertRoleEnum(dbRoleEnum)).ToList()
                        };
                    }
                case DBEnums.MemberType.Player:
                    {
                        var playerAdditionalInfoDBModel = (PlayerAdditionalInfoDBModel)additionalInfoDBModel;
                        return new PlayerAdditionalInfo()
                        {
                            Number = playerAdditionalInfoDBModel.Number,
                            Role = ConvertRoleEnum(playerAdditionalInfoDBModel.Role)
                        };
                    }

                default: return null;
            }
        }

        private static Role ConvertRoleEnum(DBEnums.Role dbRoleEnum)
        {
            switch (dbRoleEnum)
            {
                case DBEnums.Role.Attacker:
                    {
                        return Role.Attacker;
                    }
                case DBEnums.Role.Defender:
                    {
                        return Role.Defender;
                    }
                case DBEnums.Role.GoalKeeper:
                    {
                        return Role.GoalKeeper;
                    }
                case DBEnums.Role.Midfielder:
                    {
                        return Role.Midfielder;
                    }
                default: return Role.Defender;
            }
        }

        private static DBEnums.MemberType ExtractMemberType(TeamMember teamMember)
        {
            switch(teamMember.MemberType)
            {
                case MemberType.Admin:
                {
                    return DBEnums.MemberType.Admin;
                }
                case MemberType.Staff:
                {
                    return DBEnums.MemberType.Staff;
                }
                case MemberType.Player:
                {
                    return DBEnums.MemberType.Player;
                }
                
                default: return DBEnums.MemberType.Player;
            }          
        }

        private static MemberType ExtractMemberType(TeamMemberDBModel teamModelDBModel)
        {
            switch (teamModelDBModel.MemberType)
            {
                case DBEnums.MemberType.Admin:
                    {
                        return MemberType.Admin;
                    }
                case DBEnums.MemberType.Staff:
                    {
                        return MemberType.Staff;
                    }
                case DBEnums.MemberType.Player:
                    {
                        return MemberType.Player;
                    }

                default: return MemberType.Player;
            }
        }


        public static OrderDBModel ConvertOrder(Order order)
        {
            return new OrderDBModel()
            {
                Content = order.Content,
                GameId = order.GameId,
                MemberIdsReceivers = order.MemberIdsReceivers,
                MembersIdsAlreadyRead = order.MembersIdsAlreadyRead,
                RolesReceivers = order.RolesReceivers.Select(roleEnum => ConvertRoleEnum(roleEnum)).ToList(),
                SenderId = order.SenderId,
                SendingDate = order.SendingDate,
                TeamId = order.TeamId,
                Title = order.Title
            };
        }

        public static Order ConvertOrder(OrderDBModel order)
        {
            return new Order()
            {
                ID = order.Id,
                Content = order.Content,
                GameId = order.GameId,
                MemberIdsReceivers = order.MemberIdsReceivers,
                MembersIdsAlreadyRead = order.MembersIdsAlreadyRead,
                RolesReceivers = order.RolesReceivers.Select(roleEnum => ConvertRoleEnum(roleEnum)).ToList(),
                SenderId = order.SenderId,
                SendingDate = order.SendingDate,
                TeamId = order.TeamId,
                Title = order.Title
            };
        }

    }
}
