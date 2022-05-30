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
    public class UserBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var request = bindingContext.ActionContext.HttpContext.Request;
            String bodyData = BodyRequestExtractor.ExtractAsJson(request).Result;
            User user = JsonConvert.DeserializeObject<User>(bodyData);
            TeamMemberBinder.FillTeamMemberAdditionalInfo(user.TeamMember);
            bindingContext.Result = ModelBindingResult.Success(user);
            return Task.CompletedTask;
        }

    }
}
