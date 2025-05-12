public class Cycas_du_Japon:PlanteOrnementale {

    public Cycas_du_Japon(Terrain terrain) : base(terrain) {
        Nom ="Cycas du Japon";
        Type = "Vivace";
        Saisons = new List<string> {"Printemps", "Été"};
        TerrainPrefere = "Désert";
        Espacement = 3;
        VitesseCroissance = 0.3;
        BesoinsEnEau = 1.5;
        BesoinsEnLuminosite = "Luminosité forte";
        TemperaturesPreferees = new List<int> {18, 30};
        MaladiesPossibles = new List<string> {"cochenilles", "pourriture du collet"};
        ProbabilitesMaladies = new List<int> {5, 10};
        EsperanceDeVie = 4;
        Rendement = 2;
        EtatPlante ="Germination";
    }
}