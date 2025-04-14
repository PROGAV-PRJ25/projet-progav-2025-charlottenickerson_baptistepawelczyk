class SimulateurTemps
{
    DateTime DateCourante;
    ModeJeu Mode; // Enum : Classique, Urgence

    void AvancerTemps();
    bool DeclencherUrgence();
}