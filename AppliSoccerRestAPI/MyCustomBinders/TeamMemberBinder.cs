using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AppliSoccerObjects.Modeling;

namespace AppliSoccerRestAPI.MyCustomBinders
{
    public class TeamMemberBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var request = bindingContext.ActionContext.HttpContext.Request;
            String bodyData = BodyRequestExtractor.ExtractAsJson(request).Result;
            TeamMember teamMember = JsonConvert.DeserializeObject<TeamMember>(bodyData);
            FillTeamMemberAdditionalInfo(teamMember);
            bindingContext.Result = ModelBindingResult.Success(teamMember);
            return Task.CompletedTask;
        }

        
        /// <summary>
        /// Assign to the input object the additional info object that correspond to his member type
        /// </summary>
        /// <param name="teamMemberFromBody"> Object to fill</param>
        /// <returns></returns>        
        public static void FillTeamMemberAdditionalInfo(TeamMember teamMemberFromBody)
        {
            if(teamMemberFromBody == null || teamMemberFromBody.AdditionalInfo == null)
            {
                return;
            }
            // AdditionalInfo saved as JsonString
            String additionalInfoAsJson = teamMemberFromBody.AdditionalInfo.ToString();
            switch (teamMemberFromBody.MemberType)
            {
                case MemberType.Staff:
                    {
                        teamMemberFromBody.AdditionalInfo = JsonConvert.DeserializeObject<StaffAdditionalInfo>(additionalInfoAsJson);
                        break;
                    }
                case MemberType.Player:
                    {
                        teamMemberFromBody.AdditionalInfo = JsonConvert.DeserializeObject<PlayerAdditionalInfo>(additionalInfoAsJson);
                        break;
                    }
            }

        }
    }
}
