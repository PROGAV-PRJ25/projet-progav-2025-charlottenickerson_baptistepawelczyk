public class Ananas:PlanteComestible {

    public Ananas(Terrain terrain) : base(terrain) {
        Nom ="Ananas";
        Type = "Vivace";
        Saisons = new List<string> {"Été"};
        TerrainPrefere = "Cratère";
        Espacement = 6;
        VitesseCroissance = 24;
        BesoinsEnEau = 6;
        BesoinsEnLuminosite = "Luminosité forte";
        TemperaturesPreferees = new List<int> {20, 30};
        MaladiesPossibles = new List<string> {"pourriture", "anthracnose"};
        ProbabilitesMaladies = new List<int> {15, 10};
        EsperanceDeVie = 5;
        Rendement = 1;
        EtatPlante ="Germination";
    }
}