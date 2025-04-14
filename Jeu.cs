class Jeu
{
    Joueur Joueur;
    Potager Potager;
    Meteo Meteo;
    SimulateurTemps Simulateur;
    GestionnaireUrgence GestionnaireUrgence;
    AfficheurConsole UI;

    void Demarrer();
    void ExecuterTourClassique();
    void ExecuterTourUrgence();
    void Sauvegarder();
    void Charger();
}