public class Romarin:PlanteAromatique {

    public Romarin(Terrain terrain) : base(terrain) {
        Nom ="Romarin";
        Type = "Vivace";
        Saisons = new List<string> {"Printemps", "Été"};
        TerrainPrefere = "Montagne";
        Espacement = 3;
        VitesseCroissance = 0.233;
        BesoinsEnEau = 0.2;
        BesoinsEnLuminosite = "Luminosité forte";
        TemperaturesPreferees = new List<int> {20, 35};
        MaladiesPossibles = new List<string> {"pourriture racinaire"};
        ProbabilitesMaladies = new List<int> {25};
        EsperanceDeVie = 1.5;
        Rendement = 10;
        EtatPlante ="Germination";
    }
}