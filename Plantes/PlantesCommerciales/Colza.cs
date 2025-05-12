public class Colza:PlanteCommerciale {

    public Colza(Terrain terrain) : base(terrain) {
        Nom ="Colza";
        Type = "Annuelle";
        Saisons = new List<string> {"Printemps", "Automne"};
        TerrainPrefere = "Forêt";
        Espacement = 3;
        VitesseCroissance = 0.2;
        BesoinsEnEau = 3;
        BesoinsEnLuminosite = "Luminosité forte";
        TemperaturesPreferees = new List<int> {8, 22};
        MaladiesPossibles = new List<string> {"hernie du chou, sclérotinia"};
        ProbabilitesMaladies = new List<int> {20, 30};
        EsperanceDeVie = 0.25;
        Rendement = 30;
        EtatPlante ="Germination";
    }
}