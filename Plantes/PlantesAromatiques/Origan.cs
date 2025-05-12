public class Origan:PlanteAromatique {

    public Origan(Terrain terrain) : base(terrain) {
        Nom ="Origan";
        Type = "Vivace";
        Saisons = new List<string> {"Été"};
        TerrainPrefere = "Désert";
        Espacement = 2;
        VitesseCroissance = 0.167;
        BesoinsEnEau = 0.5;
        BesoinsEnLuminosite = "Luminosité forte";
        TemperaturesPreferees = new List<int> {18, 30};
        MaladiesPossibles = new List<string> {"mildiou"};
        ProbabilitesMaladies = new List<int> {25};
        EsperanceDeVie = 1.25;
        Rendement = 10;
        EtatPlante ="Germination";
    }
}