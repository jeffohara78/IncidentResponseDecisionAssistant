using System.Collections.Generic;

namespace IncidentResponseDecisionAssistant
{
    public class ResponseRecommendation
    {
        public string Summary { get; set; }

        public List<string> ImmediateActions { get; set; }

        public List<string> FollowUpActions { get; set; }

        public ResponseRecommendation()
        {
            Summary = "";
            ImmediateActions = new List<string>();
            FollowUpActions = new List<string>();
        }
    }
}