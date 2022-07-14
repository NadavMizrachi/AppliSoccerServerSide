using AppliSoccerDatabasing.DBModels;
using AppliSoccerObjects.ActionResults.EventsActions;
using AppliSoccerObjects.Modeling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppliSoccerDatabasing
{
    class DBModelConverter
    {
        public static Team ConvertTeam(TeamDBModel teamDBModel)
        {
            Team team = new Team(teamDBModel.CountryName, teamDBModel.Name);
            team.ExtMainLeagueId = teamDBModel.LeagueName;
            team.IsRegistered = teamDBModel.IsRegistred;
            team.ExtMainLeagueId = teamDBModel.ExtMainLeagueId;
            team.ExtSeconderyCompetitionsIds = teamDBModel.ExtSeconderyCompetitionsIds;
            team.ExtTeamId = teamDBModel.ExtTeamId;
            team.LogoUrl = teamDBModel.LogoUrl;
            return team;
        }

        public static List<LeagueDBModel> ConvertLeagues(List<League> leagues)
        {
            List<LeagueDBModel> output = new List<LeagueDBModel>();
            foreach (var league in leagues)
            {
                output.Add(ConvertLeague(league));
            }
            return output;
        }

        public static LeagueDBModel ConvertLeague(League league)
        {
            if (league == null) return null;

            return new LeagueDBModel
            {
                Country = league.Country,
                Id = league.ID,
                LogoUrl = league.LogoUrl,
                Name = league.Name,
                Table = ConvertLeagueTable(league.Table)
            };
        }

        public static League ConvertLeague(LeagueDBModel league)
        {
            if (league == null) return null;

            return new League
            {
                Country = league.Country,
                ID = league.Id,
                LogoUrl = league.LogoUrl,
                Name = league.Name,
                Table = ConvertLeagueTable(league.Table)
            };
        }

        public static LeagueTableDBModel ConvertLeagueTable(LeagueTable table)
        {
            if (table == null) return null;
            return new LeagueTableDBModel
            {
                SubTables = ConvertSubTables(table.SubTables)
            };
        }

        public static LeagueTable ConvertLeagueTable(LeagueTableDBModel table)
        {
            if (table == null) return null;
            return new LeagueTable
            {
                SubTables = ConvertSubTables(table.SubTables)
            };
        }

        private static List<SubTableDBModel> ConvertSubTables(List<SubTable> subTables)
        {
            if (subTables == null) return null;
            List<SubTableDBModel> subTablesModels = new List<SubTableDBModel>();
            foreach (var subTable in subTables)
            {
                subTablesModels.Add(ConvertSubTable(subTable));
            }
            return subTablesModels;
        }

        private static List<SubTable> ConvertSubTables(List<SubTableDBModel> subTables)
        {
            if (subTables == null) return null;
            List<SubTable> subTablesModels = new List<SubTable>();
            foreach (var subTable in subTables)
            {
                subTablesModels.Add(ConvertSubTable(subTable));
            }
            return subTablesModels;
        }

        private static SubTableDBModel ConvertSubTable(SubTable subTable)
        {
            if (subTable == null) return null;
            return new SubTableDBModel
            {
                Description = subTable.Description,
                Name = subTable.Name,
                Rows = ConvertTableRows(subTable.Rows)
            };
        }

        private static SubTable ConvertSubTable(SubTableDBModel subTable)
        {
            if (subTable == null) return null;
            return new SubTable
            {
                Description = subTable.Description,
                Name = subTable.Name,
                Rows = ConvertTableRows(subTable.Rows)
            };
        }

        private static List<TableRowDBModel> ConvertTableRows(List<TableRow> rows)
        {
            if(rows == null) return null;
            List<TableRowDBModel> output = new List<TableRowDBModel>();
            foreach (var row in rows)
            {
                output.Add(ConvertTableRow(row));
            }
            return output;
        }

        private static List<TableRow> ConvertTableRows(List<TableRowDBModel> rows)
        {
            if (rows == null) return null;
            List<TableRow> output = new List<TableRow>();
            foreach (var row in rows)
            {
                output.Add(ConvertTableRow(row));
            }
            return output;
        }

        private static TableRowDBModel ConvertTableRow(TableRow row)
        {
            if( row == null) return null;
            return new TableRowDBModel
            {
                Form = row.Form,
                GoalsDiff = row.GoalsDiff,
                Points = row.Points,
                Rank = row.Rank,
                TeamId = row.TeamId,
                TeamName = row.TeamName,
                TeamLogoUrl = row.LogoURL
            };
        }

        private static TableRow ConvertTableRow(TableRowDBModel row)
        {
            if (row == null) return null;
            return new TableRow
            {
                Form = row.Form,
                GoalsDiff = row.GoalsDiff,
                Points = row.Points,
                Rank = row.Rank,
                TeamId = row.TeamId,
                TeamName = row.TeamName,
                LogoURL = row.TeamLogoUrl
            };
        }

        public static TeamDBModel ConvertTeam(Team team)
        {
            return new TeamDBModel()
            {
                Id = team.Id,
                Name = team.Name,
                LogoUrl = team.LogoUrl,
                CountryName = team.CountryName,
                LeagueName = team.ExtMainLeagueId,
                IsRegistred = team.IsRegistered,
                ExtTeamId = team.ExtTeamId,
                ExtMainLeagueId = team.ExtMainLeagueId,
                ExtSeconderyCompetitionsIds = team.ExtSeconderyCompetitionsIds
            };
        }

        public static EventDetailsDBModel ConvertEvent(EventDetails eventDetails)
        {
            if (eventDetails == null) return null;

            return new EventDetailsDBModel
            {
                AdditionalInfo = eventDetails.AdditionalInfo,
                Title = eventDetails.Title,
                CreatorId = eventDetails.CreatorId,
                Description = eventDetails.Description,
                EndTime = eventDetails.EndTime,
                ParticipantsIds = eventDetails.ParticipantsIds,
                ParticipantsRoles = eventDetails.ParticipantsRoles?.Select(dbRoleEnum => ConvertRoleEnum(dbRoleEnum)).ToList(),
                Place = ConvertPlace(eventDetails.Place),
                StartTime = eventDetails.StartTime,
                TeamId = eventDetails.TeamId,
                Type = ConvertEventType(eventDetails.Type)
            };
        }

        public static EventDetails ConvertEvent(EventDetailsDBModel eventDetailsModel)
        {
            if (eventDetailsModel == null) return null;

            return new EventDetails
            {
                Id = eventDetailsModel.Id,
                Title = eventDetailsModel.Title,
                AdditionalInfo = eventDetailsModel.AdditionalInfo,
                CreatorId = eventDetailsModel.CreatorId,
                Description = eventDetailsModel.Description,
                EndTime = eventDetailsModel.EndTime,
                ParticipantsIds = eventDetailsModel.ParticipantsIds,
                ParticipantsRoles = eventDetailsModel.ParticipantsRoles?.Select(dbRoleEnum => ConvertRoleEnum(dbRoleEnum)).ToList(),
                Place = ConvertPlace(eventDetailsModel.Place),
                StartTime = eventDetailsModel.StartTime,
                TeamId = eventDetailsModel.TeamId,
                Type = ConvertEventType(eventDetailsModel.Type)
            };
        }

        private static DBEnums.EventType ConvertEventType(EventType type)
        {
            switch (type)
            {
                case EventType.Game:
                    return DBEnums.EventType.Game;
                case EventType.Training:
                    return DBEnums.EventType.Training;
                case EventType.Volunteering:
                    return DBEnums.EventType.Volunteering;
                case EventType.Forging:
                    return DBEnums.EventType.Forging;
                case EventType.Medicine:
                    return DBEnums.EventType.Medicine;
                case EventType.Other:
                    return DBEnums.EventType.Other;
                default:
                    return DBEnums.EventType.Other;
            }
        }

        private static EventType ConvertEventType(DBEnums.EventType type)
        {
            switch (type)
            {
                case DBEnums.EventType.Game:
                    return EventType.Game;
                case DBEnums.EventType.Training:
                    return EventType.Training;
                case DBEnums.EventType.Volunteering:
                    return EventType.Volunteering;
                case DBEnums.EventType.Forging:
                    return EventType.Forging;
                case DBEnums.EventType.Medicine:
                    return EventType.Medicine;
                case DBEnums.EventType.Other:
                    return EventType.Other;
                default:
                    return EventType.Other;
            }
        }

        public static List<EventDetails> ConvertEvents(List<EventDetailsDBModel> eventModels)
        {
            List<EventDetails> outputList = new List<EventDetails>();
            if (eventModels == null) return outputList;
            eventModels.ForEach(model => outputList.Add(ConvertEvent(model)));
            return outputList;
        }

        private static PlaceDBModel ConvertPlace(Place place)
        {
            if (place == null) return null;

            return new PlaceDBModel
            {
                Description = place.Description,
                Name = place.Name,
                Position = ConvertPoistion(place.Position)
            };
        }

        private static Place ConvertPlace(PlaceDBModel placeModel)
        {
            if (placeModel == null) return null;

            return new Place
            {
                Description = placeModel.Description,
                Name = placeModel.Name,
                Position = ConvertPoistion(placeModel.Position)
            };
        }

        private static PositionDBModel ConvertPoistion(Position position)
        {
            if (position == null) return null;

            return new PositionDBModel
            {
                Latitude = position.Latitude,
                Longitude = position.Longitude
            };
        }

        private static Position ConvertPoistion(PositionDBModel positionModel)
        {
            if (positionModel == null) return null;

            return new Position
            {
                Latitude = positionModel.Latitude,
                Longitude = positionModel.Longitude
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

        public static List<OrderReceiving> ConvertOrderReceivingList(List<OrderReceivingDBModel> orderReceivings)
        {
            List<OrderReceiving> output = new List<OrderReceiving>();
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
            if(userDBModel == null)
            {
                return null;
            }
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
                Id = order.ID,
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
