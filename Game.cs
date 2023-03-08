using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

class Program {
    static void Main(string[] args) {
        while (true) {
            // Charger les questions depuis le fichier JSON
            string json = File.ReadAllText("questions.json");
            List<Question> questions = JsonConvert.DeserializeObject<List<Question>>(json);

            // Mélanger les questions
            questions = questions.OrderBy(q => Guid.NewGuid()).ToList();

            // Jouer le jeu
            int points = 0;
            foreach (Question question in questions) {
                Console.WriteLine(question.Text);
                for (int i = 0; i < question.Reponses.Length; i++) {
                    Console.WriteLine($"{i + 1}. {question.Reponses[i]}");
                }

                Console.Write("Réponse : ");
                string reponseUtilisateur = Console.ReadLine();
                if (int.TryParse(reponseUtilisateur, out int choix) && choix >= 1 && choix <= 3) {
                    if (question.VerifierReponse(question.Reponses[choix - 1])) {
                        Console.WriteLine("Bonne réponse !");
                        points++;
                    }
                    else {
                        Console.WriteLine($"Mauvaise réponse. La réponse correcte est : {question.ReponseCorrecte}");
                        break;
                    }
                }
                else {
                    Console.WriteLine("Entrée invalide.");
                }

                Console.WriteLine();
            }

            // Afficher le score final
            Console.WriteLine($"Vous avez obtenu {points} point(s) sur {questions.Count}.");

            // Proposer de recommencer le jeu si l'utilisateur a obtenu tous les points
            if (points == questions.Count) {
                Console.Write("Bravo ! Vous avez obtenu tous les points. Voulez-vous rejouer ? (Oui/Non) ");
                string reponse = Console.ReadLine().ToLower();
                if (reponse == "non") {
                    break;
                }
            }
            else {
                Console.Write("Dommage ! Vous avez perdu. Voulez-vous recommencer ? (Oui/Non) ");
                string reponse = Console.ReadLine().ToLower();
                if (reponse == "non") {
                    break;
                }
            }
        }
    }
}