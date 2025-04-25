using System;

public class Saison
{
    // Énumération des différentes saisons
    public enum TypeSaison
    {
        Printemps,
        Ete,
        Automne,
        Hiver
    }
    
    // Nombre de semaines par saison
    public const int SemainesParSaison = 14;
    
    // Saison actuelle
    private TypeSaison saisonActuelle;
    
    // Constructeur par défaut
    public Saison()
    {
        // La saison de départ est Automne
        saisonActuelle = TypeSaison.Automne;
    }
    
    // Méthode pour mettre à jour la saison en fonction du numéro de semaine
    public void MettreAJour(int numeroSemaine)
    {
        // Calcul de la saison en fonction du numéro de semaine
        // Semaine 1-14: Automne, 15-28: Hiver, 29-42: Printemps, 43-56: Été
        // Puis le cycle recommence
        int cycleSaison = ((numeroSemaine - 1) / SemainesParSaison) % 4;
        
        switch (cycleSaison)
        {
            case 0:
                saisonActuelle = TypeSaison.Automne;
                break;
            case 1:
                saisonActuelle = TypeSaison.Hiver;
                break;
            case 2:
                saisonActuelle = TypeSaison.Printemps;
                break;
            case 3:
                saisonActuelle = TypeSaison.Ete;
                break;
        }
    }
    
    // Méthode pour obtenir la saison actuelle
    public TypeSaison GetSaisonActuelle()
    {
        return saisonActuelle;
    }
    
    // Méthode pour obtenir le nom de la saison actuelle en français
    public string GetNomSaison()
    {
        switch (saisonActuelle)
        {
            case TypeSaison.Printemps:
                return "Printemps";
            case TypeSaison.Ete:
                return "Été";
            case TypeSaison.Automne:
                return "Automne";
            case TypeSaison.Hiver:
                return "Hiver";
            default:
                return "Inconnu";
        }
    }
}