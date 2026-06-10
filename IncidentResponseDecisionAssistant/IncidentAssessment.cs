namespace IncidentResponseDecisionAssistant
{
    public class IncidentAssessment
    {
        public string IncidentType { get; set; }

        public string AffectedSystem { get; set; }

        public bool SensitiveDataInvolved { get; set; }

        public bool BusinessOperationsImpacted { get; set; }

        public bool IncidentStillActive { get; set; }

        public bool MultipleUsersAffected { get; set; }

        public int SeverityScore { get; set; }

        public string SeverityLevel { get; set; }

        public IncidentAssessment()
        {
            IncidentType = "";
            AffectedSystem = "";
            SeverityLevel = "Not Assessed";
        }
    }
}