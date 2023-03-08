class Question {
    public string Text { get; set; }
    public string[] Reponses { get; set; }
    public string ReponseCorrecte { get; set; }

    public bool VerifierReponse(string reponse) {
        return reponse == ReponseCorrecte;
    }
}