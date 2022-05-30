using AppliSoccerDatabasing.DBModels;
using AppliSoccerObjects.Modeling;
using System;
using System.Collections.Generic;
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
                AdditionalInfo = teamMember.AdditionalInfo
            };
        }

        public static TeamMember ConvertTeamMember(TeamMemberDBModel teamMemberDBModel)
        {
            return new TeamMember
            {
                BirthDate = teamMemberDBModel.BirthDate,
                AdditionalInfo = teamMemberDBModel.AdditionalInfo,
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
    }
}
