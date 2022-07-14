using System;
using System.Collections.Generic;
using System.Text;

namespace AppliSoccerObjects.ActionResults
{
    public enum Status { Success, Fail, Unknown }
    public class ActionResult
    {
        public Status Status { get; set; }
    }
}
