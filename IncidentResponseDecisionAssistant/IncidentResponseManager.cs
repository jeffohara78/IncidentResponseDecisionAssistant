using System;
using System.Collections.Generic;

namespace IncidentResponseDecisionAssistant
{
    public class IncidentResponseManager
    {
        public void StartIncidentAssessment()
        {
            Console.WriteLine("\n==========================================");
            Console.WriteLine("      INCIDENT RESPONSE DECISION ASSISTANT");
            Console.WriteLine("==========================================");
            Console.WriteLine("This guided assistant helps a user think through");
            Console.WriteLine("a possible cybersecurity incident and determine");
            Console.WriteLine("what response steps should happen next.");
            Console.WriteLine();
            Console.WriteLine("It does not replace an incident response team,");
            Console.WriteLine("legal counsel, management, or cybersecurity experts.");
            Console.WriteLine("Instead, it helps organize the first decision points.");
            Console.WriteLine();
            Console.WriteLine("Enter 0 at any prompt to cancel and return to the main menu.");
            Console.WriteLine();

            IncidentAssessment assessment = new IncidentAssessment();

            string incidentType = GetIncidentTypeFromUser();

            if (incidentType == "Cancel")
            {
                Console.WriteLine("Incident assessment cancelled.");
                return;
            }

            assessment.IncidentType = incidentType;

            string affectedSystem = GetTextOrCancel(
                "\nWhat system, account, device, or business area appears affected? ");

            if (affectedSystem == "0")
            {
                Console.WriteLine("Incident assessment cancelled.");
                return;
            }

            assessment.AffectedSystem = affectedSystem;

            assessment.IncidentStillActive = GetYesNoOrCancel(
                "\nIs the incident still happening right now?");

            if (assessment.IncidentStillActive == false && WasLastChoiceCancel())
            {
                Console.WriteLine("Incident assessment cancelled.");
                return;
            }

            assessment.BusinessOperationsImpacted = GetYesNoOrCancel(
                "\nIs normal business work currently disrupted?");

            if (assessment.BusinessOperationsImpacted == false && WasLastChoiceCancel())
            {
                Console.WriteLine("Incident assessment cancelled.");
                return;
            }

            assessment.SensitiveDataInvolved = GetYesNoOrCancel(
                "\nCould sensitive, personal, financial, customer, or company data be involved?");

            if (assessment.SensitiveDataInvolved == false && WasLastChoiceCancel())
            {
                Console.WriteLine("Incident assessment cancelled.");
                return;
            }

            assessment.MultipleUsersAffected = GetYesNoOrCancel(
                "\nAre multiple users, systems, or departments affected?");

            if (assessment.MultipleUsersAffected == false && WasLastChoiceCancel())
            {
                Console.WriteLine("Incident assessment cancelled.");
                return;
            }

            CalculateSeverity(assessment);

            ResponseRecommendation recommendation = BuildRecommendation(assessment);

            DisplayIncidentReport(assessment, recommendation);
        }

        private bool lastChoiceWasCancel = false;

        private bool WasLastChoiceCancel()
        {
            return lastChoiceWasCancel;
        }

        private string GetIncidentTypeFromUser()
        {
            while (true)
            {
                Console.WriteLine("Choose the type of incident that best matches the situation:");
                Console.WriteLine();
                Console.WriteLine("1. Phishing or suspicious email");
                Console.WriteLine("2. Malware or ransomware concern");
                Console.WriteLine("3. Lost or stolen device");
                Console.WriteLine("4. Unauthorized account access");
                Console.WriteLine("5. Data exposure or accidental sharing");
                Console.WriteLine("6. System outage or unavailable service");
                Console.WriteLine("7. Other / unsure");
                Console.WriteLine("0. Cancel and return to main menu");
                Console.Write("\nChoose option 0 through 7: ");

                string choice = Console.ReadLine();

                if (choice == "1") return "Phishing or Suspicious Email";
                if (choice == "2") return "Malware or Ransomware Concern";
                if (choice == "3") return "Lost or Stolen Device";
                if (choice == "4") return "Unauthorized Account Access";
                if (choice == "5") return "Data Exposure or Accidental Sharing";
                if (choice == "6") return "System Outage or Unavailable Service";
                if (choice == "7") return "Other / Unsure";
                if (choice == "0") return "Cancel";

                Console.WriteLine("Invalid option. Please choose 0 through 7.");
            }
        }

