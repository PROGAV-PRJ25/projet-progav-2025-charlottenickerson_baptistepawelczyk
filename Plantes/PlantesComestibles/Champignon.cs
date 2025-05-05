public class Champignon:PlanteComestible {

    public Champignon(Terrain terrain) : base(terrain) {
        Nom ="Champignon";
        Type = "Vivace";
        Saisons = new List<string> {"Automne", "Hiver"};
        TerrainPrefere = "Forêt";
        Espacement = 2;
        VitesseCroissance = 0.5;
        BesoinsEnEau = 5;
        BesoinsEnLuminosite = "Luminosité faible";
        TemperaturesPreferees = new List<int> {10, 20};
        MaladiesPossibles = new List<string> {"mildiou", "pourriture"};
        ProbabilitesMaladies = new List<int> {5, 10};
        EsperanceDeVie = 1;
        Rendement = 10;
        EtatPlante ="Germination";
    }
}