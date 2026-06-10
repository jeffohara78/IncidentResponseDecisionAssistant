/* Jeff O'Hara
 * 6/10/2026
 * 
 * Guides users through a series of questions to assess a potential cybersecurity incident, evaluate its severity, 
 * and identify key risk factors such as business impact, sensitive data exposure, and affected systems. 
 * Based on the responses, it generates a severity rating along with recommended immediate and 
 * follow-up actions to support incident response and recovery efforts.
 */

using System;

namespace IncidentResponseDecisionAssistant
{
    class Program
    {
        static void Main(string[] args)
        {
            IncidentResponseManager manager = new IncidentResponseManager();

            bool running = true;

            while (running)
            {
                Console.WriteLine("\n==========================================");
                Console.WriteLine("   INCIDENT RESPONSE DECISION ASSISTANT");
                Console.WriteLine("==========================================");
                Console.WriteLine("Guide users through early cybersecurity");
                Console.WriteLine("incident response questions and response steps.");
                Console.WriteLine();
                Console.WriteLine("1. Start incident assessment");
                Console.WriteLine("2. Exit");
                Console.Write("\nChoose an option 1 through 2: ");

                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    manager.StartIncidentAssessment();
                }
                else if (choice == "2")
                {
                    running = false;
                    Console.WriteLine("Exiting Incident Response Decision Assistant.");
                }
                else
                {
                    Console.WriteLine("Invalid option. Please choose 1 or 2.");
                }
            }
        }
    }
}