        private bool GetYesNoOrCancel(string question)
        {
            while (true)
            {
                Console.WriteLine(question);
                Console.WriteLine("1. Yes");
                Console.WriteLine("2. No");
                Console.WriteLine("0. Cancel and return to main menu");
                Console.Write("Choose option 0 through 2: ");

                string choice = Console.ReadLine();

                lastChoiceWasCancel = false;

                if (choice == "1")
                {
                    return true;
                }
                else if (choice == "2")
                {
                    return false;
                }
                else if (choice == "0")
                {
                    lastChoiceWasCancel = true;
                    return false;
                }

                Console.WriteLine("Invalid option. Please choose 0, 1, or 2.");
            }
        }

        private string GetTextOrCancel(string prompt)
        {
            Console.Write(prompt);

            string input = Console.ReadLine().Trim();

            return input;
        }

        private void CalculateSeverity(IncidentAssessment assessment)
        {
            int score = 0;

            if (assessment.IncidentStillActive)
            {
                score += 20;
            }

            if (assessment.BusinessOperationsImpacted)
            {
                score += 25;
            }

            if (assessment.SensitiveDataInvolved)
            {
                score += 30;
            }

            if (assessment.MultipleUsersAffected)
            {
                score += 15;
            }

            if (assessment.IncidentType == "Malware or Ransomware Concern")
            {
                score += 20;
            }
            else if (assessment.IncidentType == "Unauthorized Account Access")
            {
                score += 15;
            }
            else if (assessment.IncidentType == "Data Exposure or Accidental Sharing")
            {
                score += 15;
            }
            else if (assessment.IncidentType == "Lost or Stolen Device")
            {
                score += 10;
            }

            assessment.SeverityScore = score;

            if (score >= 70)
            {
                assessment.SeverityLevel = "Critical";
            }
            else if (score >= 45)
            {
                assessment.SeverityLevel = "High";
            }
            else if (score >= 25)
            {
                assessment.SeverityLevel = "Moderate";
            }
            else
            {
                assessment.SeverityLevel = "Low";
            }
        }

        private ResponseRecommendation BuildRecommendation(IncidentAssessment assessment)
        {
            ResponseRecommendation recommendation = new ResponseRecommendation();

            recommendation.Summary = $"Incident appears to be {assessment.SeverityLevel} severity based on the answers provided.";

            recommendation.ImmediateActions.Add("Document what happened, when it was noticed, and who reported it.");
            recommendation.ImmediateActions.Add("Notify the appropriate IT, security, or management contact.");

            if (assessment.IncidentStillActive)
            {
                recommendation.ImmediateActions.Add("Avoid making unnecessary changes until evidence and impact are understood.");
                recommendation.ImmediateActions.Add("Preserve screenshots, emails, alerts, error messages, or logs if available.");
            }

            if (assessment.IncidentType == "Phishing or Suspicious Email")
            {
                recommendation.ImmediateActions.Add("Do not click links, open attachments, or reply to the suspicious message.");
                recommendation.ImmediateActions.Add("Report the email to IT or security and keep the message available for review.");
                recommendation.FollowUpActions.Add("Review whether any user clicked links or entered credentials.");
            }
            else if (assessment.IncidentType == "Malware or Ransomware Concern")
            {
                recommendation.ImmediateActions.Add("Disconnect the affected device from the network if instructed by policy or IT.");
                recommendation.ImmediateActions.Add("Do not delete files or wipe the system before evidence is reviewed.");
                recommendation.FollowUpActions.Add("Review endpoint protection alerts and determine whether other systems are affected.");
            }
            else if (assessment.IncidentType == "Lost or Stolen Device")
            {
                recommendation.ImmediateActions.Add("Report the missing device immediately to IT, security, or management.");
                recommendation.ImmediateActions.Add("Determine whether the device had encryption, remote wipe, or sensitive data.");
                recommendation.FollowUpActions.Add("Reset passwords or revoke sessions associated with the missing device if needed.");
            }
            else if (assessment.IncidentType == "Unauthorized Account Access")
            {
                recommendation.ImmediateActions.Add("Change or reset the affected account password.");
                recommendation.ImmediateActions.Add("Review account activity for unusual logins, changes, or data access.");
                recommendation.FollowUpActions.Add("Enable or verify multi-factor authentication for the affected account.");
            }
            else if (assessment.IncidentType == "Data Exposure or Accidental Sharing")
            {
                recommendation.ImmediateActions.Add("Identify what information was exposed and who may have received it.");
                recommendation.ImmediateActions.Add("Stop further sharing if possible without destroying evidence.");
                recommendation.FollowUpActions.Add("Escalate to management, legal, privacy, or compliance contacts if sensitive data was involved.");
            }
            else if (assessment.IncidentType == "System Outage or Unavailable Service")
            {
                recommendation.ImmediateActions.Add("Identify which users or business processes are affected.");
                recommendation.ImmediateActions.Add("Check whether backups, failover systems, or manual workarounds are available.");
                recommendation.FollowUpActions.Add("Document outage duration and business impact after service is restored.");
            }
            else
            {
                recommendation.ImmediateActions.Add("Gather basic facts and avoid guessing about cause or impact.");
                recommendation.FollowUpActions.Add("Escalate to IT or security for review and classification.");
            }

            if (assessment.SensitiveDataInvolved)
            {
                recommendation.ImmediateActions.Add("Escalate quickly because sensitive data may require special handling or reporting.");
                recommendation.FollowUpActions.Add("Determine whether privacy, legal, regulatory, or customer notification obligations apply.");
            }

            if (assessment.BusinessOperationsImpacted)
            {
                recommendation.ImmediateActions.Add("Inform management that normal business operations may be affected.");
                recommendation.FollowUpActions.Add("Track downtime, affected departments, and recovery progress.");
            }

            if (assessment.MultipleUsersAffected)
            {
                recommendation.ImmediateActions.Add("Consider whether the incident may be broader than a single user or device.");
                recommendation.FollowUpActions.Add("Look for similar reports, alerts, or patterns across other systems.");
            }

            recommendation.FollowUpActions.Add("After containment and recovery, document lessons learned and recommended improvements.");

            return recommendation;
        }

        private void DisplayIncidentReport(
            IncidentAssessment assessment,
            ResponseRecommendation recommendation)
        {
            Console.WriteLine("\n==========================================");
            Console.WriteLine("          INCIDENT RESPONSE REPORT");
            Console.WriteLine("==========================================");
            Console.WriteLine($"Incident Type: {assessment.IncidentType}");
            Console.WriteLine($"Affected Area: {assessment.AffectedSystem}");
            Console.WriteLine($"Severity Score: {assessment.SeverityScore}");
            Console.WriteLine($"Severity Level: {assessment.SeverityLevel}");
            Console.WriteLine();

            Console.WriteLine("--- Key Factors ---");
            Console.WriteLine($"Incident Still Active: {(assessment.IncidentStillActive ? "Yes" : "No")}");
            Console.WriteLine($"Business Operations Impacted: {(assessment.BusinessOperationsImpacted ? "Yes" : "No")}");
            Console.WriteLine($"Sensitive Data Possibly Involved: {(assessment.SensitiveDataInvolved ? "Yes" : "No")}");
            Console.WriteLine($"Multiple Users/Systems Affected: {(assessment.MultipleUsersAffected ? "Yes" : "No")}");
            Console.WriteLine();

            Console.WriteLine("--- Summary ---");
            Console.WriteLine(recommendation.Summary);
            Console.WriteLine();

            Console.WriteLine("--- Immediate Response Actions ---");

            foreach (string action in recommendation.ImmediateActions)
            {
                Console.WriteLine($"- {action}");
            }

            Console.WriteLine();
            Console.WriteLine("--- Follow-Up Actions ---");

            foreach (string action in recommendation.FollowUpActions)
            {
                Console.WriteLine($"- {action}");
            }
        }
    }
}